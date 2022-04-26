#/bin/bash
####################################################################################
#
# Pack together in one archive all scripts and data
# @author: Boris Kajganic (boris.kajganic@fluenceenergy.com)
# @Version: 2021-06-10
# NOTE: Following packets must be installed:
#    ---
# You will need write permissions in the directory where you executing this script
#
###################################################################################

CONTROLLERALLDATA="ControllerAllData.tgz"

rm -f $CONTROLLERALLDATA
tar -czvf $CONTROLLERALLDATA *_data.tgz *.sh *_fos.txt
