#!/bin/bash

LOG () {
  echo "$1"
}

LOG "Environment: FLUENCE_IP:$FLUENCE_IP; FLUENCE_HOSTNAME:$FLUENCE_HOSTNAME; FLUENCE_TESTING:$FLUENCE_TESTING; FLUENCE_RECONFIGURE:$FLUENCE_RECONFIGURE"

splitted_ip=(${FLUENCE_IP//\// })

python3 /usr/local/bin/fluence_set-assigned-ip.py ${FLUENCE_IP}

/usr/sbin/netplan apply

