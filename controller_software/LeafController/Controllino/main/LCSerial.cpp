/*
 * LCSerial.cpp
 *
 *  Created on: 12.06.2019
 *      Author: Boris.Kajganic
 */
#include "LCSerial.h"
#include "LeafController.h"


/*
LCSerial::LCSerial()
{
  // TODO Auto-generated constructor stub

}

LCSerial::~LCSerial()
{
  // TODO Auto-generated destructor stub
}
*/

void SerialSetup(void)
{
  // Init Arduino serial
  Serial.begin(SERIAL_BAUDRATE);

  // Wait for serial port to connect. Needed for native USB, on LEONARDO, MICRO, YUN, and other 32u4 based boards.
//  while(!Serial)
//  {
//    vTaskDelay(1);
//  }
}

/**
 * Serial task.
 * Prints the received items from the queue to the serial monitor.
 */
