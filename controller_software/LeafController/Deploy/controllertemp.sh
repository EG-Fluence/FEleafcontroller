#/bin/bash
####################################################################################
#
# Get controller CPU temperature
# @author: Boris Kajganic (boris.kajganic@fluenceenergy.com)
# @Version: 2021-10-04
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
  echo "    Get controller CPU temperature on local computer"
  echo "  ${SCRIPTNAME} remote_IP rootpwd"
  echo "  ${SCRIPTNAME} file_with_remote_IPs rootpwd"
  echo "    Get controller CPU temperature  on remote computer"
  echo "  ${SCRIPTNAME} help|--help|-h"
  echo "    print help"  
}

copyAndExecuteScript () {
  sshpass -p $PASSWD scp -o StrictHostKeyChecking=no $SCRIPTNAME root@$CONTROLLERIP:~/.

  REMOTECMD="~/${SCRIPTNAME}"
  #echo "Ececute remotely: $REMOTECMD" 
  sshpass -p $PASSWD ssh -o StrictHostKeyChecking=no root@$CONTROLLERIP $REMOTECMD
}

getCPUTemperature () {
  TEMP=$(cat /sys/class/thermal/thermal_zone0/temp)
  echo " Temp= $((TEMP / 1000)) C"
  #popd > /dev/null 2>&1
  #exit 0
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
  getCPUTemperature
fi
popd > /dev/null 2>&1
