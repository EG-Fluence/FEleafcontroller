#!/bin/sh
#
# Convenience script for create_installation_sdcard.sh

SCRIPTNAME=`which $0`
SCRIPTFILENAME=${SCRIPTNAME##*/}
SCRIPTDIRECTORY=${SCRIPTNAME%/*}

echo "I need your password in order to execute some functions as sudo, i.e. writing to /dev/sdX"
sudo ls 2>&1 >/dev/null

localhelp () {
  echo "Usage: conv_create_installation_sd.sh <fluenceimagefile.gz> <Script directory> <SD card Device>"
  echo "       Full qualified paths are expected for both parameters. Image file must be gzipped."
  echo "Example: conv_create_installation_sdcard.sh ~/Downloads/fluence.img.gz ~/bpi-r2/controller_software/LeafController/BananaPiR2/images  /dev/sdX|fileName"
  echo "         conv_create_installation_sdcard.sh ~/bpi-r2/backup/fluence_202012221556.img.gz ~/bpi-r2/controller_software/LeafController/BananaPiR2/images /dev/sdX|fileName"
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

cd ${SCRIPTDIRECTORY}

sudo ./create_installation_sdcard.sh $1 $2 $3
