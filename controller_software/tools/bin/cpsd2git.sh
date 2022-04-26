#/bin/bash

LOCUSER=$USER
LOCHOME=$HOME
BPIR2=$LOCHOME/bpi-r2/controller_software/LeafController/BananaPiR2
CONTROLLINO=$LOCHOME/bpi-r2/controller_software/LeafController/Controllino
BPIBOOT=$LOCHOME/bpi-r2/mount_points/BPI-BOOT
BPIROOT=$LOCHOME/bpi-r2/mount_points/BPI-ROOT

#bin


#/usr/local/bin
sudo cp -rp  $BPIROOT/usr/local/bin/{bpi,controllino,fluence}* $BPIR2/root/usr/local/bin/.

#/usr/local/share
sudo cp -p  $BPIROOT/usr/local/share/fluence/* $BPIR2/root/usr/local/share/fluence/.

#etc
sudo cp -p $BPIROOT/etc/resolv.conf    $BPIR2/root/etc/.
sudo cp -p $BPIROOT/etc/netplan/*      $BPIR2/root/etc/netplan/.
sudo cp -p $BPIROOT/etc/dhcp/*         $BPIR2/root/etc/dhcp/.
sudo cp -p $BPIROOT/etc/default/lldpd  $BPIR2/root/etc/default/.