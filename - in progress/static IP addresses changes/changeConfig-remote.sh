#!/bin/bash


if [ $# -eq 0 ]; then
    echo "Usage:"
    echo "changeConfig-remote.sh <IP address>"
    exit 1
fi

STATIC_IP_ADDRESS=$1

echo
echo "---------------------------------------------"
echo "Changing the Configuration of $STATIC_IP_ADDRESS"
echo


cd /changeConfig


# Change /etc/netplan/network.yaml 

netplan_file_name=network.yaml
netplan_file_path=/etc/netplan/$netplan_file_name
netplan_file_bak_path=$netplan_file_path-bak

echo "Changing $netplan_file_name"

if test -f "$netplan_file_bak_path"; then
    echo "The file $netplan_file_bak_path already exists"
else
    echo "  Making a copy of $netplan_file"
    cp $netplan_file_path $netplan_file_bak_path
fi

if test -f "$netplan_file_path"; then
	echo "  Removing $netplan_file_path"
	rm $netplan_file_path
fi

echo "  Copying new $netplan_file_name to $netplan_file_path"
cp "$netplan_file_name" "$netplan_file_path"


# change the IP address in the network.yaml file
sed -i "s|STATIC_IP_ADDRESS|$STATIC_IP_ADDRESS|g" "$netplan_file_path"


echo "  Executing netplan apply"
netplan apply


# Change crontab

crontab_file_name=crontab.txt
crontab_file_bak_path=./"$crontab_file_name"-bak

echo "Changing crontab"

if test -f "$crontab_file_bak_path"; then
    echo "  The file $crontab_file_bak_path already exists"
else
    echo "  Making a copy of crontab"
    crontab -l > "$crontab_file_bak_path"
fi


echo "  Executing crontab"

crontab $crontab_file_name


echo
echo "Configuration of $1 changed."
echo "---------------------------------------------"
echo


exit 0
