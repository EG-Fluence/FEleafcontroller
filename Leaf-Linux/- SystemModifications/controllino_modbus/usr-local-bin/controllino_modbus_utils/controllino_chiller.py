# -*- coding: utf-8 -*-
"""
Created on Tue Feb  2 14:05:07 2021

@author: Georg.Kordowich
"""

from controllino_modbus_utils.controllino_generic_modbus_device import ModbusDevice

'''
The following List contains all signal names, addresses and also contains dummy values for the chiller clients.

1. Each register holds two bytes. 
2. Data transmission mode: high byte first, low byte last. 
3. If the register content is about temperature, the relationship between the register value and the actual
    temperature is 10 times. For example, if the register value is 200, the actual temperature is 20.0 degC. If the register
    value is 236, the actual temperature is 23.6 degC. 
4. If the register content is pressure, the relationship between the register value and the actual pressure is 100
    times. For example, if the register value is 200, the actual pressure is 2Bar. If the register value is 236, the actual
    pressure is 2.36Bar.
    
Description of the columns
1 - Name
2 - Address
3 - not updated value
4 - ro or rw flag
5 - 'r' real device register, read from device  
    'v' virtual device register, will be used only inside Modbus server
'''

def ChillerTypeSelection(Chiller):

    chillerHrRegisterList = None

    if Chiller.type == 'EMW75HDNC1A':
        chillerHrRegisterList = [

        # On/off status
        ['systemOnOff',                 0x0400, 0xFF, 'rw', 'r'],      # 0 OFF 1 ON

        # Setting status
        ['modeSelection',               0x0401, 0xFF, 'rw', 'r'],      # 0 OFF, 1 Inner loop, 2 cooling, 3 heating, 4 automatic 5 water supply

        ['waterTempSet',                0x8B0C, 0xFF, 'rw', 'r'],      # Keep one decimal place, if you want to set 23.5 degC, write 235 in this register
        ['waterHeatingTempSet',         0x8B0D, 0xFF, 'rw', 'r'],      # Keep one decimal place, if you want to set 23.5 degC, write 235 in this register
        ['hysteresisSet',               0x8B0E, 0xFF, 'rw', 'r'],      # Retain a decimal, if you want to set 1.5 degC, write 15 in this register
        ['hysteresisHeatingSet',        0x8B0F, 0xFF, 'rw', 'r'],      # Retain a decimal, if you want to set 1.5 degC, write 15 in this register

        ['flowRateSelection',           0x9809, 0xFF, 'rw', 'r'],      # Range [0, 2]   0:60L/min   1:80L/min   2:40L/min

        # Temperature Sensor Status
        ['supplyWaterTemp',             0xA000, 0xFF, 'ro', 'r'],      # If the value read is 235, it means 23.5 degC, keep one decimal place
        ['returnWaterTemp',             0xA002, 0xFF, 'ro', 'r'],      # If the value read is 235, it means 23.5 degC, keep one decimal place
        ['environmentTemp',             0xA00D, 0xFF, 'ro', 'r'],      # Optional temperature and humidity sensor
        ['inletWaterPressure',          0xA00E, 0xFF, 'ro', 'r'],      # If the value read is 235, it represents 2.35 Bar, with two decimal places
        ['outletWaterPressure',         0xA00F, 0xFF, 'ro', 'r'],      # If the value read is 235, it represents 2.35 Bar, with two decimal places

        # Alarm status
        ['outletHighWaterTemp',         0xB100, 0xFF, 'ro', 'r'],      # 0 normal, 1 alarm
        ['outletLowWaterTemp',          0xB101, 0xFF, 'ro', 'r'],      # 0 normal, 1 alarm
        ['outletWaterTempSensFail',     0xB102, 0xFF, 'ro', 'r'],      # 0 normal, 1 fault
        ['returnWaterTempSensFail',     0xB104, 0xFF, 'ro', 'r'],      # 0 normal, 1 fault
        ['heatingFail',                 0xB10B, 0xFF, 'ro', 'r'],      # 0 normal, 1 fault
        ['pumpFail',                    0xB111, 0xFF, 'ro', 'r'],      # 0 normal, 1 fault
        ['inverterComFail',             0xB115, 0xFF, 'ro', 'r'],      # 0 normal, 1 fault
        ['highSystemPressAlarm',        0xB11C, 0xFF, 'ro', 'r'],      # 0 normal, 1 fault


        ['highOutletPressAlarm',        0xB130, 0xFF, 'ro', 'r'],      # 0 normal, 1 fault
        ['WaterReplenishmentAlarm',     0xB132, 0xFF, 'ro', 'r'],      # 0 normal, 1 lack of fluid
        ['sysHighVoltageLock',          0xB137, 0xFF, 'ro', 'r'],      # 0 normal, 1 locked
        ['sysLowVoltageLock',           0xB138, 0xFF, 'ro', 'r'],      # 0 normal, 1 locked
        ['exhaustGasHighTempLock',      0xB139, 0xFF, 'ro', 'r'],      # 0 normal, 1 locked
        ['inverterOverCurrentLock',     0xB13A, 0xFF, 'ro', 'r'],      # 0 normal, 1 locked
        ['inverterOverTempLock',        0xB13B, 0xFF, 'ro', 'r'],      # 0 normal, 1 locked
        ['inverterOverVoltLock',        0xB13C, 0xFF, 'ro', 'r'],      # 0 normal, 1 locked
        ['inverterUnderVoltLock',       0xB13D, 0xFF, 'ro', 'r'],      # 0 normal, 1 locked
        ['inverterPhaseLossLock',       0xB13E, 0xFF, 'ro', 'r'],      # 0 normal, 1 locked
        ['inverterOtherFaultLock',      0xB13F, 0xFF, 'ro', 'r'],      # 0 normal, 1 locked

        ['heatingFaultLock',            0xB142, 0xFF, 'ro', 'r'],      # 0 normal, 1 locked

        # Pump Information
        ['pumpCommandSpeed',            0xA201, 0xFF, 'ro', 'r'],      # f the value read is 300, it means that the pump is commanded to work at 30% speed, range [0, 1000]


        # Not implemented
        ['heartbeat',                   0x8004, 0xFF, 'ro', 'r'],      #
        ['password',                    0x8100, 0xFF, 'rw', 'r'],      #

        ['comprGearSelec',              0x8928, 0xFF, 'rw', 'r'],      #
        ['comprSpeedSelec',             0x890A, 0xFF, 'rw', 'r'],      #

        ['baudrate',                    0x8A09, 0xFF, 'rw', 'r'],      #
        ['id',                          0x9000, 0xFF, 'rw', 'r'],      #

        ['softwarePartNo',              0xFE51, 0xFF, 'ro', 'r'],      #
        ['softwareVersNo',              0xFE52, 0xFF, 'ro', 'r'],      #

        ['readAlarm',                   0xFE53, 0xFF, 'ro', 'v'], #
        ['readTimestamp0',              0xFE54, 0xFF, 'ro', 'v'], #
        ['readTimestamp1',              0xFE55, 0xFF, 'ro', 'v'], #
        ['readTimestamp2',              0xFE56, 0xFF, 'ro', 'v'], #
        ['readTimestamp3',              0xFE57, 0xFF, 'ro', 'v'], #

        ['firmwareAlarm',               0xFE58, 0xFF, 'ro', 'v'], #
        ['heartbeatCube',               0xFE59, 0xFF, 'rw', 'v']  #
                                                        ]
    else:
        if Chiller.type == 'RC8057G1':
            chillerHrRegisterList = [
            # On/off status
            ['systemOnOff', 0x0400, 0xFF, 'rw', 'r'],  # 0 OFF 1 ON

            ['modeSelection', 0x0401, 0xFF, 'rw', 'r'],
            ['faultReset',    0x0402, 0xFF, 'rw', 'r'],

            ['coolingSetPoint',                     0xE600, 0xFF, 'rw', 'r'],
            ['heatingSetPoint',                     0xE601, 0xFF, 'rw', 'r'],
            ['coolingDeltaT',                       0xE602, 0xFF, 'rw', 'r'],
            ['heatingDeltaT',                       0xE603, 0xFF, 'rw', 'r'],
            ['pumpGearSelection',                   0xE604, 0xFF, 'rw', 'r'],
            ['ID',                                  0xE605, 0xFF, 'rw', 'r'],
            ['baudRate',                            0xE606, 0xFF, 'rw', 'r'],
            ['stopPosition',                        0xE607, 0xFF, 'rw', 'r'],
            ['compressorGearSelection',             0xE608, 0xFF, 'rw', 'r'],

            ['protocolControlModeSelection',        0xE60A, 0xFF, 'wo', 'r'],

            ['outletWaterTemperature',              0xE700, 0xFF, 'ro', 'r'],
            ['inletWaterTemperature',               0xE701, 0xFF, 'ro', 'r'],
            ['inletWaterPressure',                  0xE704, 0xFF, 'ro', 'r'],
            ['outletWaterPressure',                 0xE705, 0xFF, 'ro', 'r'],
            ['highOutletWaterTemperature',          0xE706, 0xFF, 'ro', 'r'],
            ['lowOutletWaterTemperature',           0xE707, 0xFF, 'ro', 'r'],
            ['outletWaterTemperatureSensorFault',   0xE708, 0xFF, 'ro', 'r'],
            ['inletWaterTemperatureSensorFault',    0xE709, 0xFF, 'ro', 'r'],

            ['systemOverPressureAlarm',             0xE718, 0xFF, 'ro', 'r'],
            ['outletWaterOverPressureAlarm',        0xE719, 0xFF, 'ro', 'r'],
            ['currentPumpSeed',                     0xE71A, 0xFF, 'ro', 'r'],
            ['pumpStatus',                          0xE71B, 0xFF, 'ro', 'r'],
            ['heartbeat',                           0xE71C, 0xFF, 'ro', 'r'],
            ['compressorStatus',                    0xE71D, 0xFF, 'ro', 'r'],

            ['softwarePartNumber',                  0xE71E, 0xFF, 'ro', 'r'],
            ['softwareVersion',                     0xE71F, 0xFF, 'ro', 'r'],

            ['totalCompressorOperatingTime',        0xE720, 0xFF, 'ro', 'r'],
            ['compressorOpeningTimes',              0xE722, 0xFF, 'ro', 'r'],
            ['totalElectricHeatingOperatingTime',   0xE724, 0xFF, 'ro', 'r'],
            ['electricHeatingOperatingTime',        0xE726, 0xFF, 'ro', 'r'],
            ['totalWaterPumpOperatingTime',         0xE728, 0xFF, 'ro', 'r'],
            ['waterPumpOpeningTimes',               0xE72A, 0xFF, 'ro', 'r'],
            ['totalExternalFanOperatingTime',       0xE72C, 0xFF, 'ro', 'r'],
            ['externalFanOpeningTimes',             0xE72E, 0xFF, 'ro', 'r'],

            ['currentSystemMode',                   0xE730, 0xFF, 'ro', 'r'],
            ['currentProtocolControlMode',          0xE731, 0xFF, 'ro', 'r'],

            ['readAlarm',                   0xFE53, 0xFF, 'ro', 'v'], #
            ['readTimestamp0',              0xFE54, 0xFF, 'ro', 'v'], #
            ['readTimestamp1',              0xFE55, 0xFF, 'ro', 'v'], #
            ['readTimestamp2',              0xFE56, 0xFF, 'ro', 'v'], #
            ['readTimestamp3',              0xFE57, 0xFF, 'ro', 'v'], #

            ['firmwareAlarm',               0xFE58, 0xFF, 'ro', 'v'], #
            ['heartbeatCube',               0xFE59, 0xFF, 'rw', 'v']  #
        ]
        else:
            if Chiller.type is not None:
                print('\r\nERROR ========== Unknown Chiller Envicool Model')
                exit()

    return chillerHrRegisterList


class ChillerDevice(ModbusDevice):
    def __init__(self, deviceSettings):
        chillerHrRegisterList = ChillerTypeSelection(deviceSettings)
        super(ChillerDevice, self).__init__(deviceSettings, 
            coRegisterList = None, 
            diRegisterList = None, 
            hrRegisterList = chillerHrRegisterList, 
            irRegisterList = None)

