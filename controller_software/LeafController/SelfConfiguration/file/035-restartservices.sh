#!/bin/bash

# Restart required services after IP address is changed with netplan apply
systemctl stop    fluence-controller
systemctl restart fluence-modbus
systemctl start   fluence-controller
