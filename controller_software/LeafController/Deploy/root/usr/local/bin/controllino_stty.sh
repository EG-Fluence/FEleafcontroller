#!/bin/bash

#default baud rate
BAUDRATE=115200

if [ -n "$1" ]; then
  BAUDRATE=$1
fi

echo Baud rate for '/dev/ttyACM0' $BAUDRATE

stty $BAUDRATE -F /dev/ttyACM0 raw -echo
cat /dev/ttyACM0



