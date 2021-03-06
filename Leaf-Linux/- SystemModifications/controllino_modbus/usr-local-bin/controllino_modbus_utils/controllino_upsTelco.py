# -*- coding: utf-8 -*-
"""
Created on Tue Feb  2 14:05:10 2021

@author: Georg.Kordowich
"""

from controllino_modbus_utils.controllino_generic_modbus_device import ModbusDevice

'''
The following List contains all signal names, addresses and also contains dummy values for the ups client.
It is used to initialize the datastore for the values which are read via modbus.

Description of the columns
1 - Name
2 - Address
3 - not updated value
4 - ro or rw flag
5 - 'r' real device register, read from device  
    'v' virtual device register, will be used only inside Modbus server
'''
upsTelcoHrRegisterList = [
    ['fwVersion',                               0x0004, 0xFF, 'ro', 'r'],
    ['serialNumber0',                           0x0005, 0xFF, 'ro', 'r'],
    ['serialNumber1',                           0x0006, 0xFF, 'ro', 'r'],
    ['serialNumber2',                           0x0007, 0xFF, 'ro', 'r'],
    ['serialNumber3',                           0x0008, 0xFF, 'ro', 'r'],
    
    ['userDeviceName0',                         0x1000, 0xFF, 'ro', 'r'],
    ['userDeviceName1',                         0x1001, 0xFF, 'ro', 'r'],
    ['userDeviceName2',                         0x1002, 0xFF, 'ro', 'r'],
    ['userDeviceName3',                         0x1003, 0xFF, 'ro', 'r'],
    ['userDeviceName4',                         0x1004, 0xFF, 'ro', 'r'],
    ['userDeviceName5',                         0x1005, 0xFF, 'ro', 'r'],
    ['userDeviceName6',                         0x1006, 0xFF, 'ro', 'r'],
    ['userDeviceName7',                         0x1007, 0xFF, 'ro', 'r'],
    ['userDeviceName8',                         0x1008, 0xFF, 'ro', 'r'],
    ['userDeviceName9',                         0x1009, 0xFF, 'ro', 'r'],
    ['userDeviceName10',                        0x100A, 0xFF, 'ro', 'r'],
    ['userDeviceName11',                        0x100B, 0xFF, 'ro', 'r'],
    ['userDeviceName12',                        0x100C, 0xFF, 'ro', 'r'],
    ['userDeviceName13',                        0x100D, 0xFF, 'ro', 'r'],
    ['userDeviceName14',                        0x100E, 0xFF, 'ro', 'r'],
    ['userDeviceName15',                        0x100F, 0xFF, 'ro', 'r'],
    
    ['userSystemName0',                         0x1020, 0xFF, 'ro', 'r'],
    ['userSystemName1',                         0x1021, 0xFF, 'ro', 'r'],
    ['userSystemName2',                         0x1022, 0xFF, 'ro', 'r'],
    ['userSystemName3',                         0x1023, 0xFF, 'ro', 'r'],
    ['userSystemName4',                         0x1024, 0xFF, 'ro', 'r'],
    ['userSystemName5',                         0x1025, 0xFF, 'ro', 'r'],
    ['userSystemName6',                         0x1026, 0xFF, 'ro', 'r'],
    ['userSystemName7',                         0x1027, 0xFF, 'ro', 'r'],
    ['userSystemName8',                         0x1028, 0xFF, 'ro', 'r'],
    ['userSystemName9',                         0x1029, 0xFF, 'ro', 'r'],
    ['userSystemName10',                        0x102A, 0xFF, 'ro', 'r'],
    ['userSystemName11',                        0x102B, 0xFF, 'ro', 'r'],
    ['userSystemName12',                        0x102C, 0xFF, 'ro', 'r'],
    ['userSystemName13',                        0x102D, 0xFF, 'ro', 'r'],
    ['userSystemName14',                        0x102E, 0xFF, 'ro', 'r'],
    ['userSystemName15',                        0x102F, 0xFF, 'ro', 'r'],
    
    
    ['setParameters',                           0x1040, 0xFF, 'rw', 'r'],
    ['setParameters_REG2',                      0x1041, 0xFF, 'rw', 'r'],
    ['setSignalingCodeDO1',                     0x1042, 0xFF, 'rw', 'r'],
    ['setSignalingCodeDO1_REG2',                0x1043, 0xFF, 'rw', 'r'],
    ['setSignalingCodeDO2',                     0x1044, 0xFF, 'rw', 'r'],
    ['setSignalingCodeDO2_REG2',                0x1045, 0xFF, 'rw', 'r'],
    ['setSignalingCodeDO3',                     0x1046, 0xFF, 'rw', 'r'],
    ['setSignalingCodeDO3_REG2',                0x1047, 0xFF, 'rw', 'r'],
    
    ['setFunctionCodeDI1',                      0x104A, 0xFF, 'rw', 'r'],
    ['setFunctionCodeDI2',                      0x104B, 0xFF, 'rw', 'r'],
    
    ['setChargeCurrentUPS',                     0x104D, 0xFF, 'rw', 'r'],
    ['setChargeAbsorbtionVoltage',              0x104E, 0xFF, 'rw', 'r'],
    ['setChargeEndVoltage',                     0x104F, 0xFF, 'rw', 'r'],
    ['setBatteryTempCoefficient',               0x1050, 0xFF, 'rw', 'r'],
    ['setDischargeBatteryEndvoltage',           0x1051, 0xFF, 'rw', 'r'],
    
    ['setSwitchingThresholdInpVoltageMin',      0x1056, 0xFF, 'rw', 'r'],
    ['setSwitchingThresholdInpVoltageMax',      0x1057, 0xFF, 'rw', 'r'],
    ['setBatmodeReturnToMainsTime',             0x1058, 0xFF, 'rw', 'r'],
    ['setCustomBuffertime',                     0x1059, 0xFF, 'rw', 'r'],
    ['setBatmodeDelaytime1PcShutdown',          0x105A, 0xFF, 'rw', 'r'],
    ['setBatmodeDelayTime2',                    0x105B, 0xFF, 'rw', 'r'],
    ['setBatmodeDelayTime3',                    0x105C, 0xFF, 'rw', 'r'],
    ['setPcModeShutdownTime',                   0x105D, 0xFF, 'rw', 'r'],
    ['setPcModeResetTime',                      0x105E, 0xFF, 'rw', 'r'],
    ['setSignalingTimeAfterSwitchOff  ',        0x105F, 0xFF, 'rw', 'r'],
    
    ['setThresholdBufferReady',                 0x1061, 0xFF, 'rw', 'r'],
    ['setBatmodeUsableCapacity',                0x1062, 0xFF, 'rw', 'r'],
    ['setUserInstalledPeripherie',              0x1063, 0xFF, 'rw', 'r'],
    ['SetBatUserInstalledCapacityNominal',      0x1064, 0xFF, 'rw', 'r'],
    
    ['SetTestIntervalBatConductance',           0x1067, 0xFF, 'rw', 'r'],
    ['SetBatAlarmUserReplaceTime',              0x1068, 0xFF, 'rw', 'r'],
    ['SetBatAlarmSOCVoltage',                   0x1069, 0xFF, 'rw', 'r'],
    ['SetBatAlarmSOCPercent',                   0x106A, 0xFF, 'rw', 'r'],
    ['SetBatAlarmSOCTime',                      0x106B, 0xFF, 'rw', 'r'],
    ['SetBatAlarmSOHPercent',                   0x106C, 0xFF, 'rw', 'r'],
    ['SetBatAlarmSOHTime',                      0x106D, 0xFF, 'rw', 'r'],
    ['SetBatWarningSocVoltage',                 0x106E, 0xFF, 'rw', 'r'],
    ['SetBatWarningSocPercent',                 0x106F, 0xFF, 'rw', 'r'],
    
    ['SetBatWarningSocTime',                    0x1070, 0xFF, 'rw', 'r'],
    ['SetBatWarningSohPercent',                 0x1071, 0xFF, 'rw', 'r'],
    ['SetBatWarningSohTime',                    0x1072, 0xFF, 'rw', 'r'],
    ['SetBatteryWarningDeltaTemperature',       0x1073, 0xFF, 'rw', 'r'],
    ['SetModeSelectorSwitch',                   0x1074, 0xFF, 'rw', 'r'],
    
    ['SetEnableDisableFunction',                0x1076, 0xFF, 'rw', 'r'],
    ['SetEnableDisableFunction_REG2',           0x1077, 0xFF, 'rw', 'r'],
    ['SetBatteryInternalResistorMax',           0x1078, 0xFF, 'rw', 'r'],
    ['SetResistorBetweenUpsAndBattery',         0x1079, 0xFF, 'rw', 'r'],
        
    ['StatusFunctions',                         0x2000, 0xFF, 'ro', 'r'],
    ['StatusFunctions_REG2',                    0x2001, 0xFF, 'ro', 'r'],
    ['StatusInterface',                         0x2002, 0xFF, 'ro', 'r'],
    ['StatusInterface_REG2',                    0x2003, 0xFF, 'ro', 'r'],
    ['StatusActualInputVoltage',                0x2004, 0xFF, 'ro', 'r'],
    ['StatusActualInputCurrent',                0x2005, 0xFF, 'ro', 'r'],
    ['StatusActualOutputVoltage1',              0x2006, 0xFF, 'ro', 'r'],
    ['StatusActualOutputCurrent1',              0x2007, 0xFF, 'ro', 'r'],
    ['StatusBatteryActualVoltage',              0x200A, 0xFF, 'ro', 'r'],
    ['StatusBatteryChargeCurrent',              0x200B, 0xFF, 'ro', 'r'],
    ['StatusBatteryTemperature',                0x200D, 0xFF, 'ro', 'r'],
    ['StatusDeviceTemperature',                 0x200E, 0xFF, 'ro', 'r'],
    ['StatusSoc',                               0x200F, 0xFF, 'ro', 'r'],
    ['StatusSocRemainingTime',                  0x2010, 0xFF, 'ro', 'r'],
    ['StatusSocRemaningTimePcsh',               0x2012, 0xFF, 'ro', 'r'],
    ['StatusSoh',                               0x2013, 0xFF, 'ro', 'r'], 
    ['StatusSohRemainingLifetime',              0x2014, 0xFF, 'ro', 'r'], 
    ['StatusInstalledPeripherie',               0x2015, 0xFF, 'ro', 'r'], 
    ['CountOperationTime',                      0x2018, 0xFF, 'ro', 'r'], 
    ['CountUserOperationTime',                  0x201A, 0xFF, 'rw', 'r'], 
    [u'CountUserOperationTime_REG2',             0x201B, 0xFF, 'rw', 'r'], 
    ['CountSystemStart',                        0x201C, 0xFF, 'ro', 'r'], 
    ['CountSystemStart_REG2',                   0x201D, 0xFF, 'ro', 'r'], 
    ['CountUserSystemStart',                    0x201E, 0xFF, 'rw', 'r'], 
    ['CountUserSystemStart_REG2',               0x201F, 0xFF, 'rw', 'r'], 
    ['CountBatteryModeEvent',                   0x2020, 0xFF, 'ro', 'r'], 
    ['CountBatteryModeEvent_REG2',              0x2021, 0xFF, 'ro', 'r'], 
    ['CountUserBatteryModeEvent',               0x2022, 0xFF, 'rw', 'r'], 
    ['CountUserBatteryModeEvent_REG2',          0x2023, 0xFF, 'rw', 'r'], 
    ['CountBatteryModeTime',                    0x2024, 0xFF, 'ro', 'r'], 
    ['CountBatteryModeTime_REG2',               0x2025, 0xFF, 'ro', 'r'], 
    ['CountUserBatteryTime',                    0x2026, 0xFF, 'rw', 'r'], 
    ['CountUserBatteryTime_REG2',               0x2027, 0xFF, 'rw', 'r'], 
    ['CountActualBatteryModeTime',              0x2028, 0xFF, 'ro', 'r'], 
    ['CountActualBatteryModeTime_REG2',         0x2029, 0xFF, 'ro', 'r'], 
    ['CountDischargeBatteryEndvoltage',         0x202A, 0xFF, 'ro', 'r'], 
    ['CountDischargeBatteryEndvoltage_REG2',    0x202B, 0xFF, 'ro', 'r'], 
    ['CountAlarmDeviceTemperature',             0x202C, 0xFF, 'ro', 'r'], 
    ['CountAlarmDeviceTemperature_REG2',        0x202D, 0xFF, 'ro', 'r'], 
    ['CountAlarmBatteryTemperature',            0x202E, 0xFF, 'ro', 'r'], 
    ['CountAlarmBatteryTemperature_REG2',       0x202F, 0xFF, 'ro', 'r'], 
    ['CountWarningBatteryTemperature',          0x2030, 0xFF, 'ro', 'r'], 
    ['CountWarningBatteryTemperature_REG2',     0x2031, 0xFF, 'ro', 'r'], 
    ['CountAlarmOverload',                      0x2032, 0xFF, 'ro', 'r'], 
    ['CountAlarmOverload_REG2',                 0x2033, 0xFF, 'ro', 'r'], 
    ['CountAlarmService',                       0x2034, 0xFF, 'ro', 'r'], 
    ['CountAlarmService_REG2',                  0x2035, 0xFF, 'ro', 'r'], 
    ['CountTimeAfterSohExpired',                0x2036, 0xFF, 'ro', 'r'], 
    ['CountTimeAfterSohExpired_REG2',           0x2037, 0xFF, 'ro', 'r'], 
    ['StatusAnalogInput',                       0x2038, 0xFF, 'ro', 'r'], 
    ['StatusBatteryInternalResistor',           0x2039, 0xFF, 'ro', 'r'], 
    ['StatusActualAlarm',                       0x3000, 0xFF, 'ro', 'r'], 
    ['StatusActualAlarm_REG2',                  0x3001, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus1',                       0x3002, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus1_REG2',                  0x3003, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus2',                       0x3004, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus2_REG2',                  0x3005, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus3',                       0x3006, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus3_REG2',                  0x3007, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus4',                       0x3008, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus4_REG2',                  0x3009, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus5',                       0x300A, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus5_REG2',                  0x300B, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus6',                       0x300C, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus6_REG2',                  0x300D, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus7',                       0x300E, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus7_REG2',                  0x300F, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus8',                       0x3010, 0xFF, 'ro', 'r'], 
    ['StatusAlarmMinus8_REG2',                  0x3011, 0xFF, 'ro', 'r'], 
    ['StatusActualWarning',                     0x3012, 0xFF, 'ro', 'r'], 
    ['StatusActualWarning_REG2',                0x3013, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus1',                     0x3014, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus1_REG2',                0x3015, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus2',                     0x3016, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus2_REG2',                0x3017, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus3',                     0x3018, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus3_REG2',                0x3019, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus4',                     0x301A, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus4_REG2',                0x301B, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus5',                     0x301C, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus5_REG2',                0x301D, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus6',                     0x301E, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus6_REG2',                0x301F, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus7',                     0x3020, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus7_REG2',                0x3021, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus8',                     0x3022, 0xFF, 'ro', 'r'], 
    ['StatusWarningMinus8_REG2',                0x3023, 0xFF, 'ro', 'r'], 
    ['LogActualInputVoltage',                   0x3024, 0xFF, 'ro', 'r'], 
    ['LogActualInputCurrent_REG2',              0x3025, 0xFF, 'ro', 'r'], 
    ['LogActualOutputVoltage1',                 0x3026, 0xFF, 'ro', 'r'], 
    ['LogActualOutputCurrent',                  0x3027, 0xFF, 'ro', 'r'], 
    ['LogActualBatteryVoltage',                 0x302A, 0xFF, 'ro', 'r'], 
    ['LogActualBatteryChargeCurrent',           0x302B, 0xFF, 'ro', 'r'], 
    ['LogActualBatteryTemperature',             0x302D, 0xFF, 'ro', 'r'], 
    ['LogStatusSoc',                            0x302E, 0xFF, 'ro', 'r'], 
    ['LogStatusSoh',                            0x302F, 0xFF, 'ro', 'r'], 
    ['LogCountOperationTime',                   0x3030, 0xFF, 'ro', 'r'], 
    ['LogCountOperationTime_REG2',              0x3031, 0xFF, 'ro', 'r'], 

    ['Battery1FwVersion',                       0x4202, 0xFF, 'ro', 'r'], 
    ['Battery1SerialnumberLSB',                 0x4203, 0xFF, 'ro', 'r'], 
    ['Battery1Serialnumber1',                   0x4204, 0xFF, 'ro', 'r'], 
    ['Battery1Serialnumber2',                   0x4205, 0xFF, 'ro', 'r'], 
    ['Battery1SerialnumberMSB',                 0x4206, 0xFF, 'ro', 'r'], 
    ['Battery1BatteryType',                     0x4207, 0xFF, 'ro', 'r'], 
    ['Battery1InstalledCapacity',               0x4211, 0xFF, 'ro', 'r'], 
    ['Battery1BatteryResistorNominal',          0x4212, 0xFF, 'ro', 'r'], 
    ['Battery1TemperatureAlarmMax',             0x4215, 0xFF, 'ro', 'r'], 
    ['Battery1TemperatureAlarmMin',             0x4216, 0xFF, 'ro', 'r'], 
    ['Battery1ChargeCharacteristicType',        0x4217, 0xFF, 'ro', 'r'], 
    ['Battery1LifetimeNominal',                 0x4218, 0xFF, 'ro', 'r'], 
    ['Battery1ChargeCurrentMax',                0x4219, 0xFF, 'ro', 'r'], 
    ['Battery1ChargeAbsorbtionVoltage',         0x421A, 0xFF, 'ro', 'r'], 
    ['Battery1ChargeEndvoltage',                0x421B, 0xFF, 'ro', 'r'], 
    ['Battery1ChargeTemperatureCoefficient',    0x421C, 0xFF, 'ro', 'r'], 
    ['Battery1DischargeEndvoltage',             0x421D, 0xFF, 'ro', 'r'], 
    ['Battery1DischargeCurrentMax',             0x421E, 0xFF, 'ro', 'r'], 
    ['Battery1TemperatureWarningMax',           0x4280, 0xFF, 'ro', 'r'], 
    ['Battery1TemperatureWarningMin',           0x4281, 0xFF, 'ro', 'r'], 
    ['Battery1DischargeEndvoltageLowCurrent',   0x4285, 0xFF, 'ro', 'r'], 
    ['Battery1StatusSOC',                       0x4287, 0xFF, 'ro', 'r'], 
    ['Battery1StatusSOH',                       0x4288, 0xFF, 'ro', 'r'], 
    ['Battery1StatusActualTemperature',         0x42A3, 0xFF, 'ro', 'r'], 
    ['Battery1StatusFuse',                      0x42A5, 0xFF, 'ro', 'r'], 
    ['Battery1StatusActualInternalVoltage',     0x42A7, 0xFF, 'ro', 'r'], 
    ['Battery1StatusActualBlockVoltage',        0x42A8, 0xFF, 'ro', 'r'], 
    
    ['Battery2FwVersion',                       0x4302, 0xFF, 'ro', 'r'], 
    ['Battery2SerialnumberLSB',                 0x4303, 0xFF, 'ro', 'r'], 
    ['Battery2Serialnumber1',                   0x4304, 0xFF, 'ro', 'r'], 
    ['Battery2Serialnumber2',                   0x4305, 0xFF, 'ro', 'r'], 
    ['Battery2SerialnumberMSB',                 0x4306, 0xFF, 'ro', 'r'], 
    ['Battery2BatteryType',                     0x4307, 0xFF, 'ro', 'r'], 
    ['Battery2InstalledCapacity',               0x4311, 0xFF, 'ro', 'r'], 
    ['Battery2BatteryResistorNominal',          0x4312, 0xFF, 'ro', 'r'], 
    ['Battery2TemperatureAlarmMax',             0x4315, 0xFF, 'ro', 'r'], 
    ['Battery2TemperatureAlarmMin',             0x4316, 0xFF, 'ro', 'r'], 
    ['Battery2ChargeCharacteristicType',        0x4317, 0xFF, 'ro', 'r'], 
    ['Battery2LifetimeNominal',                 0x4318, 0xFF, 'ro', 'r'], 
    ['Battery2ChargeCurrentMax',                0x4319, 0xFF, 'ro', 'r'], 
    ['Battery2ChargeAbsorbtionVoltage',         0x431A, 0xFF, 'ro', 'r'], 
    ['Battery2ChargeEndvoltage',                0x431B, 0xFF, 'ro', 'r'], 
    ['Battery2ChargeTemperatureCoefficient',    0x431C, 0xFF, 'ro', 'r'], 
    ['Battery2DischargeEndvoltage',             0x431D, 0xFF, 'ro', 'r'], 
    ['Battery2DischargeCurrentMax',             0x431E, 0xFF, 'ro', 'r'], 
    ['Battery2TemperatureWarningMax',           0x4380, 0xFF, 'ro', 'r'], 
    ['Battery2TemperatureWarningMin',           0x4381, 0xFF, 'ro', 'r'], 
    ['Battery2DischargeEndvoltageLowCurrent',   0x4385, 0xFF, 'ro', 'r'], 
    ['Battery2StatusSOC',                       0x4387, 0xFF, 'ro', 'r'], 
    ['Battery2StatusSOH',                       0x4388, 0xFF, 'ro', 'r'], 
    ['Battery2StatusActualTemperature',         0x43A3, 0xFF, 'ro', 'r'], 
    ['Battery2StatusFuse',                      0x43A5, 0xFF, 'ro', 'r'], 
    ['Battery2StatusActualInternalVoltage',     0x43A7, 0xFF, 'ro', 'r'], 
    ['Battery2StatusActualBlockVoltage',        0x43A8, 0xFF, 'ro', 'r'], 
    
    ['Battery3FwVersion',                       0x4402, 0xFF, 'ro', 'r'], 
    ['Battery3SerialnumberLSB',                 0x4403, 0xFF, 'ro', 'r'], 
    ['Battery3Serialnumber1',                   0x4404, 0xFF, 'ro', 'r'], 
    ['Battery3Serialnumber2',                   0x4405, 0xFF, 'ro', 'r'], 
    ['Battery3SerialnumberMSB',                 0x4406, 0xFF, 'ro', 'r'], 
    ['Battery3BatteryType',                     0x4407, 0xFF, 'ro', 'r'], 
    ['Battery3InstalledCapacity',               0x4411, 0xFF, 'ro', 'r'], 
    ['Battery3BatteryResistorNominal',          0x4412, 0xFF, 'ro', 'r'], 
    ['Battery3TemperatureAlarmMax',             0x4415, 0xFF, 'ro', 'r'], 
    ['Battery3TemperatureAlarmMin',             0x4416, 0xFF, 'ro', 'r'], 
    ['Battery3ChargeCharacteristicType',        0x4417, 0xFF, 'ro', 'r'], 
    ['Battery3LifetimeNominal',                 0x4418, 0xFF, 'ro', 'r'], 
    ['Battery3ChargeCurrentMax',                0x4419, 0xFF, 'ro', 'r'], 
    ['Battery3ChargeAbsorbtionVoltage',         0x441A, 0xFF, 'ro', 'r'], 
    ['Battery3ChargeEndvoltage',                0x441B, 0xFF, 'ro', 'r'], 
    ['Battery3ChargeTemperatureCoefficient',    0x441C, 0xFF, 'ro', 'r'], 
    ['Battery3DischargeEndvoltage',             0x441D, 0xFF, 'ro', 'r'], 
    ['Battery3DischargeCurrentMax',             0x441E, 0xFF, 'ro', 'r'], 
    ['Battery3TemperatureWarningMax',           0x4480, 0xFF, 'ro', 'r'], 
    ['Battery3TemperatureWarningMin',           0x4481, 0xFF, 'ro', 'r'], 
    ['Battery3DischargeEndvoltageLowCurrent',   0x4485, 0xFF, 'ro', 'r'], 
    ['Battery3StatusSOC',                       0x4487, 0xFF, 'ro', 'r'], 
    ['Battery3StatusSOH',                       0x4488, 0xFF, 'ro', 'r'], 
    ['Battery3StatusActualTemperature',         0x44A3, 0xFF, 'ro', 'r'], 
    ['Battery3StatusFuse',                      0x44A5, 0xFF, 'ro', 'r'], 
    ['Battery3StatusActualInternalVoltage',     0x44A7, 0xFF, 'ro', 'r'], 
    ['Battery3StatusActualBlockVoltage',        0x44A8, 0xFF, 'ro', 'r'], 
    
    ['Battery4FwVersion',                       0x4502, 0xFF, 'ro', 'r'], 
    ['Battery4SerialnumberLSB',                 0x4503, 0xFF, 'ro', 'r'], 
    ['Battery4Serialnumber1',                   0x4504, 0xFF, 'ro', 'r'], 
    ['Battery4Serialnumber2',                   0x4505, 0xFF, 'ro', 'r'], 
    ['Battery4SerialnumberMSB',                 0x4506, 0xFF, 'ro', 'r'], 
    ['Battery4BatteryType',                     0x4507, 0xFF, 'ro', 'r'], 
    ['Battery4InstalledCapacity',               0x4511, 0xFF, 'ro', 'r'], 
    ['Battery4BatteryResistorNominal',          0x4512, 0xFF, 'ro', 'r'], 
    ['Battery4TemperatureAlarmMax',             0x4515, 0xFF, 'ro', 'r'], 
    ['Battery4TemperatureAlarmMin',             0x4516, 0xFF, 'ro', 'r'], 
    ['Battery4ChargeCharacteristicType',        0x4517, 0xFF, 'ro', 'r'], 
    ['Battery4LifetimeNominal',                 0x4518, 0xFF, 'ro', 'r'], 
    ['Battery4ChargeCurrentMax',                0x4519, 0xFF, 'ro', 'r'], 
    ['Battery4ChargeAbsorbtionVoltage',         0x451A, 0xFF, 'ro', 'r'], 
    ['Battery4ChargeEndvoltage',                0x451B, 0xFF, 'ro', 'r'], 
    ['Battery4ChargeTemperatureCoefficient',    0x451C, 0xFF, 'ro', 'r'], 
    ['Battery4DischargeEndvoltage',             0x451D, 0xFF, 'ro', 'r'], 
    ['Battery4DischargeCurrentMax',             0x451E, 0xFF, 'ro', 'r'], 
    ['Battery4TemperatureWarningMax',           0x4580, 0xFF, 'ro', 'r'], 
    ['Battery4TemperatureWarningMin',           0x4581, 0xFF, 'ro', 'r'], 
    ['Battery4DischargeEndvoltageLowCurrent',   0x4585, 0xFF, 'ro', 'r'], 
    ['Battery4StatusSOC',                       0x4587, 0xFF, 'ro', 'r'], 
    ['Battery4StatusSOH',                       0x4588, 0xFF, 'ro', 'r'], 
    ['Battery4StatusActualTemperature',         0x45A3, 0xFF, 'ro', 'r'], 
    ['Battery4StatusFuse',                      0x45A5, 0xFF, 'ro', 'r'], 
    ['Battery4StatusActualInternalVoltage',     0x45A7, 0xFF, 'ro', 'r'], 
    ['Battery4StatusActualBlockVoltage',        0x45A8, 0xFF, 'ro', 'r'], 
    
    ['Battery5FwVersion',                       0x4602, 0xFF, 'ro', 'r'], 
    ['Battery5SerialnumberLSB',                 0x4603, 0xFF, 'ro', 'r'], 
    ['Battery5Serialnumber1',                   0x4604, 0xFF, 'ro', 'r'], 
    ['Battery5Serialnumber2',                   0x4605, 0xFF, 'ro', 'r'], 
    ['Battery5SerialnumberMSB',                 0x4606, 0xFF, 'ro', 'r'], 
    ['Battery5BatteryType',                     0x4607, 0xFF, 'ro', 'r'], 
    ['Battery5InstalledCapacity',               0x4611, 0xFF, 'ro', 'r'], 
    ['Battery5BatteryResistorNominal',          0x4612, 0xFF, 'ro', 'r'], 
    ['Battery5TemperatureAlarmMax',             0x4615, 0xFF, 'ro', 'r'], 
    ['Battery5TemperatureAlarmMin',             0x4616, 0xFF, 'ro', 'r'], 
    ['Battery5ChargeCharacteristicType',        0x4617, 0xFF, 'ro', 'r'], 
    ['Battery5LifetimeNominal',                 0x4618, 0xFF, 'ro', 'r'], 
    ['Battery5ChargeCurrentMax',                0x4619, 0xFF, 'ro', 'r'], 
    ['Battery5ChargeAbsorbtionVoltage',         0x461A, 0xFF, 'ro', 'r'], 
    ['Battery5ChargeEndvoltage',                0x461B, 0xFF, 'ro', 'r'], 
    ['Battery5ChargeTemperatureCoefficient',    0x461C, 0xFF, 'ro', 'r'], 
    ['Battery5DischargeEndvoltage',             0x461D, 0xFF, 'ro', 'r'], 
    ['Battery5DischargeCurrentMax',             0x461E, 0xFF, 'ro', 'r'], 
    ['Battery5TemperatureWarningMax',           0x4680, 0xFF, 'ro', 'r'], 
    ['Battery5TemperatureWarningMin',           0x4681, 0xFF, 'ro', 'r'], 
    ['Battery5DischargeEndvoltageLowCurrent',   0x4685, 0xFF, 'ro', 'r'], 
    ['Battery5StatusSOC',                       0x4687, 0xFF, 'ro', 'r'], 
    ['Battery5StatusSOH',                       0x4688, 0xFF, 'ro', 'r'], 
    ['Battery5StatusActualTemperature',         0x46A3, 0xFF, 'ro', 'r'], 
    ['Battery5StatusFuse',                      0x46A5, 0xFF, 'ro', 'r'], 
    ['Battery5StatusActualInternalVoltage',     0x46A7, 0xFF, 'ro', 'r'], 
    ['Battery5StatusActualBlockVoltage',        0x46A8, 0xFF, 'ro', 'r'], 
    
    ['readAlarm',                   0x4700, 0xFF, 'ro', 'v'], # 
    ['readTimestamp0',              0x4701, 0xFF, 'ro', 'v'], # 
    ['readTimestamp1',              0x4702, 0xFF, 'ro', 'v'], # 
    ['readTimestamp2',              0x4703, 0xFF, 'ro', 'v'], # 
    ['readTimestamp3',              0x4704, 0xFF, 'ro', 'v'], # 
    ['heartbeat',                   0x4705, 0,    'ro', 'v'] # 
                                                                        ]


class UpsTelcoDevice(ModbusDevice):
    def __init__(self, deviceSettings):
        super(UpsTelcoDevice, self).__init__(deviceSettings, 
            coRegisterList = None, 
            diRegisterList = None, 
            hrRegisterList = upsTelcoHrRegisterList, 
            irRegisterList = None)





















