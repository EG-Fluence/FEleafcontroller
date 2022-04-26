#!/bin/bash

STOP=$1

if [ "${STOP}" == "stop" ]; then
	echo "Remove cronjob"
	crontab -l | egrep -v "fluence_selfconfiguration" | crontab -
	exit
fi

# if crontab is empty -> insert it
crontab -l
if [ "$?" -ne 0 ]; then
	echo "*/5 * * * * export PATH=/usr/sbin:$PATH; /usr/local/bin/fluence_selfconfiguration.sh" | crontab -
else
	# if crontab entry not present -> insert it
	crontab -l | egrep "fluence_selfconfiguration"
	if [ "$?" -ne 0 ]; then
	        #BUG this will remove all entries from crontab and add only one
		#echo "*/5 * * * * export PATH=/usr/sbin:$PATH; /usr/local/bin/fluence_selfconfiguration.sh" | crontab -
		crontab -l > cron.txt
		echo "*/5 * * * * export PATH=/usr/sbin:$PATH; /usr/local/bin/fluence_selfconfiguration.sh" >> cron.txt
		crontab cron.txt
	fi
fi


# shell script is already running
ps -ef | egrep [l]eaf_selfconfiguration
if [ "$?" -eq 0 ]; then
	exit 0
fi

cd /home/fos/SelfConfiguration
./leaf_selfconfiguration.py &
