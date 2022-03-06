/*
 * LeafController_ModbusTCPServer.cpp
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

#include "LCModbusTCPServer.h"
#include "LeafController.h"
#include "LCInputOutput.h"

#if defined(USE_MODBUS_TCP_SERVER) || defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)


extern uint8_t  lcRelaySet[RELAY_COUNT];
extern uint16_t lcOutputSet[DIGITAL_OUT_COUNT];
extern uint16_t lcInputGet[DIGITAL_IN_COUNT + TEMPSENSORDS18B20_MAX + INTERNALVALUES_MAX];
/*
 #include <SPI.h>
 // #include <WiFiNINA.h> // for MKR WiFi 1010
 // #include <WiFi101.h> // for MKR1000

 #include <Ethernet.h>
 */

#if   defined(__AVR__)
#include <SPI.h>
#include <Ethernet.h>
#elif defined(ESP8266)
#include <ESP8266WiFi.h>
#endif

//#include <ArduinoRS485.h> // ArduinoModbus depends on the ArduinoRS485 library

#if defined(USE_MODBUS_TCP_SERVER)
#include <ArduinoModbus.h>
#elif defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
#include "ModbusTCPServerRTUClient.h"
#endif

// Initialize the Ethernet server library
// with the IP address and port you want to use
// (port 502 is default for Modbus TCP):
EthernetServer server(MODBUS_TCP_SERVER_PORT);

#if defined(USE_MODBUS_TCP_SERVER)
ModbusTCPServer modbusTCPServer;
#elif defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
ModbusTCPServerRTUClient modbusTCPServer;
#endif

/*
LCModbusTCPServer::LCModbusTCPServer()
{
  // TODO Auto-generated constructor stub

}

LCModbusTCPServer::~LCModbusTCPServer()
{
  // TODO Auto-generated destructor stub
}
*/

// The value will quickly become too large for an int to store
unsigned long previousMillis = 0;      // will store last time timer was updated

// constants won't change:
const unsigned long interval = 60000;  // interval at which to restart communication (milliseconds)

void ModbusTCPServerSetup()
{
  // start the server
  server.begin();
  Serial.print("Modbus server is at ");
  Serial.println(Ethernet.localIP());

  // start the Modbus TCP server
  if(!modbusTCPServer.begin(MODBUS_TCP_SLAVE_ID))
  {
    Serial.println("Failed to start Modbus TCP Server!");
    while(1)
      ;
  }

#if defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
  if(!ModbusRTUClient.begin(MODBUS_RTU_BAUDRATE, MODBUS_RTU_CONFIG))
  {
    Serial.println("Failed to start Modbus RTU Client!");
    while(1)
      ;
  }
  ModbusRTUClient.setTimeout(MODBUS_CLIENT_TIMEOUT);
  // Allocate memory, worst case, max of all required memory in client
  if(!ModbusRTUClient.AllocateMemory(HOLDING_REGISTERS, MODBUS_RTU_CLIENT_NUMBER_OF_ALLOCATED_VALUES))
  {
    Serial.println("Failed to allocate memory for Modbus RTU Client!");
    while(1)
      ;
  }
  modbusTCPServer.attachModbusRTUClient(ModbusRTUClient);
#endif

  // configure Relays at address 0x00
  if(modbusTCPServer.configureCoils(0x00, RELAY_COUNT) != 1)
    Serial.println("ModbusTCPServer Error: configureCoils");

  // configure Digital Outputs at address 0x00
  if(modbusTCPServer.configureHoldingRegisters(0x00, DIGITAL_OUT_COUNT) != 1)
    Serial.println("ModbusTCPServer Error: configureInputRegisters");

  // configure Digital Inputs at address 0x00
  if(modbusTCPServer.configureInputRegisters(0x00, DIGITAL_IN_COUNT + TEMPSENSORDS18B20_MAX + INTERNALVALUES_MAX) != 1)
    Serial.println("ModbusTCPServer Error: configureInputRegisters");

}

void ModbusTCPServerUpdateRelays()
{
  for(int i = 0; i < RELAY_COUNT; i++) // for all relays
  {
    // read the current value of the coil
    int8_t state = modbusTCPServer.coilRead(i);

    if(state != -1)
    {
      if(lcRelaySet[i] != state)
        UpdateRelay(i, state);  // set state of relay i
    }
  }
}

void ModbusTCPServerUpdateOutputs()
{
  for(int i = 0; i < DIGITAL_OUT_COUNT; i++) // for all digital outputs
  {
    // read the current value of the coil
    int16_t state = modbusTCPServer.holdingRegisterRead(i);

    if(state != -1)
    {
      if(lcOutputSet[i] != (uint16_t) state)
        UpdateOutput(i, state);  // set state of relay i
    }
  }
}

void ModbusTCPServerUpdateInputs()
{
  for(int i = 0; i < DIGITAL_IN_COUNT; i++) // for all digital outputs
  {
    UpdateInput(i);  // set state of digital output i
  }
}

void ModbusTCPServerLoop()
{
  for(int i = 0; i < MAX_SOCK_NUM; i++)
  {
    // listen for incoming clients
    EthernetClient client = server.available();

    if(client)
    {
      // a new client connected
      //Serial.println("Modbus new client");

      // let the Modbus TCP accept the connection
      modbusTCPServer.accept(client);

      //while(client.connected())
      if(client.connected())
      {
        // poll for Modbus TCP requests, while client connected
        modbusTCPServer.poll();

        ModbusTCPServerUpdateRelays();
        ModbusTCPServerUpdateOutputs();
        ModbusTCPServerUpdateInputs();

/*
        lcInputGet[DIGITAL_IN_COUNT + TEMPSENSORDS18B20_MAX + 1]++;

        // One tick delay (15ms) in between reads for stability
        vTaskDelay(TASK_DELAY_MIN); // -> 15 ms, remove vTaskDelay if ModbusTCPServerLoop is called directly without TaskModbusTCPServer

        unsigned long currentMillis = millis();

        if(currentMillis - previousMillis >= interval)
        {
            // save the last time
            previousMillis = currentMillis;

            // CLOSE the connection
            client.stop();
        }
        Serial.println("client disconnected");
 */
      }
      else
        client.stop();
    }
  }
}

#if defined(USE_FRERTOS)
void TaskModbusTCPServer(void* pvParameters)
{
  (void) pvParameters; // Not used
  while(true)
  {
    ModbusTCPServerLoop();
    // One tick delay (15ms) in between reads for stability
    vTaskDelay(5 * TASK_DELAY_MIN);
  }
}
#endif //USE_FRERTOS

#endif // #if defined(USE_MODBUS_TCP_SERVER) || defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
