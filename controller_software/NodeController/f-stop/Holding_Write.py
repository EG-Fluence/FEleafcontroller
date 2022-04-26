# FLUENCE_ID:wdNyVEnGopw
# FLUENCE_TYPE:ATOMIC:               -> means Atomic Skript or collection (allowed are: “ATOMIC” or “COLLECTION”)
# FLUENCE_SYS:ADVANCION:                    -> The System  which the script belongs to
# FLUENCE_DESC: Write value to Modbus Register:   -> Description
# FLUENCE_CATEGORY: CATEGORY 1:      -> Category
# FLUENCE_COMMAND:Holding_Write: -> function name later below in this script
# FLUENCE_P0:IP_Address=127.0.0.1
# FLUENCE_P1:Port=502
# FLUENCE_P1:Modbus Register=4000
# FLUENCE_P3:Data Type=8int|8uint|16int|16uint|32int|32uint|32float|64int|64uint|64float: -> normal pull down
# FLUENCE_P2:Value=42
# FLUENCE_P2:Gain=1
# FLUENCE_P2:Adder=0
# FLUENCE_P2:Comment=This is a test

import sys
import fluencelog
import convertion
import decimal
logger = fluencelog.initlog()

from pymodbus.constants import Endian
from pymodbus.payload import BinaryPayloadBuilder
from pymodbus.client.sync import ModbusTcpClient

def Holding_Write(IP_Adress, Port, Adress, DataType, ValueStr, Gain, Adder,Comment):
	logger.debug('Try to connect')
	client = ModbusTcpClient(IP_Adress, Port)
	client.connect()		
	logger.debug('Write register')
	logger.debug('%s',Comment)

	builder = 0
	Value = decimal.Decimal(ValueStr) / decimal.Decimal(Gain)
	Value = decimal.Decimal(Value) - decimal.Decimal(Adder)
	
	if DataType.startswith('8'):
		logger.debug("Type starts with 8")
		if "uint" not in DataType:
			builder = BinaryPayloadBuilder(byteorder=Endian.Big, wordorder=Endian.Big)
			converted = int(Value)
			builder.add_8bit_int(converted)
		else:
			builder = BinaryPayloadBuilder(byteorder=Endian.Big, wordorder=Endian.Big)
			converted = int(Value)
			builder.add_8bit_uint(converted)

	elif DataType.startswith('16'):
		logger.debug("Type starts with 16")
		if "uint" not in DataType:
			builder = BinaryPayloadBuilder(byteorder=Endian.Big, wordorder=Endian.Big)
			converted = int(Value)
			builder.add_16bit_int(converted)
		else:
			builder = BinaryPayloadBuilder(byteorder=Endian.Big, wordorder=Endian.Big)
			converted = int(Value)
			builder.add_16bit_uint(converted)
	elif DataType.startswith('32'):
		logger.debug("Type starts with 32")
		if "uint" not in DataType:
			if "float" not in DataType:
				builder = BinaryPayloadBuilder(byteorder=Endian.Big, wordorder=Endian.Big)
				converted = int(Value)
				builder.add_32bit_int(converted)
			else:
				builder = BinaryPayloadBuilder(byteorder=Endian.Big, wordorder=Endian.Big)
				converted = float(Value)
				builder.add_32bit_float(converted)
		else:
			builder = BinaryPayloadBuilder(byteorder=Endian.Big, wordorder=Endian.Big)
			converted = int(Value)
			builder.add_32bit_uint(converted)
	elif DataType.startswith('64'):
		logger.debug("Type starts with 64")
		if "uint" not in DataType:
			if "float" not in DataType:
				builder = BinaryPayloadBuilder(byteorder=Endian.Big, wordorder=Endian.Big)
				converted = int(Value)
				builder.add_64bit_int(converted)
			else:
				builder = BinaryPayloadBuilder(byteorder=Endian.Big, wordorder=Endian.Big)
				converted = float(Value)
				builder.add_64bit_float(converted)
		else:
			builder = BinaryPayloadBuilder(byteorder=Endian.Big, wordorder=Endian.Big)
			converted = int(Value)
			builder.add_64bit_uint(converted)
	else:
		logger.debug("Type start error")


	if builder != 0:
		payload=builder.to_registers()
		payload=builder.build() 
		rr=client.write_registers(int(Adress),payload,skip_encode=True, unit=1)
	return 


