#/bin/bash
####################################################################################
#
# getipbr0.sh Script for execution on remote/target computer
# @author: Boris Kajganic (boris.kajganic@fluenceenergy.com)
# @Version: 2021-02-08
# NOTE: Following packets must be installed:
#    ----
# You will need write permissions in the directory where you executing this script
#
####################################################################################



localhelp () {
  echo "Usage:"
  echo "  getipbr0.sh"  
  echo "    Get static ip address from local computer (br0)"  
  echo "  getipbr0.sh remote_IP rootpwd"
  echo "  getipbr0.sh file_with_remote_IPs rootpwd"  
  echo "    Get static ip address from remote computer (br0)"
}


getIPbr0 () {
  #tray to get static ip from self-configuration
  CIP=$(ip -f inet -o addr show br0 | grep "scope global br0" | awk 'NR==1{print $4}')

  if [[ -n "$CIP" ]] ; then
    echo "${CIP}"
  else
    echo "-----"
  fi
}

copyAndExecuteScript () {
  sshpass -p $PASSWD scp -p $SCRIPTFILENAME root@$CONTROLLERIP:~/.
  REMOTECMD="~/$SCRIPTNAME"
  #echo "Ececute remotely: $REMOTECMD" 
  sshpass -p $PASSWD ssh -o StrictHostKeyChecking=no root@$CONTROLLERIP $REMOTECMD
}

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
  getIPbr0
fi

