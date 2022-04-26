#/bin/bash
####################################################################################
#
# Deploy Script for Banana Pi R64
# @author: Boris Kajganic (boris.kajganic@fluenceenergy.com)
# @Version: 2021-11-11
# NOTE: Following packets must be installed:
#    sshpass
# You will need write permissions in the directory where you executing this script
#
####################################################################################

CONTROLLER="BananaPiR64"

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

