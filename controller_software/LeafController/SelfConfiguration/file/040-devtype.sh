#!/bin/bash

LOG () {
  echo "$1"
}

LOG "Environment: FLUENCE_IP:$FLUENCE_IP; FLUENCE_HOSTNAME:$FLUENCE_HOSTNAME; FLUENCE_TESTING:$FLUENCE_TESTING; FLUENCE_RECONFIGURE:$FLUENCE_RECONFIGURE; FLUENCE_DEVTYPE:$FLUENCE_DEVTYPE"

egrep -v "CONTROLLER\=" /usr/local/share/fluence/fluence_vars.sh > /usr/local/share/fluence/.tmp.fluence_vars.sh
echo "CONTROLLER=${FLUENCE_DEVTYPE}" >> /usr/local/share/fluence/.tmp.fluence_vars.sh
cp -rp /usr/local/share/fluence/.tmp.fluence_vars.sh /usr/local/share/fluence/fluence_vars.sh
rm -f /usr/local/share/fluence/.tmp.fluence_vars.sh
