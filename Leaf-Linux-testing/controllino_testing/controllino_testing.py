#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import os
import sys
import time
import csv
import signal

from pymodbus.client.sync import ModbusTcpClient

def Usage():
    print('Usage: <ServerIPaddress> <ServerPort> <PoolingInterval[milliseconds]>')
    print('       ex: 192.168.2.10  1502  100')
    quit(-1)

def signal_handler(sig, frame):
    print('\n\nExiting...')
    quit(0)


signal.signal(signal.SIGINT, signal_handler)

print()
print('Controllino_testing...\n')

n = len(sys.argv)
if n != 4:
    print("Wrong number of arguments")
    Usage()

try:
    ServerIPaddress = sys.argv[1]  # '192.168.2.10'
    ServerIPport = int(sys.argv[2])  # 1502
    PoolingInterval = int(sys.argv[3])
except:
    print("Error parsing arguments")
    Usage()

print('Modbus variables from configuration file:')
ModbusVariables = list()
reader = csv.reader(open('controllino_testing.conf', 'r'))
for row in reader:
    row = [x.strip(' ') for x in row]
    if len(row) == 4:
        ModbusVariables.append(row)
        print('  '+str(row))
    else:
        if len(row) == 0:
            print('  Configuration line error: empty line')
        else:
            print('  Configuration line error: too many elements ' + str(row))
        quit(-1)
print()

client = ModbusTcpClient(str(ServerIPaddress), port=str(ServerIPport), timeout=5)

# Create Logs directory
LogsDirectoryPath = os.path.join(os.getcwd(), "Logs")
if not os.path.exists(LogsDirectoryPath):
    os.mkdir(LogsDirectoryPath)

# Create Log file
TimeNow = time.time()
TimeNowMilliseconds = str((int(TimeNow*1000)) % 1000).rjust(3, '0')
TimeNowStr = time.strftime('%Y_%m_%d_%H_%M_%S', time.localtime(TimeNow)) + '_' + TimeNowMilliseconds
LogFileName = LogsDirectoryPath + '/' + 'Log_' + TimeNowStr + '.csv'
LogFile = open(LogFileName, "w")

#print header
Line = 'time'
for ModbusVariable in ModbusVariables:
    Line += ','
    firstloop = False
    Line += str(ModbusVariable[0])
print(Line)
LogFile.write(Line + '\n')
LogFile.flush()

# loop reading the Modbus variables and creating the log file
while True:

    LineResults = list()
    TimeNow = time.time()
    TimeNowMilliseconds = str((int(TimeNow * 1000)) % 1000).rjust(3, '0')
    TimeNowStr = time.strftime('%Y/%m/%d %H:%M:%S', time.localtime(time.time())) + '.' + TimeNowMilliseconds

    for ModbusVariable in ModbusVariables:

        ModbusVariableName = ModbusVariable[0]
        ModbusSlaveId = int(ModbusVariable[1])
        ModbusVariableAddress = int(ModbusVariable[2])
        regType = ModbusVariable[3]

        result = ""
        try:
            if regType == 'co':
                result = client.read_coils(ModbusVariableAddress, 1, unit=ModbusSlaveId)
            elif regType == 'di':
                result = client.read_discrete_inputs(ModbusVariableAddress, 1, unit=ModbusSlaveId)
            elif regType == "hr":
                result = client.read_holding_registers(ModbusVariableAddress, 1, unit=ModbusSlaveId)
            elif regType == "ir":
                result = client.read_input_registers(ModbusVariableAddress, 1, unit=ModbusSlaveId)

            if result.function_code < 0x80:
                LineResults.append(result.registers[0])
            else:
                LineResults.append('!')
                print(TimeNowStr + '     ' + 'Modbus Read Failure:' + str(result.function_code) + '  ' +
                                'ModbusVariableName:' + ModbusVariableName + '  ' +
                                'ModbusSlaveID:' + str(ModbusSlaveId) + '  ' +
                                'ModbusVariableAddress:'+str(ModbusVariableAddress))

        except:
            LineResults.append('?')
            print(TimeNowStr + '     ' + 'Modbus Read Failure: {' + str(result) + '}  ' +
                  'ModbusVariableName:' + ModbusVariableName + '  ' +
                  'ModbusSlaveID:' + str(ModbusSlaveId) + '  ' +
                  'ModbusVariableAddress:' + str(ModbusVariableAddress))

    if len(LineResults) != 0:
        Line = TimeNowStr

        for LineResult in LineResults:
            Line += ',' + str(LineResult)
        print(Line)
        LogFile.write(Line + '\n')
        LogFile.flush()

    time.sleep(PoolingInterval/1000)

