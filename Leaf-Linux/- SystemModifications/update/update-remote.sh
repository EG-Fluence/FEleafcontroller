#!/bin/bash

if [ $# -ne 1 ]; then
    echo "Usage:"
    echo "update-remote.sh <Cube Type>"
    exit 1
fi

CUBE_TYPE=$1

echo

echo "untar update.tar -----------------------------------------------------------------------"

cd /update
tar  -xvf  update.tar

echo "----------------------------------------------------------------------------------------"; echo


#---------------------------------------------------------------------------------------------
#update controllino_modbus

echo "updating controllino_modbus ------------------------------------------------------------"

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

echo "----------------------------------------------------------------------------------------"; echo


#---------------------------------------------------------------------------------------------
#update reboot_datetime

echo "updating reboot_datetime ---------------------------------------------------------------"

if test -f /logs ; then
  mkdir  /logs
fi

cp  /update/reboot_datetime/reboot_datetime.sh  /logs
chmod  777  /logs/reboot_datetime.sh
sed -i -e 's/\r$//'  /logs/reboot_datetime.sh


change_crontab=$(crontab -l | grep -c reboot_datetime.sh)
if [ "$change_crontab" -eq 0 ]; then
  (crontab -l && echo "@reboot (sh /logs/reboot_datetime.sh)") | crontab -
  echo " crontab changed with reboot_datetime.sh"
else
  echo " crontab has already been changed."
fi

echo "----------------------------------------------------------------------------------------"; echo


#---------------------------------------------------------------------------------------------
#update self_discovery

echo "updating self discover -----------------------------------------------------------------"

cp  /update/self_discovery/home-fos-SelfConfiguration/*.py  /home/fos/SelfConfiguration
chmod  777  /home/fos/SelfConfiguration/*.py

echo "----------------------------------------------------------------------------------------"; echo

# reboot
