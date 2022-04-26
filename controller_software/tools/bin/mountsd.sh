#/bin/bash

LOCUSER=$USER
LOCHOME=$HOME
BPIR2=$LOCHOME/bpi-r2/controller_software/LeafController/BananaPiR2
CONTROLLINO=$LOCHOME/bpi-r2/controller_software/LeafController/Controllino
BPIBOOT=$LOCHOME/bpi-r2/mount_points/BPI-BOOT
BPIROOT=$LOCHOME/bpi-r2/mount_points/BPI-ROOT
#BPIBOOT=/media/$LOCUSER/BPI-BOOT
#BPIROOT=/media/$LOCUSER/BPI-ROOT

DEVICE="/dev/sdb"

if [[ -n "$1" ]] && [[ "$1" == /dev/* ]]; then
  DEVICE=$1
fi

#unmount old location od SD card, it could be wrong position adter inserting into Ubuntu e.g. /media/boris/BPI-BOOT
sudo umount /media/$LOCUSER/BPI-BOOT
sudo umount /media/$LOCUSER/BPI-ROOT

sudo mount ${DEVICE}1 $BPIBOOT
sudo mount ${DEVICE}2 $BPIROOT



