# -*- coding: utf-8 -*-
"""
Created on Tue Feb  2 14:21:04 2021

@author: Georg.Kordowich
"""

from controllino_modbus_utils.controllino_generic_modbus_device import ModbusDevice

'''
The following List contains all signal names, addresses and also contains dummy values for the controllino client Input Registers.
It is used to initialize the datastore for the values which are read via modbus.

Description of the columns
1 - Name
2 - Address
3 - not updated value
4 - ro or rw flag
5 - 'r' real device register, read from device  
    'v' virtual device register, will be used only inside Modbus server
'''
controllinoIrRegisterList = [
    ['door1', 0x0000, 0xFF, 'ro', 'r'],  # door #1 - Cube
    ['door2', 0x0001, 0xFF, 'ro', 'r'],  # door #2 - Cube
    ['reserved1', 0x0002, 0xFF, 'ro', 'r'],  # reserved
    ['CoreIMD', 0x0003, 0xFF, 'ro', 'r'],  # reserved
    ['fStopButton', 0x0004, 0xFF, 'ro', 'r'],  # reserved
    ['bmsFstopButton', 0x0005, 0xFF, 'ro', 'r'],  # reserved
    ['preFirePanelSignalPoint', 0x0006, 0xFF, 'ro', 'r'],  # reserved
    ['mainFstop', 0x0007, 0xFF, 'ro', 'r'],  # reserved
    ['fstopK1Relay', 0x0008, 0xFF, 'ro', 'r'],  # F-Stop K1 relay (CUBE)
    ['fstopPushButton', 0x0009, 0xFF, 'ro', 'r'],  # F-Stop push-button (CUBE)
    ['leakageSens', 0x000A, 0xFF, 'ro', 'r'],  # leakage sensor
    ['powerSupp', 0x000B, 0xFF, 'ro', 'r'],  # 24V power supply
    ['spf', 0x000C, 0xFF, 'ro', 'r'],  # RESERVED
    ['ups', 0x000D, 0xFF, 'ro', 'r'],  # RESERVED
    ['hvac', 0x000E, 0xFF, 'ro', 'r'],  # RESERVED
    ['mvTransPress', 0x000F, 0xFF, 'ro', 'r'],  # RESERVED
    ['mvTransOilLevel', 0x0010, 0xFF, 'ro', 'r'],  # SPARE
    ['mvTransOilTemp', 0x0011, 0xFF, 'ro', 'r'],  # SPARE
    ['mvTransWindingTemp', 0x0012, 0xFF, 'ro', 'r'],  # SPARE
    ['tempSens1', 0x0013, 0xFF, 'ro', 'r'],  # temp sensor 1
    ['tempSens2', 0x0014, 0xFF, 'ro', 'r'],  # temp sensor 2
    ['tempSens3', 0x0015, 0xFF, 'ro', 'r'],  # temp sensor 3
    ['reserved11', 0x0016, 0xFF, 'ro', 'r'],  # RESERVED
    ['reserved12', 0x0017, 0xFF, 'ro', 'r'],  # RESERVED
    ['reserved13', 0x0018, 0xFF, 'ro', 'r'],  # RESERVED
    ['heartbeat', 0x0019, 0xFF, 'ro', 'r'],  # Heartbeat

    ['readAlarm', 0x001A, 0xFF, 'ro', 'v'],  # readAlarm
    ['readTimestamp0', 0x001B, 0xFF, 'ro', 'v'],  # Timestamp
    ['readTimestamp1', 0x001C, 0xFF, 'ro', 'v'],  # Timestamp
    ['readTimestamp2', 0x001D, 0xFF, 'ro', 'v'],  # Timestamp
    ['readTimestamp3', 0x001E, 0xFF, 'ro', 'v'],  # Timestamp


    # Aggregation variables implemented as virtual
    ['aggTripAlarmCore', 0x182A, 0xAB, 'ro', 'v'],  # 6186, Aggregation of CoreIMD, fStopButton, bmsFstopButton, preFirePanelSignalPoint, mainFstop
    ['aggTripAlarmCube', 0x182B, 0xBA, 'ro', 'v']   # 6187, Aggregation of fstopK1Relay, fstopPushButton

]

'''
The following List contains all signal names, addresses and also contains dummy values for the controllino client Coils.
It is used to initialize the datastore for the values which are read via modbus.
'''
controllinoCoRegisterList = [
    ['powerCycling', 0x0000, 0, 'rw', 'r'],
    ['ledFlashLight', 0x0001, 0, 'rw', 'r'],
    ['reserved2', 0x0002, 0, 'rw', 'r'],
    ['reserved3', 0x0003, 0, 'rw', 'r'],
    ['reserved4', 0x0004, 0, 'rw', 'r'],
    ['reserved5', 0x0005, 0, 'rw', 'r'],
    ['reserved6', 0x0006, 0, 'rw', 'r'],
    ['reserved7', 0x0007, 0, 'rw', 'r'],
    ['reserved8', 0x0008, 0, 'rw', 'r'],
    ['reserved9', 0x0009, 0, 'rw', 'r'],
    ['reserved10', 0x000A, 0, 'rw', 'r'],
    ['reserved11', 0x000B, 0, 'rw', 'r'],
    ['reserved12', 0x000C, 0, 'rw', 'r'],
    ['reserved13', 0x000D, 0, 'rw', 'r'],
    ['reserved14', 0x000E, 0, 'rw', 'r'],
    ['reserved15', 0x000F, 0, 'rw', 'r'],
    ['readAlarm', 0x0020, 0, 'ro', 'v']  # readAlarm
]


class ControllinoDevice(ModbusDevice):
    def __init__(self, deviceSettings):
        super(ControllinoDevice, self).__init__(deviceSettings,
                                                coRegisterList=controllinoCoRegisterList,
                                                diRegisterList=None,
                                                hrRegisterList=None,
                                                irRegisterList=controllinoIrRegisterList,
                                                assignVirtualRegisters=self.assignVirtualRegisters)

    def assignVirtualRegisters(self, coRegisterList, diRegisterList, hrRegisterList, irRegisterList):
        if ((irRegisterList.loc['CoreIMD', 'value'] == 0) and
                (irRegisterList.loc['fStopButton', 'value'] == 0) and
                (irRegisterList.loc['bmsFstopButton', 'value'] == 0) and
                (irRegisterList.loc['preFirePanelSignalPoint', 'value'] == 0) and
                (irRegisterList.loc['mainFstop', 'value'] == 0)):
            irRegisterList.loc['aggTripAlarmCore', 'value'] = 0
        else:
            irRegisterList.loc['aggTripAlarmCore', 'value'] = 1

        if ((irRegisterList.loc['fstopK1Relay', 'value'] == 0) and
                (irRegisterList.loc['fstopPushButton', 'value'] == 0)):
            irRegisterList.loc['aggTripAlarmCube', 'value'] = 0
        else:
            irRegisterList.loc['aggTripAlarmCube', 'value'] = 1
