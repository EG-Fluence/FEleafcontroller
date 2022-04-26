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

if [ -z "$1" ]; then
  echo "Image file name missing"
  echo "Usage: mountimg.sh fluenceimagefile"
  exit 1
fi

FLUENCEIMAGE=$1
PART1=$BACKUPFOLDER/${FLUENCEIMAGE}1
PART2=$BACKUPFOLDER/${FLUENCEIMAGE}2
PART1START=$(sudo fdisk -lu $BACKUPFOLDER/$FLUENCEIMAGE | awk -v pattern="$PART1" '$0 ~ pattern {print $2}')
PART1SIZE=$(sudo fdisk -lu $BACKUPFOLDER/$FLUENCEIMAGE | awk -v pattern="$PART1" '$0 ~ pattern {print $4}')
PART2START=$(sudo fdisk -lu $BACKUPFOLDER/$FLUENCEIMAGE | awk -v pattern="$PART2" '$0 ~ pattern {print $2}')
PART2SIZE=$(sudo fdisk -lu $BACKUPFOLDER/$FLUENCEIMAGE | awk -v pattern="$PART2" '$0 ~ pattern {print $4}')

#echo $PART1, $PART1START, $PART1SIZE
#echo $PART2, $PART2START, $PART2SIZE

sudo mount -t vfat -o loop,offset=$(($PART1START * 512)),sizelimit=$(($PART1SIZE * 512)) $BACKUPFOLDER/$FLUENCEIMAGE $BPIBOOT
sudo mount -t ext4 -o loop,offset=$(($PART2START * 512)),sizelimit=$(($PART2SIZE * 512)) $BACKUPFOLDER/$FLUENCEIMAGE $BPIROOT

#Do we need this?
#You need to force kernel to scan the partition table if you want to use it on loop device.
#$ sudo losetup --partscan loop1 second_loop.fs
