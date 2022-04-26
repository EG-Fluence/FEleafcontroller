#!/bin/bash

ControllinoConnect=$(lsusb | grep -o Arduino)
if [[ -n "$ControllinoConnect" ]] ; then
  /usr/local/bin/controllino_modbus.py
fi
