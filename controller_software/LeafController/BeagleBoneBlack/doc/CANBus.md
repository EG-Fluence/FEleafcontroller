# CAN Bus setup

## Hardware

- [Waveshare Beaglebone Black Expansion CAPE Features RS485 and CAN Interfaces](https://eckstein-shop.de/Waveshare-Beaglebone-Black-Expansion-CAPE-Features-RS485-and-CAN-Interfaces)
- [Beaglebone rs485 can cape](https://www.robotshop.com/de/en/beaglebone-rs485-can-cape.html)



## Install utilities
```
sudo apt-get update
sudo apt-get install can-utils
```

## Enable CAN device
```
sudo config-pin p9.24 can
sudo config-pin p9.26 can
```

The configuration state of the pin p9.24 can be check with:
```
config-pin -q p9.24
 -> P9_24 Mode: can
```

## Enable Interface
To bring up the interface CAN1 and configure the bus speed to 500kBit/sec enter the following lines:
```
sudo ip link set can1 up type can bitrate 500000
sudo ifconfig can1 up
```

## Test
on first BeagleBone Black
```
ip link set can1 up type can bitrate 500000
cansend can1 127#DEADBEEF
```
on second BeagleBone Black
```
ip link set can1 up type can bitrate 500000
candump can1
```

if you are using Modbery 500 M3, as one of device, replace can1 with can0
