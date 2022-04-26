#!/bin/bash
################################################
#
# Simple Script to check for ntp servers in a network
# @author: Per Lasse Baasch (https://skycube.net)
# @Version: 2014-03-10
# NOTE: you need ntpdate installed (should be present)
# you will need write permissions in the directory where you executing this script
#
################################################
# CLASS C NETWORK TO SCAN
# Syntax 'xxx.xxx.xxx' NO TAILING DOT
BASEIP='192.168.0';
/bin/rm -f ntpfound.log;
/bin/touch ntpfound.log;
for (( c=1; c<=254; c++ ))
do
   echo "Checking $BASEIP.$c";
   /usr/sbin/ntpdate -q $BASEIP.$c > ntpcheck.log 2>&1; cat ntpcheck.log | grep 'time server' >> ntpfound.log;
done
# Remove temporary log file
/bin/rm -f ntpcheck.log;
# Display results
cat ntpfound.log;
### Possible Output Which indicate there is a possible a nto server present
#10 Mar 13:16:01 ntpdate[25552]: adjust time server 192.168.0.23 offset 0.013292 sec
#10 Mar 13:16:09 ntpdate[25555]: adjust time server 192.168.0.66 offset 0.013306 sec
#10 Mar 13:16:39 ntpdate[30586]: adjust time server 192.168.0.102 offset -0.037400 sec
exit;

