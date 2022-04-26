#!/bin/bash

LOCUSER=$USER
LOCHOME=$HOME

if [ -z "$LOCHOME" ]; then
  LOCHOME="/root"
  echo "LOCHOME is empty, set to $LOCHOME"
fi

IMGFOLDER=$LOCHOME/Controllino
CONTROLLINOIMG=$1

localhelp () {
  echo "Usage: controllino_upload.sh controllinoimage.hex"
}

if [ -z "$1" ]; then
  echo "Controllino Image file name is missing"
  localhelp 
  exit 1
fi

if [ ! -s $IMGFOLDER/$CONTROLLINOIMG ]; then
  echo "Controllino Image file is missing or empty $IMGFOLDER/$CONTROLLINOIMG"
  localhelp
  exit 1
fi

ARDUINO_ROOT="/usr/local/share/arduino"
AVRDUDE="${ARDUINO_ROOT}/hardware/tools/avr/bin/avrdude"
AVRDUDE_CONF="${ARDUINO_ROOT}/hardware/tools/avr/etc/avrdude.conf"
AVRDUDE_OPTS="-v -patmega2560 -cwiring -P/dev/ttyACM0 -b115200 -D"

${AVRDUDE} -C ${AVRDUDE_CONF} ${AVRDUDE_OPTS} -Uflash:w:$IMGFOLDER/$CONTROLLINOIMG:i

