/*
 * LeafController_IO.h
 *
 *  Created on: 12.06.2019
 *      Author: Boris.Kajganic
 */

#ifndef LCINPUTOUTPUT_H_
#define LCINPUTOUTPUT_H_

enum LCType
{
	LC_DO,
	LC_DI,
	LC_AI,
	LC_AO,
	LC_Temp_DS18B20
};

struct LCIOPin
{
	uint8_t pin;
	uint8_t type;
};

/*
class LCIO
{
public:
  LCIO();
  virtual ~LCIO();
};
*/

void ConfigRelays(void);
void ConfigOutputs(void);
void ConfigInputs(void);
void ConfigTempSensorsDS18B20(void);
void ConfigInternalValues();


// set all relays with value cmd
void UpdateRelays(uint8_t state);
// set state of relay i
void UpdateRelay(uint8_t i, uint8_t state);

// set all (digital) ouputs with value cmd
void UpdateOutputs(uint16_t state, LCType lcType = LC_DO);
// set state of (digital) output i
void UpdateOutput(uint8_t i, uint16_t state, LCType lcType = LC_DO);

// set state of digital input i
void UpdateInput(uint8_t i);

uint16_t ReadInput(uint8_t i);

void UpdateTemperatures();

void UpdateInternalValues();

#endif /* LCINPUTOUTPUT_H_ */
