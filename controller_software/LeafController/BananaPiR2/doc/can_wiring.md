## Wiring

This is about how to wire the SBC-CAN01 module to the BananaPi R2:


| SBC-CAN01	| Banana Pi R2      |
|-------|----------------------|
|INT    | 16 (GPIO 25, interrupt 3) |
|SCK    | 23 (SCK)             |
|SI     | 19 (MOSI)            |
|S0     | 21 (MISO)            |
|CS     | 24 (CE0)             |
|GND    | 6 (GND)              |
|VCC1   | 2 (5V)               |
|VCC    | 1 (3.3V)             |

The schematics for V1.2 board is in doc folder as PDF.

## Test
First install canutils with ```apt-get install can-utils```.

Then

on first device

```
ip link set can0 up type can bitrate 500000
cansend can0 127#DEADBEEF
```
on second device
```
ip link set can0 up type can bitrate 500000
candump can0
```
