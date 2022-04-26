#!/bin/bash

FILE=$1
PW=$2
PWorig=${PW}
while read line;
do
	echo "Machine: $line"
	PW=${PWorig}
	sshpass -p "${PW}" ssh -n root@${line} 'ls >/dev/null'
	if [ "$?" -ne 0 ]; then
		PW=root
	fi
	sshpass -p "${PW}" ssh -n root@${line} 'lldpcli sh nei | egrep "Interface|ChassisID|PortDescr" | egrep "enst"; lldpcli sh nei | egrep "Interface|ChassisID|PortDescr" | egrep "FastEthernet" | tr -d "\n"; echo "Count:"; lldpcli sh nei | egrep "Interface|ChassisID|PortDescr" | egrep "^Interface" | wc -l'

done < <(cat ${FILE} | egrep "^[0-9]" | egrep -v "Core Telco Rack Controller" | cut -d " " -f 1)
