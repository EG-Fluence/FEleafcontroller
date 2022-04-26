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
BPIIMAGEFOLDER=$LOCHOME/bpi-r2/controller_software/LeafController/BananaPiR2/images

localhelp () {
  echo "Usage: cpimg2remoteemmc.sh fluenceimagefile remote_bpi_IP rootpwd"
  echo "The file must be stored inside ~/bpi-r2/backup"
}

if [ -z "$1" ]; then
  echo "Image file name is missing"
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


FLUENCEIMAGE=$1

if [ ! -s $BACKUPFOLDER/$FLUENCEIMAGE ]; then
  echo "Image file is missing or empty"
  localhelp
  exit 1
fi

BPIIP=$2
PASSWD=$3
BOOTIMG=BPI-R2-EMMC-boot0-DDR1600-0k-0905.img.gz

echo Create folder ~/bpi-r2 to Banana Pi and reside SD card to max size
sshpass -p $PASSWD ssh root@$BPIIP 'mkdir -pv bpi-r2/mount_points/{BPI-ROOT,BPI-BOOT}; fluence_partition-maxsize.sh'
echo Copy boot loader to Banana Pi
sshpass -p $PASSWD scp $BPIR2/images/$BOOTIMG root@$BPIIP:~/bpi-r2/.
echo Copy image to Banana Pi
sshpass -p $PASSWD scp $BACKUPFOLDER/$FLUENCEIMAGE root@$BPIIP:~/bpi-r2/.
echo Copy tools to Panana Pi
sshpass -p $PASSWD scp $BPIIMAGEFOLDER/cpimg2emmc.sh root@$BPIIP:~/bpi-r2/.
sshpass -p $PASSWD scp $BPIIMAGEFOLDER/auto_install.sh root@$BPIIP:~/bpi-r2/.
sshpass -p $PASSWD scp $BPIIMAGEFOLDER/erase_emmc.sh root@$BPIIP:~/bpi-r2/.
sshpass -p $PASSWD scp $BPIIMAGEFOLDER/next_boot_from.sh root@$BPIIP:~/bpi-r2/.
sshpass -p $PASSWD scp $BPIIMAGEFOLDER/uEnv* root@$BPIIP:~/bpi-r2/.
echo Activating auto install service
sshpass -p $PASSWD scp $BPIIMAGEFOLDER/fluence_autoinstall.service root@$BPIIP:/etc/systemd/system
REMOTECMD="systemctl enable fluence_autoinstall.service"
sshpass -p $PASSWD ssh root@$BPIIP $REMOTECMD
REMOTECMD="~/bpi-r2/auto_install.sh $FLUENCEIMAGE"
echo run script as root on Banana Pi $REMOTECMD or uncomment the line in this script or reboot the BPi
#sshpass -p $PASSWD ssh root@$BPIIP $REMOTECMD

