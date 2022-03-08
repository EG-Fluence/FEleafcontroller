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

extern uint8_t  lcOutputPin[DIGITAL_OUT_COUNT];
extern uint16_t lcOutputSet[DIGITAL_OUT_COUNT];  // relay status (values indicated by DO_ON / DO_OFF)

/*
// handle GET on "relay" commands
void HandleDigitalOutputs_GET()
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
//  state = server.arg( "state", CMD_ON ) ? DO_ON : state; // get requested state = on
//  state = server.arg( "state", CMD_OFF) ? DO_OFF: state; // get requested state = off
//
//  if ( index) {                                             // if specific relay is speciified
//    relayPrint( reply, relay, RELAY_ANY);                   // show state for specific relay
//    server.respond( returnCode = 200, "text/plain", reply); // send OK + reply to client
//  } else {
//    relayPrint( reply, state);                              // show relays with specific state
//    server.respond( returnCode = 200, "text/plain", reply); // respond result to client
//  }

  DigitalOutputPrint(reply, state);                   // show relays with specific state
  webServer.respond(returnCode = 200, "text/plain", reply); // respond result to client
}
*/

// handle GET on "relay" commands
void HandleDigitalOutput_GET()
{                                             // check on path / args boundaries
  if((webServer.pathCount() < 1) || (webServer.pathCount() > 2))
    return;
  if((webServer.argsCount() < 0) || (webServer.argsCount() > 1))
    return;
  if((webServer.pathCount() == 2) && (webServer.argsCount() > 0))
    return;

  char reply[600];
  strClr(reply);                   // reply buffer
  const char* index = webServer.path(1);        // DO index string
  uint8_t do_index = index ? atoi(index) : 0;   // DO index value
  uint8_t state = DO_ANY;                       // relay state

  state = webServer.arg("state", CMD_ON) ? DO_ON : state;   // check on state = CMD_ON
  state = webServer.arg("state", CMD_OFF) ? DO_OFF : state; // check on state = CMD_OFF

  if(index) // if specific DO is specified
  {
    DigitalOutput2Json(reply, sizeof(reply), do_index, state); // store specific DO in json
    webServer.respond(returnCode = 200, "text/plain", reply); // send OK + reply to client
  }
  else
  {
    DigitalOutput2Json(reply, sizeof(reply), state);          // store all DOs in json
    webServer.respond(returnCode = 200, "text/plain", reply); // send OK + reply to client
  }
}

// handle PUT on "relay" commands
void HandleDigitalOutput_PUT()
{                                             // check on path / args boundaries
  if((webServer.pathCount() < 1) || (webServer.pathCount() > 2))
    return;
  if((webServer.argsCount() < 1) || (webServer.argsCount() > 1))
    return;

  const char* index = webServer.path(1);             // DO index string
  uint8_t do_index = index ? atoi(index) : 0;        // DO index value
  uint8_t state = DO_ANY;                            // DO state

  state = webServer.arg("state", CMD_ON) ? DO_ON : state; // get requested state = on
  state = webServer.arg("state", CMD_OFF) ? DO_OFF : state; // get requested state = off

  if(index)
  {                                           // if specific digital output is specified
    UpdateOutput(do_index, state);            // set state for specific digital output
    webServer.respond(returnCode = 200);      // send OK to client
  }
  else
  {
    UpdateOutputs(state);                   // set state for all DOs
    webServer.respond(returnCode = 200);    // send OK to client
  }
}

// generate JSON for all relays
void DigitalOutput2Json(char* content, size_t size, uint8_t state)
{
  StaticJsonBuffer<800> jsonBuffer;                     // create buffer
  JsonArray& root = jsonBuffer.createArray();           // create array

  for(int i = 0; i < DIGITAL_OUT_COUNT; i++)  // for all DOs
  {
    if((state == DO_ANY) || (state == lcOutputSet[i]))
    {
      JsonObject& item = root.createNestedObject();     // create object
      item["DO"] = i;                                   // write object
      item["state"] = (lcOutputSet[i] == DO_ON) ? CMD_ON : CMD_OFF;
    }
  }

  root.printTo(content, size);                           // write to char buffer
}

// generate JSON for relay item i
void DigitalOutput2Json(char* content, size_t size, uint8_t i, uint8_t /* state */)
{
  if(/*(i >= 0) && */ (i < DIGITAL_OUT_COUNT)) // check if DO is valid
  {
    StaticJsonBuffer<128> jsonBuffer;                   // create buffer
    JsonArray& root = jsonBuffer.createArray();         // create array
    JsonObject& item = root.createNestedObject();       // create object

    item["DO"] = i;                                     // write object
    item["state"] = (lcOutputSet[i] == DO_ON) ? CMD_ON : CMD_OFF;

    root.printTo(content, size);                         // write to char buffer
  }
  else
  {
    strClr(content);                                       // clear char buffer
  }
}

// print relay state for all relays (write buffer, requested state)
void DigitalOutputPrint(char* line, uint8_t state)
{
  //Serial.println("Web DO_Print Start");
  for(int i = 0; i < DIGITAL_OUT_COUNT; i++) // for all DOs
  {
    if((state == DO_ANY) || (state == lcOutputSet[i]))
    {
      //Serial.println("Web DO_Print");
      DigitalOutputPrint(line, i, state); // write state of DO i
    }
  }
  //Serial.println("Web DO_Print End");
}

// print relay state for single relay (write buffer, relay i, requested state)
void DigitalOutputPrint(char* line, uint8_t i, uint8_t state)
{
  // check if DO is valid
  if(/* (i >= 0) && */ (i < DIGITAL_OUT_COUNT) && ((state == DO_ANY) || (state == lcOutputSet[i])))
  {  // check if state is valid
    strcat(line, "DO ");
    strcat(line, dec(i, 2));  // write state of DO i
//    strcat(line, " on pin ");
//    strcat(line, dec(lcOutputPin[i], 2));
    strcat(line, " = ");
    strcat(line, lcOutputSet[i] == DO_ON ? CMD_ON : CMD_OFF);
    strcat(line, "\r\n");
  }
  else
  {
    strcat(line, "DO ");
    strcat(line, dec(i, 2));
    strcat(line, " undef");
    strcat(line, "\r\n");   // undefined relay
  }
}

#endif //#if defined(USE_WEB_SERVER)
