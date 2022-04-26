#!/bin/bash
mac_change () {
    echo Changing device $1
    ip link set dev $1 down
    macchanger -e $1
    ip link set dev $1 up
}

conditional_mac_change () {
    local changed=0
    declare -a MACArray=("00:00:00:00:00:00" "00:00:00:00:00:01" "02:02:02:02:02:02" "02:03:03:03:03:03" )
    local MAC=$(ip addr show $1 | grep ether | cut -d " " -f6)
    if [ -n "${MAC}" ] ; then
      echo "Device $1 MAC Address: $MAC"
      for val in ${MACArray[@]}; do
        if [[ ${MAC} == ${val} ]] || [[ -n "$2" ]]; then
          mac_change $1
        else
          changed=1
        fi
      done
      if [[ "$changed"==1 ]]; then
        echo "Already changed"
      fi
    fi
}


if [[ -n "$1" ]] && [[ "$1" == "force" ]]; then
  echo "Force MAC change option for all interfaces"
  force=1
fi

for iface in eth{0..4} wan lan{0..3} br{0..1}; do
   if [ -z "${force}" ] ; then
     conditional_mac_change $iface
   else
     conditional_mac_change $iface 1
   fi
done

