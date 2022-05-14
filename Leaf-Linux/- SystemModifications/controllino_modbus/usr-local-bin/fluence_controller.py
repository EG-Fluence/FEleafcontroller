#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Tue Jan 26 13:04:36 2021

@author: Georg.Kordowich
"""


import time
from pymodbus.client.sync import ModbusTcpClient
import pymodbus.exceptions as pme
import datetime
import sys
from controllino_modbus_utils import controllino_modbus_utils as cmu_utils
import socket
import os
import math


# ============== IMPORTANT SETTING ============== #
# All settings are stored in this class. Most important settings can be set here.
scriptSettings = cmu_utils.ModbusScriptSettings()
settingsDefault = cmu_utils.ModbusDeviceSettingsDefault()

# ============================== Definition of default values. ============================== #
# Set default values. Those are used if no arguments or inputs are given.

defaultConfigFilePath = "/usr/local/share/fluence/controllino_modbus.conf"

logging = True

# Get IP address of the device that the script is running on and set it
#if sys.platform.startswith('linux'):
#    bashcommand = 'hostname -I'
#    op, error = cmu_utils.executeBash(bashcommand)
#    hostnamelist = op.decode('iso-8859-1').split()
#    if len(hostnamelist) > 1:
#        serverIpAddressDefault = str(hostnamelist[1])
#    else:
#        serverIpAddressDefault = str(hostnamelist[0])
#
#elif sys.platform.startswith('win'):
#    serverIpAddressDefault = socket.gethostbyname(socket.gethostname())
    
serverIpAddressDefault = cmu_utils.getServerIpAddress()
if(not serverIpAddressDefault):
    sys.exit(os.EX_NOHOST)

serverPortDefault = 1502

hvac1ReadDefault = True
hvac2ReadDefault = True
dl10UnitIdDefault = 5
hvac1UnitIdDefault = 3
hvac2UnitIdDefault = 4




# ============================== Initialization of settings. ============================== #

# Initialize Parameters with default values
scriptSettings.server.ipAddress = serverIpAddressDefault
scriptSettings.server.port = serverPortDefault
scriptSettings.hvac1.use = hvac1ReadDefault
scriptSettings.hvac2.use = hvac2ReadDefault
scriptSettings.dl10.unitId = dl10UnitIdDefault
scriptSettings.hvac1.unitId = hvac1UnitIdDefault
scriptSettings.hvac2.UnitId = hvac2UnitIdDefault

# Overwrite default values with values from config file

path = os.path.join(os.getcwd(), 'controllino_modbus.conf')


ret = cmu_utils.readConfigFile(path, scriptSettings, settingsDefault)
if ret is not None:
    scriptSettings = ret

if os.path.isfile(defaultConfigFilePath):
    ret = cmu_utils.readConfigFile(defaultConfigFilePath, scriptSettings, settingsDefault)
    if ret is not None:
        scriptSettings = ret


setpoint_timestamp = int(time.time())
current_program = 'default'

# Define possible setpoints for the HVAC for better readability.
lower15sp = 15
higher15sp = 15
higher17sp = 17
higher20sp = 20
higher23sp = 23
defaultsp = 25

# In this case, the client is the server of the modbus script.
client = ModbusTcpClient(str(scriptSettings.server.ipAddress), port = str(scriptSettings.server.port), timeout = 5)

def get_setpoint(temp, humid):
    global setpoint_timestamp
    global current_program
    
    if current_program != 'default':
        
        # Check stop conditions for current program
        # If the stop condition for the current programm are met, the default program will be set again.
        if current_program == 'lower15':
            if temp >= 15:
                # This is only here for clarity purposes. The stop heating point will always be set at 15 degC
                current_program = 'default'
                return defaultsp
        
        if current_program == 'higher15':
            current_timestamp = int(time.time())
            # Stop conditions as defined by Alfredo.
            if temp <= 15 or (current_timestamp - setpoint_timestamp) > 3600 or humid <= 75:
                current_program = 'default'
                return defaultsp
            else:
                return higher15sp
        
        if current_program == 'higher17':
            current_timestamp = int(time.time())
            # Stop conditions as defined by Alfredo.
            if temp <= 17 or (current_timestamp - setpoint_timestamp) > 3600 or humid <= 65:
                current_program = 'default'
                return defaultsp
            else:
                return higher17sp
        
        if current_program == 'higher20':
            current_timestamp = int(time.time())
            # Stop conditions as defined by Alfredo.
            if temp <= 20 or (current_timestamp - setpoint_timestamp) > 3600 or humid <= 52:
                current_program = 'default'
                return defaultsp
            else:
                return higher20sp
        
        if current_program == 'higher23':
            current_timestamp = int(time.time())
            # Stop conditions as defined by Alfredo.
            if temp <= 23 or (current_timestamp - setpoint_timestamp) > 3600 or humid <= 43:
                current_program = 'default'
                return defaultsp
            else:
                return higher23sp
    else:
        # No setpoint is set, check conditions as defined by Alfredo.
        if temp <= 15:
            setpoint_timestamp = int(time.time())
            current_program = 'lower15'
            # the setpoint for the cooling stop stays at 25 degC
            return defaultsp
        
        if temp > 15 and temp <= 18:
            if humid > 85:
                setpoint_timestamp = int(time.time())
                current_program = 'higher15'
                return higher15sp
        
        if temp > 18 and temp <= 21:
            if humid > 75:
                setpoint_timestamp = int(time.time())
                current_program = 'higher17'
                return higher17sp
                
        if temp > 21 and temp <= 24:
            if humid > 62:
                setpoint_timestamp = int(time.time())
                current_program = 'higher20'
                return higher20sp
        
        if temp > 24 and temp <= 27:
            if humid > 53:
                setpoint_timestamp = int(time.time())
                current_program = 'higher23'
                return higher23sp
        
        if temp > 27 and temp <= 30:
            if humid > 45:
                setpoint_timestamp = int(time.time())
                current_program = 'default'
                return defaultsp
        
        if temp > 30:
            if humid > 40:
                setpoint_timestamp = int(time.time())
                current_program = 'default'
                return defaultsp
    return defaultsp

def getTempAndHumid(client):
    temp = 2.55
    humid = 2.55
    readAlarmDl10 = 1
    try:
        # Get the current temperature and humidity from the dl10 Sensor
        result = client.read_input_registers(0x0000, 4, unit = scriptSettings.dl10.unitId)
        humid, temp = result.registers[0:2]
        temp = temp/100
        humid = humid/100

        readAlarmDl10 = result.registers[3]
        # print("readAlarmDl10: ", readAlarmDl10)

    except pme.ConnectionException as e:
        # Exceptions will be thrown on Modbus Errors.
        print(e)
        print()
        print('#===========!!!IMPORTANT!!!===========#')
        print('Connection to Client dl10 with unit ID: ' + str(scriptSettings.dl10.unitId) + '  impossible')

    except AttributeError as e:
        # Exceptions will be thrown on Modbus Errors.
        print(e)        
        if'ModbusIOException' in str(e):
            print('#===========!!!IMPORTANT!!!===========#')
            print('ModbusIOexption for Client dl10 with unit ID: ' + str(scriptSettings.dl10.unitId) + ' .')
            print('This can happen when trying to connect to a device ID that does not exist.')
        else:
            raise AttributeError(e)

    return temp, humid, readAlarmDl10



def HVAC_CubeWrite(client, refrigStopPoint, oldRefrigStopPoint, setpoint_counter, hvac, dl10Error):
    if hvac.use == True:
        try:
            if refrigStopPoint != oldRefrigStopPoint:

                # Try to write the new setpoint to the HVAC

                # if hvac.type == 'Envicool':
                #     ret = client.write_registers(0x0700, refrigStopPoint, unit=hvac.unitId)
                #
                # if hvac.type == 'BlackShileds':
                #     ret = client.write_registers(0x0029, refrigStopPoint, unit=hvac.unitId)

                if hvac.type == 'BlackShields':
                    ret = client.write_registers(0x0029, refrigStopPoint, unit=hvac.unitId)
                else:
                    ret = client.write_registers(0x0700, refrigStopPoint, unit=hvac.unitId)

                # Check if the write was successful
                if (ret.function_code < 0x80):

                    # Only safe the new setpoint if the write was successful.
                    oldRefrigStopPoint = refrigStopPoint

                    print('Write was successful')

                    if dl10Error == 1:
                        print('RefrigStopPoint was set to: 20°C ,because of Dl10 failure.')

                    # For each setpoint change raise the counter value by one
                    if setpoint_counter < 65000:
                        setpoint_counter += 1
                        client.write_registers(0x0908, setpoint_counter, unit=hvac.unitId)
                    else:
                        setpoint_counter = 0
                        client.write_registers(0x0908, setpoint_counter, unit=hvac.unitId)

                    # Return setpoint_counter since it is a virual entry
                    result = client.read_holding_registers(0x0908, 1, unit=hvac.unitId)
                    if (result.function_code < 0x80):
                        retVal = result.registers[0]
                        print('Setpoint-Counter = ', retVal)
                    else:
                        print('\n # Modbus Problem, read was unsuccessful. #')

                else:
                    print('\n # Write was unsuccessful, check Client with unit ID: ' + str(hvac.unitId) + ' #')
                    print(ret)

        except pme.ConnectionException as e:
            # Exceptions will be thrown on Modbus Errors.
            print(e)
            print()
            print('#===========!!!IMPORTANT!!!===========#')
            print('Connection to Client hvac with unit ID: ' + str(hvac.unitId) + '  impossible')

        except AttributeError as e:
            # Exceptions will be thrown on Modbus Errors.
            print(e)
            if 'ModbusIOException' in str(e):
                print('#===========!!!IMPORTANT!!!===========#')
                print('ModbusIOexption for Client hvac with unit ID: ' + str(hvac.unitId) + ' .')
                print('This can happen when trying to connect to a device ID that does not exist.')
            else:
                raise AttributeError(e)

    return oldRefrigStopPoint, setpoint_counter


def HVAC_OcteWrite(client, fixedRefrigStopPoint, fixedHeatingStopPoint, hvac):
    if hvac.use == True:
        while True:
            try:
                # Read from first coil if hvac expects values in °C or °F
                ret3 = client.read_coils(0, 1, unit=hvac.unitId)
                retValue = ret3.bits[0]

                # If coil is True, covert values from °C into °F
                if retValue == True:
                    fixedRefrigStopPoint_  = math.ceil(((fixedRefrigStopPoint/10)*1.8 + 32)*10)
                    fixedHeatingStopPoint_ = math.ceil(((fixedHeatingStopPoint/10)*1.8 + 32)*10)
                else:
                    fixedRefrigStopPoint_  = fixedRefrigStopPoint
                    fixedHeatingStopPoint_ = fixedHeatingStopPoint

                ret1 = client.write_registers(0x0000, fixedRefrigStopPoint_, unit=hvac.unitId)
                ret2 = client.write_registers(0x0002, fixedHeatingStopPoint_, unit=hvac.unitId)

                # Check if the write was successful and break while loop if successful
                if (ret1.function_code < 0x80) and (ret2.function_code < 0x80):
                    print('Fixed RefrigStopPoint was set to = ' + str(fixedRefrigStopPoint) + ' C')
                    print('Fixed HeatingStopPoint was set to = ' + str(fixedHeatingStopPoint) + ' C')
                    break
                else:
                    print('\n # Write was unsuccessful, check Client with unit ID: ' + str(hvac.unitId) + ' #')
                    print(ret1)
                    print('\n # Write was unsuccessful, check Client with unit ID: ' + str(hvac.unitId) + ' #')
                    print(ret2)

            except pme.ConnectionException as e:
                # Exceptions will be thrown on Modbus Errors.
                print(e)
                print()
                print('#===========!!!IMPORTANT!!!===========#')
                print('Connection to Client hvac with unit ID: ' + str() + '  impossible')

            except AttributeError as e:
                # Exceptions will be thrown on Modbus Errors.
                print(e)
                if 'ModbusIOException' in str(e):
                    print('#===========!!!IMPORTANT!!!===========#')
                    print('ModbusIOexption for Client hvac with unit ID: ' + str(hvac.unitId) + ' .')
                    print('This can happen when trying to connect to a device ID that does not exist.')
                else:
                    raise AttributeError(e)
        time.sleep(10)

def HVAC_AC_CubeWrite(client,  refrigSetPoint, hvac):
    ###client, hvac2_cct, oldRefrigStopPoint1, scriptSettings.hvac1)
    if hvac.use == True:
        try:
            if hvac.type == 'BlackShields':
                ret = client.write_registers(0x0029, refrigSetPoint, unit=hvac.unitId)
            else:
                ret = client.write_registers(0x0700, refrigSetPoint, unit=hvac.unitId)

            # Check if the write was successful
            if (ret.function_code < 0x80):
                print('Write to Modbus server was successful')
                print('Modbus register of unit ID: ' + str(hvac.unitId) + ' was set to: ' + str(refrigSetPoint))
            else:
                print('\n # Write was unsuccessful, check Client with unit ID: ' + str(hvac.unitId) + ' #')
                print(ret)

        except pme.ConnectionException as e:
            # Exceptions will be thrown on Modbus Errors.
            print(e)
            print()
            print('#===========!!!IMPORTANT!!!===========#')
            print('Connection to Client hvac with unit ID: ' + str(hvac.unitId) + '  impossible')

        except AttributeError as e:
            # Exceptions will be thrown on Modbus Errors.
            print(e)
            if 'ModbusIOException' in str(e):
                print('#===========!!!IMPORTANT!!!===========#')
                print('ModbusIOexption for Client hvac with unit ID: ' + str(hvac.unitId) + ' .')
                print('This can happen when trying to connect to a device ID that does not exist.')
            else:
                raise AttributeError(e)



def Cube_Controller():

    # Inital Values for oldRefrigStopPoint and setpoint_counter for hvac1 and hvac2
    oldRefrigStopPoint1 = 0
    oldRefrigStopPoint2 = 0
    setpoint_counter1 = 0
    setpoint_counter2 = 0

    while True:

        # First, connect the client
        if not client.is_socket_open():
            client.connect()

        # Get current temperature and humidity from the dl10 Sensor
        temp, humid, readAlarmDl10 = getTempAndHumid(client)

        print(temp, humid)
        #print(readAlarmDl10)
        # 2.55 is the default value divided by 100, which means the values are invalid

        # #TODO: Energy saving by turning of FAN Rack in Air cooled Cube
        #
        # if scriptSettings.clientOnAirCooledCube == True:
        #
        #     ret1 = client.write_registers(0x0001, 1, unit=scriptSettings.controllino.unitId)
        #     # ret2 = client.write_registers(0x0002, 1, unit=scriptSettings.controllino.unitId)
        #
        #     if (ret1.function_code < 0x80):
        #         print('write on FAN Rack successful')
        #     else:
        #         print('something went wrong with modbus')

            # if (ret2.function_code < 0x80):
            #     print('write on FAN Rack successful')
            # else:
            #     print('something went wrong with modbus')



            # if energysave_constraint =/</> something(Temp): # shutting down FAN Rack 1&2 to save energy
            #     ret = client.write_registers(0x0001, 1, unit=scriptSettings.controllino.unitId) # digital outputs of controllino are 1 (24V) then FAN Rack is not running
            #     ret = client.write_registers(0x0001, 1, unit=scriptSettings.controllino.unitId)
            # else:
            #     ret = client.write_registers(0x0001, 0, unit=scriptSettings.controllino.unitId) # digital outputs of controllino are 0 (0V) then FAN Rack is running
            #     ret = client.write_registers(0x0001, 0, unit=scriptSettings.controllino.unitId)


        if (readAlarmDl10 == 0) and temp != 2.55 and humid != 2.55:
            # the refrigStopPoint is the point where the chiller stops
            refrigStopPoint = get_setpoint(temp, humid)

            print(refrigStopPoint)
            # Write Settings to HVAC 1 if it is available.
            oldRefrigStopPoint1, setpoint_counter1 = HVAC_CubeWrite(client, refrigStopPoint, oldRefrigStopPoint1, setpoint_counter1, scriptSettings.hvac1, 0)
            # Write Settings to HVAC 2 if it is available.
            oldRefrigStopPoint2, setpoint_counter2 = HVAC_CubeWrite(client, refrigStopPoint, oldRefrigStopPoint2, setpoint_counter2, scriptSettings.hvac2, 0)

        else:
            # leave everything as it is, because the values are invalid
            if readAlarmDl10 == 1:
                print("ReadAlarm for DL10 is True")
            if temp == 2.55 or humid == 2.55:
                print("Values of DL10 are invalid")

            # predetermined 'emergency value' (refrigStopPointDl10Failure for HVAC) by Alfredo is 20°C in case of DL10 failure
            oldRefrigStopPoint1, setpoint_counter1 = HVAC_CubeWrite(client, 20, oldRefrigStopPoint1, setpoint_counter1, scriptSettings.hvac1, 1)
            oldRefrigStopPoint2, setpoint_counter2 = HVAC_CubeWrite(client, 20, oldRefrigStopPoint2, setpoint_counter2, scriptSettings.hvac2, 1)

        time.sleep(10)


def OCTE_Controller():

    # Take values that are applied from the config file
    fixedRefrigStopPoint = scriptSettings.octeRefrigStopPoint
    fixedHeatingStopPoint = scriptSettings.octeHeatingStopPoint

    # Overwrite values from command line for testing purpose

    if len(sys.argv) > 1:
        a = float(sys.argv[1].replace(",", "."))
        fixedRefrigStopPoint = int(a*10)
        # print(fixedHeatingStopPoint)
    if len(sys.argv) > 2:
        b = float(sys.argv[2].replace(",", "."))
        fixedHeatingStopPoint = int(b*10)
        # print(fixedHeatingStopPoint)

    HVAC_OcteWrite(client, fixedRefrigStopPoint, fixedHeatingStopPoint, scriptSettings.hvacTelco)


hvac2_cct_target_max = 260
hvac2_cct_max        = 230
hvac2_cct_min        = 190
hvac2_cct_target_min = 160

def AC_Cube_Controller():
    while True:
        try:
            # See: HVAC Sync Problem
            ret_hvac2_cct = client.read_holding_registers(0x0066, 1, unit=scriptSettings.hvac2.UnitId)
            hvac2_cct = ret_hvac2_cct.registers[0]

            # Check temperature limits
            if hvac2_cct > hvac2_cct_max:
                print("Current cabinet temperature of Hvac2: " + str(hvac2_cct) + " > " + str(hvac2_cct_max) + "  Target is " + str(hvac2_cct_target_min))
                HVAC_AC_CubeWrite(client, hvac2_cct_target_min, scriptSettings.hvac1)
                HVAC_AC_CubeWrite(client, hvac2_cct_target_min, scriptSettings.hvac2)
            if hvac2_cct < hvac2_cct_min:
                print("Current cabinet temperature of Hvac2: " + str(hvac2_cct) + " < " + str(hvac2_cct_min) + "  Target is " + str(hvac2_cct_target_max))
                HVAC_AC_CubeWrite(client, hvac2_cct_target_max, scriptSettings.hvac1)
                HVAC_AC_CubeWrite(client, hvac2_cct_target_max, scriptSettings.hvac2)
            if (hvac2_cct <= hvac2_cct_max) and (hvac2_cct >= hvac2_cct_min):
                print('Current cabinet temperature of Hvac2: ' + str(hvac2_cct))

        except pme.ConnectionException as e:
            # Exceptions will be thrown on Modbus Errors.
            print(e)
            print()
            print('#===========!!!IMPORTANT!!!===========#')
            print('Connection to Client hvac with unit ID: ' + str(scriptSettings.controllino.unitId) + '  impossible')

        except AttributeError as e:
            # Exceptions will be thrown on Modbus Errors.
            print(e)
            if 'ModbusIOException' in str(e):
                print('#===========!!!IMPORTANT!!!===========#')
                print('ModbusIOexption for Client hvac with unit ID: ' + str(scriptSettings.controllino.unitId) + ' .')
                print('This can happen when trying to connect to a device ID that does not exist.')
            else:
                raise AttributeError(e)

        time.sleep(5)


if scriptSettings.clientOnCube == True:
    print("")
    print("Running on Cube-LiquidCooled")
    print("")
    Cube_Controller()

if scriptSettings.clientOnAirCooledCube == True:
    print("")
    print("Running on Cube-AirCooled")
    print("")
    AC_Cube_Controller()

if scriptSettings.clientOnTelco == True:
    print("")
    print("Running on OCTE")
    print("")
    OCTE_Controller()
