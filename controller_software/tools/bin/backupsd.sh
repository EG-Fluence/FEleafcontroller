#/bin/bash
LOCUSER=$USER
LOCHOME=$HOME
TIMENOW=$(date +%Y%m%d%H%M)
DISKEND=$(sudo fdisk -lu /dev/sdb | awk '/\/dev\/sdb2/ {print $3}')
#DISKEND=$(sudo fdisk -lu /dev/sdb | awk '/\/dev\/sdb2/ {print $3}' | tr -d '\n\r')

COUNTEND=$(($DISKEND + 1))
#echo ${DISKEND} " diskend " ${COUNTEND} " countend"

INDEVICE=/dev/sdb

#unmount SD card
sudo umount /dev/sdb1
sudo umount /dev/sdb2

for var in "$@"
do
  echo "$var"
  if [[ "${var}" == "master" ]]; then
    MasterCopy=1
    echo "Master SD image"
  fi
  if [[ "${var}" == "pack" ]]; then
    PackImage=1
    echo "SD image will be compressed"
  fi
  if [[ "${var}" == /dev/* ]]; then
    INDEVICE="${var}"
    echo "Input Device: ${INDEVICE}"
  fi
done


if [[ "$MasterCopy" == "1" ]]; then
  OUTDEVICE="$LOCHOME/bpi-r2/backup/fluence_${TIMENOW}_master.img"
else
  OUTDEVICE="$LOCHOME/bpi-r2/backup/fluence_${TIMENOW}.img" 
fi

#sudo dd status=progress if=$INDEVICE of=$OUTDEVICE bs=512 count=$COUNTEND 

if [[ "$PackImage" == "1" ]]; then
  #echo "Compress $OUTDEVICE"
  #gzip $OUTDEVICE | pv > "${OUTDEVICE}.gz"
  sudo dd status=progress bs=512 count=$COUNTEND if=$INDEVICE | gzip > "${OUTDEVICE}.gz"
else
  sudo dd status=progress bs=512 count=$COUNTEND if=$INDEVICE of=$OUTDEVICE 
fi

sudo sync



