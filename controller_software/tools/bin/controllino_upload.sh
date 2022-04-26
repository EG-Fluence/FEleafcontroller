#/bin/bash

LOCUSER=$USER
LOCHOME=$HOME
BPIR2=$LOCHOME/bpi-r2/controller_software/LeafController/BananaPiR2
CONTROLLINO=$LOCHOME/bpi-r2/controller_software/LeafController/Controllino
BPIBOOT=$LOCHOME/bpi-r2/mount_points/BPI-BOOT
BPIROOT=$LOCHOME/bpi-r2/mount_points/BPI-ROOT
#BPIBOOT=/media/$LOCUSER/BPI-BOOT
#BPIROOT=/media/$LOCUSER/BPI-ROOT
BACKUPFOLDER=$LOCHOME/bpi-r2/backup
BPITOOLS=$LOCHOME/bpi-r2/controller_software/tools/bin

localhelp () {
  echo "Usage: controllino_upload.sh controllinoimage remote_bpi_IP rootpwd"
}

if [ -z "$1" ]; then
  echo "Controllino Image file name is missing"
  localhelp 
  exit 1
fi

if [ -z "$2" ]; then
  echo "Target Banana Pi-R2 IP address is missing"
  localhelp 
  exit 1
fi

if [ -z "$3" ]; then
  echo "Root password is missing"
  localhelp 
  exit 1
fi


CONTROLLINOIMG=$1

BPIIP=$2
PASSWD=$3

echo Create folder ~/Controllino on Banana Pi
sshpass -p $PASSWD ssh root@$BPIIP 'mkdir -pv ~/Controllino'
echo Copy image to Banana Pi
sshpass -p $PASSWD scp -p $CONTROLLINOIMG root@$BPIIP:~/Controllino/.

echo run script as root on Banana pi /usr/local/bin/controllino_upload.sh or uncomment the line in this script
#REMOTECMD='controllino_upload.sh ~/Controllino/$CONTROLLINOIMG'
REMOTECMD="controllino_upload.sh $CONTROLLINOIMG"
#echo $REMOTECMD
sshpass -p $PASSWD ssh root@$BPIIP $REMOTECMD

