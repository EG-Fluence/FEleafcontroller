# Back and restore of SD image

## Backup of your SD card example:  
```
sudo dd status=progress if=/dev/sdX | gzip > bananapir2-_backup-20200125-1.img.gz
```
or if we would like limit the size of image to first 8G of SD card
```
sudo dd status=progress if=/dev/sdX of=test_image.img bs=32M count=256
```

or use ```backupsd.sh``` script
```
backupsd.sh
```
The backuped copy od SD will be stored in ~/bpi-r2/backup. Name of the file is fluence_timestamp.img


## Restore an image example:  
```
sudo cat bananapir2-_backup-20200125-1.img.gz | gunzip | sudo dd status=progress of=/dev/sdX && sudo sync
```
or
```
sudo dd status=progress if=test_image2.img of=/dev/sdX && sudo sync
```
where ```/dev/sdX``` could be detected from:
```
$ lsblk
NAME   MAJ:MIN RM   SIZE RO TYPE MOUNTPOINT
sda      8:0    0    35G  0 disk 
└─sda1   8:1    0    35G  0 part /
sdb      8:16   1  29.8G  0 disk 
├─sdb1   8:17   1   256M  0 part /media/boris/BPI-BOOT
└─sdb2   8:18   1     7G  0 part /media/boris/BPI-ROOT
sr0     11:0    1     2G  0 rom  /media/boris/Ubuntu 18.04.3 LTS amd64
```
## Truncating Image file

Extracting the partition information from the image using fdisk:
```
$ sudo fdisk -lu test_image2.img
Disk test_image2.img: 9.4 GiB, 10066329600 bytes, 19660800 sectors
Units: sectors of 1 * 512 = 512 bytes
Sector size (logical/physical): 512 bytes / 512 bytes
I/O size (minimum/optimal): 512 bytes / 512 bytes
Disklabel type: dos
Disk identifier: 0x000ed6cd

Device           Boot  Start      End  Sectors  Size Id Type
test_image2.img1      204800   729087   524288  256M  c W95 FAT32 (LBA)
test_image2.img2      729088 15409159 14680072    7G 83 Linux
```
We see, that the partition has a size of about 7Gb (14680072 * 512) and ends at 15409159, the rest is unpartitioned.

So, everything after the end of the partition can be removed.
This will be done with the tool truncate. Don't forget to add ```1``` to the number of sectors, as block-numbers start at 0.

```
$ sudo truncate --size=$[(15409159+1)*512] test_image2.img
```
check size after truncate:

```
p$ sudo fdisk -lu test_image2.img
Disk test_image2.img: 7 GiB, 7516197376 bytes, 14680073 sectors
Units: sectors of 1 * 512 = 512 bytes
Sector size (logical/physical): 512 bytes / 512 bytes
I/O size (minimum/optimal): 512 bytes / 512 bytes
Disklabel type: dos
Disk identifier: 0x000ed6cd

Device           Boot  Start      End  Sectors  Size Id Type
test_image2.img1      204800   729087   524288  256M  c W95 FAT32 (LBA)
test_image2.img2      729088 15409159 14680072    7G 83 Linux
```
The size of the image is reduced from  9.4 GiB to 7 GiB.
