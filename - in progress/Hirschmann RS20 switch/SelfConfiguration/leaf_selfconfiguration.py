#!/usr/bin/python3

import time
import re
import requests
import sys, getopt
import fluencelog
import leaf_selfdiscovery as leaf_selfd
import leaf_configuration_client as lcc
import os
import json
import configparser
from pathlib import Path
import subprocess
from random import randint
from time import sleep
import os.path

logger = fluencelog.initlog()


fn = sys.argv[0]

print("dirname : " + os.path.dirname(os.path.abspath(fn)))
print("filename : " + os.path.abspath(fn).split("/")[-1])

configBaseDir = os.getenv('CONFIGBASEDIR', os.path.dirname(os.path.abspath(fn)))

print(configBaseDir)

config = configparser.ConfigParser()
config.read(configBaseDir + '/leaf_selfdiscovery.cfg')

def git_version():
	result = subprocess.run(['git', 'describe', '--abbrev=4', '--dirty', '--always', '--tags'], stdout=subprocess.PIPE)
	return result.stdout.decode('utf-8').replace('\n','',-1)


def hello():
	logger.info("+--------------------------------------------------")
	logger.info("+ Fluence Leaf Controller Self Configuration       ")
	logger.info("+ Git Version : " + git_version())
	logger.info("+--------------------------------------------------")

def addDhcpAsConfigServer():
	if config['DEFAULT']['AddDhcpServerAsConfigServer'] == "yes":
		out = subprocess.getoutput("dhclient -v 2>&1 | grep DHCPACK | cut -d\" \" -f5")
		if out != "":
			config['DEFAULT']['ConfigServer'] = out + ":5000, " + config['DEFAULT']['ConfigServer']
		else:
			logger.warn("ConfServer IP of DHCP should be added but not found.")

def waitForLldpD():
	found=False
	MAXRETRIES = 60
	cnt=0
	
	while not found and cnt < MAXRETRIES:
		time.sleep(1)
		if (cnt % 10) == 0:
			logger.info("Waiting for LLDP daemon to come up ")
		ret = os.system("ps -ef | grep [l]ldpd 2>&1 > /dev/null")
		if ret == 0:
			found=True
			break
		cnt += 1
	return found
	

def waitForIpConnectivity():
	found=False
	MAXRETRIES = 180
	cnt=0
	
	for url in config['DEFAULT']['ConfigServer'].split(','):
		while not found and cnt < MAXRETRIES:
			time.sleep(1)
			ip = re.findall( r'[0-9]+(?:\.[0-9]+){3}', url )
			if (cnt % 10) == 0:
				logger.info("Waiting for IP connectivity to " + ip[0])
			ret = os.system("ping -c 1 -W 1 " + ip[0] + " 2>&1 > /dev/null")
			if ret == 0:
				found=True
				break
			cnt += 1
		if found == True:
			break
	return found

def getCiscoHostNameSearch(lldp_result, eastWest):
	jsonData = json.loads(lldp_result)
	lldp_obj = jsonData["lldp"]
	if isinstance(lldp_obj["interface"], list):
		for interface_item in lldp_obj["interface"]:
			if eastWest in interface_item:
				chassis = interface_item[eastWest]["chassis"]
				for key, value in chassis.items():
					if "cisco" in value["descr"].lower():
						return key
					if "hirschmann" in value["descr"].lower():
						return key
	else:
		if eastWest in lldp_obj["interface"]:
			chassis = lldp_obj["interface"][eastWest]["chassis"]
			for key, value in chassis.items():
				if "cisco" in value["descr"].lower():
					return key
				if "hirschmann" in value["descr"].lower():
					return key
	return ""

def getCiscoHostName(lldp_result):
	hostname = getCiscoHostNameSearch(lldp_result,"wan")
	if hostname != "":
		return hostname
	hostname = getCiscoHostNameSearch(lldp_result,"eth0")
	if hostname != "":
		return hostname
	hostname = getCiscoHostNameSearch(lldp_result,"lan3")
	if hostname != "":
		return hostname
	hostname = getCiscoHostNameSearch(lldp_result,"eth2")
	if hostname != "":
		return hostname
	return "CISCOHOSTNAMENOTFOUND"

def getCiscoInterfaceName(lldp_result, eastWest):
	jsonData = json.loads(lldp_result)
	lldp_obj = jsonData["lldp"]
	if isinstance(lldp_obj["interface"], list):
		for interface_item in lldp_obj["interface"]:
			if eastWest in interface_item:
				chassis = interface_item[eastWest]["chassis"]
				for key, value in chassis.items():
					if "cisco" in value["descr"].lower():
						name = interface_item[eastWest]["port"]["id"]["value"]
						return name
					if "hirschmann" in value["descr"].lower():
						name = interface_item[eastWest]["port"]["descr"]
						return name
	else:
		if eastWest in lldp_obj["interface"]:
			chassis = lldp_obj["interface"][eastWest]["chassis"]
			for key, value in chassis.items():
				if "cisco" in value["descr"].lower():
					name = lldp_obj["interface"][eastWest]["port"]["id"]["value"]
					return name
				if "hirschmann" in value["descr"].lower():
					name = lldp_obj["interface"][eastWest]["port"]["descr"]
					return name

	
def getCiscoInterfaceNameWest(lldp_result):
	ifname = getCiscoInterfaceName(lldp_result, "wan")
	if ifname is None:
		ifname = getCiscoInterfaceName(lldp_result, "eth0")
	logger.debug("Interface name is " + ifname)
	return ifname

def getCiscoInterfaceNameEast(lldp_result):
	ifname = getCiscoInterfaceName(lldp_result, "lan3")
	if ifname is None:
		ifname = getCiscoInterfaceName(lldp_result, "eth2")
	logger.debug("Interface name is " + ifname)
	return ifname


# Determines the leaf id. Either by good CDP result
# or asking the config server
def determineLeafId(lcsd):

	# this blocks until we have a stable CDP topology
	isTopologyStable = lcsd.isTopologyStable()
	if not isTopologyStable:
		return
	
	myLeafId = -1
	lldp_result = lcsd.getStableLldpCliTopology()
	
	logger.debug("Stable lldp_cli result:" + str(lldp_result))
	
	if lcsd.checkTopologyRingIsClosed(lldp_result):
		myLeafId = lcsd.getWestSideInterfaces(lldp_result)
	elif not lcsd.checkTopologyRingIsClosed(lldp_result):
		if lcsd.checkCiscoWest(lldp_result):
			myLeafId = lcsd.getWestSideInterfaces(lldp_result)
		else:
			# hard case, we are on east (lan3) and have to ask
			# config server how many leafs are in the chain
			data={
				'switch_name': getCiscoHostName(lldp_result),
				'switch_interface_east':getCiscoInterfaceNameEast(lldp_result)
			}
			
			json_server_response=None
			for url in config['DEFAULT']['ConfigServer'].split(','):
				try:
					url = url.strip()
					if not '://' in url:
						url='http://'+url
					rc=lcc.RestClient(url)
					logger.debug(data)
					json_server_response = rc.POSTjson('/leafcount',data)
				except Exception as e:
					logger.warning("%s/leafcount %s %s"%(url, str(type(e)), str(e)))
					continue
				break
			if not json_server_response:
				logger.warning("Cannot connect to ConfServer")
				return
				
			logger.debug("ConfServer returned : " + str(json_server_response))
			west_side_cnt = lcsd.getEastSideInterfaces(lldp_result)
			myLeafId = json_server_response["leaf_count"] - west_side_cnt +1

	else:
		logger.error("Error determining the leaf id")
		return
	
	logger.info("My leaf id is : " + str(myLeafId))
	return myLeafId
	
def getConfigForLeafId(leaf_uuid, leaf_position, switch_name, switch_interface_name, side):
	data={
		'leaf_uuid':leaf_uuid,
		'leaf_position':leaf_position,
		'switch_name':switch_name,
		'switch_interface_' + side:switch_interface_name,
	}
	logger.debug(data)
	for url in config['DEFAULT']['ConfigServer'].split(','):
		try:
			url = url.strip()
			if not '://' in url:
				url='http://'+url
			rc=lcc.RestClient(url)
			json_server_response = rc.POSTjson('/config',data)
		except Exception as e:
			logger.warning("%s/config %s %s"%(url, str(type(e)), str(e)))
			continue
		break
	
	logger.debug("ConfServer returned : " + str(json_server_response))
	return json_server_response

def downloadPostScripts():
	for url in config['DEFAULT']['ConfigServer'].split(','):
		try:
			#--prepare connection
			url = url.strip()
			if not '://' in url:
				url='http://'+url
			#--connect and get list of files
			rc=lcc.RestClient(url)
			response = rc.GET('/file-list')
			if response: #has files
				for item in response["file-list"]:
					filename=item['name']
					isExecutable=item['executable']
					#--read
					filebin = rc.GETraw('/file/' + filename)
					if filebin: #has file
						#--prepare output file
						fullfilename=os.path.join(configBaseDir, "post-configuration-execute", filename)
						fulldirname=os.path.dirname(fullfilename)
						if not os.path.exists(fulldirname):
							os.makedirs(fulldirname)
						#--store output file
						with open(fullfilename, 'wb') as fd:
							fd.write(filebin)
						os.chmod(fullfilename, 0o770 if isExecutable else 0o660)
		except requests.exceptions.ConnectionError as e:
			logger.warning("%s/file-list %s %s"%(url, str(type(e)), str(e)))
			continue #next config server
		except Exception as e:
			logger.error("downloadPostScripts() : %s : %s %s"%(url, str(type(e)), str(e)))
		break #job done, stop trying find a config server
	#--case when no server found
	else:
		logger.error("downloadPostScripts() : no config server found")


def executePostScripts(ipAddress, hostname, reconfigure, devtype):
	dirList = []

	logger.debug("Scanning for post execution scripts " + configBaseDir + "/" + "post-configuration-execute")
	for entry in os.scandir(configBaseDir + "/" + "post-configuration-execute"):
		logger.debug(entry.name)
		if entry.is_file() and os.access(entry.path,os.X_OK):
			logger.debug("Adding ....")
			dirList.append(entry.name)

	dirList.sort()
	
	for entry in dirList:
		testing = os.getenv('TESTING', "false")
		command = "bash -c 'export FLUENCE_RECONFIGURE=" + str(reconfigure).lower() + " && export FLUENCE_TESTING=" + str(testing).lower() + " && export FLUENCE_DEVTYPE=" + devtype + " && export FLUENCE_IP=" + ipAddress + " && export FLUENCE_HOSTNAME=" + hostname + " && cd " + configBaseDir + "/" + "post-configuration-execute/ && ./" + entry + "'"
		if os.getenv('DRYRUN', "false") == "true":
			logger.debug("DRYRUN:: Command: " + command)
		else:
			out = subprocess.getoutput(command)
			logger.debug("Command: " + command + " returned: " + out)
		found = waitForIpConnectivity()
		if not found:
			logger.error("No IP connectivity after netplan apply")
			sys.exit(-1)

def hasLeafId():
	leafId = getPreviousLeafId()
	if leafId == "":
		return False
	return True

def checkIpAlreadyConfigured(newIp):
	ipv4 = os.popen('ip addr show br0 | grep "\<inet\>" | awk \'{ print $2 }\'').read().strip()
	if newIp in ipv4:
		logger.debug("IP already configured")
		return True
	return False

def checkHostnameAlreadyConfigured(newHostname):
	out = subprocess.getoutput("cat /etc/hostname | tr -d '\n'")
	if out in newHostname:
		logger.debug("Hostname already configured")
		return True
	return False

### main ###

hello()

if os.path.exists("/home/fos/SelfConfiguration/.installrunning") == True:
	logger.info("An installation is currently running. Aborting selfconfig")
	sys.exit(42)

# sleeptime=randint(1,120)
# logger.info("Waiting some time before starting selfconfig: " + str(sleeptime) + " seconds")
# sleep(sleeptime)
print("------------------------------ NO WAITING -----------------------")

if os.path.exists("/home/fos/SelfConfiguration/.installrunning") == True:
	logger.info("An installation is currently running. Aborting selfconfig")
	sys.exit(42)

# if configured this adds DHCP as config server
addDhcpAsConfigServer()

reconfigure=False

if not waitForLldpD():
	logger.error("LLDP deamon is not coming up")
	sys.exit(3)

if not waitForIpConnectivity():
	logger.error("IP connectivity is not coming up")
	sys.exit(4)

try:
	opts, args = getopt.getopt(sys.argv[1:],"hr")
except getopt.GetoptError:
	logger.info('Help: leaf_selfconfiguration.py -r        # to reconfigure')
	sys.exit(2)

for opt, arg in opts:
	if opt == '-h':
	  logger.info('Help: leaf_selfconfiguration.py -r        # to reconfigure')
	  sys.exit()
	elif opt in ("-r"):
	  reconfigure=True

machineId = ""
machineIdFile = Path(config["DEFAULT"]["MachineIdFile"])
if machineIdFile.is_file():
	f = open(config["DEFAULT"]["MachineIdFile"], "r")
	if f.mode == 'r':
		machineId = f.read()

if machineId == "":
	machineId = "no-machine-id-present"

machineId = machineId.replace('\n','')

lcsd = leaf_selfd.SelfDiscovery(config, configBaseDir)

leaf_position = determineLeafId(lcsd)

config_json = {}

if lcsd.checkCiscoWest(lcsd.getStableLldpCliTopology()):
	config_json = getConfigForLeafId(machineId, leaf_position, getCiscoHostName(lcsd.getStableLldpCliTopology()), getCiscoInterfaceNameWest(lcsd.getStableLldpCliTopology()), "west")
	logger.info("Got west side config")
elif lcsd.checkCiscoEast(lcsd.getStableLldpCliTopology()):
	logger.info("Got east side config")
	config_json = getConfigForLeafId(machineId, leaf_position, getCiscoHostName(lcsd.getStableLldpCliTopology()), getCiscoInterfaceNameEast(lcsd.getStableLldpCliTopology()), "east")

if config_json["force-reconfigure"] == True:
	reconfigure = True

logger.info("Got IP address: " + config_json["ip"])
logger.info("Got hostname  : " + config_json["hostname"])
logger.info("Got devtype  : "  + config_json["devtype"])

if not reconfigure and checkIpAlreadyConfigured(config_json["ip"]) and checkHostnameAlreadyConfigured(config_json["hostname"]):
	logger.info("Hostname and IP are already configured")
	sys.exit(0)

downloadPostScripts()

executePostScripts(config_json["ip"], config_json["hostname"], reconfigure, config_json["devtype"])
