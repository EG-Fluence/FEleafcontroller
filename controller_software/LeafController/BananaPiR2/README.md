# Banana Pi R2 Documentation

| Link                                                      | Description                  |
|-----------------------------------------------------------|------------------------------|
| [doc/Info](doc/BPiR2Info.md)                              | Info                         |
| [doc/hardware_setup](doc/hardware_setup.md)               | Hardware Setup               |
| [doc/customizing_image](doc/customizing_image.md)         | Customizing Image            |
| [doc/kernel_build](doc/kernel_build.md)                   | Kernel Build                 |
| [doc/image_creation_notes](doc/image_creation_notes.md)   | Image Creation Notes         |
| [doc/network_configuration](doc/network_configuration.md) | Network Configuration        |
| [doc/lldp_notes](doc/lldp_notes.md)                       | LLDP Notes                   |
| [doc/Arduino IDE](doc/Arduino_IDE_Install.md)             | Upload sketch to Controllino |
| [doc/backup_restore](doc/backup_restore_image.md)         | Backup and restore OS image  |
| [doc/EMMC](doc/EMMC.md)                                   | Move OS image to EMMC        |
| [images](images/README.md)                                | Fluence Images               |
| [doc/Fluence Software](doc/fluence_software.md)           | Fluence Software (DAS, UPS)  |
| [doc/CAN Wiring](doc/can_wiring.md)                       | CAN Module wiring            |

## Banana Pi R2 default passwords

| Login	| Password	| Permissions      |
|-------|-----------|------------------|
| fos 	| fos 	    | User permissions |
| root	| root    	| Root permissions |


## Additional packages
To install aditional packages on Banana Pi __br1__ must be down!
Routing problem. Solved in new releases of the Fluence images.

```bash
ip link set br1 down
apt install dnsutils
ip link set br1 up
```
# Banana Pi R2 Additional Software

| Link                                               | Description           |
|----------------------------------------------------|-----------------------|
| [doc/DAS](doc/DAS.md)                              | DAS                   |
| [doc/UPS](doc/UPS.md)                              | UPS                   |

# Installation
1) Insert empty SD card into Ubuntu
2) Copy last good fluence_$TIMESTAMP.img or fluence_$TIMESTAMP.img.tar.gz to the SD Card ```restoresd.sg fluenceimage``` Use fluence image without ___master__ in name.
3) ```mountsd.sh``` mount SD card
4) ```cpgit2sd.sh``` copy the last from git to SD card
5) Test SD card inside Banan Pi (activate some missing service, or manualy add files under /root)
6) Shutdown Banana Pi
7) Insert SD card back into Ubuntu
8) Mount SD card
9) exacute ```fosresetsd.sh``` This will reset network configuration inside SD Card and it will force reinitalization of the SD card on first next start.
10) ```backupsd.sh pack``` Backup SD card as compressed fluence_$TIMENOW.img.tar.gz inside ```~//bpi-r2/backup/```
11) [Make Master SD Card](../../tools/bin/README.md#conv_create_installation_sdcardsh) with help of ```conv_create_installation_sdcard.sh```
12) Test the installation on Banana Pi 
13)  ```backupsd.sh master pack``` Backup Master SD card compressed fluence_$TIMENOW_master.img.tar.gz inside ```~//bpi-r2/backup/```

# External Documentation

- [Getting Started with R2](https://wiki.banana-pi.org/Getting_Started_with_R2)
- [Banana Pi Wiki](http://wiki.banana-pi.org/Banana_Pi_BPI-R2)
- [Banana Pi Documentation Links](http://wiki.banana-pi.org/Banana_Pi_BPI-R2#Documents)
- [About banana pi BPI-R2](https://bananapi.gitbook.io/banana-pi-bpi-r2-open-source-smart-router/)

# Banana Pi R2 PRO

We cannot us the board because RealTek L2 switch don’t support linux DSA feature. Creation of network bridge between WLAN anf LAN3 is not possible.


