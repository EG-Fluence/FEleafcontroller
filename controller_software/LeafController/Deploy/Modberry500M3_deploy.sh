#/bin/bash
####################################################################################
#
# Deploy Script for Modberry 500 M3
# @author: Boris Kajganic (boris.kajganic@fluenceenergy.com)
# @Version: 2021-02-08
# NOTE: Following packets must be installed:
#    sshpass
# You will need write permissions in the directory where you executing this script
#
####################################################################################

CONTROLLER="Modberry500M3"

SCRIPTFILENAME=`which $0`
SCRIPTNAME=${SCRIPTFILENAME##*/}
SCRIPTDIR=${SCRIPTFILENAME%/*}

#pushd $(pwd)
pushd -n $(pwd) > /dev/null 2>&1
cd ${SCRIPTDIR} > /dev/null 2>&1

source ./utils_deploy.sh

#echo current directory $(pwd)
#echo SCRIPTFILENAME = $SCRIPTFILENAME
#echo SCRIPTNAME = $SCRIPTNAME
#echo SCRIPTDIR = $SCRIPTDIR

ParseAndExecuteCommandLineData "$@"

popd > /dev/null 2>&1

