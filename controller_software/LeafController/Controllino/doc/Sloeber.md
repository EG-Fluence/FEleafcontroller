# Sloeber 

Sloeber is a free, open source, Eclipse IDE for Arduino development.

## Download

| Version |
| ------- |
| [Official Linux](https://github.com/Sloeber/arduino-eclipse-plugin/releases/download/V4_3_1/V4.3.1_linux64.2018-10-10_08-21-58.tar.gz) |
| [Official Windows](https://github.com/Sloeber/arduino-eclipse-plugin/releases/download/V4_3_1/V4.3.1_win64.2018-10-10_08-21-58.tar.gz) |
| [Last Releases from Git Hub](https://github.com/Sloeber/arduino-eclipse-plugin/releases) |

### Linux

download https://github.com/Sloeber/arduino-eclipse-plugin/releases/linux64.2019-10-07_08-26-30.tar.gz into $HOME

```
tar -xzvf linux64.2019-10-07_08-26-30.tar.gz
cd Sloeber
./sloeber-ide
```

Default Sloeber Workspace 
/home/boris/Documents/sloeber-workspace

### Windows

download https://github.com/Sloeber/arduino-eclipse-plugin/releases/win64.2019-10-07_08-26-30.tar.gz
and unpack with 7zip in one folder


Default Sloeber Workspace
C:\Users\Boris.Kajganic\Documents\sloeber-workspace

## Add Libraries and Boards


__Menu->Arduino->Preferences->Arduino__


Add libraries:
C:\FProjects\Arduino\libraries

Add hardware (boards):
C:\FProjects\Arduino\hardware

### Third party index Url's
```
http://downloads.arduino.cc/packages/package_index.json
https://raw.githubusercontent.com/jantje/hardware/master/package_jantje_index.json
https://raw.githubusercontent.com/jantje/ArduinoLibraries/master/library_jantje_index.json
http://arduino.esp8266.com/stable/package_esp8266com_index.json
http://downloads.arduino.cc/libraries/library_index.json
https://raw.githubusercontent.com/CONTROLLINO-PLC/CONTROLLINO_Library/master/Boards/package_ControllinoHardware_index.json
```
Reastart Sloeber IDE

### Platforms and Boards

Enable
- Add arduino -> Arduino megaAVR Bords -> 1.8.5
- Add CONTROLLINO_Boards -> CONTROLLINO Boards -> 3.0.2


## Unofficial list of 3rd party boards support urls
https://github.com/arduino/Arduino/wiki/Unofficial-list-of-3rd-party-boards-support-urls

## Real Time Plotting Tool ( Oscilloscope )

[Plotter/Oscilloscope](https://forum.arduino.cc/index.php?topic=58911.0)





