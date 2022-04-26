#!/bin/sh

# Switches the next boot to emmc or sdcard

NEXTBOOT=$1

if [ -z "${NEXTBOOT}" ] || [ "${NEXTBOOT}" != "emmc" ] && [ "${NEXTBOOT}" != "sdcard" ]]; then
	echo "Please call 'next_boot_from (emmc|sdcard)'"
        exit 1
fi

if [ "${NEXTBOOT}" = "emmc" ]; then
	mkdir -p /tmp/emmc
	mount /dev/mmcblk1p1 /tmp/emmc
	cp -rp /tmp/emmc/bananapi/bpi-r2/linux/uEnv_emmc.txt /tmp/emmc/bananapi/bpi-r2/linux/uEnv.txt
	umount /tmp/emmc
fi

if [ "${NEXTBOOT}" = "sdcard" ]; then
	mkdir -p /tmp/emmc
	mount /dev/mmcblk1p1 /tmp/emmc
	cp -rp /tmp/emmc/bananapi/bpi-r2/linux/uEnv_sd.txt /tmp/emmc/bananapi/bpi-r2/linux/uEnv.txt
	umount /tmp/emmc
fi
