# -*- coding: utf-8 -*-
"""
Created on Tue Feb  2 17:04:12 2021

@author: Georg.Kordowich
"""

import numpy as np
import time
import subprocess
import pymodbus.exceptions as pme
import builtins
import sys, os
import socket

class WriteAccessError(Exception):
    def __init__(self, *args):
        if args:
            self.message = args[0]
        else:
            self.message = None

    def __str__(self):
        if self.message:
            return 'WriteAccessError, {0} '.format(self.message)
        else:
            return 'WriteAccessError has been raised, try adding \'force\' as an argument to overwrite writing permission.'



# Modbus function Codes for writing coils and holding registers
functionCodeDict_write = {
    'co': 15, #use multiple write
    'di': 0, 
    'hr': 16, #use multiple write
    'ir': 0, 
    5:15,
    6:16
    }

# Modbus function Codes for writing coils and holding registers
functionCodeDict_read = {
    'co': 1, 
    'di': 2, 
    'hr': 3, 
    'ir': 4, 
    1:1,
    2:2,
    3:3,
    4:4
    }


'''
The following function will pring out a helptext for first-time users.
'''
def showHelptext(MODE):
    print('#============== Welcome ==============#')
    print('This helptext is shown by default and can be disabled by setting SHOWHELPTEXT = False')
    print()
    print('The script has multiple options.')
    print('1. The MODE of the script can be set to Testing, Manual or Production. Production will read actual values from the Modbus clients but not print anything or ask for user input. Manual does the same but does print the read values, while the Testing mode will simulate reading the devices by using random values.')
    print('2. The debugging level of the script can be set to DEBUG or CRITICAL. CRITICAL will only show errors, while DEBUG will show all sent and received Data.')
    print('3. You can activate and deactivate the server and the client functions.')
    print('4. The script can run cyclic or one single time.')
    print('5. You can also select and unselect the modbus devices you want to read.')
    print('6. You can select the IP address and Port of the Modbus Client.')
    print()
    print('Selecting the options can be done in three ways: ')
    print('Option 1: Adapt the default values directly in the script and call the script without any arguments.')
    print('Option 2: Adapt the values in the config file. It is described further down.')
    print('Option 3: Start the Script from the Command Line with arguments, which are listed below:')
    print('Note that the existing values in the config file will overwrite the default values.')
    print('If any Parameters are given from the command line the config file will be ignored and the values given in the command line will overwrite the default values.')
    print()
    print('#============== Arguments ==============#')
    print('The first argument is should be the client IP-address. The second argumente should be the client port.')
    print('-f:              Can be used to specify file name or location of the config file. Either a complete path can be given or just the filename. If just the filename is given, the script will look for the file in the current working directory.')
    print('all or -a:       Reads all devices and activates server and clients. Can be combined with notcontrollino')
    print('clients:         Generally activates the readout of clients. Which clients should be read exactly must be set separately for example by using \'-a\' or \'controllino\' etc. If it is set to false NO CLIENTS WILL BE READ.')
    print('server:          Activates server')
    print('cyclic or -c:    Reads cylic. Wait time of \'sleepTime\' seconds in between')
    print('controllino:     Reads only controllino')
    print('chiller1:        Reads only chiller1')
    print('chiller2:        Reads only chiller2')
    print('hvac1:           Reads only hvac1')
    print('hvac2:           Reads only hvac2')
    print('osensa:          Reads only osensa')
    print('dl10:            Reads only dl10')
    print('flowmeter1:       Reads only flow meter 1')
    print('ups:             Reads only ups')
    print('flowmeter2:       Reads only flow meter 2')
    print('virtualslave:    Reads only the virtual slave')
    print('notserver:       Deactivates server')
    print('notcontrollino:  Does not read the controllino. Useful in combination with -a.')
    print('notchiller1:     Does not read the chiller1. Useful in combination with -a')
    print('notchiller2:     Does not read the chiller2. Useful in combination with -a')
    print('nothvac1:        Does not read the hvac1. Useful in combination with -a')
    print('nothvac2:        Does not read the hvac2. Useful in combination with -a')
    print('notosensa:       Does not read the osensa. Useful in combination with -a')
    print('notdl10:         Does not read the dl10. Useful in combination with -a')
    print('notflowmeter1:   Does not read the flow meter 1. Useful in combination with -a')
    print('notups:          Does not read the ups. Useful in combination with -a')
    print('notflowmeter2:   Does not read the flow meter 2. Useful in combination with -a')
    print('notvirtualslave: Does not read the virtual slave. Useful in combination with -a')
    print()
    print('#============== Using the Config File ==============#')
    print('The script will always look for a config file in the current working directory. In the Config File, the values can be separated from the name of the value either by "=" or by ":".')
    print('Note that the existing values in the config file will overwrite the default values. Parameters given from the command line will overwrite values given in the config file.')
    print('The following parameters can be used in the script (given with examples):')
    print('clientIpAddress: 192.168.2.3')
    print('clientPort: 502')
    print('serverIpAddress: 192.168.0.66')
    print('serverPort: 1502')
    print('ServerActive: True')
    print('ClientsActive: True')
    print('cyclicRead: True')
    print('controllinoRead: True')
    print('chiller1Read: True')
    print('chiller2Read: True')
    print('hvac1Read: True')
    print('hvac2Read: True')
    print('osensaRead: True')
    print('dl10Read: True')
    print('flowmeter1Read: True')
    print('upsRead: True')
    print('flowmeter2Read: True')
    print('virtualSlaveRead: True')
    print('sleepTime: 5')
    print('mode: Production')
    print()
    print('controllinoUnitId = 42')
    print('chiller1UnitId = 1')
    print('chiller2UnitId = 2')
    print('hvac1UnitId = 3')
    print('hvac2UnitId = 4')
    print('osensaUnitId = 10')
    print('dl10UnitId = 5')
    print('flowmeter1UnitId = 20')
    print('upsUnitID = 21')
    print('flowmeter2UnitId = 22')
    print('virtualSlaveUnitId = 100')
    print()
    print('#============== Writing Registers Manually ==============#')
    print('The Script can be used to write registers manually from the command line. Writing \'force\' as argument in front of \'write\' will ignore missing permission to write.')
    print('There are two ways of writing (The script will automatically detect which one you use and also if you use hex or dec numbers):')
    print('Version 1 (Usage of device name, register type and address):')
    print('controllino_modbus.py write chiller1 hr 1024 8')
    print('controllino_modbus.py write 7 hr 1024 8')
    print('The Command consists of five parts: ')
    print('0. force (optional)')
    print('1. The \'write\' keyword')
    print('2. The device: controllino, chiller1, chiller2, hvac1, hvac2, osensa, dl10, flowmeter1, ups, flowmeter2, virtualslave. Alternatively, the device ID can be given as a Number.')
    print('3. The Register Type: hr = holding register or co = coil')
    print('4. The Address of the register')
    print('5. The value that shall be written')
    print()
    print('Version 2 (Usage of device name and register name):')
    print('controllino_modbus.py write chiller1 systemOnOff 8')
    print('The Command consists of four parts: ')
    print('0. force (optional)')
    print('1. The \'write\' keyword')
    print('2. The device: controllino, chiller1, chiller2, hvac1, hvac2, osensa, dl10, flowmeter1, ups, flowmeter2, virtualslave. Alternatively, the device ID can be given as a Number.')
    print('3. The Register Name as given in the dataframe of the device.')
    print('4. The value that shall be written')
    print()
    print('#============== Reading Registers Manually ==============#')
    print('Reading Registers manually works similar to writing. The main difference is the lack of the last argument, as no value will be written.')
    print()
    print('There are two options: readsingle or readmultiple')
    print('#=== readsingle ===#')
    print('Version 1')
    print('1. The \'readsingle\' keyword')
    print('2. The device: controllino, chiller1, chiller2, hvac1, hvac2, osensa, dl10, flowmeter1, ups, flowmeter2, virtualslave. Alternatively, the device ID can be given as a Number.')
    print('3. The Register Name as given in the dataframe of the device.')
    print('Example: python controllino_modbus readsingle chiller1 systemOnOff')
    print()
    print('Version 2')
    print('1. The \'readsingle\' keyword')
    print('2. The device: controllino, chiller1, chiller2, hvac1, hvac2, osensa, dl10, flowmeter1, ups, flowmeter2, virtualslave. Alternatively, the device ID can be given as a Number.')
    print('3. The Register Type: hr, co, ir, di')
    print('4. The Address of the register')
    print('Example: python controllino_modbus_v22 readsingle 1 ir 1024')
    print()
    print('#=== readmultiple ===#')
    print('Version 1 will read the registers that are defined in the dataframe from the start register until the end register.')
    print('1. The \'readsingle\' keyword')
    print('2. The device: controllino, chiller1, chiller2, hvac1, hvac2, osensa, dl10, flowmeter1, ups, flowmeter2, virtualslave. Alternatively, the device ID can be given as a Number.')
    print('3. The Start Register Name as given in the dataframe of the device.')
    print('4. The End Register Name as given in the dataframe of the device.')
    print('Example: python controllino_modbus readmultiple chiller1 systemOnOff waterTempSet')
    print()
    print('Version 2 will read consecutive addresses.')
    print('1. The \'readsingle\' keyword')
    print('2. The device: controllino, chiller1, chiller2, hvac1, hvac2, osensa, dl10, flowmeter1, ups, flowmeter2, virtualslave. Alternatively, the device ID can be given as a Number.')
    print('3. The Register Type: hr, co, ir, di')
    print('4. The Address of the start register')
    print('5. The Address of the end register')
    print('Example: python controllino_modbus readmultiple 42 ir 3 5')
    print()
    
    
    print()
    print('\n\n\n!!!=========!!!IMPORTANT!!!=========!!!')
    print('When using the script for the first time, please read through the helptext above and disable it afterwards.\n\n')
    if not MODE == 'Production':
        input('Press Enter to continue.')



'''
Function will return the current unix time as a list of 16 bit unsigned integers. This means the value can be used in modbus registers.
'''
def getTimeStampRegs():
    unixTime = int(time.time())
    a = np.uint16(unixTime>>48)
    b = np.uint16(unixTime>>32)
    c = np.uint16(unixTime>>16)
    d = np.uint16(unixTime)
    
    return [a, b, c, d]


'''
The function will execute a bash command and return the output and the error.
'''
def executeBash(bashCommand):
    try:
        process = subprocess.Popen(bashCommand.split(), stdout = subprocess.PIPE);
        op, error = process.communicate();
    except builtins.FileNotFoundError:
        return None, None
        
    return op, error



'''
The function will read one register or coil and return the value. If an error is thrown, None is returned.
'''
def genericRead(client, deviceIdRequest, registerTypeRequest, addressRequest):
    print('Starting Manual Read')
    
    try:
        if registerTypeRequest == 1 or registerTypeRequest == 'co':
            # Coil
            result = client.read_coils(addressRequest, 1, unit = deviceIdRequest)
            # Check the function code to detect errors.
            if (result.function_code < 0x80):
                retVal = result.bits[0]
            else:
                print('\n # Modbus Problem, manual read was unsuccessful. #')
        if registerTypeRequest == 2 or registerTypeRequest == 'di':
            # Discrete Input
            result = client.read_discrete_inputs(addressRequest, 1, unit = deviceIdRequest)
            if (result.function_code < 0x80):
                retVal = result.bits[0]
            else:
                print('\n # Modbus Problem, manual read was unsuccessful. #')
        if registerTypeRequest == 3 or registerTypeRequest == 'hr':
            # Holding Register
            result = client.read_holding_registers(addressRequest, 1, unit = deviceIdRequest)
            if (result.function_code < 0x80):
                retVal = result.registers[0]
            else:
                print('\n # Modbus Problem, manual read was unsuccessful. #')
        if registerTypeRequest == 4 or registerTypeRequest == 'ir':
            # Input Register
            result = client.read_input_registers(addressRequest, 1, unit = deviceIdRequest)
            if (result.function_code < 0x80):
                retVal = result.registers[0]
            else:
                print('\n # Modbus Problem, manual read was unsuccessful. #')
        return retVal
    except pme.ConnectionException as e:
        print(e)
        print()
        print('#===========!!!IMPORTANT!!!===========#')
        print('Connection to Client ' + str(deviceIdRequest) + ' impossible, manual read unsuccessful')
    except AttributeError as e:
        print(e)
        if'ModbusIOException' in str(e):
            print('#===========!!!IMPORTANT!!!===========#')
            print('ModbusIOexption for Client: ' + str(deviceIdRequest))
            print('This can happen when trying to connect to a device ID that does not exist.')
        else:
            raise AttributeError(e)


class ModbusDeviceSettingsDefault:
    
    def __init__(self):
        self.clientIpAddress = None
        self.clientPort = None
        self.serverIpAddress = None
        self.serverPort = None
        
        self.cyclicRead = None
        self.serverUse = None
        self.clientsUse = None
        self.controllinoRead = None
        self.chiller1Use = None
        self.chiller2Use = None
        self.hvac1Use = None
        self.hvac2Use = None
        self.osensaUse = None
        self.dl10Use = None
        self.flowmeter1Use = None
        self.upsUse = None
        self.flowmeter2Use = None
        self.virtualSlaveUse = None
        self.sleepTime = None
        self.hvacTelcoUse = None
        self.upsTelcoUse = None


# Modbus function Codes for writing coils and holding registers
modbusDeviceTypeDict = {
    'virtualslave' :  1, 
     'server'      :  1, 
     'clients'     :  2,         
    'controllino'  :  3, 
    'chiller'      :  4, 
    'hvac'         :  5, 
    'osensa'       :  6, 
    'dl10'         :  7,
    'ups'          :  8,
    'flowmeter'    :  9,
    'ups'          : 10,
    'hvactelco'    : 11, 
    'upstelco'     : 12,     
    1 :  1,      
    2 :  2,
    3 :  3,
    4 :  4,
    5 :  5,
    6 :  6,
    7 :  7,
    8 :  8,
    9 :  9,
   10 : 10,    
   11 : 11,       
   12 : 12
   }


class ModbusDeviceSettings:
 
    def __init__(self, name, typeId = None, unitId = None, ipAddress = None, port = None, use = None):
        self.name   = name  # Device ID, Chiller, Controllino, etc.
        self.typeId = typeId  # Device ID, Chiller, Controllino, etc.
        self.unitId = unitId  # Modbus Slave ID
        self.ipAddress = ipAddress
        self.port = port
        self.use = use # Exist and use device
        #self.Read = Read # cyclic or one tyme read
        self.client = None; # ModbusTcpClient
        
class ModbusScriptSettings:
    
    def __init__(self):
        # ================ Default Values ================ #
        self.clients = ModbusDeviceSettings('clients', modbusDeviceTypeDict['clients'], 0, '192.168.2.3', 502)
        self.server = ModbusDeviceSettings('server', modbusDeviceTypeDict['server'], 100)
        #self.controllino = modbusDeviceSettings(modbusDeviceTypeDict['controllino'], 42, '192.168.2.3', 502)
        self.controllino = ModbusDeviceSettings('controllino', modbusDeviceTypeDict['controllino'], 42)         
         
        self.chiller1 = ModbusDeviceSettings('chiller1', modbusDeviceTypeDict['chiller'], 1) 
        self.chiller2 = ModbusDeviceSettings('chiller2', modbusDeviceTypeDict['chiller'], 2) 
        self.hvac1 = ModbusDeviceSettings('hvac1', modbusDeviceTypeDict['hvac'], 3) 
        self.hvac2 = ModbusDeviceSettings('hvac2', modbusDeviceTypeDict['hvac'], 4) 
        self.osensa = ModbusDeviceSettings('osensa', modbusDeviceTypeDict['osensa'], 10) 
        self.dl10 = ModbusDeviceSettings('dl10', modbusDeviceTypeDict['dl10'], 5) 
        self.flowmeter1 = ModbusDeviceSettings('flowmeter1', modbusDeviceTypeDict['flowmeter'], 20) 
        self.ups = ModbusDeviceSettings('ups', modbusDeviceTypeDict['ups'], 21) 
        self.flowmeter2 = ModbusDeviceSettings('flowmeter2', modbusDeviceTypeDict['flowmeter'], 22) 
        self.virtualSlave = ModbusDeviceSettings('virtualslave', modbusDeviceTypeDict['virtualslave'], 100)          
 
        self.hvacTelco = ModbusDeviceSettings('hvactelco', modbusDeviceTypeDict['hvactelco'], 1) # it cannot be changed
        self.upsTelco = ModbusDeviceSettings('upstelco', modbusDeviceTypeDict['upstelco'], 192) # any Modbus ID is allowed

        # OCTE fixed values by Alfred
        # In case there are no values defined in the Config-File, these values will be used (10 bigger in C):
        self.octeRefrigStopPoint = 222
        self.octeHeatingStopPoint = 155
         
        # ================ Real Values ================ #
        # General settings for the script
        # Set Mode (can be overwritten from commandline or config file):
        # 'Testing' will simulate a modbus connection by using random values, if connection is impossible
        # 'Manual' will use input values from the user.
        # 'Production' won't request any input from the user and will not print out any values, only errors
        self.MODE = None
        
        # Set showhelptext: True will show the Helptext when starting the script, False will not show it.
        self.SHOWHELPTEXT = None
        
        # Here the Software versions for which the script was written can be set.
        self.MAX_SOFTWARE_VERSION_CHILLER = None
        self.MAX_SOFTWARE_VERSION_HVAC = None
        self.OSENSA_ALARM_RESET_TEMP = None
        self.MODBUS_TIMEOUT = None
        
        self.clientOnCube = False
        self.clientOnTelco = False
        
        self.cyclicRead = None
        self.server.use = None
        self.clients.use = None

        self.sleepTime = None
     
        
        
    def setDefaultSettings(self, settingsDefault):
        self.clients.ipAddress   = settingsDefault.clientIpAddress
        self.clients.port        = settingsDefault.clientPort
        self.server.ipAddress    = settingsDefault.serverIpAddress
        self.server.port         = settingsDefault.serverPort
        
        self.cyclicRead          = settingsDefault.cyclicRead
        self.server.use          = settingsDefault.serverUse
        self.clients.use         = settingsDefault.clientsUse
        self.controllino.use     = settingsDefault.controllinoUse
        self.chiller1.use        = settingsDefault.chiller1Use
        self.chiller2.use        = settingsDefault.chiller2Use
        self.hvac1.use           = settingsDefault.hvac1Use
        self.hvac2.use           = settingsDefault.hvac2Use
        self.osensa.use          = settingsDefault.osensaUse
        self.dl10.use            = settingsDefault.dl10Use
        self.flowmeter1.use      = settingsDefault.flowmeter1Use
        self.ups.use             = settingsDefault.upsUse
        self.flowmeter2.use      = settingsDefault.flowmeter2Use
        self.virtualSlave.use    = settingsDefault.virtualSlaveUse
        self.sleepTime           = settingsDefault.sleepTime
        
        self.hvacTelco.use       = settingsDefault.hvacTelcoUse
        self.upsTelco.use        = settingsDefault.upsTelcoUse
        
        self.MODE = 'Manual'
    
    def printSettings(self):
        print()
        print('#==============Settings==============#')
        print('The Following Settings have been applied\n')
        print('MODE: ' + str(self.MODE))
        print('clientIpAddress: ' + str(self.clients.ipAddress))
        print('clientPort: ' + str(self.clients.port))
        print('serverIpAddress: ' + str(self.server.ipAddress))
        print('serverPort: ' + str(self.server.port))
        print('Server Active: ' + str(self.server.use))
        print('Clients Active: ' + str(self.clients.use))
        print('cyclicRead: ' + str(self.cyclicRead))
        print('controllinoRead: ' + str(self.controllino.use))
        print('chiller1Read: ' + str(self.chiller1.use))
        print('chiller2Read: ' + str(self.chiller2.use))
        print('hvac1Read: ' + str(self.hvac1.use))
        print('hvac2Read: ' + str(self.hvac2.use))
        print('osensaRead: ' + str(self.osensa.use))
        print('dl10Read: ' + str(self.dl10.use))
        print('flowmeter1Read: ' + str(self.flowmeter1.use))
        print('upsRead: ' + str(self.ups.use))
        print('flowmeter2Read: ' + str(self.flowmeter2.use))
        print('virtualSlaveRead: ' + str(self.virtualSlave.use))
        print('sleepTime: ' + str(self.sleepTime))
        
        print('hvacTelcoRead: ' + str(self.hvacTelco.use))
        print('upsTelcoRead: ' + str(self.upsTelco.use))
        
        print('controllinoUnitId: ' + str(self.controllino.unitId))
        print('chiller1UnitId: ' + str(self.chiller1.unitId))
        print('chiller2UnitId: ' + str(self.chiller2.unitId))
        print('hvac1UnitId: ' + str(self.hvac1.unitId))
        print('hvac2UnitId: ' + str(self.hvac2.unitId))
        print('osensaUnitId: ' + str(self.osensa.unitId))
        print('dl10UnitId: ' + str(self.dl10.unitId))
        print('flowmeter1UnitId: ' + str(self.flowmeter1.unitId))
        print('upsUnitId: ' + str(self.ups.unitId))
        print('flowmeter2UnitId: ' + str(self.flowmeter2.unitId))
        print('virtualSlaveUnitId: ' + str(self.virtualSlave.unitId))
        
        print('hvacTelcoUnitId: ' + str(self.hvacTelco.unitId))
        print('upsTelcoUnitId: ' + str(self.upsTelco.unitId))
        
        # IP-Addresses
        print('controllinoIp: '   + str(self.controllino.ipAddress))
        print('chiller1Ip: '      + str(self.chiller1.ipAddress))
        print('chiller2Ip: '      + str(self.chiller2.ipAddress))
        print('hvac1Ip: '         + str(self.hvac1.ipAddress))
        print('hvac2Ip: '         + str(self.hvac2.ipAddress))
        print('osensaIp: '        + str(self.osensa.ipAddress))
        print('dl10Ip: '          + str(self.dl10.ipAddress))
        print('flowmeter1Ip: '    + str(self.flowmeter1.ipAddress))
        print('upsIp: '           + str(self.ups.ipAddress))
        print('flowmeter2Ip: '    + str(self.flowmeter2.ipAddress))


        print('hvacTelcoIp: '     + str(self.hvacTelco.ipAddress))
        print('upsTelcoIp: '      + str(self.upsTelco.ipAddress))

        # Ports
        print('controllinoPort: ' + str(self.controllino.port))
        print('chiller1Port: '    + str(self.chiller1.port))
        print('chiller2Port: '    + str(self.chiller2.port))
        print('hvac1Port: '       + str(self.hvac1.port))
        print('hvac2Port: '       + str(self.hvac2.port))
        print('osensaPort: '      + str(self.osensa.port))
        print('dl10Port: '        + str(self.dl10.port))
        print('flowmeter1Port: '  + str(self.flowmeter1.port))
        print('upsPort: '         + str(self.ups.port))
        print('flowmeter2Port: '  + str(self.flowmeter2.port))

        print('hvacTelcoPort: '   + str(self.hvacTelco.port))
        print('upsTelcoPort: '    + str(self.upsTelco.port))

        
        
        if not self.MODE == 'Production':
            input('Press Enter to continue.')
        





'''
Function is used to read the config file at a given path and to import the parameters given in the config file. If the file does not exist, it will print a warning.
'''
def readConfigFile(path, settings, settingsDefault):

    try:
        conf = open(path, 'r').read()

        # remove comments
        conflist = conf.splitlines()
        for i in conflist:
            if i.strip().startswith('#'):
                # completely remove lines that start with a '#'
                conflist[conflist.index(i)] = ''
            else:
                # Remove the part of lines that is behind a '#'
                conflist[conflist.index(i)] = i.split('#')[0].strip()
        conf = '\n'.join(conflist)
        
        if 'controllerType' in conf:
            # check if it is not empty
            tmp = conf.split('controllerType')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                if str(tmp) == 'Cube':
                    settings.clientOnCube = True
                
                if str(tmp) == 'OCTE':
                    settings.clientOnTelco = True
        
        if 'mode' in conf:
            # check if it is not empty
            tmp = conf.split('mode')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.MODE = str(tmp)
        
        if 'clientIpAddress' in conf:
            # check if it is not empty
            tmp = conf.split('clientIpAddress')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.clients.ipAddress = tmp
                if (settingsDefault.clientIpAddress == settings.clients.ipAddress): # 192.168.2.2 means it is in Cube or OCTE
                    settings.clientOnCube = True

        if 'clientPort' in conf:
            # check if it is not empty
            tmp = conf.split('clientPort')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.clients.port = int(tmp)

        if 'serverIpAddress' in conf:
            # check if it is not empty
            tmp = conf.split('serverIpAddress')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.server.ipAddress = tmp

        if 'serverPort' in conf:
            # check if it is not empty
            tmp = conf.split('serverPort')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.server.port = int(tmp)

        if 'cyclicRead' in conf:
            # check if it is not empty
            tmp = conf.split('cyclicRead')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.cyclicRead = (tmp=='True')

        if 'ServerActive' in conf:
            # check if it is not empty
            tmp = conf.split('ServerActive')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.server.use = (tmp=='True')

        if 'ClientsActive' in conf:
            # check if it is not empty
            tmp = conf.split('ClientsActive')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.clients.use = (tmp=='True')

        if 'controllinoRead' in conf:
            # check if it is not empty
            tmp = conf.split('controllinoRead')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.controllino.use = (tmp=='True')

        if 'chiller1Read' in conf:
            # check if it is not empty
            tmp = conf.split('chiller1Read')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.chiller1.use = (tmp=='True')

        if 'chiller2Read' in conf:
            # check if it is not empty
            tmp = conf.split('chiller2Read')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.chiller2.use = (tmp=='True')

        if 'hvac1Read' in conf:
            # check if it is not empty
            tmp = conf.split('hvac1Read')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.hvac1.use = (tmp=='True')

        if 'hvac2Read' in conf:
            # check if it is not empty
            tmp = conf.split('hvac2Read')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.hvac2.use = (tmp=='True')

        if 'osensaRead' in conf:
            # check if it is not empty
            tmp = conf.split('osensaRead')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.osensa.use = (tmp=='True')

        if 'dl10Read' in conf:
            # check if it is not empty
            tmp = conf.split('dl10Read')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.dl10.use = (tmp=='True')

        if 'flowmeterRead' in conf: # Alias flowmeter1read for backwards compatibility.
            # check if it is not empty
            tmp = conf.split('flowmeterRead')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.flowmeter1.use = (tmp=='True')

        if 'flowmeter1Read' in conf:
            # check if it is not empty
            tmp = conf.split('flowmeter1Read')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.flowmeter1.use = (tmp=='True')

        if 'upsRead' in conf:
            # check if it is not empty
            tmp = conf.split('upsRead')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.ups.use = (tmp=='True')
        
        if 'flowmeter2Read' in conf:
            # check if it is not empty
            tmp = conf.split('flowmeter2Read')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.flowmeter2.use = (tmp=='True')
        
        if 'virtualSlaveRead' in conf:
            # check if it is not empty
            tmp = conf.split('virtualSlaveRead')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.virtualSlave.use = (tmp=='True')

        if 'sleepTime' in conf:
            # check if it is not empty
            tmp = conf.split('sleepTime')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.sleepTime = int(tmp)

        if 'hvacTelcoRead' in conf:
            # check if it is not empty
            tmp = conf.split('hvacTelcoRead')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.hvacTelco.use = (tmp=='True')

        if 'upsTelcoRead' in conf:
            # check if it is not empty
            tmp = conf.split('upsTelcoRead')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.upsTelco.use = (tmp=='True')

        if 'controllinoUnitId' in conf:
            # check if it is not empty
            tmp = conf.split('controllinoUnitId')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.controllino.unitId = int(tmp)

        if 'chiller1UnitId' in conf:
            # check if it is not empty
            tmp = conf.split('chiller1UnitId')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.chiller1.unitId = int(tmp)

        if 'chiller2UnitId' in conf:
            # check if it is not empty
            tmp = conf.split('chiller2UnitId')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.chiller2.unitId = int(tmp)

        if 'hvac1UnitId' in conf:
            # check if it is not empty
            tmp = conf.split('hvac1UnitId')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.hvac1.unitId = int(tmp)

        if 'hvac2UnitId' in conf:
            # check if it is not empty
            tmp = conf.split('hvac2UnitId')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.hvac2.UnitId = int(tmp)

        if 'osensaUnitId' in conf:
            # check if it is not empty
            tmp = conf.split('osensaUnitId')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.osensa.unitId = int(tmp)

        if 'dl10UnitId' in conf:
            # check if it is not empty
            tmp = conf.split('dl10UnitId')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.dl10.unitId = int(tmp)

        if 'flowmeterUnitId' in conf: # Alias for backwards compatibility
            # check if it is not empty
            tmp = conf.split('flowmeterUnitId')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.flowmeter1.unitId = int(tmp)

        if 'flowmeter1UnitId' in conf:
            # check if it is not empty
            tmp = conf.split('flowmeter1UnitId')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.flowmeter1.unitId = int(tmp)
        
        if 'upsUnitId' in conf:
            # check if it is not empty
            tmp = conf.split('upsUnitId')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.ups.unitId = int(tmp)
        
        if 'flowmeter2UnitId' in conf:
            # check if it is not empty
            tmp = conf.split('flowmeter2UnitId')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.flowmeter2.unitId = int(tmp)

        if 'virtualSlaveUnitId' in conf:
            # check if it is not empty
            tmp = conf.split('virtualSlaveUnitId')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.virtualSlave.unitId = int(tmp)
        
        if 'hvacTelcoUnitId' in conf:
            # check if it is not empty
            tmp = conf.split('hvacTelcoUnitId')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.hvacTelco.unitId = int(tmp)
        
        if 'upsTelcoUnitId' in conf:
            # check if it is not empty
            tmp = conf.split('upsTelcoUnitId')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.upsTelco.unitId = int(tmp)
        
        if 'controllinoIp' in conf:
            tmp = conf.split('controllinoIp')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:     
                settings.controllino.ipAddress = str(tmp)
        
        if 'chiller1Ip' in conf:
            tmp = conf.split('chiller1Ip')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.chiller1.ipAddress = str(tmp)
        
        if 'chiller2Ip' in conf:
            tmp = conf.split('chiller2Ip')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.chiller2.ipAddress = str(tmp)
        
        if 'hvac1Ip' in conf:
            tmp = conf.split('hvac1Ip')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.hvac1.ipAddress = str(tmp)
        
        if 'hvac2Ip' in conf:
            tmp = conf.split('hvac2Ip')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.hvac2.ipAddress = str(tmp)
        
        if 'osensaIp' in conf:
            tmp = conf.split('osensaIp')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.osensa.ipAddress = str(tmp)
        
        if 'dl10Ip' in conf:
            tmp = conf.split('dl10Ip')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.dl10.ipAddress = str(tmp)
        
        if 'flowmeter1Ip' in conf:
            tmp = conf.split('flowmeter1Ip')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.flowmeter1.ipAddress = str(tmp)
        
        if 'upsIp' in conf:
            tmp = conf.split('upsIp')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.ups.ipAddress = str(tmp)
        
        if 'flowmeter2Ip' in conf:
            tmp = conf.split('flowmeter2Ip')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.flowmeter2.ipAddress = str(tmp)
        
        if 'hvacTelcoIp' in conf:
            tmp = conf.split('hvacTelcoIp')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.hvacTelco.ipAddress = str(tmp)
        
        if 'upsTelcoIp' in conf:
            tmp = conf.split('upsTelcoIp')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.upsTelco.ipAddress = str(tmp)
        
        if 'controllinoPort' in conf:
            tmp = conf.split('controllinoPort')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.controllino.port = int(tmp)
        
        if 'chiller1Port' in conf:
            tmp = conf.split('chiller1Port')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.chiller1.port = int(tmp)
        
        if 'chiller2Port' in conf:
            tmp = conf.split('chiller2Port')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.chiller2.port = int(tmp)
        
        if 'hvac1Port' in conf:
            tmp = conf.split('hvac1Port')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.hvac1.port = int(tmp)
        
        if 'hvac2Port' in conf:
            tmp = conf.split('hvac2Port')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.hvac2.port = int(tmp)
        
        if 'osensaPort' in conf:
            tmp = conf.split('osensaPort')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.osensa.port = int(tmp)
        
        if 'dl10Port' in conf:
            tmp = conf.split('dl10Port')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.dl10.port = int(tmp)
        
        if 'flowmeter1Port' in conf:
            tmp = conf.split('flowmeter1Port')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.flowmeter1.port = int(tmp)
        
        if 'upsPort' in conf:
            tmp = conf.split('upsPort')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.ups.port = int(tmp)
        
        if 'flowmeter2Port' in conf:
            tmp = conf.split('flowmeter2Port')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.flowmeter2.port = int(tmp)
        
        if 'hvacTelcoPort' in conf:
            tmp = conf.split('hvacTelcoPort')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.hvacTelco.port = int(tmp)
        
        if 'upsTelcoPort' in conf:
            tmp = conf.split('upsTelcoPort')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.upsTelco.port = int(tmp)

        # TODO: KaiW: OCTE fixed values
        if 'fixedRefrigStopPoint' in conf:
            tmp = conf.split('fixedRefrigStopPoint')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.octeRefrigStopPoint = int(float(tmp)*10)

        if 'fixedHeatingStopPoint' in conf:
            tmp = conf.split('fixedHeatingStopPoint')[1].splitlines()[0].split(':', 1)[-1].split('=', 1)[-1].strip()
            if tmp:
                settings.octeHeatingStopPoint = int(float(tmp)*10)

        
        print('Config File Loaded successfully')

    except FileNotFoundError:
        print('\n\n\n!!!=========!!!WARNING!!!=========!!!')
        print('Could not find or decode Config File with path: ' + str(path))
        return None
    
    return settings



# The modbus server is emulated by this script. The modbus server has the same IP address as the device this script is running on.
# Get IP address of the device that the script is running on and set it
def getServerIpAddress():
    if sys.platform.startswith('linux'):
        # Os is linux.
        bashcommand = 'hostname -I'
        op, error = executeBash(bashcommand)
        hostIplist = op.decode('iso-8859-1').split()
    
        for ip in hostIplist:
            ips = ip[0:10]
            #print('\nFound IP:' + ip + ' IPs:' + ips )
            if(ips != '192.168.2.'): # Not local br1 address
                return ip
      
        # for local testing only. If the IP is not assigned to the br0, use 192.168.2.1 or 192.168.2.2 from br1.
        # The internal br1 network must be used for testing, connect the laptop tt br1 network
        for ip in hostIplist:
            ips = ip[0:10]
            if(ips == '192.168.2.'): # local br1 address
                return ip
                    
        return None
        
    elif sys.platform.startswith('win'):
        # OS is windows.
        return socket.gethostbyname(socket.gethostname())
















