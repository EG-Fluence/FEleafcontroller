# Banana Pi R2 Image Documentation

| Link                                                      | Description           |
|-----------------------------------------------------------|-----------------------|
| [../doc/EMMC](../doc/EMMC.md)                             | Move OS image to EMMC |
| [Image Change Log](https://fluenceenergy.sharepoint.com/:t:/r/sites/nextgen/Shared%20Documents/Controls%20HW%20and%20SW/NextGen%20Controller%20Workstream/Device%20Images/Image%20Change%20Log.txt?csf=1&web=1&e=sUpfCK)  | Image Change Log File |
| [Fluence Images](https://fluenceenergy.sharepoint.com/:f:/r/sites/nextgen/Shared%20Documents/Controls%20HW%20and%20SW/NextGen%20Controller%20Workstream/Device%20Images?csf=1&web=1&e=Y9RSZV)                             | Fluence Images in SharePoint|

## erase_emmc.sh
Erases the eMMC, uboot partition and system partition

## cpimg2emmc.sh
Execute on remote Banana PI. Storaged image will be copied to eMMC. the location of script is inside ```/root/~/bpi-r2/```

Example
```
root@leaf-dc2b2f355c:~/bpi-r2#: ./cpimg2emmc.sh fluence_DataStamp.img.gz
```
Additional read
- [Manual configuration of EMMC](../doc/EMMC.md) 

## Automatic Installation
### General
Be sure that the switch on the board is always in eMMC position. It's the position in direction of the black power plug.
For automatic installation, we have one script essentially and a service which could run on Master SD card or eMMC. The service is calling the auto_install.sh script which performs automatic installation procedure. 

### auto_install.sh
On eMMC, it will do nothing.
On SD card (Master SD card), it will check if eMMC system has already been installed. If it is already installed, it will do nothing. Otherwise it performs all required operations, flashing the u-boot, flashing the boot- and rootfs to eMMC, then performs a series (three) of reboots to change u-boot startup. At the end, the system will start in eMMC.

### next_boot_from.sh
It takes the parameters "emmc" or "sdcard" to change u-boot properly so that the next boot of the system will go into eMMC system or SD system.
```
next_boot_from.sh (emmc|sdcard)
```
If the SD card is removed, the system will always boot into eMMC (if installed for sure).

### fluence_autoinstall.service
The service that will be installed in Master SD card and activated. This service is called when all other services are started. On a clean BPi, it will then call automatic_install.sh which makes a countdown to install system into eMMC properly.
