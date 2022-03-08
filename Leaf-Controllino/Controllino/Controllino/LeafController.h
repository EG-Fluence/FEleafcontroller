#ifndef _LEAFCONTROLLER_H_
#define _LEAFCONTROLLER_H_

//#include <Arduino.h>
#include "Controllino.h"

#include "LeafController_defines.h"

#if defined(USE_FRERTOS)

#include <Arduino_FreeRTOS.h>

void TaskWebServer(void* pvParameters);
void TaskModbusTCPServer(void* pvParameters);
void TaskModbusRTUServer(void* pvParameters);
void TaskModbusRTUClient(void* pvParameters);
void TaskIO(void* pvParameters);

#endif

#include <avr/wdt.h>

#include "SimpleWebServer.h"
#include "SimpleUtils.h"
#include "SimplePrint.h"
#include "ArduinoJson.h"

#if defined(CONTROLLINO_MINI)
        #define CONTROLLINO_R0 4
        #define CONTROLLINO_R1 5
        #define CONTROLLINO_R2 6
        #define CONTROLLINO_R3 7
        #define CONTROLLINO_R4 8
        #define CONTROLLINO_R5 9

        #define ANALOG_IN_COUNT    6                             // number of available analog inputs
        #define DIGITAL_IN_COUNT   6                             // number of available digital inputs
        #define DIGITAL_OUT_COUNT  8                             // number of available digital outputs
        #define RELAY_COUNT        6                             // number of available relays
  #elif defined(CONTROLLINO_MAXI)
        #define ANALOG_IN_COUNT    10                            // number of available analog inputs
        #define DIGITAL_IN_COUNT   10                            // number of available digital inputs
        #define DIGITAL_OUT_COUNT  12                            // number of available digital outputs
        #define RELAY_COUNT        10                            // number of available relays
    #elif defined(CONTROLLINO_MEGA)

//#define CONTROLLINO_PIN_HEADER_PWM_12 42
#define CONTROLLINO_PIN_HEADER_DIGITAL_OUT_12 42
#define CONTROLLINO_SCREW_TERMINAL_PWM_12 42
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_12 42

//#define CONTROLLINO_PIN_HEADER_PWM_13 43
#define CONTROLLINO_PIN_HEADER_DIGITAL_OUT_13 43
#define CONTROLLINO_SCREW_TERMINAL_PWM_13 43
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_13 43

#define CONTROLLINO_PIN_HEADER_PWM_14 44
#define CONTROLLINO_PIN_HEADER_DIGITAL_OUT_14 44
#define CONTROLLINO_SCREW_TERMINAL_PWM_14 44
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_14 44

#define CONTROLLINO_PIN_HEADER_PWM_15 45
#define CONTROLLINO_PIN_HEADER_DIGITAL_OUT_15 45
#define CONTROLLINO_SCREW_TERMINAL_PWM_15 45
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_15 45

#define CONTROLLINO_PIN_HEADER_PWM_16 46
#define CONTROLLINO_PIN_HEADER_DIGITAL_OUT_16 46
#define CONTROLLINO_SCREW_TERMINAL_PWM_16 46
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_16 46

#define CONTROLLINO_PIN_HEADER_PWM_17 47
#define CONTROLLINO_PIN_HEADER_DIGITAL_OUT_17 47
#define CONTROLLINO_SCREW_TERMINAL_PWM_17 47
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_17 47

#define CONTROLLINO_PIN_HEADER_PWM_18 48
#define CONTROLLINO_PIN_HEADER_DIGITAL_OUT_18 48
#define CONTROLLINO_SCREW_TERMINAL_PWM_18 48
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_18 48

#define CONTROLLINO_PIN_HEADER_PWM_19 49
#define CONTROLLINO_PIN_HEADER_DIGITAL_OUT_19 49
#define CONTROLLINO_SCREW_TERMINAL_PWM_19 49
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_19 49

#define CONTROLLINO_PIN_HEADER_PWM_20 77
#define CONTROLLINO_PIN_HEADER_DIGITAL_OUT_20 77
#define CONTROLLINO_SCREW_TERMINAL_PWM_20 77
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_20 77

#define CONTROLLINO_PIN_HEADER_PWM_21 78
#define CONTROLLINO_PIN_HEADER_DIGITAL_OUT_21 78
#define CONTROLLINO_SCREW_TERMINAL_PWM_21 78
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_21 78

#define CONTROLLINO_PIN_HEADER_PWM_22 79
#define CONTROLLINO_PIN_HEADER_DIGITAL_OUT_22 79
#define CONTROLLINO_SCREW_TERMINAL_PWM_22 79
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_22 79

#define CONTROLLINO_PIN_HEADER_PWM_23 80
#define CONTROLLINO_PIN_HEADER_DIGITAL_OUT_23 80
#define CONTROLLINO_SCREW_TERMINAL_PWM_23 80
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_OUT_23 80

//#define CONTROLLINO_PIN_HEADER_ANALOG_ADC_IN_10 64
//#define CONTROLLINO_PIN_HEADER_DIGITAL_ADC_IN_10 64
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_ADC_IN_10 64
#define CONTROLLINO_SCREW_TERMINAL_ANALOG_ADC_IN_10 64
//#define CONTROLLINO_PIN_HEADER_DIGITAL_IN_10 64
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_10 64

//#define CONTROLLINO_PIN_HEADER_ANALOG_ADC_IN_11 65
//#define CONTROLLINO_PIN_HEADER_DIGITAL_ADC_IN_11 65
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_ADC_IN_11 65
#define CONTROLLINO_SCREW_TERMINAL_ANALOG_ADC_IN_11 65
//#define CONTROLLINO_PIN_HEADER_DIGITAL_IN_11 65
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_11 65

//#define CONTROLLINO_PIN_HEADER_ANALOG_ADC_IN_12 66
//#define CONTROLLINO_PIN_HEADER_DIGITAL_ADC_IN_12 66
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_ADC_IN_12 66
#define CONTROLLINO_SCREW_TERMINAL_ANALOG_ADC_IN_12 66
//#define CONTROLLINO_PIN_HEADER_DIGITAL_IN_12 66
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_12 66

//#define CONTROLLINO_PIN_HEADER_ANALOG_ADC_IN_13 67
//#define CONTROLLINO_PIN_HEADER_DIGITAL_ADC_IN_13 67
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_ADC_IN_13 67
#define CONTROLLINO_SCREW_TERMINAL_ANALOG_ADC_IN_13 67
//#define CONTROLLINO_PIN_HEADER_DIGITAL_IN_13 67
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_13 67

#define CONTROLLINO_PIN_HEADER_ANALOG_ADC_IN_14 68
#define CONTROLLINO_PIN_HEADER_DIGITAL_ADC_IN_14 68
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_ADC_IN_14 68
#define CONTROLLINO_SCREW_TERMINAL_ANALOG_ADC_IN_14 68
#define CONTROLLINO_PIN_HEADER_DIGITAL_IN_14 68
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_14 68

#define CONTROLLINO_PIN_HEADER_ANALOG_ADC_IN_15 69
#define CONTROLLINO_PIN_HEADER_DIGITAL_ADC_IN_15 69
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_ADC_IN_15 69
#define CONTROLLINO_SCREW_TERMINAL_ANALOG_ADC_IN_15 69
#define CONTROLLINO_PIN_HEADER_DIGITAL_IN_15 69
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_15 69

#define CONTROLLINO_PIN_HEADER_DIGITAL_ADC_IN_16 38
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_ADC_IN_16 38
//#define CONTROLLINO_PIN_HEADER_DIGITAL_IN_16 38
//#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_16 38

#define CONTROLLINO_PIN_HEADER_DIGITAL_ADC_IN_17 39
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_ADC_IN_17 39
//#define CONTROLLINO_PIN_HEADER_DIGITAL_IN_17 39
//#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_17 39

#define CONTROLLINO_PIN_HEADER_DIGITAL_ADC_IN_18 40
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_ADC_IN_18 40
#define CONTROLLINO_PIN_HEADER_DIGITAL_IN_18 40
#define CONTROLLINO_SCREW_TERMINAL_DIGITAL_IN_18 40


        #define ANALOG_IN_COUNT    16                            // number of available analog inputs
        #define DIGITAL_IN_COUNT   19                            // number of available digital inputs
        #define DIGITAL_OUT_COUNT  24                            // number of available digital outputs
        #define RELAY_COUNT        16                            // number of available relays

#elif defined(CONTROLLINO_MAXI_AUTOMATION)
        #define ANALOG_IN_COUNT    12                            // number of available analog inputs
        #define DIGITAL_IN_COUNT   12                            // number of available digital inputs
        #define DIGITAL_OUT_COUNT   8                            // number of available digital outputs
        #define RELAY_COUNT        10                            // number of available relays
      #else
        #error Please, select one of the CONTROLLINO variants in Tools->Board
#endif

#define RELAY_OFF    0                                      // status for relay = off
#define RELAY_ON     1                                      // status for relay = on
#define RELAY_ANY    2

#define DO_OFF    0                                      // status for digital output = off
#define DO_ON     1                                      // status for digital output = on
#define DO_ANY    2

#define DI_OFF    0                                      // status for digital input = off
#define DI_ON     1                                      // status for digital input = on
#define DI_ANY    2

#define CMD_ON  "on"                                        // command to switch relay/digital output on
#define CMD_OFF "off"                                       // command to switch relay/digital output off


void SoftReset(void);

void EthernetSetup(void);
void EthernetLoop(void);

void SerialSetup(void);
void SerialLoop(void);

void WebServerSetup(void);
void WebServerLoop(void);

void ModbusTCPServerSetup(void);
void ModbusTCPServerLoop(void);

void IOSetup();
void IOLoop();

void ModbusTCPServerSetup();
void ModbusTCPServerLoop();

void ModbusRTUServerSetup();
void ModbusRTUServerLoop();

void ModbusRTUClientSetup();
void ModbusRTUClientLoop();

void setup();
void loop();

#endif
