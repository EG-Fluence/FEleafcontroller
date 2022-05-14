# -*- coding: utf-8 -*-
"""
Created on Tue Feb  2 14:05:11 2021

@author: Georg.Kordowich
"""

from controllino_modbus_utils.controllino_generic_modbus_device import ModbusDevice

'''
The following List contains all signal names, addresses and also contains dummy values for the dl10 client.
It is used to initialize the datastore for the values which are read via modbus.

Description of the columns
1 - Name
2 - Address
3 - not updated value
4 - ro or rw flag
5 - 'r' real device register, read from device  
    'v' virtual device register, will be used only inside Modbus server
'''
dl10IrRegisterList = [
    ['dl10Humid',                   0x0000, 0xFF, 'ro', 'r'], # Ratio: x100 %
    ['dl10TempC',                   0x0001, 0xFF, 'ro', 'r'], # Ratio: x100 degC
    ['dl10TempF',                   0x0002, 0xFF, 'ro', 'r'], # Ratio: x100 degF
    
    ['readAlarm',                   0x0003, 0xFF, 'ro', 'v'], # 
    ['readTimestamp0',              0x0004, 0xFF, 'ro', 'v'], # 
    ['readTimestamp1',              0x0005, 0xFF, 'ro', 'v'], # 
    ['readTimestamp2',              0x0006, 0xFF, 'ro', 'v'], # 
    ['readTimestamp3',              0x0007, 0xFF, 'ro', 'v'], # 
    ['heartbeat',                   0x0008, 0,    'ro', 'v'] # 
                                                    ]

class Dl10Device(ModbusDevice):
    def __init__(self, deviceSettings):
        super(Dl10Device, self).__init__(deviceSettings, 
               coRegisterList = None, 
               diRegisterList = None, 
               hrRegisterList = None, 
               irRegisterList = dl10IrRegisterList)
















