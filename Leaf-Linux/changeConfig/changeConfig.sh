#!/bin/bash

if [ $# -ne 2 ]; then
    echo "Usage:"
    echo "changeConfig.sh <IP address> <root password>"
    exit 1
fi


STATIC_IP_ADDRESS=$1

PASSWORD=$2

remote_directory="changeConfig"

echo
echo "***************************************************"
echo

echo "Preparing remote system $STATIC_IP_ADDRESS"

sshpass -p "$PASSWORD" ssh root@$STATIC_IP_ADDRESS "mkdir -p /$remote_directory" 2>/dev/null

echo "Transfering files using scp"

sshpass -p "$PASSWORD" scp -v changeConfig-remote.sh crontab.txt network.yaml root@$STATIC_IP_ADDRESS:"/$remote_directory" 2>/dev/null

sshpass -p "$PASSWORD" ssh root@$STATIC_IP_ADDRESS "bash -s" < ./changeConfig-remote.sh $STATIC_IP_ADDRESS


echo
echo "***************************************************"
echo


#
# Windows Test:
#
# ssh root@192.168.2.1 "mkdir -p /changeConfig"
#
# scp -v crontab.txt network.yaml root@192.168.2.1:/changeConfig
#
# ssh root@192.168.2.1 "bash -s" < changeConfig-remote.sh 10.9.11.101
#
