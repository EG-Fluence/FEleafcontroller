/*
 * LeafController_WebServer.cpp
 *
 *  Created on: 12.06.2019
 *      Author: Boris.Kajganic
 */


// Test working with (replace with proper IP address if changed):
// curl -i -X GET "http://192.168.0.107"                        -> return HTTP identify
// curl -i -X GET "http://192.168.0.107/all"                    -> return show status of all relays and digital outputs

// curl -i -X GET "http://192.168.0.107/relays"                 -> show status of all relays
// curl -i -X GET "http://192.168.0.107/relays?state=on"        -> show relays with status on
// curl -i -X GET "http://192.168.0.107/relays/3"               -> show status of relay 3
// curl -i -X GET "http://192.168.0.107/relays/44"              -> show (invalid relay)
// curl -i -X PUT "http://192.168.0.107/relays?state=on"        -> switch all relays on
// curl -i -X PUT "http://192.168.0.107/relays?state=off"       -> switch all relays off
// curl -i -X PUT "http://192.168.0.107/relays/3?state=on"      -> switch relay 3 on
// curl -i -X PUT "http://192.168.0.107/relays/3?state=off"     -> switch relay 3 off
// curl -i -X PUT "http://192.168.0.107/relays/3?state=blink"   -> error (invalid value)

// curl -i -X GET "http://192.168.0.107/digitaloutputs"                 -> show status of all digital outputs
// curl -i -X GET "http://192.168.0.107/digitaloutputs?state=on"        -> show digital outputs with status on
// curl -i -X GET "http://192.168.0.107/digitaloutputs/3"               -> show status of digital output 3
// curl -i -X GET "http://192.168.0.107/digitaloutputs/44"              -> show (invalid digital outputs)
// curl -i -X PUT "http://192.168.0.107/digitaloutputs?state=on"        -> switch all digital outputs on
// curl -i -X PUT "http://192.168.0.107/digitaloutputs?state=off"       -> switch all digital outputs off
// curl -i -X PUT "http://192.168.0.107/digitaloutputs/3?state=on"      -> switch digital output 3 on
// curl -i -X PUT "http://192.168.0.107/digitaloutputs/3?state=off"     -> switch digital output 3 off
// curl -i -X PUT "http://192.168.0.107/digitaloutputs/3?state=blink"   -> error (invalid value)


// curl -i -X GET "http://192.168.0.107/digitalinputs"                 -> show status of all digital inputs
// curl -i -X GET "http://192.168.0.107/digitalinputs?state=on"        -> show digital outputs with status on
// curl -i -X GET "http://192.168.0.107/digitalinputs/3"               -> show status of digital output 3
// curl -i -X GET "http://192.168.0.107/digitalinputs/44"              -> show (invalid digital outputs)

// curl -i -X GET "http://192.168.0.107/modbus"                        -> show modbus values

#include "LeafController.h"
#include "LCInputOutput.h"
#include "LCWebServer.h"

#if defined(USE_WEB_SERVER)

SimpleWebServer webServer(WEB_SERVER_PORT);  // Arduino Web Server

/*
LCWebServer::LCWebServer()
{
  // TODO Auto-generated constructor stub

}

LCWebServer::~LCWebServer()
{
  // TODO Auto-generated destructor stub
}
*/

void WebServerSetup()
{
  webServer.begin();  // starting webserver
  webServer.handleOn(HandleSoftReset,         "reset",          HTTP_ANY);
  webServer.handleOn(HandleAll_GET,           "all",            HTTP_GET); // set function for "/all"
  webServer.handleOn(HandleRelay_GET,         "relays",         HTTP_GET); // set function for "/relays"
  webServer.handleOn(HandleRelay_PUT,         "relays",         HTTP_PUT); // set function for "/relays"
  webServer.handleOn(HandleDigitalOutput_GET, "digitaloutputs", HTTP_GET); // set function for "/digitaloutputs"
  webServer.handleOn(HandleDigitalOutput_PUT, "digitaloutputs", HTTP_PUT); // set function for "/digitaloutputs"
  webServer.handleOn(HandleDigitalInput_GET,  "digitalinputs",  HTTP_GET); // set function for "/digitalinputs"

#if defined(USE_MODBUS_RTU_CLIENT)
#if defined(EXAMPLE_MODBUS_RTU_CLIENT_WITH_KUNBUS_REVPI)
  webServer.handleOn(HandleModbus_GET,        "modbus",         HTTP_GET); // set function for "/modbus"
#endif
#endif

  PRINT( F( "# ready for requests"))
  LF;
  PRINT( F( "#"))
  LF;
}

// Only for single thread application
void WebServerLoop()
{
  webServer.handle(); // handle each new request
  yield();

  // One tick delay (15ms) in between reads for stability
#if defined(USE_FRERTOS)
  vTaskDelay(TASK_DELAY_MIN); // -> 15 ms, remove vTaskDelay if WebServerLoop is called directly without TaskWebServer
#endif
}

#if defined(USE_FRERTOS)
void TaskWebServer(void* pvParameters)
{
   (void) pvParameters; // Not used

   while(true)
   {
     WebServerLoop();

     // One tick delay (15ms) in between reads for stability
     vTaskDelay(10);  // -> 150 ms
   }
}
#endif //USE_FRERTOS

 // handle GET on "relay and DO" commands
 void HandleAll_GET()
 {                                             // check on path / args boundaries
 //  if (( server.pathCount() <  1) || ( server.pathCount() > 2)) return;
 //  if (( server.argsCount() <  0) || ( server.argsCount() > 1)) return;
 //  if (( server.pathCount() == 2) && ( server.argsCount() > 0)) return;
 //
   //char reply[2048];
   //char reply[1536];
   char reply[900];
   strClr(reply);                   // reply buffer
 //  const char* index      = server.path( 1);                 // relay index string
 //  uint8_t     relay      = index ? atoi( index) : 0;        // relay index value

 //
 //  state = server.arg( "state", CMD_ON ) ? RELAY_ON : state; // get requested state = on
 //  state = server.arg( "state", CMD_OFF) ? RELAY_OFF: state; // get requested state = off
 //
 //  if ( index) {                                             // if specific relay is speciified
 //    relayPrint( reply, relay, RELAY_ANY);                   // show state for specific relay
 //    server.respond( returnCode = 200, "text/plain", reply); // send OK + reply to client
 //  } else {
 //    relayPrint( reply, state);                              // show relays with specific state
 //    server.respond( returnCode = 200, "text/plain", reply); // respond result to client
 //  }

   //Serial.println("Web HandleAll_GET");

   uint8_t state = RELAY_ANY;             // relay state
   RelayPrint(reply, state);              // show relays with specific state
   state = DO_ANY;
   DigitalOutputPrint(reply, state);      // show DOs with specific state
   state = DI_ANY;
   DigitalInputPrint(reply, state);       // show DIs with specific state

   //Serial.println(reply);
   webServer.respond(returnCode = 200, "text/plain", reply); // respond result to client
 }


 void HandleSoftReset()
 {
   char reply[30];
   strClr(reply);
   strcat(reply, "Reset in 1 second");
   strcat(reply, "\r\n");

   webServer.respond(returnCode = 200, "text/plain", reply); // respond result to client

   SoftReset();
 }

#endif // #if defined(USE_WEB_SERVER)

