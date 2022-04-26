#/bin/sh
set -e

if [ "$USER" != "root" ]; then
    echo "You have to run this script as sudo to write to /dev/sdX. Please call it with sudo."
    exit 0
fi

localhelp () {
  echo "Usage: create_installation_sd.sh <fluenceimagefile.gz> <Script directory> <SD card Device>"
  echo "       Full qualified paths are expected for both parameters. Image file must be gzipped."
  echo "Example: create_installation_sdcard.sh ~/Downloads/fluence.img.gz ~/bpi-r2/controller_software/LeafController/BananaPiR2/images  /dev/sdX|fileName"
}

if [ -z "$1" ]; then
  echo "Image file name is missing"
  localhelp
  exit 1
fi

if [ -z "$2" ]; then
  echo "Script directory is missing"
  localhelp
  exit 1
fi

if [ -z "$3" ]; then
  echo "SD card device is missing"
  localhelp
  exit 1
fi

FREE=`df -k --output=avail /tmp | tail -n1`   # df -k not df -h
if [ $FREE -lt 12582912 ]; then               # 12G = 12*1024*1024k
    echo "Sorry you have less than 12G free on your device /tmp ."
    exit 1
fi;

FLUENCEIMAGELOC=$1
FLUENCEIMAGE=${FLUENCEIMAGELOC%.*}
FLUENCEIMAGE=${FLUENCEIMAGE##*/}
#FLUENCEIMAGE=${FLUENCEIMAGELOC##*/}
SCRIPTDIR=$2
SDDEVICELOC=$3
LOCTMP=/tmp

if [ -f "${LOCTMP}/${FLUENCEIMAGE}" ]; then
    rm -f ${LOCTMP}/${FLUENCEIMAGE}
fi

#echo "Making a copy of the image ${FLUENCEIMAGE} file to ${LOCTMP} ..."
#cp -rp ${FLUENCEIMAGELOC} ${LOCTMP}
echo "Unzipping image ${FLUENCEIMAGE} ..."
gunzip -c ${FLUENCEIMAGELOC} > ${LOCTMP}/${FLUENCEIMAGE}
sync

echo "Extending the image file by 3GB"
dd status=progress if=/dev/zero conv=notrunc oflag=append bs=1M count=3000 of=${LOCTMP}/${FLUENCEIMAGE}
sync

echo "Loading the loop stuff ..."
modprobe loop
LOOPDEV=`losetup -f`
losetup ${LOOPDEV} ${LOCTMP}/${FLUENCEIMAGE}
partprobe ${LOOPDEV}
parted ${LOOPDEV} resizepart 2 100%
e2fsck -f ${LOOPDEV}p2
resize2fs ${LOOPDEV}p2
sync

echo "Filling the installation data into the image ..."
mkdir -p ${LOCTMP}/rootfs
mount ${LOOPDEV}p2 ${LOCTMP}/rootfs

BPI_HOME=${LOCTMP}/rootfs/root/bpi-r2
mkdir -p ${BPI_HOME}
mkdir -p ${BPI_HOME}/mount_points/BPI-ROOT
mkdir -p ${BPI_HOME}/mount_points/BPI-BOOT

echo "Copying target image to installation image ..."
cp -rp ${FLUENCEIMAGELOC} ${BPI_HOME}

echo "Copying scripts to image ..."
cp -rp ${SCRIPTDIR}/* ${BPI_HOME}
ln -s ${BPI_HOME}/next_boot_from.sh ${LOCTMP}/rootfs/usr/local/bin/fluence_next_boot_from.sh
egrep -v "FOS\=" ${LOCTMP}/rootfs/usr/local/share/fluence/fluence_vars.sh > ${LOCTMP}/rootfs/usr/local/share/fluence/.tmp.fluence_vars.sh
echo "FOS=${FLUENCEIMAGE}" >> ${LOCTMP}/rootfs/usr/local/share/fluence/.tmp.fluence_vars.sh
cp -rp ${LOCTMP}/rootfs/usr/local/share/fluence/.tmp.fluence_vars.sh ${LOCTMP}/rootfs/usr/local/share/fluence/fluence_vars.sh
rm -f ${LOCTMP}/rootfs/usr/local/share/fluence/.tmp.fluence_vars.sh

echo "Creating symlinks on image ..."
cp -rp ${BPI_HOME}/fluence_autoinstall.service ${LOCTMP}/rootfs/etc/systemd/system/fluence_autoinstall.service
cd ${LOCTMP}/rootfs/etc/systemd/system/multi-user.target.wants/
ln -s ../fluence_autoinstall.service fluence_autoinstall.service

echo "Compatibility hack for bpi-copy on image"
FILE=${LOCTMP}/rootfs/usr/local/bin/bpi-copy2
if [ -f "$FILE" ]; then
    echo "$FILE exists."
else
    cat ${LOCTMP}/rootfs/usr/local/bin/bpi-copy | sed 's/seek=8//g' > ${LOCTMP}/rootfs/usr/local/bin/bpi-copy2
    chmod 755 ${LOCTMP}/rootfs/usr/local/bin/bpi-copy2
fi

cd # go out of folder else we cannot unmount

sync
umount ${LOCTMP}/rootfs
losetup -d ${LOOPDEV}

echo "Writing the image to the SD card/file ${SDDEVICELOC} ..."
dd if=${LOCTMP}/${FLUENCEIMAGE} of=${SDDEVICELOC} bs=10M status=progress
sync

echo "SUCCESS ! Plug the sd card in the BPi and eMMC installation will start automatically. Have a nice day."
