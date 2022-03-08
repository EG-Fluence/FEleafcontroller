/*
 * NTPClientEx.cpp
 *
 *  Created on: 19.09.2019
 *      Author: Boris.Kajganic
 */

#include "NTPClientEx.h"
#include <time.h>

/*
NTPClientEx::NTPClientEx()
{
  // TODO Auto-generated constructor stub

}

NTPClientEx::~NTPClientEx()
{
  // TODO Auto-generated destructor stub
}
*/


int NTPClientEx::getYear() {
  time_t rawtime = this->getEpochTime();
  struct tm * ti;
  ti = localtime (&rawtime);
  int year = ti->tm_year + 1900;

  return year;
}

int NTPClientEx::getMonth() {
  time_t rawtime = this->getEpochTime();
  struct tm * ti;
  ti = localtime (&rawtime);
  //int month = (ti->tm_mon + 1) < 10 ? 0 + (ti->tm_mon + 1) : (ti->tm_mon + 1);
  int month = ti->tm_mon + 1;

  return month;
}

int NTPClientEx::getDate() {
  time_t rawtime = this->getEpochTime();
  struct tm * ti;
  ti = localtime (&rawtime);
  //int day = (ti->tm_mday) < 10 ? 0 + (ti->tm_mday) : (ti->tm_mday);
  int day = ti->tm_mday;

  return day;
}

String NTPClientEx::getFormattedDateTime() {
   time_t rawtime = this->getEpochTime();
   struct tm * ti;
   ti = localtime (&rawtime);

   uint16_t year = ti->tm_year + 1900;
   String yearStr = String(year);

   uint8_t month = ti->tm_mon + 1;
   String monthStr = month < 10 ? "0" + String(month) : String(month);

   uint8_t day = ti->tm_mday;
   String dayStr = day < 10 ? "0" + String(day) : String(day);

   uint8_t hours = ti->tm_hour;
   String hoursStr = hours < 10 ? "0" + String(hours) : String(hours);

   uint8_t minutes = ti->tm_min;
   String minuteStr = minutes < 10 ? "0" + String(minutes) : String(minutes);

   uint8_t seconds = ti->tm_sec;
   String secondStr = seconds < 10 ? "0" + String(seconds) : String(seconds);

#ifdef YEAR_FIRST
   return yearStr + "-" + monthStr + "-" + dayStr + " " + hoursStr + ":" + minuteStr + ":" + secondStr;
#else
   return dayStr + "." + monthStr + "." + yearStr + " " + hoursStr + ":" + minuteStr + ":" + secondStr;
#endif
}

String NTPClientEx::getFormattedDate()
{
  time_t rawtime = this->getEpochTime();
   struct tm * ti;
   ti = localtime (&rawtime);

   uint16_t year = ti->tm_year + 1900;
   String yearStr = String(year);

   uint8_t month = ti->tm_mon + 1;
   String monthStr = month < 10 ? "0" + String(month) : String(month);

   uint8_t day = ti->tm_mday;
   String dayStr = day < 10 ? "0" + String(day) : String(day);

#ifdef YEAR_FIRST
   return yearStr + "-" + monthStr + "-" + dayStr;
#else
   return dayStr + "." + monthStr + "." + yearStr;
#endif
}
