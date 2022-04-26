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
  echo "Usage: infoimg.sh fluenceimagefile"
  echo "The file must be stored inside ~/bpi-r2/backup"
}

if [ -z "$1" ]; then
  echo "Image file name missing"
  localhelp 
  exit 1
fi

FLUENCEIMAGE=$1

if [ ! -s $BACKUPFOLDER/$FLUENCEIMAGE ]; then
  echo "Image file is missing or empty"
  localhelp
  exit 1
fi


PART1=$BACKUPFOLDER/${FLUENCEIMAGE}1
PART2=$BACKUPFOLDER/${FLUENCEIMAGE}2
PART1START=$(sudo fdisk -lu $BACKUPFOLDER/$FLUENCEIMAGE | awk -v pattern="$PART1" '$0 ~ pattern {print $2}')
PART1END=$(sudo fdisk -lu $BACKUPFOLDER/$FLUENCEIMAGE | awk -v pattern="$PART1" '$0 ~ pattern {print $3}')
PART1SIZE=$(sudo fdisk -lu $BACKUPFOLDER/$FLUENCEIMAGE | awk -v pattern="$PART1" '$0 ~ pattern {print $4}')
PART2START=$(sudo fdisk -lu $BACKUPFOLDER/$FLUENCEIMAGE | awk -v pattern="$PART2" '$0 ~ pattern {print $2}')
PART2END=$(sudo fdisk -lu $BACKUPFOLDER/$FLUENCEIMAGE | awk -v pattern="$PART2" '$0 ~ pattern {print $3}')
PART2SIZE=$(sudo fdisk -lu $BACKUPFOLDER/$FLUENCEIMAGE | awk -v pattern="$PART2" '$0 ~ pattern {print $4}')

echo "Partition    Start    End     Size"
echo $PART1 $PART1START $PART1END $PART1SIZE
echo $PART2 $PART2START $PART2END $PART2SIZE

echo "Image size:" $(($PART1START + $PART1SIZE + $PART2SIZE))


