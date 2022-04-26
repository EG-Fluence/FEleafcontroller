# Arduino Development

- [Setup IDE](doc/Sloeber.md)

# Controllino

## Controllino MEGA

- [Datasheet](doc/MEGA_DATASHEET-19-04-20161.pdf)
- [Pinout](doc/CONTROLLINO-MEGA-Pinout.pdf)

## Controllino MAXI

- [Datasheet](doc/MAXI_DATASHEET-19-04-20161.pdf)
- [Pinout](doc/CONTROLLINO-MAXI-Pinout.pdf)

## Usage

- [Debugging](doc/Debugging.md)
- [Modbus Mapping](doc/Modbus_Mapping.md)
- [IR Addresses](doc/IR_Sensor_Addresses.md)

# LeafController Base on Controllino

## Usage

### Web 

Test working with (replace with proper IP address if changed).
All GET works in Web brouser!

Return HTTP identify
```
curl -i -X GET "http://192.168.0.107"
```
Return show status of all relays and digital outputs
```
curl -i -X GET "http://192.168.0.107/all" 
```
Show status of all relays
```
curl -i -X GET "http://192.168.0.107/relays"                 
```
Show relays with status on
```
curl -i -X GET "http://192.168.0.107/relays?state=on"
```
Show status of relay 3
```
curl -i -X GET "http://192.168.0.107/relays/3"               
```
Show (invalid relay)
```
curl -i -X GET "http://192.168.0.107/relays/44"              
```
Switch all relays on
```
curl -i -X PUT "http://192.168.0.107/relays?state=on"
```
Switch all relays off
```
curl -i -X PUT "http://192.168.0.107/relays?state=off"       
```
Switch relay 3 on
```
curl -i -X PUT "http://192.168.0.107/relays/3?state=on"     
```
Switch relay 3 off
```
curl -i -X PUT "http://192.168.0.107/relays/3?state=off"     
```
Error (invalid value)
```
curl -i -X PUT "http://192.168.0.107/relays/3?state=blink"
```
Show status of all digital outputs
```
curl -i -X GET "http://192.168.0.107/digitaloutputs"                 
```
Show digital outputs with status on
```
curl -i -X GET "http://192.168.0.107/digitaloutputs?state=on"
```
Show status of digital output 3
```
curl -i -X GET "http://192.168.0.107/digitaloutputs/3"               
```
Show (invalid digital outputs)
```
curl -i -X GET "http://192.168.0.107/digitaloutputs/44"
```
Switch all digital outputs on
```
curl -i -X PUT "http://192.168.0.107/digitaloutputs?state=on"
```
Switch all digital outputs off
```
curl -i -X PUT "http://192.168.0.107/digitaloutputs?state=off" 
```
Switch digital output 3 on
```
curl -i -X PUT "http://192.168.0.107/digitaloutputs/3?state=on"
```
Switch digital output 3 off
```
curl -i -X PUT "http://192.168.0.107/digitaloutputs/3?state=off"
```
Error (invalid value)
```
curl -i -X PUT "http://192.168.0.107/digitaloutputs/3?state=blink"   
```
Show status of all digital inputs
```
curl -i -X GET "http://192.168.0.107/digitalinputs"
```
Show digital outputs with status on
```
curl -i -X GET "http://192.168.0.107/digitalinputs?state=on"
```
Show status of digital output 3
```
curl -i -X GET "http://192.168.0.107/digitalinputs/3" 
```
Show (invalid digital outputs)
```
curl -i -X GET "http://192.168.0.107/digitalinputs/44"             
```

### Modbus

[QModMaster](https://sourceforge.net/projects/qmodmaster/) is a free Qt-based implementation of a ModBus master application. 

[QModSlave](https://sourceforge.net/projects/pymodslave/) pyModSlave is a free python-based implementation of a ModBus slave application for simulation purposes. You can install the python module or use the precompiled (for Windows only) stand alone GUI (Qt based) utility (unzip and run).





