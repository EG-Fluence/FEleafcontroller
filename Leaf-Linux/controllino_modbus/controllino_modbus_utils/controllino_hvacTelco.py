# -*- coding: utf-8 -*-
"""
Created on Tue Feb  2 13:54:11 2021

@author: Georg.Kordowich
"""

from controllino_modbus_utils.controllino_generic_modbus_device import ModbusDevice

'''
The following List contains all signal names, addresses and also contains dummy values for the hvac clients.
It is used to initialize the datastore for the values which are read via modbus. Ratios are mentioned in comments.
A ratio of x10 means that a read value of 235 = 23.5 degC

Description of the columns
1 - Name
2 - Address
3 - not updated value
4 - ro or rw flag
5 - 'r' real device register, read from device  
    'v' virtual device register, will be used only inside Modbus server
'''

def hvacTelcoTypeSelection(hvacTelco):

    hvacTelcoHrRegisterList = None
    hvacTelcoCoRegisterList = None
    hvacTelcoDiRegisterList = None

    if hvacTelco.type == 'nVentTCPIP':

        hvacTelcoHrRegisterList = [
            # Version Information
            ['coolSp',                0x0000, 0xFF, 'rw', 'r'], # Value is multiple by 10.
            ['coolDif',               0x0001, 0xFF, 'rw', 'r'], # Value is multiple by 10.
            ['heatSp',                0x0002, 0xFF, 'rw', 'r'], # Value is multiple by 10.
            ['heatDif',               0x0003, 0xFF, 'rw', 'r'], # Value is multiple by 10.
            ['hiTempAlarm',           0x0004, 0xFF, 'rw', 'r'], # Value is multiple by 10.
            ['loTempAlarm',           0x0005, 0xFF, 'rw', 'r'], # Value is multiple by 10.
            ['unitId1',               0x0006, 0xFF, 'rw', 'r'], # Holds 2 ASCII chars.
            ['unitId2',               0x0007, 0xFF, 'rw', 'r'], # Holds 2 ASCII chars.
            ['unitId3',               0x0008, 0xFF, 'rw', 'r'], # Holds 2 ASCII chars.
            ['unitId4',               0x0009, 0xFF, 'rw', 'r'], # Holds 2 ASCII chars.
                                                          #
            ['inletTemp',             0x001E, 0xFF, 'ro', 'r'], # Value is multiple by 10.
            ['outletTemp',            0x001F, 0xFF, 'ro', 'r'], # Value is multiple by 10.
            ['coolMin',               0x0020, 0xFF, 'ro', 'r'], # Value is multiple by 10.
            ['coolMax',               0x0021, 0xFF, 'ro', 'r'], # Value is multiple by 10.
            ['heatMin',               0x0022, 0xFF, 'ro', 'r'], # Value is multiple by 10.
            ['heatMax',               0x0023, 0xFF, 'ro', 'r'], # Value is multiple by 10.
            ['commVers',              0x0024, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['carelVers',             0x0025, 0xFF, 'ro', 'r'], # Digit
            ['cbSerial1',             0x0026, 0xFF, 'ro', 'r'], # Holds 4 Hex values.
            ['cbSerial2',             0x0027, 0xFF, 'ro', 'r'], # Holds 4 Hex values.
            ['cbSerial3',             0x0028, 0xFF, 'ro', 'r'], # Holds 4 Hex values.
            ['unitSerial1',           0x0029, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitSerial2',           0x002A, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitSerial3',           0x002B, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitSerial4',           0x002C, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitSerial5',           0x002D, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitSerial6',           0x002E, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitSerial7',           0x002F, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitSerial8',           0x0030, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitSerial9',           0x0031, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitModel1',            0x0032, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitModel2',            0x0033, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitModel3',            0x0034, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitModel4',            0x0035, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitModel5',            0x0036, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitModel6',            0x0037, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitModel7',            0x0038, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.
            ['unitModel8',            0x0039, 0xFF, 'ro', 'r'], # Holds 2 ASCII chars.

            ['readAlarm',             0x0040, 0xFF, 'ro', 'v'], #
            ['readTimestamp0',        0x0041, 0xFF, 'ro', 'v'], #
            ['readTimestamp1',        0x0042, 0xFF, 'ro', 'v'], #
            ['readTimestamp2',        0x0043, 0xFF, 'ro', 'v'], #
            ['readTimestamp3',        0x0044, 0xFF, 'ro', 'v'], #
            ['heartbeat',             0x0045, 0,    'ro', 'v']  #
                                                 ]
        hvacTelcoCoRegisterList = [
            # Version Information
            ['unitOfMeasure',           0x0000, 0xFF, 'ro', 'r'], # 0 = degree C, 1 = degree F
            ['frostAlarm',              0x0001, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On
            ['highTempAlarm',           0x0002, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On
            ['lowTempAlarm',            0x0003, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On
            ['inletSensorAlarm',        0x0004, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On
            ['outletSensorAlarm',       0x0005, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On
            ['doorOrSmokeAlarm',        0x0006, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On
            ['highPressureAlarm',       0x0007, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On
            ['controllerCommFail',      0x0008, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On

            ['readAlarm',               0x000A, 0,    'ro', 'v'] # readAlarm
                                                        ]
        hvacTelcoDiRegisterList = [
            # Version Information
            ['unitOfMeasure',           0x0000, 0xFF, 'ro', 'r'], # 0 = degree C, 1 = degree F
            ['frostAlarm',              0x0001, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On
            ['highTempAlarm',           0x0002, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On
            ['lowTempAlarm',            0x0003, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On
            ['inletSensorAlarm',        0x0004, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On
            ['outletSensorAlarm',       0x0005, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On
            ['doorOrSmokeAlarm',        0x0006, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On
            ['highPressureAlarm',       0x0007, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On
            ['controllerCommFail',      0x0008, 0xFF, 'ro', 'r'], # 0 = Alarm Off, 1 = Alarm On

            ['readAlarm',               0x000A, 0,    'ro', 'v']  # readAlarm
                                                        ]

    else:
        if hvacTelco.type == 'nVentRS485':

            hvacTelcoHrRegisterList = [
                ['coolingTemperatureSetpoint',             51, 0xFF, 'rw', 'r'],
                ['coolingTemperatureDifferential',         54, 0xFF, 'rw', 'r'],
                ['highTemperatureSetpoint',                60, 0xFF, 'rw', 'r'],
                ['lowTemperatureSetpoint',                 63, 0xFF, 'rw', 'r'],
                ['heatingSetpoint',                        68, 0xFF, 'rw', 'r'],
                ['heatingTemperatureDifferentialSetpoint', 71, 0xFF, 'rw', 'r'],

                ['evaporatorFanStartUpDelayTimeBeforeCompressorStarts',  43, 0xFF, 'ro', 'r'],
                ['evaporatorFanStopDelayTimeAfterCompressorStops',       44, 0xFF, 'ro', 'r'],

                ['readAlarm',             0x0040, 0xFF, 'ro', 'v'],  #
                ['readTimestamp0',        0x0041, 0xFF, 'ro', 'v'],  #
                ['readTimestamp1',        0x0042, 0xFF, 'ro', 'v'],  #
                ['readTimestamp2',        0x0043, 0xFF, 'ro', 'v'],  #
                ['readTimestamp3',        0x0044, 0xFF, 'ro', 'v'],  #
                ['heartbeat',             0x0045, 0,    'ro', 'v']   #
                                                        ]

            hvacTelcoDiRegisterList = None

            #     hvacTelcoDiRegisterList = [
            #     ['readAlarm', 0x000A, 0, 'ro', 'v']  # readAlarm
            # ]

            hvacTelcoCoRegisterList = [
                ['hysteresisOperatingScheme',  19, 0xFF, 'ro', 'r'],
                ['alarmRelayOutput',           21, 0xFF, 'ro', 'r'],
                ['doorOpenSwitchControlling',  22, 0xFF, 'ro', 'r'],
                ['temperatureScaleSelection',  26, 0xFF, 'ro', 'r'],
                ['alarmHistoryReset',          27, 0xFF, 'ro', 'r'],
                ['factoryDefaultReset',        28, 0xFF, 'ro', 'r'],

                ['readAlarm',               0x000A, 0,    'ro', 'v']  # readAlarm
                                                        ]

        else:
            if hvacTelco.type is not None:
                print('\r\nERROR ========== Unknown HVAC Telco Model')
                exit()

    return hvacTelcoCoRegisterList, hvacTelcoDiRegisterList, hvacTelcoHrRegisterList


class HvacTelcoDevice(ModbusDevice):
    def __init__(self, deviceSettings):
        hvacTelcoCoRegisterList, hvacTelcoDiRegisterList, hvacTelcoHrRegisterList = hvacTelcoTypeSelection(deviceSettings)
        super(HvacTelcoDevice, self).__init__(deviceSettings,
            coRegisterList=hvacTelcoCoRegisterList,
            diRegisterList=hvacTelcoDiRegisterList,
            hrRegisterList=hvacTelcoHrRegisterList,
            irRegisterList=None)





