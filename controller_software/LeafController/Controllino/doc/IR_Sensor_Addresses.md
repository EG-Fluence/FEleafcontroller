# IR Sensor TMP006


__[adafruit](https://learn.adafruit.com/infrared-thermopile-sensor-breakout/featured_products)__ - Contact-less Infrared Thermopile Sensor Breakout - TMP006

## Connections for R3 and Later Arduinos:

|     |            | Controllino X1 |
| --- | ---------- |---------------:|
| VCC | 3.3v or 5v | 1              |
| GND | GND        | 2              |
| SDA | SDA        | 18             |
| SCL | SCL        | 19             |


The default address of the TMP006 is 0x40. By connecting the address pins as in the following table, you can generate any address between 0x40 and 0x47

## Mapping

| AD0 | AD1 | I2C Address | 
| --- | --- | ----------- |
| GND | GND | 0x40        |
| GND | VCC | 0x41        |
| GND | SDA | 0x42        |
| GND | SCL | 0x43        |
| VCC | GND | 0x44        |
| VCC | VCC | 0x45        |
| VCC | SDA | 0x46        |
| VCC | SCL | 0x47        |

# IR Sensor TMP007

The TMP007 is the latest thermopile sensor from TI, and is an update of the TMP006. The internal math engine does all the temperature calculations so its easier to integrate - you can read the die and target temperatures directly over I2C. The TMP007 also has better transient management, so you don't get as much over/undershoot when the temperature changes a lot.

__[adafruit](https://learn.adafruit.com/adafruit-tmp007-sensor-breakout/pinouts)__ - Contact-less Infrared Thermopile Sensor Breakout - TMP007
