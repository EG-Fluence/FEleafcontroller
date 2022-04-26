#!/bin/sh
#

# Automatic install script for BPi

COUNT_SECONDS=20
FORCE=$1

USER=root
HOME=/root

STAGE1FILE=${HOME}/bpi-r2/mount_points/emmc_boot/bananapi/bpi-r2/linux/STAGE1_DONE

mount | egrep " / " | egrep mmcblk0p2 2>&1 >/dev/null
if [ $? -eq 0 ]; then
	echo "You're running on SD card now !" > /etc/motd
	echo "You're running on SD card now !" > /etc/issue
fi
mount | egrep " / " | egrep mmcblk1p2 2>&1 >/dev/null
if [ $? -eq 0 ]; then
	echo "System installed. You're running on eMMC now !" > /etc/motd
	echo "System installed. You're running on eMMC now !" > /etc/issue
	exit 0
fi

mkdir -p ${HOME}/bpi-r2/mount_points/emmc_boot
mkdir -p ${HOME}/bpi-r2/mount_points/sd_boot

fdisk -l /dev/mmcblk1 | egrep "mmcblk1p2" 2>&1 >/dev/null
if [ $? -eq 0 ] && [ "$FORCE" != "force" ]; then
	# Check if installation is in STAGE1_DONE
	mount /dev/mmcblk1p1 ${HOME}/bpi-r2/mount_points/emmc_boot
	if [ -f ${STAGE1FILE} ]; then
		echo "System installation in progress ..." >> /etc/motd
		echo "System installation in progress ..." >> /etc/issue
		cp -rp ${HOME}/bpi-r2/mount_points/emmc_boot/bananapi/bpi-r2/linux/uEnv_emmc.txt ${HOME}/bpi-r2/mount_points/emmc_boot/bananapi/bpi-r2/linux/uEnv.txt
		rm -rf ${HOME}/bpi-r2/mount_points/emmc_boot/bananapi/bpi-r2/linux/STAGE1_DONE
		umount ${HOME}/bpi-r2/mount_points/emmc_boot
		mount /dev/mmcblk0p1 ${HOME}/bpi-r2/mount_points/sd_boot
		cp -rp ${HOME}/bpi-r2/mount_points/sd_boot/bananapi/bpi-r2/linux/uEnv.txt.old ${HOME}/bpi-r2/mount_points/sd_boot/bananapi/bpi-r2/linux/uEnv.txt
		umount ${HOME}/bpi-r2/mount_points/sd_boot
		reboot
	fi
	umount ${HOME}/bpi-r2/mount_points/emmc_boot
	echo "System already installed in eMMC !" >> /etc/motd
	echo "System already installed in eMMC !" >> /etc/issue
	echo "System already installed in eMMC, exiting ..."
	exit 0
fi

echo "System installation in progress ..." >> /etc/motd
echo "System installation in progress ..." >> /etc/issue

echo "Automatic installation will start in ${COUNT_SECONDS} seconds"

while [ ${COUNT_SECONDS} -gt 0 ]
do
  echo "Installing in ${COUNT_SECONDS} ..."
  COUNT_SECONDS=`expr $COUNT_SECONDS - 1`
  sleep 1
done

cd ${HOME}/bpi-r2

FLUENCE_IMAGE_FILENAME=`find . -maxdepth 1 -name "fluence_*.img.*" | sort -n | tail -1`

if [ -z "${FLUENCE_IMAGE_FILENAME}" ]; then
	echo "No fluence image filename found"
	exit 1
fi;

echo "Installing image ${FLUENCE_IMAGE_FILENAME}"

echo 1 > /sys/class/leds/bpi-r2:isink:blue/brightness
echo timer > /sys/class/leds/bpi-r2:isink:blue/trigger
echo 100 > /sys/class/leds/bpi-r2:isink:blue/delay_on
echo 100 > /sys/class/leds/bpi-r2:isink:blue/delay_off

/usr/local/bin/fluence_partition-maxsize.sh
${HOME}/bpi-r2/cpimg2emmc.sh ${FLUENCE_IMAGE_FILENAME}

if [ $? -ne 0 ]; then
	echo "Something went wrong with installation ..."
	echo 0 > /sys/class/leds/bpi-r2:isink:blue/brightness
	exit 1
fi

echo 0 > /sys/class/leds/bpi-r2:isink:blue/brightness

mount /dev/mmcblk0p1 ${HOME}/bpi-r2/mount_points/sd_boot
mount /dev/mmcblk1p1 ${HOME}/bpi-r2/mount_points/emmc_boot

mv ${HOME}/bpi-r2/mount_points/sd_boot/bananapi/bpi-r2/linux/uEnv.txt ${HOME}/bpi-r2/mount_points/sd_boot/bananapi/bpi-r2/linux/uEnv.txt.old
cp -rp uEnv_changevars.txt ${HOME}/bpi-r2/mount_points/sd_boot/bananapi/bpi-r2/linux/uEnv.txt

cp -rp uEnv_emmc.txt ${HOME}/bpi-r2/mount_points/emmc_boot/bananapi/bpi-r2/linux
cp -rp uEnv_sd.txt ${HOME}/bpi-r2/mount_points/emmc_boot/bananapi/bpi-r2/linux
cp -rp uEnv_sd.txt ${HOME}/bpi-r2/mount_points/emmc_boot/bananapi/bpi-r2/linux/uEnv.txt

touch ${HOME}/bpi-r2/mount_points/emmc_boot/bananapi/bpi-r2/linux/STAGE1_DONE

sync

umount ${HOME}/bpi-r2/mount_points/emmc_boot
umount ${HOME}/bpi-r2/mount_points/sd_boot

mkdir /tmp/emmcroot
mount /dev/mmcblk1p2 /tmp/emmcroot
echo "System installed. You're running on eMMC now !" > /tmp/emmcroot/etc/motd
echo "System installed. You're running on eMMC now !" > /tmp/emmcroot/etc/issue
umount /tmp/emmcroot

reboot
