#/bin/bash
####################################################################################
#
# Deploy Utils it is not intend to call the script directly
# @author: Boris Kajganic (boris.kajganic@fluenceenergy.com)
# @Version: 2021-09-22
# NOTE: Following packets must be installed:
#    xxx
# You will need write permissions in the directory where you executing this script
#
####################################################################################

# Avoid to call scrip directly, it would not work if CONTROLLER is not defined
# CONTROLLER type must be defined
if [ -z "${CONTROLLER}" ] ; then
  echo "  CONTROLLER variable is not defined"
  popd
  exit 1
fi

TIMENOW=$(date +%Y%m%d%H%M)

#Declare a string array of known Controllers
ControllerArray=("BananaPiR2" "BananaPiR64" "Modberry500M3" "ESPRESSObin-Ultra")

for val1 in ${ControllerArray[*]}; do
  if [ "${val1}" == "${CONTROLLER}" ]; then
    ControllerFound=1
  fi
done

if [[ -z "${ControllerFound}" ]] ; then
  echo "  CONTROLLER variable is not in $ {ControllerArray}"
  for val1 in ${ControllerArray[*]}; do
    echo "    ${val1}"
  done
  popd
  exit 1
fi

CONTROLLERDATA="${CONTROLLER}_data.tgz"
TRANSVERFOLDER="${CONTROLLER}_transfer"
FOSVERSION="${CONTROLLER}_fos.txt"

localhelp () {
  echo "Usage:"
  echo "  ${CONTROLLER}_deploy.sh clear"
  echo "    Remove ${TRANSVERFOLDER} and ${CONTROLLERDATA}"
  echo "  ${CONTROLLER}_deploy.sh copy"
  echo "    Collect necessary data in transfer folder"
  echo "  ${CONTROLLER}_deploy.sh pack"
  echo "    pack transfer folder"
  echo "  ${CONTROLLER}_deploy.sh cpack"
  echo "    copy and pack transfer folder"
  echo "  ${CONTROLLER}_deploy.sh fos"
  echo "    create ${CONTROLLER}_fos.txt version file"
  echo "  ${CONTROLLER}_deploy.sh remote_IP rootpwd copy pack"
  echo "  ${CONTROLLER}_deploy.sh remote_IP rootpwd"
  echo "  ${CONTROLLER}_deploy.sh file_with_remote_IPs rootpwd"
  echo "    copy packed data and execute deploy_script.sh on remote computer"
  echo
  echo "always 'copy' before 'pack'"
}


clearData() {
  #echo "ClearData"
  rm -rf $TRANSVERFOLDER
  rm -f $CONTROLLERDATA
  rm -f $FOSVERSION
}

copybaseData() {
  rm -rf ${TRANSVERFOLDER}
  mkdir -pv ${TRANSVERFOLDER}/{Controllino,etc/{netplan,systemd},usr/local/{bin,share/fluence}}  >/dev/null
  mkdir -pv ${TRANSVERFOLDER}/usr/share/netplan/netplan/cli/commands/  >/dev/null

  cp -rp root/* ${TRANSVERFOLDER}/.
  cp -rp ../SelfConfiguration ${TRANSVERFOLDER}/. 
  cp -rp ../Controllino/images/* ${TRANSVERFOLDER}/Controllino/. 
}

copyData () {
  echo "Copy data to ${TRANSVERFOLDER}"

  # Base copy
#  ./prepare_script.sh
  copybaseData

  # Board specific
  cp -rp ../${CONTROLLER}/root/* ${TRANSVERFOLDER}/.

  # not working for some reason
  #chown -R root:root transfer
}

packData () {
  echo "Pack data from ${TRANSVERFOLDER}"
  rm -f $CONTROLLERDATA
  tar -czvf $CONTROLLERDATA ${TRANSVERFOLDER} >$CONTROLLERDATA.txt 2>&1
}

fosVersion () {
  echo "fosVersion"
  GIT=$(git describe --abbrev=8 --dirty --always --tags)
  printf 'FOS=GIT-%s\n' "$GIT" >${FOSVERSION}
}

copyAndExecuteScript () {
  echo "Installing $CONTROLLERIP"
  
  FirstInstallation=False
  # If the network is already installed, br1 exists and ip is assigned to the interface
  # br1 is used instead br0, because ESPRESSObin-Ultra comes with predefined br0 (bridge to wifi)
  CIP=$(sshpass -p $PASSWD ssh -o StrictHostKeyChecking=no root@$CONTROLLERIP ip -f inet -o addr show br1 | awk 'NR==1{print $4}' |cut -d/ -f 1)
  if [[ -z "$CIP" ]] ; then
    FirstInstallation=True
    echo "First Installation of FOS"
  else
    echo "Update Installation of FOS"
  fi
  
  
  LOCSETTINGS=$(locale | grep LANG=)
  LOCLANG=${LOCSETTINGS#LANG=}
  TIMEZONE=$(cat /etc/timezone)
  DATE=$(LC_TIME="en_US" date -u)
  #REMOTECMD="timedatectl set-timezone $TIMEZONE; LC_TIME=\"en_US\" date -s \"${DATE}\";"
  #REMOTECMD="locale-gen $LOCLANG; update-locale $LOCSETTINGS; timedatectl set-timezone $TIMEZONE; LC_TIME=\"en_US\" date -s \"${DATE}\";"  
  REMOTECMD="grep -q '# $LOCLANG' /etc/locale.gen && sed -i -e 's/# $LOCLANG/$LOCLANG/' /etc/locale.gen && locale-gen $LOCLANG; localectl set-locale $LOCSETTINGS; timedatectl set-timezone $TIMEZONE; LC_TIME=\"en_US\" date -s \"${DATE}\";"
  echo "Ececute remotely: $REMOTECMD" 
  sshpass -p $PASSWD ssh -o StrictHostKeyChecking=no root@$CONTROLLERIP $REMOTECMD

  sshpass -p $PASSWD scp -o StrictHostKeyChecking=no $CONTROLLERDATA root@$CONTROLLERIP:~/.
  sshpass -p $PASSWD scp -o StrictHostKeyChecking=no $FOSVERSION     root@$CONTROLLERIP:~/.
  sshpass -p $PASSWD scp -o StrictHostKeyChecking=no deploy_script.sh root@$CONTROLLERIP:~/.

  if [[ "$FirstInstallation" == "True" ]] && [[ "${CONTROLLER}" == "Modberry500M3" || "${CONTROLLER}" == "ESPRESSObin-Ultra" ]] ; then
    REMOTECMD1="tar -xzvf ${CONTROLLERDATA} >/dev/null; nohup bash ~/deploy_script.sh ${CONTROLLER} > ~/deploy_${TIMENOW}.txt"
    REMOTECMD1+=' & 2>&1'
  else
    REMOTECMD1="tar -xzvf ${CONTROLLERDATA} >/dev/null; ~/deploy_script.sh ${CONTROLLER} 2>&1"
  fi
  echo "Ececute remotely: $REMOTECMD1" 
  sshpass -p $PASSWD ssh -o StrictHostKeyChecking=no root@$CONTROLLERIP $REMOTECMD1
  
  #REMOTECMD2='tail -f ~/deploy.txt'
  #echo "Ececute remotely: $REMOTECMD2" 
  #sshpass -p $PASSWD ssh -o StrictHostKeyChecking=no root@$CONTROLLERIP $REMOTECMD2
  #echo "$@"
}



ParseAndExecuteCommandLineData () {
  for var in "$@"
  do
    #echo "$var"
    shift # remove argument from arg list
    if [[ "${var}" == "clear" ]]; then
      clearData
    elif [[ "${var}" == "fos" ]]; then
      fosVersion
    elif [[ "${var}" == "copy" ]]; then
      copyData
    elif [[ "${var}" == "pack" ]]; then
      packData
    elif [[ "${var}" == "cpack" ]]; then
      copyData
      packData
    else
    set -- "$@" "$var" # add the argument back as the last if is not one of above
    fi
    minOneVarFound=1  # IP, password, copy, pack or cpack
  done

  if [ -z "${minOneVarFound}" ] ; then
    localhelp
    popd > /dev/null 2>&1
    exit 1
  fi

  if [ -z "$1" ]; then
    echo "Target ${CONTROLLER} IP address or file with remote IPs is missing"
    localhelp 
    popd > /dev/null 2>&1
    exit 1
  fi

  if [ -z "$2" ]; then
    echo "Root password is missing"
    localhelp 
    popd > /dev/null 2>&1
    exit 1
  fi

  # IP / root without command
  # if the archive is not already exists
  if [ ! -f $CONTROLLERDATA ]; then
    copyData
    packData
  fi
  
  if [ ! -f $FOSVERSION ]; then
    fosVersion
  fi

  PASSWD=$2
  FILE=$1
  CONTROLLERIP=$1

  if [[ -f "$FILE" ]]; then

    for CONTROLLERIP in $(cat $FILE); do
      #echo "TEST FOR ${CONTROLLERIP}"
      # The -c 1 means send one packet, and the -t 1 means a 1 second timeout    
      #ping -c 1 -t 1 ${CONTROLLERIP} >> ${CONTROLLERIP}.log 2>&1
      #ping -c 1 -t 1 ${CONTROLLERIP}
      #echo "$CONTROLLERIP  $(copyAndExecuteScript) "
      copyAndExecuteScript
    done

  elif [[ $CONTROLLERIP =~ ^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+$ ]]; then
    #echo "$CONTROLLERIP  $(copyAndExecuteScript) "
    copyAndExecuteScript
  else
    echo "$CONTROLLERIP is not correct IP"
  fi
}


