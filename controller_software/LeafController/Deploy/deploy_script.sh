#/bin/bash
####################################################################################
#
# Deploy Script for execution on remote/target computer
# @author: Boris Kajganic (boris.kajganic@fluenceenergy.com)
# @Version: 2021-09-22
# NOTE: Following packets must be installed:
#    ----
# You will need write permissions in the directory where you executing this script
#
####################################################################################

#Declare a string array of known Controllers
ControllerArray=("BananaPiR2" "BananaPiR64" "Modberry500M3" "ESPRESSObin-Ultra")
DEPRUNFILE=/root/deployrunning
TIMENOW=$(date +%Y%m%d%H%M)
INSTERROR=0

printControllers() {
  for val1 in ${ControllerArray[*]}; do
    echo "    ${val1}"
  done
}

checkStartConditions() {
  # CONTROLLER type must be defined
  CONTROLLER=$1
  if [[ -z "${CONTROLLER}" ]] ; then
    echo "  CONTROLLER variable is not defined, available values:"
    printControllers
    exit 1
  fi

  for val1 in ${ControllerArray[*]}; do
    if [[ "${val1}" == "${CONTROLLER}" ]] ; then
      ControllerFound=1
    fi
  done

  if [[ -z "${ControllerFound}" ]] ; then
    echo "  CONTROLLER ${CONTROLLER} is not in ControllerArray"
    printControllers
    exit 1
  fi

  # Check if the command is running on correct controller. Test kernel
  FILE=/boot/bananapi/bpi-r2/linux/uImage
  if [[ "${CONTROLLER}" == "BananaPiR2" ]] && [[ ! -f "$FILE" ]]; then
    echo "  CONTROLLER is not BananaPiR2"
    exit 1
  fi
  FILE=/boot/bananapi/bpi-r64/linux-5.4/uImage
  if [[ "${CONTROLLER}" == "BananaPiR64" ]] && [[ ! -f "$FILE" ]]; then
    echo "  CONTROLLER is not BananaPiR64"
    exit 1
  fi  
  FILE=/boot/zImage
  if [[ "${CONTROLLER}" == "Modberry500M3" ]] && [[ ! -f "$FILE" ]]; then
    echo "  CONTROLLER is not Modberry500M3"
    exit 1
  fi
  FILE=/boot/Image
  if [[ "${CONTROLLER}" == "ESPRESSObin-Ultra" ]] && [[ ! -f "$FILE" ]]; then
    echo "  CONTROLLER is not ESPRESSObin-Ultra"
    exit 1
  fi

  FOSVERSION="${CONTROLLER}_fos.txt"
  FLUENCEFILE="/usr/local/share/fluence/fluence_vars.sh"
  if [[ ! -f "$FOSVERSION" ]]; then
    echo "${FOSVERSION} is missing"
    exit 1
  elif [[ -f "$FLUENCEFILE" ]]; then
    difflines=$(egrep 'FOS\=' ${FLUENCEFILE} > /tmp/fos.txt; diff -w ${FOSVERSION} /tmp/fos.txt | wc -l)
    rm /tmp/fos.txt
    #echo "Difflines $difflines"
    if [[ "$difflines" == 0 ]]; then 
      echo "${CONTROLLER} version inside '${FLUENCEFILE}' is the same. Update is not required."
      exit 0
    fi
  fi

  # Deploy in progress
  if [[ -f "$DEPRUNFILE" ]]; then
    echo "  File ${DEPRUNFILE} detected, deploy process already runs."
    echo "  Remove the file manualy if you are sure the process is stopped and start the installation once more."
    echo "  Example:"
    echo "    rm ${DEPRUNFILE}"
    exit 1
  else
    touch $DEPRUNFILE
  fi
  

}

installPackage() {
  NoPrompt=False
  CriticalPackage=False
  for var in "$@"
  do
    #echo "$var"
    shift # remove argument from arg list
    if [[ "${var}" == "--noPrompt" ]]; then
      NoPrompt=True
    elif [[ "${var}" == "--critical" ]]; then
      CriticalPackage=True
    else
      set -- "$@" "$var" # add the argument back as the last if is not one of above
    fi
  done

  #echo "Command line arguments:"
  #printf '  %s\n' "$@"
  
  if [[ "$NoPrompt" == "True" ]] ; then
    #echo "install silent"
	  yes '' | apt-get -y -o Dpkg::Options::="--force-confdef" -o Dpkg::Options::="--force-confold" install $@
  else
    #echo "install normal"
    apt-get -y install $@
  fi
  
  if [[ $? -ne 0 ]]; then
    echo "Error: Unable to install $@"
    INSTERROR=1
    if [[ "$CriticalPackage" == "True" ]] ; then
      cleanUp
      exit 1
    fi
  fi
}

installPythonPackage() {
  pip3 install $@
  if [[ $? -ne 0 ]]; then
    echo "Error: Unable to install $@"
    INSTERROR=1
  fi
}

installPython() {
  echo "*** Python ***"
  systemctl stop fluence-controller
  systemctl stop fluence-modbus
  
  if [[ "${CONTROLLER}" == "Modberry500M3" || "${CONTROLLER}" == "ESPRESSObin-Ultra" ]] ; then  
    #apt-get -y install libatlas-base-dev
    installPackage libatlas-base-dev
  fi
  if [[ "${CONTROLLER}" == "Modberry500M3" ]] ; then
    #pip3 install cython
    installPythonPackage numpy
    installPythonPackage twisted
    installPythonPackage pandas
    installPythonPackage pymodbus
  elif [[ "${CONTROLLER}" == "ESPRESSObin-Ultra" ]] ; then
    #apt-get -y install python3 python3-pip
    installPackage --critical python3 python3-pip
    
    #Version 1, need more then 2 hours only for Python
    #installPythonPackage cython
    #installPythonPackage numpy
    #installPythonPackage twisted
    #installPythonPackage pandas
    #installPythonPackage pymodbus
    
    #Version 2
    installPackage python3-numpy
    installPackage python3-twisted
    installPythonPackage pandas
    installPythonPackage pymodbus
  fi

  # Modbus Server (controllino_modbus.py) on Cube/Telco rack controller additional packages
  #pip3 install -U cython
  #pip3 install -U numpy
  #pip3 install -U twisted
  #pip3 install -U pandas
  #pip3 install -U pymodbus
  #apt-get -y install python3-numpy
  #apt-get -y install python3-twisted
  #apt-get -y install python3-pandas
  #apt-get -y install python3-pymodbus
}

installUtilities() {
  echo "*** Utilities ***"
  #apt-get -y install nano
  #apt-get -y install vim
  #apt-get -y install sshpass
  installPackage nano
  installPackage vim
  installPackage sshpass
  
  #xmllint 
  #apt-get -y install libxml2-utils
  #apt-get -y install htop
  installPackage libxml2-utils
  installPackage htop
  installPackage iotop
  installPackage ncdu
  installPackage wget
  installPackage dfc
  
  # required for power cycling
  #apt-get -y install expect
  installPackage expect
}

installNetworkUtilities() {
  echo "*** Network Utilities ***"
  #On Ubuntu systemd-timesyncd is responsible synchronosation, Not need for ntpdate
  #apt-get -y install ntpdate
  #Conflict between systemd-timesyncd and ntpd on Modberry
  systemctl stop ntp
  systemctl disable ntp
  apt-get -y purge ntp

  # Network Utilities
  #apt-get -y install dnsutils
  #apt-get -y install macchanger
  #apt-get -y install fping

  installPackage dnsutils
  installPackage macchanger
  installPackage fping
  installPackage telnet
  installPackage mtr
  installPackage iftop
  installPackage tcpdump
  
  installPackage tcptraceroute
  wget http://www.vdberg.org/~richard/tcpping -O /usr/bin/tcping
  chmod 755 /usr/bin/tcping

}

installLogs() {
  echo "  Configure Logs"
  #the configuration files are already copied
  # /etc/rsyslog.conf
  # /etc/logrotate.d/fos

  # if crontab is empty -> insert it
  crontab -l
  if [ "$?" -ne 0 ]; then
    echo "* * * * * /usr/sbin/logrotate /etc/logrotate.d/fos" | crontab -
  else
    # if crontab entry not present -> insert it
    crontab -l | egrep "fos"
    if [ "$?" -ne 0 ]; then
      crontab -l > cron.txt
      #echo "* * * * * /usr/sbin/logrotate /etc/logrotate.d/fos" | crontab -	
      echo "* * * * * /usr/sbin/logrotate /etc/logrotate.d/fos" >> cron.txt 
      crontab cron.txt
    fi
  fi
  
  touch /var/log/fos.log
  chmod 0664 /var/log/fos.log
  chown sdu:users /var/log/fos.log

  #Add users group to syslog
  usermod -a -G users syslog
  
  systemctl restart rsyslog
}

renameLinkOrFile() {
  # $f is local variable
  local f="$1"

  if [[ -L ${f} ]] ; then
    mv ${f} ${f}_${TIMENOW}
  elif [[ -f ${f} ]] ; then
    mv ${f} ${f}.${TIMENOW}
  fi
}

addGroup() {
  if [ $(getent group $2) ]; then
    echo "Group $2 exists."
  else
    #echo "Group does not exist."
    groupadd -g $1 $2
  fi
}

addGroupsAndUsers() {
  #groupadd -g 27 sudo

  # Modberry500M3 has already 1000 and 1001
  if [[ "${CONTROLLER}" == "Modberry500M3" ]] ; then
    echo "  Modberry500M3 groups"
    #groupadd -g 2000 admin
    #groupadd -g 2001 fos
    addGroup 2000 admin
    addGroup 2001 fos
  else
    #groupadd -g 1000 admin
    #groupadd -g 1001 fos
    addGroup 1000 admin
    addGroup 1001 fos
  fi
  
  #groupadd -g 14657 sdu
  addGroup 14657 sdu
# Modberry500M3 has already 1000 and 1001
#groupadd -g 1000 admin
#if [ ! "$?" = "0" ]; then
#  echo "Cannot add group 1000 admin, try without 1000"
#  groupadd admin
#fi
#groupadd -g 1001 fos
#if [ ! "$?" = "0" ]; then
#  echo "Cannot add group 1001 fos, try without 1001"
#  groupadd fos
#fi

  if id "fos" >/dev/null 2>&1; then
    echo "user fos exists"
  else
    useradd -g fos   -G sudo,admin -m -s /bin/bash -u 10000 fos -c "Fluence OS user"
    
passwd fos <<EOF
fos_2021!
fos_2021!
EOF
  fi
  
  if id "sdu" >/dev/null 2>&1; then
    echo "user sdu exists"
  else
    useradd -g 14657 -m -s /bin/bash -u 14657 sdu
    
passwd sdu <<EOF
sdu_2021!
sdu_2021!
EOF
  fi

  if id "ijefferson.admin" >/dev/null 2>&1; then
    echo "user ijefferson.admin exists"
  else
    useradd -g admin -G sudo,fos   -m -s /bin/bash -u 10010 ijefferson.admin -c "Isaiah Jefferson <US ARL TEC EM>"
    
passwd ijefferson.admin <<EOF
ijefferson_2021!
ijefferson_2021!
EOF
  fi

  if id "smori.admin" >/dev/null 2>&1; then
    echo "user smori.admin exists"
  else
    useradd -g admin -G sudo,fos   -m -s /bin/bash -u 10011 smori.admin -c "Shakti Mori <US ARL TEC EM>"
    
passwd smori.admin <<EOF
smori_2021!
smori_2021!
EOF
  fi
  
  if id "bkajganic.admin" >/dev/null 2>&1; then
    echo "user bkajganic.admin exists"
  else
    useradd -g admin -G sudo,fos   -m -s /bin/bash -u 10012 bkajganic.admin -c "Dr. Boris Kajganic <DE ERL TEC EM>"
    
passwd bkajganic.admin <<EOF
bkajganic_2021!
bkajganic_2021!
EOF
  fi

}

isNetworkAlive() {
  i=0
  while [[ $i -lt 5 ]]
  do
    #echo "Number: $i"
    ping -c1 www.google.com >/dev/null 2>&1
    if [ "$?" -eq 0 ]; then
      echo "Ping Google OK"
      break
    fi

    ping -c1 10.0.0.3 >/dev/null 2>&1
    if [ "$?" -eq 0 ]; then
      echo "Ping array OK"
      break
    fi

    ((i++))
    sleep 2
  done
}

stopSelfConfiguration() {
  echo "*** Stopping self-configuration ***"
  #prohibit self-configuration process
  touch /home/fos/SelfConfiguration/.installrunning
  #remove from crontab
  crontab -l | egrep -v "fluence_selfconfiguration" | crontab -
  until ! ps -ef | egrep [l]eaf_selfconfiguration >/dev/null 2>&1 ; 
  do 
    echo "  -- self-configuration is running --"
    sleep 10 
  done

  #flag=0
  #while [[ "flag -eq 0 ]]  #process is running
  #do
  #  ps -ef | egrep [l]eaf_selfconfiguration >/dev/null 2>&1 
  #  flag=${?}
  #  echo "*** self-configuration is running, wait to exit ***"
  #  sleep 5
  #done
  echo "*** self-configuration stopped ***"
}

cleanUp () {
  #Allow self-configuration process again
  rm -f /home/fos/SelfConfiguration/.installrunning

  MESSGAGE="System installed."
  if [[ "${CONTROLLER}" == "BananaPiR2" ]] ; then
    mount | egrep " / " | egrep mmcblk0p2 2>&1 >/dev/null
    if [ $? -eq 0 ]; then
      MESSGAGE="System installed. You're running on SD card now !"
    fi
    mount | egrep " / " | egrep mmcblk1p2 2>&1 >/dev/null
    if [ $? -eq 0 ]; then
      MESSGAGE="System installed. You're running on eMMC now !"
    fi
  fi

  #Update or remove FOS from fluence_vars.sh
  egrep -v "FOS\=" ${FLUENCEFILE} > /tmp/fluence_vars.sh
  if [[ "$INSTERROR" -eq 0 ]]; then
    cat ${FOSVERSION} >> /tmp/fluence_vars.sh
  else
    MESSGAGE="System is NOT correctly installed. Restart the installation process."
  fi
  #remove unnecessary lines
  grep --color=none "\S" /tmp/fluence_vars.sh > ${FLUENCEFILE}
  rm /tmp/fluence_vars.sh
    
  echo $MESSGAGE > /etc/motd
  echo $MESSGAGE > /etc/issue

  # do this only on first installation
  if [[ "$FirstInstallation" == "True" ]]; then
    echo "Reboot in 5 seconds"
    #reboot
    nohup bash -c 'sleep 5; reboot' & >/dev/null
  else
    echo "****** Starting Fluence services ******"
    #Services updated & installed, start the services
    FSERVICES=$(ls ~/${TRANSVERFOLDER}/etc/systemd/system)
    for i in $FSERVICES; do
        if [[ $i =~ ^fluence.*\.service$ ]] ; then
            #echo "I want to do something with the file $i"
            systemctl start $i
        fi
    done
  fi

  #Allow deploy process again
  rm -f $DEPRUNFILE
  if [[ "$INSTERROR" -eq 0 ]]; then
    echo "****** Installation finished ******"
  else
    echo "****** System is NOT correctly installed. Restart the installation process. ******"
  fi
}

##
## Main program ##
##
checkStartConditions $1

#CONTROLLERDATA="${CONTROLLER}_data.tgz"
TRANSVERFOLDER="${CONTROLLER}_transfer"


FirstInstallation=False
# If the network is already installed, br1 exists and ip is assigned to the interface
# br1 is used instead br0, because ESPRESSObin-Ultra comes with predefined br0 (bridge to wifi)
CONTROLLERIP=$(ip -f inet -o addr show br1 | awk 'NR==1{print $4}' |cut -d/ -f 1)
if [[ -z "$CONTROLLERIP" ]] ; then
  FirstInstallation=True
else # this will work because only configurec controller has br0 and br1
  #tray to get static ip from self-configuration
  CONTROLLERIP=$(ip -f inet -o addr show br0 | grep "scope global br0" | awk 'NR==1{print $4}')
fi


#Inform user not to make reboot
echo "System installation in progress ...  NO Reboot!!!!" > /etc/motd
echo "System installation in progress ...  NO Reboot!!!!" > /etc/issue 

stopSelfConfiguration

if [[ "$FirstInstallation" == "True" ]] && [[ "${CONTROLLER}" == "Modberry500M3" ]] ; then
  echo "  ${CONTROLLER} Resize eMMC Partitions"
  #raspi-config --expand-rootfs   request reboot
  
  #100% - Production
  #40%  - Min intsallation
  parted /dev/mmcblk0 resizepart 2 100%
  resize2fs /dev/mmcblk0p2
  sync

  # TODO test
  mount -o rw,remount /
  
  #second variant
  #raspi-config --expand-rootfs
fi

# not working with nohup bash, first installation for Modberry500M3 & ESPRESSObin-Ultra
# For Banana is allready set
#if [[ "$FirstInstallation" == "False" ]] ; then
#  echo "*** Language settings ***"
#  export LANG=en_US.UTF-8
#  export LANGUAGE=
#  export LC_CTYPE="en_US.UTF-8"
#  export LC_NUMERIC="en_US.UTF-8"
#  export LC_TIME="en_US.UTF-8"
#  export LC_COLLATE="en_US.UTF-8"
#  export LC_MONETARY="en_US.UTF-8"
#  export LC_MESSAGES="en_US.UTF-8"
#  export LC_PAPER="en_US.UTF-8"
#  export LC_NAME="en_US.UTF-8"
#  export LC_ADDRESS="en_US.UTF-8"
#  export LC_TELEPHONE="en_US.UTF-8"
#  export LC_MEASUREMENT="en_US.UTF-8"
#  export LC_IDENTIFICATION="en_US.UTF-8"
#  export LC_ALL=
#  /usr/sbin/locale-gen
#  #locale -a
#fi

# Users & Groups
echo "*** Users & Groups ***"
addGroupsAndUsers

# change owner of the folders
chown -R root:root ~/${TRANSVERFOLDER}/etc
#chown -R root:nut  ~/${TRANSVERFOLDER}/etc/nut
chown -R root:root ~/${TRANSVERFOLDER}/usr
chown -R root:root ~/${TRANSVERFOLDER}/Controllino
chown -R fos:fos   ~/${TRANSVERFOLDER}/SelfConfiguration

# Update & installation
echo "****** Update & installation ******"

echo "*** Network settings ***"
#store orig. files
renameLinkOrFile "/etc/resolv.conf"
renameLinkOrFile "/etc/network/interfaces"
renameLinkOrFile "/etc/netplan/network.yaml"
renameLinkOrFile "${FLUENCEFILE}"

# Nedded to setup correct nameserver and proxy if is used
# nameserver
cp -p ~/${TRANSVERFOLDER}/etc/resolv.conf /etc/.
# proxy server
FILE=~/${TRANSVERFOLDER}/etc/profile.d/proxy.sh
if [[ -f "$FILE" ]]; then
  cp -p ~/${TRANSVERFOLDER}/etc/profile.d/proxy.sh /etc/profile.d/.
  source /etc/profile
fi

#use default answer to all install
export DEBIAN_FRONTEND=noninteractive
#apt-get update
apt-get update --allow-releaseinfo-change
if [[ $? -ne 0 ]]; then
  echo "Error: Unable to update packages list"
  INSTERROR=1
  cleanUp
  exit 1
fi

# Never, but never call upgrade on Modberry500M3. You've been warned
if [[ "${CONTROLLER}" != "Modberry500M3" ]] ; then
  echo "Enable upgrade if is required"
  #apt-get -y upgrade
fi

installUtilities
installNetworkUtilities
installPython


# UPS support
apt-get -y install nut
# change owner of the folders
chown -R root:nut  ~/${TRANSVERFOLDER}/etc/nut

# Remove unnecessary
apt-get -y remove ifupdown

if [[ "${CONTROLLER}" == "Modberry500M3" ]] ; then
  echo "  ${CONTROLLER} remove specific settings START"
  
  #TODO check if exist on ESPRESSObin-Ultra
  # disable GUI
  systemctl set-default multi-user.target
  gnome-session-quit > /dev/null 2>&1
    
  systemctl stop redis-server  >/dev/null 2>&1
  systemctl disable redis-server  >/dev/null 2>&1
  systemctl stop vncserver-x11-serviced  >/dev/null 2>&1
  systemctl disable vncserver-x11-serviced  >/dev/null 2>&1
  
  #npe-conf  serial port
	#npe-conf
  #npe_service
  #npe_service
  #npeudp_start
  #npeudp_start
  systemctl stop mosquitto
  systemctl disable mosquitto
  #exe_service
  #exe_service
  
  #remove from cronjob
  crontab -l | egrep -v "vpn_watchdog" | crontab -
  
  echo "  ${CONTROLLER} remove specific settings END"
fi

# do this only on first installation
if [[ "$FirstInstallation" == "True" ]] && [[ "${CONTROLLER}" == "Modberry500M3" || "${CONTROLLER}" == "ESPRESSObin-Ultra" ]] ; then
  echo "  ${CONTROLLER} specific network settings START"

  systemctl stop inetd
  systemctl disable inetd

  # This will kill the network and connection is lost
  # Not exist on ESPRESSObin-Ultra
  systemctl stop dhcpcd
  systemctl disable dhcpcd
  apt-get -y remove dhcpcd5 isc-dhcp-client isc-dhcp-common

  # have only one network manager
  systemctl stop networking
  systemctl disable networking

  systemctl enable systemd-networkd.service

  # make temporary first interface as DHCP client, otherwise we are without the network
  cp -p ~/${TRANSVERFOLDER}/etc/systemd/network/dhcp.network /etc/systemd/network/.

  systemctl start systemd-networkd.service

  # wait to start network
  # until ping -c1 www.google.com >/dev/null 2>&1; do :; done
  # until dig +short google.com >/dev/null 2>&1; do :; done
  #sleep 5
  isNetworkAlive
  
  echo "  ${CONTROLLER} specific network settings END"
fi

# install the rest of the packages
#apt-get -y install netplan.io
installPackage --critical netplan.io

echo "*** Copy new files ***"
# Create folders is the foldera are not already created
mkdir -pv /{etc/{default,netplan,systemd},usr/local/{bin,share/{arduino,fluence}}}

# Copy all folders
cp -pr ~/${TRANSVERFOLDER}/etc/* /etc/.
cp -pr ~/${TRANSVERFOLDER}/usr/* /usr/.
cp -pr ~/${TRANSVERFOLDER}/SelfConfiguration /home/fos/.
cp -pr ~/${TRANSVERFOLDER}/Controllino ~/.

FLUENCEOLDFILE="${FLUENCEFILE}.${TIMENOW}"
if [[ -f "$FLUENCEOLDFILE" ]]; then
    #TODO Mege difference back to FLUENCEFILE
    difflines=$(diff -w ${FLUENCEOLDFILE} ${FLUENCEFILE} | wc -l)
    #echo "Difflines $difflines"
    #if [[ "$difflines" == 0 ]]; then
    #  echo "Netplan files are the same. Network restart is not required."
    #fi
fi

# Force off auto negosiation
if [[ "${CONTROLLER}" == "Modberry500M3" ]] ; then
  echo "ethtool -s eth0 speed 100 duplex full autoneg off" >> /usr/local/bin/fluence_system-init.sh
fi

# Fluence services
echo "*** Reenabling Fluence services ***"
FSERVICES=$(ls ~/${TRANSVERFOLDER}/etc/systemd/system)
for i in $FSERVICES; do
    if [[ $i =~ .*\.service$ ]] ; then
        #echo "I want to do something with the file $i"
        systemctl stop $i
        systemctl disable $i
    fi
    # enable back only fluence services !!!!!
    if [[ $i =~ ^fluence.*\.service$ ]] ; then
        #echo "I want to do something with the file $i"
        systemctl enable $i
    fi
done

# UPS
echo "*** UPS rules ***"
ln -sf /lib/udev/rules.d/62-nut-usbups.rules /etc/udev/rules.d/.

# Arduino/Controllino
# already installed

# do this only on first installation
if [[ "$FirstInstallation" == "True" ]] && [[ "${CONTROLLER}" == "Modberry500M3" || "${CONTROLLER}" == "ESPRESSObin-Ultra" ]] ; then
  echo "  ${CONTROLLER} specific network settings. Remove DHCP from first port"
  # Remove temp DHCP address from first port
  rm /etc/systemd/network/dhcp.network
  systemctl restart systemd-networkd
fi

# do this only on first installation
if [[ "$FirstInstallation" == "True" ]]; then
  rm /etc/FLUENCE_INITIALIZED
fi

# use new network defined in /etc/netplan/network.yaml
# Assign static ip to br0
if [[ -n "$CONTROLLERIP" ]] ; then
  /usr/local/bin/fluence_set-assigned-ip.py "${CONTROLLERIP}"
fi
# Assign internal network to br1
/usr/local/bin/fluence_set-static-ip.py


RestartNetwork=True
NETPLANOLDFILE="/etc/netplan/network.yaml.${TIMENOW}"
if [[ -f "$NETPLANOLDFILE" ]]; then
    difflines=$(diff -w ${NETPLANOLDFILE} /etc/netplan/network.yaml | wc -l)
    #echo "Difflines $difflines"
    if [[ "$difflines" == 0 ]]; then
      RestartNetwork=False
      echo "Netplan files are the same. Network restart is not required."
    fi
fi

if [[ "$RestartNetwork" == "True" || "$FirstInstallation" == "True" ]]; then
  echo "*** Netplan apply ***" 
  netplan generate
  netplan apply
  # wait to start network
  #sleep 5
  isNetworkAlive
fi

installLogs

#This is intentionally left as the last in the installation
#apt-get -y install snmp
installPackage snmp
#yes '' | apt-get -y -o Dpkg::Options::="--force-confdef" -o Dpkg::Options::="--force-confold" install lldpd
installPackage --noPrompt lldpd
# DHCP server on br1
#yes '' | apt-get -y -o Dpkg::Options::="--force-confdef" -o Dpkg::Options::="--force-confold" install isc-dhcp-server
installPackage --noPrompt isc-dhcp-server

cleanUp



