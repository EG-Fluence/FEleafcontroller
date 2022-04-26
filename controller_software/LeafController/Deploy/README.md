# Controller Deploy Scripts

Deploy always from ```Gen6``` branch. ```master``` is developing branch.
```
git checkout Gen6
```
If the project has own branch as ```PaP``` deploy from project branch
```
git checkout RaP
```

The goal is to make abstract unique script for deploing of:
- Cube and Telco rack Controller  (Ex. Leaf Controller)
- Core Controller 
- Node Controller
- Array Controller


## Starting point

__Outdated__
~~https://github.com/aeses/dep_sdu/blob/master/misc/debian93_adv5/deploy_controller.sh~~

~~Execute script deploy.sh from
https://github.com/aeses/dep_sdu/blob/master/misc/debian93_adv5/deploy_controller/deploy.sh
on remote controller~~

## Cube and Telco rack Controller

Basic installation of all controller type is the same. All hardwere will be install as cube controller at the begining.

## Node Controller

[Documentation](https://fluenceenergy.atlassian.net/wiki/spaces/APD/pages/946798632/Node+Controller+Configuration+Advancion+5) in SharePoint

## Core Controller

Basic instalation of Core Controller is thesame as for Node Controller

## Installation

Detailed instruction how to setup the project on array controller you can find [here](./doc/ProjectSetup.md)

Go to Gen6 array controller, your home directory.
```
cd
mkdir -p bpi-r2
git clone https://github.com/FluenceEnergy/controller_software.git
cd controller_software
git checkout Gen6
```
if you would like to make some correction
```
git config --local user.email "name.familyname@flueneceenergy.com"
git config --local user.name "GitUserName"
```

### Get correct time from array controller
__Not required anymore, integrated inside deploy script.__
```
$ date
Tue Mar  9 02:07:47 CET 2021
```
Put correct time on each other controller (Core,node,Cube,etc), otherwise ```apt update``` will not work.
```
date -s "Tue Mar  9 02:07:47 CET 2021"
```

### Supported hardware
- BananaPiR2
- Modberry500M3
- ESPRESSObin-Ultra

```
cd ~/bpi-r2/controller_software/LeafController/Deploy
```
three scripts one for each SBC.
| SBC                 | script                      |
|---------------------|-----------------------------|      
| BananaPiR2          | BananaPiR2_deploy.sh        |
| Modberry500M3       | Modberry500M3_deploy.sh     |
| ESPRESSObin-Ultra   | ESPRESSObin-Ultra_deploy.sh |

all three script have the same functionality
```
./BananaPiR2_deploy.sh

Usage:
  BananaPiR2_deploy.sh clear
    Remove transfer folder and controller.tgz data
  BananaPiR2_deploy.sh copy
    Collect necessary data in transfer folder
  BananaPiR2_deploy.sh pack
    pack transfer folder
  BananaPiR2_deploy.sh cpack
    copy and pack transfer folder
  BananaPiR2_deploy.sh remote_IP|file_with_remote_IPs rootpwd
    copy, packed data and execute deploy_script.sh on remote computer
  BananaPiR2_deploy.sh remote_IP rootpwd copy pack

always 'copy' before 'pack'

```
Usage example:
```
./BananaPiR2_deploy.sh 192.168.178.30 root copy pack
./Modberry500M3_deploy.sh 192.168.178.31 techbase copy pack
./ESPRESSObin-Ultra_deploy.sh 192.168.178.32 admin copy pack
```

Modberry500M3_deploy.sh and ESPRESSObin-Ultra_deploy.sh will execute remote script with ```nohup bash ~/deploy_script.sh ${CONTROLLER} > ~/deploy.txt```
On the remote computer in folder ```/root```, the workflow could be monitor
```
tail -f ~/deploy.txt
```
__Hint__: After some time, tail will stop working because, the network configuration and IP are changed. Waith until the computer is back on the network. It could be checked with help of fping 
The controller is installed if the name is like ```leaf-5ec8299eba```. It must start with __leaf-__

## Python

Additional rquired for python. Instlled automaticaly from script.
```
sudo apt -qq list python3-cython python3-numpy python3-twisted python3-pandas python-pymodbus
```
