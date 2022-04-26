#!/bin/bash

LOG () {
  echo "$1"
}

LOG "Environment: FLUENCE_IP:$FLUENCE_IP; FLUENCE_HOSTNAME:$FLUENCE_HOSTNAME; FLUENCE_TESTING:$FLUENCE_TESTING; FLUENCE_RECONFIGURE:$FLUENCE_RECONFIGURE"

currenthostname=`cat /etc/hostname | tr -d '\n'`
LOG "Setting hostname: ${FLUENCE_HOSTNAME} . Current hostname: ${currenthostname}"

splitted_hostname=(${FLUENCE_HOSTNAME//./ })
splitted_ip=(${FLUENCE_IP//\// })

echo ${splitted_hostname[0]} > /etc/hostname

if [ ${#splitted_hostname[@]} -gt 1 ]; then
	sed -i "s/.*${currenthostname}.*/${splitted_ip[0]}   ${splitted_hostname[0]} ${FLUENCE_HOSTNAME}/g" /etc/hosts
else
	sed -i "s/.*${currenthostname}.*/${splitted_ip[0]}   ${splitted_hostname[0]}/g" /etc/hosts
fi

