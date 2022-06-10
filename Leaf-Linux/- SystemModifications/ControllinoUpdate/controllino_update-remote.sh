#!/bin/bash

if [ $# -ne 0 ]; then
    echo "Usage:"
    echo "controllino_update-remote.sh"
    echo
    exit 1
fi


echo


#---------------------------------------------------------------------------------------------
#update controllino_modbus

echo "updating controllino ------------------------------------------------------------"

CONTROLLINOIMG=/root/Controllino/Controllino.hex

ARDUINO_ROOT="/usr/local/share/arduino"
AVRDUDE="${ARDUINO_ROOT}/hardware/tools/avr/bin/avrdude"
AVRDUDE_CONF="${ARDUINO_ROOT}/hardware/tools/avr/etc/avrdude.conf"
AVRDUDE_OPTS="-v -patmega2560 -cwiring -P/dev/ttyACM0 -b115200 -D"

${AVRDUDE} -C ${AVRDUDE_CONF} ${AVRDUDE_OPTS} -Uflash:w:$CONTROLLINOIMG:i

echo "----------------------------------------------------------------------------------------"; echo
