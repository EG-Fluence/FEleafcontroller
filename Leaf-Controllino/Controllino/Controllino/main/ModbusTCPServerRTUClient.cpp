/*
 * ModbusTCPServerRTUClient.cpp
 *
 *  Created on: 21.08.2019
 *      Author: Boris.Kajganic
 */

#include <stdio.h>
//#include <string.h>
//#include <stdlib.h>
//#include <stdarg.h>
#include <errno.h>
//#include <limits.h>
//#include <time.h>


extern "C"
{
#include "libmodbus/modbus.h"
#include "libmodbus/modbus-private.h"
#include "libmodbus/modbus-tcp.h"
#include "libmodbus/modbus-rtu.h"
}

#include "ModbusTCPServerRTUClient.h"

extern "C"
{
void _sleep_response_timeout(modbus_t *ctx);
int response_exception(modbus_t *ctx, sft_t *sft,
                              int exception_code, uint8_t *rsp,
                              unsigned int to_flush,
                              const char* templ, ...);
int send_msg(modbus_t *ctx, uint8_t *msg, int msg_length);
int response_io_status(uint8_t *tab_io_status,
                              int address, int nb,
                              uint8_t *rsp, int offset);
}


ModbusTCPServerRTUClient::ModbusTCPServerRTUClient() : _modbusRTUClient(NULL)
{
  // TODO Auto-generated constructor stub

}

ModbusTCPServerRTUClient::~ModbusTCPServerRTUClient()
{
  // TODO Auto-generated destructor stub
}

void ModbusTCPServerRTUClient::attachModbusRTUClient(ModbusRTUClientClass& client)
{
  _modbusRTUClient = &client;
}

void ModbusTCPServerRTUClient::poll()
{
  if(_modbusRTUClient == NULL) // ignore work as standard ModbusTCPServer
    return ModbusTCPServer::poll();


  if (_client != NULL)
  {
    uint8_t request[MODBUS_TCP_MAX_ADU_LENGTH];

    int requestLength = modbus_receive(_mb, request);

    if (requestLength > 0)   // _mb is valid
    {
      int offset= _mb->backend->header_length;
      int slave = request[offset - 1];

      if(_mb->slave == slave) // As normal ModbusTCPServer
      {
        modbus_reply(_mb, request, requestLength, &_mbMapping);
      }
      else // reroute request to RS485
      {
        int function;
        uint16_t address;
        int rsp_length = 0;
        sft_t sft;
        uint8_t rsp[MODBUS_TCP_MAX_ADU_LENGTH];
        bool bRead = false;
        int nb = 0;

        function = request[offset];
        address = (request[offset + 1] << 8) + request[offset + 2];

        sft.slave = slave;
        sft.function = function;
        sft.t_id = _mb->backend->prepare_response_tid(request, &requestLength);

        switch(function)
        {
          case MODBUS_FC_READ_COILS:
          case MODBUS_FC_READ_DISCRETE_INPUTS: {
                                                 bRead = false;
                                                 nb = (request[offset + 3] << 8) + request[offset + 4];

                                                 if(function == MODBUS_FC_READ_DISCRETE_INPUTS)
                                                 {
                                                   if(!_modbusRTUClient->requestFrom(slave, DISCRETE_INPUTS, address, nb))
                                                   {
                                                     // Error
                                                     Serial.print("Modbus ");
                                                     Serial.print(slave);
                                                     Serial.print(": Failed to read discrete inputs! ");
                                                     Serial.println(_modbusRTUClient->lastError());
                                                   }
                                                   else
                                                     bRead = true;
                                                 }
                                                 else
                                                 {
                                                   if(!_modbusRTUClient->requestFrom(slave, COILS, address, nb))
                                                   {
                                                     // Error
                                                     Serial.print("Modbus ");
                                                     Serial.print(slave);
                                                     Serial.print(": Failed to read coils! ");
                                                     Serial.println(_modbusRTUClient->lastError());
                                                   }
                                                   else
                                                     bRead = true;
                                                 }

                                                 if(bRead == true)
                                                 {
                                                   rsp_length = _mb->backend->build_response_basis(&sft, rsp);
                                                   rsp[rsp_length++] = (nb / 8) + ((nb % 8) ? 1 : 0);

                                                   uint8_t tab_bits[nb];
                                                   for(int i = 0; i < nb; i++)
                                                   {
                                                     tab_bits[i] = _modbusRTUClient->read();
                                                   }
                                                   rsp_length = response_io_status(tab_bits, 0, nb, rsp, rsp_length);
                                                 }
                                                 else
                                                 {
                                                   rsp_length = response_exception(_mb, &sft, MODBUS_EXCEPTION_SLAVE_OR_SERVER_FAILURE, rsp, TRUE,
                                                       "Modbus function code: 0x%0X, slave: %d, address: %d, number: %d, Error: %s\n", function, slave, address, nb, _modbusRTUClient->lastError());
                                                 }
                                               }
                                               break;

          case MODBUS_FC_READ_HOLDING_REGISTERS:
          case MODBUS_FC_READ_INPUT_REGISTERS: {
                                                 bRead = false;
                                                 nb = (request[offset + 3] << 8) + request[offset + 4];
//                                                 Serial.println(slave);
//                                                 Serial.println(address);
//                                                 Serial.println(nb);

                                                 if(function == MODBUS_FC_READ_INPUT_REGISTERS)
                                                 {
                                                   if(!_modbusRTUClient->requestFrom(slave, INPUT_REGISTERS, address, nb))
                                                   {
                                                     // Error
                                                     Serial.print("Modbus ");
                                                     Serial.print(slave);
                                                     Serial.print(": Failed to read input registers! ");
                                                     Serial.println(_modbusRTUClient->lastError());
                                                   }
                                                   else
                                                     bRead = true;
                                                 }
                                                 else
                                                 {
                                                   if(!_modbusRTUClient->requestFrom(slave, HOLDING_REGISTERS, address, nb))
                                                   {
                                                     // Error
                                                     Serial.print("Modbus ");
                                                     Serial.print(slave);
                                                     Serial.print(": Failed to read holding registers! ");
                                                     Serial.println(_modbusRTUClient->lastError());
                                                   }
                                                   else
                                                     bRead = true;
                                                 }

                                                 if(bRead)
                                                 {
                                                   rsp_length = _mb->backend->build_response_basis(&sft, rsp);
                                                   rsp[rsp_length++] = nb << 1;
                                                   //Serial.println(nb);
                                                   for(int i = 0; i < nb; i++)
                                                   {
                                                     uint16_t raw = _modbusRTUClient->read();
                                                     //Serial.println(raw);
                                                     rsp[rsp_length++] = raw >> 8;
                                                     rsp[rsp_length++] = raw & 0xFF;

//                                                     Serial.println(raw);
//                                                     Serial.println(raw >> 8);
//                                                     Serial.println(raw & 0xFF);
                                                   }
                                                 }
                                                 else
                                                 {
                                                   rsp_length = response_exception(_mb, &sft, MODBUS_EXCEPTION_SLAVE_OR_SERVER_FAILURE, rsp, TRUE,
                                                       "Modbus function code: 0x%0X, slave: %d, address: %d, number: %d, Error: %s\n", function, slave, address, nb, _modbusRTUClient->lastError());
                                                 }
                                               }
                                               break;

          case MODBUS_FC_WRITE_SINGLE_COIL: {
                                              int data = (request[offset + 3] << 8) + request[offset + 4];
                                              #if defined(ARDUINO) && defined(__AVR__)
                                                if (data == (int)0xFF00 || data == 0x0)
                                              #else
                                                if (data == 0xFF00 || data == 0x0)
                                              #endif
                                                {
                                                  //mb_mapping->tab_bits[mapping_address] = data ? ON : OFF;
                                                  if(!_modbusRTUClient->coilWrite(slave, address, data ? ON : OFF))
                                                  {
                                                    // Error
                                                    Serial.print("Modbus ");
                                                    Serial.print(slave);
                                                    Serial.print(": Failed to write coil! ");
                                                    Serial.println(_modbusRTUClient->lastError());

                                                    rsp_length = response_exception(_mb, &sft, MODBUS_EXCEPTION_SLAVE_OR_SERVER_FAILURE, rsp, TRUE,
                                                        "Modbus function code: 0x%0X, slave: %d, address: %d, Error: %s\n", function, slave, address, _modbusRTUClient->lastError());

                                                  }
                                                  else
                                                  {
                                                    memcpy(rsp, request, requestLength);
                                                    rsp_length = requestLength;
                                                  }
                                                }
                                                else
                                                {
                                                  rsp_length = response_exception(_mb, &sft,
                                                               MODBUS_EXCEPTION_ILLEGAL_DATA_VALUE, rsp, FALSE,
                                                               "Illegal data value 0x%0X in write_bit request at address %0X\n",
                                                               data, address);
                                                }
                                            }
                                            break;

          case MODBUS_FC_WRITE_SINGLE_REGISTER: {
                                                  int data = (request[offset + 3] << 8) + request[offset + 4];
                                                  //mb_mapping->tab_registers[mapping_address] = data;

                                                  if(!_modbusRTUClient->holdingRegisterWrite(slave, address, data))
                                                  {
                                                    // Error
                                                    Serial.print("Modbus ");
                                                    Serial.print(slave);
                                                    Serial.print(": Failed to write holding register! ");
                                                    Serial.println(_modbusRTUClient->lastError());

                                                    rsp_length = response_exception(_mb, &sft, MODBUS_EXCEPTION_SLAVE_OR_SERVER_FAILURE, rsp, TRUE,
                                                        "Modbus function code: 0x%0X, slave: %d, address: %d, Error: %s\n", function, slave, address, _modbusRTUClient->lastError());
                                                  }
                                                  else
                                                  {
                                                    memcpy(rsp, request, requestLength);
                                                    rsp_length = requestLength;
                                                  }
                                                }
                                                break;

          case MODBUS_FC_WRITE_MULTIPLE_COILS: {
                                                 int nb = (request[offset + 3] << 8) + request[offset + 4];
                                                 if (nb < 1 || MODBUS_MAX_WRITE_BITS < nb)
                                                 {
                                                   /* May be the indication has been truncated on reading because of
                                                    * invalid address (eg. nb is 0 but the request contains values to
                                                    * write) so it's necessary to flush. */
                                                   rsp_length = response_exception(_mb, &sft, MODBUS_EXCEPTION_ILLEGAL_DATA_VALUE, rsp, TRUE,
                                                         "Illegal number of values %d in write_bits (max %d)\n", nb, MODBUS_MAX_WRITE_BITS);
                                                 }
                                                 else
                                                 {
                                                   uint8_t tab_bits[nb];
                                                   /* 6 = byte count */
                                                   // modbus_set_bits_from_bytes(mb_mapping->tab_bits, mapping_address, nb, &req[offset + 6]);
                                                   modbus_set_bits_from_bytes(tab_bits, 0, nb, &request[offset + 6]);

                                                   // write nb Coil values to (slave) id, address
                                                   _modbusRTUClient->beginTransmission(slave, COILS, address, nb);

                                                   for(int i = 0; i < nb; i++)
                                                   {
                                                     _modbusRTUClient->write(tab_bits[i]);
                                                   }

                                                   if (!_modbusRTUClient->endTransmission())
                                                   {
                                                     Serial.print("Modbus ");
                                                     Serial.print(slave);
                                                     Serial.print(": Failed to write coils! ");
                                                     Serial.println(ModbusRTUClient.lastError());

                                                     rsp_length = response_exception(_mb, &sft, MODBUS_EXCEPTION_SLAVE_OR_SERVER_FAILURE, rsp, TRUE,
                                                         "Modbus function code: 0x%0X, slave: %d, address: %d, number: %d, Error: %s\n", function, slave, address, nb, _modbusRTUClient->lastError());
                                                   }
                                                   else
                                                   {
                                                     rsp_length = _mb->backend->build_response_basis(&sft, rsp);
                                                     /* 4 to copy the bit address (2) and the quantity of bits */
                                                     memcpy(rsp + rsp_length, request + rsp_length, 4);
                                                     rsp_length += 4;
                                                   }
                                                 }
                                               }
                                               break;

          case MODBUS_FC_WRITE_MULTIPLE_REGISTERS: {
                                                     int nb = (request[offset + 3] << 8) + request[offset + 4];

                                                     if(nb < 1 || MODBUS_MAX_WRITE_REGISTERS < nb)
                                                     {
                                                       rsp_length = response_exception(_mb, &sft, MODBUS_EXCEPTION_ILLEGAL_DATA_VALUE, rsp, TRUE,
                                                           "Illegal number of values %d in write_registers (max %d)\n",
                                                           nb, MODBUS_MAX_WRITE_REGISTERS);
                                                     }
                                                     else
                                                     {
                                                       uint16_t tab_registers[nb];
                                                       int i, j;
                                                       for (i = 0, j = 6; i < nb; i++, j += 2)
                                                       {
                                                           /* 6 and 7 = first value */
                                                         tab_registers[i] = (request[offset + j] << 8) + request[offset + j + 1];
                                                       }

                                                       // write nb holding resister values to (slave) id, address
                                                       _modbusRTUClient->beginTransmission(slave, HOLDING_REGISTERS, address, nb);

                                                       for(int i = 0; i < nb; i++)
                                                       {
                                                         _modbusRTUClient->write(tab_registers[i]);
                                                       }

                                                       if (!_modbusRTUClient->endTransmission())
                                                       {
                                                         Serial.print("Modbus ");
                                                         Serial.print(slave);
                                                         Serial.print(": Failed to write holding registers! ");
                                                         Serial.println(ModbusRTUClient.lastError());

                                                         rsp_length = response_exception(_mb, &sft, MODBUS_EXCEPTION_SLAVE_OR_SERVER_FAILURE, rsp, TRUE,
                                                             "Modbus function code: 0x%0X, slave: %d, address: %d, number: %d, Error: %s\n", function, slave, address, nb, _modbusRTUClient->lastError());
                                                       }
                                                       else
                                                       {
                                                         rsp_length = _mb->backend->build_response_basis(&sft, rsp);
                                                         /* 4 to copy the address (2) and the no. of registers */
                                                         memcpy(rsp + rsp_length, request + rsp_length, 4);
                                                         rsp_length += 4;
                                                       }
                                                     }
                                                   }
                                                   break;

//          case MODBUS_FC_REPORT_SLAVE_ID: {
//                                          }
//                                          break;
//
//          case MODBUS_FC_READ_EXCEPTION_STATUS: {
//                                                }
//                                                break;
//
//          case MODBUS_FC_MASK_WRITE_REGISTER: {
//                                              }
//                                              break;
//
//          case MODBUS_FC_WRITE_AND_READ_REGISTERS: {
//                                                   }
//                                                   break;
//
          default:
              rsp_length = response_exception(_mb, &sft, MODBUS_EXCEPTION_ILLEGAL_FUNCTION, rsp, TRUE,
                  "Unknown Modbus function code: 0x%0X\n", function);
              break;
        }

        // Send response
        (slave == MODBUS_BROADCAST_ADDRESS) ? 0 : send_msg(_mb, rsp, rsp_length);
      }
    }
  }
}
