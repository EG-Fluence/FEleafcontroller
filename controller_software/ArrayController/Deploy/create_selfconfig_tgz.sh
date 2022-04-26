#!bin/bash


cd ..
tar czvf selfconfig-$(date '+%Y-%m-%d').tgz controller_software/LeafController/SelfConfiguration controller_software/ArrayController

cd -
