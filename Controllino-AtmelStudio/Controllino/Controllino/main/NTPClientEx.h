/*
 * NTPClientEx.h
 *
 *  Created on: 19.09.2019
 *      Author: Boris.Kajganic
 */

#ifndef NTPCLIENTEX_H_
#define NTPCLIENTEX_H_

#include <NTPClient.h>

//#define YEAR_FIRST

class NTPClientEx: public NTPClient
{
public:
  //NTPClientEx();
  //virtual ~NTPClientEx();
  NTPClientEx(UDP& udp) : NTPClient(udp) {};
  NTPClientEx(UDP& udp, long timeOffset) : NTPClient(udp, timeOffset) {};
  NTPClientEx(UDP& udp, const char* poolServerName) : NTPClient(udp, poolServerName) {};
  NTPClientEx(UDP& udp, const char* poolServerName, long timeOffset) : NTPClient(udp, poolServerName, timeOffset) {};
  NTPClientEx(UDP& udp, const char* poolServerName, long timeOffset, unsigned long updateInterval) : NTPClient(udp, poolServerName, timeOffset, updateInterval) {};

  int getYear();
  int getMonth();
  int getDate();

  /**
  * @return time formatted like `DD.MM.YYYY`
  */
  String getFormattedDate();

  /**
  * @return time formatted like `DD.MM.YYYY hh:mm:ss`
  */
  String getFormattedDateTime();

protected:

};

#endif /* NTPCLIENTEX_H_ */
