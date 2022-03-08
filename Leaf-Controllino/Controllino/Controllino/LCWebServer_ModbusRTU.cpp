/*
 * LCWebServer_DigitalOutput.cpp
 *
 *  Created on: 25.06.2019
 *      Author: Boris.Kajganic
 */

#include "LeafController.h"
#include "LCInputOutput.h"
#include "LCWebServer.h"




#if defined(USE_WEB_SERVER)
#if defined(USE_MODBUS_RTU_CLIENT)

#if defined(EXAMPLE_MODBUS_RTU_CLIENT_WITH_KUNBUS_REVPI)

extern uint16_t ModbusSlaveRegisters[8];
extern float temperature1;
extern float temparature2;
extern float humidity;

// handle GET on "Modbus" commands
void HandleModbus_GET()
{                                             // check on path / args boundaries
//  if (( server.pathCount() <  1) || ( server.pathCount() > 2)) return;
//  if (( server.argsCount() <  0) || ( server.argsCount() > 1)) return;
//  if (( server.pathCount() == 2) && ( server.argsCount() > 0)) return;
//
  char reply[1024];
  strClr(reply);                   // reply buffer

  ModbusRTUPrint(reply);                                    // show relays with specific state
  webServer.respond(returnCode = 200, "text/plain", reply); // respond result to client
}

// print digital input state for single digital input (write buffer, relay i, requested state)
void ModbusRTUPrint(char* line)
{
  // To get the temperature in Celsius and the humidity reading as
   // a percentage, divide the raw value by 10.0.
//   float temperature1 = ((double)ModbusSlaveRegisters[0]) / 10.0;
//   float temparature2 = ModbusSlaveRegisters[1] / 10.0;
//   float humidity = ModbusSlaveRegisters[2] / 10.0;

   //snprintf(ans, 20, "%f", myf);
   sprintf(line, "Temperature 1:%f \nTemperature 2:%f \nHumidity:%f \n", temperature1, temparature2, humidity);

   //String s;
    Serial.println(temperature1);
    Serial.println(temparature2);
    Serial.println(humidity);

    Serial.println(line);


/*
  Serial.println("---------- READ RESPONSE RECEIVED ----");
  Serial.print("Slave ");
  Serial.print(SlaveModbusAdd, DEC);
  Serial.print(" Temp 1: ");
  Serial.print(ModbusSlaveRegisters[0], DEC);
  Serial.print(" , Temp 2: ");
  Serial.print(ModbusSlaveRegisters[1], DEC);
  Serial.print(" , Humidity: ");
  Serial.print(ModbusSlaveRegisters[2], DEC);
  Serial.println("-------------------------------------");
  Serial.println("");



  if((i >= 0) && (i < DIGITAL_IN_COUNT) &&                // check if DI is valid
      ((state == DI_ANY) || (state == lcInputGet[i])))
  {  // check if state is valid
    strcat(line, "# DI ");
    strcat(line, dec(i, 2));  // write state of relay i
    strcat(line, " on pin ");
    strcat(line, dec(lcInputGet[i], 2));
    strcat(line, " = ");
    strcat(line, lcInputGet[i] == DI_ON ? CMD_ON : CMD_OFF);
    strcat(line, "\r\n");
  }
  else
  {
    strcat(line, "# DI ");
    strcat(line, dec(i, 2));
    strcat(line, " not defined");
    strcat(line, "\r\n");   // undefined digital input
  }
*/
}

#endif //defined(EXAMPLE_MODBUS_RTU_CLIENT_WITH_KUNBUS_REVPI)

#endif // #if defined(USE_MODBUS_RTU_CLIENT)
#endif // #if defined(USE_WEB_SERVER)
