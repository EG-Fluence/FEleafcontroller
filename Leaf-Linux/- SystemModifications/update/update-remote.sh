#!/bin/bash


Usage()
{
    echo "Usage:"
    echo "update-remote.sh <Cube/Telco type>"
    echo "  <Cube type>"
    echo "    Cube-AC        = Cube Air    Cooled"
    echo "    Cube-LC-LD-EMW = Cube Liquid Cooled - Long  Duration - Envicool Chiller EMW75HDNC1A"
    echo "    Cube-LC-LD-RC  = Cube Liquid Cooled - Long  Duration - Boyd     Chiller RC8057G1"
    echo "    Cube-LC-SD     = Cube Liquid Cooled - Short Duration"
    echo "    Telco-rs485    = Telco nVent HVAC RS485"
    echo "    Telco-ethernet = Telco nVent HVAC Ethernet"
    echo
    exit 1
}


if [ $# -ne 1 ]; then
    Usage
fi


# Convert Cube/Telco Type to upper case
Cube_Telco_type=$(tr '[a-z]' '[A-Z]' <<< $1)


case $Cube_Telco_type in
        "CUBE-AC" | "CUBE-LC-LD-EMW" | "CUBE-LC-LD-RC" | "CUBE-LC-SD")
                echo "Updating for Cube $Cube_Telco_type"
                ;;
        "TELCO-RS485" | "TELCO-ETHERNET")
                echo "Updating for Telco $Cube_Telco_type"
                ;;

        *)
                echo "Unknown Cube or Telco type!"
                echo
                Usage
                ;;
esac


echo

echo "untar update.tar -----------------------------------------------------------------------"

cd /update
tar  -xvf  update.tar

echo "----------------------------------------------------------------------------------------"; echo


#---------------------------------------------------------------------------------------------
#update controllino_modbus

echo "updating controllino_modbus ------------------------------------------------------------"

cp  /update/controllino_modbus/usr-local-bin/*.py                           /usr/local/bin
cp  /update/controllino_modbus/usr-local-bin/controllino_modbus_utils/*.py  /usr/local/bin/controllino_modbus_utils

chmod  755  /usr/local/bin/*.py
chmod  755  /usr/local/bin/controllino_modbus_utils/*.py

cp  /update/controllino_modbus/usr-local-share-fluence/*  /usr/local/share/fluence/

if test -f /usr/local/share/fluence/controllino_modbus.conf ; then
  rm  /usr/local/share/fluence/controllino_modbus.conf
fi

case $Cube_Telco_type in
        "CUBE-AC")
                cp  /usr/local/share/fluence/controllino_modbus_Cube_AirCooled.conf                                   /usr/local/share/fluence/controllino_modbus.conf
                ;;
        "CUBE-LC-LD-EMW")
                cp  /usr/local/share/fluence/controllino_modbus_Cube_LiquidCooled_LD_chillerEnvicoolEMW75HDNC1A.conf  /usr/local/share/fluence/controllino_modbus.conf
                ;;
        "CUBE-LC-LD-RC")
                cp  /usr/local/share/fluence/controllino_modbus_Cube_LiquidCooled_LD_chillerBoydRC8057G1.conf         /usr/local/share/fluence/controllino_modbus.conf
                ;;
        "LC-SD")
                cp  /usr/local/share/fluence/controllino_modbus_Cube_LiquidCooled_SD.conf                             /usr/local/share/fluence/controllino_modbus.conf
                ;;
        "TELCO-RS485")
                cp  /usr/local/share/fluence/controllino_modbus_Telco_hvacRS485.conf                                  /usr/local/share/fluence/controllino_modbus.conf
                ;;
        "TELCO-ETHERNET")
                cp  /usr/local/share/fluence/controllino_modbus_Telco_hvacTCPIP.conf                                  /usr/local/share/fluence/controllino_modbus.conf
                ;;

        *)
                ;;
esac


echo "----------------------------------------------------------------------------------------"; echo


#---------------------------------------------------------------------------------------------
#update reboot_datetime

echo "updating reboot_datetime ---------------------------------------------------------------"

if [[ ! -d /logs ]]; then
  mkdir  /logs
fi

cp  /update/reboot_datetime/reboot_datetime.sh  /logs
chmod  755  /logs/reboot_datetime.sh
sed -i -e 's/\r$//'  /logs/reboot_datetime.sh

echo "----------------------------------------------------------------------------------------"; echo

#---------------------------------------------------------------------------------------------
#update crontab

echo "updating crontab -----------------------------------------------------------------------"

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

echo "updating self discovery ----------------------------------------------------------------"

cp  /update/self_discovery/home-fos-SelfConfiguration/*.py  /home/fos/SelfConfiguration
chmod  755  /home/fos/SelfConfiguration/*.py

echo "----------------------------------------------------------------------------------------"; echo



# reboot
