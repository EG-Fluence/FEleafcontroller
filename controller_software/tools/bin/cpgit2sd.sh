#/bin/bash

LOCUSER=$USER
LOCHOME=$HOME
BPIR2=$LOCHOME/bpi-r2/controller_software/LeafController/BananaPiR2
CONTROLLINO=$LOCHOME/bpi-r2/controller_software/LeafController/Controllino
SELFCONFIG=$LOCHOME/bpi-r2/controller_software/LeafController/SelfConfiguration
BPIBOOT=$LOCHOME/bpi-r2/mount_points/BPI-BOOT
BPIROOT=$LOCHOME/bpi-r2/mount_points/BPI-ROOT

#bin


#/usr/local/bin
sudo cp -rp $BPIR2/root/usr/local/bin/* $BPIROOT/usr/local/bin/.
sudo chown root:root $BPIROOT/usr/local/bin/bpi-*
sudo chown -R root:root $BPIROOT/usr/local/bin/controllino*
sudo chown root:root $BPIROOT/usr/local/bin/fluence*

#share 
# Arduino
sudo cp -p $BPIR2/root/usr/local/share/arduino/hardware/tools/avr/bin/* $BPIROOT/usr/local/share/arduino/hardware/tools/avr/bin/.
sudo chown root:root $BPIROOT/usr/local/share/arduino/hardware/tools/avr/bin/avr*
sudo cp -p $BPIR2/root/usr/local/share/arduino/hardware/tools/avr/etc/* $BPIROOT/usr/local/share/arduino/hardware/tools/avr/etc/.
sudo chown root:root $BPIROOT/usr/local/share/arduino/hardware/tools/avr/etc/avr*

sudo cp -p $BPIR2/root/usr/local/share/fluence/* $BPIROOT/usr/local/share/fluence/.
sudo chown root:root $BPIROOT/usr/local/share/fluence/*

#etc
sudo cp -p $BPIR2/root/etc/resolv.conf  $BPIROOT/etc/.
sudo chown root:root $BPIROOT/etc/resolv.conf
sudo cp -p $BPIR2/root/etc/netplan/*  $BPIROOT/etc/netplan/.
sudo chown root:root $BPIROOT/etc/netplan/*
sudo cp -p $BPIR2/root/etc/systemd/system/* $BPIROOT/etc/systemd/system/.
sudo chown root:root $BPIROOT/etc/systemd/system/{aws-greengrass.service,das.service,fluence-*.service}
#mkdir -pv BPIROOT/etc/dhcp
sudo cp -p $BPIR2/root/etc/dhcp/dhcpd.conf $BPIROOT/etc/dhcp/.
sudo chown root:root $BPIROOT/etc/dhcp/dhcpd.conf
sudo cp -p $BPIR2/root/etc/default/lldpd  $BPIROOT/etc/default/.
sudo chown root:root $BPIROOT/etc/default/lldpd

#Fred part
sudo cp -pr $SELFCONFIG/*  $BPIROOT/home/fos/SelfConfiguration/.


sudo sync
