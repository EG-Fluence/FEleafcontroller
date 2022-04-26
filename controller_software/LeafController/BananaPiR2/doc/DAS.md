# DAS

## Start
```
systemctl start das
```

## Stop
```
systemctl stop das
```

## Status
```
systemctl status das
```

## DAS Web Server on Leaf Controller
Web Server on leaf Controller is running on port 9080
```
LeafController:9080
```

user: ```admin```

pasword: ```admin```

## DAS Web Server at Deployment Center 

Working if VPN is establish

https://us-lab-array.fluenceenergy.com:9443/
  
## DAS Web Server at Erlangen Lab 

Working if VPN is establish

https://eu-lab-array-das.fluenceenergy.com:9443/

## DAS Automatic Import of Datapoints
Two devices are necessary: A BananaPI (BP) and a laptop (LT).
Usually, the following Steps are necessary to import Datapoints. It is always mentioned on which device the step has to be executed.   

Important: The IP-Address is preconfigured as `172.16.1.19` and the Port is preconfigured as `1602`. If this does not have to be changed, only the first and the last two steps are necessary.

1. LT: Get all necessary files from sharepoint:  
[Necessary Files](https://fluenceenergy.sharepoint.com/:f:/r/sites/nextgen/Shared%20Documents/Controls%20HW%20and%20SW/NextGen%20Controller%20Workstream/Documentation/Testing/DAS?csf=1&web=1&e=QY3cFb)    
If IP-Adress and Port do not have to be changed, the preconfigured json file in the folder can be used.
1. LT: Adapt the IP-Address if necessary in the file `buildspec-dataPoints_leaf.xlsx`
1. LT: Adapt the Port in `cube_ds-20201118.xlsx`
1. LT: Install ruby if necessary
1. LT: Clone the following repository: https://github.com/FluenceEnergy/das_utilities
1. LT: Paste all the excel sheets in `\das_utilities-master\src\mango\build_templates`
1. LT: Create a folder called `builds` in `\das_utilities-master\src\mango`
1. LT: Open a terminal and cd to location `\das_utilities-master\src\mango`
1. LT: Execute the following command:

        ruby maintain/build_spec.rb build_templates\buildspec-dataPoints_leaf.xlsx -f builds\leaf.json

    
 
      (If there are errors, consult the manual mentioned below)

1. BP: Start the BananaPi and on the BananaPi start the DAS with the following command: `systemctl start das`. Note: It can take a few minutes for the DAS to start.

1. LT: Log in to DAS by entering `IPADDRESSOFBANANAPI:9080` in a browser (Your Laptop must be connected to the same network as the BananaPi for this to work) Go to Admin home. There Configuration import/export can be seen. Click it and Drop the JSON file that was created (or coppied directly from sharepoint).

If there are any errors consult the extensive manual in the following location: 
[Extensive Manual](https://fluenceenergy.atlassian.net/wiki/spaces/APD/pages/823754800/Updating+Data+Points+in+DAS+existing+sites)

# Confluence
- [Automated DAS Updates](https://fluenceenergy.atlassian.net/wiki/spaces/APD/pages/245104671/Automated+DAS+Updates+OLD+DOCUMENTATION)
- [Updating Data Points in DAS (existing sites)](https://fluenceenergy.atlassian.net/wiki/spaces/APD/pages/823754800/Updating+Data+Points+in+DAS+existing+sites)
- [Create empty MariaDB](https://fluenceenergy.atlassian.net/wiki/spaces/APD/pages/1736212627/DAS+-+Create+empty+ma+DB+ma4+test+Alamitos)

# SharePoint Gen6 Excel files
https://fluenceenergy.sharepoint.com/sites/ArchitectureandSoftwareEngineering/Services/Forms/AllItems.aspx?csf=1&web=1&e=qDYZL1&cid=03ae3181%2D9c89%2D4ec4%2D826d%2D05a1757cdc0f&FolderCTID=0x012000BA123C1EE8879B4AA429A7DD0565FC4F&viewid=8322b87b%2D9074%2D4a47%2Da01a%2Dad8cc2b5a53c&id=%2Fsites%2FArchitectureandSoftwareEngineering%2FServices%2FSite%20Specific%20Current%20Releases%2FDAS%2Fdatapoints%2F6%2E0%2E0%2E0
