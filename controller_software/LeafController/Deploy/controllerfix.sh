#/bin/bash
####################################################################################
#
# Fix Core telco raclk Controller
# @author: Boris Kajganic (boris.kajganic@fluenceenergy.com)
# @Version: 2021-10-10
# NOTE: Following packets must be installed:
#    sshpass
# You will need write permissions in the directory where you executing this script
#
####################################################################################

SCRIPTFILENAME=`which $0`
SCRIPTNAME=${SCRIPTFILENAME##*/}
SCRIPTDIR=${SCRIPTFILENAME%/*}

#echo "SCRIPTFILENAME = ${SCRIPTFILENAME}"
#echo "SCRIPTNAME = ${SCRIPTNAME}"
#echo "SCRIPTDIR = ${SCRIPTDIR}"


#pushd $(pwd) 
pushd -n $(pwd) > /dev/null 2>&1
cd ${SCRIPTDIR} > /dev/null 2>&1

localhelp () {
  echo "Usage:"
  echo "  ${SCRIPTNAME}"
  echo "    Fix Core telco raclk Controller"
  echo "  ${SCRIPTNAME} remote_IP rootpwd"
  echo "  ${SCRIPTNAME} file_with_remote_IPs rootpwd"
  echo "    Fix Core telco raclk Controller as remote computer"
  echo "  ${SCRIPTNAME} help|--help|-h"
  echo "    print help"  
}

copyAndExecuteScript () {
  sshpass -p $PASSWD scp -o StrictHostKeyChecking=no $SCRIPTNAME root@$CONTROLLERIP:~/.

  REMOTECMD="~/${SCRIPTNAME}"
  #echo "Ececute remotely: $REMOTECMD" 
  sshpass -p $PASSWD ssh -o StrictHostKeyChecking=no root@$CONTROLLERIP $REMOTECMD
}

 fixModbusConf() {
  #echo "Fix Modbus Conf"
  cp /usr/local/share/fluence/controllino_modbus_octe.conf /usr/local/share/fluence/controllino_modbus_coreX.conf
  sed -i -e "s/10.11.8.171/$1.171/" /usr/local/share/fluence/controllino_modbus_coreX.conf
  sed -i -e "s/10.11.8.181/$1.181/" /usr/local/share/fluence/controllino_modbus_coreX.conf
  cp /usr/local/share/fluence/controllino_modbus_coreX.conf /usr/local/share/fluence/controllino_modbus.conf
  systemctl restart fluence-modbus
}

stopServices () {
  #echo "Stop services"
  systemctl stop nut-client >/dev/null 2>&1
  systemctl stop nut-server >/dev/null 2>&1
  systemctl stop nut-driver >/dev/null 2>&1
  systemctl stop nut-monitor >/dev/null 2>&1
  systemctl disable nut-client >/dev/null 2>&1
  systemctl disable nut-server >/dev/null 2>&1
  systemctl disable nut-monitor >/dev/null 2>&1
  systemctl disable nut-driver >/dev/null 2>&1
}

fixCoreController () {
  stopServices
}

fixNodeController () {
  stopServices
}

fixCTRController () {
  stopServices
  fixModbusConf $1
}

fixBasedOnControllerType () {
  PORTID=$(lldpcli sho nei | grep PortID | awk 'NR==1{print $3}')
  echo "PortID = ${PORTID}"
  if [ -z "${PORTID}" ] ; then
    echo "Unknown Controller"
    #exit 0
  else
    #echo "Port = ${PORTID}"
    if [[ "${PORTID}"  == "FastEthernet0/14" ]]; then
      echo "Node Controller 1"
      fixNodeController
    elif [[ "${PORTID}"  == "FastEthernet0/15" ]]; then
      echo "Node Controller 2"
      fixNodeController
    elif [[ "${PORTID}"  == "FastEthernet0/16" ]]; then
      echo "Node Controller 3"
      fixNodeController
    elif [[ "${PORTID}"  == "FastEthernet0/17" ]]; then
      echo "Node Controller 4"
      fixNodeController
    elif [[ "${PORTID}"  == "FastEthernet0/23" ]]; then
      echo "CTR Controller"
      fixCTRController $1
    elif [[ "${PORTID}"  == "FastEthernet0/24" ]]; then
      echo "Core Controller"
      fixCoreController
    #elif [[ "${PORTID}" == "FastEthernet0/1" || "${PORTID}" == "FastEthernet0/2" || "${PORTID}" == "FastEthernet0/3" || "${PORTID}" == "FastEthernet0/4" || "${PORTID}" == "FastEthernet0/5" || "${PORTID}" == "FastEthernet0/6" || "${PORTID}" == "FastEthernet0/7" || "${PORTID}" == "FastEthernet0/8" ]]; then
    #  echo "Cube Controller"
    #elif [[ "${PORTID}" == "lan3" || "${PORTID}" == "eth2" ]]; then
    #  echo "Cube Controller"
    else
      echo "Unknown Controller"
    fi
  fi
}

controllerMain () {
  #echo "fixCoreTelcoRackController"
  CIPdyn=$(ip -f inet -o addr show br0 | grep "scope global secondary dynamic br0" | awk 'NR==1{print $4}' |cut -d/ -f 1)
  if [[ -z "$CIPdyn" ]] ; then
    CIPdyn=$(ip -f inet -o addr show br0 | grep "scope global dynamic br0" | awk 'NR==1{print $4}' |cut -d/ -f 1)
  fi
  if [[ -n "$CIPdyn" ]] ; then
    #echo "IPdyn exists"
    #make XXX.XXX.XXX.161 address
    baseIP=`echo $CIPdyn | cut -d"." -f1-3`
    CIPdyn=$(echo $baseIP".161")
    CIPstatic=$(ip -f inet -o addr show br0 | grep "scope global br0" | awk 'NR==1{print $4}' |cut -d/ -f 1)
    if [[ -n "$CIPstatic" ]] ; then
      if [[ "$CIPstatic" == "$CIPdyn" ]] ; then
        echo "CTR Controller IP: $CIPdyn"
        fixCTRController $baseIP
        popd > /dev/null 2>&1
        exit 0
      fi
    fi
    fixBasedOnControllerType $baseIP
  fi
}

for var in "$@"
do
  #echo "$var"
  if [[ "${var}" == "help" || "${var}" == "--help" || "${var}" == "-h" ]]; then
    localhelp
    popd > /dev/null 2>&1
    exit 0
  fi
  #if [ "${var}" == "clear" ]; then
  #  clearData
  #fi
  minOneVarFound=1  # IP, password, help
done

SCRIPTFILENAME=`which $0`
SCRIPTNAME=${SCRIPTFILENAME##*/}
SCRIPTDIR=${SCRIPTFILENAME%/*}

PASSWD=$2
FILE=$1
CONTROLLERIP=$1

if [[ -f "$FILE" ]]; then
  if [ -z "$PASSWD" ]; then
    echo "Root password is missing"
    localhelp 
    exit 1
  fi
  
  for CONTROLLERIP in $(cat $FILE); do
    #echo "TEST FOR ${CONTROLLERIP}"
    # The -c 1 means send one packet, and the -t 1 means a 1 second timeout    
    #ping -c 1 -t 1 ${IP_ADDRESS} >> ${IP_ADDRESS}.log 2>&1
    #ping -c 1 -t 1 ${CONTROLLERIP}
    echo "$CONTROLLERIP  $(copyAndExecuteScript) "
  done
  
elif [[ $CONTROLLERIP =~ ^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+$ ]]; then
  if [ -z "$PASSWD" ]; then
    echo "Root password is missing"
    localhelp 
    exit 1
  fi
  echo "$CONTROLLERIP  $(copyAndExecuteScript) "
else
  controllerMain
fi
popd > /dev/null 2>&1
