#!/bin/bash

parted /dev/mmcblk0 resizepart 2 100%
resize2fs /dev/mmcblk0p2
sync
