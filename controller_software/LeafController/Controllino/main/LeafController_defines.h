/*
 * LeafController_defines.h
 *
 *  Created on: 19.09.2019
 *      Author: Boris.Kajganic
 */

#ifndef LEAFCONTROLLER_DEFINES_H_
#define LEAFCONTROLLER_DEFINES_H_

// !!!! READ CAREFULY
// !!!! if you like to use FreeRTOS version
// !!!! Add library FreeRTOS to the project (Menu -> Arduino > Add Library to selected Project -> FreeRTOS)
// !!!! If you disable back USE_FRERTOS don't forget to remove FreeRTOS from the project.
// !!!! Otherwise you will get very ugly behavior of the project with strange crash.
//#define USE_FRERTOS

//#define SERIAL_BAUDRATE 1200
//#define SERIAL_BAUDRATE 2400
//#define SERIAL_BAUDRATE 4800
//#define SERIAL_BAUDRATE 9600
//#define SERIAL_BAUDRATE 14400
//#define SERIAL_BAUDRATE 19200
//#define SERIAL_BAUDRATE 38400
//#define SERIAL_BAUDRATE 57600
#define SERIAL_BAUDRATE 115200


//#define SERVER_NAME             "Node-01" // host name
#define WEB_SERVER_PORT         80  // host port

#define USE_WEB_SERVER
//#define USE_MODBUS_TCP_SERVER
#define USE_MODBUS_TCP_SERVER_RTU_GATEWAY
//#define USE_MODBUS_RTU_SERVER
//#define USE_MODBUS_RTU_CLIENT

#if defined(USE_MODBUS_TCP_SERVER) && defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
    #error Please, select USE_MODBUS_TCP_SERVER or USE_MODBUS_TCP_SERVER_RTU_GATEWAY
#endif

#if defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY) && defined(USE_MODBUS_RTU_SERVER)
    #error Please, select USE_MODBUS_TCP_SERVER_RTU_GATEWAY or USE_MODBUS_RTU_SERVER
#endif

#if defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY) && defined(USE_MODBUS_RTU_CLIENT)
    #error Please, select USE_MODBUS_TCP_SERVER_RTU_GATEWAY or USE_MODBUS_RTU_CLIENT
#endif

#if defined(USE_MODBUS_RTU_SERVER) && defined(USE_MODBUS_RTU_CLIENT)
    #error Please, select USE_MODBUS_RTU_SERVER or USE_MODBUS_RTU_CLIENT
#endif

#define MODBUS_TCP_SERVER_PORT  502 // host port

#define MODBUS_TCP_SLAVE_ID  42   // Modbus TCP Server ID, the answer to the “ultimate question of life, the universe, and everything,”
#define MODBUS_RTU_SLAVE_ID  24   // Modbus RTU Server ID

//#define MODBUS_RTU_BAUDRATE 1200
//#define MODBUS_RTU_BAUDRATE 2400
//#define MODBUS_RTU_BAUDRATE 4800
//#define MODBUS_RTU_BAUDRATE 9600
//#define MODBUS_RTU_BAUDRATE 14400
#define MODBUS_RTU_BAUDRATE 19200
//#define MODBUS_RTU_BAUDRATE 38400
//#define MODBUS_RTU_BAUDRATE 57600
//#define MODBUS_RTU_BAUDRATE 115200

#define MODBUS_RTU_CONFIG   SERIAL_8N1
#define MODBUS_CLIENT_TIMEOUT  1000    // 1000 ms

// FreeRTOS has problem with realloc function
// allocate max buffer in the beginning of setup() before start the xTaskCreate or loop()
#define MODBUS_RTU_CLIENT_NUMBER_OF_ALLOCATED_VALUES 130 //MODBUS_TCP_MAX_ADU_LENGTH = 260

// Comment to use Temp in F
#define USE_TEMP_IN_C

// Comment to use DHCP
#define USE_FIXIP_ADDRESS

//#define NTPSERVER "europe.pool.ntp.org"
#define NTPSERVER "192.168.0.3"

#if defined(CONTROLLINO_MINI)
//  #define CONTROLLINO_R0 4
  #elif defined(CONTROLLINO_MAXI)
//  #define CONTROLLINO_R0 4

   #if defined(USE_FIXIP_ADDRESS)
     //#define FIXIP_ADDRESS     "192.168.0.111"
     #define FIXIP_ADDRESS    "192.168.2.3" // Controllino connected to Banana Pi
     //#define FIXIP_ADDRESS    "192.168.178.18"  // Lab PIL2
   #endif

  #elif defined(CONTROLLINO_MEGA)
//  #define CONTROLLINO_R0 4
   #if defined(USE_FIXIP_ADDRESS)
     //#define FIXIP_ADDRESS    "192.168.0.107"
     //#define FIXIP_ADDRESS    "192.168.178.107"
     #define FIXIP_ADDRESS    "192.168.2.3" // Controllino connected to Banana Pi
     //#define FIXIP_ADDRESS    "192.168.178.18"  // Lab PIL2
   #endif

/*
#undef MODBUS_RTU_SLAVE_ID
#define MODBUS_RTU_SLAVE_ID  1   // Modbus RTU Server ID
*/

#elif defined(CONTROLLINO_MAXI_AUTOMATION)

#else
    #error Please, select one of the CONTROLLINO variants in Tools->Board
#endif

//#define EXAMPLE_MODBUS_RTU_CLIENT_WITH_KUNBUS_REVPI


#define TASK_DELAY_MIN 1  // one tick delay (15ms) needed for stability

#if (TASK_DELAY_MIN < 1)
    #error Please, select TASK_DELAY_MIN 1 or more
#endif

// Maximal number of temperature sensors DS18B20
#define TEMPSENSORDS18B20_MAX 6

#if (TEMPSENSORDS18B20_MAX < 1)
    #error Please, select TEMPSENSORDS18B20_MAX 1 or more
#endif

#define INTERNALVALUES_MAX 2

//INTERNALVALUES 0: Heartbeat
//INTERNALVALUES 1: User defined

#if (INTERNALVALUES_MAX < 1)
    #error Please, select INTERNALVALUES_MAX 1 or more
#endif

#endif /* LEAFCONTROLLER_DEFINES_H_ */
