/*
 * LCWebServer_Relay.cpp
 *
 *  Created on: 27.06.2019
 *      Author: Boris.Kajganic
 */

#include "LeafController.h"
#include "LCInputOutput.h"
#include "LCWebServer.h"

#if defined(USE_WEB_SERVER)

extern uint8_t lcRelayPin[RELAY_COUNT];
extern uint8_t lcRelaySet[RELAY_COUNT]; // relay status (values indicated by RELAY_ON / RELAY_OFF)


/*
// handle GET on "relay" commands
void HandleRelays_GET()
{                                             // check on path / args boundaries
//  if (( server.pathCount() <  1) || ( server.pathCount() > 2)) return;
//  if (( server.argsCount() <  0) || ( server.argsCount() > 1)) return;
//  if (( server.pathCount() == 2) && ( server.argsCount() > 0)) return;
//
  char reply[1024];
  strClr(reply);                   // reply buffer
//  const char* index      = server.path( 1);                 // relay index string
//  uint8_t     relay      = index ? atoi( index) : 0;        // relay index value
  uint8_t state = RELAY_ANY;                       // relay state
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

  RelayPrint(reply, state);                   // show relays with specific state
  webServer.respond(returnCode = 200, "text/plain", reply); // respond result to client
}
*/

// handle GET on "relay" commands
void HandleRelay_GET()
{                                             // check on path / args boundaries
  if((webServer.pathCount() < 1) || (webServer.pathCount() > 2))
    return;
  if((webServer.argsCount() < 0) || (webServer.argsCount() > 1))
    return;
  if((webServer.pathCount() == 2) && (webServer.argsCount() > 0))
    return;

  char reply[450];
  strClr(reply);                   // reply buffer
  const char* index = webServer.path(1);                 // relay index string
  uint8_t relay = index ? atoi(index) : 0;        // relay index value
  uint8_t state = RELAY_ANY;                       // relay state

  state = webServer.arg("state", CMD_ON) ? RELAY_ON : state; // check on state = CMD_ON
  state = webServer.arg("state", CMD_OFF) ? RELAY_OFF : state; // check on state = CMD_OFF

  if(index)
  {                                           // if specific relay is speciified
    Relay2Json(reply, sizeof(reply), relay, state); // store specific relay in json
    webServer.respond(returnCode = 200, "text/plain", reply); // send OK + reply to client
  }
  else
  {
    Relay2Json(reply, sizeof(reply), state);       // store all relays in json
    webServer.respond(returnCode = 200, "text/plain", reply); // send OK + reply to client
  }
}

// handle PUT on "relay" commands
void HandleRelay_PUT()
{                                             // check on path / args boundaries
  if((webServer.pathCount() < 1) || (webServer.pathCount() > 2))
    return;
  if((webServer.argsCount() < 1) || (webServer.argsCount() > 1))
    return;

  const char* index = webServer.path(1);               // relay index string
  uint8_t relay = index ? atoi(index) : 0;             // relay index value
  uint8_t state = RELAY_ANY;                           // relay state

  state = webServer.arg("state", CMD_ON) ? RELAY_ON : state; // get requested state = on
  state = webServer.arg("state", CMD_OFF) ? RELAY_OFF : state; // get requested state = off

  if(index)
  {                                           // if specific relay is speciified
    UpdateRelay(relay, state);                // set state for specfic relay
    webServer.respond(returnCode = 200);      // send OK to client
  }
  else
  {
    UpdateRelays(state);                              // set state for all relays
    webServer.respond(returnCode = 200);                      // send OK to client
  }
}


// generate JSON for all relays
void Relay2Json(char* content, size_t size, uint8_t state)
{
  StaticJsonBuffer<550> jsonBuffer;                     // create buffer
  JsonArray& root = jsonBuffer.createArray();           // create array

  for(int i = 0; i < RELAY_COUNT; i++)
  {                   // for all relays
    if((state == RELAY_ANY) || (state == lcRelaySet[i]))
    {
      JsonObject& item = root.createNestedObject();       // create object
      item["relay"] = i;                                  // write object
      item["state"] = (lcRelaySet[i] == RELAY_ON) ? CMD_ON : CMD_OFF;
    }
  }

  root.printTo(content, size);                           // write to char buffer
}

// generate JSON for relay item i
void Relay2Json(char* content, size_t size, uint8_t i, uint8_t /* state */)
{
  if(/* (i >= 0) && */ (i < RELAY_COUNT))
  {                     // check if relay is valid
    StaticJsonBuffer<128> jsonBuffer;                       // create buffer
    JsonArray& root = jsonBuffer.createArray();         // create array
    JsonObject& item = root.createNestedObject();        // create object

    item["relay"] = i;                                     // write object
    item["state"] = (lcRelaySet[i] == RELAY_ON) ? CMD_ON : CMD_OFF;

    root.printTo(content, size);                         // write to char buffer
  }
  else
  {
    strClr(content);                                       // clear char buffer
  }
}

// print relay state for all relays (write buffer, requested state)
void RelayPrint(char* line, uint8_t state)
{
  for(int i = 0; i < RELAY_COUNT; i++)  // for all relays
  {
    if((state == RELAY_ANY) || (state == lcRelaySet[i]))
    {
      RelayPrint(line, i, state);                      // write state of relay i
    }
  }
}

// print relay state for single relay (write buffer, relay i, requested state)
void RelayPrint(char* line, uint8_t i, uint8_t state)
{
  // check if index and state are valid
  if(/*(i >= 0) && */ (i < RELAY_COUNT) && ((state == RELAY_ANY) || (state == lcRelaySet[i])))
  {
    strcat(line, "Relay ");
    strcat(line, dec(i, 2));  // write state of relay i
//    strcat(line, " on pin ");
//    strcat(line, dec(lcRelayPin[i], 2));
    strcat(line, " = ");
    strcat(line, lcRelaySet[i] == RELAY_ON ? CMD_ON : CMD_OFF);
    strcat(line, "\r\n");
  }
  else
  {
    strcat(line, "R ");
    strcat(line, dec(i, 2));
    strcat(line, " undef");
    strcat(line, "\r\n");   // undefined relay
  }
}

#endif // #if defined(USE_WEB_SERVER)

