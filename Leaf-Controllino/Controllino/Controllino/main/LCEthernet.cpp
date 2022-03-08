/*
 * LCEthernet.cpp
 *
 *  Created on: 12.06.2019
 *      Author: Boris.Kajganic
 */


#include "LCEthernet.h"
#include "LeafController.h"
#include <EEPROM.h>
#include <TrueRandom.h>

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
//#include <ArduinoModbus.h>


//#define SERVER_NAME "Node-01" // host name

byte mac[] =
{ 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };

/*
byte server_ip4[] =
{ 192, 168, 0, 107 };     // lan ip (e.g. "192.168.0.107")
byte server_gateway[] =
{ 192, 168, 0, 1 };       // router gateway
byte server_subnet[] =
{ 255, 255, 255, 0 };     // subnet mask
*/

/*
LCEthernet::LCEthernet()
{
  // TODO Auto-generated constructor stub

}

LCEthernet::~LCEthernet()
{
  // TODO Auto-generated destructor stub
}
*/

void EthernetSetup(void)
{
#if defined(CONTROLLINO_MAXI)
  if(EEPROM.read(0) == '#')
  {
    //Serial.println("MAC from EEPROM");
    for (int i = 0; i < 6; i++)
    {
      mac[i] = EEPROM.read(i+1);
    }
  }
  else
  {
    byte locmac[6];
    TrueRandom.mac(locmac);

    //Serial.println("MAC to EEPROM");
    //leave first two bites unchanged
    for (int i = 0; i < 2; i++)
    {
      EEPROM.write(i+1, mac[i]);
    }
    for (int i = 2; i < 6; i++)
    {
      EEPROM.write(i+1, locmac[i]);
      mac[i] = locmac[i];
    }
    //Serial.println("MAC set");
    EEPROM.write(0, '#');
  }
#elif defined(CONTROLLINO_MEGA)
  mac[5] += 1;
#elif defined(CONTROLLINO_MAXI_AUTOMATION)
  mac[5] += 2;
#else
  #error Please, select one of the CONTROLLINO variants with Ethernet in Tools->Board
#endif

#if defined(ESP8266)                                        // ESP8266 = connect via WiFi
  WiFi.hostname( SERVER_NAME);                              // set host name
  WiFi.config( server_ip4, server_gateway, server_subnet);  // set fixed IP address
  WiFi.begin( ssid, pass);                                  // open WiFi connection
  while ( WiFi.status() != WL_CONNECTED) delay(500);        // wait for  connection

  LABEL( F( "# connected to "), ssid);
  LABEL( F( " / IP ="), WiFi.localIP()) LF;
#else                                                       // Arduino = connect via Ethernet
//Ethernet.hostName( SERVER_NAME);                          // not supported (yet)
//  ETHERNET_RESET(11U);                                     // Leonardo ETH reset

#if defined(USE_FIXIP_ADDRESS)
  //Ethernet.begin(server_mac, server_ip4, server_gateway, server_subnet);
  IPAddress ip;
  if(ip.fromString(FIXIP_ADDRESS))
    Ethernet.begin(mac, ip);
  else
  {
    Serial.print("Wrong IP Address : ");
    Serial.println(FIXIP_ADDRESS);
    while(true)
    {
      delay(1); // do nothing, no point running with wrong IP address
    }
  }
#else
  Ethernet.begin(mac);
#endif

  // Check for Ethernet hardware present
  if(Ethernet.hardwareStatus() == EthernetNoHardware)
  {
    Serial.println("Ethernet shield was not found.  Sorry, can't run without hardware. :(");
    while(true)
    {
      delay(1); // do nothing, no point running without Ethernet hardware
    }
  }
  if(Ethernet.linkStatus() == LinkOFF)
  {
    Serial.println("Ethernet cable is not connected.");
  }

  // open Ethernet connection
  LABEL(F( "# connected to "), Ethernet.localIP()) LF;
#endif
}

/*
void EthernetLoop(void)
{
    vTaskDelay(TASK_DELAY_MIN);
}

void TaskEthernet(void* pvParameters)
{
  (void) pvParameters; // Not used

  while(true)
  {
    EthernetLoop(void);
    vTaskDelay(20);
  }
}
*/
