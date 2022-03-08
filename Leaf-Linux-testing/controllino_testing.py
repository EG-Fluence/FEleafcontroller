#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import time
from pymodbus.client.sync import ModbusTcpClient
import pymodbus.exceptions as pme
import datetime
import sys
from controllino_modbus_utils import controllino_modbus_utils as cmu_utils
import socket
import os

# ============== IMPORTANT SETTING ============== #
# All settings are stored in this class. Most important settings can be set here.
scriptSettings = cmu_utils.ModbusScriptSettings()
settingsDefault = cmu_utils.ModbusDeviceSettingsDefault()

# ============================== Definition of default values. ============================== #
# Set default values. Those are used if no arguments or inputs are given.

defaultConfigFilePath = "../Linux-Python/controllino_modbus.conf"

logging = True

# Get IP address of the device that the script is running on and set it
# if sys.platform.startswith('linux'):
#    bashcommand = 'hostname -I'
#    op, error = cmu_utils.executeBash(bashcommand)
#    hostnamelist = op.decode('iso-8859-1').split()
#    if len(hostnamelist) > 1:
#        serverIpAddressDefault = str(hostnamelist[1])
#    else:
#        serverIpAddressDefault = str(hostnamelist[0])
#
# elif sys.platform.startswith('win'):
#    serverIpAddressDefault = socket.gethostbyname(socket.gethostname())

serverIpAddressDefault = cmu_utils.getServerIpAddress()
if (not serverIpAddressDefault):
    sys.exit(os.EX_NOHOST)

serverPortDefault = 1502

hvac1ReadDefault = True
hvac2ReadDefault = True
dl10UnitIdDefault = 5
hvac1UnitIdDefault = 3
hvac2UnitIdDefault = 4

# ============================== Initialization of settings. ============================== #

# Initialize Parameters with default values
scriptSettings.server.ipAddress = serverIpAddressDefault
scriptSettings.server.port = serverPortDefault
scriptSettings.hvac1.use = hvac1ReadDefault
scriptSettings.hvac2.use = hvac2ReadDefault
scriptSettings.dl10.unitId = dl10UnitIdDefault
scriptSettings.hvac1.unitId = hvac1UnitIdDefault
scriptSettings.hvac2.UnitId = hvac2UnitIdDefault

# Overwrite default values with values from config file
path = os.path.join(os.getcwd(), 'controllino_modbus.conf')
ret = cmu_utils.readConfigFile(path, scriptSettings, settingsDefault)
if ret is not None:
    scriptSettings = ret

if os.path.isfile(defaultConfigFilePath):
    ret = cmu_utils.readConfigFile(defaultConfigFilePath, scriptSettings, settingsDefault)
    if ret is not None:
        scriptSettings = ret

setpoint_timestamp = int(time.time())
current_program = 'default'

# Testing Values

#Real Values
refrigStopPoint = 22
heatingStopPoint = 5

#Virtual Values
setpoint_counter = 10
testing_value1 = 1

# In this case, the client is the server of the modbus script.
client = ModbusTcpClient(str(scriptSettings.server.ipAddress), port = str(scriptSettings.server.port), timeout = 5)


# Testing: Reading/Writing entries

address = 0X000D
value = 60
if len(sys.argv) > 1:
    address = int(sys.argv[1])
    print(address)

if len(sys.argv) > 2:
    value = int(sys.argv[2])

# if scriptSettings.chiller1.use == True:
#     client.write_registers(address, value, unit=scriptSettings.chiller1.unitId)
#     result1 = client.read_holding_registers(address, 1, unit=scriptSettings.chiller1.unitId)

# if scriptSettings.controllino.use == True:
#     client.write_registers(address, value, unit=scriptSettings.controllino.unitId)
#     result1 = client.read_holding_registers(address, 1, unit=scriptSettings.controllino.unitId)

if scriptSettings.hvac1.use == True:
    ret = client.write_registers(address, value, unit=scriptSettings.hvac1.unitId)
    result1 = client.read_holding_registers(address, 1, unit=scriptSettings.hvac1.unitId)

    if (ret.function_code < 0x80):
        print("write was successful")
    else:
        print('\n # Write was unsuccessful, check Client with unit ID: ' + str(scriptSettings.hvac1.unitId) + ' #')
        print(ret)


if (result1.function_code < 0x80):
    retVal1 = result1.registers[0]
    print('Value = ', retVal1)
else:
    print('\n # Modbus Problem, read was unsuccessful. #')