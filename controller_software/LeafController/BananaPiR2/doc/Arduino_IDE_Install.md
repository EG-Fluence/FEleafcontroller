# Arduino IDE 

## Add package
```
sudo apt-get install usbutils
```
## Old

https://www.ftdichip.com/Drivers/D2XX.htm
https://www.ftdichip.com/Drivers/D2XX/Linux/ReadMe-linux.txt

```
cd
cd Download
wget https://www.ftdichip.com/Drivers/D2XX/Linux/libftd2xx-arm-v7-hf-1.4.8.gz
tar xzvf libftd2xx-arm-v7-hf-1.4.8.gz
cd release
cd build
cp libftd2xx.* /usr/local/lib
chmod 0755 /usr/local/lib/libftd2xx.so.1.4.8
ln -sf /usr/local/lib/libftd2xx.so.1.4.8 /usr/local/lib/libftd2xx.so
```

```
apt-get install arduino arduino-core arduino-mk
apt-get remove arduino arduino-core arduino-mk
```

### lsusb
```
root@bpi-prototype-01:/dev# lsusb
Bus 004 Device 001: ID 1d6b:0003 Linux Foundation 3.0 root hub
Bus 003 Device 003: ID 2341:0042 Arduino SA Mega 2560 R3 (CDC ACM)
Bus 003 Device 001: ID 1d6b:0002 Linux Foundation 2.0 root hub
Bus 002 Device 001: ID 1d6b:0003 Linux Foundation 3.0 root hub
Bus 001 Device 001: ID 1d6b:0002 Linux Foundation 2.0 root hub
```

```
usermod -a -G dialout root
chmod a+rw /dev/ttyACM0
```

## New

Download from __[Arduino](https://www.arduino.cc/en/Main/Software)__ __Linux ARM 32 bits__

### Copy on target computer

```
scp -oProxyJump=bkajganic.admin@10.250.225.7,bkajganic.admin@10.194.5.8 arduino-1.8.12-linuxarm.tar.xz root@172.16.1.19:~/.
```

### Install Arduino

```
tar xvf arduino-1.8.12-linuxarm.tar.xz -C /usr/local/share
mv /usr/local/share/arduino{-1.8.12,}
```

### Install Arduino IDE

```
pushd /usr/local/share/arduino
./install.sh
popd
```

### Uninstall Arduino IDE

```
pushd /usr/local/share/arduino
./uninstall.sh
popd
```

### Upload Sketch to Controllino

Use script __[controllino_upload.sh](../root/usr/local/bin/controllino_upload.sh)__ 

#### Usage
```
controllino_upload.sh Arduino_Blink.hex
```

### Reset Controllino

Use script __[controllino_reset.sh](../root/usr/local/bin/controllino_reset.sh)__ 

#### Usage
```
controllino_reset.sh
```

### Serial monitor to Controllino

Use script __[controllino_stty.sh](../root/usr/local/bin/controllino_stty.sh)__ 

#### Usage
```
controllino_stty.sh 115200
```

The stty command sets the parameters and speed of the COM port. Its format is:

stty [-F DEVICE | --file=DEVICE] [SETTING]...

for our case:
```
stty 9600 -F /dev/ttyACM0 raw -echo
```

#### Receive data Controllino
Enter the command in the console:
```
$ cat /dev/ttyACM0
```
To see the hex data codes coming from the device, use the hexdump command.
```
cat /dev/ttyACM0 | hexdump -C
```
To output data from the device to the screen and to a text file, need to use tee:
```
cat /dev/ttyACM0 | tee output.txt
```

#### Send Commands to Controllino
Enter in another console:
```
$ echo -n "Command" > /dev/ttyACM0
```
