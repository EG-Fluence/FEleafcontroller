#!/bin/bash

if [ $# -ne 1 ]; then
    echo "Usage:"
    echo "update-remote.sh <Cube Type>"
    exit 1
fi

CUBE_TYPE=$1

cd /update
tar  -xvf  update.tar


#----------------------------------------------------------------------------------------------
#update controllino_modbus

cp  /update/controllino_modbus/usr-local-bin/*.py  /usr/local/bin
cp  /update/controllino_modbus/usr-local-bin/*.py  /usr/local/bin/controllino_modbus_utils/

chmod  777  /usr/local/bin/*.py
chmod  777  /usr/local/bin/controllino_modbus_utils/*.py

cp  /update/controllino_modbus/usr-local-share-fluence/*  /usr/local/share/fluence/

if test -f /usr/local/share/fluence/controllino_modbus.conf ; then
  rm  /usr/local/share/fluence/controllino_modbus.conf
fi

case "$CUBE_TYPE" in
	"AC")
		cp  /usr/local/share/fluence/controllino_modbus_AirCooled.conf        /usr/local/share/fluence/controllino_modbus.conf
                ;;
	"LC-LD")
		cp  /usr/local/share/fluence/controllino_modbus_LiquidCooled_LD.conf  /usr/local/share/fluence/controllino_modbus.conf
                ;;
	"LC-SD")
		cp  /usr/local/share/fluence/controllino_modbus_LiquidCooled_SD.conf  /usr/local/share/fluence/controllino_modbus.conf
                ;;
	*)
                ;;
esac


#----------------------------------------------------------------------------------------------
#update reboot_datetime

if test -f /logs ; then
  mkdir  /logs
fi

cp  /update/reboot_datetime/reboot_datetime.sh  /logs
chmod  777  /logs/reboot_datetime.sh
sed -i -e 's/\r$//'  /logs/reboot_datetime.sh


(crontab -l && echo "@reboot (sh /logs/reboot_datetime.sh)") | crontab -


#----------------------------------------------------------------------------------------------
#update self_discovery

cp  /update/self_discovery/home-fos-SelfConfiguration/*.py  /home/fos/SelfConfiguration
chmod  777  /home/fos/SelfConfiguration/*.py


reboot
