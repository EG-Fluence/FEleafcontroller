# EMMC

Empty EMMC on Banana Pi R2
  
  ```
  root@leaf-dc2b2f355c:/usr/local/bin# fdisk -lu /dev/mmcblk1
  Disk /dev/mmcblk1: 7.3 GiB, 7818182656 bytes, 15269888 sectors
  Units: sectors of 1 * 512 = 512 bytes
  Sector size (logical/physical): 512 bytes / 512 bytes
  I/O size (minimum/optimal): 512 bytes / 512 bytes

  root@leaf-dc2b2f355c:~# fdisk -lu /dev/mmcblk1boot0
  Disk /dev/mmcblk1boot0: 4 MiB, 4194304 bytes, 8192 sectors
  Units: sectors of 1 * 512 = 512 bytes
  Sector size (logical/physical): 512 bytes / 512 bytes
  I/O size (minimum/optimal): 512 bytes / 512 bytes

  root@leaf-dc2b2f355c:~# fdisk -lu /dev/mmcblk1boot1
  Disk /dev/mmcblk1boot1: 4 MiB, 4194304 bytes, 8192 sectors
  Units: sectors of 1 * 512 = 512 bytes
  Sector size (logical/physical): 512 bytes / 512 bytes
  I/O size (minimum/optimal): 512 bytes / 512 bytes
  ```

[Frank-W EMMC](http://www.fw-web.de/dokuwiki/doku.php?id=en:bpi-r2:storage)

as root

  ```
  apt-get install pv, curl
  cd
  mkdir bpi-r2
  cd bpi-r2
  mkdir -pv {mount_points/{BPI-ROOT,BPI-BOOT}}
  ```
  
  Add BPI-TOOLS
  ```
  curl -sL https://github.com/BPI-SINOVOIP/bpi-tools/raw/master/bpi-tools | sudo -E bash
  ```


An easier way to write an image to emmc For example, as an ubuntu. 
 
  ```
  #increase SD card to 32G
  fluence_partition-maxsize.sh
  
  #download BPI-R2-EMMC-boot0-0k.img.gz
  scp user@192.168.178.XXX:~/bpi-r2/controller_software/BananaPiR2/images/BPI-R2-EMMC-boot0-0k.img.gz .
  
  #download /path_to_your_image/new.img.zip
  scp user@192.168.178.XXX:~/bpi-r2/controller_software/BananaPiR2/images/new.img.zip . 
    
  #enable /dev/mmcblk1boot0 write mode
  echo 0 > /sys/block/mmcblk1boot0/force_ro 

  #write once
  bpi-bootsel BPI-R2-EMMC-boot0-0k.img.gz /dev/mmcblk1boot0 

  bpi-copy /path_to_your_image/new.img.zip /dev/mmcblk1
  ```
or something like
  ```
  dd if=lede.img of=/dev/mmcblk1
  ```

## MMC Utils  
  
Make EMMC bootable  
  ```  
  apt-get install mmc-utils

  mmc extcsd read /dev/mmcblk1 | grep PARTITION_CONFIG
         Boot configuration bytes [PARTITION_CONFIG: 0x00]

  mmc bootpart enable 1 1 /dev/mmcblk1

  mmc extcsd read /dev/mmcblk1 | grep PARTITION_CONFIG
         Boot configuration bytes [PARTITION_CONFIG: 0x48]

  ```

## Change root and boot device of EMMC
  
Mount EMMC 
  ```  
  mount /dev/mmcblk1p1 ~/bpi-r2/mount_points/BPI-BOOT
  mount /dev/mmcblk1p2 ~/bpi-r2/mount_points/BPI-ROOT
  ```  
Change mount points
  ``` 
  nano ~/bpi-r2/mount_points/BPI-ROOT/etc/fstab
  
  # UNCONFIGURED FSTAB FOR BASE SYSTEM
  # <file system> <dir>           <type>  <options>               <dump>  <pass>
  /dev/mmcblk1p1  /boot           vfat    errors=remount-ro       0       1
  /dev/mmcblk1p2  /               ext4    defaults                0       0
  ``` 
or
  ``` 
  sed -i 's/mmcblk0/mmcblk1/g' ~/bpi-r2/mount_points/BPI-ROOT/etc/fstab
  ``` 
Umount EMMC 
  ``` 
  umount /dev/mmcblk1p1
  umount /dev/mmcblk1p2
  ``` 

Power off Banana PI and remove SD card. Power on Banana PI. It must start from EMMC.

## Scripts

Work Flow:

1. Prepare new image for Leaf Controller
2. Flash the image on host computer (Ubuntu) to SD Card (restoresd.sh)
3. Boot Leaf Controller from SC Card. 
4. From host computer prepare SD Card on leaf controller as special version (cpimg2remoteeemmc.sh). It will copy the required software into /root/bpi-r2
5. SD Card is ready to move OS to eMMC . Execute on LC (cpimg2emmc.sh)
6. For next Leaf Controller, boot LC from already prepared SD Card and repeat step 5. 

Additional readings
- [Copy image to EMMC](../../../tools/bin/README.md#copy-image-to-emmc)
- [cpimg2emmc.sh](../../../tools/bin/README.md#cpimg2emmcsh)
- [cpimg2remoteemmc.sh](../../../tools/bin/README.md#cpimg2remoteemmcsh)


