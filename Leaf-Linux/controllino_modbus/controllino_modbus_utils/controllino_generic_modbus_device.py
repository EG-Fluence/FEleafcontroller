# -*- coding: utf-8 -*-
"""
Created on Tue Feb  2 13:45:53 2021

@author: Georg.Kordowich
"""

import pandas as pd
import pymodbus.exceptions as pme
import controllino_modbus_utils.controllino_modbus_utils as cmu_utils
import builtins
import time

import variables

maxNumberOfProcessableRegisters = 30

def singleRead(client, regType, unitId, startAddress, numberOfRegs, dataFrame):
    if regType == 'co':
        result = client.read_coils(startAddress, numberOfRegs, unit = unitId)
    if regType == 'di':
        result = client.read_discrete_inputs(startAddress, numberOfRegs, unit = unitId)
    if regType == 'hr':
        result = client.read_holding_registers(startAddress, numberOfRegs, unit = unitId)
    if regType == 'ir':
        result = client.read_input_registers(startAddress, numberOfRegs, unit = unitId)
    
    return result


def readFullRegister(client, regType, unitId, dataFrame):

    print("===================================== readFullRegister BEGIN")

    # for every read-try implement heartbeat
    try:
        dataFrame.loc['heartbeat', 'value'] = (dataFrame.loc['heartbeat', 'value'] + 1)%65536
    except KeyError:
        # Some devices have no heartbeat.
        pass
    
    size = len(dataFrame.index)
    i = 0

    while i < size:
        startIndex = dataFrame.index.values[i]
        if(dataFrame.loc[startIndex, 'regtype'] == 'v'):
            i += 1
            continue # while (i < size):

        numberOfRegs = 1        

        j = i + 1
        while j < size:
            currentIndex = dataFrame.index.values[j]
            # not virtual register
            if((dataFrame.loc[currentIndex, 'regtype'] != 'v') and
                # id previous address from the table incremented by 1 == current address eg. delta is 1, continues addresses
                (dataFrame.iloc[j - 1].loc['address'] + 1) == (dataFrame.iloc[j].loc['address']) and
                (numberOfRegs < maxNumberOfProcessableRegisters)):
                    numberOfRegs += 1  
            else:
                break  # while (j < size):
            j += 1;        
        
        startAddress = dataFrame.loc[startIndex, 'address']
        lastIndex = dataFrame.index.values[i + numberOfRegs - 1]

        ReadAttempts = 0
        while True:

            try:
                variables.ReadMessages_counter += 1
                result = singleRead(client, regType, unitId, startAddress, numberOfRegs, dataFrame)
                ReadAttempts += 1

                if result.function_code < 0x80:
                    if regType == 'co':
                        dataFrame.loc[startIndex:lastIndex, 'value'] = result.bits[0:numberOfRegs]
                        if variables.DEBUG:
                            print("    UnitID'co':" + str(result.unit_id) + "  " + str(result.bits))
                    if regType == 'di':
                        dataFrame.loc[startIndex:lastIndex, 'value'] = result.bits[0:numberOfRegs]
                        if variables.DEBUG:
                            print("    UnitID'di':" + str(result.unit_id) + "  " + str(result.bits))
                    if regType == 'hr':
                        dataFrame.loc[startIndex:lastIndex, 'value'] = result.registers[0:numberOfRegs]
                        if variables.DEBUG:
                            print("    UnitID'hr':" + str(result.unit_id) + "  " + str(result.registers))
                    if regType == 'ir':
                        dataFrame.loc[startIndex:lastIndex, 'value'] = result.registers[0:numberOfRegs]
                        if variables.DEBUG:
                            print("    UnitID'ir':" + str(result.unit_id) + "  " + str(result.registers))

                    break  # success, so no more retries

                else:
                    variables.ReadMessages_error_counter += 1
                    # print('\n # Check Device with unit ID: ' + str(unitId) + ' error code: ' + str(result.function_code))
                    # print('\n regType: ' + regType + ' startAddress: ' + str(startAddress) + ' numberOfRegs: ' + str(numberOfRegs))
                    dataFrame.loc['readAlarm', 'value'] = 1
                    if ReadAttempts == variables.MaximumReadAttempts:
                        print("Giving up from trying to send a Read message to unitID: " + str(unitId))
                        return dataFrame

            except pme.ConnectionException as e:
                variables.ReadMessages_error_counter += 1
                print(e)
                print()
                print('#===========!!!IMPORTANT!!!===========#')
                print('Connection impossible to Client with unit ID: ' + str(unitId) + ', StartAddress: ' + str(startAddress) + ', Number of Registers: ' + str(numberOfRegs))
                dataFrame.loc['readAlarm', 'value'] = 1
                if ReadAttempts == variables.MaximumReadAttempts:
                    return dataFrame

            except AttributeError as e:
                variables.ReadMessages_error_counter += 1
                print(e)
                if'ModbusIOException' in str(e):
                    print('#===========!!!IMPORTANT!!!===========#')
                    print('ModbusIOException for Client with unit ID: ' + str(unitId) + ', StartAddress: ' + str(startAddress) + ', Number of Registers: ' + str(numberOfRegs))
                    print('This can happen when trying to connect to a device ID that does not exist.')
                    dataFrame.loc['readAlarm', 'value'] = 1
                    if ReadAttempts == variables.MaximumReadAttempts:
                        return dataFrame
                else:
                    if ReadAttempts == variables.MaximumReadAttempts:
                        raise AttributeError(e)

            if variables.DEBUG:
                print("Retrying Read message to unitId: " + str(unitId))

            # the message got an error, so let's try again...

        # if we come out of the loop by retrying, then maybe we will do better next time...

        i += numberOfRegs
    
    try:
        # Try setting the timestamp. Some registers don´t have a timestamp, those can be skipped.
        dataFrame.loc['readTimestamp0':'readTimestamp3', 'value'] = cmu_utils.getTimeStampRegs()
        dataFrame.loc['readAlarm', 'value'] = 0
    except builtins.KeyError:
        pass

    print("===================================== readFullRegister END\r\n\r")




class ModbusDevice():
    def __init__(self, deviceSettings, coRegisterList = None, diRegisterList = None, hrRegisterList = None, irRegisterList = None):
        self.deviceSettings = deviceSettings
        
        if coRegisterList is not None:
            coDataFrame = pd.DataFrame(data = coRegisterList, columns = ['signal_name', 'address', 'value', 'writeaccess', 'regtype'])
            coDataFrame = coDataFrame.set_index('signal_name')
            self.coDataFrame = coDataFrame
        else:
            self.coDataFrame = None
        
        if diRegisterList is not None:
            diDataFrame = pd.DataFrame(data = diRegisterList, columns = ['signal_name', 'address', 'value', 'writeaccess', 'regtype'])
            diDataFrame = diDataFrame.set_index('signal_name')
            self.diDataFrame = diDataFrame
        else:
            self.diDataFrame = None
        
        if hrRegisterList is not None:
            hrDataFrame = pd.DataFrame(data = hrRegisterList, columns = ['signal_name', 'address', 'value', 'writeaccess', 'regtype'])
            hrDataFrame = hrDataFrame.set_index('signal_name')
            self.hrDataFrame = hrDataFrame
        else:
            self.hrDataFrame = None
        
        if irRegisterList is not None:
            irDataFrame = pd.DataFrame(data = irRegisterList, columns = ['signal_name', 'address', 'value', 'writeaccess', 'regtype'])
            irDataFrame = irDataFrame.set_index('signal_name')
            self.irDataFrame = irDataFrame
        else:
            self.irDataFrame = None
    
    
    '''
    Function will read all registers that are defined in the register list.
    '''
    def readAll(self):
        if not self.deviceSettings.client.is_socket_open():
            self.deviceSettings.client.connect()
        
        if self.coDataFrame is not None:
            readFullRegister(self.deviceSettings.client, 'co', self.deviceSettings.unitId, self.coDataFrame)
        if self.diDataFrame is not None:
            readFullRegister(self.deviceSettings.client, 'di', self.deviceSettings.unitId, self.diDataFrame)
        if self.hrDataFrame is not None:
            readFullRegister(self.deviceSettings.client, 'hr', self.deviceSettings.unitId, self.hrDataFrame)
        if self.irDataFrame is not None:
            readFullRegister(self.deviceSettings.client, 'ir', self.deviceSettings.unitId, self.irDataFrame)
        
        #TODO: Why is this as return arguments?
        return self.coDataFrame, self.diDataFrame, self.hrDataFrame, self.irDataFrame
    

    '''
    Function print all data that is defined in the register list.
    '''
    def printDataFrames(self):
        if self.coDataFrame is not None:
            print('# ------------ Coils ------------ #')
            print(self.coDataFrame)
        if self.diDataFrame is not None:
            print('# ------------ Discrete Inputs ------------ #')
            print(self.diDataFrame)
        if self.hrDataFrame is not None:
            print('# ------------ Holding Registers ------------ #')
            print(self.hrDataFrame)
        if self.irDataFrame is not None:
            print('# ------------ Input Registers ------------ #')
            print(self.irDataFrame)


























