#!/bin/sh

# this thing erases eMMC uboot and rootfs

echo 0 > /sys/block/mmcblk1boot0/force_ro
dd if=/dev/zero of=/dev/mmcblk1boot0
dd if=/dev/zero of=/dev/mmcblk1 bs=512 count=1
echo 1 > /sys/block/mmcblk1boot0/force_ro
sync
