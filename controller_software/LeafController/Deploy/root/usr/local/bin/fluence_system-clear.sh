#!/bin/bash
. /usr/local/share/fluence/fluence_vars.sh

rm -fv $INIT_FILE
cp -p $INIT_NETPLAN_FILE /etc/netplan/network.yaml

# Remove iproutes all chains
iptables -F

# Change MAC addresses
/usr/local/bin/fluence_mac-change.sh force

# Generate and apply default network settings
netplan generate
netplan apply

echo "execute reboot to finish the settings"
