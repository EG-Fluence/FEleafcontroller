#!/bin/bash

Usage()
{
    echo "Usage:"
    echo "update.sh <IP address> <root password> <Cube/Telco type>"
    echo "  <Cube type>"
    echo "    Cube-AC        = Cube Air    Cooled"
    echo "    Cube-LC-LD-EMW = Cube Liquid Cooled - Long  Duration - Envicool Chiller EMW75HDNC1A"
    echo "    Cube-LC-LD-RC  = Cube Liquid Cooled - Long  Duration - Envicool Chiller RC8057G1"
    echo "    Cube-LC-SD     = Cube Liquid Cooled - Short Duration"
    echo "    Telco-rs485    = Telco nVent HVAC RS485"
    echo "    Telco-ethernet = Telco nVent HVAC Ethernet"
    echo
    exit 1
}

if [ $# -ne 3 ]; then
  Usage
fi


STATIC_IP_ADDRESS=$1
PASSWORD=$2
# Convert Cube/Telco Type to upper case
Cube_Telco_type=$(tr '[a-z]' '[A-Z]' <<< $3)
Cube_Telco_type_str=""


case $Cube_Telco_type in
	"CUBE-AC")
		Cube_Telco_type_str="Cube Air Cooled"
                ;;
	"CUBE-LC-LD-EMW")
		Cube_Telco_type_str="Cube Liquid Cooled - Long Duration - Envicool Chiller EMW75HDNC1A"
                ;;
        "CUBE-LC-LD-RC")
                Cube_Telco_type_str="Cube Liquid Cooled - Long Duration - Envicool Chiller RC8057G1"
                ;;
	"LC-SD")
		Cube_Telco_type_str="Cube Liquid Cooled - Short Duration"
                ;;
        "TELCO-RS485")
                Cube_Telco_type_str="Telco nVent HVAC RS485"
                ;;
        "TELCO-ETHERNET")
                Cube_Telco_type_str="Telco nVent HVAC RS485"
                ;;

	*)
		echo "Unknown Cube or Telco type!"
                echo
                Usage
                ;;
esac


remote_directory="update"

echo
echo "***************************************************"
echo

echo "Updating remote system $STATIC_IP_ADDRESS $Cube_Telco_type_str"

sshpass -p "$PASSWORD" ssh -oStrictHostKeyChecking=no  root@$STATIC_IP_ADDRESS "mkdir -p /$remote_directory" 2>/dev/null

echo "  Transfering files using scp"
sshpass -p "$PASSWORD" scp -v update.tar root@$STATIC_IP_ADDRESS:"/$remote_directory" 2>/dev/null

echo "  Executing remote update script"
sshpass -p "$PASSWORD" ssh -oStrictHostKeyChecking=no  root@$STATIC_IP_ADDRESS "bash -s" < ./update-remote.sh $Cube_Telco_type


echo
echo "***************************************************"
echo

