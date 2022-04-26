## Customizing Image

If it is needed to customize the operating system prior to first boot, or look at the root file system to find system information or other troubleshsooting, the root and boot file system can be mounted to a host.

| Partition | File System Type | Description | Size |
|-----------|------------------|-------------|------|
| free space|                  | boot loader | 100M |
| 1         | FAT32            | /boot       | 256M |
| 2         | ext4             | /           | 7G   |

info:

```
boris@ubuntu:~/bpi-r2$ sudo fdisk -lu /dev/sdb
Disk /dev/sdb: 29.8 GiB, 32010928128 bytes, 62521344 sectors
Units: sectors of 1 * 512 = 512 bytes
Sector size (logical/physical): 512 bytes / 512 bytes
I/O size (minimum/optimal): 512 bytes / 512 bytes
Disklabel type: dos
Disk identifier: 0x000ed6cd

Device     Boot  Start      End  Sectors  Size Id Type
/dev/sdb1       204800   729087   524288  256M  c W95 FAT32 (LBA)
/dev/sdb2       729088 15409159 14680072    7G 83 Linux
```


The simplest way to define partition is GUI program gparted.

```
sudo apt install gparted
sudo gparted
```

If you prefer console, you can use ```fdisk```, ```cfdisk``` or ```parted``` too.

```
sudo apt-get install parted zip
```
### Create image file (7GB):
```
sudo dd status=progress if=/dev/zero bs=1M count=7296 of=bpir2.img
```
### Load image as virtual drive:
```
sudo losetup /dev/loop16 bpir2.img
```

free  ```loopX ``` could be find with:
```
sudo losetup --list
```

### Make partitions and format:
```
sudo parted -s /dev/loop16 mklabel msdos

sudo parted -s /dev/loop16 unit MiB mkpart primary fat32 -- 100MiB 356MiB

sudo parted -s /dev/loop16 unit MiB mkpart primary ext4 -- 356MiB 7295MiB

sudo partprobe /dev/loop16

sudo mkfs.vfat /dev/loop16p1 -I -n BPI-BOOT

sudo mkfs.ext4 -O ^has_journal -E stride=2,stripe-width=1024 -b 4096 /dev/loop16p2 -L BPI-ROOT

sudo sync

sudo parted -s /dev/loop16 print
```

### Remove loop device:
```
sudo losetup -d /dev/loop16
```

## Additional information about backup or restore SD card and trunctate of the image
[backup/restore](backup_restore_image.md)
