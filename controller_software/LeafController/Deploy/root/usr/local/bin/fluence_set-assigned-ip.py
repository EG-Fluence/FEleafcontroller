#!/usr/bin/env python3

import yaml
import sys

if len(sys.argv) != 2:
	sys.exit("Missing IP as argument")

with open("/etc/netplan/network.yaml","r") as f:
    netplan = yaml.load(f)

br0 = netplan["network"]["bridges"]["br0"]
br0["addresses"] = [ sys.argv[1] ]
# routing problem with gateway, copy paste error from another script
# br0["gateway4"] = "192.168.2.1"
# remove gateway4 if it was there
br0.pop("gateway4", None)

with open("/etc/netplan/network.yaml","w") as f:
    f.write(yaml.dump(netplan, default_flow_style=False))

