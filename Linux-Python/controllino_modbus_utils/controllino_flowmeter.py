# -*- coding: utf-8 -*-
"""
Created on Tue Feb  2 14:09:32 2021

@author: Georg.Kordowich
"""

# -*- coding: utf-8 -*-
"""
Created on Tue Feb  2 14:05:11 2021

@author: Georg.Kordowich
"""

from controllino_modbus_utils.controllino_generic_modbus_device import ModbusDevice
import struct

'''
The following List contains all signal names, addresses and also contains dummy values for the flow meter client Holding Registers.
It is used to initialize the datastore for the values which are read via modbus.

Description of the columns
1 - Name
2 - Address
3 - not updated value
4 - ro or rw flag
5 - 'r' real device register, read from device  
    'v' virtual device register, will be used only inside Modbus server
'''

flowmeterHrRegisterList = [
    ['FlowMeterDeviceID',                       0x0000, 0xFF, 'ro', 'r'], # 0xAC = U1000MkII-FM/HM
    ['FlowMeterStatus',                         0x0001, 0xFF, 'ro', 'r'], # 0x0000 OK, not (0x0000) Fault
    ['FlowMeterSystemType',                     0x0002, 0xFF, 'ro', 'r'], # 0x04 Heating system, 0x0C Chiller system
    ['FlowMeterSerialIdentifier',               0x0003, 0xFF, 'ro', 'r'], # Serial Identifier ( 3 x int)
    ['FlowMeterSerialIdentifier2',              0x0004, 0xFF, 'ro', 'r'], # 
    ['FlowMeterSerialIdentifier3',              0x0005, 0xFF, 'ro', 'r'], # 
    ['FlowMeterMeasuredVelocity',               0x0006, 0xFF, 'ro', 'r'], # Units in m/s (IEEE754 float)
    ['FlowMeterMeasuredVelocity2',              0x0007, 0xFF, 'ro', 'r'], # 
    ['FlowMeterMeasuredFlow',                   0x0008, 0xFF, 'ro', 'r'], # Units in m3/hr for Metric Units, in US Gal/m for Imperial (IEEE754 float)
    ['FlowMeterMeasuredFlow2',                  0x0009, 0xFF, 'ro', 'r'], # 
    ['FlowMeterCalculatedPower',                0x000A, 0xFF, 'ro', 'r'], # Units in kW for Metric Units, in BTU/s for Imperial (IEEE754 float)
    ['FlowMeterCalculatedPower2',               0x000B, 0xFF, 'ro', 'r'], # 
    ['FlowMeterCalculatedEnergy',               0x000C, 0xFF, 'ro', 'r'], # Units in kWh for Metric Units, in BTU for Imperial (IEEE754 float)
    ['FlowMeterCalculatedEnergy2',              0x000D, 0xFF, 'ro', 'r'], # 
    ['FlowMeterMeasuredTemperatureHot',         0x000E, 0xFF, 'ro', 'r'], # Units in Degrees Celsius for Metric Units, in Degrees Fahrenheit for Imperial (IEEE754 float)
    ['FlowMeterMeasuredTemperatureHot2',        0x000F, 0xFF, 'ro', 'r'], # 
    ['FlowMeterMeasuredTemperatureCold',        0x0010, 0xFF, 'ro', 'r'], # Units in Degrees Celsius for Metric Units, in Degrees Fahrenheit for Imperial (IEEE754 float)
    ['FlowMeterMeasuredTemperatureCold2',       0x0011, 0xFF, 'ro', 'r'], # 
    ['FlowMeterMeasuredTemperatureDifference',  0x0012, 0xFF, 'ro', 'r'], # Units in Degrees Celsius for Metric Units, in Degrees Fahrenheit for Imperial (IEEE754 float)
    ['FlowMeterMeasuredTemperatureDifference2', 0x0013, 0xFF, 'ro', 'r'], #
    ['FlowMeterMeasuredVolumeTotal',            0x0014, 0xFF, 'ro', 'r'], # Units in m3 for Metric Units in US Gal for Imperial (IEEE754 float)
    ['FlowMeterMeasuredVolumeTotal2',           0x0015, 0xFF, 'ro', 'r'], #
    ['FlowMeterInstrument Units',               0x0016, 0xFF, 'ro', 'r'], # 0x00 Metric, 0x01 Imperial
    ['FlowMeterInstrumentGain',                 0x0017, 0xFF, 'ro', 'r'], # Gain in dB
    ['FlowMeterInstrumentSNR',                  0x0018, 0xFF, 'ro', 'r'], # Gain in dB
    ['FlowMeterInstrumentSignal',               0x0019, 0xFF, 'ro', 'r'], # Gain in dB    
    ['FlowMeterMeasuredDeltaTimeDifference',    0x001A, 0xFF, 'ro', 'r'], # Diagnostic Data Units in nanoseconds (IEEE754 float)
    ['FlowMeterMeasuredDeltaTimeDifference2',   0x001B, 0xFF, 'ro', 'r'], #
    ['FlowMeterInstrumentETA',                  0x001C, 0xFF, 'ro', 'r'], # Diagnostic Data Units in nanoseconds (IEEE754 float)
    ['FlowMeterInstrumentETA2',                 0x001D, 0xFF, 'ro', 'r'], #
    ['FlowMeterInstrumentATA',                  0x001E, 0xFF, 'ro', 'r'], # Diagnostic Data Units in nanoseconds (IEEE754 float)
    ['FlowMeterInstrumentATA2',                 0x001F, 0xFF, 'ro', 'r'], #
    
    ['readAlarm',                               0x0020, 0xFF, 'ro', 'v'], # 
    ['readTimestamp0',                          0x0021, 0xFF, 'ro', 'v'], # 
    ['readTimestamp1',                          0x0022, 0xFF, 'ro', 'v'], # 
    ['readTimestamp2',                          0x0023, 0xFF, 'ro', 'v'], # 
    ['readTimestamp3',                          0x0024, 0xFF, 'ro', 'v'], #  
    ['heartbeat',                               0x0025, 0,    'ro', 'v'] # 
                                                                ]

'''
Function is used to Decode the Modbus-Values from the flowmeter.
The original values shall be stored in the dataframe and servercontext.
This function should only be used when printing out values.
'''
def flowmeterDecode(toBeConvertedDF):
    # Decoding: The Pymodbus library already decodes the two bytes in a register.
    # Therefor, at first those bytes have to be encoded, then added, then the wholy four bytes have to be decoded to float.

    convertedDF = toBeConvertedDF.copy(deep = True)  # Changes to the copy shall not be reflected in the original

    req = toBeConvertedDF.loc['FlowMeterMeasuredVelocity':'FlowMeterMeasuredVelocity2', 'value'].values
    if req[0] != 255 and req[1] != 255:
        convertedDF.at['FlowMeterMeasuredVelocity', 'value'] = float(struct.unpack('>f', struct.pack('>H', req[0]) + struct.pack('>H', req[1]))[0])  # big endian
        convertedDF.at['FlowMeterMeasuredVelocity2', 'value'] = 0

    req = toBeConvertedDF.loc['FlowMeterMeasuredFlow':'FlowMeterMeasuredFlow2', 'value'].values
    if req[0] != 255 and req[1] != 255:
        convertedDF.at['FlowMeterMeasuredFlow', 'value'] = struct.unpack('>f', struct.pack('>H', req[0]) + struct.pack('>H', req[1]))[0]  # big endian
        convertedDF.at['FlowMeterMeasuredFlow2', 'value'] = 0

    req = toBeConvertedDF.loc['FlowMeterCalculatedPower':'FlowMeterCalculatedPower2', 'value'].values
    if req[0] != 255 and req[1] != 255:
        convertedDF.at['FlowMeterCalculatedPower', 'value'] = struct.unpack('>f', struct.pack('>H', req[0]) + struct.pack('>H', req[1]))[0]  # big endian
        convertedDF.at['FlowMeterCalculatedPower2', 'value'] = 0

    req = toBeConvertedDF.loc['FlowMeterCalculatedEnergy':'FlowMeterCalculatedEnergy2', 'value'].values
    if req[0] != 255 and req[1] != 255:
        convertedDF.at['FlowMeterCalculatedEnergy', 'value'] = struct.unpack('>f', struct.pack('>H', req[0]) + struct.pack('>H', req[1]))[0]  # big endian
        convertedDF.at['FlowMeterCalculatedEnergy2', 'value'] = 0

    req = toBeConvertedDF.loc['FlowMeterMeasuredTemperatureHot':'FlowMeterMeasuredTemperatureHot2', 'value'].values
    if req[0] != 255 and req[1] != 255:
        convertedDF.at['FlowMeterMeasuredTemperatureHot', 'value'] = struct.unpack('>f', struct.pack('>H', req[0]) + struct.pack('>H', req[1]))[0]  # big endian
        convertedDF.at['FlowMeterMeasuredTemperatureHot2', 'value'] = 0

    req = toBeConvertedDF.loc['FlowMeterMeasuredTemperatureCold':'FlowMeterMeasuredTemperatureCold2', 'value'].values
    if req[0] != 255 and req[1] != 255:
        convertedDF.at['FlowMeterMeasuredTemperatureCold', 'value'] = struct.unpack('>f', struct.pack('>H', req[0]) + struct.pack('>H', req[1]))[0]  # big endian
        convertedDF.at['FlowMeterMeasuredTemperatureCold2', 'value'] = 0

    req = toBeConvertedDF.loc['FlowMeterMeasuredTemperatureDifference':'FlowMeterMeasuredTemperatureDifference2', 'value'].values
    if req[0] != 255 and req[1] != 255:
        convertedDF.at['FlowMeterMeasuredTemperatureDifference', 'value'] = struct.unpack('>f', struct.pack('>H', req[0]) + struct.pack('>H', req[1]))[0]  # big endian
        convertedDF.at['FlowMeterMeasuredTemperatureDifference2', 'value'] = 0

    req = toBeConvertedDF.loc['FlowMeterMeasuredVolumeTotal':'FlowMeterMeasuredVolumeTotal2', 'value'].values
    if req[0] != 255 and req[1] != 255:
        convertedDF.at['FlowMeterMeasuredVolumeTotal', 'value'] = struct.unpack('>f', struct.pack('>H', req[0]) + struct.pack('>H', req[1]))[0]  # big endian
        convertedDF.at['FlowMeterMeasuredVolumeTotal2', 'value'] = 0

    req = toBeConvertedDF.loc['FlowMeterMeasuredDeltaTimeDifference':'FlowMeterMeasuredDeltaTimeDifference2', 'value'].values
    if req[0] != 255 and req[1] != 255:
        convertedDF.at['FlowMeterMeasuredDeltaTimeDifference', 'value'] = struct.unpack('>f', struct.pack('>H', req[0]) + struct.pack('>H', req[1]))[0]  # big endian
        convertedDF.at['FlowMeterMeasuredDeltaTimeDifference2', 'value'] = 0

    req = toBeConvertedDF.loc['FlowMeterInstrumentETA':'FlowMeterInstrumentETA2', 'value'].values
    if req[0] != 255 and req[1] != 255:
        convertedDF.at['FlowMeterInstrumentETA', 'value'] = struct.unpack('>f', struct.pack('>H', req[0]) + struct.pack('>H', req[1]))[0]  # big endian
        convertedDF.at['FlowMeterInstrumentETA2', 'value'] = 0

    req = toBeConvertedDF.loc['FlowMeterInstrumentATA':'FlowMeterInstrumentATA2', 'value'].values
    if req[0] != 255 and req[1] != 255:
        convertedDF.at['FlowMeterInstrumentATA', 'value'] = struct.unpack('>f', struct.pack('>H', req[0]) + struct.pack('>H', req[1]))[0]  # big endian
        convertedDF.at['FlowMeterInstrumentATA2', 'value'] = 0

    return convertedDF





class FlowmeterDevice(ModbusDevice):
    def __init__(self, deviceSettings):
        super(FlowmeterDevice, self).__init__(deviceSettings, 
               coRegisterList = None, 
               diRegisterList = None, 
               hrRegisterList = flowmeterHrRegisterList, 
               irRegisterList = None)
    
    def printDataFrames(self):
        print(flowmeterDecode(self.hrDataFrame))























