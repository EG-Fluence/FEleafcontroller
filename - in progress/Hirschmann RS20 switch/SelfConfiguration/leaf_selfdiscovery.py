
import os
import fluencelog
import json
import configparser
import decimal
import time
from enum import Enum

logger = fluencelog.initlog()
#config = configparser.ConfigParser()
#config.read(configBaseDir + '/leaf_selfdiscovery.cfg')
#print(config.sections())

import subprocess

class SelfDiscovery:
	config = None
	baseDir = ""

	def __init__(self, config, baseDir):
		self.config = config
		self.baseDir = baseDir

	class State(Enum):
		TopolygyRingClosed = 1
		TopolygyRingOpen = 2
		TopologyIsland = 3

	stableLldpCliResult = ""

	def lldpcli_result(self):
		if os.getenv('TESTING', "false") == "true":
			# for testing with fake lldpcli
			result = subprocess.run([self.baseDir + '/lldpcli_fake'], stdout=subprocess.PIPE)
		else:
			result = subprocess.run(['lldpcli', '-f', 'json', 'sh', 'nei'], stdout=subprocess.PIPE)
		return result.stdout.decode('utf-8')

	# Lan3 side
	def getWestSideInterfaces(self,json_str):
		jsonData = json.loads(json_str)
		lldp_obj = jsonData["lldp"]
		count = 0
		for interface_item in lldp_obj["interface"]:
			if "wan" in interface_item or "eth0" in interface_item:
				count += 1
		logger.info("Found west side (wan) interfaces : " + str(count))
		return count

	# Wan Side
	def getEastSideInterfaces(self,json_str):
		jsonData = json.loads(json_str)
		lldp_obj = jsonData["lldp"]
		count = 0
		for interface_item in lldp_obj["interface"]:
			if "lan3" in interface_item or "eth2" in interface_item:
				count += 1
		logger.info("Found east side (lan3) interfaces : " + str(count))
		return count

	# Cisco Ring is closed check
	def checkTopologyRingIsClosed(self,json_str):
		return self.checkCiscoEast(json_str) and self.checkCiscoWest(json_str)

	def noCiscoVisible(self,json_str):
		return not self.checkCiscoEast(json_str) and not self.checkCiscoWest(json_str)

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

		return False

	def checkCiscoWest(self,json_str):
		return self.checkCisco(json_str,"wan") or self.checkCisco(json_str,"eth0")

	def checkCiscoEast(self,json_str):
		return self.checkCisco(json_str,"lan3") or self.checkCisco(json_str,"eth2")

	def getStableLldpCliTopology(self):
		return self.stableLldpCliResult;

	def isTopologyStable(self):
		logger.info("Waiting for topology stabilization " + self.config["DEFAULT"]["StabilizeCount"] + " times with interval of " + self.config["DEFAULT"]["StabilizeSeconds"] + " seconds")

		currentCnt = 0
		cnt = decimal.Decimal(self.config["DEFAULT"]["StabilizeCount"])
		seconds = decimal.Decimal(self.config["DEFAULT"]["StabilizeSeconds"])

		lldp_call = self.lldpcli_result()
		east_count = self.getEastSideInterfaces(lldp_call)
		west_count = self.getWestSideInterfaces(lldp_call)
		isStable = False

		state = self.State.TopologyIsland

		if self.checkTopologyRingIsClosed(lldp_call) == False:
			if self.noCiscoVisible(lldp_call):
				state = self.State.TopologyIsland
			else:
				state = self.State.TopolygyRingOpen
		else:
			state = self.State.TopolygyRingClosed

		logger.debug("Current State: " + str(state))
		previousState = state

		while (not isStable):
			if os.path.exists("/home/fos/SelfConfiguration/.installrunning") == True:
				logger.info("An installation is currently running. Aborting selfconfig")
				sys.exit(42)

			logger.debug("Detecting the " + str(currentCnt) + " time ...")
			lldp_call = self.lldpcli_result()

			if not previousState == state:
					logger.debug("State changed to : " + str(state))
					previousState = state

			if state == self.State.TopolygyRingClosed:
				if self.checkTopologyRingIsClosed(lldp_call) == False:
					state = self.State.TopolygyRingOpen
					currentCnt = 0
					continue

				# even the ring is closed it may happen that
				# a leaf is joining or leaving (also valid for zombie
				# leafs which are currently reconfigured)
				tmp_east_count = self.getEastSideInterfaces(lldp_call)
				tmp_west_count = self.getWestSideInterfaces(lldp_call)

				if east_count != tmp_east_count:
					logger.debug("East count has changed, resetting StabilizeCount")
					currentCnt = 0
					east_count = tmp_east_count
					continue;

				if west_count != tmp_west_count:
					logger.debug("West count has changed, resetting StabilizeCount")
					currentCnt = 0
					west_count = tmp_west_count
					continue;

				currentCnt += 1

			elif state == self.State.TopolygyRingOpen:
				logger.warn("The topology ring is not closed, check cabling")
				if self.checkTopologyRingIsClosed(lldp_call) == True:
					state = self.State.TopolygyRingClosed
					currentCnt = 0
					continue

				if self.noCiscoVisible(lldp_call):
					state = self.State.TopolygyIsland
					currentCnt = 0
					continue

				# from here usual case that the ring is not closed
				# but a Cisco is seen on one side
				tmp_east_count = self.getEastSideInterfaces(lldp_call)
				tmp_west_count = self.getWestSideInterfaces(lldp_call)

				if east_count != tmp_east_count:
					logger.debug("East count has changed, resetting StabilizeCount")
					currentCnt = 0
					east_count = tmp_east_count
					continue;

				if west_count != tmp_west_count:
					logger.debug("West count has changed, resetting StabilizeCount")
					currentCnt = 0
					west_count = tmp_west_count
					continue;

				currentCnt += 1
			elif state == self.State.TopologyIsland:
				logger.warn("I do not see a Cisco on west (wan) and east (lan3) side. Could never configure myself ! Check cabling.")
				if not self.noCiscoVisible(lldp_call):
					state = self.State.TopolygyRingOpen
					currentCnt = 0
					continue
			else:
				logger.error("State unknown !")

			if currentCnt >= cnt:
				isStable = True
				logger.info("Detected topology as stable.")
			else:
				time.sleep(seconds)

		if isStable == True:
			self.stableLldpCliResult = lldp_call

		return isStable

