#/bin/bash
####################################################################################
#
# Get controller info on local or remote computer
# @author: Boris Kajganic (boris.kajganic@fluenceenergy.com)
# @Version: 2021-06-17
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
  echo "  controllerinfo.sh"
  echo "    Get controller info on local computer: Node Controller | Core Controller | Cube Controller | TR Controller | Unknown Controller"
  echo "  controllerinfo.sh remote_IP rootpwd"
  echo "  controllerinfo file_with_remote_IPs rootpwd"  
  echo "    Get controller info on remote computer: Node Controller | Core Controller | Cube Controller | TR Controller | Unknown Controller"
  echo "  controllerinfo.sh help|--help|-h"
  echo "    print help"  
}

copyAndExecuteScript () {
  sshpass -p $PASSWD scp -o StrictHostKeyChecking=no $SCRIPTNAME root@$CONTROLLERIP:~/.

  REMOTECMD="~/${SCRIPTNAME}"
  #echo "Ececute remotely: $REMOTECMD" 
  sshpass -p $PASSWD ssh -o StrictHostKeyChecking=no root@$CONTROLLERIP $REMOTECMD
}

findControllerInfo () {
  CIP=$(ip -f inet -o addr show br0 | grep "scope global br0" | awk 'NR==1{print $4}')
  PORTID=$(lldpcli sho nei | grep PortID | awk 'NR==1{print $3}')
  #echo "Static IP = ${CIP}, PortID = ${PORTID}"
  if [ -z "${PORTID}" ] ; then
    CTRTYPE="Unknown Controller"
    #exit 0
  else
    #echo "Port = ${PORTID}"
    if [[ "${PORTID}"  == "FastEthernet0/14" ]]; then
      CTRTYPE="Node Controller 1"
    elif [[ "${PORTID}"  == "FastEthernet0/15" ]]; then
      CTRTYPE="Node Controller 2"
    elif [[ "${PORTID}"  == "FastEthernet0/16" ]]; then
      CTRTYPE="Node Controller 3"
    elif [[ "${PORTID}"  == "FastEthernet0/17" ]]; then
      CTRTYPE="Node Controller 4"
    elif [[ "${PORTID}"  == "FastEthernet0/23" ]]; then
      CTRTYPE="CTR Controller"
    elif [[ "${PORTID}"  == "FastEthernet0/24" ]]; then
      CTRTYPE="Core Controller"
    elif [[ "${PORTID}" == "FastEthernet0/1" || "${PORTID}" == "FastEthernet0/2" || "${PORTID}" == "FastEthernet0/3" || "${PORTID}" == "FastEthernet0/4" || "${PORTID}" == "FastEthernet0/5" || "${PORTID}" == "FastEthernet0/6" || "${PORTID}" == "FastEthernet0/7" || "${PORTID}" == "FastEthernet0/8" ]]; then
      CTRTYPE="Cube Controller"
    elif [[ "${PORTID}" == "lan3" || "${PORTID}" == "eth2" ]]; then
      CTRTYPE="Cube Controller"
    else
      CTRTYPE="Unknown Controller"
    fi
  fi

  if [[ "${CTRTYPE}"  == "CTR Controller" || "${CTRTYPE}"  == "Cube Controller" ]]; then
    ControllinoConnect=$(lsusb | grep -o Arduino)
    if [[ -n "$ControllinoConnect" ]] ; then
      CONTROLLINO="Controllino Connected"
    else
      CONTROLLINO="Controllino Error"
    fi
    if [[ "${CTRTYPE}"  == "Cube Controller" ]]; then
      UPSConnect=$(lsusb | grep -o UPS)
      if [[ -n "$UPSConnect" ]] ; then
        UPS="UPS Connected"
      else
        UPS="UPS Error"
      fi
      CSPEED="WLAN$(ethtool wan | grep Speed), LAN3$(ethtool lan3 | grep Speed)"
    fi
  fi
  #echo "Static IP = ${CIP}, PortID = ${PORTID}  $CTRTYPE  $CONTROLLINO  $UPS"
  echo "Static IP = $CIP, $CTRTYPE  $CONTROLLINO  $UPS"
  #echo "  $CTRTYPE  $CONTROLLINO  $UPS"
  if [ -n "${CSPEED}" ] ; then
    echo "            $CSPEED"
 fi
  popd > /dev/null 2>&1
  exit 0
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
  findControllerInfo
fi

