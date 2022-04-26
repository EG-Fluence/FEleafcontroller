#!/bin/bash
. /usr/local/share/fluence/fluence_vars.sh

#moved to fluence_vars.sh
#this is wrong, IP must be for controlino not bridge 1
#CONTROLLINOIP=$(ip -f inet -o addr show br1 | awk 'NR==1{print $4}' |cut -d/ -f 1)

LEAFIP=$(ip -f inet -o addr show br0 | awk 'NR==1{print $4}' |cut -d/ -f 1)


if [[ -z "$LEAFIP" ]]; then
  echo "LEAFIP is empty"
  exit 1
fi

if [[ -z "$CONTROLLINOIP" ]]; then
  echo "CONTROLLINOIP is empty"
  exit 1
fi

# enable IP forwarding
echo 1 > /proc/sys/net/ipv4/ip_forward


localhelp () {
  echo "Usage:"
  echo "  fluence_iptables.sh stop"
  echo "    Delete IP forwarding table"
  echo "  fluence_iptables.sh [start]"
  echo "    Delete IP forwarding table and create new one"
  echo "  fluence_iptables.sh print"
  echo "    Print IP forwarding table"
  echo "  fluence_iptables.sh help"
  echo "    Print help"
}

stopIProuting () {
  iptables -F
  iptables -t nat -F
}

startIProuting () {
  iptables -t nat -A PREROUTING -p tcp --dport 8080 -j DNAT --to-destination $CONTROLLINOIP:80
  iptables -t nat -A POSTROUTING -p tcp -d $CONTROLLINOIP --dport 80 -j SNAT --to-source $LEAFIP
  iptables -t nat -A PREROUTING -p tcp --dport 1602 -j DNAT --to-destination $CONTROLLINOIP:502
  iptables -t nat -A POSTROUTING -p tcp -d $CONTROLLINOIP --dport 502 -j SNAT --to-source $LEAFIP
}

printIProuting () {
 iptables -t nat -L -n -v
}

for var in "$@"
do
  echo "$var"
  if [ "${var}" == "start" ]; then
    stopIProuting
    startIProuting
    exit 0
  fi
  if [ "${var}" == "stop" ]; then
    stopIProuting
    exit 0
  fi
  if [ "${var}" == "print" ]; then
    printIProuting
    exit 0
  fi
  if [ "${var}" == "help" ]; then
    localhelp
    exit 0
  fi
  minOneVarFound=1
done

#if [ -z "${minOneVarFound}" ] ; then
#  localhelp 
#  exit 1
# fi

stopIProuting
# printIProuting
startIProuting
printIProuting

#iptables -F
#iptables -t nat -F
#iptables -t nat -L -n -v

#iptables -t nat -A PREROUTING -p tcp --dport 8080 -j DNAT --to-destination $CONTROLLINOIP:80
#iptables -t nat -A POSTROUTING -p tcp -d $CONTROLLINOIP --dport 80 -j SNAT --to-source $LEAFIP
#iptables -t nat -A PREROUTING -p tcp --dport 1602 -j DNAT --to-destination $CONTROLLINOIP:502
#iptables -t nat -A POSTROUTING -p tcp -d $CONTROLLINOIP --dport 502 -j SNAT --to-source $LEAFIP

#iptables -t nat -L -n -v
