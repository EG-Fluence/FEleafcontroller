# Introduction

The can2ethernet (abbrev. c2e) software is a piece of code that acts as a gateway between CAN bus and multicast UDP ip.

Principle:

The c2e listens on multicast IP port and CAN bus interface specified as parameters. A packet on CAN that comes in will be
transparently forwarded without modification on the multicast IP.
A Multicast packet that comes in will be transparently forwarded on the can interface.

The code does not modify the CAN/muticast messages/packets

# Installation

## Compile

Get the code and run

```
./configure
make
```

and you will have a binary in
<pre> ./src/can2ethernet </pre>

## Running

Be sure that the can0 interface exists. If not bring it up with:

```
ip link set can0 up type can bitrate 500000
```

Call the Binary with the apropriate parameters. If you call it with '-h' you will get a help.
if you call it without parameter(s) each value will be replaced by its default below.

```
can2ethernet usage:
can2ethernet <params>
Params:
  -a || --mcast-ip-address : Multicast IP address, default: 239.0.0.1
  -p || --mcast-ip-port    : Multicast IP port, default: 6000
  -c || --can-interface    : can interface name, default: can0
```
