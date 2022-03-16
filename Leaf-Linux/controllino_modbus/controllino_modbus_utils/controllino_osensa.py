# -*- coding: utf-8 -*-
"""
Created on Tue Feb  2 14:05:10 2021

@author: Georg.Kordowich
"""

from controllino_modbus_utils.controllino_generic_modbus_device import ModbusDevice

'''
The following List contains all signal names, addresses and also contains dummy values for the osensa client.
It is used to initialize the datastore for the values which are read via modbus.

Description of the columns
1 - Name
2 - Address
3 - not updated value
4 - ro or rw flag
5 - 'r' real device register, read from device  
    'v' virtual device register, will be used only inside Modbus server
'''
osensaHrRegisterList = [
    ['osensaTemp1',                 0x0400, 0xFF, 'ro', 'r'], # Ratio: x10
    ['osensaTemp2',                 0x0401, 0xFF, 'ro', 'r'], # Ratio: x10
    ['osensaTemp3',                 0x0402, 0xFF, 'ro', 'r'], # Ratio: x10
    ['osensaTemp4',                 0x0403, 0xFF, 'ro', 'r'], # Ratio: x10
    ['osensaTemp5',                 0x0404, 0xFF, 'ro', 'r'], # Ratio: x10
    ['osensaTemp6',                 0x0405, 0xFF, 'ro', 'r'], # Ratio: x10
    ['osensaTemp7',                 0x0406, 0xFF, 'ro', 'r'], # Ratio: x10
    ['osensaTemp8',                 0x0407, 0xFF, 'ro', 'r'], # Ratio: x10
    
    ['readAlarm',                   0x0418, 0xFF, 'ro', 'v'], # 
    ['readTimestamp0',              0x0419, 0xFF, 'ro', 'v'], # 
    ['readTimestamp1',              0x041A, 0xFF, 'ro', 'v'], # 
    ['readTimestamp2',              0x041B, 0xFF, 'ro', 'v'], # 
    ['readTimestamp3',              0x041C, 0xFF, 'ro', 'v'], # 
    ['heartbeat',                   0x041D, 0,    'ro', 'v'] # 
                                                    ]

class OsensaDevice(ModbusDevice):
    def __init__(self, deviceSettings):
        super(OsensaDevice, self).__init__(deviceSettings, 
            coRegisterList = None, 
            diRegisterList = None, 
            hrRegisterList = osensaHrRegisterList, 
            irRegisterList = None)
























