#!/bin/bash

# . /usr/local/share/fluence/fluence_vars.sh

#rm -fv $INIT_FILE
#cp -p $INIT_NETPLAN_FILE /etc/netplan/network.yaml

# Remove iproutes all chains
#iptables -F


localhelp () {
  echo "Usage:"
  echo "  fluence_octe-power-cycling.sh [hard]"
}

UPShardresetOld () {
  systemctl stop fluence-modbus
# avoid press ENTER
expect <<END
  spawn controllino_modbus.py force write controllino co 0 1
  expect "Press Enter to continue." { send "\r" }
  expect eof
END
# avoid press ENTER
expect <<END
  spawn controllino_modbus.py force write virtualslave hr 61 1
  expect "Press Enter to continue." { send "\r" }
  expect eof
END
}


UPShardreset () {
/usr/local/bin/fluence_octe-power-cycling.py
}

ParseAndExecuteCommandLineData () {
  for var in "$@"
  do
    # echo "$var"
    if [[ "${var}" == "help" || "${var}" == "--help" || "${var}" == "-h" ]]; then
      localhelp
      exit 0
    fi
    if [[  "${var}" == "hard" ]]; then
      UPShardreset
	  exit 0
    fi
    
    minOneVarFound=1  # one option found
  done

  if [ -z "${minOneVarFound}" ] ; then #default hard power cycling
    UPShardreset
  fi
}


ParseAndExecuteCommandLineData "$@"

# echo "If you see this message, the power cycling is not working or the controller is on separate aux power"
