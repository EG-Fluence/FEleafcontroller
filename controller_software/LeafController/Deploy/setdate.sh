#/bin/bash
####################################################################################
#
# Set locale & date on remote computer
# @author: Boris Kajganic (boris.kajganic@fluenceenergy.com)
# @Version: 2021-02-08
# NOTE: Following packets must be installed:
#    sshpass
# You will need write permissions in the directory where you executing this script
#
####################################################################################

localhelp () {
  echo "Usage:"
  echo "  setdate.sh remote_IP|file_with_remote_IPs rootpwd"
  echo "    Set locale & date on remote computer(s)"
}

executeScript () {
  LOCSETTINGS=$(locale | grep LANG=)
  LOCLANG=${LOCSETTINGS#LANG=}
  TIMEZONE=$(cat /etc/timezone)
  DATE=$(LC_TIME="en_US" date -u)
  REMOTECMD="grep -q '# $LOCLANG' /etc/locale.gen && sed -i -e 's/# $LOCLANG/$LOCLANG/' /etc/locale.gen && locale-gen $LOCLANG; localectl set-locale $LOCSETTINGS; timedatectl set-timezone $TIMEZONE; LC_TIME=\"en_US\" date -s \"${DATE}\";"
  #echo "Ececute remotely: $REMOTECMD" 
  sshpass -p $PASSWD ssh -o StrictHostKeyChecking=no root@$CONTROLLERIP $REMOTECMD

  FILE=./root/etc/systemd/timesyncd.conf
  if [[ -f "$FILE" ]]; then
    echo "Setup time server, copy timesyncd.conf"  
    sshpass -p $PASSWD scp -p $FILE root@$CONTROLLERIP:/etc/systemd/. 
    REMOTECMD="systemctl restart systemd-timesyncd"
    #echo "Ececute remotely: $REMOTECMD" 
    sshpass -p $PASSWD ssh -o StrictHostKeyChecking=no root@$CONTROLLERIP $REMOTECMD
  fi
}

if [ -z "$1" ]; then
  echo "Target IP address  or file with remote IPs is missing"
  localhelp 
  exit 1
fi

if [ -z "$2" ]; then
  echo "Root password is missing"
  localhelp 
  exit 1
fi


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
    echo "$CONTROLLERIP  $(executeScript) "
  done

elif [[ $CONTROLLERIP =~ ^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+$ ]]; then
  echo "$CONTROLLERIP  $(executeScript) "
else
  echo "$CONTROLLERIP is not correct IP"
fi




