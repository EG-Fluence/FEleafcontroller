#include "LeafController.h"
/*
 * Example of a basic FreeRTOS queue
 * https://www.freertos.org/Embedded-RTOS-Queues.html
 */

/*
 * Declaring a global variable of type QueueHandle_t
 *
 */
//QueueHandle_t structQueue;

void setup()
{
  //wdt_disable(); // disable all Watchdogs

  SerialSetup();
  IOSetup();

#if defined(USE_WEB_SERVER) || defined(USE_MODBUS_TCP_SERVER) || defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
  EthernetSetup();
  if(Ethernet.hardwareStatus() != EthernetNoHardware)
  {
#if defined(USE_WEB_SERVER)
    WebServerSetup();
#endif
#if defined(USE_MODBUS_TCP_SERVER) || defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
    ModbusTCPServerSetup();
#endif
  }
#endif // #if defined(USE_WEB_SERVER) || defined(USE_MODBUS_TCP_SERVER) || defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)

#if defined(USE_MODBUS_RTU_SERVER)
  ModbusRTUServerSetup();
#endif

#if defined(USE_MODBUS_RTU_CLIENT)
  ModbusRTUClientSetup();
#endif

#if defined(USE_FRERTOS)

  // Priority, with 3 (configMAX_PRIORITIES - 1) being the highest, and 0 being the lowest.
  if(Ethernet.hardwareStatus() != EthernetNoHardware)
  {
#if defined(USE_WEB_SERVER)
    xTaskCreate(TaskWebServer, // Task function
        "Web", // Task name
        //configMINIMAL_STACK_SIZE*12, // Stack size
        //1700, // Stack size
        3000,
        NULL,
        3, // Priority the highest, it must be only one with priority 3
        NULL);
#endif



#if defined(USE_MODBUS_TCP_SERVER) || defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
    xTaskCreate(TaskModbusTCPServer, // Task function
        "MTCPS", // Task name
#if defined(USE_MODBUS_TCP_SERVER)
        1000, // Stack size
#else
        1300,
#endif
        NULL,
        2, // Priority
        NULL);
#endif
  }

#if defined(USE_MODBUS_RTU_SERVER)
  //  Moved to loop()
    xTaskCreate(TaskModbusRTUServer, // Task function
        "MRTUS", // Task name
        800, // Stack size
        NULL,
        2, // Priority
        NULL);
#endif


#if defined(USE_MODBUS_RTU_CLIENT)
    //  Moved to loop()
  xTaskCreate(TaskModbusRTUClient, // Task function
      "MRTUC", // Task name
      2 * configMINIMAL_STACK_SIZE, // Stack size
      //4500, // Stack size
      NULL,
      1, // Priority
      NULL);
#endif

// Moved to loop()
//  xTaskCreate(TaskIO, // Task function
//      "IO", // Task name
//      configMINIMAL_STACK_SIZE, // Stack size
//      NULL,
//      1, // Priority
//      NULL);

#endif

  //wdt_enable(WDTO_8S);  // Set Watchdog to 8 second
}

void loop()
{
#if !defined(USE_FRERTOS)

#if defined(USE_MODBUS_TCP_SERVER) || defined(USE_MODBUS_TCP_SERVER_RTU_GATEWAY)
  ModbusTCPServerLoop();
#endif

#if defined(USE_WEB_SERVER)
  WebServerLoop();
#endif //USE_WEB_SERVER

#endif //!USE_FRERTOS

  IOLoop();
#if defined(USE_MODBUS_RTU_CLIENT)
  //ModbusRTUClientLoop();
#endif

#if defined(USE_MODBUS_RTU_SERVER)
  //ModbusRTUServerLoop();
#endif

  //wdt_reset();
}

