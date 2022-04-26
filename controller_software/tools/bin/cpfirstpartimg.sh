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

localhelp () {
  echo "Usage: cpfirstpartition.sh imagesource imagedestination"
  echo "The file must be stored inside ~/bpi-r2/backup"
}

if [ -z "$1" ] && [ -z "$2" ]; then
  echo "Image file names are missing"
  localhelp 
  exit 1
fi

FISOURCE=$1
FIDESTINATION=$2

if [ ! -s $BACKUPFOLDER/$FISOURCE ]; then
  echo "Source image file is missing or empty"
  localhelp
  exit 1
fi

if [ ! -s $BACKUPFOLDER/$FIDESTINATION ]; then
  echo "Destination image file is missing or empty"
  localhelp
  exit 1
fi

SPART1=$BACKUPFOLDER/${FISOURCE}1
DPART1=$BACKUPFOLDER/${FIDESTINATION}1

SPART1START=$(sudo fdisk -lu $BACKUPFOLDER/$FISOURCE | awk -v pattern="$SPART1" '$0 ~ pattern {print $2}')
SPART1END=$(sudo fdisk -lu $BACKUPFOLDER/$FISOURCE | awk -v pattern="$SPART1" '$0 ~ pattern {print $3}')
SPART1SIZE=$(sudo fdisk -lu $BACKUPFOLDER/$FISOURCE | awk -v pattern="$SPART1" '$0 ~ pattern {print $4}')
DPART1START=$(sudo fdisk -lu $BACKUPFOLDER/$FIDESTINATION | awk -v pattern="$DPART1" '$0 ~ pattern {print $2}')
DPART1END=$(sudo fdisk -lu $BACKUPFOLDER/$FIDESTINATION | awk -v pattern="$DPART1" '$0 ~ pattern {print $3}')
DPART1SIZE=$(sudo fdisk -lu $BACKUPFOLDER/$FIDESTINATION | awk -v pattern="$DPART1" '$0 ~ pattern {print $4}')

echo "Partition   Start   End    Size"
echo $SPART1 $SPART1START $SPART1END $SPART1SIZE
echo $DPART1 $DPART1START $DPART1END $DPART1SIZE

if [[ $SPART1START == $DPART1START && $SPART1END == $DPART1END && $SPART1SIZE == $DPART1SIZE ]]; then
  echo "The first partitions are equal in size."
  echo "Partition size:" $(($SPART1START + $SPART1SIZE))
  sudo dd status=progress if=$FISOURCE bs=512 count=$(($SPART1START + $SPART1SIZE)) of=$FIDESTINATION conv=notrunc,nocreat  && sudo sync
else
  echo "The first partitions are NOT equal in size."
fi



