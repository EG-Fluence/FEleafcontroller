#!/bin/bash

LOCUSER=$USER
LOCHOME=$HOME


localhelp () {
  echo "Usage: controllino_upload.sh controllinoimage.hex"
}

if [ -z "$1" ]; then
  echo "Controllino Image file name is missing"
  localhelp 
  exit 1
fi


AVRDUDE="/usr/local/bin/avrdude"
AVRDUDE_CONF="./avrdude.conf"
AVRDUDE_OPTS="-v -patmega2560 -cwiring -P/dev/cu.usbmodem323101 -b115200 -D"

${AVRDUDE} -C ${AVRDUDE_CONF} ${AVRDUDE_OPTS} -Uflash:w:$1:i

