#/bin/bash

LOCUSER=$USER
LOCHOME=$HOME
BPIR2=$LOCHOME/bpi-r2/controller_software/LeafController/BananaPiR2
CONTROLLINO=$LOCHOME/bpi-r2/controller_software/LeafController/Controllino
BPIBOOT=$LOCHOME/bpi-r2/mount_points/BPI-BOOT
BPIROOT=$LOCHOME/bpi-r2/mount_points/BPI-ROOT
#BPIBOOT=/media/$LOCUSER/BPI-BOOT
#BPIROOT=/media/$LOCUSER/BPI-ROOT

#unmount old location od SD card, it could be wrong position adter inserting into Ubuntu e.g. /media/boris/BPI-BOOT
sudo umount /media/$LOCUSER/BPI-BOOT
sudo umount /media/$LOCUSER/BPI-ROOT

sudo mount /dev/sdb1 $BPIBOOT
sudo mount /dev/sdb2 $BPIROOT

sudo rm -f $BPIROOT/etc/FLUENCE_INITIALIZED
sudo cp -p $BPIROOT/etc/netplan/network.yaml_start $BPIROOT/etc/netplan/network.yaml

