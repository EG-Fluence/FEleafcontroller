#/bin/bash
####################################################################################
#
# Get controller on local or remote computer
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
  echo "  controllertype.sh"
  echo "    Get controller type on local computer: Node Controller | Core Controller | Cube Controller | TR Controller | Unknown Controller"
  echo "  controllertype.sh remote_IP rootpwd"
  echo "  controllertype.sh file_with_remote_IPs rootpwd"
  echo "    Get controller type on remote computer: Node Controller | Core Controller | Cube Controller | TR Controller | Unknown Controller"
  echo "  controllertype.sh help|--help|-h"
  echo "    print help"  
}

copyAndExecuteScript () {
  sshpass -p $PASSWD scp -o StrictHostKeyChecking=no $SCRIPTNAME root@$CONTROLLERIP:~/.

  REMOTECMD="~/${SCRIPTNAME}"
  #echo "Ececute remotely: $REMOTECMD" 
  sshpass -p $PASSWD ssh -o StrictHostKeyChecking=no root@$CONTROLLERIP $REMOTECMD
}

findControllerType() {
  FILE=/boot/bananapi/bpi-r2/linux/uImage
  if [[ -f "$FILE" ]]; then
    echo "    CONTROLLER is BananaPiR2"
  fi
  FILE=/boot/zImage
  if [[ -f "$FILE" ]]; then
    echo "    CONTROLLER is Modberry500M3"
  fi
  FILE=/boot/Image
  if [[ -f "$FILE" ]]; then
    echo "    CONTROLLER is ESPRESSObin-Ultra"
  fi
}


findControllerFromConnectedPort () {
  PORTID=$(lldpcli sho nei | grep PortID | awk 'NR==1{print $3}')
  echo "PortID = ${PORTID}"
  if [ -z "${PORTID}" ] ; then
    echo "Unknown Controller"
    #exit 0
  else
    #echo "Port = ${PORTID}"
    if [[ "${PORTID}"  == "FastEthernet0/14" ]]; then
      echo "Node Controller 1"
    elif [[ "${PORTID}"  == "FastEthernet0/15" ]]; then
      echo "Node Controller 2"
    elif [[ "${PORTID}"  == "FastEthernet0/16" ]]; then
      echo "Node Controller 3"
    elif [[ "${PORTID}"  == "FastEthernet0/17" ]]; then
      echo "Node Controller 4"
    elif [[ "${PORTID}"  == "FastEthernet0/23" ]]; then
      echo "CTR Controller"
    elif [[ "${PORTID}"  == "FastEthernet0/24" ]]; then
      echo "Core Controller"
    elif [[ "${PORTID}" == "FastEthernet0/1" || "${PORTID}" == "FastEthernet0/2" || "${PORTID}" == "FastEthernet0/3" || "${PORTID}" == "FastEthernet0/4" || "${PORTID}" == "FastEthernet0/5" || "${PORTID}" == "FastEthernet0/6" || "${PORTID}" == "FastEthernet0/7" || "${PORTID}" == "FastEthernet0/8" ]]; then
      echo "Cube Controller"
    elif [[ "${PORTID}" == "lan3" || "${PORTID}" == "eth2" ]]; then
      echo "Cube Controller"
    else
      echo "Unknown Controller"
    fi
  fi
  findControllerType
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
  findControllerFromConnectedPort
fi
popd > /dev/null 2>&1
