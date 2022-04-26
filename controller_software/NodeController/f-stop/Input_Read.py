# FLUENCE_ID:tYxT8PNPYx8
# FLUENCE_TYPE:ATOMIC:               -> means Atomic Skript or collection (allowed are: “ATOMIC” or “COLLECTION”)
# FLUENCE_SYS:ADVANCION:                    -> The System  which the script belongs to
# FLUENCE_DESC: Read value from Input Register:   -> Description
# FLUENCE_CATEGORY: CATEGORY 1:      -> Category
# FLUENCE_COMMAND:Input_Read: -> function name later below in this script
# FLUENCE_P0:IP Address=127.0.0.1: -> String Input
# FLUENCE_P1:Port=5020: -> Integer Input 
# FLUENCE_P1:Modbus Register=4000: -> Integer Input 
# FLUENCE_P1:Unit=1: -> Integer Input
# FLUENCE_P3:Data Type=8int|8uint|16int|16uint|32int|32uint|32float|64int|64uint|64float: -> normal pull down
# FLUENCE_P1:Offset=0: -> Integer Input (2= float)
# FLUENCE_P2:Multiplier=1: -> Integer Input (2= float)
# FLUENCE_P2:Adder=0: -> Integer Input (2= float)
# FLUENCE_P0:Store Result Into=myVar


import sys
import fluencelog
import decimal
import GLOBAL
logger = fluencelog.initlog()
from pymodbus.client.sync import ModbusTcpClient
from pymodbus.constants import Endian
from pymodbus.payload import BinaryPayloadDecoder

def Input_Read(IP_Adress, Port, Adress, Unit, DataType, Offset, Multiplier, Adder, storeResultInto=""):
	#try:
	logger.debug('Try to connect')
	client = ModbusTcpClient(IP_Adress, int(Port))
	client.connect()
	logger.debug('Read register')

	rr=0
	result=0

	if DataType.startswith('8'):
		logger.debug("Type starts with 8")
		rr = client.read_input_registers(int(Adress)+int(Offset),1,unit=int(Unit))
		if "uint" not in DataType:
			decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
			result = decoder.decode_8bit_int()
		else:
			decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
			result = decoder.decode_8bit_uint()
		client.close()
	elif DataType.startswith('16'):
		logger.debug("Type starts with 16")
		rr = client.read_input_registers(int(Adress)+int(Offset),1,unit=int(Unit))
		if "uint" not in DataType:
			decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
			result = decoder.decode_16bit_int()
		else:
			decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
			result = decoder.decode_16bit_uint()
		client.close()
	elif DataType.startswith('32'):
		logger.debug("Type starts with 32")
		rr = client.read_input_registers(int(Adress)+int(Offset),2,unit=int(Unit))
		if "uint" not in DataType:
			if "float" not in DataType:
				decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
				result = decoder.decode_32bit_int()
			else:
				decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
				result = decoder.decode_32bit_float()
				logger.debug("the value is %s",str(result))
		else:
			decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
			result = decoder.decode_32bit_uint()
		client.close()
	elif DataType.startswith('64'):
		logger.debug("Type starts with 64")
		rr = client.read_input_registers(int(Adress)+int(Offset),4,unit=int(Unit))
		if "uint" not in DataType:
			if "float" not in DataType:
				decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
				result = decoder.decode_64bit_float()
			else:
				decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
				result = decoder.decode_64bit_int()
		else:
			decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
			result = decoder.decode_64bit_uint()
		client.close()
	else:
		logger.debug("Type start error")

	assert(rr.function_code < 0x80)     # test that we are not an error
	#print (rr)
	#print (rr.registers)

	result = decimal.Decimal(result) * decimal.Decimal(Multiplier)
	result = decimal.Decimal(result) + decimal.Decimal(Adder)

	GLOBAL.XChangeData[storeResultInto] = result

	#print (result)
	return (result)


