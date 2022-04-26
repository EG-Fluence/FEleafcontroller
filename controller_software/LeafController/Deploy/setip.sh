#/bin/bash
####################################################################################
#
# Fix br0 & br1 for execution on remote/target computer
# @author: Boris Kajganic (boris.kajganic@fluenceenergy.com)
# @Version: 2021-06-29
# NOTE: Following packets must be installed:
#    sshpass
# You will need write permissions in the directory where you executing this script
#
####################################################################################


setIPbr01 () {

  # If the network is already installed, br1 exists and ip is assigned to the interface
  # br1 is used instead br0, because ESPRESSObin-Ultra comes with predefined br0 (bridge to wifi)
  CIP=$(ip -f inet -o addr show br1 | awk 'NR==1{print $4}' |cut -d/ -f 1)
  if [[ -z "$CIP" ]] ; then
    FirstInstallation=True
  else # this will work because only configurec controller has br0 and br1
    #tray to get static ip from self-configuration
    CIP=$(ip -f inet -o addr show br0 | grep "scope global br0" | awk 'NR==1{print $4}')
  fi

  # use new network defined in /etc/netplan/network.yaml
  # Assign static ip to br0
  if [[ -n "$CIP" ]] ; then
    echo "Assign IP: ${CIP} to br0"
    /usr/local/bin/fluence_set-assigned-ip.py "${CIP}"
  fi
  # Assign internal network to br1
  /usr/local/bin/fluence_set-static-ip.py

  netplan generate
  netplan apply

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
    #ping -c 1 -t 1 ${CONTROLLERIP} >> ${CONTROLLERIP}.log 2>&1
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
  setIPbr01
fi



