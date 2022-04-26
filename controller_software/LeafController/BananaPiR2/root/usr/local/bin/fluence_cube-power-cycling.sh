#!/bin/bash

# . /usr/local/share/fluence/fluence_vars.sh

#rm -fv $INIT_FILE
#cp -p $INIT_NETPLAN_FILE /etc/netplan/network.yaml

# Remove iproutes all chains
#iptables -F


localhelp () {
  echo "Usage:"
  echo "  fluence_cube-power-cycling.sh [soft]"
  echo "    Software reset (power cycling) of EATON UPS, with shutdown of the controller. With time delay of 15 seconds for switch off"
  echo "  fluence_cube-power-cycling.sh hard"
  echo "    Immediately hard reset (power cycling) of EATON UPS, without shutdown of the controller"
  echo "  fluence_cube-power-cycling.sh on"
  echo "    Immediately All Load On of EATON UPS"  
  echo "  fluence_cube-power-cycling.sh off"
  echo "    Immediately All Load Off of EATON UPS"    
  echo "  fluence_cube-power-cycling.sh outlet1on"
  echo "    Immediately Outlet 1 Load On of EATON UPS"  
  echo "  fluence_cube-power-cycling.sh outlet1off"
  echo "    Immediately Outlet 1 Load Off of EATON UPS"     
  echo "  fluence_cube-power-cycling.sh outlet2on"
  echo "    Immediately Outlet 2 Load On of EATON UPS"  
  echo "  fluence_cube-power-cycling.sh outlet2off"
  echo "    Immediately Outlet 2 Load Off of EATON UPS"      
}

UPSrestart () {
  #force restart driver
  upsdrvctl stop
  upsdrvctl start
  if [[ $? -ne 0 ]]; then
    echo "Unable to restart UPS driver"
    exit 1
  fi

  #give some time for ups driver
  sleep 5
}


UPSsoftreset () {
  UPSrestart

  upscmd -u upsuser -p ups eaton5p load.off.delay 15
  if [[ $? -ne 0 ]]; then
    echo "Error executing upscmd load.off"
     exit 1
  fi

  upscmd -u upsuser -p ups eaton5p load.on.delay 25
  if [[ $? -ne 0 ]]; then
    echo "Error executing upscmd load.on"
     exit 1
  fi

  shutdown -h now
}

UPSAllLoadOn () {
  UPSrestart
  upscmd -u upsuser -p ups eaton5p load.on
  if [[ $? -ne 0 ]]; then
    echo "Error executing upscmd load.on"
     exit 1
  fi    
}

UPSAllLoadOff () {
  UPSrestart
  upscmd -u upsuser -p ups eaton5p load.off
  if [[ $? -ne 0 ]]; then
    echo "Error executing upscmd load.off"
     exit 1
  fi  
}

UPSOutlet1On () {
  UPSrestart
  upscmd -u upsuser -p ups eaton5p outlet.1.load.on
  if [[ $? -ne 0 ]]; then
    echo "Error executing upscmd outlet.1.load.on"
     exit 1
  fi  
}

UPSOutlet1Off () {
  UPSrestart
  upscmd -u upsuser -p ups eaton5p outlet.1.load.off
  if [[ $? -ne 0 ]]; then
    echo "Error executing upscmd outlet.1.load.off"
     exit 1
  fi  
}

UPSOutlet2On () {
  UPSrestart
  upscmd -u upsuser -p ups eaton5p outlet.2.load.on
  if [[ $? -ne 0 ]]; then
    echo "Error executing upscmd outlet.2.load.on"
     exit 1
  fi  
}

UPSOutlet2Off () {
  UPSrestart
  upscmd -u upsuser -p ups eaton5p outlet.2.load.off
  if [[ $? -ne 0 ]]; then
    echo "Error executing upscmd outlet.2.load.off"
     exit 1
  fi  
}



UPShardreset () {
  systemctl stop fluence-modbus
# avoid press ENTER
expect <<END
  spawn controllino_modbus.py force write controllino co 0 1
  expect "Press Enter to continue." { send "\r" }
  expect eof
END
}

ParseAndExecuteCommandLineData () {
  for var in "$@"
  do
    # echo "$var"
    if [[ "${var}" == "help" || "${var}" == "--help" || "${var}" == "-h" ]]; then
      localhelp
      exit 0
    fi
    if [[  "${var}" == "soft" ]]; then
      UPSsoftreset
    fi
    if [[  "${var}" == "hard" ]]; then
      UPShardreset
    fi
    if [[  "${var}" == "on" ]]; then
      UPSAllLoadOn
    fi  
    if [[  "${var}" == "off" ]]; then
      UPSAllLoadOff
    fi      
    if [[  "${var}" == "outlet1on" ]]; then
      UPSOutlet1On
    fi  
    if [[  "${var}" == "outlet1off" ]]; then
      UPSOutlet1Off
    fi      
    if [[  "${var}" == "outlet2on" ]]; then
      UPSOutlet2On
    fi  
    if [[  "${var}" == "outlet2off" ]]; then
      UPSOutlet2Off
    fi       
  
    
    minOneVarFound=1  # IP, password, copy, pack or cpack
  done

  if [ -z "${minOneVarFound}" ] ; then #default soft power cycling
    UPSsoftreset
  fi


}


ParseAndExecuteCommandLineData "$@"

# echo "If you see this message, the power cycling is not working or the controller is on separate aux power"
