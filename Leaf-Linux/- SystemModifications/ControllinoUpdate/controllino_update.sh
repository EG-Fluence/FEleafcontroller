#!/bin/bash

if [ $# -ne 2 ]; then
    echo "Usage:"
    echo "controllino_update.sh <IP address> <root password>"
    echo
    exit 1
fi


STATIC_IP_ADDRESS=$1
PASSWORD=$2


remote_directory="/root/Controllino"

echo
echo "***************************************************"
echo

echo "Updating remote system $STATIC_IP_ADDRESS"

# sshpass -p "$PASSWORD" ssh -oStrictHostKeyChecking=no  root@$STATIC_IP_ADDRESS "mkdir -p $remote_directory"  2>/dev/null

echo "  Transfering files using scp"
sshpass -p "$PASSWORD" scp -oStrictHostKeyChecking=no -v Controllino.hex root@$STATIC_IP_ADDRESS:"$remote_directory"  2>/dev/null

echo "  Executing remote update script"
sshpass -p "$PASSWORD" ssh -oStrictHostKeyChecking=no  root@$STATIC_IP_ADDRESS "bash -s" < ./controllino_update-remote.sh


echo
echo "***************************************************"
echo

