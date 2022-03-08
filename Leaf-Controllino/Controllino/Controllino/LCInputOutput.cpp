/*
 * LeafController_IO.cpp
 *
 *  Created on: 12.06.2019
 *      Author: Boris.Kajganic
 */

#include "LeafController.h"
#include "LCInputOutput.h"

#include <OneWire.h>
#include <DallasTemperature.h>

#if defined(USE_MODBUS_TCP_SERVER) || defined(USE_MODBUS_RTU_SERVER)
#include <ArduinoModbus.h>
#elif defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
#include "ModbusTCPServerRTUClient.h"
#endif

#if defined(USE_MODBUS_TCP_SERVER)
  extern ModbusTCPServer modbusTCPServer;
#elif defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
  extern ModbusTCPServerRTUClient modbusTCPServer;
#endif


#if defined(CONTROLLINO_MINI)

    LCIOPin lcInputPin[DIGITAL_IN_COUNT] =
    {              // pin configuration for relay (e.g. relay pin 0 = arduino pin 2)
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_00, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_01, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_02, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_03, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_04, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_05, LC_DI
    };

  #elif defined(CONTROLLINO_MAXI)

    LCIOPin lcInputPin[DIGITAL_IN_COUNT] =
    {              // pin configuration for relay (e.g. relay pin 0 = arduino pin 2)
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_00, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_01, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_02, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_03, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_04, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_05, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_06, LC_AI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_07, LC_AI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_08, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_09, LC_DI
    };
  #elif defined(CONTROLLINO_MEGA)

    LCIOPin lcInputPin[DIGITAL_IN_COUNT] =
    {              // pin configuration for relay (e.g. relay pin 0 = arduino pin 2)
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_00, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_01, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_02, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_03, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_04, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_05, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_06, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_07, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_08, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_09, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_10, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_11, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_12, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_13, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_14, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_15, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_16, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_17, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_18, LC_DI
    };

  #elif defined(CONTROLLINO_MAXI_AUTOMATION)

    LCIOPin lcInputPin[DIGITAL_IN_COUNT] =
    {              // pin configuration for relay (e.g. relay pin 0 = arduino pin 2)
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_00, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_01, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_02, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_03, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_04, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_05, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_06, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_07, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_08, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_09, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_10, LC_DI,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_11, LC_DI
    };
  #else
    #error Please, select one of the CONTROLLINO variants in Tools->Board
#endif

#if defined(CONTROLLINO_MINI)

    LCIOPin lcOutputPin[DIGITAL_OUT_COUNT] =
    {              // pin configuration for relay (e.g. relay pin 0 = arduino pin 2)
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_00, LC_Temp_DS18B20,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_01, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_02, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_03, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_04, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_05, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_06, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_07, LC_DO
    };

  #elif defined(CONTROLLINO_MAXI)

    LCIOPin lcOutputPin[DIGITAL_OUT_COUNT] =
    {              // pin configuration for relay (e.g. relay pin 0 = arduino pin 2)
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_00, LC_Temp_DS18B20,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_01, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_02, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_03, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_04, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_05, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_06, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_07, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_08, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_09, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_10, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_11, LC_DO
    };

  #elif defined(CONTROLLINO_MEGA)

    LCIOPin lcOutputPin[DIGITAL_OUT_COUNT] =
    {              // pin configuration for relay (e.g. relay pin 0 = arduino pin 2)
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_00, LC_Temp_DS18B20,
        //CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_00, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_01, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_02, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_03, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_04, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_05, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_06, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_07, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_08, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_09, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_10, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_11, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_12, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_13, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_14, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_15, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_16, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_17, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_18, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_19, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_20, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_21, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_22, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_23, LC_DO
    };

  #elif defined(CONTROLLINO_MAXI_AUTOMATION)

    LCIOPin lcOutputPin[DIGITAL_OUT_COUNT] =
    {              // pin configuration for relay (e.g. relay pin 0 = arduino pin 2)
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_00, LC_Temp_DS18B20,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_01, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_02, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_03, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_04, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_05, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_06, LC_DO,
        CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_07, LC_DO
    };
#else
  #error Please, select one of the CONTROLLINO variants in Tools->Board
#endif


#if defined(CONTROLLINO_MINI)

    uint8_t lcRelayPin[RELAY_COUNT] =
    {                        // pin configuration for relay (e.g. relay pin 0 = arduino pin 2)
        CONTROLLINO_R0,
        CONTROLLINO_R1,
        CONTROLLINO_R2,
        CONTROLLINO_R3,
        CONTROLLINO_R4,
        CONTROLLINO_R5
    };
  #elif defined(CONTROLLINO_MAXI)
        uint8_t lcRelayPin[RELAY_COUNT] =
        {                        // pin configuration for relay (e.g. relay pin 0 = arduino pin 2)
          CONTROLLINO_R0,
          CONTROLLINO_R1,
          CONTROLLINO_R2,
          CONTROLLINO_R3,
          CONTROLLINO_R4,
          CONTROLLINO_R5,
          CONTROLLINO_R6,
          CONTROLLINO_R7,
          CONTROLLINO_R8,
          CONTROLLINO_R9
        };
  #elif defined(CONTROLLINO_MEGA)

    uint8_t lcRelayPin[RELAY_COUNT] =
    {              // pin configuration for relay (e.g. relay pin 0 = arduino pin 2)
        CONTROLLINO_R0,
        CONTROLLINO_R1,
        CONTROLLINO_R2,
        CONTROLLINO_R3,
        CONTROLLINO_R4,
        CONTROLLINO_R5,
        CONTROLLINO_R6,
        CONTROLLINO_R7,
        CONTROLLINO_R8,
        CONTROLLINO_R9,
        CONTROLLINO_R10,
        CONTROLLINO_R11,
        CONTROLLINO_R12,
        CONTROLLINO_R13,
        CONTROLLINO_R14,
        CONTROLLINO_R15
    };

   #elif defined(CONTROLLINO_MAXI_AUTOMATION)

      uint8_t lcRelayPin[RELAY_COUNT] = {                        // pin configuration for relay (e.g. relay pin 0 = arduino pin 2)
                                        CONTROLLINO_R0,
                                        CONTROLLINO_R1,
                                        CONTROLLINO_R2,
                                        CONTROLLINO_R3,
                                        CONTROLLINO_R4,
                                        CONTROLLINO_R5,
                                        CONTROLLINO_R6,
                                        CONTROLLINO_R7,
                                        CONTROLLINO_R8,
                                        CONTROLLINO_R9
                                      };
      #else
        #error Please, select one of the CONTROLLINO variants in Tools->Board
#endif


uint8_t  lcRelaySet[RELAY_COUNT];                                                   // relay status (values indicated by RELAY_ON / RELAY_OFF)
uint16_t lcOutputSet[DIGITAL_OUT_COUNT];                                            // digital output status (values indicated by DO_ON / DO_OFF)  //!!! uint16_t Modbus Holding Register
uint16_t lcInputGet[DIGITAL_IN_COUNT + TEMPSENSORDS18B20_MAX + INTERNALVALUES_MAX]; // digital input status (values indicated by DI_ON / DI_OFF)  // !!! uint16_t Modbus Input Register

/*
LCIO::LCIO()
{
  // TODO Auto-generated constructor stub

}

LCIO::~LCIO()
{
  // TODO Auto-generated destructor stub
}
*/

#define ONE_WIRE_BUS 2   //Sensor DS18B20 am digitalen Pin 2, Digital 0, Connector X1 pin 12, CONTROLLINO_D0

//OneWire oneWire(ONE_WIRE_BUS); //
OneWire oneWire; //

//�bergabe der OnewWire Referenz zum kommunizieren mit dem Sensor.
DallasTemperature sensors(&oneWire);

int sensorCount = 0;


void IOSetup()
{
  ConfigRelays();
  ConfigOutputs();
  ConfigInputs();
  ConfigTempSensorsDS18B20();
  ConfigInternalValues();
}

void IOLoop()
{
  for(int i = 0; i < DIGITAL_IN_COUNT; i++)
  { // to avoid an immediate switch on:
    // digitalWrite(lcOutputPin[i], LOW);    // 1st: set pin for Digital Output i to low
    //pinMode(lcInputPin[i], INPUT_PULLUP);  // 2nd: set pin for DigitalOutput i as output
    lcInputGet[i] = ReadInput(i);
  }

  UpdateTemperatures();
  UpdateInternalValues();

  //Serial.println("IOLoop()");
}

#if defined(USE_FRERTOS)
void TaskIO(void* pvParameters)
{
  (void) pvParameters; // Not used
  while(true)
  {
    IOLoop();
    // One tick delay (15ms) in between reads for stability
    vTaskDelay(10);
  }
}
#endif //USE_FRERTOS

// initialize pins with relays inactive
void ConfigRelays()
{
  for(int i = 0; i < RELAY_COUNT; i++)
  { // to avoid an immediate switch on:
    digitalWrite(lcRelayPin[i], LOW);        // 1st: set pin for relay i to low
    pinMode(lcRelayPin[i], OUTPUT);          // 2nd: set pin for relay i as output
    lcRelaySet[i] = RELAY_OFF;               // set initial relay state
  }
}

// initialize pins with relays inactive
void ConfigOutputs()
{
  for(int i = 0; i < DIGITAL_OUT_COUNT; i++)
  {
    if(lcOutputPin[i].type == LC_DO)
    {
      // to avoid an immediate switch on:
      digitalWrite(lcOutputPin[i].pin, LOW); // 1st: set pin for Digital Output i to low
      pinMode(lcOutputPin[i].pin, OUTPUT);   // 2nd: set pin for DigitalOutput i as output

      lcOutputSet[i] = DO_OFF;               // set initial Digital Output state
    }
  }
}

// initialize pins with relays inactive
void ConfigInputs()
{
  for(int i = 0; i < DIGITAL_IN_COUNT; i++)
  { // to avoid an immediate switch on:
    // digitalWrite(lcOutputPin[i], LOW); // 1st: set pin for Digital Input i to low
    if(lcInputPin[i].type == LC_DI)
    {
      pinMode(lcInputPin[i].pin, INPUT_PULLUP);   // 2nd: set pin for DigitalInput i as input
    }
    else if(lcInputPin[i].type == LC_AI)
    {
      pinMode(lcInputPin[i].pin, INPUT);   // 2nd: set pin for DigitalInput i as input
    }

    lcInputGet[i] = ReadInput(i);           // set initial Digital Input state
  }
}


void ConfigTempSensorsDS18B20()
{
  oneWire.begin(ONE_WIRE_BUS);
  sensors.begin(); //Starten der Kommunikation mit dem Sensor
  sensorCount = sensors.getDS18Count(); //Lesen der Anzahl der angeschlossenen Temperatursensoren.
  if(sensorCount > TEMPSENSORDS18B20_MAX)
    sensorCount = TEMPSENSORDS18B20_MAX;
}

void ConfigInternalValues()
{
  int i = 0;
  lcInputGet[DIGITAL_IN_COUNT + TEMPSENSORDS18B20_MAX + i++] = 0;
  lcInputGet[DIGITAL_IN_COUNT + TEMPSENSORDS18B20_MAX + i] = 0;
}

// set all relays with value cmd
void UpdateRelays(uint8_t state)
{
  for(int i = 0; i < RELAY_COUNT; i++) // for all relays
  {
    UpdateRelay(i, state); // set state of relay i
  }
}

// set state of relay i
void UpdateRelay(uint8_t i, uint8_t state)
{
  if(/*(i >= 0) && */ (i < RELAY_COUNT)) // check if relay exists
  {
#if defined(USE_MODBUS_TCP_SERVER) || defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
    modbusTCPServer.coilWrite(i, state);
#endif
#if defined(USE_MODBUS_RTU_SERVER)
    ModbusRTUServer.coilWrite(i, state);
#endif
    digitalWrite(lcRelayPin[i], lcRelaySet[i] = state); // set state of relay i
  }
}

// set all (digital) ouputs with value cmd
void UpdateOutputs(uint16_t state, LCType lcType /* = LC_DO */)
{
  for(int i = 0; i < DIGITAL_OUT_COUNT; i++) // for all digital outputs
  {
    UpdateOutput(i, state, lcType); // set state of digital output i
  }
}

// set state of (digital) output i
void UpdateOutput(uint8_t i, uint16_t state, LCType lcType /* = LC_DO */)
{
  if(/*(i >= 0) && */ (i < DIGITAL_OUT_COUNT)) // check if digital output exists
  {
    if(lcOutputPin[i].type == lcType)
	  {
#if defined(USE_MODBUS_TCP_SERVER) || defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
      modbusTCPServer.holdingRegisterWrite(i, state);
#endif
#if defined(USE_MODBUS_RTU_SERVER)
      ModbusRTUServer.holdingRegisterWrite(i, state);
#endif
      digitalWrite(lcOutputPin[i].pin, lcOutputSet[i] = state); // set state of digital output i
	  }
  }
}

// set state of (digital) input i
void UpdateInput(uint8_t i)
{
  if(/*(i >= 0) && */ (i < DIGITAL_IN_COUNT)) // check if digital output exists
  {
#if defined(USE_MODBUS_TCP_SERVER) || defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
    modbusTCPServer.inputRegisterWrite(i, lcInputGet[i]);
#endif
#if defined(USE_MODBUS_RTU_SERVER)
    ModbusRTUServer.inputRegisterWrite(i, lcInputGet[i]);
#endif
  }
}

uint16_t ReadInput(uint8_t i)
{
  if(/*(i >= 0) && */ (i < DIGITAL_IN_COUNT))
  {
    if(lcInputPin[i].type == LC_DI)
    {
      return digitalRead(lcInputPin[i].pin); // set initial Digital Output state
    }
    else if(lcInputPin[i].type == LC_AI)
    {
      int meaVoltage = analogRead(lcInputPin[i].pin);
      return (uint16_t) map(meaVoltage, 0, 1023, 0, 30400);  // because the voltage dividers are dimensioned with some safety reserve
    }
  }
  return 0;
}

// set state of (digital) input i
void UpdateTemperatures()
{
  // TODO add periodically check for sensorCount
  if(sensorCount <= 0)
    return;

  sensors.requestTemperatures();

  for(int i = 0; i < sensorCount; i++)
  {
#if defined(USE_TEMP_IN_C)
    lcInputGet[DIGITAL_IN_COUNT + i] = sensors.getTempCByIndex(i)*10;
#else
    lcInputGet[DIGITAL_IN_COUNT + i] = sensors.getTempFByIndex(i)*10;
#endif

#if defined(USE_MODBUS_TCP_SERVER) || defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
    modbusTCPServer.inputRegisterWrite(DIGITAL_IN_COUNT + i, lcInputGet[DIGITAL_IN_COUNT + i]);
#endif
#if defined(USE_MODBUS_RTU_SERVER)
    ModbusRTUServer.inputRegisterWrite(DIGITAL_IN_COUNT + i, lcInputGet[DIGITAL_IN_COUNT + i]);
#endif
    /*
    Serial.print(i);
    Serial.println(". Temperatur :");
    printValue(sensors.getTempCByIndex(i), "�C");
    printValue(sensors.getTempFByIndex(i), "�F");
    */
  }
}

void UpdateInternalValues()
{
  int i = 0;
  lcInputGet[DIGITAL_IN_COUNT + TEMPSENSORDS18B20_MAX + i]++; //Heartbeat

#if defined(USE_MODBUS_TCP_SERVER) || defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
    modbusTCPServer.inputRegisterWrite(DIGITAL_IN_COUNT + TEMPSENSORDS18B20_MAX  + i, lcInputGet[DIGITAL_IN_COUNT + TEMPSENSORDS18B20_MAX + i]);
#endif
#if defined(USE_MODBUS_RTU_SERVER)
    ModbusRTUServer.inputRegisterWrite(DIGITAL_IN_COUNT + TEMPSENSORDS18B20_MAX + i, lcInputGet[DIGITAL_IN_COUNT + TEMPSENSORDS18B20_MAX + i]);
#endif
}
