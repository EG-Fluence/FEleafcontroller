#!/bin/bash

#PARTITION=mmcblk0
#TARGET_SIZE=`expr 15 \* 1024 \* 1024 \* 1024`
#FDISK_REPORT=(`fdisk -l --bytes | grep ${PARTITION}p2`)
#START=${FDISK_REPORT[1]}
#END=${FDISK_REPORT[2]}
#NUM_SECTORS=${FDISK_REPORT[3]}
#ORIG_SIZE=${FDISK_REPORT[4]}
#SECTOR_SIZE=`expr $ORIG_SIZE / $NUM_SECTORS`
#NEW_END=`expr $START + \( $TARGET_SIZE / $SECTOR_SIZE \)`

parted /dev/mmcblk0 resizepart 2 15373
resize2fs /dev/mmcblk0p2
sync
