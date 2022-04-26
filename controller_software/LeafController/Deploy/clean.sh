#/bin/bash
####################################################################################
#
# Clean Script is removs all transver and packed transver datapart of Deploy Script 
# @author: Boris Kajganic (boris.kajganic@fluenceenergy.com)
# @Version: 2021-06-10
# NOTE: Following packets must be installed:
#    ---
# You will need write permissions in the directory where you executing this script
#
###################################################################################

rm -rf *_transfer
rm -f *_data.tgz
rm -f *_data.tgz.txt
rm -f *_fos.txt
rm -f ControllerAllData.tgz


