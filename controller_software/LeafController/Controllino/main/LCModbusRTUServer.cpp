/*
 * LeafController_ModbusRTUServer.cpp
 *
 *  Created on: 12.06.2019
 *      Author: Boris.Kajganic
 */


/*
 Modbus object types:

 Object type        Access        Size
 Coil               Read-write    1 bit
 Discrete input     Read          1 bit
 Input register     Read          16 bits
 Holding registers  Read-write    16 bits

 Normally coils a are used to write digital values to an outputs.
 Discrete inputs are used to read digital inputs.
 Registers are use to communicate data between the devices and also usually used for analog I/Os.
 */

#include "LCModbusRTUServer.h"
#include "LeafController.h"
#include "LCInputOutput.h"

#if defined(USE_MODBUS_RTU_SERVER)

extern uint8_t  lcRelaySet[RELAY_COUNT];
extern uint16_t lcOutputSet[DIGITAL_OUT_COUNT];
extern uint16_t lcInputGet[DIGITAL_IN_COUNT + TEMPSENSORDS18B20_MAX];


//#include <ArduinoRS485.h> // ArduinoModbus depends on the ArduinoRS485 library
#include <ArduinoModbus.h>

//ModbusRTUServer modbusRTUServer;

/*
LCModbusRTUServer::LCModbusRTUServer()
{
  // TODO Auto-generated constructor stub

}

LCModbusRTUServer::~LCModbusRTUServer()
{
  // TODO Auto-generated destructor stub
}
*/


void ModbusRTUServerSetup()
{
  // start the Modbus RTU server, with (slave) with Slave_id
  if(!ModbusRTUServer.begin(MODBUS_RTU_SLAVE_ID, MODBUS_RTU_BAUDRATE, MODBUS_RTU_CONFIG))
  {
    Serial.println("Failed to start Modbus RTU Server!");
    while (1);
  }


  // configure Relays at address 0x00
  if(ModbusRTUServer.configureCoils(0x00, RELAY_COUNT) != 1)
    Serial.println("ModbusTCPServer Error: configureCoils");

  // configure Digital Outputs at address 0x00
  if(ModbusRTUServer.configureHoldingRegisters(0x00, DIGITAL_OUT_COUNT) != 1)
    Serial.println("ModbusTCPServer Error: configureInputRegisters");

  // configure Digital Inputs at address 0x00
  if(ModbusRTUServer.configureInputRegisters(0x00, DIGITAL_IN_COUNT + TEMPSENSORDS18B20_MAX + INTERNALVALUES_MAX) != 1)
    Serial.println("ModbusTCPServer Error: configureInputRegisters");

}

void ModbusRTUServerUpdateRelays()
{
  for(int i = 0; i < RELAY_COUNT; i++) // for all relays
  {
    // read the current value of the coil
    int8_t state = ModbusRTUServer.coilRead(i);

    if(state != -1)
    {
      if(lcRelaySet[i] != state)
        UpdateRelay(i, state);  // set state of relay i
    }
  }
}

void ModbusRTUServerUpdateOutputs()
{
  for(int i = 0; i < DIGITAL_OUT_COUNT; i++) // for all digital outputs
  {
    // read the current value of the coil
    int16_t state = ModbusRTUServer.holdingRegisterRead(i);

    if(state != -1)
    {
      if(lcOutputSet[i] != (uint16_t) state)
        UpdateOutput(i, state);  // set state of digital output i
    }
  }
}

void ModbusRTUServerUpdateInputs()
{
  for(int i = 0; i < DIGITAL_IN_COUNT; i++) // for all digital outputs
  {
    UpdateInput(i);  // set state of digital input i
  }
}

void ModbusRTUServerLoop()
{
   // poll for Modbus RTU requests, while client connected
   ModbusRTUServer.poll();

   ModbusRTUServerUpdateRelays();
   ModbusRTUServerUpdateOutputs();
   ModbusRTUServerUpdateInputs();

   // One tick delay (15ms) in between reads for stability
   vTaskDelay(TASK_DELAY_MIN); // -> 15 ms, remove vTaskDelay if ModbusRTUServerLoop is called directly without TaskModbusRTUServer
}

#if defined(USE_FRERTOS)
void TaskModbusRTUServer(void* pvParameters)
{
  (void) pvParameters; // Not used
  while(true)
  {
    ModbusRTUServerLoop();
    // One tick delay (15ms) in between reads for stability
    vTaskDelay(5 * TASK_DELAY_MIN);
  }
}
#endif //USE_FRERTOS

#endif // #if defined(USE_MODBUS_RTU_SERVER)
