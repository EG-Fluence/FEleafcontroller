/*
 * SoftReset.cpp
 *
 *  Created on: 25.06.2019
 *      Author: Boris.Kajganic
 */

#include "LeafController.h"


void SoftReset()
{
  wdt_enable(WDTO_15MS);
  //wdt_enable(WDTO_1S);
  while(true)
  {
#if defined(USE_FRERTOS)
    vTaskDelay(TASK_DELAY_MIN);
#else
    delay(100);
#endif
  }
}

