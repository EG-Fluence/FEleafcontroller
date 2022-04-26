#!/usr/bin/env python3

import yaml

with open("/etc/netplan/network.yaml","r") as f:
    netplan = yaml.load(f)

br1 = netplan["network"]["bridges"]["br1"]
br1["addresses"] = [ "192.168.2.1/24", "192.168.2.2/24" ]
# routing problem with gateway, added as additional address
# br1["gateway4"] = "192.168.2.1"
# remove gateway4 if it was there
br1.pop("gateway4", None)

with open("/etc/netplan/network.yaml","w") as f:
    f.write(yaml.dump(netplan, default_flow_style=False))

