#!/bin/bash

FILE=$1
PW=$2

while read line;
do
	echo $line
	sshpass -p "${PW}" scp -rp SelfConfiguration root@${line}:~/
	if [ "$?" -ne 0 ]; then
		sshpass -p "root" scp -o "StrictHostKeyChecking no" -rp SelfConfiguration root@${line}:~/
	fi
done < <(cat ${FILE} | egrep "^[0-9]" | egrep -v "Core Telco Rack Controller" | cut -d " " -f 1)
