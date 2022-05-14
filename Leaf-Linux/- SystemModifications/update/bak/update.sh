#!/bin/bash

if [ $# -ne 3 ]; then
    echo "Usage:"
    echo "update.sh <IP address> <root password> <cube type>"
    exit 1
fi


STATIC_IP_ADDRESS=$1
PASSWORD=$2
CUBE_TYPE=$3
CUBE_TYPE_str=""


case $CUBE_TYPE in
	"AC")
		CUBE_TYPE_str="Air Cooled"
                ;;
	"LC-LD")
		CUBE_TYPE_str="Liquid Cooled - Long Duration"
                ;;
	"LC-SD")
		CUBE_TYPE_str="Liquid Cooled - Short Duration"
                ;;
	*)
		echo "Unknown Cube type:"
		echo "  AC    = Air    Cooled"
		echo "  LC-LD = Liquid Cooled - Long  Duration"
		echo "  LC-SD = Liquid Cooled - Short Duration"
                echo
                exit 1
                ;;
esac


remote_directory="update"

echo
echo "***************************************************"
echo

echo "Updating remote system $STATIC_IP_ADDRESS $CUBE_TYPE_str"

sshpass -p "$PASSWORD" ssh -oStrictHostKeyChecking=no  root@$STATIC_IP_ADDRESS "mkdir -p /$remote_directory" 2>/dev/null

echo "  Transfering files using scp"
sshpass -p "$PASSWORD" scp -v update.tar root@$STATIC_IP_ADDRESS:"/$remote_directory" 2>/dev/null

echo "  Executing remote update script"
sshpass -p "$PASSWORD" ssh -oStrictHostKeyChecking=no  root@$STATIC_IP_ADDRESS "bash -s" < ./update-remote.sh $CUBE_TYPE


echo
echo "***************************************************"
echo

