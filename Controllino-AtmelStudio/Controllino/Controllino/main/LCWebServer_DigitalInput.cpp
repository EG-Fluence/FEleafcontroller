/*
 * LCWebServer_DigitalOutput.cpp
 *
 *  Created on: 25.06.2019
 *      Author: Boris.Kajganic
 */

#include "LeafController.h"
#include "LCInputOutput.h"
#include "LCWebServer.h"

#if defined(USE_WEB_SERVER)

extern LCIOPin lcInputPin[DIGITAL_IN_COUNT];
extern uint16_t lcInputGet[DIGITAL_IN_COUNT + TEMPSENSORDS18B20_MAX + INTERNALVALUES_MAX];  // digital input status (values indicated by DI_ON / DI_OFF)

/*
// handle GET on "relay" commands
void HandleDigitalInputs_GET()
{                                             // check on path / args boundaries
//  if (( server.pathCount() <  1) || ( server.pathCount() > 2)) return;
//  if (( server.argsCount() <  0) || ( server.argsCount() > 1)) return;
//  if (( server.pathCount() == 2) && ( server.argsCount() > 0)) return;
//
  char reply[1024];
  strClr(reply);                   // reply buffer
//  const char* index      = server.path( 1);                 // relay index string
//  uint8_t     relay      = index ? atoi( index) : 0;        // relay index value
  uint8_t state = DO_ANY;                       // digital output state
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

  DigitalInputPrint(reply, state);                          // show relays with specific state
  webServer.respond(returnCode = 200, "text/plain", reply); // respond result to client
}
*/

// handle GET on "digitalinput" commands
void HandleDigitalInput_GET()
{                                             // check on path / args boundaries
  if((webServer.pathCount() < 1) || (webServer.pathCount() > 2))
    return;
  if((webServer.argsCount() < 0) || (webServer.argsCount() > 1))
    return;
  if((webServer.pathCount() == 2) && (webServer.argsCount() > 0))
    return;

  char reply[500];
  strClr(reply);                   // reply buffer
  const char* index = webServer.path(1);          // DI index string
  uint8_t di_index = index ? atoi(index) : 0;     // DI index value
  uint8_t state = DI_ANY;                         // DI state

  state = webServer.arg("state", CMD_ON) ? DI_ON : state;   // check on state = CMD_ON
  state = webServer.arg("state", CMD_OFF) ? DI_OFF : state; // check on state = CMD_OFF

  if(index) // if specific DI is specified
  {
    DigitalInput2Json(reply, sizeof(reply), di_index, state); // store specific DI in json
    webServer.respond(returnCode = 200, "text/plain", reply); // send OK + reply to client
  }
  else
  {
    DigitalInput2Json(reply, sizeof(reply), state);           // store all DIs in json
    webServer.respond(returnCode = 200, "text/plain", reply); // send OK + reply to client
  }
}

// generate JSON for all relays
void DigitalInput2Json(char* content, size_t size, uint8_t state)
{
  StaticJsonBuffer<650> jsonBuffer;            // create buffer
  JsonArray& root = jsonBuffer.createArray();  // create array

  for(int i = 0; i < DIGITAL_IN_COUNT; i++)
  {                   // for all relays
    if((state == DI_ANY) || (state == lcInputGet[i]))
    {
      JsonObject& item = root.createNestedObject();    // create object
      item["DI"] = i;                                  // write object
      item["state"] = (lcInputGet[i] == DI_ON) ? CMD_ON : CMD_OFF;
    }
  }

  root.printTo(content, size); // write to char buffer
}

// generate JSON for relay item i
void DigitalInput2Json(char* content, size_t size, uint8_t i, uint8_t /* state */)
{
  if(/*(i >= 0) && */ (i < DIGITAL_IN_COUNT)) // check if DI is valid
  {
    StaticJsonBuffer<128> jsonBuffer;                   // create buffer
    JsonArray& root = jsonBuffer.createArray();         // create array
    JsonObject& item = root.createNestedObject();       // create object

    item["DI"] = i;                                     // write object
    item["state"] = (lcInputGet[i] == DI_ON) ? CMD_ON : CMD_OFF;

    root.printTo(content, size);                         // write to char buffer
  }
  else
  {
    strClr(content);                                     // clear char buffer
  }
}

// print digital input state for all digital inputs (write buffer, requested state)
void DigitalInputPrint(char* line, uint8_t state)
{
  for(int i = 0; i < DIGITAL_IN_COUNT; i++) // for all DIs
  {
    if((state == DI_ANY) || (state == lcInputGet[i]))
    {
      DigitalInputPrint(line, i, state);  // write state of DI i
    }
  }
}

// print digital input state for single digital input (write buffer, relay i, requested state)
void DigitalInputPrint(char* line, uint8_t i, uint8_t state)
{
  // check if DI is valid
  if(/* (i >= 0) && */ (i < DIGITAL_IN_COUNT) && ((state == DI_ANY) || (state == lcInputGet[i])))
  {  // check if state is valid
    strcat(line, "DI ");
    strcat(line, dec(i, 2));  // write state of DI i
//    strcat(line, " on pin ");
//    strcat(line, dec(lcInputGet[i], 2));
    strcat(line, " = ");
    strcat(line, lcInputGet[i] == DI_ON ? CMD_ON : CMD_OFF);
    strcat(line, "\r\n");
  }
  else
  {
    strcat(line, "DI ");
    strcat(line, dec(i, 2));
    strcat(line, " undef");
    strcat(line, "\r\n");   // undefined digital input
  }
}

#endif // #if defined(USE_WEB_SERVER)
