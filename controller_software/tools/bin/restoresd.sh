#/bin/bash
LOCUSER=$USER
LOCHOME=$HOME
TIMENOW=$(date +%Y%m%d%H%M)
BACKUPFOLDER=$LOCHOME/bpi-r2/backup
DEVICE="/dev/sdb"

if [ -z "$1" ]; then
  echo "Backup file name missing"
  echo "Usage: restoresd.sh fluenceimagefile [/dev/sdb]"
  exit 1
fi

if [[ -n "$2" ]] && [[ "$2" == /dev/* ]]; then
  DEVICE=$2
fi

#unmount SD card
sudo umount ${DEVICE}1
sudo umount ${DEVICE}2

FLUENCEIMAGE=$BACKUPFOLDER/$1

if [ -s "$FLUENCEIMAGE" ]; then
  echo "Restore file $FLUENCEIMAGE"

  if [[ "${FLUENCEIMAGE#*.}" == "img.gz" ]]; then
    #uncompress the file to stdout
    gunzip -dc $FLUENCEIMAGE | pv | sudo dd of=$DEVICE bs=10M status=noxfer && sudo sync
  elif [[ "${FLUENCEIMAGE#*.}" == "img" ]]; then 
    sudo dd status=progress if=$FLUENCEIMAGE of=$DEVICE bs=10M && sudo sync
  else 
    echo "Not valid extension"
  fi


else
  echo "not exist $1" 
fi
