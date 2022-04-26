#/bin/bash
set -e

LOCUSER=$USER
LOCHOME=$HOME

# the case for automatic installation where we are not logged in
if [ -z "${LOCHOME}" ]; then
        LOCHOME=/root
fi;
# the case for automatic installation where we are not logged in
if [ -z "${LOCUSER}" ]; then
        LOCUSER=/root
fi;

BPIR2=$LOCHOME/bpi-r2/controller_software/LeafController/BananaPiR2
CONTROLLINO=$LOCHOME/bpi-r2/controller_software/LeafController/Controllino
BPIBOOT=$LOCHOME/bpi-r2/mount_points/BPI-BOOT
BPIROOT=$LOCHOME/bpi-r2/mount_points/BPI-ROOT
#BPIBOOT=/media/$LOCUSER/BPI-BOOT
#BPIROOT=/media/$LOCUSER/BPI-ROOT
IMGFOLDER=$LOCHOME/bpi-r2

localhelp () {
  echo "Usage: cpimg2emmc.sh fluenceimagefile"
  echo "The file must be stored inside $LOCHOME/bpi-r2/"
}

if [ -z "$1" ]; then
  echo "Image file name is missing"
  localhelp 
  exit 1
fi

FLUENCEIMAGE=$1

if [ ! -s $IMGFOLDER/$FLUENCEIMAGE ]; then
  echo "Image file is missing or empty"
  localhelp
  exit 1
fi

mkdir -p ${BPIROOT}
mkdir -p ${BPIBOOT}

BOOTIMG=BPI-R2-EMMC-boot0-DDR1600-0k-0905.img.gz

echo enable /dev/mmcblk1boot0 write mode
echo 0 > /sys/block/mmcblk1boot0/force_ro

#write once is enought but how to check
bpi-bootsel ${LOCHOME}/bpi-r2/$BOOTIMG /dev/mmcblk1boot0
echo 1 > /sys/block/mmcblk1boot0/force_ro

echo copy image to EMMC
bpi-copy2 ${LOCHOME}/bpi-r2/$FLUENCEIMAGE /dev/mmcblk1 

echo Make EMMC bootable
mmc bootpart enable 1 1 /dev/mmcblk1

echo Mount EMMC
mount /dev/mmcblk1p1 ${LOCHOME}/bpi-r2/mount_points/BPI-BOOT
mount /dev/mmcblk1p2 ${LOCHOME}/bpi-r2/mount_points/BPI-ROOT

cp -rp ${LOCHOME}/bpi-r2/next_boot_from.sh ${LOCHOME}/bpi-r2/mount_points/BPI-ROOT/usr/local/bin/fluence_next_boot_from.sh

echo Change mount points
sed -i 's/mmcblk0/mmcblk1/g' ${LOCHOME}/bpi-r2/mount_points/BPI-ROOT/etc/fstab
sed -i 's/mmcblk0/mmcblk1/g' ${LOCHOME}/bpi-r2/mount_points/BPI-BOOT/bananapi/bpi-r2/linux/uEnv.txt

#Force reinitalization of fluence enviroment
rm -f ${LOCHOME}/bpi-r2/mount_points/BPI-ROOT/etc/FLUENCE_INITIALIZED
cp -p ${LOCHOME}/bpi-r2/mount_points/BPI-ROOT/etc/netplan/network.yaml_start ${LOCHOME}/bpi-r2/mount_points/BPI-ROOT/etc/netplan/network.yaml

echo Umount EMMC
umount /dev/mmcblk1p1 
umount /dev/mmcblk1p2
