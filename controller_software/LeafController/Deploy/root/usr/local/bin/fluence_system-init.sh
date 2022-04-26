#!/bin/bash
. /usr/local/share/fluence/fluence_vars.sh

# Run at first boot
if [ ! -f "$INIT_FILE" ]; then
    init=1
    /usr/local/bin/fluence_new-machine-id.sh
#    /usr/local/bin/fluence_unlicense_das.sh
    /usr/local/bin/fluence_set-hostname-random.sh
    /usr/local/bin/fluence_set-static-ip.py
    touch "$INIT_FILE"
fi

# Run every boot
if [ -n "${init}" ] ; then
  /usr/local/bin/fluence_mac-change.sh force
  systemctl start systemd-networkd.service
  netplan apply
else
  /usr/local/bin/fluence_mac-change.sh 
fi

# /usr/local/bin/fluence_iptables.sh Moved to separate service
# echo before upload
ControllinoConnect=$(lsusb | grep -o Arduino)
if [[ -n "$ControllinoConnect" ]] ; then
  /usr/local/bin/controllino_upload.sh LeafController.hex
fi
/usr/local/bin/fluence_selfconfiguration.sh
