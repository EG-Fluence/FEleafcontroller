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
  echo "Usage: makesd.sh fluenceimagefile"
  exit 1
fi

FLUENCEIMAGE=$1
FREELD=$(sudo losetup -f)

#sudo dd status=progress if=/dev/zero bs=1M count=7296 of=$BACKUPFOLDER/$FLUENCEIMAGE
sudo dd status=progress if=/dev/zero bs=1M count=7457 of=$BACKUPFOLDER/$FLUENCEIMAGE

sudo losetup $FREELD $BACKUPFOLDER/$FLUENCEIMAGE

sudo parted -s $FREELD mklabel msdos

sudo parted -s $FREELD unit MiB mkpart primary fat32 -- 100MiB 356MiB

#sudo parted -s $FREELD unit MiB mkpart primary ext2 -- 356MiB 7295MiB
sudo parted -s $FREELD unit MiB mkpart primary ext2 -- 356MiB 7456MiB

#Note that mounting / partitioning loopback devices does not always cause kernel to re-read partition table
sudo partprobe $FREELD

sudo mkfs.vfat ${FREELD}p1 -I -n BPI-BOOT

sudo mkfs.ext4 -O ^has_journal -E stride=2,stripe-width=1024 -b 4096 ${FREELD}p2 -L BPI-ROOT

sudo sync

sudo parted -s $FREELD print

sudo losetup -d $FREELD

