#!/bin/bash

ControllinoConnect=$(lsusb | grep -o Arduino)
if [[ -n "$ControllinoConnect" ]] ; then
  /usr/local/bin/fluence_controller.py
fi
