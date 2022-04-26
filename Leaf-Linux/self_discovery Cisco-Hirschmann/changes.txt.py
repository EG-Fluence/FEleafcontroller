
# ========================================  leaf_selfdiscovery.py


	def checkCisco(self,json_str, eastWest):
		jsonData = json.loads(json_str)
		lldp_obj = jsonData["lldp"]
		if isinstance(lldp_obj["interface"], list):
			for interface_item in lldp_obj["interface"]:
				if eastWest in interface_item:
					chassis = interface_item[eastWest]["chassis"]
					for key, value in chassis.items():
						if "cisco" in value["descr"].lower():
							return True
						if "hirschmann" in value["descr"].lower():
							return True
		else:
			if eastWest in lldp_obj["interface"]:
				chassis = lldp_obj["interface"][eastWest]["chassis"]
				for key, value in chassis.items():
					if "cisco" in value["descr"].lower():
						return True
					if "hirschmann" in value["descr"].lower():
						return True


# ======================================  leaf_selfconfiguration.py


def getCiscoInterfaceName(lldp_result, eastWest):
	jsonData = json.loads(lldp_result)
	lldp_obj = jsonData["lldp"]
	if isinstance(lldp_obj["interface"], list):
		for interface_item in lldp_obj["interface"]:
			if eastWest in interface_item:
				chassis = interface_item[eastWest]["chassis"]
				for key, value in chassis.items():
					if "cisco" in value["descr"].lower():
						name = interface_item[eastWest]["port"]["id"]["value"]   	<<<---
						return name
					if "hirschmann" in value["descr"].lower():
						name = interface_item[eastWest]["port"]["descr"]			<<<---
						return name
	else:
		if eastWest in lldp_obj["interface"]:
			chassis = lldp_obj["interface"][eastWest]["chassis"]
			for key, value in chassis.items():
				if "cisco" in value["descr"].lower():
					name = lldp_obj["interface"][eastWest]["port"]["id"]["value"]	<<<---
					return name
				if "hirschmann" in value["descr"].lower():
					name = lldp_obj["interface"][eastWest]["port"]["descr"]			<<<---
					return name



# ======================================  leaf_selfconfiguration.py


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



# ===============================================================================


leaf_uuid: 90ae2bef894a5c7283297f086238c2e3
leaf_position: 1
switch_name: enst22-25sw01-pr
switch_interface_name: ec:e5:55:a8:49:b9
side: west


=====================================================
2022-04-22:09:13:23,440 DEBUG    [leaf_selfconfiguration.py:227] {'leaf_uuid': '90ae2bef894a5c7283297f086238c2e3', 'leaf_position': 1, 'switch_name': 'enst22-25sw01-pr', 'switch_interface_west': 'ec:e5:55:a8:49:b9'}
2022-04-22:09:13:23,469 DEBUG    [connectionpool.py:208] Starting new HTTP connection (1): 10.0.0.3
2022-04-22:09:13:23,491 DEBUG    [connectionpool.py:396] http://10.0.0.3:5000 "POST /config HTTP/1.1" 204 0
!!! status=204

2022-04-22:09:13:23,497 DEBUG    [leaf_selfconfiguration.py:240] ConfServer returned : {}
2022-04-22:09:13:23,499 INFO     [leaf_selfconfiguration.py:392] Got west side config



======================

Hirschmann RSPE35:

Interface: wan, via: LLDP, RID: 1, Time: 1 day, 07:30:00
Chassis:
ChassisID: mac ec:e5:55:a8:49:b0
SysName: enst22-25sw01-pr
SysDescr: Hirschmann Rail Switch Power Enhanced - SW: HiOS-3S-09.0.00
MgmtIP: 10.0.0.4
Capability: Bridge, on
Capability: Router, off
Port:
PortID: mac ec:e5:55:a8:49:b9
PortDescr: Module: 1 Port: 5 - 10/100 Mbit TX
TTL: 120
Unknown TLVs:
TLV: OUI: 00,80,63, SubType: 2, Len: 1 01
TLV: OUI: 00,80,63, SubType: 3, Len: 1 05
TLV: OUI: 00,80,63, SubType: 4, Len: 6 0C,80,80,44,00,00

 

 

Cisco 2520:

Interface: lan3, via: CDPv2, RID: 198, Time: 3 days, 23:01:34
Chassis:
ChassisID: local enst89-25sw01-pr.fluence.com
SysName: enst89-25sw01-pr.fluence.com
SysDescr: cisco CGS-2520-24TC running on
Cisco IOS Software, CGS2520 Software (CGS2520-LANBASEK9-M), Version 15.2(7)E2, RELEASE SOFTWARE (fc3)
Technical Support: http://www.cisco.com/techsupport
Copyright (c) 1986-2020 by Cisco Systems, Inc.
Compiled Sun 15-Mar-20 06:42 by prod_rel_team
MgmtIP: 10.194.140.134
Capability: Bridge, on
Port:
PortID: ifname FastEthernet0/4
PortDescr: FastEthernet0/4
TTL: 180