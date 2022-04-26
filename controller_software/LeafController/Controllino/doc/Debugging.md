See:
[Fluence documentation](https://fluenceenergy.sharepoint.com/sites/nextgen/Shared%20Documents/Forms/AllItems.aspx?viewid=2cf5bfe0%2D965c%2D4b36%2D8092%2D9e87f2ca317c&id=%2Fsites%2Fnextgen%2FShared%20Documents%2FControls%20HW%20and%20SW%2FNextGen%20Controller%20Workstream%2FVendor%20Info%2FArduino%2FControllino%2FDebugging)

# Debugging

## The Atmel-ICE Debugger

[Documentation](ATATMEL-ICE.pdf)

##  Atmel-ICE and Controllino connection

| Atmel --- ICE |  _____ C _ O _ N _ T _ R _ O _ L _ L _ I _ N _ O _____             |
| ----------  | ------------------------------------- |

| ____ AVR ____  | _____ SPI _____ | _____________ JTAG _____________    |
| ----------- | ------------|------------------------ |

| ____ Port ____ | _____ X1 _____ |  _____ X1 _____ | _____ X2 _____ |
| ----------  | ------------|------------|----------- |

| Pin | Name  | Pin | Name  | Pin |Name | Pin | Name |
| --: | ----  | --: | ----- | --: | ---- | --: | ---- |
| 1   | TCK   | 23  | SCK   |  7  | A4   |     |      |
| 2   | GND   |  2  | GND   |  2  | GND  |     |      |
| 3   | TDO   | 22  | MISO  |     |      |  3  | A6   |
| 4   | VTG   |  1  | 5 V   | 1   | 5 V  |     |      |
| 5   | TMS   |     |       | 8   | A5   |     |      |
| 6   | nSRST |  11 | RESET | 11  | RESET|     |      |
| 7   | NC    |     |       |     |      |     |      |
| 8   | nTRST |     |       |     |      |     |      |
| 9   | TDI   | 21  |MOSI   |     |      |  4  | A7   |
| 10  | GND   |     |       |     |      |     |      |

##  Atmel-Studio 7

Studio 7 is the integrated development platform (IDP) for developing and debugging all AVR® and SAM microcontroller applications. The Atmel Studio 7 IDP gives you a seamless and easy-to-use environment to write, build and debug your applications written in C/C++ or assembly code. It also connects seamlessly to the debuggers, programmers and development kits that support AVR® and SAM devices.

[Download](http://studio.download.atmel.com/7.0.2397/as-installer-7.0.2397-web.exe)

Once the ICE cable and the external power is connected to the board, you need to start Atmel Studio-7, click on "Tools", then "Device Programming" to call up the dialog box.
In the dialog box, select "Atmel-ICE" for the tool, "ATmega2560" for the device, "SPI" for the interface, then click "Apply". This will bring up a menu on the left side. Select "Fuses", and a list of fuses will appear. Follow the pictures from table under.

| Atmel Studio |  |
| ----------  | ------------------------------------- |
| Enable fuses (SPI and X1 connector) | [Atmega2560 fuses enable JTAG via ISP](pics/Atmega2560_fuses_enableJTAG_via_ISP.jpg) |
| Check fuses (JTAG and X1/X2 connectors) | [Atmega2560 fuses read via JTAG](pics/Atmega2560_fuses_read_via_JTAG.jpg)|
| Enable debugging | [Atmega2560 JTAG Debugger Setup](pics/Atmega2560_JTAG_DebuggerSetup.jpg)|
| Debugging | [Atmega2560 JTAG Debugging Works](pics/Atmega2560_JTAG_DebuggingWorks.jpg)|

# Old collected from internet

## Sources
- [Source 1](https://www.avrfreaks.net/comment/1807511#comment-1807511)
- [Source 2](https://forum.arduino.cc/index.php?topic=387246.0)

Ok, I think I've finally got this thing worked out.  There was an incredible amount of confusion on this matter, at least for me (and probably many other users with Arduino development boards).  Here's what I've found:

 

1.  When connecting to the Arduino ATMEGA2560 board, the small 6-pin connector at the end of the ICE cable IS NOT FOR JTAG!  It is for accessing the on-board memory of the 2560 device through SPI.
2.  As per above, you DO have to connect the ICE 6-pin connector (tab towards MCU) to the ICSP header in the middle of the board next to the 2560 chip to access its memory.
3.  The ICE box DOES NOT power the ATMEGA2560 board like the original USB cable did.  You must plug in an external +9VDC wall wart to provide power, which the ICE box then senses.
4.  Once the ICE cable and the external power is connected to the board, you need to open up Atmel Studio-7, click on "Tools", then "Device Programming" to call up the setup dialog box.
5.  Once in the dialog box, select "Atmel-ICE" for the tool, "ATmega2560" for the device, "SPI" for the interface, then click "Apply".  This will bring up a menu on the left side.  Select "Fuses", and a list of fuses will appear.
6.  Place a check mark in the "OCDEN", "JTAGEN", and "BOOTRST" check boxes, then click "Program" to set the fuses in the 2560 chip.  Close the dialog box.  Unplug power from the board and the USB cable from the ICE box.
7.  Unplug the 6-pin connector from the ATMEGA2560 board and the cable from the ICE box.  Peel back a small section of the 5th wire on the ICE box ribbon cable that has been left un-terminated, and solder a 3-4" wire onto it.  Strip the other end and solder a pin to it.
8.  Now, use jumper wires and connect between the ICE box 6-pin connector and the ATMEGA2560 board Analog-In pins in the following order:  Pin-1 (TDO) to Analog-In A6,  Pin-3 (TCK) to Analog-In A4,  Pin-4 (TDI) to Analog-in A7, the wire you terminated in step-7 (TMS) to Analog-In A5, Pin-2 (Vsense) to +5VDC on the board, and finally, Pin-6 (GND) to ground on board.  I did not connect the reset line; time will tell if I need it.
9.  Next, plug in the USB cable to the ICE box, then the board power plug (in that order).  The left LED on the ICE box should be lit up green, and the middle LED should be lit up red, indicating power is properly applied to the board and the ICE box.
10.  Call up the setup dialog box in Studio-7 once again by clicking  on "Tools", then "Device Programming".  This time select "Atmel-ICE" for the tool, "ATmega2560" for the device, "JTAG" for the interface, then click "Apply".
11.  If all is well the a menu will once again pop up on the left side.  Select "Fuses" once again.  Check that "JTAGEN" and "BOOTRST" are checked ("OCDEN" may or may not be checked - don't worry about it).  Device Signature and Target voltage will be displayed if you've successfully connected.


Hope this helps somebody in some small way.  This was an incredible amount of digging, reading, and experimentation.  I'm not sure why it has to be that way, but I did get it to work.  Thanks to all who contributed to this thread and got me pointed in the right direction.  I don't think I would have been successful otherwise.


That didn't work for me with an ATMega2560, i had to use the following:
| Atmel-ICE JTEG | ATMega2560 |
| ---------------| ---------- |
| 1 | A4 |
| 5 | A5 |
| 3 | A6 |
| 9 | A7 |
| 4 | +5v |
| 2 | GND |
| 8 |  RESET |

Connected as above everything works fine.

or 
| Atmel-ICE JTEG | ATMega2560 |
| ---------------| ---------- |
| 1 | A4 |
| 5 | A5 |
| 3 | A6 |
| 9 | A7 |
| 4 | +5v |
| 2 | GND |
| 6 | RESET |


