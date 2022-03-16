
# General
## Files

|Filename | Description |
| --- | --- |
|server.py               | This is the server to be called i.e. on the array controller |
|leaf_selconfiguration.py| This is the leaf selfconfiguration script to be executed when you|
|                        | want to reconfigure the leaf|
|leaf_selfdiscovery.cfg  | A config file of the leaf_selconfiguration.py script|
|config.ini              | configuration file of the server|

# Installation

* get the repository
* start the configuration server ('python3 ./server.py'), it will run on 127.0.0.1:5000, the config frontend is under http://127.0.0.1:5000/admin.
* Configure a loop over the GUI (by default there is a testing loop configured)
* start the configuration script manually ('sudo python3 ./leaf_selfconfiguration.py'). If you are in testing mode, set the environment variable before executing the script ('export TESTING=true')
* The config server uses the directory 'file' for the data to be distributed. All files will be downloaded by the leaf and put in 'post-configuration-execute'. All files with executable rights will be sorted (therefore some of them are starting with 010, 020, etc.) and executed.


# Introduction

## Starting the server

First of all you have to start the server, preferably on the array controller where also the DHCP server is located. It will show up the requests on command line

The URL for auto-configuration of the leaf_selfconfiguration.py script is http://<IP\>:5000 . There are different endpoints for the auto-configuration. **TODO: Describe endpoints**

The GUI configration is located at http://<IP\>:5000/admin .
You have to configure for each loop on the switch 

* the name of the switch as it appears in the lldpcli command (CDP)
* the west (wan) and the east (lan3) interface, i.e. 'gi4' and 'gi7'
* the core and the node ( i.e. core 1, node 3) -> IP address will be derived from this, so leaf 5 here would be 10.1.3.5, naming convention is 10.<core\>.<node\>.<leaf-position\>
* the number of leafs in the loop
* you also might want to configure globally the SITE-ID in the file config.ini 'site_code = enst00', which will prefix the hostname

## The Self configuration script on the leaf controller

### What is does

The script calls the lldpcli command the determine the topology based on CDP protocol. If it detects a stable topology within a configured timeframe, means the same topology for a certain time (with regular calls), it goes into the configuration process. Whenver the topology changes in between this timeframe, it will restart from the beginning of the timeframe to detect a stable topology.

The timeframe to detect a stable topology should be longer than the update time/interval of the cisco routers. It could be configured in with the parameters 'StabilizeCount' and 'StabilizeSeconds', see below.

### Configuration process

The configuration process starts with a stable topology, means the same picture of lldpcli for a certain time frame.

Fom this point on, it determines the position in the loop. This could either be done when the loop is closed (The cisco switch is seen by the leaf on east **and** west) or when the leaf sees the cisco switch only on one side. If the cisco switch could not be seen on any side, configuration is not possible.

Once it has determined the position in the loop, it contacts the configuration server to receive its

* hostname
* IP address
* scripts that will be executed

It will the apply the hostname to /etc/hostname as well as to /etc/hosts with the new IP address via a downloaded script. This IP address is also applied to the br0 interface with the netplan template.

Multiple calls to the script will re-determine the position in the loop and download the data from the config server and reexecute the scripts. The scripts will do nothing if the IP address and hostname haven't changed. Otherwise it will reconfigure like in the origin configuration process.

You can force a reconfiguration by using the '-r' parameter on the script or by checking the reconfigure-checkbox in the Configuration Server GUI.

`sudo ./leaf_selfconfiguration.py -r`

**You have to call the script with proper rights in order to access the netplan stuff, /etc/ ... whatever and so on...**

### Parmeters in leaf_selfdiscovery.cfg

|Parameter                   | Description |
| --- | --- |
|StabilizeCount              | Number as a count how often the loop should be called, in former  time this was referred as the tick count|
|StabilizeSeconds            | This is the number in seconds for the tick interval. i.e. 1s, so  every second there will be a tick|
|ConfigServer                | List of config server addresses, separated by a ','    i.e. 192.168.2.3:5000, 127.0.0.1:5000|
|AddDhcpServerAsConfigServer | 'yes' or 'no' and the address of the config server will be added in front of the list of 'ConfigServer'|
|MachineIdFile               | Location of the machine-id file, i.e. /etc/machine-id|
|NetplanTemplate             | The network template to use, i.e. network.yaml.template|
|NetplanDest                 | The destination location of the netplan file,  i.e. /etc/netplan/network.yaml|
|IptablesTemplate            | The iptables template to use, i.e. iptables.sh.template|


# Server and Web Interface

## Presentation

The server gives the network configuration to device.

This program launches a REST server with a web interface.  
- The main use of the REST server is for communication with cubes.  
- The web interface is to manage the loop list and watch log.  

The network configuration *(IP, mask, gateway, name)* is calculated according to the properties of the loop *(switch name, connected ports, breaker, feeder, core, node, row)* and its position.

The web interface is in `/admin` address, in port 5000 by default.

Server shares file in 'file' folder, dowloadable from address `/file/*.*`

## Installation

### pre-require

- Python3 *(min 3.6)*
- modules : flask, requests

### files

- **server.py**: this program.
- **config.ini**: configuration of this program.
- **database.sqlite3**: database storing all data *(loops, cubes, logs)*.
- **admin**: folder which contains all file for the management interface.

### standalone, for testing

Copy files and the folder in a same location.  
Then, run the command : `python3 server.py`

### for production

**[TODO]** Require a real WSGI server. Doc: https://flask.palletsprojects.com/en/1.1.x/deploying/  
**[TODO]** Make the program compatible with a WSGI server  

### Gen6 installation

For the moment, the installation is a testing configuration. All is in home folder of **mt.fgras**, waiting to find a better location

#### Gen6 installation / preparation

Instead of mt.fgras, use the appropriate user <fluence>

Files are copied in `/home/mt.fgras/selfconfig-server/`

Installation of the Python Virtual ENVironment: *(venv, optionnal but recommended)*  
`sudo apt-get install python3-venv`  
`python3 -m venv /home/mt.fgras/venv/selfconfig-server` **(TODO: find a better folder)**  
Activation of the Python virtual environment:  
`. /home/mt.fgras/venv/selfconfig-server/bin/activate`  
Install of required modules:  
`pip install flask,requests`

#### Gen6: transform in service

First, create a shell script to initialise and launch the program:  
**/home/mt.fgras/selfconfig-server/run.sh** *(with 755 access right)*  
> #!/bin/bash  
> 
> . /home/mt.fgras/venv/selfconfig-server/bin/activate  
> 
> python /home/mt.fgras/selfconfig-server/server.py -c /home/mt.fgras/selfconfig-server/config.ini -d /home/mt.fgras/selfconfig-server/database.sqlite3

It activates the Python virtual environment, then it launches the server with location in absolute of the configuration and the database files

Second, create a service script:
** /etc/systemd/system/selfconfiguration.service**  
> [Unit]  
> Description=LeafController / SelfConfiguration  
> After=network.target  
> StartLimitIntervalSec=0  
>   
> [Service]  
> Type=simple  
> Restart=on-failure  
> RestartSec=1  
> User=47002  
> WorkingDirectory=/home/mt.fgras/selfconfig-server  
> ExecStart=/bin/bash /home/mt.fgras/selfconfig-server/run.sh  
> StandardOutput=syslog  
> StandardError=syslog  
>   
> [Install]  
> WantedBy=multi-user.target

Note: User 47002 is "*mt.fgras*". I was not able to type a dot in user name, so I had to use the user ID.

#### Gen6: Service manager

Launch the service: `sudo systemctl start selfconfiguration.service`  
Stop the service: `sudo systemctl stop selfconfiguration.service`  

Enable auto-start at boot: `sudo systemctl enable selfconfiguration.service`  
Disable the auto-start: `sudo systemctl disable selfconfiguration.service`  

Show status : `sudo systemctl status selfconfiguration.service`  
Show log : `sudo journalctl -u selfconfiguration.service -b`  



# Useful commands for Isaiah's test environment

## Tunnel to GUI

ssh -J mt.fmetz@enst01nm02pr.fluenceenergy.com,sandbox -f array -L 5001:127.0.0.1:5000 -N

## Jump to machine

ssh -l mt.fmetz -J mt.fmetz@enst01nm02pr,mt.fmetz@sandbox c1n1l2

# Gen6 in DEPCTR

## Tunnel to GUI
```
ssh -J bkajganic.admin@10.250.225.7 -f bkajganic.admin@10.194.5.3 -L 5000:localhost:5000 -N
```
## Local Host

Access config server GUI with:
```
http://localhost:5000/admin/
```
on local machine.


