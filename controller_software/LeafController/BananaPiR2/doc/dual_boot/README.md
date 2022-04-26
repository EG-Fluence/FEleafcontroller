# U-boot on Banana-pi R2

### Behaviour with SD/EMMC switch

The SD/EMMC switch determines from which device the preloader/u-boot must be loaded. The table below describes the boot order depending on the switch. If no preloader/uboot is present on the sd not the EMMC, the board wont boot, and the rgb leds will stay on until we leave the power button.

| Switch | SD  | U-Boot/preloader |
| ------ | --- | ---------------- |
| SD     | yes | SD               |
| SD     | no  | EMMC             |
| EMMC   | yes | EMMC             |
| EMMC   | no  | EMMC             |

***NOTE***: The switch does not define which OS (i.e kernel/rootfs) will be loader by u-boot. The u-boot and uEnv.txt files I found were all first checking if an OS is available on SD card, and if yes boot from SD in priority, booting the OS from EMMC only in case there is no SD.

### The devices in u-boot and linux

We can access the devices from uboot and linux. We must be aware that in linux, sd is refered with 0 whereas it is 1 in u-boot. The id of each storage device in linux (mmcblk0-1) should be defined somewhere in the DTB, so in theory it can be changed.

| Partition     | U-boot | Linux          |
| ------------- | ------ | -------------- |
| SD/Boot       | 1:1    | /dev/mmcblk0p1 |
| SD/RootFS     | 1:2    | /dev/mmcblk0p2 |
| **EMMC/Boot** | 0:1    | /dev/mmcblk1p1 |
| EMMC/RootFS   | 0:2    | /dev/mmcblk1p2 |

### Choosing the OS using uEnv.txt

- Location of uEnv.txt: The file is located on the first partition (boot partition, mmcblkXp1). 

- U-boot will first check uEnv.txt from SDCard (if it does exist).

- If we modify uEnv.txt on the SDCard we can boot one OS or the other

- 2 Variables must be modified in uEnv.txt for the boot from eMMC or SD:
  
  - root will define the rootfs (passed in the kernel command line, so linux format)
  
  - partition defines the partition where to load the kernel from, u-boot format.
  
  For eMMC boot:
  
  ```
  root=/dev/mmcblk1p2 rootfstype=ext4 rootwait
  partition=0:1 
  ```
  
  For SD boot:
  
  ```
  root=/dev/mmcblk0p2 rootfstype=ext4 rootwait
  partition=1:1
  ```



#### Changing default uboot variables to load uEnv.txt from eMMC

Some of the default variables must be changed, because by default, the provided u-boot will first try to grab uEnv.txt and the kernel from the SD card, then on the .

To be able to take the uEnv.txt from the eMMC even though the SD contains uEnv.txt, we must modify the default variables with setenv or editenv (e.g *setenv boot10 'mmc init 0; run boot_normal; bootm'* ).

The modified variables are:

```
boot10=mmc init 0; run boot_normal; bootm
boot_normal=if run loadbootenv; then echo Loaded environment from ${bootenv}; env import -t ${scriptaddr} ${filesize}; fi; run uenvcmd; fatload mmc 0:1 ${loadaddr} ${bpi}/${board}/${service}/${kernel}; bootm
loadbootenv=fatload ${device} 0:1 ${scriptaddr} ${bpi}/${board}/${service}/${bootenv} || fatload ${device} $0:1 ${scriptaddr} ${bootenv}
```



#### Testing if a SD is inserted:

If we choose boot from SD, we must still have a failsafe boot to eMMC if no SD is present. This can be done by the following line:

```
achoosebp=if mmc init 1 ; then echo Boot from SD ; setenv partition 1:1 ; setenv root /dev/mmcblk0p2 rootfstype=ext4 rootwait ; else echo Boot from eMMC ; mmc init 0 ; setenv partition 0:1 ; setenv root /dev/mmcblk1p2 rootfstype=ext4 rootwait ; fi
```



### Flashing ubuntu on the SD

We need a big SD (best is at least 32 GB).

The image is here:

[images – Google Drive](https://drive.google.com/drive/folders/1oP7jy1KrrIOifvImo2nQ59wx3_9hHkgk)

#### Flashing the image on the SDCard

```
sudo dd if=ubuntu-18.04-bpi-r2-preview.img of=/dev/mmcblk0 status=progress
```

Create a partition with the remaining space (The image will take ~8GB). 

Then copy the img file *ubuntu-18.04-bpi-r2-preview.img* and *BPI-R2-EMMC-boot0-DDR1600-0k-0905.img*. Those files will be used for flashing tyhe eMMC

#### Installing preloader+u-boot on the eMMC

Boot the banana pi from the SDCard (username:root, password:bananapi), then from ubuntu just flash the eMMC using

```
dd if=BPI-R2-EMMC-boot0-DDR1600-0k-0905.img of=/dev/mmcblk1boot0
```

#### Installing ubuntu on the eMMC

```
dd if=ubuntu-18.04-bpi-r2-preview.img of=/dev/mmcblk1
```

### Links (sources)

[GitHub - BPI-SINOVOIP/BPI-R2-bsp: Supports Banana Pi BPI-R2 (MT7623N) (Kernel 4.4)](https://github.com/BPI-SINOVOIP/BPI-R2-bsp/)

[en:bpi-r2:uboot [FW-Web - Wiki]](http://fw-web.de/dokuwiki/doku.php?id=en/bpi-r2/uboot)
