/*
 * LeafController_WebServer.h
 *
 *  Created on: 12.06.2019
 *      Author: Boris.Kajganic
 */

#ifndef LCWEBSERVER_H_
#define LCWEBSERVER_H_

void HandleSoftReset(void);

void HandleAll_GET();

void HandleRelays_GET();
void HandleRelay_GET();
void HandleRelay_PUT();

void HandleDigitalOutputs_GET();
void HandleDigitalOutput_GET();
void HandleDigitalOutput_PUT();

void HandleDigitaInputs_GET();
void HandleDigitalInput_GET();
//void HandleDigitalInput_PUT();

#if defined(USE_MODBUS_RTU_CLIENT)
void HandleModbus_GET();
#endif

void Relay2Json(char* content, size_t size, uint8_t state);
void Relay2Json(char* content, size_t size, uint8_t i, uint8_t /* state */);

void RelayPrint(char* line, uint8_t state);
void RelayPrint(char* line, uint8_t i, uint8_t state);

void DigitalOutput2Json(char* content, size_t size, uint8_t state);
void DigitalOutput2Json(char* content, size_t size, uint8_t i, uint8_t /* state */);

void DigitalOutputPrint(char* line, uint8_t state);
void DigitalOutputPrint(char* line, uint8_t i, uint8_t state);

void DigitalInput2Json(char* content, size_t size, uint8_t state);
void DigitalInput2Json(char* content, size_t size, uint8_t i, uint8_t /* state */);

void DigitalInputPrint(char* line, uint8_t state);
void DigitalInputPrint(char* line, uint8_t i, uint8_t state);


void ModbusRTUPrint(char* line);

extern SimpleWebServer webServer;

/*
class LCWebServer
{
public:
  LCWebServer();
  virtual ~LCWebServer();
};
*/

#endif /* LEAFCONTROLLER_TEST_H_ */
