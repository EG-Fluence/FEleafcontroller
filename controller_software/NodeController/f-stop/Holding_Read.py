
import sys
import decimal
import fluencelog
import GLOBAL
logger = fluencelog.initlog()
from pymodbus.client.sync import ModbusTcpClient
from pymodbus.constants import Endian
from pymodbus.payload import BinaryPayloadDecoder

def Holding_Read(IP_Adress, Port, Adress, Unit, DataType, Offset, Multiplier, Adder, storeResultInto=""):
	#try:
	logger.debug('Try to connect')
	client = ModbusTcpClient(IP_Adress, int(Port))
	client.connect()
	logger.debug('Read register')

	rr=0
	result=0

	if DataType.startswith('8'):
		logger.debug("Type starts with 8")
		rr = client.read_holding_registers(int(Adress)+int(Offset),1,unit=int(Unit))
		if "uint" not in DataType:
			decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
			result = decoder.decode_8bit_int()
		else:
			decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
			result = decoder.decode_8bit_uint()
		client.close()
	elif DataType.startswith('16'):
		logger.debug("Type starts with 16")
		rr = client.read_holding_registers(int(Adress)+int(Offset),1,unit=int(Unit))
		if "uint" not in DataType:
			decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
			result = decoder.decode_16bit_int()
		else:
			decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
			result = decoder.decode_16bit_uint()
		client.close()
	elif DataType.startswith('32'):
		logger.debug("Type starts with 32")
		rr = client.read_holding_registers(int(Adress)+int(Offset),2,unit=int(Unit))
		if "uint" not in DataType:
			if "float" not in DataType:
				decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Little)  # !!!!!! Something strange with my libmodbus stuff
				result = decoder.decode_32bit_int()
				logger.debug(result)
			else:
				decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
				result = decoder.decode_32bit_float()
		else:
			decoder = BinaryPayloadDecoder.fromRegisters(rr.registers, byteorder=Endian.Big, wordorder=Endian.Big)
			result = decoder.decode_32bit_uint()
		client.close()
	elif DataType.startswith('64'):
		logger.debug("Type starts with 64")
		rr = client.read_holding_registers(int(Adress)+int(Offset),4,unit=int(Unit))
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
	logger.debug(rr)
	logger.debug(rr.registers)

	result = decimal.Decimal(result) * decimal.Decimal(Multiplier)
	result = decimal.Decimal(result) + decimal.Decimal(Adder)

	GLOBAL.XChangeData[storeResultInto] = result

	logger.debug(result)
	return (result)


