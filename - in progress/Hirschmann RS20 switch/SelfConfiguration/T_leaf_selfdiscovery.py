

import fluencelog
import subprocess
import leaf_selfdiscovery as leaf_selfd

logger = fluencelog.initlog()

# --- main


lcsd = leaf_selfd.SelfDiscovery()

lcsd.hello()

f=open("testdata_leaf1.json", "r")
if f.mode == 'r':
	json_str = f.read()
	lcsd.getWestSideInterfaces(json_str)
	lcsd.getEastSideInterfaces(json_str)
	lcsd.checkTopologyRingIsClosed(json_str) and logger.debug("Ring closed")
f.close()

f=open("testdata_leaf2.json", "r")
if f.mode == 'r':
	json_str = f.read()
	lcsd.getWestSideInterfaces(json_str)
	lcsd.getEastSideInterfaces(json_str)
	lcsd.checkTopologyRingIsClosed(json_str)  and logger.debug("Ring closed")
f.close()

f=open("testdata_leaf3.json", "r")
if f.mode == 'r':
	json_str = f.read()
	lcsd.getWestSideInterfaces(json_str)
	lcsd.getEastSideInterfaces(json_str)
	lcsd.checkTopologyRingIsClosed(json_str)  and logger.debug("Ring closed")
f.close()

lcsd.isTopologyStable()
