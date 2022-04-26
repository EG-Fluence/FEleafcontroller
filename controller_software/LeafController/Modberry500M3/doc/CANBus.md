# CAN Bus setup

## Install utilities
```
apt-get install can-utils
```

## add CAN device to config.txt
```
nano /boot/config.txt
```
add
```
dtparam=spi=on
dtoverlay=mcp2515-can0,oscillator=16000000,interrupt=29
dtoverlay=sp1-1cs
```

Reboot ModBerry
```
reboot
```

## Test
on first Modberry
```
ip link set can0 up type can bitrate 500000
cansend can0 127#DEADBEEF
```
on second Modberry
```
ip link set can0 up type can bitrate 500000
candump can0
```
