#!/bin/bash

if [ $# -eq 0 ]; then
    echo "Usage:"
    echo "changeConfig.sh <IP address>"
    exit 1
fi


STATIC_IP_ADDRESS=$1

remote_directory="changeConfig"

echo
echo "***************************************************"
echo


ssh root@$STATIC_IP_ADDRESS "mkdir -p /$remote_directory"

scp -v crontab.txt network.yaml root@$STATIC_IP_ADDRESS:"/$remote_directory"

ssh root@$STATIC_IP_ADDRESS 'bash -s' < changeConfig-remote.sh


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