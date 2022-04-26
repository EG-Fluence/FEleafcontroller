#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""

Created 21. 06. 2021.

@author: Boris Kajganic
"""
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

defaultConfigFilePath = "/usr/local/share/fluence/controllino_modbus.conf"

logging = True

serverIpAddressDefault = cmu_utils.getServerIpAddress()
if (not serverIpAddressDefault):
    sys.exit(os.EX_NOHOST)

serverPortDefault = 1502

upsTelcoReadDefault = True
upsTelcoUnitIdDefault = 192

# ============================== Initialization of settings. ============================== #

# Initialize Parameters with default values
scriptSettings.server.ipAddress = serverIpAddressDefault
scriptSettings.server.port = serverPortDefault
scriptSettings.upsTelco.use = upsTelcoReadDefault
scriptSettings.upsTelco.unitId = upsTelcoUnitIdDefault

# Overwrite default values with values from config file
path = os.path.join(os.getcwd(), 'controllino_modbus.conf')
ret = cmu_utils.readConfigFile(path, scriptSettings, settingsDefault)
if ret is not None:
    scriptSettings = ret

if os.path.isfile(defaultConfigFilePath):
    ret = cmu_utils.readConfigFile(defaultConfigFilePath, scriptSettings, settingsDefault)
    if ret is not None:
        scriptSettings = ret

# In this case, the client is the server of the modbus script.
#client = ModbusTcpClient(str(scriptSettings.server.ipAddress), port = str(scriptSettings.server.port), timeout = 5)
client = ModbusTcpClient(str(scriptSettings.upsTelco.ipAddress), port = str(scriptSettings.upsTelco.port), timeout = 5)
try:
    if not client.is_socket_open():
        client.connect()
except pme.ConnectionException as e:
    print(e)
    print()
    print('Connection to Client with unit ID: ' + str(scriptSettings.upsTelco.unitId) + ' impossible')
    sys.exit(os.os.EX_NOHOST)
    
# 0x007
#queue.put((deviceName, cmu_utils.functionCodeDict_write['hr'], upsTelcoDev.hrDataFrame.loc['setSignalingCodeDO2', 'address'], [0, 0], 0))
#['setSignalingCodeDO2',                     0x1044, 0xFF, 'rw', 'r'],
ret = client.write_registers(0x1044, [0, 0], unit = scriptSettings.upsTelco.unitId)        
if (ret.function_code < 0x80):
    #print('Write was successful')
    pass
else:
    print('\n # Write was unsuccessful, check Client with unit ID: ' + str(scriptSettings.upsTelco.unitId) + ' #')
    print(ret)
    sys.exit(os.EX_DATAERR)      
    
# 0x009
#queue.put((deviceName, cmu_utils.functionCodeDict_write['hr'], upsTelcoDev.hrDataFrame.loc['setFunctionCodeDI1', 'address'], [4], 0))
#['setFunctionCodeDI1',                      0x104A, 0xFF, 'rw', 'r'],
ret = client.write_registers(0x104A, [4], unit = scriptSettings.upsTelco.unitId)  
if (ret.function_code < 0x80):
    #print('Write was successful')
    pass
else:
    print('\n # Write was unsuccessful, check Client with unit ID: ' + str(scriptSettings.upsTelco.unitId) + ' #')
    print(ret)
    sys.exit(os.EX_DATAERR)

# Toggle bit 1 of register On by using bitwise or
# 0x005
#valueLo = upsTelcoDev.hrDataFrame.loc['setParameters', 'value']
#valueHi = upsTelcoDev.hrDataFrame.loc['setParameters_REG2', 'value']
#['setParameters',                           0x1040, 0xFF, 'rw', 'r'],
#['setParameters_REG2',                      0x1041, 0xFF, 'rw', 'r'],
ret = client.read_holding_registers(0x1040, 1, unit = scriptSettings.upsTelco.unitId)
if (ret.function_code < 0x80):
    valueLo = ret.registers[0]
    print('valueLo = ', valueLo)
else:
    print('\n # Write was unsuccessful, check Client with unit ID: ' + str(scriptSettings.upsTelco.unitId) + ' #')
    print(ret)
    sys.exit(os.EX_DATAERR)
    
ret = client.read_holding_registers(0x1041, 1, unit = scriptSettings.upsTelco.unitId)
if (ret.function_code < 0x80):
    valueHi = ret.registers[0]
    print('valueHi = ', valueHi)
else:
    print('\n # Write was unsuccessful, check Client with unit ID: ' + str(scriptSettings.upsTelco.unitId) + ' #')
    print(ret)
    sys.exit(os.EX_DATAERR)
    
value = (valueHi << 16) | valueLo
value = value | 2 
valueLo = 0xFFFF & value
valueHi = value >> 16   

#queue.put((deviceName, cmu_utils.functionCodeDict_write['hr'], upsTelcoDev.hrDataFrame.loc['setParameters', 'address'], [valueLo, valueHi], 0))
ret = client.write_registers(0x1040, [valueLo, valueHi],  unit = scriptSettings.upsTelco.unitId)  
if (ret.function_code < 0x80):
    #print('Write was successful')
    pass
else:
    print('\n # Write was unsuccessful, check Client with unit ID: ' + str(scriptSettings.upsTelco.unitId) + ' #')
    print(ret)
    sys.exit(os.EX_DATAERR)
    
# Toggle bit 1 of register Off by using bitwise And
value = value & 0xFFFFFFFD
valueLo = 0xFFFF & value
valueHi = value >> 16           

time.sleep(3)
#queue.put((deviceName, cmu_utils.functionCodeDict_write['hr'], upsTelcoDev.hrDataFrame.loc['setParameters', 'address'], [valueLo, valueHi], 3)
ret = client.write_registers(0x1040, [valueLo, valueHi],  unit = scriptSettings.upsTelco.unitId)  
if (ret.function_code < 0x80):
    #print('Write was successful')
    pass
else:
    print('\n # Write was unsuccessful, check Client with unit ID: ' + str(scriptSettings.upsTelco.unitId) + ' #')
    print(ret)
    sys.exit(os.EX_DATAERR)
    
client.close()
print("Writing commands committed")
