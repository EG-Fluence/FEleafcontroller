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

def hvacManfTypeSelection(hvac):

    if hvac.type == 'BlackShields':

        hvacHrRegisterList = [

            # Parameter Setting, Ratio x10
            ['heatingSetPoint',         0x000D, 0xFF, 'rw', 'r'],  # Heating stop = Heating set point + Heating deviation
            ['heatingDeviation',        0x000E, 0xFF, 'rw', 'r'],  # Heating start = Heating set point
            ['refrigSetPoint',          0x0029, 0xFF, 'rw', 'r'],  # Refrigeration stop point = Refrigeration set Point
            ['refrigDeviation',         0x002A, 0xFF, 'rw', 'r'],  # Refrigeration start point = Refrigeration set point + Refrigeration deviation

            # Sensor status, Ratio x10
            ['highTempAlarmPoint',      0x0026, 0xFF, 'rw', 'r'],
            ['lowTempAlarmPoint',       0x0028, 0xFF, 'rw', 'r'],
            ['currentCabinetTemp',      0x0066, 0xFF, 'rw', 'r'],
            ['currentCabinetHum',       0x0069, 0xFF, 'ro', 'r'],

            ['readAlarm',               0x0901, 0xFF, 'ro', 'v'],  #
            ['readTimestamp0',          0x0902, 0xFF, 'ro', 'v'],  #
            ['readTimestamp1',          0x0903, 0xFF, 'ro', 'v'],  #
            ['readTimestamp2',          0x0904, 0xFF, 'ro', 'v'],  #
            ['readTimestamp3',          0x0905, 0xFF, 'ro', 'v'],  #

            ['firmwareAlarm',           0x0906, 0xFF, 'ro', 'v'],  #
            ['heartbeat',               0x0907, 0,    'ro', 'v'],  #
            ['outerLoop',               0x0908, 0,    'rw', 'v']   #
        ]

        hvacCoRegisterList = [

            # Alarm Status（1: alarm, 0: no alarm）
            ['highAndLowPressureAlarm', 0x0029, 0,      'ro', 'r'],  # 1: alarm, 0: no alarm
            ['sensorFailure',           0x002E, 0,      'ro', 'r'],  # 1: alarm, 0: no alarm
            ['compressorFailure',       0x002F, 0,      'ro', 'r'],  # 1: alarm, 0: no alarm
            ['highAndLowVoltageAlarm',  0x0032, 0,      'ro', 'r'],  # 1: alarm, 0: no alarm
            ['highAndLowTempAlarm',     0x0033, 0,      'ro', 'r'],  # 1: alarm, 0: no alarm
            ['switchingMachine',        0x0040, 0,      'rw', 'r'],  # 1: switch on, 0: shut down
            ['readAlarm',               0x0064, 0xFF,   'ro', 'v'],  #

        ]


    else:
        hvacHrRegisterList = [
            # Version Information
            ['softwareVersion',         0x0000, 0xFF, 'ro', 'r'],

            # Running status 0:Stop, 1:Running, 2:Invalid
            ['unitRunningStatus',       0x0100, 0xFF, 'ro', 'r'],  #
            ['internalFanStatus',       0x0101, 0xFF, 'ro', 'r'],  #
            ['externalFanStatus',       0x0102, 0xFF, 'ro', 'r'],  #
            ['compressorStatus',        0x0103, 0xFF, 'ro', 'r'],  #
            ['heaterStatus',            0x0104, 0xFF, 'ro', 'r'],  #
            ['emergencyFanStatus',      0x0105, 0xFF, 'ro', 'r'],  #

            # Sensor status: （The invalid value of Temp. is 2000. The invalid value of humidity is 120. The invalid value of humidity is 32767.）
            ['evapTemp',                0x0500, 0xFF, 'ro', 'r'],  # Ratio x10
            ['outdoorTemp',             0x0501, 0xFF, 'ro', 'r'],  # Ratio x10
            ['condenserTemp',           0x0502, 0xFF, 'ro', 'r'],  # Ratio x10
            ['indoorTemp',              0x0503, 0xFF, 'ro', 'r'],  # Ratio x10
            ['humidity',                0x0504, 0xFF, 'ro', 'r'],  # Ratio x1
            ['dischargeTemp',           0x0505, 0xFF, 'ro', 'r'],  # Ratio x10
            ['acRunningCurrent',        0x0506, 0xFF, 'ro', 'r'],  # Ratio x1000
            ['acInputVoltage',          0x0507, 0xFF, 'ro', 'r'],  # Ratio x1
            ['dcInputVoltage',          0x0508, 0xFF, 'ro', 'r'],  # Ratio x10

            # Alarm Status（Normal:0, Fault:1）
            ['highTempAlarm',           0x0600, 0xFF, 'ro', 'r'],  #
            ['lowTempAlarm',            0x0601, 0xFF, 'ro', 'r'],  #
            ['highHumidAlarm',          0x0602, 0xFF, 'ro', 'r'],  #
            ['lowHumidAlarm',           0x0603, 0xFF, 'ro', 'r'],  #
            ['coilFreezeProtection',    0x0604, 0xFF, 'ro', 'r'],  #
            ['highExhaustTempAlarm',    0x0605, 0xFF, 'ro', 'r'],  #
            ['evapTempSensFail',        0x0606, 0xFF, 'ro', 'r'],  #
            ['outdoorTempSensFail',     0x0607, 0xFF, 'ro', 'r'],  #
            ['condensTempSensFail',     0x0608, 0xFF, 'ro', 'r'],  #
            ['indoorTempSensFail',      0x0609, 0xFF, 'ro', 'r'],  #
            ['exhaustTempSensFail',     0x060A, 0xFF, 'ro', 'r'],  #
            ['humidSensFail',           0x060B, 0xFF, 'ro', 'r'],  #
            ['internalFanFailAlarm',    0x060C, 0xFF, 'ro', 'r'],  #
            ['externalFanFailAlarm',    0x060D, 0xFF, 'ro', 'r'],  #
            ['compressorFailAlarm',     0x060E, 0xFF, 'ro', 'r'],  #
            ['heaterFailAlarm',         0x060F, 0xFF, 'ro', 'r'],  #
            ['emergencyFanFailAlarm',   0x0610, 0xFF, 'ro', 'r'],  #
            ['hpAlarm',                 0x0611, 0xFF, 'ro', 'r'],  #
            ['lpAlarm',                 0x0612, 0xFF, 'ro', 'r'],  #
            ['waterAlarm',              0x0613, 0xFF, 'ro', 'r'],  #
            ['fireAlarm',               0x0614, 0xFF, 'ro', 'r'],  #
            ['gatingAlarm',             0x0615, 0xFF, 'ro', 'r'],  #
            ['hpLock',                  0x0616, 0xFF, 'ro', 'r'],  #
            ['lpLock',                  0x0617, 0xFF, 'ro', 'r'],  #
            ['highExhaustTempLock',     0x0618, 0xFF, 'ro', 'r'],  #
            ['acOverVoltageAlarm',      0x0619, 0xFF, 'ro', 'r'],  #
            ['acUnderVoltageAlarm',     0x061A, 0xFF, 'ro', 'r'],  #
            ['acPowerSupplyFail',       0x061B, 0xFF, 'ro', 'r'],  #
            ['losePhaseAlarm',          0x061C, 0xFF, 'ro', 'r'],  #
            ['freqFault',               0x061D, 0xFF, 'ro', 'r'],  #
            ['antiPhaseAlarm',          0x061E, 0xFF, 'ro', 'r'],  #
            ['dcOverVoltageAlarm',      0x061F, 0xFF, 'ro', 'r'],  #
            ['dcUnderVoltageAlarm',     0x0620, 0xFF, 'ro', 'r'],  #

            # Parameter Setting
            ['refrigStopPoint',         0x0700, 0xFF, 'rw', 'r'],  # Ratio x1, 15 - 50 degC
            ['refrigBand',              0x0701, 0xFF, 'rw', 'r'],  # Ratio x1, 1 - 10 degC
            ['heatingStopPoint',        0x0702, 0xFF, 'rw', 'r'],  # Ratio x1, -15 - 15 degC
            ['heatingBand',             0x0703, 0xFF, 'rw', 'r'],  # Ratio x1, 1 - 10 degC
            ['reserve',                 0x0704, 0xFF, 'ro', 'r'],  #
            ['reserve1',                0x0705, 0xFF, 'ro', 'r'],  #
            ['highTempPoint',           0x0706, 0xFF, 'rw', 'r'],  # Ratio x1, 25 - 50 degC
            ['lowTempPoint',            0x0707, 0xFF, 'rw', 'r'],  # Ratio x1, -20 - 50 degC
            ['highHumPoint',            0x0708, 0xFF, 'rw', 'r'],  # Ratio x1, 0 - 100 percent
            ['internFanStopPoint',      0x070A, 0xFF, 'rw', 'r'],  # Ratio x1, -20 - 50 degC

            ['remoteControl',           0x0801, 0xFF, 'rw', 'r'],  # 1：Open, 0: Close

            ['readAlarm',               0x0901, 0xFF, 'ro', 'v'],  #
            ['readTimestamp0',          0x0902, 0xFF, 'ro', 'v'],  #
            ['readTimestamp1',          0x0903, 0xFF, 'ro', 'v'],  #
            ['readTimestamp2',          0x0904, 0xFF, 'ro', 'v'],  #
            ['readTimestamp3',          0x0905, 0xFF, 'ro', 'v'],  #

            ['firmwareAlarm',           0x0906, 0xFF, 'ro', 'v'],  #
            ['heartbeat',               0x0907, 0,    'ro', 'v'],  #
            ['outerLoop',               0x0908, 0,    'rw', 'v']  #
        ]

        hvacCoRegisterList = None

    return hvacHrRegisterList, hvacCoRegisterList


class HvacDevice(ModbusDevice):
    def __init__(self, deviceSettings):
        hvacHrRegisterList, hvacCoRegisterList = hvacManfTypeSelection(deviceSettings)
        super(HvacDevice, self).__init__(deviceSettings,
                                         coRegisterList=hvacCoRegisterList,
                                         diRegisterList=None,
                                         hrRegisterList=hvacHrRegisterList,
                                         irRegisterList=None)