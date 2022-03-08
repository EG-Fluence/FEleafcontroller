/*
 * LeafController_ModbusRTUClient.cpp
 *
 *  Created on: 12.06.2019
 *      Author: Boris.Kajganic
 */

#include "LCModbusRTUClient.h"
#include "LeafController.h"
#include "LCInputOutput.h"

#if defined(USE_MODBUS_RTU_CLIENT)

#if defined(EXAMPLE_MODBUS_RTU_CLIENT_WITH_KUNBUS_REVPI)

//#include <ArduinoRS485.h> // ArduinoModbus depends on the ArduinoRS485 library
//#include <ArduinoModbus.h>

#include "ModbusRtu.h"


 float temperature1;
 float temparature2;
 float humidity;


// This MACRO defines Modbus master address.
// For any Modbus slave devices are reserved addresses in the range from 1 to 247.
// Important note only address 0 is reserved for a Modbus master device!
#define MasterModbusAdd  0
#define SlaveModbusAdd   1

// This MACRO defines number of the comport that is used for RS 485 interface.
// For MAXI and MEGA RS485 is reserved UART Serial3.
#define RS485Serial     3

// The object ControllinoModbuSlave of the class Modbus is initialized with three parameters.
// The first parametr specifies the address of the Modbus slave device.
// The second parameter specifies type of the interface used for communication between devices - in this sketch is used RS485.
// The third parameter can be any number. During the initialization of the object this parameter has no effect.
Modbus ControllinoModbusMaster(MasterModbusAdd, RS485Serial, 0);

// This uint16 array specified internal registers in the Modbus slave device.
// Each Modbus device has particular internal registers that are available for the Modbus master.
// In this example sketch internal registers are defined as follows:
// (ModbusSlaveRegisters 0 - 3 read only and ModbusSlaveRegisters 4 - 7 write only from the Master perspective):
// ModbusSlaveRegisters[0] - Read an analog value from the CONTROLLINO_A0 - returns value in the range from 0 to 1023.
// ModbusSlaveRegisters[1] - Read an digital value from the CONTROLLINO_D0 - returns only the value 0 or 1.
// ModbusSlaveRegisters[2] - Read the number of incoming messages - Communication diagnostic.
// ModbusSlaveRegisters[3] - Read the number of number of outcoming messages - Communication diagnostic.
// ModbusSlaveRegisters[4] - Sets the Relay output CONTROLLINO_R0 - only the value 0 or 1 is accepted.
// ModbusSlaveRegisters[5] - Sets the Relay output CONTROLLINO_R1 - only the value 0 or 1 is accepted.
// ModbusSlaveRegisters[6] - Sets the Relay output CONTROLLINO_R2 - only the value 0 or 1 is accepted.
// ModbusSlaveRegisters[7] - Sets the Relay output CONTROLLINO_R3 - only the value 0 or 1 is accepted.
uint16_t ModbusSlaveRegisters[8];

// This is an structure which contains a query to an slave device
modbus_t ModbusQuery[2];

uint8_t myState; // machine state
uint8_t currentQuery; // pointer to message query

unsigned long WaitingTime;

/*
LCModbusRTUClient::LCModbusRTUClient()
{
  // TODO Auto-generated constructor stub

}

LCModbusRTUClient::~LCModbusRTUClient()
{
  // TODO Auto-generated destructor stub
}
*/

void ModbusRTUClientSetup()
{
  // start the Modbus RTU client

  /* Here we initialize RS485 serial at 9600 baudrate for communication */
  //Serial3.begin(9600);
  /* This will initialize Controllino RS485 pins */
//  Controllino_RS485Init(9600);
  //pinMode(LED_BUILTIN, OUTPUT);
  /*
   void RS485Class::setPins(int txPin, int dePin, int rePin)
   {
   _txPin = txPin;
   _dePin = dePin;
   _rePin = rePin;
   }

   RS485Class RS485(SERIAL_PORT_HARDWARE, RS485_DEFAULT_TX_PIN, RS845_DEFAULT_DE_PIN, RS845_DEFAULT_RE_PIN);
   */
  /*
   RS485.setSerial(SERIAL_PORT_HARDWARE3);
   //RS485.setPins(CONTROLLINO_RS485_TX, CONTROLLINO_RS485_DE, CONTROLLINO_RS485_nRE);
   RS485.setPins(CONTROLLINO_UART_TX, CONTROLLINO_RS485_DE, CONTROLLINO_RS485_nRE);

   if (!ModbusRTUClient.begin(9600))
   {
   Serial.println("Failed to start Modbus RTU Client!");
   }
   */

  Serial.println("-----------------------------------------");
  Serial.println("Nodecontroller Modbus RTU Master");
  Serial.println("-----------------------------------------");
  Serial.println("");

  // ModbusQuery 0: read input registers
  ModbusQuery[0].u8id = SlaveModbusAdd; // slave address
  ModbusQuery[0].u8fct = 4; // function code (this one is input registers read)
  ModbusQuery[0].u16RegAdd = 0; // start address in slave
  ModbusQuery[0].u16CoilsNo = 3; // number of elements (coils or registers) to read
  ModbusQuery[0].au16reg = &ModbusSlaveRegisters[0]; // pointer to a memory array in the CONTROLLINO

  ControllinoModbusMaster.begin(9600); // baud-rate at 19200
  ControllinoModbusMaster.setTimeOut(5000); // if there is no answer in 5000 ms, roll over

  WaitingTime = millis() + 1000;
  myState = 0;
  currentQuery = 0;

}

/*
 void ModbusRTUClientLoop()
 {
 //ModbusRTUClient.inputRegisterRead(...)
 // send a Holding registers read request to (slave) id 1, for 3 registers
 if(!ModbusRTUClient.requestFrom(1, HOLDING_REGISTERS, 0x00, 3))
 //if(!ModbusRTUClient.requestFrom(1, INPUT_REGISTERS, 0x00, 3))
 {
 //    Serial.print("failed to read registers! ");
 //    Serial.println(ModbusRTUClient.lastError());
 UpdateOutput(DO_ON);
 vTaskDelay( 250 / portTICK_PERIOD_MS );

 UpdateOutput(DO_OFF);
 vTaskDelay( 250 / portTICK_PERIOD_MS );
 }
 else
 {
 // If the request succeeds, the sensor sends the readings, that are
 // stored in the holding registers. The read() method can be used to
 // get the raw temperature and the humidity values.
 short rawtemperature1 = ModbusRTUClient.read();
 short rawtemperature2 = ModbusRTUClient.read();
 short rawhumidity = ModbusRTUClient.read();

 // To get the temperature in Celsius and the humidity reading as
 // a percentage, divide the raw value by 10.0.
 temperature1 = rawtemperature1 / 10.0;
 temparature2 = rawtemperature2 / 10.0;
 humidity = rawhumidity / 10.0;
 //    Serial.println(temperature1);
 //    Serial.println(temparature2);
 //    Serial.println(humidity);



 }
 */

void ModbusRTUClientLoop()
{
  switch(myState)
  {
  case 0:
    if(millis() > WaitingTime)
    {
      myState++; // wait state
    }
    break;
  case 1:
    Serial.print("---- Sending query ");
    Serial.print(currentQuery);
    Serial.println(" -------------");
    ControllinoModbusMaster.query(ModbusQuery[currentQuery]); // send query (only once)
    myState++;
    /*
     currentQuery++;
     if (currentQuery == 2)
     {
     currentQuery = 0;
     }
     */
    break;
  case 2:
    ControllinoModbusMaster.poll(); // check incoming messages
    if(ControllinoModbusMaster.getState() == COM_IDLE)
    {
      // response from the slave was received
      myState = 0;
      WaitingTime = millis() + 1000;

      temperature1 = ((float)ModbusSlaveRegisters[0]) / 10.0;
      temparature2 = ModbusSlaveRegisters[1] / 10.0;
      humidity = ModbusSlaveRegisters[2] / 10.0;

      // debug printout
      // registers read was proceed
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

    }
  }
}

//  UpdateOutput(DO_ON);
//  vTaskDelay( 250 / portTICK_PERIOD_MS );
//
//  UpdateOutput(DO_OFF);
//  vTaskDelay( 250 / portTICK_PERIOD_MS );

//  vTaskDelay(10);
//delay(5000);
//  digitalWrite(LED_BUILTIN, HIGH);
//  vTaskDelay( 250 / portTICK_PERIOD_MS );
//  digitalWrite(LED_BUILTIN, LOW);
//  vTaskDelay( 250 / portTICK_PERIOD_MS );


#endif //#if defined(CONTROLLINO_MODBUS)

#include <ArduinoModbus.h>

extern uint8_t  lcRelaySet[RELAY_COUNT];
extern uint16_t lcOutputSet[DIGITAL_OUT_COUNT];
extern uint16_t lcInputGet[DIGITAL_IN_COUNT + TEMPSENSORDS18B20_MAX];

void ModbusRTUClientSetup()
{
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
}

void ModbusRTUClientLoop()
{
  //int RelayCount = RELAY_COUNT;
  int RelayCount = 10;
  int DICount = 10;
  int DOCount = 10;

  if(!ModbusRTUClient.requestFrom(MODBUS_RTU_SLAVE_ID, INPUT_REGISTERS, 0, DICount))
  {
    // Error
    Serial.print("failed to read input registers! ");
    Serial.println(ModbusRTUClient.lastError());
  }
  else
  {
    for(int i = 0; i < DICount; i++) // for all digital inputs
    {
      uint16_t raw = ModbusRTUClient.read();
      Serial.println("-- READ INPUT REGISTERS RESPONSE RECEIVED --");
      Serial.print("DI (");
      Serial.print(i);
      Serial.print(") = ");
      Serial.print(raw);
      Serial.println("----------------------------------------------");
      Serial.println("");
    }
  }

  if(!ModbusRTUClient.requestFrom(MODBUS_RTU_SLAVE_ID, HOLDING_REGISTERS, 0, DOCount))
  {
    // Error
    Serial.print("failed to read holding registers! ");
    Serial.println(ModbusRTUClient.lastError());
  }
  else
  {
    for(int i = 0; i < DOCount; i++) // for all digital outputs
    {
      uint16_t raw = ModbusRTUClient.read();
      Serial.println("-- READ HOLDING REGISTERS RESPONSE RECEIVED --");
      Serial.print("DO (");
      Serial.print(i);
      Serial.print(") = ");
      Serial.print(raw);
      Serial.println("----------------------------------------------");
      Serial.println("");
    }
  }

  if(!ModbusRTUClient.requestFrom(MODBUS_RTU_SLAVE_ID, COILS, 0, RelayCount))
  {
    // Error
    Serial.print("failed to coils registers! ");
    Serial.println(ModbusRTUClient.lastError());
  }
  else
  {
    for(int i = 0; i < RelayCount; i++) // for all relays
    {
      uint16_t raw = ModbusRTUClient.read();
      Serial.println("-- READ COILS RESPONSE RECEIVED --");
      Serial.print("Relay (");
      Serial.print(i);
      Serial.print(") = ");
      Serial.print(raw);
      Serial.println("----------------------------------------------");
      Serial.println("");
    }
  }
  // One tick delay (15ms) in between reads for stability
  //vTaskDelay(TASK_DELAY_MIN);  // -> 15 ms, remove vTaskDelay if ModbusRTUClientLoop is called directly without TaskModbusRTUClient
}

#if defined(USE_FRERTOS)
void TaskModbusRTUClient(void* pvParameters)
{
  (void) pvParameters; // Not used
  while(true)
  {
    ModbusRTUClientLoop();

//    UpdateOutput(DO_ON);
//    vTaskDelay( 250 / portTICK_PERIOD_MS );
//
//    UpdateOutput(DO_OFF);
//    vTaskDelay( 250 / portTICK_PERIOD_MS );

    // One tick delay (15ms) in between reads for stability
    vTaskDelay(30 * TASK_DELAY_MIN);   // 30 x 15 ms = 450 ms
  }
}
#endif //USE_FRERTOS

#endif //#if defined(USE_MODBUS_RTU_CLIENT)
