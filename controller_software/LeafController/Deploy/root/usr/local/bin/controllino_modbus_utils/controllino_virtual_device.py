# -*- coding: utf-8 -*-
"""
Created on Wed Feb  3 10:07:29 2021

@author: Georg.Kordowich
"""

from controllino_modbus_utils.controllino_generic_modbus_device import ModbusDevice
#import controllino_modbus_utils as cmu_utils
#from controllino_modbus_utils.controllino_controllino import ControllinoDevice
#from controllino_modbus_utils.controllino_chiller import ChillerDevice
#from controllino_modbus_utils.controllino_hvac import HvacDevice
#from controllino_modbus_utils.controllino_osensa import OsensaDevice
#from controllino_modbus_utils.controllino_dl10 import Dl10Device
#from controllino_modbus_utils.controllino_flowmeter import FlowmeterDevice
#from controllino_modbus_utils.controllino_ups import UpsDevice



#TODO: Where the class virtualDevice is used ?

class virtualDevice(ModbusDevice):
    def __init__(self, deviceSettings):
        super.__init__(self, deviceSettings, 
               coRegisterList = None, 
               diRegisterList = None, 
               hrRegisterList = None, 
               irRegisterList = None)


















































