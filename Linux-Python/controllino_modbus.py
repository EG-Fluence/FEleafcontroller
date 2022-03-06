#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Thu Aug 27 09:20:02 2020

@author: Georg.Kordowich
"""
#import ipaddress

from controllino_modbus_utils import controllino_modbus_utils as cmu_utils

from pymodbus.client.sync import ModbusTcpClient
from controllino_modbus_utils.controllino_controllino import ControllinoDevice
from controllino_modbus_utils.controllino_chiller import ChillerDevice
from controllino_modbus_utils.controllino_hvac import HvacDevice
from controllino_modbus_utils.controllino_osensa import OsensaDevice
from controllino_modbus_utils.controllino_dl10 import Dl10Device
from controllino_modbus_utils.controllino_flowmeter import FlowmeterDevice
from controllino_modbus_utils.controllino_ups import UpsDevice

from controllino_modbus_utils.controllino_hvacTelco import HvacTelcoDevice
from controllino_modbus_utils.controllino_upsTelco import UpsTelcoDevice

import pandas as pd
import sys, os
import time
import pymodbus.exceptions as pme
import random
from pymodbus.server.asynchronous import StartTcpServer
from pymodbus.device import ModbusDeviceIdentification
from pymodbus.datastore import ModbusSequentialDataBlock
from pymodbus.datastore import ModbusSparseDataBlock
from pymodbus.datastore import ModbusSlaveContext, ModbusServerContext
import logging
from twisted.internet import reactor
import twisted
import socket
import numpy as np
#from copy import copy
import copy

# this looks weird, because of the uppercase and lowercase q, but is necessary like that.
from multiprocessing import Queue
from queue import Empty

# Initialize Queue for write Requests
queue = Queue()
global devDict

      
#def openClient(mdsettings, timeoutsetting):
#def openClient(mdsettings):
#   
#   if mdsettings.ipAddress is not None and mdsettings.port is not None and len(mdsettings.ipAddress.split('.')) == 4:
#        mdsettings.client = ModbusTcpClient(mdsettings.ipAddress), port = str(mdsettings.port), timeout = settings.MODBUS_TIMEOUT)    

    
'''
The following function will initialize the clients for the devices. If Ip-Address or Port are 'None', the client will also stay
'None', and the default client will be used.
'''
def clientsInit(settings):
    
    # default client for all connection
    settings.clients.client = ModbusTcpClient(str(settings.clients.ipAddress), port = str(settings.clients.port), timeout = settings.MODBUS_TIMEOUT)

    # IP-Addresses
    if settings.controllino.ipAddress != None and settings.controllino.port != None and len(settings.controllino.ipAddress.split('.')) == 4:
        settings.controllino.client = ModbusTcpClient(str(settings.controllino.ipAddress), port = str(settings.controllino.port), timeout = settings.MODBUS_TIMEOUT)
    else:
        settings.controllino.client = settings.clients.client

    if settings.chiller1.ipAddress != None and settings.chiller1.port != None and len(settings.chiller1.ipAddress.split('.')) == 4:
        settings.chiller1.client = ModbusTcpClient(str(settings.chiller1.ipAddress), port = str(settings.chiller1.port), timeout = settings.MODBUS_TIMEOUT)
    else:
        settings.chiller1.client = settings.clients.client
        
    if settings.chiller2.ipAddress != None and settings.chiller2.port != None and len(settings.chiller2.ipAddress.split('.')) == 4:
        settings.chiller2.client = ModbusTcpClient(str(settings.chiller2.ipAddress), port = str(settings.chiller2.port), timeout = settings.MODBUS_TIMEOUT)
    else:
        settings.chiller2.client = settings.clients.client
        
    if settings.hvac1.ipAddress != None and settings.hvac1.port != None and len(settings.hvac1.ipAddress.split('.')) == 4:
        settings.hvac1.client = ModbusTcpClient(str(settings.hvac1.ipAddress), port = str(settings.hvac1.port), timeout = settings.MODBUS_TIMEOUT)
    else:
        settings.hvac1.client = settings.clients.client
        
    if settings.hvac2.ipAddress != None and settings.hvac2.port != None and len(settings.hvac2.ipAddress.split('.')) == 4:
        settings.hvac2.client = ModbusTcpClient(str(settings.hvac2.ipAddress), port = str(settings.hvac2.port), timeout = settings.MODBUS_TIMEOUT)
    else:
        settings.hvac2.client = settings.clients.client
        
    if settings.osensa.ipAddress != None and settings.osensa.port != None and len(settings.osensa.ipAddress.split('.')) == 4:
        settings.osensa.client = ModbusTcpClient(str(settings.osensa.ipAddress), port = str(settings.osensa.port), timeout = settings.MODBUS_TIMEOUT)
    else:
        settings.osensa.client = settings.clients.client
        
    if settings.dl10.ipAddress != None and settings.dl10.port != None and len(settings.dl10.ipAddress.split('.')) == 4:
        settings.dl10.client = ModbusTcpClient(str(settings.dl10.ipAddress), port = str(settings.dl10.port), timeout = settings.MODBUS_TIMEOUT)
    else:
        settings.dl10.client = settings.clients.client
        
    if settings.flowmeter1.ipAddress != None and settings.flowmeter1.port != None and len(settings.flowmeter1.ipAddress.split('.')) == 4:
        settings.flowmeter1.client = ModbusTcpClient(str(settings.flowmeter1.ipAddress), port = str(settings.flowmeter1.port), timeout = settings.MODBUS_TIMEOUT)
    else:
        settings.flowmeter1.client = settings.clients.client
        
    if settings.ups.ipAddress != None and settings.ups.port != None and len(settings.ups.ipAddress.split('.')) == 4:
        settings.ups.client = ModbusTcpClient(str(settings.ups.ipAddress), port = str(settings.ups.port), timeout = settings.MODBUS_TIMEOUT)
    else:
        settings.ups.client = settings.clients.client

    if settings.flowmeter2.ipAddress != None and settings.flowmeter2.port != None and len(settings.flowmeter2.ipAddress.split('.')) == 4:
        settings.flowmeter2.client = ModbusTcpClient(str(settings.flowmeter2.ipAddress), port                = str(settings.flowmeter2.port), timeout = settings.MODBUS_TIMEOUT)
    else:
        settings.flowmeter2.client = settings.clients.client
        
    if settings.hvacTelco.ipAddress != None and settings.hvacTelco.port != None and len(settings.hvacTelco.ipAddress.split('.')) == 4:
        settings.hvacTelco.client = ModbusTcpClient(str(settings.hvacTelco.ipAddress), port = str(settings.hvacTelco.port), timeout = settings.MODBUS_TIMEOUT)
    else:
        settings.hvacTelco.client = settings.clients.client
        
    if settings.upsTelco.ipAddress != None and settings.upsTelco.port != None and len(settings.upsTelco.ipAddress.split('.')) == 4:
        settings.upsTelco.client = ModbusTcpClient(str(settings.upsTelco.ipAddress), port = str(settings.upsTelco.port), timeout = settings.MODBUS_TIMEOUT)
    else:
        settings.upsTelco.client = settings.clients.client
        
    if settings.virtualSlave.ipAddress != None and settings.virtualSlave.port != None and len(settings.virtualSlave.ipAddress.split('.')) == 4:
        settings.virtualSlave.client = ModbusTcpClient(str(settings.virtualSlave.ipAddress), port = str(settings.virtualSlave.port), timeout = settings.MODBUS_TIMEOUT)
    else:
        settings.virtualSlave.client = settings.clients.client        
        
    global controllinoDev
    global chillerDev1
    global chillerDev2
    global hvacDev1
    global hvacDev2
    global osensaDev
    global dl10Dev
    global flowmeterDev1
    global flowmeterDev2
    global upsDev
    global hvacTelcoDev
    global upsTelcoDev    
    
    controllinoDev = ControllinoDevice(settings.controllino)
    chillerDev1    = ChillerDevice(settings.chiller1)
    chillerDev2    = ChillerDevice(settings.chiller2)
    hvacDev1       = HvacDevice(settings.hvac1)
    hvacDev2       = HvacDevice(settings.hvac2)
    osensaDev      = OsensaDevice(settings.osensa)
    dl10Dev        = Dl10Device(settings.dl10)
    flowmeterDev1  = FlowmeterDevice(settings.flowmeter1)
    flowmeterDev2  = FlowmeterDevice(settings.flowmeter2)
    upsDev         = UpsDevice(settings.ups)
    
    hvacTelcoDev   = HvacTelcoDevice(settings.hvacTelco)
    upsTelcoDev    = UpsTelcoDevice(settings.upsTelco)
    
    
'''
The following function will initialize the server part. 
'''
def serverInit(settings): 
    global controllinoDev
    global chillerDev1
    global chillerDev2
    global hvacDev1
    global hvacDev2
    global osensaDev
    global dl10Dev
    global flowmeterDev1
    global flowmeterDev2
    global upsDev
    global hvacTelcoDev
    global upsTelcoDev    
           
    global virtualSlaveDF
    '''
    The following List contains addresses and unit-ids that are necessary to map write requests from the virtual slave device
    to the real slave device. The values are not used right now, but are prepared for later usage. This register list does not have
    a writeaccess column, as it is only used for mapping. The write access will be checked with the register list of the real device.
    '''
    virtualSlaveRegisterList = [
        ['vdContDoor1',                  controllinoDev.irDataFrame.loc['door1',                   'address'], settings.controllino, 0xFF, 'ro'], # '''00'''
        ['vdContDoor2',                  controllinoDev.irDataFrame.loc['door2',                   'address'], settings.controllino, 0xFF, 'ro'], # '''01'''
        ['vdContReserved1',              controllinoDev.irDataFrame.loc['reserved1',               'address'], settings.controllino, 0xFF, 'ro'], # '''02'''
        ['vdContReserved2',              controllinoDev.irDataFrame.loc['CoreIMD',                 'address'], settings.controllino, 0xFF, 'ro'], # '''03'''
        ['vdContReserved3',              controllinoDev.irDataFrame.loc['fStopButton',             'address'], settings.controllino, 0xFF, 'ro'], # '''04'''
        ['vdContReserved4',              controllinoDev.irDataFrame.loc['bmsFstopButton',          'address'], settings.controllino, 0xFF, 'ro'], # '''05'''
        ['vdContReserved5',              controllinoDev.irDataFrame.loc['preFirePanelSignalPoint', 'address'], settings.controllino, 0xFF, 'ro'], # '''06'''
        ['vdContReserved6',              controllinoDev.irDataFrame.loc['mainFstop',               'address'], settings.controllino, 0xFF, 'ro'], # '''07'''
        ['vdContFstopK1Relay',           controllinoDev.irDataFrame.loc['fstopK1Relay',            'address'], settings.controllino, 0xFF, 'ro'], # '''08'''
        ['vdContFstopPushButton',        controllinoDev.irDataFrame.loc['fstopPushButton',         'address'], settings.controllino, 0xFF, 'ro'], # '''09'''
        ['vdContLeakageSens',            controllinoDev.irDataFrame.loc['leakageSens',             'address'], settings.controllino, 0xFF, 'ro'], # '''10'''
        ['vdContPowerSupp',              controllinoDev.irDataFrame.loc['powerSupp',               'address'], settings.controllino, 0xFF, 'ro'], # '''11'''
        ['vdContReserved7',              controllinoDev.irDataFrame.loc['spf',                     'address'], settings.controllino, 0xFF, 'ro'], # '''12'''
        ['vdContReserved8',              controllinoDev.irDataFrame.loc['ups',                     'address'], settings.controllino, 0xFF, 'ro'], # '''13'''
        ['vdContReserved9',              controllinoDev.irDataFrame.loc['hvac',                    'address'], settings.controllino, 0xFF, 'ro'], # '''14'''
        ['vdContReserved10',             controllinoDev.irDataFrame.loc['mvTransPress',            'address'], settings.controllino, 0xFF, 'ro'], # '''15'''
        ['vdContSpare1',                 controllinoDev.irDataFrame.loc['mvTransOilLevel',         'address'], settings.controllino, 0xFF, 'ro'], # '''16'''
        ['vdContSpare2',                 controllinoDev.irDataFrame.loc['mvTransOilTemp',          'address'], settings.controllino, 0xFF, 'ro'], # '''17'''
        ['vdContSpare3',                 controllinoDev.irDataFrame.loc['mvTransWindingTemp',      'address'], settings.controllino, 0xFF, 'ro'], # '''18'''
        ['vdContTempSens1',              controllinoDev.irDataFrame.loc['tempSens1',               'address'], settings.controllino, 0xFF, 'ro'], # '''19'''
        ['vdContTempSens2',              controllinoDev.irDataFrame.loc['tempSens2',               'address'], settings.controllino, 0xFF, 'ro'], # '''20'''
        ['vdContTempSens3',              controllinoDev.irDataFrame.loc['tempSens3',               'address'], settings.controllino, 0xFF, 'ro'], # '''21'''
        ['vdContReserved11',             controllinoDev.irDataFrame.loc['reserved11',              'address'], settings.controllino, 0xFF, 'ro'], # '''22'''
        ['vdContReserved12',             controllinoDev.irDataFrame.loc['reserved12',              'address'], settings.controllino, 0xFF, 'ro'], # '''23'''
        ['vdContReserved13',             controllinoDev.irDataFrame.loc['reserved13',              'address'], settings.controllino, 0xFF, 'ro'], # '''24'''
        ['vdContHeartbeat',              controllinoDev.irDataFrame.loc['heartbeat',               'address'], settings.controllino, 0xFF, 'ro'], # '''25'''
        
        ['vdCh1PressLow',                chillerDev1.hrDataFrame.loc['WaterReplenishmentAlarm', 'address'],    settings.chiller1,    0xFF, 'ro'], # '''26'''
        ['vdCh1OutletWaterTempSensFail', chillerDev1.hrDataFrame.loc['outletWaterTempSensFail', 'address'],    settings.chiller1,    0xFF, 'ro'], # '''27'''
        ['vdCh1waterTempSet',            chillerDev1.hrDataFrame.loc['waterTempSet',            'address'],    settings.chiller1,    0xFF, 'rw'], # '''28'''
        ['vdCh1hysteresisSet',           chillerDev1.hrDataFrame.loc['hysteresisSet',           'address'],    settings.chiller1,    0xFF, 'rw'], # '''29'''
        ['vdCh1pumpCommandSpeed',        chillerDev1.hrDataFrame.loc['pumpCommandSpeed',        'address'],    settings.chiller1,    0xFF, 'ro'], # '''30'''
        ['vdCh1heartbeat',               chillerDev1.hrDataFrame.loc['heartbeat',               'address'],    settings.chiller1,    0xFF, 'ro'], # '''31'''
        ['vdCh1systemOnOff',             chillerDev1.hrDataFrame.loc['systemOnOff',             'address'],    settings.chiller1,    0xFF, 'rw'], # '''32'''
        
        ['vdCh2PressLow',                chillerDev2.hrDataFrame.loc['WaterReplenishmentAlarm', 'address'],    settings.chiller2,    0xFF, 'ro'], # '''33'''
        ['vdCh2OutletWaterTempSensFail', chillerDev2.hrDataFrame.loc['outletWaterTempSensFail', 'address'],    settings.chiller2,    0xFF, 'ro'], # '''34'''
        ['vdCh2waterTempSet',            chillerDev2.hrDataFrame.loc['waterTempSet',            'address'],    settings.chiller2,    0xFF, 'rw'], # '''35'''
        ['vdCh2hysteresisSet',           chillerDev2.hrDataFrame.loc['hysteresisSet',           'address'],    settings.chiller2,    0xFF, 'rw'], # '''36'''
        ['vdCh2pumpCommandSpeed',        chillerDev2.hrDataFrame.loc['pumpCommandSpeed',        'address'],    settings.chiller2,    0xFF, 'ro'], # '''37'''
        ['vdCh2heartbeat',               chillerDev2.hrDataFrame.loc['heartbeat',               'address'],    settings.chiller2,    0xFF, 'ro'], # '''38'''
        ['vdCh2systemOnOff',             chillerDev2.hrDataFrame.loc['systemOnOff',             'address'],    settings.chiller2,    0xFF, 'rw'], # '''39'''
        
        ['vdHv1highTempAlarm',           hvacDev1.hrDataFrame.loc['highTempAlarm',     'address'],             settings.hvac1,       0xFF, 'ro'], # '''40'''
        ['vdHv1refrigStopPoint',         hvacDev1.hrDataFrame.loc['refrigStopPoint',   'address'],             settings.hvac1,       0xFF, 'rw'], # '''41'''
        ['vdHv1Heartbeat',               0,                                                                    settings.hvac1,       0xFF, 'ro'], # '''42''' # Heartbeat does not exist
        ['vdHv1unitRunningStatus',       hvacDev1.hrDataFrame.loc['unitRunningStatus', 'address'],             settings.hvac1,       0xFF, 'ro'], # '''43'''
        
        ['vdHv1highTempAlarm',           hvacDev2.hrDataFrame.loc['highTempAlarm',     'address'],             settings.hvac2,       0xFF, 'ro'], # '''44'''
        ['vdHv1refrigStopPoint',         hvacDev2.hrDataFrame.loc['refrigStopPoint',   'address'],             settings.hvac2,       0xFF, 'rw'], # '''45'''
        ['vdHv1Heartbeat',               0,                                                                    settings.hvac2,       0xFF, 'ro'], # '''46''' # Heartbeat does not exist
        ['vdHv1unitRunningStatus',       hvacDev2.hrDataFrame.loc['unitRunningStatus', 'address'],             settings.hvac2,       0xFF, 'ro'], # '''47'''
        
        ['vdOsTemp1',                    osensaDev.hrDataFrame.loc['osensaTemp1', 'address'],                  settings.osensa,      0xFF, 'ro'], # '''48'''
        ['vdOsTemp2',                    osensaDev.hrDataFrame.loc['osensaTemp2', 'address'],                  settings.osensa,      0xFF, 'ro'], # '''49'''
        ['vdOsTemp3',                    osensaDev.hrDataFrame.loc['osensaTemp3', 'address'],                  settings.osensa,      0xFF, 'ro'], # '''50'''
        ['vdOsTemp4',                    osensaDev.hrDataFrame.loc['osensaTemp4', 'address'],                  settings.osensa,      0xFF, 'ro'], # '''51'''
        ['vdOsTemp5',                    osensaDev.hrDataFrame.loc['osensaTemp5', 'address'],                  settings.osensa,      0xFF, 'ro'], # '''52'''
        ['vdOsTemp6',                    osensaDev.hrDataFrame.loc['osensaTemp6', 'address'],                  settings.osensa,      0xFF, 'ro'], # '''53'''
        ['vdOsTemp7',                    osensaDev.hrDataFrame.loc['osensaTemp7', 'address'],                  settings.osensa,      0xFF, 'ro'], # '''54'''
        ['vdOsTemp8',                    osensaDev.hrDataFrame.loc['osensaTemp8', 'address'],                  settings.osensa,      0xFF, 'ro'], # '''55'''
        
        ['vdDlTemp',                     dl10Dev.irDataFrame.loc['dl10TempC', 'address'],                      settings.dl10,        0xFF, 'ro'], # '''56'''
        
        ['vdAnyChillerWarning',          0xFF,                                                                 0xFF,                 0xFF, 'ro'], # '''57'''
        ['vdAnyChillerAlarm',            0xFF,                                                                 0xFF,                 0xFF, 'ro'], # '''58'''

        ['vdOsHighTempWarning',          0xFF,                                                                 0xFF,                 0xFF, 'ro'], # '''59'''
        ['vdOsHighTempAlarm',            0xFF,                                                                 0xFF,                 0xFF, 'ro'], # '''60'''
        
        ['vdUpsPowerCycle',              0xFF,                                                                 0xFF,                 0,    'rw'], # '''61'''
        
        ['readTimestamp0',               0xFF,                                                                 0xFF,                 0xFF, 'ro'], # '''62'''
        ['readTimestamp1',               0xFF,                                                                 0xFF,                 0xFF, 'ro'], # '''63'''
        ['readTimestamp2',               0xFF,                                                                 0xFF,                 0xFF, 'ro'], # '''64'''
        ['readTimestamp3',               0xFF,                                                                 0xFF,                 0xFF, 'ro'], # '''65'''
        ['heartbeat',                    0xFF,                                                                 0xFF,                 0,    'ro'], # '''66'''
                ]
    
    virtualSlaveDF = pd.DataFrame(data = virtualSlaveRegisterList, columns = ['signal_name', 'address', 'deviceSettings', 'value', 'writeaccess'])
    virtualSlaveDF = virtualSlaveDF.set_index('signal_name')    
        
'''
The following function will initialize the devices. 
Especially the dataframes in which the the values read from the devices are defined in the init function of the classes.
All modbus device classes (like osensaDevice etc.) are child classes of the class genericModbusDevice.
'''
def devicesInit(settings):
    
    clientsInit(settings)
    serverInit(settings)
    
    
    # DevDict must be defined here, because the Unit ID can change while reading the config file.
    # It is used to "Translate" the device to be written or read from the string given by the user to the necessary values.
    global devDict
    
    devDict = {
        'controllino'    : [settings.controllino,  controllinoDev.hrDataFrame, 'hr'],
        'controllinoCoil': [settings.controllino,  controllinoDev.coDataFrame, 'co'],
        'chiller'        : [settings.chiller1,     chillerDev1.hrDataFrame,    'hr'],  # Alias for backwards compatibility.
        'chiller1'       : [settings.chiller1,     chillerDev1.hrDataFrame,    'hr'], 
        'chiller2'       : [settings.chiller2,     chillerDev2.hrDataFrame,    'hr'], 
        'hvac'           : [settings.hvac1,        hvacDev1.hrDataFrame,       'hr'],  # Alias for backwards compatibility.
        'hvac1'          : [settings.hvac1,        hvacDev1.hrDataFrame,       'hr'], 
        'hvac2'          : [settings.hvac2,        hvacDev2.hrDataFrame,       'hr'], 
        'osensa'         : [settings.osensa,       osensaDev.hrDataFrame,      'hr'], 
        'dl10'           : [settings.dl10,         dl10Dev.irDataFrame,        'ir'], 
        'flowmeter'      : [settings.flowmeter1,   flowmeterDev1.hrDataFrame,  'hr'], # Alias for backwards compatibility.
        'flowmeter1'     : [settings.flowmeter1,   flowmeterDev1.hrDataFrame,  'hr'], 
        'ups'            : [settings.ups,          upsDev.hrDataFrame,         'hr'],
        'flowmeter2'     : [settings.flowmeter2,   flowmeterDev2.hrDataFrame,  'hr'], 
        'virtualslave'   : [settings.virtualSlave, virtualSlaveDF,             'hr'], 
        'upstelco'       : [settings.upsTelco,     upsTelcoDev.hrDataFrame,    'hr'],
        'hvactelco'      : [settings.hvacTelco,    hvacTelcoDev.hrDataFrame,   'hr'],
        'hvactelcoDi'    : [settings.hvacTelco,    hvacTelcoDev.diDataFrame,   'di'],
        'hvactelcoCoil'  : [settings.hvacTelco,    hvacTelcoDev.coDataFrame,   'co']
        }


'''
Function will summarize all important values in one list, so it can be read with one request.
'''
def getVirtualSlaveValues(settings, localDf):
    
    # print('getVirtualSlaveValues, copy form other devices')

    #This was correct but not anymore for some reason.
    #localDf.loc['vdContDoor1':'vdContHeartbeat', 'value'] = controllinoDev.irDataFrame.loc['door1':'heartbeat', 'value']
    localDf.loc['vdContDoor1', 'value']           = controllinoDev.irDataFrame.loc['door1', 'value']
    localDf.loc['vdContDoor2', 'value']           = controllinoDev.irDataFrame.loc['door2', 'value']
    localDf.loc['vdContReserved1', 'value']       = controllinoDev.irDataFrame.loc['reserved1', 'value']
    localDf.loc['vdContReserved2', 'value']       = controllinoDev.irDataFrame.loc['CoreIMD', 'value']
    localDf.loc['vdContReserved3', 'value']       = controllinoDev.irDataFrame.loc['fStopButton', 'value']
    localDf.loc['vdContReserved4', 'value']       = controllinoDev.irDataFrame.loc['bmsFstopButton', 'value']
    localDf.loc['vdContReserved5', 'value']       = controllinoDev.irDataFrame.loc['preFirePanelSignalPoint', 'value']
    localDf.loc['vdContReserved6', 'value']       = controllinoDev.irDataFrame.loc['mainFstop', 'value']
    localDf.loc['vdContFstopK1Relay', 'value']    = controllinoDev.irDataFrame.loc['fstopK1Relay', 'value']            
    localDf.loc['vdContFstopPushButton', 'value'] = controllinoDev.irDataFrame.loc['fstopPushButton', 'value']          
    localDf.loc['vdContLeakageSens', 'value']     = controllinoDev.irDataFrame.loc['leakageSens', 'value']             
    localDf.loc['vdContPowerSupp', 'value']       = controllinoDev.irDataFrame.loc['powerSupp', 'value']                 
    localDf.loc['vdContReserved7', 'value']       = controllinoDev.irDataFrame.loc['spf', 'value']                  
    localDf.loc['vdContReserved8', 'value']       = controllinoDev.irDataFrame.loc['ups', 'value']                  
    localDf.loc['vdContReserved9', 'value']       = controllinoDev.irDataFrame.loc['hvac', 'value']                   
    localDf.loc['vdContReserved10', 'value']      = controllinoDev.irDataFrame.loc['mvTransPress', 'value']             
    localDf.loc['vdContSpare1', 'value']          = controllinoDev.irDataFrame.loc['mvTransOilLevel', 'value']           
    localDf.loc['vdContSpare2', 'value']          = controllinoDev.irDataFrame.loc['mvTransOilTemp', 'value']                
    localDf.loc['vdContSpare3', 'value']          = controllinoDev.irDataFrame.loc['mvTransWindingTemp', 'value']                      
    localDf.loc['vdContTempSens1', 'value']       = controllinoDev.irDataFrame.loc['tempSens1', 'value']           
    localDf.loc['vdContTempSens2', 'value']       = controllinoDev.irDataFrame.loc['tempSens2', 'value']                
    localDf.loc['vdContTempSens3', 'value']       = controllinoDev.irDataFrame.loc['tempSens3', 'value']                 
    localDf.loc['vdContReserved11', 'value']      = controllinoDev.irDataFrame.loc['reserved11', 'value']                
    localDf.loc['vdContReserved12', 'value']      = controllinoDev.irDataFrame.loc['reserved12', 'value']           
    localDf.loc['vdContReserved13', 'value']      = controllinoDev.irDataFrame.loc['reserved13', 'value']     
    localDf.loc['vdContHeartbeat', 'value']       = controllinoDev.irDataFrame.loc['heartbeat', 'value']
    
    localDf.loc['vdCh1PressLow', 'value']                 = chillerDev1.hrDataFrame.loc['WaterReplenishmentAlarm', 'value']
    localDf.loc['vdCh1OutletWaterTempSensFail', 'value']  = 1 if chillerDev1.hrDataFrame.loc['outletWaterTempSensFail':'heatingFaultLock', 'value'].sum() > 1 else 0
    localDf.loc['vdCh1waterTempSet', 'value']             = chillerDev1.hrDataFrame.loc['waterTempSet', 'value']
    localDf.loc['vdCh1hysteresisSet', 'value']            = chillerDev1.hrDataFrame.loc['hysteresisSet', 'value']
    localDf.loc['vdCh1pumpCommandSpeed', 'value']         = chillerDev1.hrDataFrame.loc['pumpCommandSpeed', 'value']
    localDf.loc['vdCh1heartbeat', 'value']                = chillerDev1.hrDataFrame.loc['heartbeat', 'value']
    localDf.loc['vdCh1systemOnOff', 'value']              = chillerDev1.hrDataFrame.loc['systemOnOff', 'value']
    
    localDf.loc['vdCh2PressLow', 'value']                 = chillerDev2.hrDataFrame.loc['WaterReplenishmentAlarm', 'value']
    localDf.loc['vdCh2OutletWaterTempSensFail', 'value']  = 1 if chillerDev2.hrDataFrame.loc['outletWaterTempSensFail':'heatingFaultLock', 'value'].sum() > 1 else 0
    localDf.loc['vdCh2waterTempSet', 'value']             = chillerDev2.hrDataFrame.loc['waterTempSet', 'value']
    localDf.loc['vdCh2hysteresisSet', 'value']            = chillerDev2.hrDataFrame.loc['hysteresisSet', 'value']
    localDf.loc['vdCh2pumpCommandSpeed', 'value']         = chillerDev2.hrDataFrame.loc['pumpCommandSpeed', 'value']
    localDf.loc['vdCh2heartbeat', 'value']                = chillerDev2.hrDataFrame.loc['heartbeat', 'value']
    localDf.loc['vdCh2systemOnOff', 'value']              = chillerDev2.hrDataFrame.loc['systemOnOff', 'value']
    
    localDf.loc['vdHv1highTempAlarm', 'value']            = 1 if hvacDev1.hrDataFrame.loc['highTempAlarm':'dcUnderVoltageAlarm', 'value'].sum() > 1 else 0
    localDf.loc['vdHv1refrigStopPoint', 'value']          = hvacDev1.hrDataFrame.loc['refrigStopPoint', 'value']
    localDf.loc['vdHv1Heartbeat', 'value']                = 1  #TODO: heartbeat
    localDf.loc ['vdHv1unitRunningStatus', 'value']       = hvacDev1.hrDataFrame.loc['unitRunningStatus', 'value']
    
    localDf.loc['vdHv1highTempAlarm', 'value']            = 1 if hvacDev2.hrDataFrame.loc['highTempAlarm':'dcUnderVoltageAlarm', 'value'].sum() > 1 else 0
    localDf.loc['vdHv1refrigStopPoint', 'value']          = hvacDev2.hrDataFrame.loc['refrigStopPoint', 'value']
    localDf.loc['vdHv1Heartbeat', 'value']                = 1  #TODO: heartbeat
    localDf.loc['vdHv1unitRunningStatus', 'value']        = hvacDev2.hrDataFrame.loc['unitRunningStatus', 'value']
    
    #localDf.loc['vdOsTemp1':'vdOsTemp8', 'value']         = osensaDev.hrDataFrame.loc['osensaTemp1':'osensaTemp8', 'value']
    localDf.loc['vdOsTemp1', 'value']                     = osensaDev.hrDataFrame.loc['osensaTemp1', 'value']
    localDf.loc['vdOsTemp2', 'value']                     = osensaDev.hrDataFrame.loc['osensaTemp2', 'value']
    localDf.loc['vdOsTemp3', 'value']                     = osensaDev.hrDataFrame.loc['osensaTemp3', 'value']
    localDf.loc['vdOsTemp4', 'value']                     = osensaDev.hrDataFrame.loc['osensaTemp4', 'value']
    localDf.loc['vdOsTemp5', 'value']                     = osensaDev.hrDataFrame.loc['osensaTemp5', 'value']
    localDf.loc['vdOsTemp6', 'value']                     = osensaDev.hrDataFrame.loc['osensaTemp6', 'value']
    localDf.loc['vdOsTemp7', 'value']                     = osensaDev.hrDataFrame.loc['osensaTemp7', 'value']
    localDf.loc['vdOsTemp8', 'value']                     = osensaDev.hrDataFrame.loc['osensaTemp8', 'value']
     
    localDf.loc['vdDlTemp', 'value']                      = dl10Dev.irDataFrame.loc['dl10TempC', 'value']
    
    if (chillerDev1.hrDataFrame.loc['outletLowWaterTemp', 'value'] > 0 or
        chillerDev1.hrDataFrame.loc['pumpFail', 'value'] > 0 or
        chillerDev1.hrDataFrame.loc['inverterComFail', 'value'] > 0 or
        chillerDev1.hrDataFrame.loc['highSystemPressAlarm', 'value'] > 0 or
        chillerDev1.hrDataFrame.loc['sysHighVoltageLock', 'value'] > 0 or
        chillerDev1.hrDataFrame.loc['sysLowVoltageLock', 'value'] > 0 or
        chillerDev1.hrDataFrame.loc['exhaustGasHighTempLock', 'value'] > 0 or
        chillerDev1.hrDataFrame.loc['inverterOverCurrentLock', 'value'] > 0 or
        chillerDev1.hrDataFrame.loc['inverterOverTempLock', 'value'] > 0 or
        chillerDev1.hrDataFrame.loc['inverterOverVoltLock', 'value'] > 0 or
        chillerDev1.hrDataFrame.loc['inverterUnterVoltLock', 'value'] > 0 or
        chillerDev1.hrDataFrame.loc['inverterPhaseLossLock', 'value'] > 0 or
        chillerDev1.hrDataFrame.loc['inverterOtherFaultLock', 'value'] > 0 or
    
        chillerDev2.hrDataFrame.loc['outletLowWaterTemp', 'value'] > 0 or
        chillerDev2.hrDataFrame.loc['pumpFail', 'value'] > 0 or
        chillerDev2.hrDataFrame.loc['inverterComFail', 'value'] > 0 or
        chillerDev2.hrDataFrame.loc['highSystemPressAlarm', 'value'] > 0 or
        chillerDev2.hrDataFrame.loc['sysHighVoltageLock', 'value'] > 0 or
        chillerDev2.hrDataFrame.loc['sysLowVoltageLock', 'value'] > 0 or
        chillerDev2.hrDataFrame.loc['exhaustGasHighTempLock', 'value'] > 0 or
        chillerDev2.hrDataFrame.loc['inverterOverCurrentLock', 'value'] > 0 or
        chillerDev2.hrDataFrame.loc['inverterOverTempLock', 'value'] > 0 or
        chillerDev2.hrDataFrame.loc['inverterOverVoltLock', 'value'] > 0 or
        chillerDev2.hrDataFrame.loc['inverterUnterVoltLock', 'value'] > 0 or
        chillerDev2.hrDataFrame.loc['inverterPhaseLossLock', 'value'] > 0 or
        chillerDev2.hrDataFrame.loc['inverterOtherFaultLock', 'value'] > 0
        ):
    
        localDf.loc['vdAnyChillerAlarm', 'value'] = 1
    else:
        localDf.loc['vdAnyChillerAlarm', 'value'] = 0
    
    if ((settings.chiller1.use and 
         (chillerDev1.hrDataFrame.loc['outletHighWaterTemp', 'value'] > 0 or
          chillerDev1.hrDataFrame.loc['outletWaterTempSensFail', 'value'] > 0 or
          chillerDev1.hrDataFrame.loc['returnWaterTempSensFail', 'value'] > 0 or
          chillerDev1.hrDataFrame.loc['heatingFail', 'value'] > 0 or
          chillerDev1.hrDataFrame.loc['highOutletPressAlarm', 'value'] > 0 or
          chillerDev1.hrDataFrame.loc['heatingFaultLock', 'value'] > 0)) 
        or
        (settings.chiller2.use and 
         (chillerDev2.hrDataFrame.loc['outletHighWaterTemp', 'value'] > 0 or
          chillerDev2.hrDataFrame.loc['outletWaterTempSensFail', 'value'] > 0 or
          chillerDev2.hrDataFrame.loc['returnWaterTempSensFail', 'value'] > 0 or
          chillerDev2.hrDataFrame.loc['heatingFail', 'value'] > 0 or
          chillerDev2.hrDataFrame.loc['highOutletPressAlarm', 'value'] > 0 or
          chillerDev2.hrDataFrame.loc['heatingFaultLock', 'value'] > 0
        ))):
    
        localDf.loc['vdAnyChillerWarning', 'value'] = 1
    else:
        localDf.loc['vdAnyChillerWarning', 'value'] = 0
    
    if (osensaDev.hrDataFrame.loc['osensaTemp1', 'value']/10 > 92.5 or
        osensaDev.hrDataFrame.loc['osensaTemp2', 'value']/10 > 92.5 or
        osensaDev.hrDataFrame.loc['osensaTemp3', 'value']/10 > 92.5 or
        osensaDev.hrDataFrame.loc['osensaTemp4', 'value']/10 > 92.5 or
        osensaDev.hrDataFrame.loc['osensaTemp5', 'value']/10 > 92.5 or
        osensaDev.hrDataFrame.loc['osensaTemp6', 'value']/10 > 92.5 or
        osensaDev.hrDataFrame.loc['osensaTemp7', 'value']/10 > 92.5 or
        osensaDev.hrDataFrame.loc['osensaTemp8', 'value']/10 > 92.5
        ):
        localDf.loc['vdOsHighTempWarning', 'value'] = 1
    else:
        localDf.loc['vdOsHighTempWarning', 'value'] = 0
    
    
    if (osensaDev.hrDataFrame.loc['osensaTemp1', 'value']/10 > 95 or
        osensaDev.hrDataFrame.loc['osensaTemp2', 'value']/10 > 95 or
        osensaDev.hrDataFrame.loc['osensaTemp3', 'value']/10 > 95 or
        osensaDev.hrDataFrame.loc['osensaTemp4', 'value']/10 > 95 or
        osensaDev.hrDataFrame.loc['osensaTemp5', 'value']/10 > 95 or
        osensaDev.hrDataFrame.loc['osensaTemp6', 'value']/10 > 95 or
        osensaDev.hrDataFrame.loc['osensaTemp7', 'value']/10 > 95 or
        osensaDev.hrDataFrame.loc['osensaTemp8', 'value']/10 > 95
        ):
        localDf.loc['vdOsHighTempAlarm', 'value'] = 1
    
    elif (osensaDev.hrDataFrame.loc['osensaTemp1', 'value']/10 < settings.OSENSA_ALARM_RESET_TEMP or
        osensaDev.hrDataFrame.loc['osensaTemp2', 'value']/10 < settings.OSENSA_ALARM_RESET_TEMP or
        osensaDev.hrDataFrame.loc['osensaTemp3', 'value']/10 < settings.OSENSA_ALARM_RESET_TEMP or
        osensaDev.hrDataFrame.loc['osensaTemp4', 'value']/10 < settings.OSENSA_ALARM_RESET_TEMP or
        osensaDev.hrDataFrame.loc['osensaTemp5', 'value']/10 < settings.OSENSA_ALARM_RESET_TEMP or
        osensaDev.hrDataFrame.loc['osensaTemp6', 'value']/10 < settings.OSENSA_ALARM_RESET_TEMP or
        osensaDev.hrDataFrame.loc['osensaTemp7', 'value']/10 < settings.OSENSA_ALARM_RESET_TEMP or
        osensaDev.hrDataFrame.loc['osensaTemp8', 'value']/10 < settings.OSENSA_ALARM_RESET_TEMP
        ):
        localDf.loc['vdOsHighTempAlarm', 'value'] = 0
    
    localDf.loc['readTimestamp0':'readTimestamp3', 'value'] = cmu_utils.getTimeStampRegs()
    localDf.loc['heartbeat', 'value'] = (virtualSlaveDF.loc['heartbeat', 'value'] + 1) % 65536
    

    #return localDf


'''
Function will print settings of the given instance of the class cmu_utils.ModbusScriptSettings().
'''
def printSettings(settings):
    # Print settings
    settings.printSettings()


'''
Function will check if the requested address of the requested device is writable.
'''
def checkWriteAccess(deviceSettings, address, fx, useRegType = False):
    df = None
    
    if (fx == 5):
        tfx = 15
    elif (fx == 6):
        tfx = 16
    else:
        tfx = fx

    # Find correct dataframe for the device
    for deviceName, properties in devDict.items():
        #print(properties[0], unitId, properties[0] == unitId, cmu_utils.functionCodeDict_write[properties[2]], fx, cmu_utils.functionCodeDict_write[properties[2]] == fx)
        #if (properties[0] == unitId) and (cmu_utils.functionCodeDict_write[properties[2]] == fx):
        if ((properties[0] == deviceSettings) and (cmu_utils.functionCodeDict_write[properties[2]] == tfx)):    
            df = properties[1]
            break # for loop
    
    if df is not None:
        try:
            # Georg version
            #if (df[df['address'] == address]['writeaccess'].values[0] == 'rw'):
            #    return True
            #else:
            #    return False
            
            # Boris first version
            #data = df[df['address'] == address]
            #if not data.empty:
            #    if ((data['writeaccess'].values[0] == 'rw') and (useRegType == False)):
            #        return True
                
            #    if (useRegType == True and (data['regtype'].values[0] == 'r')):
            #        return True
                
            # Boris second version
            if (useRegType == False):
                if ((df[df['address'] == address]['writeaccess'].values[0] == 'rw')):
                    return True
            else:
                if ((df[df['address'] == address]['writeaccess'].values[0] == 'rw') and (df[df['address'] == address]['regtype'].values[0] == 'r')):
                    return True               
            
        
        except IndexError:
            print('Index Error in checkWriteAccess, Address ', address, ' could not be found in Dataframe of device (', deviceSettings.name, ').')
            #print(deviceSettings.name, address, fx)
            return False
    else:
        print('Empty DataFrame. Check device (', deviceSettings.name, ') or fx code (', fx, ').')
        
    return False

'''
Function will check if the requested address of the requested device is virtual
'''
def isVirtualRegister(deviceSettings, address, fx):
    df = None
    # Find correct dataframe for the device
    for deviceName, properties in devDict.items():
        if ((properties[0] == deviceSettings) and (cmu_utils.functionCodeDict_write[properties[2]] == fx)):    
            df = properties[1]
            break # for loop
    
    if df is not None:
        try:
            if (df[df['address'] == address]['regtype'].values[0] == 'v'):
                return True
            #data = df[df['address'] == address]
            #if not data.empty:
            #    if (data['regtype'].values[0] == 'v'):
            #        return True
   
        except IndexError:
            print('Index Error in isVirtualRegister, Address ', address, ' could not be found in Dataframe of device (', deviceSettings.name, ').')
            #print(deviceSettings.name, address, fx)
            return False

    return False

def setPowerCyclingOff(queue, settings):
    if settings.clientOnCube:
        # Stop Power Cycling by setting coil 1 of Controllino to 0.
        queue.put((settings.controllino, 0, cmu_utils.functionCodeDict_write['co'], [0], 0))
    if settings.clientOnTelco:
        # TODO
        pass


#TODO: This cannot work how to determine clientOnTelco ?
def setPowerCyclingOn(queue, settings):
    if settings.clientOnCube:
        # Initiate Power Cycling by setting coil 0 of Controllino to 1.
        deviceName = 'controllino'
        queue.put((deviceName, cmu_utils.functionCodeDict_write['co'], 0, [1], 0))
    if settings.clientOnTelco:
        deviceName = 'upstelco'
        # 0x007
        queue.put((deviceName, cmu_utils.functionCodeDict_write['hr'], upsTelcoDev.hrDataFrame.loc['setSignalingCodeDO2', 'address'], [0, 0], 0))
        # 0x009
        queue.put((deviceName, cmu_utils.functionCodeDict_write['hr'], upsTelcoDev.hrDataFrame.loc['setFunctionCodeDI1', 'address'], [4], 0))
        # Toggle bit 1 of register On by using bitwise or
        # 0x005
        valueLo = upsTelcoDev.hrDataFrame.loc['setParameters', 'value']
        valueHi = upsTelcoDev.hrDataFrame.loc['setParameters_REG2', 'value']
        value = (valueHi << 16) | valueLo
        value = value | 2 
        valueLo = 0xFFFF & value
        valueHi = value >> 16   
        queue.put((deviceName, cmu_utils.functionCodeDict_write['hr'], upsTelcoDev.hrDataFrame.loc['setParameters', 'address'], [valueLo, valueHi], 0))
        # Toggle bit 1 of register Off by using bitwise And
        value = value & 0xFFFFFFFD
        valueLo = 0xFFFF & value
        valueHi = value >> 16           
        #time.sleep(4)
        queue.put((deviceName, cmu_utils.functionCodeDict_write['hr'], upsTelcoDev.hrDataFrame.loc['setParameters', 'address'], [valueLo, valueHi], 3))
        # Toggle bit 1 of register On by using bitwise or
        #value = value|2
        #queue.put((deviceName, cmu_utils.functionCodeDict_write['hr'], upsTelcoDev.hrDataFrame.loc['setParameters', 'address'], [value], 0))
        

'''
The following class is a child class of the ModbusSlaveContext class.
The class necessary, to put write requests, that the server receives into a queue.
If clients are read, and the updated data are written to the server-context, it is highly advisable, 
to use the argument setQueue = False to avoid the unnecessary writing back of the data.
'''
class CustomModbusVirtualSlaveContext(ModbusSlaveContext):

    def __init__(self, settings, queue, *args, **kwargs):
        # Queue and device ID are necessary to write requested data to the clients
        self.queue = queue
        self.settings = settings
        # settings.virtualSlave
        super(CustomModbusVirtualSlaveContext, self).__init__(*args, **kwargs)


    def setVirtualDeviceQueue(self, address, fx, values):
        # Check if a power Cycling Request was made
        #print('setVirtualDeviceQueue for: ' + str(address))
    
        if address == 61: #TODO: what is this? It is hardcoded address for UPS
            if values[0] == 1:
                setPowerCyclingOn(self.queue, self.settings);
                # Don´t request another power cycle.
                virtualSlaveDF.loc['vdUpsPowerCycle', 'value'] = 0
            else:
                setPowerCyclingOff(self.queue, self.settings);
                pass
        else:
            newAddress = virtualSlaveDF.iloc[address]['address']
            deviceSettings = virtualSlaveDF.iloc[address]['deviceSettings']
            
            # check if the register of the real device is writable.
            if checkWriteAccess(deviceSettings, newAddress, fx):
                self.queue.put((deviceSettings.name, fx, newAddress, values, 0))
            else:
                # The raising of an Error will automatically trigger the response of an error code via modbus.
                raise cmu_utils.WriteAccessError()    
  
    
    # This function is called if the server receives write requests.
    # The write requests are put in a queue and will be handled.
    #def setValues(self, settings, fx, address, values, setQueue = True):
    def setValues(self, fx, address, values, setQueue = True):
        
        if setQueue:
            #Check if the write request was for the virtual device.
            #If so, map the write request to the correct address and unitID
            if ((self.settings.clientOnCube == True) or (self.settings.clientOnTelco == True)):
                self.setVirtualDeviceQueue(address, fx, values)

        super(CustomModbusVirtualSlaveContext, self).setValues(fx, address, values)


'''
The following class is a child class of the ModbusSlaveContext class.
The class necessary, to put write requests, that the server receives into a queue.
If clients are read, and the updated data are written to the server-context, it is highly advisable, 
to use the argument setQueue = False to avoid the unnecessary writing back of the data.
'''
class CustomModbusSlaveContext(ModbusSlaveContext):

    def __init__(self, deviceSettings, queue, *args, **kwargs):
        # Queue and device ID are necessary to write requested data to the clients
        self.queue = queue
        self.deviceSettings = deviceSettings
        super(CustomModbusSlaveContext, self).__init__(*args, **kwargs)

    # This function is called if the server receives write requests.
    # The write requests are put in a queue and will be handled.
    #def setValues(self, settings, fx, address, values, setQueue = True):
    def setValues(self, fx, address, values, setQueue = True):

        if setQueue:
            # It is a "normal" device, write to the server on clientIpAddress
            # check if the register of the real device is writable.
            if (checkWriteAccess(self.deviceSettings, address, fx, True)):
                self.queue.put((self.deviceSettings.name, fx, address, values, 0))

            elif (isVirtualRegister(self.deviceSettings, address, fx)):
                deviceSetting, df, fc = devDict[self.deviceSettings.name]
                # data = df[df['address'] == address]
                # if not data.empty:
                #    data['value'] = values
                #df[df['address'] == address]['value'] = values
                try:
                    df.loc[df['address'] == address, 'value'] = values
                except IndexError:
                    print('Index Error in setValues, Address could not be found in Dataframe of device.')
            else:
                # The raising of an Error will automatically trigger the response of an error code via modbus.
                raise cmu_utils.WriteAccessError()

        super(CustomModbusSlaveContext, self).setValues(fx, address, values)

class CustomModbusChillerSlaveContext(CustomModbusSlaveContext):

    #def __init__(self, deviceSettings, queue, *args, **kwargs):
    #    # Queue and device ID are necessary to write requested data to the clients
    #    self.queue = queue
    #    self.deviceSettings = deviceSettings
    #    super(CustomModbusChillerSlaveContext, self).__init__(*args, **kwargs)

    # This function is called if the server receives write requests.
    # The write requests are put in a queue and will be handled.
    #def setValues(self, settings, fx, address, values, setQueue = True):
    def setValues(self, fx, address, values, setQueue = True):
        
        if setQueue:
            # Send password before any command
            queue.put((self.deviceSettings.name, cmu_utils.functionCodeDict_write['hr'], 0x8100, [1], 0)) # Send password

        super(CustomModbusChillerSlaveContext, self).setValues(fx, address, values, setQueue)


def closeAllClients(settings):
    if settings.clients.client is not None:
        settings.clients.client.close()
        
    if settings.controllino.client is not None:
        settings.controllino.client.close()
    if settings.chiller1.client is not None:
        settings.chiller1.client.close()
    if settings.chiller2.client is not None:
        settings.chiller2.client.close()
    if settings.hvac1.client is not None:
        settings.hvac1.client.close()
    if settings.hvac2.client is not None:
        settings.hvac2.client.close()
    if settings.osensa.client is not None:
        settings.osensa.client.close()
    if settings.dl10.client is not None:
        settings.dl10.client.close()
    if settings.flowmeter1.client is not None:
        settings.flowmeter1.client.close()
    if settings.ups.client is not None:
        settings.ups.client.close()
    if settings.flowmeter2.client is not None:
        settings.flowmeter2.client.close()
    if settings.hvacTelco.client is not None:
        settings.hvacTelco.client.close()
    if settings.upsTelco.client is not None:
        settings.upsTelco.client.close()

'''
Function checks if reactor is still running. 
This is important in the client thread so it is shut down if the main thread is aborted (e.g. with ctrl c).
'''
def checkReactorRunning(settings):
    if not reactor.running:
        closeAllClients(settings)
        print('Shutdown of Client Readout Thread')
        time.sleep(1)
        os._exit(1)


'''
Function reads (or simulates reading) values from all modbus clients and prints them out afterwards.
If the Testing mode is active it will simulate the reading of the clients by using random values.
Only the clients that are supposed to be read as decided by user input will be read and printed.
'''
def readClients(settings):
    global virtualSlaveDF
    print('Starting Client')
    
    checkReactorRunning(settings)
    
    if settings.MODE == 'Production' or settings.MODE == 'Manual':
        # read input registers
        # Check if the Device is supposed to be read first. controllinoRead can be set as an argument in the command line.
        if settings.controllino.use:
            checkReactorRunning(settings)
            controllinoDev.readAll()

        # Chiller 1
        if settings.chiller1.use:
            checkReactorRunning(settings)
            chillerDev1.readAll()

        # Chiller 2
        if settings.chiller2.use:
            checkReactorRunning(settings)
            chillerDev2.readAll()

        # HVAC 1
        if settings.hvac1.use:
            checkReactorRunning(settings)
            hvacDev1.readAll()

        # HVAC 2
        if settings.hvac2.use:
            checkReactorRunning(settings)
            hvacDev2.readAll()

        # Osensa
        if settings.osensa.use:
            checkReactorRunning(settings)
            osensaDev.readAll()

        # dl10
        if settings.dl10.use:
            checkReactorRunning(settings)
            dl10Dev.readAll()

        # Flow Meter 1
        if settings.flowmeter1.use:
            checkReactorRunning(settings)
            flowmeterDev1.readAll()

        # UPS                  
        if settings.ups.use:
            checkReactorRunning(settings)
            upsDev.readAll()

        # Flow Meter 1                
        if settings.flowmeter2.use:
            checkReactorRunning(settings)
            flowmeterDev2.readAll()
        
        # HVAC (Telco)
        if settings.hvacTelco.use:
            checkReactorRunning(settings)
            hvacTelcoDev.readAll()
        
        # UPS (Telco)
        if settings.upsTelco.use:
            checkReactorRunning(settings)
            upsTelcoDev.readAll()
        
        if settings.virtualSlave.use:
            #TODO: Extend code with correct handling if server is on the cube/OCTE or working as proxy on another computer
            # if settings.clientOnCube or settings.clientOnTelco:
            #     getVirtualSlaveValues(settings, virtualSlaveDF)
            # else :
            #     virtualSlaveDev.readAll()
                
            #virtualSlaveDF = getVirtualSlaveValues(settings, virtualSlaveDF)
            getVirtualSlaveValues(settings, virtualSlaveDF)
        
    elif settings.MODE == 'Testing':
        if settings.controllino.use:
            for i in controllinoDev.irDataFrame.index:
                controllinoDev.irDataFrame.loc[i, 'value'] = random.randrange(2)
            for i in controllinoDev.coDataFrame.index:
                controllinoDev.coDataFrame.loc[i, 'value'] = random.randrange(2)
        
        if settings.chiller1.use:
            for i in chillerDev1.hrDataFrame.index:
                chillerDev1.hrDataFrame.loc[i, 'value'] = random.randrange(2)

        if settings.chiller2.use:
            for i in chillerDev2.hrDataFrame.index:
                chillerDev2.hrDataFrame.loc[i, 'value'] = random.randrange(2)

        if settings.hvac1.use:
            for i in hvacDev1.hrDataFrame.index:
                hvacDev1.hrDataFrame.loc[i, 'value'] = random.randrange(2)

        if settings.hvac2.use:
            for i in hvacDev2.hrDataFrame.index:
                hvacDev2.hrDataFrame.loc[i, 'value'] = random.randrange(2)

        if settings.osensa.use:
            for i in osensaDev.hrDataFrame.index:
                osensaDev.hrDataFrame.loc[i, 'value'] = random.randrange(2)

        if settings.dl10.use:
            for i in dl10Dev.irDataFrame.index:
                dl10Dev.irDataFrame.loc[i, 'value'] = random.randrange(2)

        if settings.flowmeter1.use:
            for i in flowmeterDev1.hrDataFrame.index:
                flowmeterDev1.hrDataFrame.loc[i, 'value'] = random.randrange(2)

        if settings.ups.use:
            for i in upsDev.hrDataFrame.index:
                upsDev.hrDataFrame.loc[i, 'value'] = random.randrange(2)
        
        if settings.flowmeter2.use:
            for i in flowmeterDev2.hrDataFrame.index:
                flowmeterDev2.hrDataFrame.loc[i, 'value'] = random.randrange(2)
        
        if settings.virtualSlave.use:
            #virtualSlaveDF = getVirtualSlaveValues(settings, virtualSlaveDF)
            getVirtualSlaveValues(settings, virtualSlaveDF)
        
        if settings.hvacTelco.use:
            for i in hvacTelcoDev.hrDataFrame.index:
                hvacTelcoDev.hrDataFrame.loc[i, 'value'] = random.randrange(2)
            for i in hvacTelcoDev.coDataFrame.index:
                hvacTelcoDev.coDataFrame.loc[i, 'value'] = random.randrange(2)
        
        if settings.upsTelco.use:
            for i in upsTelcoDev.hrDataFrame.index:
                upsTelcoDev.hrDataFrame.loc[i, 'value'] = random.randrange(2)

    # Those Settings are necessary, so all values get printed out without truncation
    pd.set_option('display.max_rows', None)
    pd.set_option('display.max_columns', None)
    pd.set_option('display.width', None)
    pd.set_option('display.max_colwidth', None)

    if not settings.MODE == 'Production':
        # print out all read values
        if settings.controllino.use or settings.chiller1.use or settings.chiller2.use or settings.hvac1.use or settings.hvac2.use or settings.osensa.use or settings.dl10.use or settings.flowmeter1.use or settings.ups.use or settings.flowmeter2.use or settings.virtualSlave.use or settings.hvacTelco.use or settings.upsTelco.use: 
            print('\n\n\n # ============ New Data ============ #')
        if settings.controllino.use == True:
            print('\n\n# ============ Controllino ============ #')
            controllinoDev.printDataFrames()
        if settings.chiller1.use == True:
            print('\n\n# ============ Chiller 1 ============ #')
            chillerDev1.printDataFrames()
        if settings.chiller2.use == True:
            print('\n\n# ============ Chiller 2 ============ #')
            chillerDev2.printDataFrames()
        if settings.hvac1.use == True:
            print('\n\n# ============ Hvac 1 ============ #')
            hvacDev1.printDataFrames()
        if settings.hvac2.use == True:
            print('\n\n# ============ Hvac 2 ============ #')
            hvacDev2.printDataFrames()
        if settings.osensa.use == True:
            print('\n\n# ============ Osensa ============ #')
            osensaDev.printDataFrames()
        if settings.dl10.use:
            print('\n\n# ============ Dl10 ============ #')
            dl10Dev.printDataFrames()
        if settings.flowmeter1.use:
            print('\n\n# ============ Flow Meter 1 ============ #')
            flowmeterDev1.printDataFrames()
        if settings.ups.use:
            print('\n\n# ============ UPS ============ #')
            upsDev.printDataFrames()
        if settings.flowmeter2.use:
            print('\n\n# ============ Flow Meter 2 ============ #')
            flowmeterDev2.printDataFrames()
        if settings.virtualSlave.use:
            print('\n\n# ============ Virtual Slave ============ #')
            print(virtualSlaveDF)
        
        if settings.hvacTelco.use:
            print('\n\n# ============ HVAC (Telco) ============ #')
            hvacTelcoDev.printDataFrames()
        if settings.upsTelco.use:
            print('\n\n# ============ UPS (Telco) ============ #')
            upsTelcoDev.printDataFrames()


'''
Function first reads all clients and then transfers the data to the server context store.
'''
def readClientsWriteToServer(log, settings, context):

    log.debug("updating the context")

    coil = 1
    discreteinput = 2
    holdingregister = 3
    inputregister = 4

    # Reads all Modbus clients
    readClients(settings)

    # Commented code can be used to simulate reading of alternating zeros and ones for all modbus devices.
    # if(cubesDf.iloc[0, 1]) == 1:
    #     cubesDf.iloc[0, :] = [0 for i in range(len(cubesDf.iloc[0, :])) ]
    # else:
    #     cubesDf.iloc[0, :] = [1 for i in range(len(cubesDf.iloc[0, :])) ]

    # Write the read values to the server context.
    # IMPORTANT: context.setValues expects a list, so use Brackets [integer] to set an integer value.
    log.debug("values updated")
    # print('hallom')
    # print(settings.controllino.use)
    # print(settings.dl10.use)
    # print(settings.chiller1.use)

    if settings.controllino.use == True:
        for i in controllinoDev.irDataFrame.index:
            values = [ controllinoDev.irDataFrame.loc[i, 'value'] ]
            #context[settings.controllino.unitId].setValues(settings, inputregister, controllinoDev.irDataFrame.loc[i, 'address'], values, setQueue = False)
            context[settings.controllino.unitId].setValues(inputregister, controllinoDev.irDataFrame.loc[i, 'address'], values, setQueue = False)

        for i in controllinoDev.coDataFrame.index:
            values = [ controllinoDev.coDataFrame.loc[i, 'value'] ]
            #context[settings.controllino.unitId].setValues(settings, coil, controllinoDev.coDataFrame.loc[i, 'address'], values, setQueue = False)
            context[settings.controllino.unitId].setValues(coil, controllinoDev.coDataFrame.loc[i, 'address'], values, setQueue = False)
    # else:
    #     print('controllino not in use')

    if settings.chiller1.use == True:
        for i in chillerDev1.hrDataFrame.index:
            values = [ chillerDev1.hrDataFrame.loc[i, 'value'] ]
            #context[settings.chiller1.unitId].setValues(settings, holdingregister, chillerDev1.hrDataFrame.loc[i, 'address'], values, setQueue = False)
            context[settings.chiller1.unitId].setValues(holdingregister, chillerDev1.hrDataFrame.loc[i, 'address'], values, setQueue = False)
    # else:
    #     print('chiller1 not in use')

    if settings.chiller2.use == True:
        for i in chillerDev2.hrDataFrame.index:
            values = [ chillerDev2.hrDataFrame.loc[i, 'value'] ]
            #context[settings.chiller2.unitId].setValues(settings, holdingregister, chillerDev2.hrDataFrame.loc[i, 'address'], values, setQueue = False)
            context[settings.chiller2.unitId].setValues(holdingregister, chillerDev2.hrDataFrame.loc[i, 'address'], values, setQueue = False)

    if settings.hvac1.use == True:
        for i in hvacDev1.hrDataFrame.index:
            values = [ hvacDev1.hrDataFrame.loc[i, 'value'] ]
            #context[settings.hvac1.unitId].setValues(settings, holdingregister, hvacDev1.hrDataFrame.loc[i, 'address'], values, setQueue = False)
            context[settings.hvac1.unitId].setValues(holdingregister, hvacDev1.hrDataFrame.loc[i, 'address'], values, setQueue = False)

    if settings.hvac2.use == True:
        for i in hvacDev2.hrDataFrame.index:
            values = [ hvacDev2.hrDataFrame.loc[i, 'value'] ]
            #context[settings.hvac2.unitId].setValues(settings, holdingregister, hvacDev2.hrDataFrame.loc[i, 'address'], values, setQueue = False)
            context[settings.hvac2.unitId].setValues(holdingregister, hvacDev2.hrDataFrame.loc[i, 'address'], values, setQueue = False)

    if settings.osensa.use == True:
        for i in osensaDev.hrDataFrame.index:
            values = [ osensaDev.hrDataFrame.loc[i, 'value'] ]
            #context[settings.osensa.unitId].setValues(settings, holdingregister, osensaDev.hrDataFrame.loc[i, 'address'], values, setQueue = False)
            context[settings.osensa.unitId].setValues(holdingregister, osensaDev.hrDataFrame.loc[i, 'address'], values, setQueue = False)

    if settings.dl10.use == True:
        for i in dl10Dev.irDataFrame.index:
            values = [ dl10Dev.irDataFrame.loc[i, 'value'] ]
            #context[settings.dl10.unitId].setValues(settings, inputregister, dl10Dev.irDataFrame.loc[i, 'address'], values, setQueue = False)
            context[settings.dl10.unitId].setValues(inputregister, dl10Dev.irDataFrame.loc[i, 'address'], values, setQueue = False)

    if settings.flowmeter1.use == True:
        for i in flowmeterDev1.hrDataFrame.index:
            values = [ flowmeterDev1.hrDataFrame.loc[i, 'value'] ]
            #context[settings.flowmeter1.unitId].setValues(settings, holdingregister, flowmeterDev1.hrDataFrame.loc[i, 'address'], values, setQueue = False)
            context[settings.flowmeter1.unitId].setValues(holdingregister, flowmeterDev1.hrDataFrame.loc[i, 'address'], values, setQueue = False)

    if settings.ups.use == True:
        for i in upsDev.hrDataFrame.index:
            values = [ upsDev.hrDataFrame.loc[i, 'value'] ]
            #context[settings.ups.unitId].setValues(settings, holdingregister, upsDev.hrDataFrame.loc[i, 'address'], values, setQueue = False)
            context[settings.ups.unitId].setValues(holdingregister, upsDev.hrDataFrame.loc[i, 'address'], values, setQueue = False)

    if settings.flowmeter2.use == True:
        for i in flowmeterDev2.hrDataFrame.index:
            values = [ flowmeterDev2.hrDataFrame.loc[i, 'value'] ]
            #context[settings.flowmeter2.unitId].setValues(settings, holdingregister, flowmeterDev2.hrDataFrame.loc[i, 'address'], values, setQueue = False)
            context[settings.flowmeter2.unitId].setValues(holdingregister, flowmeterDev2.hrDataFrame.loc[i, 'address'], values, setQueue = False)

    if settings.hvacTelco.use == True:
        for i in hvacTelcoDev.hrDataFrame.index:
            values = [ hvacTelcoDev.hrDataFrame.loc[i, 'value'] ]
            #context[settings.hvacTelco.unitId].setValues(settings, holdingregister, hvacTelcoDev.hrDataFrame.loc[i, 'address'], values, setQueue = False)
            context[settings.hvacTelco.unitId].setValues(holdingregister, hvacTelcoDev.hrDataFrame.loc[i, 'address'], values, setQueue = False)

        for i in hvacTelcoDev.coDataFrame.index:
            values = [ hvacTelcoDev.coDataFrame.loc[i, 'value'] ]
            #context[settings.hvacTelco.unitId].setValues(settings, holdingregister, hvacTelcoDev.coDataFrame.loc[i, 'address'], values, setQueue = False)
            context[settings.hvacTelco.unitId].setValues(coil, hvacTelcoDev.coDataFrame.loc[i, 'address'], values, setQueue = False)
            context[settings.hvacTelco.unitId].setValues(discreteinput, hvacTelcoDev.diDataFrame.loc[i, 'address'], values, setQueue = False)

    if settings.upsTelco.use == True:
        for i in upsTelcoDev.hrDataFrame.index:
            values = [ upsTelcoDev.hrDataFrame.loc[i, 'value'] ]
            #context[settings.upsTelco.unitId].setValues(settings, holdingregister, upsTelcoDev.hrDataFrame.loc[i, 'address'], values, setQueue = False)
            context[settings.upsTelco.unitId].setValues(holdingregister, upsTelcoDev.hrDataFrame.loc[i, 'address'], values, setQueue = False)

    address = 0
    for i in virtualSlaveDF.index:
        values = [ virtualSlaveDF.loc[i, 'value'] ]
        context[settings.virtualSlave.unitId].setValues(holdingregister, address, values, setQueue = False)
        address += 1

    #print(virtualSlaveDF)
     
'''
Function will check the Queue for write-requests. If there are any, it will write coils or
registers of the respective client.
'''
def writeToClients():
    while True:
        # check the queue for write requests
        try:
            deviceName, fx, address, values, sleepvalue = queue.get(timeout = 1)
            deviceSetting, df, fc = devDict[deviceName]
            client = deviceSetting.client
            unitId = deviceSetting.unitId            
            print('Found in Queue')
            
        except Empty:
            print('Queue empty')
            break # while True:
        
        #if(sleepvalue != None):
        time.sleep(sleepvalue) # must be set, 0 is allowed
            
        # write coils or registers depending on the function code(singular coil/register is also possible with the same function).
        if fx == 5 or fx == 15:
            print('Writing Coil of Client with UnitID: ' + str(unitId))
            try:
                if not client.is_socket_open():
                    client.connect()
                    
                values = np.multiply(values, 1) # Convert True/False to 1/0
                print(unitId, fx, address, values, sleepvalue)                    
                  
                if fx == 5:
                    value = values[0]
                    ret = client.write_coil(address, value, unit = unitId)
                else:
                    ret = client.write_coils(address, values, unit = unitId)
                    
                # Check the function code to detect errors.
                if (ret.function_code < 0x80):
                    print('Write was successful')
                else:
                    print('\n # Write was unsuccessful, check Client with unit ID: ' + str(unitId) + ' #')
                    print(ret)

            except pme.ConnectionException as e:
                print(e)
                print()
                print('#===========!!!IMPORTANT!!!===========#')
                print('Connection to Client with unit ID: ' + str(unitId) + ' impossible')
            except AttributeError as e:
                print(e)
                if'ModbusIOException' in str(e):
                    print('#===========!!!IMPORTANT!!!===========#')
                    print('ModbusIOexption for Client with unit ID: ' + str(unitId) + ' ')
                    print('This can happen when trying to connect to a device ID that does not exist.')
                else:
                    raise AttributeError(e)

        elif fx == 6 or fx == 16:
            print('Trying to write Registers of Client with UnitID: ' + str(unitId))

            try:
                if not client.is_socket_open():
                    client.connect()
                    
                print(unitId, fx, address, values, sleepvalue)    
                
                if fx == 6:
                    value = values[0]
                    ret = client.write_register(address, value, unit = unitId)
                else:
                    ret = client.write_registers(address, values, unit = unitId)

                # Check the function code to detect errors.
                if (ret.function_code < 0x80):
                    print('Write was successful')
                else:
                    print('\n # Write was unsuccessful, check Client with unit ID: ' + str(unitId) + ' #')
                    print(ret)

            except pme.ConnectionException as e:
                print(e)
                print()
                print('#===========!!!IMPORTANT!!!===========#')
                print('Connection to Client with unit ID: ' + str(unitId) + ' impossible')
            except AttributeError as e:
                print(e)
                if'ModbusIOException' in str(e):
                    print('#===========!!!IMPORTANT!!!===========#')
                    print('ModbusIOexption for Client with unit ID: ' + str(unitId) + ' ')
                    print('This can happen when trying to connect to a device ID that does not exist.')
                else:
                    raise AttributeError(e)
        
        else:
            print('Function Code not Found: ' + str(fx))

        # TODO: Why we need this if?
        if not unitId: 
            break # while True:


'''
Function calls the updating writer in which the transfer of read client data to the server context happens.
It will loop until it realizes the main thread is not running anymore.
'''
def loopTransferDataClientToServer(log, settings, context):
    while True:
        if settings.clients.use:
            readClientsWriteToServer(log, settings, context)
    
            writeToClients()

        if not settings.cyclicRead:
            closeAllClients(settings)
            print('Shutdown of Client Readout Thread and Server')
            time.sleep(1)
            os._exit(1)

        time.sleep(settings.sleepTime)


'''
Function is called upon shutting down the Main Thread.
'''
def shutdownProcess(settings):
    closeAllClients(settings)
    print('Shutdown of Main Thread')
    print('\n\n\n\n\n!!IMPORTANT!!\nThe client readout Thread can take up to 10 seconds to shutdown\n\n\n\n\n')


'''
Function initializes data store for slaves and initializes and starts
the modbus server and the cyclic readout of the clients.
The cyclic client readout is done in a separate thread.
'''
def runUpdatingServerAndClients(log, settings):

    global slavesList

    # If the server receives a write request, it will put it in this queue.
    # Later the values in this queue are written to the clients.
    global queue

    clientOnCubeOrTelco = False 
    if (settings.clientOnCube == True or settings.clientOnTelco == True):
          clientOnCubeOrTelco = True

    # Initialize datastore for all the different slaves.

    def SparseInput(dataFrame):
        dic = {}

        if dataFrame is None:
            dic[0] = [0]*1
            #print(dic)
            return dic

        size = len(dataFrame.index)
        i = 0
        count = 0

        while (i < size):
            a = dataFrame['address'][i]
            j = i + 1
            if j == size:
                dic[b - count] = [0] * (count + 1)
                break
            b = dataFrame['address'][j]
            diff = b - a
            if diff == 1:
                count += 1
            if diff != 1:
                dic[a - count] = [0]* (count + 1)
                count = 0
            i += 1
        #print(dic)
        return dic

    # #Initialize datastore for all the different slaves. (old Version):
    # controllinoSlave = CustomModbusSlaveContext(settings.controllino, queue,
    #         co=ModbusSequentialDataBlock(0, [0] * controllinoDev.coDataFrame['address'].max()),
    #         di=ModbusSequentialDataBlock(0, [0] * 1),
    #         hr=ModbusSequentialDataBlock(0, [0] * 1),
    #         ir=ModbusSequentialDataBlock(0, [0] * controllinoDev.irDataFrame['address'].max()))  # Necessary
    #
    # chiller1Slave = CustomModbusChillerSlaveContext(settings.chiller1, queue,
    #         co=ModbusSequentialDataBlock(0, [0] * 1),
    #         di=ModbusSequentialDataBlock(0, [0] * 1),
    #         hr=ModbusSequentialDataBlock(0, [0] * chillerDev1.hrDataFrame['address'].max()),  # Necessary
    #         ir=ModbusSequentialDataBlock(0, [0] * 1))
    #
    # chiller2Slave = CustomModbusChillerSlaveContext(settings.chiller2, queue,
    #         co=ModbusSequentialDataBlock(0, [0] * 1),
    #         di=ModbusSequentialDataBlock(0, [0] * 1),
    #         hr=ModbusSequentialDataBlock(0, [0] * chillerDev2.hrDataFrame['address'].max()),  # Necessary
    #         ir=ModbusSequentialDataBlock(0, [0] * 1))
    #
    # hvac1Slave = CustomModbusSlaveContext(settings.hvac1, queue,
    #       co=ModbusSequentialDataBlock(0, [0] * 1),
    #       di=ModbusSequentialDataBlock(0, [0] * 1),
    #       hr=ModbusSequentialDataBlock(0, [0] * hvacDev1.hrDataFrame['address'].max()),  # Necessary
    #       ir=ModbusSequentialDataBlock(0, [0] * 1))
    #
    # hvac2Slave = CustomModbusSlaveContext(settings.hvac2, queue,
    #       co=ModbusSequentialDataBlock(0, [0] * 1),
    #       di=ModbusSequentialDataBlock(0, [0] * 1),
    #       hr=ModbusSequentialDataBlock(0, [0] * hvacDev2.hrDataFrame['address'].max()), # Necessary
    #       ir=ModbusSequentialDataBlock(0, [0] * 1))
    #
    # osensaSlave = CustomModbusSlaveContext(settings.osensa, queue,
    #        co=ModbusSequentialDataBlock(0, [0] * 1),
    #        di=ModbusSequentialDataBlock(0, [0] * 1),
    #        # hr = ModbusSequentialDataBlock(1024, [0] * osensaDev.hrDataFrame['address'].max()),   # Necessary
    #        hr=ModbusSequentialDataBlock(0, [0] * osensaDev.hrDataFrame['address'].max()),   # Necessary
    #        ir=ModbusSequentialDataBlock(0, [0] * 1))
    #
    # dl10Slave = CustomModbusSlaveContext(settings.dl10, queue,
    #      co=ModbusSequentialDataBlock(0, [0] * 1),
    #      di=ModbusSequentialDataBlock(0, [0] * 1),
    #      hr=ModbusSequentialDataBlock(0, [0] * 1),
    #      ir=ModbusSequentialDataBlock(0, [0] * dl10Dev.irDataFrame['address'].max()))  # Necessary
    #
    # flowmeter1Slave = CustomModbusSlaveContext(settings.flowmeter1, queue,
    #        co=ModbusSequentialDataBlock(0, [0] * 1),
    #        di=ModbusSequentialDataBlock(0, [0] * 1),
    #        hr=ModbusSequentialDataBlock(0, [0] * flowmeterDev1.hrDataFrame['address'].max()),  # Necessary
    #        ir=ModbusSequentialDataBlock(0, [0] * 1))
    #
    # upsSlave = CustomModbusSlaveContext(settings.ups, queue,
    #         co=ModbusSequentialDataBlock(0, [0] * 1),
    #         di=ModbusSequentialDataBlock(0, [0] * 1),
    #         hr=ModbusSequentialDataBlock(0, [0] * upsDev.hrDataFrame['address'].max()), # Necessary
    #         ir=ModbusSequentialDataBlock(0, [0] * 1))
    #
    # flowmeter2Slave = CustomModbusSlaveContext(settings.flowmeter2, queue,
    #        co=ModbusSequentialDataBlock(0, [0] * 1),
    #        di=ModbusSequentialDataBlock(0, [0] * 1),
    #        hr=ModbusSequentialDataBlock(0, [0] * flowmeterDev2.hrDataFrame['address'].max()),  # Necessary
    #        ir=ModbusSequentialDataBlock(0, [0] * 1))
    #
    # virtualSlave = CustomModbusVirtualSlaveContext(settings, queue,
    #        co=ModbusSequentialDataBlock(0, [0] * 1),
    #        di=ModbusSequentialDataBlock(0, [0] * 1),
    #        hr=ModbusSequentialDataBlock(0, [0] * virtualSlaveDF['address'].max()),  # Necessary
    #        ir=ModbusSequentialDataBlock(0, [0] * 1))
    #
    # hvacTelcoSlave = CustomModbusSlaveContext(settings.hvacTelco, queue,
    #       co=ModbusSequentialDataBlock(0, [0] * hvacTelcoDev.coDataFrame['address'].max()),  # Necessary
    #       di=ModbusSequentialDataBlock(0, [0] * hvacTelcoDev.diDataFrame['address'].max()),  # Necessary
    #       hr=ModbusSequentialDataBlock(0, [0] * hvacTelcoDev.hrDataFrame['address'].max()),  # Necessary
    #       ir=ModbusSequentialDataBlock(0, [0] * 1))
    #
    # upsTelcoSlave = CustomModbusSlaveContext(settings.upsTelco, queue,
    #      co=ModbusSequentialDataBlock(0, [0] * 1),
    #      di=ModbusSequentialDataBlock(0, [0] * 1),
    #      hr=ModbusSequentialDataBlock(0, [0] * upsTelcoDev.hrDataFrame['address'].max()),  # Necessary
    #      ir=ModbusSequentialDataBlock(0, [0] * 1))

    #Initialize datastore for all the different slaves. (new Version):
    controllinoSlave = CustomModbusSlaveContext(settings.controllino, queue,
        co = ModbusSparseDataBlock(SparseInput(controllinoDev.coDataFrame)),
        di = ModbusSparseDataBlock(SparseInput(controllinoDev.diDataFrame)),
        hr = ModbusSparseDataBlock(SparseInput(controllinoDev.hrDataFrame)),
        ir = ModbusSparseDataBlock(SparseInput(controllinoDev.irDataFrame)),
        zero_mode=True)

    chiller1Slave = CustomModbusChillerSlaveContext(settings.chiller1, queue,
        co=ModbusSparseDataBlock(SparseInput(chillerDev1.coDataFrame)),
        di=ModbusSparseDataBlock(SparseInput(chillerDev1.diDataFrame)),
        hr=ModbusSparseDataBlock(SparseInput(chillerDev1.hrDataFrame)),
        ir=ModbusSparseDataBlock(SparseInput(chillerDev1.irDataFrame)),
        zero_mode=True)

    chiller2Slave = CustomModbusChillerSlaveContext(settings.chiller2, queue,
        co=ModbusSparseDataBlock(SparseInput(chillerDev2.coDataFrame)),
        di=ModbusSparseDataBlock(SparseInput(chillerDev2.diDataFrame)),
        hr=ModbusSparseDataBlock(SparseInput(chillerDev2.hrDataFrame)),
        ir=ModbusSparseDataBlock(SparseInput(chillerDev2.irDataFrame)),
        zero_mode=True)

    hvac1Slave = CustomModbusSlaveContext(settings.hvac1, queue,
        co=ModbusSparseDataBlock(SparseInput(hvacDev1.coDataFrame)),
        di=ModbusSparseDataBlock(SparseInput(hvacDev1.diDataFrame)),
        hr=ModbusSparseDataBlock(SparseInput(hvacDev1.hrDataFrame)),
        ir=ModbusSparseDataBlock(SparseInput(hvacDev1.irDataFrame)),
        zero_mode=True)

    hvac2Slave = CustomModbusSlaveContext(settings.hvac2, queue,
        co=ModbusSparseDataBlock(SparseInput(hvacDev2.coDataFrame)),
        di=ModbusSparseDataBlock(SparseInput(hvacDev2.diDataFrame)),
        hr=ModbusSparseDataBlock(SparseInput(hvacDev2.hrDataFrame)),
        ir=ModbusSparseDataBlock(SparseInput(hvacDev2.irDataFrame)),
        zero_mode=True)

    osensaSlave = CustomModbusSlaveContext(settings.osensa, queue,
        co=ModbusSparseDataBlock(SparseInput(osensaDev.coDataFrame)),
        di=ModbusSparseDataBlock(SparseInput(osensaDev.diDataFrame)),
        hr=ModbusSparseDataBlock(SparseInput(osensaDev.hrDataFrame)),
        ir=ModbusSparseDataBlock(SparseInput(osensaDev.irDataFrame)),
        zero_mode=True)

    dl10Slave = CustomModbusSlaveContext(settings.dl10, queue,
        co=ModbusSparseDataBlock(SparseInput(dl10Dev.coDataFrame)),
        di=ModbusSparseDataBlock(SparseInput(dl10Dev.diDataFrame)),
        hr=ModbusSparseDataBlock(SparseInput(dl10Dev.hrDataFrame)),
        ir=ModbusSparseDataBlock(SparseInput(dl10Dev.irDataFrame)),
        zero_mode=True)

    flowmeter1Slave = CustomModbusSlaveContext(settings.flowmeter1, queue,
        co=ModbusSparseDataBlock(SparseInput(flowmeterDev1.coDataFrame)),
        di=ModbusSparseDataBlock(SparseInput(flowmeterDev1.diDataFrame)),
        hr=ModbusSparseDataBlock(SparseInput(flowmeterDev1.hrDataFrame)),
        ir=ModbusSparseDataBlock(SparseInput(flowmeterDev1.irDataFrame)),
        zero_mode=True)

    upsSlave = CustomModbusSlaveContext(settings.ups, queue,
        co=ModbusSparseDataBlock(SparseInput(upsDev.coDataFrame)),
        di=ModbusSparseDataBlock(SparseInput(upsDev.diDataFrame)),
        hr=ModbusSparseDataBlock(SparseInput(upsDev.hrDataFrame)),
        ir=ModbusSparseDataBlock(SparseInput(upsDev.irDataFrame)),
        zero_mode=True)

    flowmeter2Slave = CustomModbusSlaveContext(settings.flowmeter2, queue,
        co=ModbusSparseDataBlock(SparseInput(flowmeterDev2.coDataFrame)),
        di=ModbusSparseDataBlock(SparseInput(flowmeterDev2.diDataFrame)),
        hr=ModbusSparseDataBlock(SparseInput(flowmeterDev2.hrDataFrame)),
        ir=ModbusSparseDataBlock(SparseInput(flowmeterDev2.irDataFrame)),
        zero_mode=True)

    # virtualSlave = CustomModbusVirtualSlaveContext(settings.virtualSlave, queue,
    #     co=ModbusSparseDataBlock(SparseInput(virtualSlaveDF)),
    #     di=ModbusSparseDataBlock(SparseInput(virtualSlaveDF)),
    #     hr=ModbusSparseDataBlock(SparseInput(virtualSlaveDF)),
    #     ir=ModbusSparseDataBlock(SparseInput(virtualSlaveDF)),
    #     zero_mode=True)

    # Using old Version for virtualSlave
    virtualSlave = CustomModbusVirtualSlaveContext(settings.virtualSlave, queue,
        co = ModbusSequentialDataBlock(0, [0] * 1),
        di = ModbusSequentialDataBlock(0, [0] * 1),
        hr = ModbusSequentialDataBlock(0, [0] * virtualSlaveDF['address'].max()),  # Necessary
        ir = ModbusSequentialDataBlock(0, [0] * 1))

    hvacTelcoSlave = CustomModbusSlaveContext(settings.hvacTelco, queue,
        co=ModbusSparseDataBlock(SparseInput(hvacTelcoDev.coDataFrame)),
        di=ModbusSparseDataBlock(SparseInput(hvacTelcoDev.diDataFrame)),
        hr=ModbusSparseDataBlock(SparseInput(hvacTelcoDev.hrDataFrame)),
        ir=ModbusSparseDataBlock(SparseInput(hvacTelcoDev.irDataFrame)),
        zero_mode=True)

    upsTelcoSlave = CustomModbusSlaveContext(settings.upsTelco, queue,
        co=ModbusSparseDataBlock(SparseInput(upsTelcoDev.coDataFrame)),
        di=ModbusSparseDataBlock(SparseInput(upsTelcoDev.diDataFrame)),
        hr=ModbusSparseDataBlock(SparseInput(upsTelcoDev.hrDataFrame)),
        ir=ModbusSparseDataBlock(SparseInput(upsTelcoDev.irDataFrame)),
        zero_mode=True)




    #TODO: settings.hvacTelco.UnitId = 1, collision with settings.chiller1.UnitId
    slavesList = {
        settings.controllino.unitId : controllinoSlave, 
        settings.chiller1.unitId    : chiller1Slave, 
        settings.chiller2.unitId    : chiller2Slave, 
        settings.hvac1.unitId       : hvac1Slave, 
        settings.hvac2.unitId       : hvac2Slave, 
        settings.osensa.unitId      : osensaSlave, 
        settings.dl10.unitId        : dl10Slave, 
        settings.flowmeter1.unitId  : flowmeter1Slave, 
        settings.ups.unitId         : upsSlave,
        settings.flowmeter2.unitId  : flowmeter2Slave, 
        settings.virtualSlave.unitId: virtualSlave,
        
        settings.upsTelco.unitId    : upsTelcoSlave,
        settings.hvacTelco.unitId   : hvacTelcoSlave
    }

    # Set the Server Name. Not really used for anything at the moment.
    identity = ModbusDeviceIdentification()
    identity.VendorName = 'pymodbus'

    # Set context for the Modbus Server (i.e. The previously initialized data store of slaves.)
    context = ModbusServerContext(slaves = slavesList, single = False)

    # Start the cyclic readout of the clients in a separate thread.
    # The received values are written into the server context.
    reactor.callInThread(loopTransferDataClientToServer, log, settings, context)

    # calls shutdownProcess function to exit all threads cleanly.
    reactor.addSystemEventTrigger('before', 'shutdown', shutdownProcess, settings)

    # Starts the modbus tcp server with deferred reactor run for more obvious code.
    if settings.server.use == True:
        try:
            print('Starting Server')
            StartTcpServer(context, identity = identity, address = (settings.server.ipAddress, settings.server.port), defer_reactor_run = True)
        except OSError as e:
            print('#======== ERROR ========#')
            print('Another Modbus server is already running on this IP-Address')
            print('It is not possible to have two Modbus servers on the same IP-address and Port')
            print('Exiting Script')
            print(e)
            os._exit(1)
        except twisted.internet.error.CannotListenError as e:
            print('#======== ERROR ========#')
            print('Another Modbus server is already running on this IP-Address')
            print('It is not possible to have two Modbus servers on the same IP-address and Port')
            print('Exiting Script')
            print(e)
            os._exit(1)

    # Start the reactor to actually start all previous processes.
    reactor.run()

#def init():

def main():
    # ============== IMPORTANT SETTING ============== #
    # All settings are stored in this class. Most important settings can be set here.
    settings = cmu_utils.ModbusScriptSettings()
    settingsDefault = cmu_utils.ModbusDeviceSettingsDefault()
    settings.MODE = 'Production'

    # Set showhelptext: True will show the Helptext when starting the script, False will not show it.
    settings.SHOWHELPTEXT = False    
    
    settings.MAX_SOFTWARE_VERSION_CHILLER = 4245
    settings.MAX_SOFTWARE_VERSION_HVAC = 100
    settings.OSENSA_ALARM_RESET_TEMP = 90
    settings.MODBUS_TIMEOUT = 5

    print('Starting Script')

    # ============================== General Settings, normally not too important. ============================== #
    # Setup for Pymodbus Server Logging
    FORMAT = ('%(asctime)-15s %(threadName)-15s'
              ' %(levelname)-8s %(module)-15s:%(lineno)-8s %(message)s')
    logging.basicConfig(format = FORMAT)
    log = logging.getLogger()

    # Se Log Level:
    # 'logging.DEBUG' will print out received and sent data as well as lots of other information
    # 'logging.CRITICAL' will only print out critical errors
    log.setLevel(logging.CRITICAL)

    # ============================== Definition of default values. Important when no config file or command line is used. ============================== #
    # Set default values. Those are used if no arguments or inputs are given.
    
    #defaultConfigFilePath = "/usr/local/share/fluence/controllino_modbus.conf"
    defaultConfigFilePath = "controllino_modbus.conf"

    # Client is the modbus device, that the script "talks to". It could be the controllino, an hvac etc.
    settingsDefault.clientIpAddress = '192.168.2.3'
    settingsDefault.clientPort = 502
    
    # The modbus server is emulated by this script. The modbus server has the same IP address as the device this script is running on.
    # Get IP address of the device that the script is running on and set it
    settingsDefault.serverIpAddress = cmu_utils.getServerIpAddress()
    if(not settingsDefault.serverIpAddress):
        sys.exit(os.EX_NOHOST)
        
    settingsDefault.serverPort = 1502
    
    # Default Values to change the behaviour of the script. Change those if necessary.
    # Default values are only used if no config file or command line arguments are given.
    settingsDefault.cyclicRead = False  # False
    settingsDefault.serverUse = False  # True
    settingsDefault.clientsUse = True  # True
    settingsDefault.controllinoUse = False  # True
    settingsDefault.chiller1Use = False  # True
    settingsDefault.chiller2Use = False  # True
    settingsDefault.hvac1Use = False  # True
    settingsDefault.hvac2Use = False  # True
    settingsDefault.osensaUse = False  # True
    settingsDefault.dl10Use = False  # True
    settingsDefault.flowmeter1Use = False  # True
    settingsDefault.upsUse = False #True
    settingsDefault.flowmeter2Use = False  # True
    settingsDefault.virtualSlaveUse = False
    settingsDefault.sleepTime = 2
    
    settingsDefault.hvacTelcoUse = False
    settingsDefault.upsTelcoUse = False
    
    
    # ============================== Initialization of settings. ============================== #
    # Initialize Parameters with default values
    # All settings are stored in the instance "settings" of the class cmu_utils.ModbusScriptSettings().
    # Here the default values are defined.
    # Function will set the actual settings to the default values.
    settings.setDefaultSettings(settingsDefault)
   
    
    # Shows helptext, which is useful for first-time users.
    if settings.SHOWHELPTEXT == True:
        cmu_utils.showHelptext(settings.MODE)
    
    # check for user input, arguments and config file to set correct ip address and mode.
    # First, check if the user just wants to see the helptext.
    if ('-h' in sys.argv or 'help' in sys.argv):
        if settings.SHOWHELPTEXT == True:
            # Helptext was already shown.
            sys.exit()
        else:
            cmu_utils.showHelptext(settings.MODE)
            sys.exit()

    # If user wants to use the script:
    # First load the hardcoded config file path:
    if os.path.isfile(defaultConfigFilePath):
        ret = cmu_utils.readConfigFile(defaultConfigFilePath, settings, settingsDefault)
        if ret is not None:
            settings = ret

    # Second, check if the user wants to use a different config file.
    if ('-f' in sys.argv):
        # Use config file
        print('Using Config Values from path:')

        # Check if user gave only file name as argument
        if os.path.isfile(os.path.join(os.getcwd(), str(sys.argv[sys.argv.index('-f') + 1]))):
            path = os.path.join(os.getcwd(), str(sys.argv[sys.argv.index('-f') + 1]))

        # check if user gave full path as argument
        elif os.path.isfile(str(sys.argv[sys.argv.index('-f') + 1])):
            path = str(sys.argv[sys.argv.index('-f') + 1])
#        else:
#            path = os.path.join(os.getcwd(), 'controllino_modbus.conf')

        # Overwrite the values from the default Filepath with the manually entered filepath
        ret = cmu_utils.readConfigFile(path, settings, settingsDefault)
        if ret is not None:
            settings = ret
#    else:
#        path = os.path.join(os.getcwd(), 'controllino_modbus.conf')
#        ret = cmu_utils.readConfigFile(path, settings, settingsDefault)
#        if ret is not None:
#            settings = ret
    
    # If command line arguments are given, ignore config file.
    if ('-f' in sys.argv):
        if(len(sys.argv) > 3):
            print('\n\n\n!!!=========!!!WARNING!!!=========!!!')
            print('Arguments are given in the command line, values from config file will be ignored')
            # Initialize Parameters with default values
            settings.setDefaultSettings(settingsDefault)
    else:
        if(len(sys.argv) > 1):
            print('\n\n\n!!!=========!!!WARNING!!!=========!!!')
            print('Arguments are given in the command line, values from config file will be ignored')
            # Initialize Parameters with default values
            settings.setDefaultSettings(settingsDefault)
    
    # Even if the Config File is used, use user arguments or input values to overwrite options.
    # First, set Ip addresses and ports from input or arguments.
    if(len(sys.argv) > 2):
        # check if Ip Address has correct Format
        if len(str(sys.argv[1]).split('.')) == 4:
            settings.clients.ipAddress = str(sys.argv[1])
            if (settingsDefault.clientIpAddress == settings.clients.ipAddress):
                settings.clientOnCube = True
                
            # only try to assign port in combination with IP
            try:
                val = int(sys.argv[2])
                settings.clients.port = val
            except ValueError:
                pass

    # More arguments than IP address and Port are given, so use them.
    # make every argument lower case to avoid typos
    map(str.lower, sys.argv)

    # At first, check if -a or all is given. Afterwards disable or enable specific settings.
    if 'all' in sys.argv or '-a' in sys.argv:
        settings.server.use = True
        settings.clients.use = True
        settings.controllino.use = True
        settings.chiller1.use = True
        settings.chiller2.use = True
        settings.hvac1.use = True
        settings.hvac2.use = True
        settings.osensa.use = True
        settings.dl10.use = True
        settings.flowmeter1.use = True
        settings.ups.use = True
        settings.flowmeter2.use = True
        settings.virtualSlave.use = True
        settings.hvacTelco.use = True
        settings.upsTelco.use = True
        

    # Disable or enable specific settings.
    if 'help' in sys.argv or '-h' in sys.argv:
        cmu_utils.showHelptext(settings.MODE)
        sys.exit()

    if 'sleepTime' in sys.argv or '-s' in sys.argv:
        if 'sleepTime' in sys.argv:
            i = sys.argv.index('sleepTime')
        elif '-s' in sys.argv:
            i = sys.argv.index('-s')
        settings.sleepTime = int(sys.argv[i + 1])

    if 'mode' in sys.argv:
        i = sys.argv.index('mode')
        settings.MODE = sys.argv[i + 1]

    if 'cyclic' in sys.argv or '-c' in sys.argv:
        settings.cyclicRead = True

    if 'clients' in sys.argv:
        settings.clients.use = True

    if 'server' in sys.argv:
        settings.server.use = True

    if 'controllino' in sys.argv:
        settings.controllino.use = True

    if 'chiller' in sys.argv: # ALias for chiller1
        settings.chiller1.use = True

    if 'chiller1' in sys.argv:
        settings.chiller1.use = True

    if 'chiller2' in sys.argv:
        settings.chiller2.use = True

    if 'hvac' in sys.argv:
        settings.hvac1.use = True

    if 'hvac1' in sys.argv:
        settings.hvac1.use = True

    if 'hvac2' in sys.argv:
        settings.hvac2.use = True

    if 'osensa' in sys.argv:
        settings.osensa.use = True

    if 'dl10' in sys.argv:
        settings.dl10.use = True

    if 'flowmeter' in sys.argv: # Alias for backwards compatibility.
        settings.flowmeter1.use = True

    if 'flowmeter1' in sys.argv:
        settings.flowmeter1.use = True

    if 'ups' in sys.argv:
        settings.ups.use = True

    if 'flowmeter2' in sys.argv:
        settings.flowmeter2.use = True

    if 'virtualslave' in sys.argv:
        settings.virtualSlave.use = True

    if 'hvactelco' in sys.argv:
        settings.hvacTelco.use = True

    if 'upstelco' in sys.argv:
        settings.upsTelco.use = True

    if 'notclients' in sys.argv:
        settings.clients.use = False

    if 'notserver' in sys.argv:
        settings.server.use = False

    if 'notcyclic' in sys.argv:
        settings.cyclicRead = False

    if 'notcontrollino' in sys.argv:
        settings.controllino.use = False

    if 'notchiller' in sys.argv: # Alias for notchiller1
        settings.chiller1.use = False

    if 'notchiller1' in sys.argv:
        settings.chiller1.use = False

    if 'notchiller2' in sys.argv:
        settings.chiller2.use = False

    if 'nothvac' in sys.argv: # Alias for nothvac1
        settings.hvac1.use = False

    if 'nothvac1' in sys.argv:
        settings.hvac1.use = False

    if 'nothvac2' in sys.argv:
        settings.hvac2.use = False

    if 'notosensa' in sys.argv:
        settings.osensa.use = False

    if 'notdl10' in sys.argv:
        settings.dl10.use = False

    if 'notflowmeter' in sys.argv: # Alias for backwards compatibility.
        settings.flowmeter1.use = False

    if 'notflowmeter1' in sys.argv:
        settings.flowmeter1.use = False

    if 'notups' in sys.argv:
        settings.ups.use = False

    if 'notflowmeter2' in sys.argv:
        settings.flowmeter2.use = False

    if 'notvirtualslave' in sys.argv:
        settings.virtualSlave.use = False

    if 'nothvacTelco' in sys.argv:
        settings.hvacTelco.use = False

    if 'notupsTelco' in sys.argv:
        settings.upsTelco.use = False

    # All Settings set. Print them.
    printSettings(settings)
    
    # After config and command line arguments are read, UnitIDs are determined so initialize the Dataframes.
    devicesInit(settings)
    
    #Example 1:  controllino_modbus.py write chiller1 hr 35596 180
    #Example 1b: controllino_modbus.py write 1        hr 35596 180
    #Example 2:  ccontrollino_modbus.py write chiller1 systemOnOff 1
    #Example 3:  controllino_modbus.py write chiller1 35596 180   (undocumented, not listed in help)
    if 'write' in sys.argv:
        i = sys.argv.index('write')
        
        #                                  Example 1                          | Example2 
        wrArg1 = sys.argv[i + 1].lower() #device controllino, chiller1, hvac1 | the same
        wrArg2 = sys.argv[i + 2]         # hr or co                           | Register name -> will be converted address
        wrArg3 = sys.argv[i + 3]         # Address                            | value
        try:
            wrArg4 = sys.argv[i + 4]     # value
        except IndexError:
            wrArg4 = None
        
        df = None
        deviceSetting = None # class modbusDeviceSettings
        deviceName = None
        if wrArg1 in devDict:
            # returns a deviceId, a dataframe and a functioncode
            #deviceIdRequest, df, fc = devDict[wrArg1]
            deviceName = wrArg1
            deviceSetting, df, fc = devDict[deviceName]
            deviceIdRequest = deviceSetting.unitId
            
        elif wrArg1.isdigit(): #Example 1b
            deviceIdRequest = int(wrArg1)
            for devName, properties in devDict.items():
                if (properties[0].unitId == deviceIdRequest):    
                    deviceSetting = properties[0];
                    deviceName = devName
                    break
           
        ##else:
        if ((deviceSetting == None) or (deviceName == None)):
            print('ERROR: Device ID or Requested Device has wrong Format or does not exist, exiting script')
            os._exit(1)
        
        if df is not None and wrArg2 in df.index: #Example 2
            addressRequest = df.loc[wrArg2, 'address']
            registerTypeRequest = int(cmu_utils.functionCodeDict_write[fc])
            valueRequest = int(wrArg3, 0)
        else:
            if wrArg4 is not None: #Example 1
                registerTypeRequest = int(cmu_utils.functionCodeDict_write[wrArg2.lower()])
                # int(x, 0) means it will automatically detect the base from a string (e.g. will recognize 0x12 as hex)
                addressRequest = int(wrArg3, 0)
                valueRequest = [ int(wrArg4, 0) ]
            else: #Example 3
                registerTypeRequest = int(cmu_utils.functionCodeDict_write[fc])
                addressRequest = int(wrArg2, 0)
                valueRequest = [ int(wrArg3, 0) ]
        
        if not ('force' in sys.argv):
            if checkWriteAccess(deviceSetting, addressRequest, registerTypeRequest):    
                #with deviceSetting what is logical solution is not working, locking problem
                queue.put((deviceName, registerTypeRequest, addressRequest, valueRequest, 0))
                
            else: 
                raise cmu_utils.WriteAccessError()
        else:
            queue.put((deviceName, registerTypeRequest, addressRequest, valueRequest, 0))

        settings.cyclicRead = False
        settings.server.use = True
        settings.clients.use = True
        settings.controllino.use = False
        settings.chiller1.use = False
        settings.chiller2.use = False
        settings.hvac1.use = False
        settings.hvac2.use = False
        settings.osensa.use = False
        settings.dl10.use = False
        settings.flowmeter1.use = False
        settings.ups.use = False
        settings.flowmeter2.use = False
        settings.virtualSlave.use = False
        settings.hvacTelco.use = False
        settings.upsTelco.use = False
    
    if 'readsingle' in sys.argv:
        i = sys.argv.index('readsingle')
        
        rdArg1 = sys.argv[i + 1].lower()
        rdArg2 = sys.argv[i + 2]
        try:
            rdArg3 = sys.argv[i + 3]
        except IndexError:
            rdArg3 = None
        
        df = None
        deviceSetting = None # class modbusDeviceSettings
        deviceName = None
        if rdArg1 in devDict:
            # returns a deviceId, a dataframe and a functioncode
            #deviceIdRequest, df, fc = devDict[rdArg1]
            deviceName = wrArg1
            deviceSetting, df, fc = devDict[deviceName]
            deviceIdRequest = deviceSetting.unitId           
            
        elif rdArg1.isdigit():
            deviceIdRequest = int(rdArg1)
            for devName, properties in devDict.items():
                if (properties[0].UnitId == deviceIdRequest):    
                    deviceSetting = properties[0];
                    deviceName = devName
                    break
        #else:
        if ((deviceSetting == None) or (deviceName == None)):     
            print('ERROR: Device ID or Requested Device has wrong Format or does not exist, exiting script')
            os._exit(1)
        
        if df is not None and rdArg2 in df.index:
            addressRequest = df.loc[rdArg2, 'address']
            registerTypeRequest = int(cmu_utils.functionCodeDict_read[fc])
        else:
            if rdArg3 is not None:
                registerTypeRequest = int(cmu_utils.functionCodeDict_read[rdArg2.lower()])
                # int(x, 0) means it will automatically detect the base from a string (e.g. will recognize 0x12 as hex)
                addressRequest = int(rdArg3, 0)
            else:
                registerTypeRequest = int(cmu_utils.functionCodeDict_read[fc])
                # int(x, 0) means it will automatically detect the base from a string (e.g. will recognize 0x12 as hex)
                addressRequest = int(rdArg2, 0)
        
        readclient = ModbusTcpClient(str(deviceSetting.ipAddress), port = str(deviceSetting.port), timeout = 5)
        readclient.connect()
        
        value = cmu_utils.genericRead(readclient, deviceIdRequest, registerTypeRequest, addressRequest)
        
        if value is not None:
            value = int(value)
            print('Successful Read!')
            printDF = pd.DataFrame(data = [deviceIdRequest, registerTypeRequest, addressRequest, value], index = ['deviceId', 'registerType', 'address', 'value'])
            print(printDF)
            os._exit(0)
        else:
            os._exit(1)
    
    if 'readmultiple' in sys.argv:
        i = sys.argv.index('readmultiple')
        
        rdArg1 = sys.argv[i + 1].lower()
        rdArg2 = sys.argv[i + 2]
        rdArg3 = sys.argv[i + 3]
        try:
            rdArg4 = sys.argv[i + 4]
        except IndexError:
            rdArg4 = None
        
        df = None
        deviceSetting = None # class modbusDeviceSettings
        deviceName = None
        if rdArg1 in devDict:
            # returns a deviceId, a dataframe and a functioncode
            #deviceIdRequest, df, fc = devDict[rdArg1]
            deviceSetting, df, fc = devDict[rdArg1]
            deviceIdRequest = deviceSetting.unitId   
            
        elif rdArg1.isdigit():
            deviceIdRequest = int(rdArg1)
            for deviceName, properties in devDict.items():
                if (properties[0].UnitId == deviceIdRequest):    
                    deviceSetting = properties[0];
                    deviceName = devName
                    break
        #else:
        if ((deviceSetting == None) or (deviceName == None)):        
            print('ERROR: Device ID or Requested Device has wrong Format or does not exist, exiting script')
            os._exit(1)
        
        if df is not None and rdArg2 in df.index and rdArg3 in df.index:
            addresses = df.loc[rdArg2:rdArg3, 'address']
            
            registerTypeRequest = int(cmu_utils.functionCodeDict_read[fc])
        else:
            if rdArg4 is not None:
                registerTypeRequest = int(cmu_utils.functionCodeDict_read[rdArg2.lower()])
                # int(x, 0) means it will automatically detect the base from a string (e.g. will recognize 0x12 as hex)
                addressstart = int(rdArg3, 0)
                addressend = int(rdArg4, 0)
                addresses = np.arange(addressstart, addressend, 1)
        
        readclient = ModbusTcpClient(str(deviceSetting.ipAddress), port = str(deviceSetting.port), timeout = 5)
        readclient.connect()
        
        value = [cmu_utils.genericRead(readclient, deviceIdRequest, registerTypeRequest, i) for i in addresses]
        
        if value is not None:
            for i,v in enumerate(value):
                if value[i] is None:
                    os._exit(1)
                value[i] = int(value[i])
            print('Successful Read!')
            printDF = pd.DataFrame(data = [deviceIdRequest, registerTypeRequest], index = ['deviceId', 'registerType'])
            print(printDF)
            print()
            print('#===Values===#')
            if df is not None and rdArg2 in df.index and rdArg3 in df.index:
                valuedf = pd.DataFrame(data = value, index = df[rdArg2:rdArg3].index)
            else:
                valuedf = pd.DataFrame(data = value, index = addresses)
            
            print(valuedf)
            os._exit(0)
        else:
            os._exit(1)
    
 #   init()
    runUpdatingServerAndClients(log, settings)
    
    
'''
Main
'''
if __name__ == "__main__":
    main()


os._exit(1)
