#!/bin/bash


if [ $# -ne 1 ]; then
    echo "Usage:"
    echo "unchangeConfig-remote.sh <IP address>"
    exit 1
fi

STATIC_IP_ADDRESS=$1

echo
echo "UnChanging the Configuration of $STATIC_IP_ADDRESS"
echo


cd /unchangeConfig


# Change /etc/netplan/network.yaml 

netplan_file_name=network.yaml
netplan_file_path=/etc/netplan/$netplan_file_name
netplan_file_bak_path=$netplan_file_path-bak

echo "UnChanging $netplan_file_name"

if test -f "$netplan_file_path"; then
	echo "  Removing $netplan_file_path"
	rm $netplan_file_path
fi

echo "  Copying $netplan_file_name to $netplan_file_path"
cp "$netplan_file_name" "$netplan_file_path"


# change the IP address in the network.yaml file
sed -i "s|STATIC_IP_ADDRESS|$STATIC_IP_ADDRESS|g" "$netplan_file_path"

echo "  Executing netplan apply"
netplan apply


# Change crontab

crontab_file_name=crontab.txt
crontab_file_bak_path=./"$crontab_file_name"-bak

echo "UnChanging crontab"

echo "  Executing crontab"
crontab $crontab_file_name


echo
echo "Configuration of $1 changed."


reboot

exit 0
