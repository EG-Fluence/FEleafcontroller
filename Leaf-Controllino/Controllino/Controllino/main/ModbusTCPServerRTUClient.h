/*
 * ModbusTCPServerRTUClient.h
 *
 *  Created on: 21.08.2019
 *      Author: Boris.Kajganic
 */

#ifndef MODBUSTCPSERVERRTUCLIENT_H_
#define MODBUSTCPSERVERRTUCLIENT_H_

#include <ModbusRTUClient.h>
#include <ModbusTCPServer.h>

class ModbusTCPServerRTUClient: public ModbusTCPServer
{
public:
  ModbusTCPServerRTUClient();
  virtual ~ModbusTCPServerRTUClient();

  /**
   * Accept client connection
   *
   * @param client client to accept
   */
  void attachModbusRTUClient(ModbusRTUClientClass& client);

  /**
   * Poll accepted client for requests
   */
  virtual void poll();

protected:
  ModbusRTUClientClass* _modbusRTUClient;
};

#endif /* MODBUSTCPSERVERRTUCLIENT_H_ */
