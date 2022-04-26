#!/bin/bash

FILE=$1
PWuser=$2
PWroot=root
PW=${PWuser}

while read line;
do
	PW=${PWuser}
	echo "Machine: $line"
	sshpass -p "${PW}" ssh -n root@${line} 'ls'
	if [ "$?" -ne 0 ]; then
		PW=root
	fi	
	machine=`sshpass -p "${PW}" ssh -n root@${line} 'ifconfig | grep wan'`
	if [ "$?" -ne 0 ]; then
		machine=`sshpass -p "${PW}" ssh -n root@${line} 'ifconfig | grep wan'`
	fi
	echo ${machine} | egrep "^wan"
	if [ "$?" -eq 0 ]; then
		echo "Running on BananaPi"
		#sshpass -p "${PW}" scp -rp ../../LeafController/BananaPiR2/root/etc/default/lldpd root@${line}:/etc/default/lldpd
	else
		echo "Running on Modberry"
		#sshpass -p "${PW}" scp -rp ../../LeafController/Modberry500M3/root/etc/default/lldpd root@${line}:/etc/default/lldpd
	fi
	# sshpass -p "${PW}" ssh -n root@${line} 'systemctl stop lldpd && systemctl start lldpd'
	#sshpass -p "${PW}" scp -rp ../../LeafController/BananaPiR2/root/usr/local/bin/fluence_selfconfiguration.sh root@${line}:/usr/local/bin
	#sshpass -p "${PW}" scp -rp ../../LeafController/SelfConfiguration/leaf_selfconfiguration.py  root@${line}:/home/fos/SelfConfiguration
	#sshpass -p "${PW}" scp -rp ../../LeafController/SelfConfiguration/leaf_selfdiscovery.py  root@${line}:/home/fos/SelfConfiguration
	#sshpass -p "${PW}" ssh -n root@${line} 'reboot'
	sshpass -p "${PW}" ssh -n root@${line} 'hostname'
	#sshpass -p "${PW}" ssh -n root@${line} 'lldpcli sh nei | grep PortDes'
done < <(cat ${FILE} | egrep "^[0-9]" | egrep -v "Core Telco Rack Controller" | cut -d " " -f 1)
