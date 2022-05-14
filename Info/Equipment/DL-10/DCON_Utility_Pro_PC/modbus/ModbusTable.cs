public static bool GetModbus(CIO io, CmdDebug p, ref UInt16 functionCode, ref UInt16 refAddr)
{
string cmdTitle = string.Empty;
bool hasCmd = false;
functionCode = 0;
//refAddr = 0;
switch (p.cmdType)
{
case 10:
case 100: // GET Configuration
if (p.cmdName.IndexOf("GET_") >= 0)
cmdTitle = p.cmdName;
else
cmdTitle = "GET_" + p.cmdName;
break;
case 11:
case 1:
if (p.cmdName.IndexOf("SET_") >= 0)
cmdTitle = p.cmdName;
else
cmdTitle = "SET_" + p.cmdName;
break;
case 2:
if (p.cmdName.IndexOf("OUTPUT_") >= 0)
cmdTitle = p.cmdName;
else
cmdTitle = "OUTPUT_" + p.cmdName;
break;
case 20:
if (p.cmdName.IndexOf("CLEAR_") >= 0)
cmdTitle = p.cmdName;
else
cmdTitle = "CLEAR_" + p.cmdName;
break;
case -1:
if (p.cmdName.IndexOf("READ_") >= 0)
cmdTitle = p.cmdName;
else
cmdTitle = "READ_" + p.cmdName;
break;
default:
cmdTitle = p.cmdName;
break;
}
switch (io.module.ID)
{
  #region CL204WF
                case "CL204WF":
                    hasCmd = false;
                    switch (cmdTitle)
                    {
                        case "GET_MODULE_NAME":
                            functionCode = 0x46;
                            refAddr = 65535;
                            break;
                        case "GET_MODULE_FIRMWARE":
                            functionCode = 0x46;
                            refAddr = 65535;
                            break;
                        case "GET_COMMUNICATE_PARAMETER":
                            functionCode = 0x46;
                            refAddr = 65535;
                            break;
                        case "GET_MODULE_ADDRESS":
                            functionCode = 0x3;
                            refAddr = 484;
                            break;
                        case "GET_MODBUS_MISC":
                            functionCode = 0x46;
                            refAddr = 65535;
                            break;
                        case "GET_RESPONSE_DELAY_TIME":
                            functionCode = 0x3;
                            refAddr = 487;
                            break;
                        case "GET_WDT_TIMER":
                            functionCode = 0x3;
                            refAddr = 488;
                            break;
                        case "GET_WDT_ENABLE":
                            functionCode = 0x1;
                            refAddr = 260;
                            break;
                        case "GET_WDT_OVERWRITE":
                            functionCode = 0x1;
                            refAddr = 259;
                            break;
                        case "READ_LOG_RTC":
                            functionCode = 0x3;
                            refAddr = 864;
                            break;
                        case "GET_LOG_SAMPLE_MODE":
                            functionCode = 0x3;
                            refAddr = 875;
                            break;
                        case "GET_LOG_OVERWRITE":
                            functionCode = 0x3;
                            refAddr = 876;
                            break;
                        case "GET_LOG_SAMPLE_PERIOD":
                            functionCode = 0x3;
                            refAddr = 877;
                            break;
                        case "GET_LOG_START_TIME":
                            functionCode = 0x3;
                            refAddr = 880;
                            break;
                        case "READ_LOG_END_TIME":
                            functionCode = 0x3;
                            refAddr = 886;
                            break;
                        case "GET_CH0_SIMPLE_AI_HIGH_ALARM_LIMIT":
                            functionCode = 0x3;
                            refAddr = 224;
                            break;
                        case "GET_CH0_AI_ALARM_ENABLE_STATUS":
                            functionCode = 0x1;
                            refAddr = 320;
                            break;
                        case "GET_CH1_SIMPLE_AI_HIGH_ALARM_LIMIT":
                            functionCode = 0x3;
                            refAddr = 225;
                            break;
                        case "GET_CH1_AI_ALARM_ENABLE_STATUS":
                            functionCode = 0x1;
                            refAddr = 321;
                            break;
                        case "GET_CH2_SIMPLE_AI_HIGH_ALARM_LIMIT":
                            functionCode = 0x3;
                            refAddr = 226;
                            break;
                        case "GET_CH2_SIMPLE_AI_LOW_ALARM_LIMIT":
                            functionCode = 0x3;
                            refAddr = 234;
                            break;
                        case "GET_CH2_AI_ALARM_ENABLE_STATUS":
                            functionCode = 0x1;
                            refAddr = 322;
                            break;
                        case "GET_CH3_SIMPLE_AI_HIGH_ALARM_LIMIT":
                            functionCode = 0x3;
                            refAddr = 227;
                            break;
                        case "GET_CH3_SIMPLE_AI_LOW_ALARM_LIMIT":
                            functionCode = 0x3;
                            refAddr = 235;
                            break;
                        case "GET_CH3_AI_ALARM_ENABLE_STATUS":
                            functionCode = 0x1;
                            refAddr = 323;
                            break;
                        case "GET_CH4_SIMPLE_AI_HIGH_ALARM_LIMIT":
                            functionCode = 0x3;
                            refAddr = 228;
                            break;
                        case "GET_CH4_SIMPLE_AI_LOW_ALARM_LIMIT":
                            functionCode = 0x3;
                            refAddr = 236;
                            break;
                        case "GET_CH4_AI_ALARM_ENABLE_STATUS":
                            functionCode = 0x1;
                            refAddr = 324;
                            break;
                        case "GET_CH5_SIMPLE_AI_HIGH_ALARM_LIMIT":
                            functionCode = 0x3;
                            refAddr = 229;
                            break;
                        case "GET_CH5_SIMPLE_AI_LOW_ALARM_LIMIT":
                            functionCode = 0x3;
                            refAddr = 237;
                            break;
                        case "GET_CH5_AI_ALARM_ENABLE_STATUS":
                            functionCode = 0x1;
                            refAddr = 325;
                            break;
                        case "GET_CH6_SIMPLE_AI_HIGH_ALARM_LIMIT":
                            functionCode = 0x3;
                            refAddr = 230;
                            break;
                        case "GET_CH6_SIMPLE_AI_LOW_ALARM_LIMIT":
                            functionCode = 0x3;
                            refAddr = 238;
                            break;
                        case "GET_CH6_AI_ALARM_ENABLE_STATUS":
                            functionCode = 0x1;
                            refAddr = 326;
                            break;
                        case "GET_DL_BUZZER_ENSTATUS":
                            functionCode = 0x1;
                            refAddr = 448;
                            break;
                        case "GET_DL30X_BUZZER":
                            functionCode = 0x4;
                            refAddr = 496;
                            break;
                        case "GET_DL30X_TEMPERATURE_OFFSET":
                            functionCode = 0x3;
                            refAddr = 450;
                            break;
                        case "GET_DL30X_HUMIDITY_OFFSET":
                            functionCode = 0x3;
                            refAddr = 449;
                            break;
                        case "GET_HCHO_OFFSET":
                            functionCode = 0x4;
                            refAddr = 448;
                            break;
                        case "GET_TVOC_OFFSET":
                            functionCode = 0x4;
                            refAddr = 449;
                            break;
                        case "READ_CH0_AI":
                            functionCode = 0x4;
                            refAddr = 0;
                            break;
                        case "READ_CH1_AI":
                            functionCode = 0x4;
                            refAddr = 1;
                            break;
                        case "READ_CH2_AI":
                            functionCode = 0x4;
                            refAddr = 2;
                            break;
                        case "READ_CH3_AI":
                            functionCode = 0x4;
                            refAddr = 3;
                            break;
                        case "READ_CH5_AI":
                            functionCode = 0x4;
                            refAddr = 5;
                            break;
                        case "READ_CH4_AI":
                            functionCode = 0x4;
                            refAddr = 4;
                            break;
                        case "READ_CH6_AI":
                            functionCode = 0x4;
                            refAddr = 6;
                            break;
                        case "READ_SIMPLE_AI_DO_STATUS":
                            functionCode = 0x1;
                            refAddr = 0;
                            break;
                        case "READ_LOW_ALARM_DO_ON":
                            functionCode = 0x1;
                            refAddr = 288;
                            break;
                        case "READ_HIGH_ALARM_DO_ON":
                            functionCode = 0x1;
                            refAddr = 304;
                            break;
                        case "READ_LOG_STATUS":
                            functionCode = 0x4;
                            refAddr = 874;
                            break;
                        case "READ_WDT_STATUS":
                            functionCode = 0x1;
                            refAddr = 269;
                            break;
                        case "SET_RESPONSE_DELAY_TIME":
                            functionCode = 0x6;
                            refAddr = 487;
                            break;
                        case "SET_MODBUS_MISC":
                            functionCode = 0x5;
                            refAddr = 270;
                            break;
                        case "SET_HCHO_OFFSET":
                            functionCode = 0x6;
                            refAddr = 448;
                            break;
                        case "SET_TVOC_OFFSET":
                            functionCode = 0x6;
                            refAddr = 449;
                            break;
                        case "SET_DL30X_HUMIDITY_OFFSET":
                            functionCode = 0x6;
                            refAddr = 449;
                            break;
                        case "SET_DL30X_TEMPERATURE_OFFSET":
                            functionCode = 0x6;
                            refAddr = 450;
                            break;
                        case "CLEAR_CH2_LOW_ALARM_LATCH":
                            functionCode = 0x5;
                            refAddr = 290;
                            break;
                        case "CLEAR_CH6_HIGH_ALARM_LATCH":
                            functionCode = 0x5;
                            refAddr = 310;
                            break;
                        case "CLEAR_CH5_HIGH_ALARM_LATCH":
                            functionCode = 0x5;
                            refAddr = 309;
                            break;
                        case "CLEAR_CH5_LOW_ALARM_LATCH":
                            functionCode = 0x5;
                            refAddr = 293;
                            break;
                        case "CLEAR_CH4_LOW_ALARM_LATCH":
                            functionCode = 0x5;
                            refAddr = 292;
                            break;
                        case "CLEAR_CH3_LOW_ALARM_LATCH":
                            functionCode = 0x5;
                            refAddr = 291;
                            break;
                        case "CLEAR_CH2_HIGH_ALARM_LATCH":
                            functionCode = 0x5;
                            refAddr = 306;
                            break;
                        case "CLEAR_CH1_HIGH_ALARM_LATCH":
                            functionCode = 0x5;
                            refAddr = 305;
                            break;
                        case "CLEAR_CH0_HIGH_ALARM_LATCH":
                            functionCode = 0x5;
                            refAddr = 304;
                            break;
                        case "CLEAR_CH6_LOW_ALARM_LATCH":
                            functionCode = 0x5;
                            refAddr = 294;
                            break;
                        case "CLEAR_CH4_HIGH_ALARM_LATCH":
                            functionCode = 0x5;
                            refAddr = 308;
                            break;
                        case "CLEAR_CH3_HIGH_ALARM_LATCH":
                            functionCode = 0x5;
                            refAddr = 307;
                            break;
                    }
                    break;
                #endregion CL204WF
#region 7002
case "7002":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
// AI alarm Clear low/high alarm latch
case "SET_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 261;
break;
case "SET_ENABLE_AI_ALARM_MODE":
functionCode = 0x0F;
refAddr  = 261;
break;
case "SET_CH0_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 224;
break;
case "SET_CH1_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 225;
break;
case "SET_CH2_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 226;
break;
case "SET_CH3_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 227;
break;
case "GET_CH0_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 224;
break;
case "GET_CH1_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 225;
break;
case "GET_CH2_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 226;
break;
case "GET_CH3_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 227;
break;
case "SET_CH0_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 232;
break;
case "SET_CH1_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 233;
break;
case "SET_CH2_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 234;
break;
case "SET_CH3_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 235;
break;
case "READ_SIMPLE_AI_ALARM_STATUS":
functionCode = 0x01;
refAddr  = 261;
break;
case "GET_CH0_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 232;
break;
case "GET_CH1_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 233;
break;
case "GET_CH2_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 234;
break;
case "GET_CH3_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 235;
break;
case "CLEAR_CH0_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 288;
break;
case "CLEAR_CH0_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 304;
break;
case "CLEAR_CH1_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 289;
break;
case "CLEAR_CH1_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 305;
break;
case "CLEAR_CH2_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 290;
break;
case "CLEAR_CH2_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 306;
break;
case "CLEAR_CH3_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 291;
break;
case "CLEAR_CH3_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 307;
break;
case "READ_LOW_ALARM_DO_ON":
functionCode = 0x01;
refAddr  = 288;
break;
case "READ_HIGH_ALARM_DO_ON":
functionCode = 0x01;
refAddr  = 304;
break;
// DI function
case "READ_SIMPLE_AI_DI_STATUS":
functionCode = 0x02;
refAddr  = 32;
break;
case "SET_DI_REVERSE":
functionCode = 0x05;
refAddr  = 264;
break;
case "GET_DI_REVERSE":
functionCode = 0x01;
refAddr  = 264;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x03;
refAddr  = 96;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x03;
refAddr  = 97;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x03;
refAddr  = 98;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x03;
refAddr  = 99;
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x03;
refAddr  = 100;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
// DO ch0 ~ ch3 output power on /safe value
case "READ_SIMPLE_AI_DO_STATUS":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_MFDO":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_MFDO":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_MFDO":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_MFDO":
functionCode = 0x05;
refAddr  = 3;
break;
case "SET_MF_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 192;
break;
case "READ_MF_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 192;
break;
case "SET_MF_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_MF_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x05;
refAddr  = 259;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7002
#region 7003
case "7003":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
// AI alarm Clear low/high alarm latch
case "SET_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 261;
break;
case "SET_ENABLE_AI_ALARM_MODE":
functionCode = 0x0F;
refAddr  = 261;
break;
case "READ_SIMPLE_AI_ALARM_STATUS":
functionCode = 0x01;
refAddr  = 261;
break;
//      40577 ~		// 240h: AI Alarm High Value
case "SET_CH0_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 576;
break;
case "SET_CH1_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 577;
break;
case "SET_CH2_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 578;
break;
case "SET_CH3_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 579;
break;
case "GET_CH0_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 576;
break;
case "GET_CH1_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 577;
break;
case "GET_CH2_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 578;
break;
case "GET_CH3_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 579;
break;
// 40609 ~		// 260h: AI Alarm Low Value
case "SET_CH0_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 608;
break;
case "SET_CH1_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 609;
break;
case "SET_CH2_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 610;
break;
case "SET_CH3_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 611;
break;
case "GET_CH0_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 608;
break;
case "GET_CH1_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 609;
break;
case "GET_CH2_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 610;
break;
case "GET_CH3_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 611;
break;
//00736 ~		// 2E0h: AI Alarm High, write 1 to clear
case "READ_HIGH_ALARM_DO_ON":
functionCode = 0x01;
refAddr  = 704;
break;
case "CLEAR_CH0_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 704;
break;
case "CLEAR_CH1_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 705;
break;
case "CLEAR_CH2_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 706;
break;
case "CLEAR_CH3_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 707;
break;
// Low Alarm
case "READ_LOW_ALARM_DO_ON":
functionCode = 0x01;
refAddr  = 736;
break;
//00705 ~		// 2C0h: AI Alarm Low, write 1 to clear
case "CLEAR_CH0_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 736;
break;
case "CLEAR_CH1_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 737;
break;
case "CLEAR_CH2_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 738;
break;
case "CLEAR_CH3_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 739;
break;
// DI function
case "READ_SIMPLE_AI_DI_STATUS":
functionCode = 0x02;
refAddr  = 32;
break;
// DO ch0 ~ ch3 output power on /safe value
case "READ_SIMPLE_AI_DO_STATUS":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_MFDO":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_MFDO":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_MFDO":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_MFDO":
functionCode = 0x05;
refAddr  = 3;
break;
case "SET_MF_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "READ_MF_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
case "SET_MF_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_MF_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x05;
refAddr  = 259;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7003
#region 7004
case "7004":
case "2004":
    hasCmd = false;
    switch (cmdTitle)
    {
        case "READ_CH0_SENSOR_AI":
            functionCode = 0x04;
            refAddr  = 0;
            break;
        case "READ_CH1_SENSOR_AI":
            functionCode = 0x04;
            refAddr  = 1;
            break;
        case "READ_CH2_SENSOR_AI":
            functionCode = 0x04;
            refAddr  = 2;
            break;
        case "READ_CH3_SENSOR_AI":
            functionCode = 0x04;
            refAddr  = 3;
            break;
    }
    break;
#endregion 7004
#region 7005
case "7005":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_TEMPERATURE_DISPLAY_FORMAT":
functionCode = 0x05;
refAddr  = 266;
break;
case "GET_TEMPERATURE_DISPLAY_FORMAT":
functionCode = 0x01;
refAddr  = 266;
break;
case "SET_USER_DEFINED_TYPE_A":
functionCode = 0x10;
refAddr  = 768;
break;
case "SET_USER_DEFINED_TYPE_B":
functionCode = 0x10;
refAddr  = 800;
break;
case "SET_USER_DEFINED_TYPE_C":
functionCode = 0x10;
refAddr  = 832;
break;
case "GET_USER_DEFINED_TYPE_A":
functionCode = 0x03;
refAddr  = 768;
break;
case "GET_USER_DEFINED_TYPE_B":
functionCode = 0x03;
refAddr  = 800;
break;
case "GET_USER_DEFINED_TYPE_C":
functionCode = 0x03;
refAddr  = 832;
break;
// Alarm DO Channel
case "READ_CH0_ALARM_DO_CHANNELS":
functionCode = 0x03;
refAddr  = 736;
break;
case "READ_CH1_ALARM_DO_CHANNELS":
functionCode = 0x03;
refAddr  = 737;
break;
case "READ_CH2_ALARM_DO_CHANNELS":
functionCode = 0x03;
refAddr  = 738;
break;
case "READ_CH3_ALARM_DO_CHANNELS":
functionCode = 0x03;
refAddr  = 739;
break;
case "READ_CH4_ALARM_DO_CHANNELS":
functionCode = 0x03;
refAddr  = 740;
break;
case "READ_CH5_ALARM_DO_CHANNELS":
functionCode = 0x03;
refAddr  = 741;
break;
case "READ_CH0_ALARM_DO_ON":
functionCode = 0x03;
refAddr  = 704;
break;
case "READ_CH1_ALARM_DO_ON":
functionCode = 0x03;
refAddr  = 705;
break;
case "READ_CH2_ALARM_DO_ON":
functionCode = 0x03;
refAddr  = 706;
break;
case "READ_CH3_ALARM_DO_ON":
functionCode = 0x03;
refAddr  = 707;
break;
case "READ_CH4_ALARM_DO_ON":
functionCode = 0x03;
refAddr  = 708;
break;
case "READ_CH5_ALARM_DO_ON":
functionCode = 0x03;
refAddr  = 709;
break;
// Disable low/high alarm mode
case "SET_CH0_LOW_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 328;
break;
case "SET_CH1_LOW_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 329;
break;
case "SET_CH2_LOW_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 330;
break;
case "SET_CH3_LOW_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 331;
break;
case "SET_CH4_LOW_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 332;
break;
case "SET_CH5_LOW_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 333;
break;
case "SET_CH6_LOW_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 334;
break;
case "SET_CH7_LOW_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 335;
break;
case "SET_CH0_HIGH_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 320;
break;
case "SET_CH1_HIGH_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 321;
break;
case "SET_CH2_HIGH_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 322;
break;
case "SET_CH3_HIGH_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 323;
break;
case "SET_CH4_HIGH_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 324;
break;
case "SET_CH5_HIGH_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 325;
break;
case "SET_CH6_HIGH_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 326;
break;
case "SET_CH7_HIGH_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 327;
break;
// Get Ch Low/High alarm enable status
case "SET_CH0_ENABLE_MULTI_AI_LOW_ALARM":
functionCode = 0x05;
refAddr  = 328;
break;
case "SET_CH1_ENABLE_MULTI_AI_LOW_ALARM":
functionCode = 0x05;
refAddr  = 329;
break;
case "SET_CH2_ENABLE_MULTI_AI_LOW_ALARM":
functionCode = 0x05;
refAddr  = 330;
break;
case "SET_CH3_ENABLE_MULTI_AI_LOW_ALARM":
functionCode = 0x05;
refAddr  = 331;
break;
case "SET_CH4_ENABLE_MULTI_AI_LOW_ALARM":
functionCode = 0x05;
refAddr  = 332;
break;
case "SET_CH5_ENABLE_MULTI_AI_LOW_ALARM":
functionCode = 0x05;
refAddr  = 333;
break;
case "SET_CH6_ENABLE_MULTI_AI_LOW_ALARM":
functionCode = 0x05;
refAddr  = 334;
break;
case "SET_CH7_ENABLE_MULTI_AI_LOW_ALARM":
functionCode = 0x05;
refAddr  = 335;
break;
case "GET_CH0_LOW_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 328;
break;
case "GET_CH1_LOW_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 329;
break;
case "GET_CH2_LOW_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 330;
break;
case "GET_CH3_LOW_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 331;
break;
case "GET_CH4_LOW_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 332;
break;
case "GET_CH5_LOW_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 333;
break;
case "GET_CH6_LOW_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 334;
break;
case "GET_CH7_LOW_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 335;
break;
//set enable high alarm
case "SET_CH0_ENABLE_MULTI_AI_HIGH_ALARM":
functionCode = 0x05;
refAddr  = 320;
break;
case "SET_CH1_ENABLE_MULTI_AI_HIGH_ALARM":
functionCode = 0x05;
refAddr  = 321;
break;
case "SET_CH2_ENABLE_MULTI_AI_HIGH_ALARM":
functionCode = 0x05;
refAddr  = 322;
break;
case "SET_CH3_ENABLE_MULTI_AI_HIGH_ALARM":
functionCode = 0x05;
refAddr  = 323;
break;
case "SET_CH4_ENABLE_MULTI_AI_HIGH_ALARM":
functionCode = 0x05;
refAddr  = 324;
break;
case "SET_CH5_ENABLE_MULTI_AI_HIGH_ALARM":
functionCode = 0x05;
refAddr  = 325;
break;
case "SET_CH6_ENABLE_MULTI_AI_HIGH_ALARM":
functionCode = 0x05;
refAddr  = 326;
break;
case "SET_CH7_ENABLE_MULTI_AI_HIGH_ALARM":
functionCode = 0x05;
refAddr  = 327;
break;
case "GET_CH0_HIGH_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 320;
break;
case "GET_CH1_HIGH_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 321;
break;
case "GET_CH2_HIGH_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 322;
break;
case "GET_CH3_HIGH_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 323;
break;
case "GET_CH4_HIGH_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 324;
break;
case "GET_CH5_HIGH_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 325;
break;
case "GET_CH6_HIGH_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 326;
break;
case "GET_CH7_HIGH_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 327;
break;
// set low alarm mode
case "SET_CH0_MULTI_AI_LOW_ALARM_MODE":
functionCode = 0x05;
refAddr  = 344;
break;
case "SET_CH1_MULTI_AI_LOW_ALARM_MODE":
functionCode = 0x05;
refAddr  = 345;
break;
case "SET_CH2_MULTI_AI_LOW_ALARM_MODE":
functionCode = 0x05;
refAddr  = 346;
break;
case "SET_CH3_MULTI_AI_LOW_ALARM_MODE":
functionCode = 0x05;
refAddr  = 347;
break;
case "SET_CH4_MULTI_AI_LOW_ALARM_MODE":
functionCode = 0x05;
refAddr  = 348;
break;
case "SET_CH5_MULTI_AI_LOW_ALARM_MODE":
functionCode = 0x05;
refAddr  = 349;
break;
case "SET_CH6_MULTI_AI_LOW_ALARM_MODE":
functionCode = 0x05;
refAddr  = 350;
break;
case "SET_CH7_MULTI_AI_LOW_ALARM_MODE":
functionCode = 0x05;
refAddr  = 351;
break;
// Get Low Alarm Mode
case "GET_CH0_LOW_ALARM_MODE":
functionCode = 0x01;
refAddr  = 344;
break;
case "GET_CH1_LOW_ALARM_MODE":
functionCode = 0x01;
refAddr  = 345;
break;
case "GET_CH2_LOW_ALARM_MODE":
functionCode = 0x01;
refAddr  = 346;
break;
case "GET_CH3_LOW_ALARM_MODE":
functionCode = 0x01;
refAddr  = 347;
break;
case "GET_CH4_LOW_ALARM_MODE":
functionCode = 0x01;
refAddr  = 348;
break;
case "GET_CH5_LOW_ALARM_MODE":
functionCode = 0x01;
refAddr  = 349;
break;
case "GET_CH6_LOW_ALARM_MODE":
functionCode = 0x01;
refAddr  = 350;
break;
case "GET_CH7_LOW_ALARM_MODE":
functionCode = 0x01;
refAddr  = 351;
break;
// set high alarm mode
case "SET_CH0_MULTI_AI_HIGH_ALARM_MODE":
functionCode = 0x05;
refAddr  = 336;
break;
case "SET_CH1_MULTI_AI_HIGH_ALARM_MODE":
functionCode = 0x05;
refAddr  = 337;
break;
case "SET_CH2_MULTI_AI_HIGH_ALARM_MODE":
functionCode = 0x05;
refAddr  = 338;
break;
case "SET_CH3_MULTI_AI_HIGH_ALARM_MODE":
functionCode = 0x05;
refAddr  = 339;
break;
case "SET_CH4_MULTI_AI_HIGH_ALARM_MODE":
functionCode = 0x05;
refAddr  = 340;
break;
case "SET_CH5_MULTI_AI_HIGH_ALARM_MODE":
functionCode = 0x05;
refAddr  = 341;
break;
case "SET_CH6_MULTI_AI_HIGH_ALARM_MODE":
functionCode = 0x05;
refAddr  = 342;
break;
case "SET_CH7_MULTI_AI_HIGH_ALARM_MODE":
functionCode = 0x05;
refAddr  = 343;
break;
// get high alarm mode
case "GET_CH0_HIGH_ALARM_MODE":
functionCode = 0x01;
refAddr  = 336;
break;
case "GET_CH1_HIGH_ALARM_MODE":
functionCode = 0x01;
refAddr  = 337;
break;
case "GET_CH2_HIGH_ALARM_MODE":
functionCode = 0x01;
refAddr  = 338;
break;
case "GET_CH3_HIGH_ALARM_MODE":
functionCode = 0x01;
refAddr  = 339;
break;
case "GET_CH4_HIGH_ALARM_MODE":
functionCode = 0x01;
refAddr  = 340;
break;
case "GET_CH5_HIGH_ALARM_MODE":
functionCode = 0x01;
refAddr  = 341;
break;
case "GET_CH6_HIGH_ALARM_MODE":
functionCode = 0x01;
refAddr  = 342;
break;
case "GET_CH7_HIGH_ALARM_MODE":
functionCode = 0x01;
refAddr  = 343;
break;
// set low alarm limit
case "SET_CH0_MULTI_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 232;
break;
case "SET_CH1_MULTI_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 233;
break;
case "SET_CH2_MULTI_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 234;
break;
case "SET_CH3_MULTI_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 235;
break;
case "SET_CH4_MULTI_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 236;
break;
case "SET_CH5_MULTI_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 237;
break;
case "SET_CH6_MULTI_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 238;
break;
case "SET_CH7_MULTI_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 239;
break;
case "GET_CH0_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 232;
break;
case "GET_CH1_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 233;
break;
case "GET_CH2_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 234;
break;
case "GET_CH3_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 235;
break;
case "GET_CH4_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 236;
break;
case "GET_CH5_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 237;
break;
case "GET_CH6_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 238;
break;
case "GET_CH7_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 239;
break;
// set high alarm limit
case "SET_CH0_MULTI_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 224;
break;
case "SET_CH1_MULTI_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 225;
break;
case "SET_CH2_MULTI_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 226;
break;
case "SET_CH3_MULTI_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 227;
break;
case "SET_CH4_MULTI_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 228;
break;
case "SET_CH5_MULTI_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 229;
break;
case "SET_CH6_MULTI_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 230;
break;
case "SET_CH7_MULTI_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 231;
break;
case "GET_CH0_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 224;
break;
case "GET_CH1_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 225;
break;
case "GET_CH2_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 226;
break;
case "GET_CH3_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 227;
break;
case "GET_CH4_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 228;
break;
case "GET_CH5_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 229;
break;
case "GET_CH6_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 230;
break;
case "GET_CH7_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 231;
break;
// set low alarm doch
case "SET_CH0_MULTI_AI_LOW_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 328;
break;
case "SET_CH1_MULTI_AI_LOW_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 329;
break;
case "SET_CH2_MULTI_AI_LOW_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 330;
break;
case "SET_CH3_MULTI_AI_LOW_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 331;
break;
case "SET_CH4_MULTI_AI_LOW_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 332;
break;
case "SET_CH5_MULTI_AI_LOW_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 333;
break;
case "SET_CH6_MULTI_AI_LOW_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 334;
break;
case "SET_CH7_MULTI_AI_LOW_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 335;
break;
case "GET_CH0_LOW_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 328;
break;
case "GET_CH1_LOW_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 329;
break;
case "GET_CH2_LOW_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 330;
break;
case "GET_CH3_LOW_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 331;
break;
case "GET_CH4_LOW_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 332;
break;
case "GET_CH5_LOW_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 333;
break;
case "GET_CH6_LOW_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 334;
break;
case "GET_CH7_LOW_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 335;
break;
// set high alarm doch
case "SET_CH0_MULTI_AI_HIGH_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 320;
break;
case "SET_CH1_MULTI_AI_HIGH_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 321;
break;
case "SET_CH2_MULTI_AI_HIGH_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 322;
break;
case "SET_CH3_MULTI_AI_HIGH_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 323;
break;
case "SET_CH4_MULTI_AI_HIGH_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 324;
break;
case "SET_CH5_MULTI_AI_HIGH_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 325;
break;
case "SET_CH6_MULTI_AI_HIGH_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 326;
break;
case "SET_CH7_MULTI_AI_HIGH_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 327;
break;
case "GET_CH0_HIGH_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 320;
break;
case "GET_CH1_HIGH_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 321;
break;
case "GET_CH2_HIGH_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 322;
break;
case "GET_CH3_HIGH_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 323;
break;
case "GET_CH4_HIGH_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 324;
break;
case "GET_CH5_HIGH_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 325;
break;
case "GET_CH6_HIGH_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 326;
break;
case "GET_CH7_HIGH_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 327;
break;
// Clear low latch
case "CLEAR_CH0_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 288;
break;
case "CLEAR_CH1_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 289;
break;
case "CLEAR_CH2_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 290;
break;
case "CLEAR_CH3_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 291;
break;
case "CLEAR_CH4_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 292;
break;
case "CLEAR_CH5_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 293;
break;
case "CLEAR_CH6_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 294;
break;
case "CLEAR_CH7_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 295;
break;
// clear high alarm latch
case "CLEAR_CH0_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 304;
break;
case "CLEAR_CH1_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 305;
break;
case "CLEAR_CH2_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 306;
break;
case "CLEAR_CH3_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 307;
break;
case "CLEAR_CH4_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 308;
break;
case "CLEAR_CH5_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 309;
break;
case "CLEAR_CH6_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 310;
break;
case "CLEAR_CH7_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 311;
break;
// Temperature Offset
case "SET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 288;
break;
case "SET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 289;
break;
case "SET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 290;
break;
case "SET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 291;
break;
case "SET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 292;
break;
case "SET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 293;
break;
case "SET_CH6_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 294;
break;
case "SET_CH7_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 295;
break;
case "GET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 288;
break;
case "GET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 289;
break;
case "GET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 290;
break;
case "GET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 291;
break;
case "GET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 292;
break;
case "GET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 293;
break;
case "GET_CH6_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 294;
break;
case "GET_CH7_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 295;
break;
// Read AI function
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
// channel type code
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
// DO function
case "READ_MULTI_AI_DIO_STATUS":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_MFDO":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_MFDO":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_MFDO":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_MFDO":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH5_MFDO":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH4_MFDO":
functionCode = 0x05;
refAddr  = 4;
break;
case "READ_MF_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 96;
break;
case "READ_MF_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 192;
break;
case "SET_MF_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 96;
break;
case "SET_MF_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 192;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7005
#region 7011
case "7011D":
case "7011":
case "7011PD":
case "7011P":
hasCmd = false;
switch (cmdTitle)
{
// module function
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
// AI and cjc function
case "SET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CJC_TEMPERATURE":
functionCode = 0x04;
refAddr  = 128;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
// alarm function
case "SET_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 261;
break;
case "SET_ENABLE_AI_ALARM_MODE":
functionCode = 0x0F;
refAddr  = 261;
break;
case "SET_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 224;
break;
case "SET_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 225;
break;
case "READ_SIMPLE_AI_ALARM_STATUS":
functionCode = 0x01;
refAddr  = 262;
break;
case "GET_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 224;
break;
case "GET_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 225;
break;
case "CLEAR_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
// di function
case "READ_SIMPLE_AI_DI_STATUS":
functionCode = 0x02;
refAddr  = 0;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x03;
refAddr  = 96;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 265;
break;
// do function
case "READ_SIMPLE_AI_DO_STATUS":
functionCode = 0x01;
refAddr  = 32;
break;
case "OUTPUT_CH0_MFDO":
functionCode = 0x05;
refAddr  = 32;
break;
case "OUTPUT_CH1_MFDO":
functionCode = 0x05;
refAddr  = 33;
break;
case "SET_MF_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 96;
break;
case "SET_MF_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 192;
break;
case "READ_MF_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 96;
break;
case "READ_MF_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 192;
break;
// LED function
case "SET_LED_MODE":
functionCode = 0x06;
refAddr  = 494;
break;
case "GET_LED_MODE":
functionCode = 0x03;
refAddr  = 494;
break;
case "SET_LED_DATA":
functionCode = 0x06;
refAddr  = 495;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7011
#region 7015P
case "7015":
case "7015P":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "SET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 288;
break;
case "SET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 289;
break;
case "SET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 290;
break;
case "SET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 291;
break;
case "SET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 292;
break;
case "SET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 293;
break;
case "GET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 288;
break;
case "GET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 289;
break;
case "GET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 290;
break;
case "GET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 291;
break;
case "GET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 292;
break;
case "GET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 293;
break;
}
break;
#endregion 7015P
#region 7016
case "7016":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_LINEAR_MODE_ENABLE_STATUS":
functionCode = 0x05;
refAddr  = 264;
break;
case "GET_LINEAR_MODE_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 264;
break;
case "SET_CH0_LINEAR_MAP_CONFIG":
functionCode = 0x10;
refAddr  = 160;
break;
case "SET_CH1_LINEAR_MAP_CONFIG":
functionCode = 0x10;
refAddr  = 160;
break;
case "GET_CH0_LINEAR_MAP_CONFIG":
functionCode = 0x03;
refAddr  = 160;
break;
case "GET_CH1_LINEAR_MAP_CONFIG":
functionCode = 0x03;
refAddr  = 160;
break;
// Alarm
case "SET_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 261;
break;
case "SET_ENABLE_AI_ALARM_MODE":
functionCode = 0x0F;
refAddr  = 261;
break;
case "CLEAR_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "READ_SIMPLE_AI_ALARM_STATUS":
functionCode = 0x01;
refAddr  = 261;
break;
case "SET_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 224;
break;
case "GET_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 224;
break;
case "SET_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 225;
break;
case "GET_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 225;
break;
// AI function
case "SET_7016_DUAL_CHANNEL":
functionCode = 0x06;
refAddr  = 489;
break;
case "SET_7016_CHANNEL_SELECT":
functionCode = 0x06;
refAddr  = 489;
break;
case "GET_7016_CHANNEL_SELECT":
functionCode = 0x03;
refAddr  = 489;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
// AO function
case "OUTPUT_EXCITATION_AO":
functionCode = 0x06;
refAddr  = 32;
break;
case "READ_EXCITATION_AO":
functionCode = 0x03;
refAddr  = 32;
break;
case "SET_CH0_AO_POWERON":
functionCode = 0x06;
refAddr  = 192;
break;
// DO function
case "OUTPUT_CH0_MFDO":
functionCode = 0x05;
refAddr  = 32;
break;
case "OUTPUT_CH1_MFDO":
functionCode = 0x05;
refAddr  = 33;
break;
case "SET_MF_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 96;
break;
case "SET_MF_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 192;
break;
case "READ_SIMPLE_AI_DO_STATUS":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_MF_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 96;
break;
case "READ_MF_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 192;
break;
// DI function
case "READ_SIMPLE_AI_DI_STATUS":
functionCode = 0x02;
refAddr  = 0;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 265;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7016
#region 7017/7017C/7017F
case "7017":
case "7017C":
case "7017RC":
case "7017F":
case "7017A5":
case "tAD2":
case "tAD8":
case "tAD8C":
case "tAD5":
case "tAD5C":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_SOFTWARE_WDT_ENABLE_STATUS":
functionCode = 0x05;
refAddr  = 277;
break;
case "GET_SOFTWARE_WDT_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 277;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
}
break;
#endregion 7017/7017C/7017F
#region 7017R
case "7017R":
case "7017RM":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_SOFTWARE_WDT_ENABLE_STATUS":
functionCode = 0x05;
refAddr  = 277;
break;
case "GET_SOFTWARE_WDT_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 277;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
// AI function
case "SET_CH0_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 256;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 256;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 257;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 257;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 258;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 258;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 259;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 259;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 260;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 260;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 261;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 261;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 262;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 262;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 263;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 263;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
}
break;
#endregion 7017R
#region 7017mC16
case "7017mC16":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_17Z_SINGLE_ENDED":
functionCode = 0x05;
refAddr  = 276;
break;
case "READ_17Z_SINGLE_ENDED":
functionCode = 0x01;
refAddr  = 276;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH8_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH8_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH9_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH9_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH10_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH10_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH11_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH11_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH12_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH12_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH13_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH13_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH14_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH14_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH15_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH15_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
case "READ_CH8_AI":
functionCode = 0x04;
refAddr  = 8;
break;
case "READ_CH9_AI":
functionCode = 0x04;
refAddr  = 9;
break;
case "READ_CH10_AI":
functionCode = 0x04;
refAddr  = 10;
break;
case "READ_CH11_AI":
functionCode = 0x04;
refAddr  = 11;
break;
case "READ_CH12_AI":
functionCode = 0x04;
refAddr  = 12;
break;
case "READ_CH13_AI":
functionCode = 0x04;
refAddr  = 13;
break;
case "READ_CH14_AI":
functionCode = 0x04;
refAddr  = 14;
break;
case "READ_CH15_AI":
functionCode = 0x04;
refAddr  = 15;
break;
// Log function
case "READ_LOG_STATUS":
functionCode = 0x04;
refAddr  = 874;
break;
case "READ_LOG_RTC":
functionCode = 0x04;
refAddr  = 864;
break;
case "WRITE_LOG_RTC":
functionCode = 0x10; // write registers
refAddr  = 864;
break;
case "SET_LOG_SAMPLE_MODE":
functionCode = 0x10; // write registers
refAddr  = 875;
break;
case "GET_LOG_SAMPLE_MODE":
functionCode = 0x04;
refAddr  = 875;
break;
case "SET_LOG_OVERWRITE": // ok
functionCode = 0x10; // write registers
refAddr  = 876;
break;
case "GET_LOG_OVERWRITE": //ok
functionCode = 0x04;
refAddr  = 876;
break;
case "SET_LOG_SAMPLE_PERIOD": //OK
functionCode = 0x10; // write registers
refAddr  = 877;
break;
case "GET_LOG_SAMPLE_PERIOD": //OK
functionCode = 0x04;
refAddr  = 877;
break;
case "SET_LOG_START_TIME": //OK
functionCode = 0x10; // write registers
refAddr  = 880;
break;
case "GET_LOG_START_TIME": //OK
functionCode = 0x04;
refAddr  = 880;
break;
case "SET_LOG_END_TIME": //OK
functionCode = 0x10; // write registers
refAddr  = 886;
break;
case "GET_LOG_END_TIME": //OK
functionCode = 0x04;
refAddr  = 886;
break;
}
break;
#endregion 7017mC16
#region 7017Z
case "7017Z":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_17Z_SINGLE_ENDED":
functionCode = 0x05;
refAddr  = 276;
break;
case "READ_17Z_SINGLE_ENDED":
functionCode = 0x01;
refAddr  = 276;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH8_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH8_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH9_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH9_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH10_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH10_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH11_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH11_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH12_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH12_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH13_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH13_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH14_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH14_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH15_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH15_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH16_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH16_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH17_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH17_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH18_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH18_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH19_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH19_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
case "READ_CH8_AI":
functionCode = 0x04;
refAddr  = 8;
break;
case "READ_CH9_AI":
functionCode = 0x04;
refAddr  = 9;
break;
case "READ_CH10_AI":
functionCode = 0x04;
refAddr  = 10;
break;
case "READ_CH11_AI":
functionCode = 0x04;
refAddr  = 11;
break;
case "READ_CH12_AI":
functionCode = 0x04;
refAddr  = 12;
break;
case "READ_CH13_AI":
functionCode = 0x04;
refAddr  = 13;
break;
case "READ_CH14_AI":
functionCode = 0x04;
refAddr  = 14;
break;
case "READ_CH15_AI":
functionCode = 0x04;
refAddr  = 15;
break;
case "READ_CH16_AI":
functionCode = 0x04;
refAddr  = 16;
break;
case "READ_CH17_AI":
functionCode = 0x04;
refAddr  = 17;
break;
case "READ_CH18_AI":
functionCode = 0x04;
refAddr  = 18;
break;
case "READ_CH19_AI":
functionCode = 0x04;
refAddr  = 19;
break;
}
break;
#endregion 7017Z
#region 7018
case "7018":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
// CJC function
case "SET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CJC_TEMPERATURE":
functionCode = 0x04;
refAddr  = 128;
break;
case "SET_CH0_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
}
break;
#endregion 7018
#region 701816
case "701816":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
// CJC function
case "SET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CJC_TEMPERATURE":
functionCode = 0x04;
refAddr  = 128;
break;
case "SET_CH0_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH8_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH9_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH10_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH11_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH12_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH13_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH14_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH15_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH8_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH9_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH10_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH11_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH12_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH13_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH14_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH15_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
case "READ_CH8_AI":
functionCode = 0x04;
refAddr  = 8;
break;
case "READ_CH9_AI":
functionCode = 0x04;
refAddr  = 9;
break;
case "READ_CH10_AI":
functionCode = 0x04;
refAddr  = 10;
break;
case "READ_CH11_AI":
functionCode = 0x04;
refAddr  = 11;
break;
case "READ_CH12_AI":
functionCode = 0x04;
refAddr  = 12;
break;
case "READ_CH13_AI":
functionCode = 0x04;
refAddr  = 13;
break;
case "READ_CH14_AI":
functionCode = 0x04;
refAddr  = 14;
break;
case "READ_CH15_AI":
functionCode = 0x04;
refAddr  = 15;
break;
}
break;
#endregion 701816
#region 7018R
case "7018R":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_ENABLE_THERMOCOUPLE_OPEN_DETECTION":
functionCode = 0x05;
refAddr  = 275;
break;
case "GET_ENABLE_THERMOCOUPLE_OPEN_DETECTION":
functionCode = 0x01;
refAddr  = 275;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
// CJC function
case "SET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CJC_TEMPERATURE":
functionCode = 0x04;
refAddr  = 128;
break;
case "SET_CH0_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
// AI function
case "SET_CH0_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 256;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 256;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 257;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 257;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 258;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 258;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 259;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 259;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 260;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 260;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 261;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 261;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 262;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 262;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 263;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 263;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
}
break;
#endregion 7018R
#region 7018Z
case "7018Z":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH8_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH8_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH9_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH9_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
case "READ_CH8_AI":
functionCode = 0x04;
refAddr  = 8;
break;
case "READ_CH9_AI":
functionCode = 0x04;
refAddr  = 9;
break;
// CJC function
case "SET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CJC_TEMPERATURE":
functionCode = 0x04;
refAddr  = 128;
break;
case "SET_CH0_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH8_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH9_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH8_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH9_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
}
break;
#endregion 7018Z
#region 7019R
case "7019":
case "7019R":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
// CJC function
case "SET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CJC_TEMPERATURE":
functionCode = 0x04;
refAddr  = 128;
break;
case "SET_CH0_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
// temperature offset
case "SET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 288;
break;
case "SET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 289;
break;
case "SET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 290;
break;
case "SET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 291;
break;
case "SET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 292;
break;
case "SET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 293;
break;
case "SET_CH6_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 294;
break;
case "SET_CH7_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 295;
break;
case "GET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 288;
break;
case "GET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 289;
break;
case "GET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 290;
break;
case "GET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 291;
break;
case "GET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 292;
break;
case "GET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 293;
break;
case "GET_CH6_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 294;
break;
case "GET_CH7_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 295;
break;
}
break;
#endregion 7019R
#region 7019Z
case "7019Z":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH8_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH8_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH9_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH9_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
case "READ_CH8_AI":
functionCode = 0x04;
refAddr  = 8;
break;
case "READ_CH9_AI":
functionCode = 0x04;
refAddr  = 9;
break;
// CJC function
case "SET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CJC_TEMPERATURE":
functionCode = 0x04;
refAddr  = 128;
break;
case "SET_CH0_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH8_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH9_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH8_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH9_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
// temperature offset
case "SET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 288;
break;
case "SET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 289;
break;
case "SET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 290;
break;
case "SET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 291;
break;
case "SET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 292;
break;
case "SET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 293;
break;
case "SET_CH6_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 294;
break;
case "SET_CH7_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 295;
break;
case "SET_CH8_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 296;
break;
case "SET_CH9_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 297;
break;
case "GET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 288;
break;
case "GET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 289;
break;
case "GET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 290;
break;
case "GET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 291;
break;
case "GET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 292;
break;
case "GET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 293;
break;
case "GET_CH6_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 294;
break;
case "GET_CH7_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 295;
break;
case "GET_CH8_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 296;
break;
case "GET_CH9_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 297;
break;
}
break;
#endregion 7019Z
#region 7022
case "7022A":
case "7022":
hasCmd = false;
switch (cmdTitle)
{
// module function
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// AO Type Code Slew Rate
case "SET_CH0_AO_TYPE_CODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_AO_TYPE_CODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_AO_SLEW_RATE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_AO_SLEW_RATE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_AO_TYPE_CODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_AO_TYPE_CODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_AO_SLEW_RATE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_AO_SLEW_RATE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_LAST_AO":
functionCode = 0x03;
refAddr  = 0;
break;
case "READ_CH1_LAST_AO":
functionCode = 0x03;
refAddr  = 1;
break;
// AO
case "OUTPUT_CH0_AO":
functionCode = 0x06;
refAddr  = 0;
break;
case "READ_CH0_AO":
functionCode = 0x03;
refAddr  = 64;
break;
case "OUTPUT_CH1_AO":
functionCode = 0x06;
refAddr  = 1;
break;
case "READ_CH1_AO":
functionCode = 0x03;
refAddr  = 65;
break;
case "SET_CH0_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 96;
break;
case "READ_CH0_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 96;
break;
case "SET_CH1_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 97;
break;
case "READ_CH1_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 97;
break;
case "SET_CH0_AO_POWERON":
functionCode = 0x06;
refAddr  = 192;
break;
case "SET_CH1_AO_POWERON":
functionCode = 0x06;
refAddr  = 193;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7022
#region 7024
case "7024":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_SLEWRATE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_SLEWRATE":
functionCode = 0x46;
refAddr  = 65535;
break;
// AO funciton
case "OUTPUT_CH0_AO":
functionCode = 0x06;
refAddr  = 0;
break;
case "OUTPUT_CH1_AO":
functionCode = 0x06;
refAddr  = 1;
break;
case "OUTPUT_CH2_AO":
functionCode = 0x06;
refAddr  = 2;
break;
case "OUTPUT_CH3_AO":
functionCode = 0x06;
refAddr  = 3;
break;
case "READ_CH0_LAST_AO":
functionCode = 0x03;
refAddr  = 0;
break;
case "READ_CH1_LAST_AO":
functionCode = 0x03;
refAddr  = 1;
break;
case "READ_CH2_LAST_AO":
functionCode = 0x03;
refAddr  = 2;
break;
case "READ_CH3_LAST_AO":
functionCode = 0x03;
refAddr  = 3;
break;
case "READ_CH0_AO":
functionCode = 0x03;
refAddr  = 64;
break;
case "READ_CH1_AO":
functionCode = 0x03;
refAddr  = 65;
break;
case "READ_CH2_AO":
functionCode = 0x03;
refAddr  = 66;
break;
case "READ_CH3_AO":
functionCode = 0x03;
refAddr  = 67;
break;
case "SET_CH0_AO_POWERON":
functionCode = 0x06;
refAddr  = 192;
break;
case "SET_CH1_AO_POWERON":
functionCode = 0x06;
refAddr  = 193;
break;
case "SET_CH2_AO_POWERON":
functionCode = 0x06;
refAddr  = 194;
break;
case "SET_CH3_AO_POWERON":
functionCode = 0x06;
refAddr  = 195;
break;
case "READ_CH0_AO_POWERON":
functionCode = 0x03;
refAddr  = 192;
break;
case "READ_CH1_AO_POWERON":
functionCode = 0x03;
refAddr  = 193;
break;
case "READ_CH2_AO_POWERON":
functionCode = 0x03;
refAddr  = 194;
break;
case "READ_CH3_AO_POWERON":
functionCode = 0x03;
refAddr  = 195;
break;
case "SET_CH0_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 96;
break;
case "SET_CH1_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 97;
break;
case "SET_CH2_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 98;
break;
case "SET_CH3_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 99;
break;
case "READ_CH0_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 96;
break;
case "READ_CH1_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 97;
break;
case "READ_CH2_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 98;
break;
case "READ_CH3_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 99;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7024
#region 7024R
case "7024R":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x05;
refAddr  = 264;
break;
case "GET_DI_REVERSE":
functionCode = 0x01;
refAddr  = 264;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x03;
refAddr  = 128;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x03;
refAddr  = 129;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x03;
refAddr  = 130;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x03;
refAddr  = 131;
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x03;
refAddr  = 132;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "SET_MODULE_SLEWRATE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_SLEWRATE":
functionCode = 0x46;
refAddr  = 65535;
break;
// AO funciton
case "OUTPUT_CH0_AO":
functionCode = 0x06;
refAddr  = 0;
break;
case "OUTPUT_CH1_AO":
functionCode = 0x06;
refAddr  = 1;
break;
case "OUTPUT_CH2_AO":
functionCode = 0x06;
refAddr  = 2;
break;
case "OUTPUT_CH3_AO":
functionCode = 0x06;
refAddr  = 3;
break;
case "READ_CH0_LAST_AO":
functionCode = 0x03;
refAddr  = 0;
break;
case "READ_CH1_LAST_AO":
functionCode = 0x03;
refAddr  = 1;
break;
case "READ_CH2_LAST_AO":
functionCode = 0x03;
refAddr  = 2;
break;
case "READ_CH3_LAST_AO":
functionCode = 0x03;
refAddr  = 3;
break;
case "READ_CH0_AO":
functionCode = 0x03;
refAddr  = 64;
break;
case "READ_CH1_AO":
functionCode = 0x03;
refAddr  = 65;
break;
case "READ_CH2_AO":
functionCode = 0x03;
refAddr  = 66;
break;
case "READ_CH3_AO":
functionCode = 0x03;
refAddr  = 67;
break;
case "SET_CH0_AO_POWERON":
functionCode = 0x06;
refAddr  = 192;
break;
case "SET_CH1_AO_POWERON":
functionCode = 0x06;
refAddr  = 193;
break;
case "SET_CH2_AO_POWERON":
functionCode = 0x06;
refAddr  = 194;
break;
case "SET_CH3_AO_POWERON":
functionCode = 0x06;
refAddr  = 195;
break;
case "READ_CH0_AO_POWERON":
functionCode = 0x03;
refAddr  = 192;
break;
case "READ_CH1_AO_POWERON":
functionCode = 0x03;
refAddr  = 193;
break;
case "READ_CH2_AO_POWERON":
functionCode = 0x03;
refAddr  = 194;
break;
case "READ_CH3_AO_POWERON":
functionCode = 0x03;
refAddr  = 195;
break;
case "SET_CH0_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 96;
break;
case "SET_CH1_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 97;
break;
case "SET_CH2_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 98;
break;
case "SET_CH3_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 99;
break;
case "READ_CH0_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 96;
break;
case "READ_CH1_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 97;
break;
case "READ_CH2_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 98;
break;
case "READ_CH3_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 99;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7024R
#region 7024U
case "7024U":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_BATTERY_BACKUP_ENABLE_STATUS":
functionCode = 0x0f;
refAddr  = 768;
break;
case "GET_BATTERY_BACKUP_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 768;
break;
case "OUTPUT_CH0_AO":
functionCode = 0x06;
refAddr  = 0;
break;
case "OUTPUT_CH1_AO":
functionCode = 0x06;
refAddr  = 1;
break;
case "OUTPUT_CH2_AO":
functionCode = 0x06;
refAddr  = 2;
break;
case "OUTPUT_CH3_AO":
functionCode = 0x06;
refAddr  = 3;
break;
case "READ_CH0_LAST_AO":
functionCode = 0x03;
refAddr  = 0;
break;
case "READ_CH1_LAST_AO":
functionCode = 0x03;
refAddr  = 1;
break;
case "READ_CH2_LAST_AO":
functionCode = 0x03;
refAddr  = 2;
break;
case "READ_CH3_LAST_AO":
functionCode = 0x03;
refAddr  = 3;
break;
case "READ_CH0_AO":
functionCode = 0x03;
refAddr  = 64;
break;
case "READ_CH1_AO":
functionCode = 0x03;
refAddr  = 65;
break;
case "READ_CH2_AO":
functionCode = 0x03;
refAddr  = 66;
break;
case "READ_CH3_AO":
functionCode = 0x03;
refAddr  = 67;
break;
case "SET_CH0_AO_POWERON":
functionCode = 0x06;
refAddr  = 192;
break;
case "SET_CH1_AO_POWERON":
functionCode = 0x06;
refAddr  = 193;
break;
case "SET_CH2_AO_POWERON":
functionCode = 0x06;
refAddr  = 194;
break;
case "SET_CH3_AO_POWERON":
functionCode = 0x06;
refAddr  = 195;
break;
case "READ_CH0_AO_POWERON":
functionCode = 0x03;
refAddr  = 192;
break;
case "READ_CH1_AO_POWERON":
functionCode = 0x03;
refAddr  = 193;
break;
case "READ_CH2_AO_POWERON":
functionCode = 0x03;
refAddr  = 194;
break;
case "READ_CH3_AO_POWERON":
functionCode = 0x03;
refAddr  = 195;
break;
case "SET_CH0_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 96;
break;
case "SET_CH1_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 97;
break;
case "SET_CH2_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 98;
break;
case "SET_CH3_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 99;
break;
case "READ_CH0_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 96;
break;
case "READ_CH1_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 97;
break;
case "READ_CH2_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 98;
break;
case "READ_CH3_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 99;
break;
case "SET_CH0_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 256;
break;
case "SET_CH0_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 288;
break;
case "SET_CH1_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 257;
break;
case "SET_CH1_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 289;
break;
case "SET_CH2_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 258;
break;
case "SET_CH2_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 290;
break;
case "SET_CH3_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 259;
break;
case "SET_CH3_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 291;
break;
case "GET_CH0_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 256;
break;
case "GET_CH0_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 288;
break;
case "GET_CH1_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 257;
break;
case "GET_CH1_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 289;
break;
case "GET_CH2_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 258;
break;
case "GET_CH2_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 290;
break;
case "GET_CH3_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 259;
break;
case "GET_CH3_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 291;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x05;
refAddr  = 264;
break;
case "GET_DI_REVERSE":
functionCode = 0x01;
refAddr  = 264;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x03;
refAddr  = 128;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x03;
refAddr  = 129;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x03;
refAddr  = 130;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x03;
refAddr  = 131;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "SET_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "READ_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7024U
#region 7028
case "7028":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_BATTERY_BACKUP_ENABLE_STATUS":
functionCode = 0x0f;
refAddr  = 768;
break;
case "GET_BATTERY_BACKUP_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 768;
break;
case "READ_WIRING_STATUS":
functionCode = 0x01;
refAddr  = 224;
break;
case "OUTPUT_CH0_AO":
functionCode = 0x06;
refAddr  = 0;
break;
case "OUTPUT_CH1_AO":
functionCode = 0x06;
refAddr  = 1;
break;
case "OUTPUT_CH2_AO":
functionCode = 0x06;
refAddr  = 2;
break;
case "OUTPUT_CH3_AO":
functionCode = 0x06;
refAddr  = 3;
break;
case "OUTPUT_CH4_AO":
functionCode = 0x06;
refAddr  = 4;
break;
case "OUTPUT_CH5_AO":
functionCode = 0x06;
refAddr  = 5;
break;
case "OUTPUT_CH6_AO":
functionCode = 0x06;
refAddr  = 6;
break;
case "OUTPUT_CH7_AO":
functionCode = 0x06;
refAddr  = 7;
break;
case "READ_CH0_LAST_AO":
functionCode = 0x03;
refAddr  = 0;
break;
case "READ_CH1_LAST_AO":
functionCode = 0x03;
refAddr  = 1;
break;
case "READ_CH2_LAST_AO":
functionCode = 0x03;
refAddr  = 2;
break;
case "READ_CH3_LAST_AO":
functionCode = 0x03;
refAddr  = 3;
break;
case "READ_CH4_LAST_AO":
functionCode = 0x03;
refAddr  = 4;
break;
case "READ_CH5_LAST_AO":
functionCode = 0x03;
refAddr  = 5;
break;
case "READ_CH6_LAST_AO":
functionCode = 0x03;
refAddr  = 6;
break;
case "READ_CH7_LAST_AO":
functionCode = 0x03;
refAddr  = 7;
break;
case "READ_CH0_AO":
functionCode = 0x03;
refAddr  = 64;
break;
case "READ_CH1_AO":
functionCode = 0x03;
refAddr  = 65;
break;
case "READ_CH2_AO":
functionCode = 0x03;
refAddr  = 66;
break;
case "READ_CH3_AO":
functionCode = 0x03;
refAddr  = 67;
break;
case "READ_CH4_AO":
functionCode = 0x03;
refAddr  = 68;
break;
case "READ_CH5_AO":
functionCode = 0x03;
refAddr  = 69;
break;
case "READ_CH6_AO":
functionCode = 0x03;
refAddr  = 70;
break;
case "READ_CH7_AO":
functionCode = 0x03;
refAddr  = 71;
break;
case "SET_CH0_AO_POWERON":
functionCode = 0x06;
refAddr  = 192;
break;
case "SET_CH1_AO_POWERON":
functionCode = 0x06;
refAddr  = 193;
break;
case "SET_CH2_AO_POWERON":
functionCode = 0x06;
refAddr  = 194;
break;
case "SET_CH3_AO_POWERON":
functionCode = 0x06;
refAddr  = 195;
break;
case "SET_CH4_AO_POWERON":
functionCode = 0x06;
refAddr  = 196;
break;
case "SET_CH5_AO_POWERON":
functionCode = 0x06;
refAddr  = 197;
break;
case "SET_CH6_AO_POWERON":
functionCode = 0x06;
refAddr  = 198;
break;
case "SET_CH7_AO_POWERON":
functionCode = 0x06;
refAddr  = 199;
break;
case "READ_CH0_AO_POWERON":
functionCode = 0x03;
refAddr  = 192;
break;
case "READ_CH1_AO_POWERON":
functionCode = 0x03;
refAddr  = 193;
break;
case "READ_CH2_AO_POWERON":
functionCode = 0x03;
refAddr  = 194;
break;
case "READ_CH3_AO_POWERON":
functionCode = 0x03;
refAddr  = 195;
break;
case "READ_CH4_AO_POWERON":
functionCode = 0x03;
refAddr  = 196;
break;
case "READ_CH5_AO_POWERON":
functionCode = 0x03;
refAddr  = 197;
break;
case "READ_CH6_AO_POWERON":
functionCode = 0x03;
refAddr  = 198;
break;
case "READ_CH7_AO_POWERON":
functionCode = 0x03;
refAddr  = 199;
break;
case "SET_CH0_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 96;
break;
case "SET_CH1_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 97;
break;
case "SET_CH2_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 98;
break;
case "SET_CH3_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 99;
break;
case "SET_CH4_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 100;
break;
case "SET_CH5_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 101;
break;
case "SET_CH6_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 102;
break;
case "SET_CH7_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 103;
break;
case "READ_CH0_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 96;
break;
case "READ_CH1_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 97;
break;
case "READ_CH2_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 98;
break;
case "READ_CH3_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 99;
break;
case "READ_CH4_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 100;
break;
case "READ_CH5_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 101;
break;
case "READ_CH6_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 102;
break;
case "READ_CH7_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 103;
break;
case "SET_CH0_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 256;
break;
case "SET_CH0_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 288;
break;
case "SET_CH1_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 257;
break;
case "SET_CH1_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 289;
break;
case "SET_CH2_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 258;
break;
case "SET_CH2_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 290;
break;
case "SET_CH3_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 259;
break;
case "SET_CH3_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 291;
break;
case "SET_CH4_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 260;
break;
case "SET_CH4_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 292;
break;
case "SET_CH5_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 261;
break;
case "SET_CH5_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 293;
break;
case "SET_CH6_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 262;
break;
case "SET_CH6_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 294;
break;
case "SET_CH7_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 263;
break;
case "SET_CH7_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 295;
break;
case "GET_CH0_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 256;
break;
case "GET_CH0_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 288;
break;
case "GET_CH1_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 257;
break;
case "GET_CH1_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 289;
break;
case "GET_CH2_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 258;
break;
case "GET_CH2_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 290;
break;
case "GET_CH3_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 259;
break;
case "GET_CH3_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 291;
break;
case "GET_CH4_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 260;
break;
case "GET_CH4_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 288;
break;
case "GET_CH5_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 261;
break;
case "GET_CH5_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 289;
break;
case "GET_CH6_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 262;
break;
case "GET_CH6_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 290;
break;
case "GET_CH7_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 263;
break;
case "GET_CH7_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 291;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7028
#region 7026
case "7026":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x06;
refAddr  = 487;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x03;
refAddr  = 487;
break;
case "SET_DI_REVERSE":
functionCode = 0x05;
refAddr  = 264;
break;
case "GET_DI_REVERSE":
functionCode = 0x01;
refAddr  = 264;
break;
// AI function
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 256;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 257;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 258;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 259;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
//AI Alarm function
case "READ_CH0_AI_ALARM_MODE":
functionCode = 0x01;
refAddr  = 336;
break;
case "READ_CH1_AI_ALARM_MODE":
functionCode = 0x01;
refAddr  = 337;
break;
case "READ_CH2_AI_ALARM_MODE":
functionCode = 0x01;
refAddr  = 338;
break;
case "SET_CH0_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 320;
break;
case "SET_CH1_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 321;
break;
case "SET_CH2_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 322;
break;
case "SET_CH0_ENABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 320;
break;
case "SET_CH1_ENABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 321;
break;
case "SET_CH2_ENABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 322;
break;
case "SET_CH0_AI_ALARM_MODE":
functionCode = 0x05;
refAddr  = 336;
break;
case "SET_CH1_AI_ALARM_MODE":
functionCode = 0x05;
refAddr  = 337;
break;
case "SET_CH2_AI_ALARM_MODE":
functionCode = 0x05;
refAddr  = 338;
break;
case "GET_CH0_AI_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 320;
break;
case "GET_CH1_AI_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 321;
break;
case "GET_CH2_AI_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 322;
break;
case "SET_CH0_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 224;
break;
case "SET_CH1_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 225;
break;
case "SET_CH2_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 226;
break;
case "SET_CH0_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 232;
break;
case "SET_CH1_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 233;
break;
case "SET_CH2_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 234;
break;
case "GET_CH0_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 232;
break;
case "GET_CH1_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 233;
break;
case "GET_CH2_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 234;
break;
case "GET_CH0_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 224;
break;
case "GET_CH1_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 225;
break;
case "GET_CH2_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 226;
break;
case "READ_SIMPLE_AI_ALARM_STATUS":
functionCode = 0x01;
refAddr  = 261;
break;
case "READ_LOW_ALARM_DO_ON":
functionCode = 0x01;
refAddr  = 288;
break;
case "READ_HIGH_ALARM_DO_ON":
functionCode = 0x01;
refAddr  = 304;
break;
case "CLEAR_CH0_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 304;
break;
case "CLEAR_CH1_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 305;
break;
case "CLEAR_CH2_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 306;
break;
case "CLEAR_CH0_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 288;
break;
case "CLEAR_CH1_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 289;
break;
case "CLEAR_CH2_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 290;
break;
// DI function
case "READ_SIMPLE_AI_DI_STATUS":
functionCode = 0x02;
refAddr  = 32;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x03;
refAddr  = 128;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x03;
refAddr  = 129;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x03;
refAddr  = 130;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
// DO function
case "READ_SIMPLE_AI_DO_STATUS":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_MFDO":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_MFDO":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_MFDO":
functionCode = 0x05;
refAddr  = 2;
break;
case "SET_MF_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "READ_MF_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
case "SET_MF_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_MF_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// AO function
case "SET_CH0_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 416;
break;
case "SET_CH1_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 417;
break;
case "SET_CH0_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 288;
break;
case "SET_CH1_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 289;
break;
case "GET_CH0_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 416;
break;
case "GET_CH1_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 417;
break;
case "GET_CH0_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 288;
break;
case "GET_CH1_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 289;
break;
case "READ_CH0_LAST_AO":
functionCode = 0x03;
refAddr  = 32;
break;
case "READ_CH1_LAST_AO":
functionCode = 0x03;
refAddr  = 33;
break;
case "READ_CH0_AO":
functionCode = 0x03;
refAddr  = 64;
break;
case "READ_CH1_AO":
functionCode = 0x03;
refAddr  = 65;
break;
case "OUTPUT_CH0_AO":
functionCode = 0x06;
refAddr  = 32;
break;
case "OUTPUT_CH1_AO":
functionCode = 0x06;
refAddr  = 33;
break;
case "READ_CH0_AO_POWERON":
functionCode = 0x03;
refAddr  = 192;
break;
case "READ_CH1_AO_POWERON":
functionCode = 0x03;
refAddr  = 193;
break;
case "READ_CH0_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 96;
break;
case "READ_CH1_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 97;
break;
case "SET_CH0_AO_POWERON":
functionCode = 0x06;
refAddr  = 192;
break;
case "SET_CH1_AO_POWERON":
functionCode = 0x06;
refAddr  = 193;
break;
case "SET_CH0_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 96;
break;
case "SET_CH1_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 97;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7026
#region 7033
case "7033":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS": //M-7033 does not support this command
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
}
break;
#endregion 7033
#region 7041 14DI
case "7041":
case "7041D":
case "7041P":
case "7041PD":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 256;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_DI_COUNTER":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_DI_COUNTER":
functionCode = 0x04;
refAddr  = 7;
break;
case "READ_CH8_DI_COUNTER":
functionCode = 0x04;
refAddr  = 8;
break;
case "READ_CH9_DI_COUNTER":
functionCode = 0x04;
refAddr  = 9;
break;
case "READ_CH10_DI_COUNTER":
functionCode = 0x04;
refAddr  = 10;
break;
case "READ_CH11_DI_COUNTER":
functionCode = 0x04;
refAddr  = 11;
break;
case "READ_CH12_DI_COUNTER":
functionCode = 0x04;
refAddr  = 12;
break;
case "READ_CH13_DI_COUNTER":
functionCode = 0x04;
refAddr  = 13;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "CLEAR_CH6_DI_COUNTER":
functionCode = 0x05;
refAddr  = 518;
break;
case "CLEAR_CH7_DI_COUNTER":
functionCode = 0x05;
refAddr  = 519;
break;
case "CLEAR_CH8_DI_COUNTER":
functionCode = 0x05;
refAddr  = 520;
break;
case "CLEAR_CH9_DI_COUNTER":
functionCode = 0x05;
refAddr  = 521;
break;
case "CLEAR_CH10_DI_COUNTER":
functionCode = 0x05;
refAddr  = 522;
break;
case "CLEAR_CH11_DI_COUNTER":
functionCode = 0x05;
refAddr  = 523;
break;
case "CLEAR_CH12_DI_COUNTER":
functionCode = 0x05;
refAddr  = 524;
break;
case "CLEAR_CH13_DI_COUNTER":
functionCode = 0x05;
refAddr  = 525;
break;
}
break;
#endregion 7041
#region 7042 13 DO
case "7042":
case "7042D":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH5_DO_BIT":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_DO_BIT":
functionCode = 0x05;
refAddr  = 6;
break;
case "OUTPUT_CH7_DO_BIT":
functionCode = 0x05;
refAddr  = 7;
break;
case "OUTPUT_CH8_DO_BIT":
functionCode = 0x05;
refAddr  = 8;
break;
case "OUTPUT_CH9_DO_BIT":
functionCode = 0x05;
refAddr  = 9;
break;
case "OUTPUT_CH10_DO_BIT":
functionCode = 0x05;
refAddr  = 10;
break;
case "OUTPUT_CH11_DO_BIT":
functionCode = 0x05;
refAddr  = 11;
break;
case "OUTPUT_CH12_DO_BIT":
functionCode = 0x05;
refAddr  = 12;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7042
#region 7043/7045 16 DO
case "7043":
case "7043D":
case "7045":
case "7045D":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH5_DO_BIT":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_DO_BIT":
functionCode = 0x05;
refAddr  = 6;
break;
case "OUTPUT_CH7_DO_BIT":
functionCode = 0x05;
refAddr  = 7;
break;
case "OUTPUT_CH8_DO_BIT":
functionCode = 0x05;
refAddr  = 8;
break;
case "OUTPUT_CH9_DO_BIT":
functionCode = 0x05;
refAddr  = 9;
break;
case "OUTPUT_CH10_DO_BIT":
functionCode = 0x05;
refAddr  = 10;
break;
case "OUTPUT_CH11_DO_BIT":
functionCode = 0x05;
refAddr  = 11;
break;
case "OUTPUT_CH12_DO_BIT":
functionCode = 0x05;
refAddr  = 12;
break;
case "OUTPUT_CH13_DO_BIT":
functionCode = 0x05;
refAddr  = 13;
break;
case "OUTPUT_CH14_DO_BIT":
functionCode = 0x05;
refAddr  = 14;
break;
case "OUTPUT_CH15_DO_BIT":
functionCode = 0x05;
refAddr  = 15;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7043/7045
#region 7044 4DI/8DO
case "7044":
case "7044D":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 256;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
refAddr  = 3;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH5_DO_BIT":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_DO_BIT":
functionCode = 0x05;
refAddr  = 6;
break;
case "OUTPUT_CH7_DO_BIT":
functionCode = 0x05;
refAddr  = 7;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7044
#region 7050 7DI/8DO
case "7050":
case "7050D":
case "7050A":
case "7050AD":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 256;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_DI_COUNTER":
functionCode = 0x04;
refAddr  = 6;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "CLEAR_CH6_DI_COUNTER":
functionCode = 0x05;
refAddr  = 518;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH5_DO_BIT":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_DO_BIT":
functionCode = 0x05;
refAddr  = 6;
break;
case "OUTPUT_CH7_DO_BIT":
functionCode = 0x05;
refAddr  = 7;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7050
#region 7051/7053 16DI
case "7046":
case "7051":
case "7051D":
case "7053":
case "7053D":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_FILTER_WIDTH":
functionCode = 0x06;
refAddr  = 497;
break;
case "GET_FILTER_WIDTH":
functionCode = 0x03;
refAddr  = 497;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 256;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_DI_COUNTER":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_DI_COUNTER":
functionCode = 0x04;
refAddr  = 7;
break;
case "READ_CH8_DI_COUNTER":
functionCode = 0x04;
refAddr  = 8;
break;
case "READ_CH9_DI_COUNTER":
functionCode = 0x04;
refAddr  = 9;
break;
case "READ_CH10_DI_COUNTER":
functionCode = 0x04;
refAddr  = 10;
break;
case "READ_CH11_DI_COUNTER":
functionCode = 0x04;
refAddr  = 11;
break;
case "READ_CH12_DI_COUNTER":
functionCode = 0x04;
refAddr  = 12;
break;
case "READ_CH13_DI_COUNTER":
functionCode = 0x04;
refAddr  = 13;
break;
case "READ_CH14_DI_COUNTER":
functionCode = 0x04;
refAddr  = 14;
break;
case "READ_CH15_DI_COUNTER":
functionCode = 0x04;
refAddr  = 15;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "CLEAR_CH6_DI_COUNTER":
functionCode = 0x05;
refAddr  = 518;
break;
case "CLEAR_CH7_DI_COUNTER":
functionCode = 0x05;
refAddr  = 519;
break;
case "CLEAR_CH8_DI_COUNTER":
functionCode = 0x05;
refAddr  = 520;
break;
case "CLEAR_CH9_DI_COUNTER":
functionCode = 0x05;
refAddr  = 521;
break;
case "CLEAR_CH10_DI_COUNTER":
functionCode = 0x05;
refAddr  = 522;
break;
case "CLEAR_CH11_DI_COUNTER":
functionCode = 0x05;
refAddr  = 523;
break;
case "CLEAR_CH12_DI_COUNTER":
functionCode = 0x05;
refAddr  = 524;
break;
case "CLEAR_CH13_DI_COUNTER":
functionCode = 0x05;
refAddr  = 525;
break;
case "CLEAR_CH14_DI_COUNTER":
functionCode = 0x05;
refAddr  = 526;
break;
case "CLEAR_CH15_DI_COUNTER":
functionCode = 0x05;
refAddr  = 527;
break;
}
break;
#endregion 7051/7053
#region 7052/7058/7059 8DI
case "7052":
case "7052D":
case "7058":
case "7058D":
case "7059":
case "7059D":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 256;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_DI_COUNTER":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_DI_COUNTER":
functionCode = 0x04;
refAddr  = 7;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "CLEAR_CH6_DI_COUNTER":
functionCode = 0x05;
refAddr  = 518;
break;
case "CLEAR_CH7_DI_COUNTER":
functionCode = 0x05;
refAddr  = 519;
break;
}
break;
#endregion 7052
#region 7054 16 universal DI/DO
case "7054":
case "7054D":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 256;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_DI_COUNTER":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_DI_COUNTER":
functionCode = 0x04;
refAddr  = 7;
break;
case "READ_CH8_DI_COUNTER":
functionCode = 0x04;
refAddr  = 8;
break;
case "READ_CH9_DI_COUNTER":
functionCode = 0x04;
refAddr  = 9;
break;
case "READ_CH10_DI_COUNTER":
functionCode = 0x04;
refAddr  = 10;
break;
case "READ_CH11_DI_COUNTER":
functionCode = 0x04;
refAddr  = 11;
break;
case "READ_CH12_DI_COUNTER":
functionCode = 0x04;
refAddr  = 12;
break;
case "READ_CH13_DI_COUNTER":
functionCode = 0x04;
refAddr  = 13;
break;
case "READ_CH14_DI_COUNTER":
functionCode = 0x04;
refAddr  = 14;
break;
case "READ_CH15_DI_COUNTER":
functionCode = 0x04;
refAddr  = 15;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "CLEAR_CH6_DI_COUNTER":
functionCode = 0x05;
refAddr  = 518;
break;
case "CLEAR_CH7_DI_COUNTER":
functionCode = 0x05;
refAddr  = 519;
break;
case "CLEAR_CH8_DI_COUNTER":
functionCode = 0x05;
refAddr  = 520;
break;
case "CLEAR_CH9_DI_COUNTER":
functionCode = 0x05;
refAddr  = 521;
break;
case "CLEAR_CH10_DI_COUNTER":
functionCode = 0x05;
refAddr  = 522;
break;
case "CLEAR_CH11_DI_COUNTER":
functionCode = 0x05;
refAddr  = 523;
break;
case "CLEAR_CH12_DI_COUNTER":
functionCode = 0x05;
refAddr  = 524;
break;
case "CLEAR_CH13_DI_COUNTER":
functionCode = 0x05;
refAddr  = 525;
break;
case "CLEAR_CH14_DI_COUNTER":
functionCode = 0x05;
refAddr  = 526;
break;
case "CLEAR_CH15_DI_COUNTER":
functionCode = 0x05;
refAddr  = 527;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH5_DO_BIT":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_DO_BIT":
functionCode = 0x05;
refAddr  = 6;
break;
case "OUTPUT_CH7_DO_BIT":
functionCode = 0x05;
refAddr  = 7;
break;
case "OUTPUT_CH8_DO_BIT":
functionCode = 0x05;
refAddr  = 8;
break;
case "OUTPUT_CH9_DO_BIT":
functionCode = 0x05;
refAddr  = 9;
break;
case "OUTPUT_CH10_DO_BIT":
functionCode = 0x05;
refAddr  = 10;
break;
case "OUTPUT_CH11_DO_BIT":
functionCode = 0x05;
refAddr  = 11;
break;
case "OUTPUT_CH12_DO_BIT":
functionCode = 0x05;
refAddr  = 12;
break;
case "OUTPUT_CH13_DO_BIT":
functionCode = 0x05;
refAddr  = 13;
break;
case "OUTPUT_CH14_DO_BIT":
functionCode = 0x05;
refAddr  = 14;
break;
case "OUTPUT_CH15_DO_BIT":
functionCode = 0x05;
refAddr  = 15;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7055
#region 7055 8DI/8DO
case "7055":
case "7055D":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 256;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_DI_COUNTER":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_DI_COUNTER":
functionCode = 0x04;
refAddr  = 7;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "CLEAR_CH6_DI_COUNTER":
functionCode = 0x05;
refAddr  = 518;
break;
case "CLEAR_CH7_DI_COUNTER":
functionCode = 0x05;
refAddr  = 519;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH5_DO_BIT":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_DO_BIT":
functionCode = 0x05;
refAddr  = 6;
break;
case "OUTPUT_CH7_DO_BIT":
functionCode = 0x05;
refAddr  = 7;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7055
#region 7060 4DI/4DO
case "7060":
case "7060D":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 256;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
refAddr  = 3;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
// DO function
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7060
#region 7061 12DO
case "7061":
case "7061D":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH5_DO_BIT":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_DO_BIT":
functionCode = 0x05;
refAddr  = 6;
break;
case "OUTPUT_CH7_DO_BIT":
functionCode = 0x05;
refAddr  = 7;
break;
case "OUTPUT_CH8_DO_BIT":
functionCode = 0x05;
refAddr  = 8;
break;
case "OUTPUT_CH9_DO_BIT":
functionCode = 0x05;
refAddr  = 9;
break;
case "OUTPUT_CH10_DO_BIT":
functionCode = 0x05;
refAddr  = 10;
break;
case "OUTPUT_CH11_DO_BIT":
functionCode = 0x05;
refAddr  = 11;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7061
#region 7063 8DI/3DO
case "7063":
case "7063D":
case "7063A":
case "7063AD":
case "7063B":
case "7063BD":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 256;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_DI_COUNTER":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_DI_COUNTER":
functionCode = 0x04;
refAddr  = 7;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "CLEAR_CH6_DI_COUNTER":
functionCode = 0x05;
refAddr  = 518;
break;
case "CLEAR_CH7_DI_COUNTER":
functionCode = 0x05;
refAddr  = 519;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7063
#region 7065 4DI/5DO
case "7065":
case "7065D":
case "7065A":
case "7065AD":
case "7065B":
case "7065BD":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 256;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
refAddr  = 3;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
// DO function
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7065
#region 7066/7066P/7067 7DO
case "7066":
case "7066D":
case "7066P":
case "7066PD":
case "7067":
case "7067D":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH5_DO_BIT":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_DO_BIT":
functionCode = 0x05;
refAddr  = 6;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7067
#region 7068/7069 8DO
case "7068":
case "7068D":
case "7069":
case "7069D":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH5_DO_BIT":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_DO_BIT":
functionCode = 0x05;
refAddr  = 6;
break;
case "OUTPUT_CH7_DO_BIT":
functionCode = 0x05;
refAddr  = 7;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7068/7069
#region 7080/7080D/7080B/7080BD
case "7080":
case "7080D":
case "7080B":
case "7080BD":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_GATE_MODE":
functionCode = 0x0F;
refAddr  = 130;
break;
case "GET_GATE_MODE":
functionCode = 0x01;
refAddr  = 130;
break;
case "SET_CH0_INPUT_SIGNAL":
functionCode = 0x0F;
refAddr  = 128;
break;
case "SET_CH1_INPUT_SIGNAL":
functionCode = 0x0F;
refAddr  = 128;
break;
case "GET_CH0_INPUT_SIGNAL":
functionCode = 0x02;
refAddr  = 128;
break;
case "GET_CH1_INPUT_SIGNAL":
functionCode = 0x02;
refAddr  = 128;
break;
case "SET_CH0_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 96;
break;
case "SET_CH1_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 98;
break;
case "GET_CH0_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 96;
break;
case "GET_CH1_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 98;
break;
case "SET_CH0_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 64;
break;
case "SET_CH1_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 66;
break;
case "GET_CH0_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 64;
break;
case "GET_CH1_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 66;
break;
case "SET_CH0_COUNTER_ENABLE_STATUS":
functionCode = 0x05;
refAddr  = 134;
break;
case "SET_CH1_COUNTER_ENABLE_STATUS":
functionCode = 0x05;
refAddr  = 135;
break;
case "GET_CH0_COUNTER_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 134;
break;
case "GET_CH1_COUNTER_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 135;
break;
case "SET_LOW_PASS_FILTER_ENABLE_STATUS":
functionCode = 0x05;
refAddr  = 138;
break;
case "GET_LOW_PASS_FILTER_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 138;
break;
case "GET_LOW_TRIGGER_LEVEL_VOLTAGE":
functionCode = 0x03;
refAddr  = 162;
break;
case "GET_HIGH_TRIGGER_LEVEL_VOLTAGE":
functionCode = 0x03;
refAddr  = 163;
break;
case "SET_LOW_SIGNAL_WIDTH":
functionCode = 0x06;
refAddr  = 160;
break;
case "GET_LOW_SIGNAL_WIDTH":
functionCode = 0x03;
refAddr  = 160;
break;
case "SET_HIGH_SIGNAL_WIDTH":
functionCode = 0x06;
refAddr  = 161;
break;
case "GET_HIGH_SIGNAL_WIDTH":
functionCode = 0x03;
refAddr  = 161;
break;
case "SET_CF_ALARM_MODE":
functionCode = 0x05;
refAddr  = 139;
break;
case "GET_CF_ALARM_MODE":
functionCode = 0x01;
refAddr  = 139;
break;
case "READ_CF_ALARM_DO_STATUS":
functionCode = 0x01;
refAddr  = 0;
break;
case "SET_CF_ALARM_MODE_STATUS":
functionCode = 0x0F;
refAddr  = 136;
break;
case "READ_CF_ALARM_MODE_STATUS":
functionCode = 0x01;
refAddr  = 136;
break;
case "SET_CH0_CF_ALARM_VALUE":
functionCode = 0x10;
refAddr  = 128;
break;
case "SET_CH1_CF_ALARM_VALUE":
functionCode = 0x10;
refAddr  = 130;
break;
case "GET_CH0_CF_ALARM_VALUE":
functionCode = 0x03;
refAddr  = 128;
break;
case "GET_CH1_CF_ALARM_VALUE":
functionCode = 0x03;
refAddr  = 130;
break;
case "READ_CH0_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 0;
break;
case "READ_CH1_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 2;
break;
case "CLEAR_CH0_COUNTER":
functionCode = 0x05;
refAddr  = 132;
break;
case "CLEAR_CH1_COUNTER":
functionCode = 0x05;
refAddr  = 133;
break;
case "SET_HIGH_TRIGGER_LEVEL_VOLTAGE":
functionCode = 0x06;
refAddr  = 163;
break;
case "SET_LOW_TRIGGER_LEVEL_VOLTAGE":
functionCode = 0x06;
refAddr  = 162;
break;
case "SET_CF_DO_OUTPUT":
functionCode = 0x0F;
refAddr  = 0;
break;
case "SET_LED_MODE":
functionCode = 0x05;
refAddr  = 142;
break;
case "GET_LED_MODE":
functionCode = 0x01;
refAddr  = 142;
break;
//7080 WDT ??ˆå??????????è±???
//// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7080D
#region 7084
case "7084":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COUNTER_ENABLE_STATUS":
functionCode = 0x06;
refAddr  = 489;
break;
case "GET_COUNTER_ENABLE_STATUS":
functionCode = 0x03;
refAddr  = 489;
break;
case "SET_CF_XOR":
functionCode = 0x0F;
refAddr  = 928;
break;
case "GET_CF_XOR":
functionCode = 0x01;
refAddr  = 928;
break;
case "SET_FREQ_TIMEOUT":
functionCode = 0x06;
refAddr  = 160;
break;
case "GET_FREQ_TIMEOUT":
functionCode = 0x03;
refAddr  = 160;
break;
case "SET_FREQ_MODE":
functionCode = 0x0F;
refAddr  = 832;
break;
case "GET_FREQ_MODE":
functionCode = 0x01;
refAddr  = 832;
break;
case "SET_STOP_ON_OVERFLOW":
functionCode = 0x0F;
refAddr  = 864;
break;
case "GET_STOP_ON_OVERFLOW":
functionCode = 0x01;
refAddr  = 864;
break;
case "SET_LOW_PASS_FILTER_ENABLE_STATUS":
functionCode = 0x0F;
refAddr  = 896;
break;
case "GET_LOW_PASS_FILTER_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 896;
break;
case "SET_BATTERY_BACKUP_ENABLE_STATUS":
functionCode = 0x0F;
refAddr  = 768;
break;
case "GET_BATTERY_BACKUP_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 768;
break;
case "SET_CH0_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 96;
break;
case "SET_CH1_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 98;
break;
case "SET_CH2_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 100;
break;
case "SET_CH3_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 102;
break;
case "SET_CH4_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 104;
break;
case "SET_CH5_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 106;
break;
case "SET_CH6_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 108;
break;
case "SET_CH7_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 110;
break;
case "GET_CH0_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 96;
break;
case "GET_CH1_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 98;
break;
case "GET_CH2_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 100;
break;
case "GET_CH3_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 102;
break;
case "GET_CH4_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 104;
break;
case "GET_CH5_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 106;
break;
case "GET_CH6_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 108;
break;
case "GET_CH7_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 110;
break;
case "SET_CH0_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 64;
break;
case "SET_CH1_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 66;
break;
case "SET_CH2_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 68;
break;
case "SET_CH3_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 70;
break;
case "SET_CH4_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 72;
break;
case "SET_CH5_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 74;
break;
case "SET_CH6_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 76;
break;
case "SET_CH7_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 78;
break;
case "GET_CH0_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 64;
break;
case "GET_CH1_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 66;
break;
case "GET_CH2_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 68;
break;
case "GET_CH3_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 70;
break;
case "GET_CH4_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 72;
break;
case "GET_CH5_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 74;
break;
case "GET_CH6_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 76;
break;
case "GET_CH7_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 78;
break;
case "SET_CH0_LOW_PASS_FILTER":
functionCode = 0x06;
refAddr  = 161;
break;
case "SET_CH1_LOW_PASS_FILTER":
functionCode = 0x06;
refAddr  = 161;
break;
case "SET_CH2_LOW_PASS_FILTER":
functionCode = 0x06;
refAddr  = 162;
break;
case "SET_CH3_LOW_PASS_FILTER":
functionCode = 0x06;
refAddr  = 162;
break;
case "SET_CH4_LOW_PASS_FILTER":
functionCode = 0x06;
refAddr  = 163;
break;
case "SET_CH5_LOW_PASS_FILTER":
functionCode = 0x06;
refAddr  = 163;
break;
case "SET_CH6_LOW_PASS_FILTER":
functionCode = 0x06;
refAddr  = 163;
break;
case "SET_CH7_LOW_PASS_FILTER":
functionCode = 0x06;
refAddr  = 163;
break;
case "GET_CH0_LOW_PASS_FILTER":
functionCode = 0x03;
refAddr  = 161;
break;
case "GET_CH1_LOW_PASS_FILTER":
functionCode = 0x03;
refAddr  = 161;
break;
case "GET_CH2_LOW_PASS_FILTER":
functionCode = 0x03;
refAddr  = 162;
break;
case "GET_CH3_LOW_PASS_FILTER":
functionCode = 0x03;
refAddr  = 162;
break;
case "GET_CH4_LOW_PASS_FILTER":
functionCode = 0x03;
refAddr  = 163;
break;
case "GET_CH5_LOW_PASS_FILTER":
functionCode = 0x03;
refAddr  = 163;
break;
case "GET_CH6_LOW_PASS_FILTER":
functionCode = 0x03;
refAddr  = 163;
break;
case "GET_CH7_LOW_PASS_FILTER":
functionCode = 0x03;
refAddr  = 163;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI
case "READ_CF_DI_XOR":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_CF_DI_LOW_PASS_FILTER":
functionCode = 0x01;
refAddr  = 40;
break;
// Counter
case "READ_CH0_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 0;
break;
case "READ_CH1_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 2;
break;
case "READ_CH2_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 4;
break;
case "READ_CH3_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 6;
break;
case "READ_CH4_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 8;
break;
case "READ_CH5_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 10;
break;
case "READ_CH6_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 12;
break;
case "READ_CH7_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 14;
break;
case "READ_COUNTER_OVERFLOW":
functionCode = 0x01;
refAddr  = 64;
break;
}
break;
#endregion 7084
#region 7088
case "7088":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DI_REVERSE":
functionCode = 0x05;
refAddr  = 277;
break;
case "GET_DI_REVERSE":
functionCode = 0x01;
refAddr  = 277;
break;
case "SET_CH0_PWM_FREQ":
functionCode = 0x10;
refAddr  = 736;
break;
case "GET_CH0_PWM_FREQ":
functionCode = 0x03;
refAddr  = 736;
break;
case "SET_CH1_PWM_FREQ":
functionCode = 0x10;
refAddr  = 738;
break;
case "GET_CH1_PWM_FREQ":
functionCode = 0x03;
refAddr  = 738;
break;
case "SET_CH2_PWM_FREQ":
functionCode = 0x10;
refAddr  = 740;
break;
case "GET_CH2_PWM_FREQ":
functionCode = 0x03;
refAddr  = 740;
break;
case "SET_CH3_PWM_FREQ":
functionCode = 0x10;
refAddr  = 742;
break;
case "GET_CH3_PWM_FREQ":
functionCode = 0x03;
refAddr  = 742;
break;
case "SET_CH4_PWM_FREQ":
functionCode = 0x10;
refAddr  = 744;
break;
case "GET_CH4_PWM_FREQ":
functionCode = 0x03;
refAddr  = 744;
break;
case "SET_CH5_PWM_FREQ":
functionCode = 0x10;
refAddr  = 746;
break;
case "GET_CH5_PWM_FREQ":
functionCode = 0x03;
refAddr  = 746;
break;
case "SET_CH6_PWM_FREQ":
functionCode = 0x10;
refAddr  = 748;
break;
case "GET_CH6_PWM_FREQ":
functionCode = 0x03;
refAddr  = 748;
break;
case "SET_CH7_PWM_FREQ":
functionCode = 0x10;
refAddr  = 750;
break;
case "GET_CH7_PWM_FREQ":
functionCode = 0x03;
refAddr  = 750;
break;
case "SET_CH0_PWM_DUTY":
functionCode = 0x06;
refAddr  = 704;
break;
case "GET_CH0_PWM_DUTY":
functionCode = 0x03;
refAddr  = 704;
break;
case "SET_CH1_PWM_DUTY":
functionCode = 0x06;
refAddr  = 705;
break;
case "GET_CH1_PWM_DUTY":
functionCode = 0x03;
refAddr  = 705;
break;
case "SET_CH2_PWM_DUTY":
functionCode = 0x06;
refAddr  = 706;
break;
case "GET_CH2_PWM_DUTY":
functionCode = 0x03;
refAddr  = 706;
break;
case "SET_CH3_PWM_DUTY":
functionCode = 0x06;
refAddr  = 707;
break;
case "GET_CH3_PWM_DUTY":
functionCode = 0x03;
refAddr  = 707;
break;
case "SET_CH4_PWM_DUTY":
functionCode = 0x06;
refAddr  = 708;
break;
case "GET_CH4_PWM_DUTY":
functionCode = 0x03;
refAddr  = 708;
break;
case "SET_CH5_PWM_DUTY":
functionCode = 0x06;
refAddr  = 709;
break;
case "GET_CH5_PWM_DUTY":
functionCode = 0x03;
refAddr  = 709;
break;
case "SET_CH6_PWM_DUTY":
functionCode = 0x06;
refAddr  = 710;
break;
case "GET_CH6_PWM_DUTY":
functionCode = 0x03;
refAddr  = 710;
break;
case "SET_CH7_PWM_DUTY":
functionCode = 0x06;
refAddr  = 711;
break;
case "GET_CH7_PWM_DUTY":
functionCode = 0x03;
refAddr  = 711;
break;
case "SET_CH0_PULSE_COUNT":
functionCode = 0x06;
refAddr  = 800;
break;
case "GET_CH0_PULSE_COUNT":
functionCode = 0x03;
refAddr  = 800;
break;
case "SET_CH1_PULSE_COUNT":
functionCode = 0x06;
refAddr  = 801;
break;
case "GET_CH1_PULSE_COUNT":
functionCode = 0x03;
refAddr  = 801;
break;
case "SET_CH2_PULSE_COUNT":
functionCode = 0x06;
refAddr  = 802;
break;
case "GET_CH2_PULSE_COUNT":
functionCode = 0x03;
refAddr  = 802;
break;
case "SET_CH3_PULSE_COUNT":
functionCode = 0x06;
refAddr  = 803;
break;
case "GET_CH3_PULSE_COUNT":
functionCode = 0x03;
refAddr  = 803;
break;
case "SET_CH4_PULSE_COUNT":
functionCode = 0x06;
refAddr  = 804;
break;
case "GET_CH4_PULSE_COUNT":
functionCode = 0x03;
refAddr  = 804;
break;
case "SET_CH5_PULSE_COUNT":
functionCode = 0x06;
refAddr  = 805;
break;
case "GET_CH5_PULSE_COUNT":
functionCode = 0x03;
refAddr  = 805;
break;
case "SET_CH6_PULSE_COUNT":
functionCode = 0x06;
refAddr  = 806;
break;
case "GET_CH6_PULSE_COUNT":
functionCode = 0x03;
refAddr  = 806;
break;
case "SET_CH7_PULSE_COUNT":
functionCode = 0x06;
refAddr  = 807;
break;
case "GET_CH7_PULSE_COUNT":
functionCode = 0x03;
refAddr  = 807;
break;
case "SET_CH0_PWM_PULSE_MODE":
functionCode = 0x05;
refAddr  = 864;
break;
case "GET_CH0_PWM_PULSE_MODE":
functionCode = 0x01;
refAddr  = 864;
break;
case "SET_CH1_PWM_PULSE_MODE":
functionCode = 0x05;
refAddr  = 865;
break;
case "GET_CH1_PWM_PULSE_MODE":
functionCode = 0x01;
refAddr  = 865;
break;
case "SET_CH2_PWM_PULSE_MODE":
functionCode = 0x05;
refAddr  = 866;
break;
case "GET_CH2_PWM_PULSE_MODE":
functionCode = 0x01;
refAddr  = 866;
break;
case "SET_CH3_PWM_PULSE_MODE":
functionCode = 0x05;
refAddr  = 867;
break;
case "GET_CH3_PWM_PULSE_MODE":
functionCode = 0x01;
refAddr  = 867;
break;
case "SET_CH4_PWM_PULSE_MODE":
functionCode = 0x05;
refAddr  = 868;
break;
case "GET_CH4_PWM_PULSE_MODE":
functionCode = 0x01;
refAddr  = 868;
break;
case "SET_CH5_PWM_PULSE_MODE":
functionCode = 0x05;
refAddr  = 869;
break;
case "GET_CH5_PWM_PULSE_MODE":
functionCode = 0x01;
refAddr  = 869;
break;
case "SET_CH6_PWM_PULSE_MODE":
functionCode = 0x05;
refAddr  = 870;
break;
case "GET_CH6_PWM_PULSE_MODE":
functionCode = 0x01;
refAddr  = 870;
break;
case "SET_CH7_PWM_PULSE_MODE":
functionCode = 0x05;
refAddr  = 871;
break;
case "GET_CH7_PWM_PULSE_MODE":
functionCode = 0x01;
refAddr  = 871;
break;
case "SET_CH0_SYNC_PWM":
functionCode = 0x05;
refAddr  = 960;
break;
case "GET_CH0_SYNC_PWM":
functionCode = 0x01;
refAddr  = 960;
break;
case "SET_CH1_SYNC_PWM":
functionCode = 0x05;
refAddr  = 961;
break;
case "GET_CH1_SYNC_PWM":
functionCode = 0x01;
refAddr  = 961;
break;
case "SET_CH2_SYNC_PWM":
functionCode = 0x05;
refAddr  = 962;
break;
case "GET_CH2_SYNC_PWM":
functionCode = 0x01;
refAddr  = 962;
break;
case "SET_CH3_SYNC_PWM":
functionCode = 0x05;
refAddr  = 963;
break;
case "GET_CH3_SYNC_PWM":
functionCode = 0x01;
refAddr  = 963;
break;
case "SET_CH4_SYNC_PWM":
functionCode = 0x05;
refAddr  = 964;
break;
case "GET_CH4_SYNC_PWM":
functionCode = 0x01;
refAddr  = 964;
break;
case "SET_CH5_SYNC_PWM":
functionCode = 0x05;
refAddr  = 965;
break;
case "GET_CH5_SYNC_PWM":
functionCode = 0x01;
refAddr  = 965;
break;
case "SET_CH6_SYNC_PWM":
functionCode = 0x05;
refAddr  = 966;
break;
case "GET_CH6_SYNC_PWM":
functionCode = 0x01;
refAddr  = 966;
break;
case "SET_CH7_SYNC_PWM":
functionCode = 0x05;
refAddr  = 967;
break;
case "GET_CH7_SYNC_PWM":
functionCode = 0x01;
refAddr  = 967;
break;
case "SET_CH0_ENABLE_DI_TRIGGER":
functionCode = 0x05;
refAddr  = 896;
break;
case "GET_CH0_ENABLE_DI_TRIGGER":
functionCode = 0x01;
refAddr  = 896;
break;
case "SET_CH1_ENABLE_DI_TRIGGER":
functionCode = 0x05;
refAddr  = 897;
break;
case "GET_CH1_ENABLE_DI_TRIGGER":
functionCode = 0x01;
refAddr  = 897;
break;
case "SET_CH2_ENABLE_DI_TRIGGER":
functionCode = 0x05;
refAddr  = 898;
break;
case "GET_CH2_ENABLE_DI_TRIGGER":
functionCode = 0x01;
refAddr  = 898;
break;
case "SET_CH3_ENABLE_DI_TRIGGER":
functionCode = 0x05;
refAddr  = 899;
break;
case "GET_CH3_ENABLE_DI_TRIGGER":
functionCode = 0x01;
refAddr  = 899;
break;
case "SET_CH4_ENABLE_DI_TRIGGER":
functionCode = 0x05;
refAddr  = 900;
break;
case "GET_CH4_ENABLE_DI_TRIGGER":
functionCode = 0x01;
refAddr  = 900;
break;
case "SET_CH5_ENABLE_DI_TRIGGER":
functionCode = 0x05;
refAddr  = 901;
break;
case "GET_CH5_ENABLE_DI_TRIGGER":
functionCode = 0x01;
refAddr  = 901;
break;
case "SET_CH6_ENABLE_DI_TRIGGER":
functionCode = 0x05;
refAddr  = 902;
break;
case "GET_CH6_ENABLE_DI_TRIGGER":
functionCode = 0x01;
refAddr  = 902;
break;
case "SET_CH7_ENABLE_DI_TRIGGER":
functionCode = 0x05;
refAddr  = 903;
break;
case "GET_CH7_ENABLE_DI_TRIGGER":
functionCode = 0x01;
refAddr  = 903;
break;
case "SET_CH0_DI_TRIGGER_STATE":
functionCode = 0x05;
refAddr  = 928;
break;
case "GET_CH0_DI_TRIGGER_STATE":
functionCode = 0x01;
refAddr  = 928;
break;
case "SET_CH1_DI_TRIGGER_STATE":
functionCode = 0x05;
refAddr  = 929;
break;
case "GET_CH1_DI_TRIGGER_STATE":
functionCode = 0x01;
refAddr  = 929;
break;
case "SET_CH2_DI_TRIGGER_STATE":
functionCode = 0x05;
refAddr  = 930;
break;
case "GET_CH2_DI_TRIGGER_STATE":
functionCode = 0x01;
refAddr  = 930;
break;
case "SET_CH3_DI_TRIGGER_STATE":
functionCode = 0x05;
refAddr  = 931;
break;
case "GET_CH3_DI_TRIGGER_STATE":
functionCode = 0x01;
refAddr  = 931;
break;
case "SET_CH4_DI_TRIGGER_STATE":
functionCode = 0x05;
refAddr  = 932;
break;
case "GET_CH4_DI_TRIGGER_STATE":
functionCode = 0x01;
refAddr  = 932;
break;
case "SET_CH5_DI_TRIGGER_STATE":
functionCode = 0x05;
refAddr  = 933;
break;
case "GET_CH5_DI_TRIGGER_STATE":
functionCode = 0x01;
refAddr  = 933;
break;
case "SET_CH6_DI_TRIGGER_STATE":
functionCode = 0x05;
refAddr  = 934;
break;
case "GET_CH6_DI_TRIGGER_STATE":
functionCode = 0x01;
refAddr  = 934;
break;
case "SET_CH7_DI_TRIGGER_STATE":
functionCode = 0x05;
refAddr  = 935;
break;
case "GET_CH7_DI_TRIGGER_STATE":
functionCode = 0x01;
refAddr  = 935;
break;
case "OUTPUT_PWM":
functionCode = 0x0F;
refAddr  = 0;
break;
case "OUTPUT_SYNC_PWM":
functionCode = 0x05;
refAddr  = 289;
break;
case "SET_SAVE_PWM_CONFIG":
functionCode = 0x05;
refAddr  = 288;
break;
case "READ_PWM_ON_STATUS":
functionCode = 0x01;
refAddr  = 0;
break;
case "READ_PWM_DI_STATUS":
functionCode = 0x01;
refAddr  = 32;
break;
case "SET_CH0_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 96;
break;
case "GET_CH0_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 96;
break;
case "SET_CH1_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 98;
break;
case "GET_CH1_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 98;
break;
case "SET_CH2_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 100;
break;
case "GET_CH2_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 100;
break;
case "SET_CH3_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 102;
break;
case "GET_CH3_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 102;
break;
case "SET_CH4_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 104;
break;
case "GET_CH4_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 104;
break;
case "SET_CH5_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 106;
break;
case "GET_CH5_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 106;
break;
case "SET_CH6_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 108;
break;
case "GET_CH6_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 108;
break;
case "SET_CH7_PRESET_COUNTER":
functionCode = 0x10;
refAddr  = 110;
break;
case "GET_CH7_PRESET_COUNTER":
functionCode = 0x03;
refAddr  = 110;
break;
case "SET_CH0_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 64;
break;
case "GET_CH0_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 64;
break;
case "SET_CH1_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 66;
break;
case "GET_CH1_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 66;
break;
case "SET_CH2_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 68;
break;
case "GET_CH2_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 68;
break;
case "SET_CH3_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 70;
break;
case "GET_CH3_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 70;
break;
case "SET_CH4_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 72;
break;
case "GET_CH4_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 72;
break;
case "SET_CH5_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 74;
break;
case "GET_CH5_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 74;
break;
case "SET_CH6_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 76;
break;
case "GET_CH6_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 76;
break;
case "SET_CH7_MAX_COUNTER":
functionCode = 0x10;
refAddr  = 78;
break;
case "GET_CH7_MAX_COUNTER":
functionCode = 0x03;
refAddr  = 78;
break;
case "READ_CH0_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 0;
break;
case "READ_CH1_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 2;
break;
case "READ_CH2_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 4;
break;
case "READ_CH3_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 6;
break;
case "READ_CH4_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 8;
break;
case "READ_CH5_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 10;
break;
case "READ_CH6_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 12;
break;
case "READ_CH7_COUNTER_FREQ":
functionCode = 0x03;
refAddr  = 14;
break;
case "CLEAR_CH0_COUNTER":
functionCode = 0x05;
refAddr  = 64;
break;
case "CLEAR_CH1_COUNTER":
functionCode = 0x05;
refAddr  = 65;
break;
case "CLEAR_CH2_COUNTER":
functionCode = 0x05;
refAddr  = 66;
break;
case "CLEAR_CH3_COUNTER":
functionCode = 0x05;
refAddr  = 67;
break;
case "CLEAR_CH4_COUNTER":
functionCode = 0x05;
refAddr  = 68;
break;
case "CLEAR_CH5_COUNTER":
functionCode = 0x05;
refAddr  = 69;
break;
case "CLEAR_CH6_COUNTER":
functionCode = 0x05;
refAddr  = 70;
break;
case "CLEAR_CH7_COUNTER":
functionCode = 0x05;
refAddr  = 71;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion 7088
#region GPS721
case "GPS721":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_GPS_DATE":
functionCode = 0x05;
refAddr  = 273;
break;
case "READ_GPS_DATE":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_GPS_DATA":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion GPS721
#region XV110
case "XV110":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 1;
else
refAddr  = 2;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 2;
else
refAddr  = 4;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 3;
else
refAddr  = 6;
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 4;
else
refAddr  = 8;
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 5;
else
refAddr  = 10;
break;
case "READ_CH6_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 6;
else
refAddr  = 12;
break;
case "READ_CH7_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 7;
else
refAddr  = 14;
break;
case "READ_CH8_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 8;
else
refAddr  = 16;
break;
case "READ_CH9_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 9;
else
refAddr  = 18;
break;
case "READ_CH10_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 10;
else
refAddr  = 20;
break;
case "READ_CH11_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 11;
else
refAddr  = 22;
break;
case "READ_CH12_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 12;
else
refAddr  = 24;
break;
case "READ_CH13_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 13;
else
refAddr  = 26;
break;
case "READ_CH14_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 14;
else
refAddr  = 28;
break;
case "READ_CH15_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 15;
else
refAddr  = 30;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "CLEAR_CH6_DI_COUNTER":
functionCode = 0x05;
refAddr  = 518;
break;
case "CLEAR_CH7_DI_COUNTER":
functionCode = 0x05;
refAddr  = 519;
break;
case "CLEAR_CH8_DI_COUNTER":
functionCode = 0x05;
refAddr  = 520;
break;
case "CLEAR_CH9_DI_COUNTER":
functionCode = 0x05;
refAddr  = 521;
break;
case "CLEAR_CH10_DI_COUNTER":
functionCode = 0x05;
refAddr  = 522;
break;
case "CLEAR_CH11_DI_COUNTER":
functionCode = 0x05;
refAddr  = 523;
break;
case "CLEAR_CH12_DI_COUNTER":
functionCode = 0x05;
refAddr  = 524;
break;
case "CLEAR_CH13_DI_COUNTER":
functionCode = 0x05;
refAddr  = 525;
break;
case "CLEAR_CH14_DI_COUNTER":
functionCode = 0x05;
refAddr  = 526;
break;
case "CLEAR_CH15_DI_COUNTER":
functionCode = 0x05;
refAddr  = 527;
break;
}
break;
#endregion XV110
#region XV111
case "XV111":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH8_DO_BIT":
functionCode = 0x05;
refAddr  = 8;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH9_DO_BIT":
functionCode = 0x05;
refAddr  = 9;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH10_DO_BIT":
functionCode = 0x05;
refAddr  = 10;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH11_DO_BIT":
functionCode = 0x05;
refAddr  = 11;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH12_DO_BIT":
functionCode = 0x05;
refAddr  = 12;
break;
case "OUTPUT_CH13_DO_BIT":
functionCode = 0x05;
refAddr  = 13;
break;
case "OUTPUT_CH5_DO_BIT":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_DO_BIT":
functionCode = 0x05;
refAddr  = 6;
break;
case "OUTPUT_CH14_DO_BIT":
functionCode = 0x05;
refAddr  = 14;
break;
case "OUTPUT_CH15_DO_BIT":
functionCode = 0x05;
refAddr  = 15;
break;
case "OUTPUT_CH7_DO_BIT":
functionCode = 0x05;
refAddr  = 7;
break;
case "SET_DO_REVERSE":
functionCode = 0x05;
refAddr  = 265;
break;
case "GET_DO_REVERSE":
functionCode = 0x01;
refAddr  = 265;
break;
case "SET_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "READ_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
}
break;
#endregion XV111
#region XV107
case "XV107":
case "'X116":
hasCmd = false;
switch (cmdTitle)
{
// module function
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH5_DO_BIT":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_DO_BIT":
functionCode = 0x05;
refAddr  = 6;
break;
case "OUTPUT_CH7_DO_BIT":
functionCode = 0x05;
refAddr  = 7;
break;
case "SET_DO_REVERSE":
functionCode = 0x05;
refAddr  = 265;
break;
case "GET_DO_REVERSE":
functionCode = 0x01;
refAddr  = 265;
break;
case "SET_DO_POWER_ON":
functionCode = 0xf;
refAddr  = 160;
break;
case "READ_DO_POWER_ON":
functionCode = 0x1;
refAddr  = 160;
break;
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 1;
else
refAddr  = 2;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 2;
else
refAddr  = 4;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 3;
else
refAddr  = 6;
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 4;
else
refAddr  = 8;
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 5;
else
refAddr  = 10;
break;
case "READ_CH6_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 6;
else
refAddr  = 12;
break;
case "READ_CH7_DI_COUNTER":
functionCode = 0x04;
if (Utility.ToFirmware(io.module.Firmware) == 0x100)
refAddr  = 7;
else
refAddr  = 14;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "CLEAR_CH6_DI_COUNTER":
functionCode = 0x05;
refAddr  = 518;
break;
case "CLEAR_CH7_DI_COUNTER":
functionCode = 0x05;
refAddr  = 519;
break;
}
break;
#endregion XV107
//  XV310_XV307_XV306 ??î§€???????? 310 ??AI,AO,DI,DO, 307
#region XV306
case "XV306":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x05;
refAddr  = 270;
break;
case "GET_MODBUS_MISC":
functionCode = 0x01;
refAddr  = 270;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// AI function
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_SIMPLE_AI_ALARM_STATUS":
functionCode = 0x01;
refAddr  = 261;
break;
// DI function
case "READ_SIMPLE_AI_DI_STATUS":
functionCode = 0x02;
refAddr  = 32;
break;
case "SET_DI_REVERSE":
functionCode = 0x05;
refAddr  = 264;
break;
case "GET_DI_REVERSE":
functionCode = 0x01;
refAddr  = 264;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x03;
refAddr  = 128;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x03;
refAddr  = 130;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x03;
refAddr  = 132;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x03;
refAddr  = 134;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
// DO function
case "READ_SIMPLE_AI_DO_STATUS":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_MFDO":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_MFDO":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_MFDO":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_MFDO":
functionCode = 0x05;
refAddr  = 3;
break;
case "SET_MF_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "SET_DO_REVERSE":
functionCode = 0x05;
refAddr  = 265;
break;
case "GET_DO_REVERSE":
functionCode = 0x01;
refAddr  = 265;
break;
case "SET_MF_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "READ_MF_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
}
break;
#endregion XV306
#region XV307
case "XV307":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x05;
refAddr  = 270;
break;
case "GET_MODBUS_MISC":
functionCode = 0x01;
refAddr  = 270;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_SIMPLE_AI_ALARM_STATUS":
functionCode = 0x01;
refAddr  = 261;
break;
// DI function
case "READ_SIMPLE_AI_DI_STATUS":
functionCode = 0x02;
refAddr  = 32;
break;
case "SET_DI_REVERSE":
functionCode = 0x05;
refAddr  = 264;
break;
case "GET_DI_REVERSE":
functionCode = 0x01;
refAddr  = 264;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x03;
refAddr  = 128;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x03;
refAddr  = 130;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x03;
refAddr  = 132;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x03;
refAddr  = 134;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
// AO function
case "OUTPUT_CH0_AO":
functionCode = 0x06;
refAddr  = 32;
break;
case "OUTPUT_CH1_AO":
functionCode = 0x06;
refAddr  = 33;
break;
case "READ_CH0_AO":
functionCode = 0x03;
refAddr  = 64;
break;
case "READ_CH1_AO":
functionCode = 0x03;
refAddr  = 65;
break;
case "READ_CH0_LAST_AO":
functionCode = 0x03;
refAddr  = 32;
break;
case "READ_CH1_LAST_AO":
functionCode = 0x03;
refAddr  = 33;
break;
case "SET_CH0_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 416;
break;
case "SET_CH0_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 288;
break;
case "SET_CH1_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 417;
break;
case "SET_CH1_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 289;
break;
case "GET_CH0_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 416;
break;
case "GET_CH0_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 288;
break;
case "GET_CH1_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 417;
break;
case "GET_CH1_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 289;
break;
case "SET_CH0_AO_POWERON":
functionCode = 0x06;
refAddr  = 192;
break;
case "SET_CH1_AO_POWERON":
functionCode = 0x06;
refAddr  = 193;
break;
case "READ_CH0_AO_POWERON":
functionCode = 0x03;
refAddr  = 192;
break;
case "READ_CH1_AO_POWERON":
functionCode = 0x03;
refAddr  = 193;
break;
// DO function
case "READ_SIMPLE_AI_DO_STATUS":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_MFDO":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_MFDO":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_MFDO":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_MFDO":
functionCode = 0x05;
refAddr  = 3;
break;
case "SET_MF_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "SET_DO_REVERSE":
functionCode = 0x05;
refAddr  = 265;
break;
case "GET_DO_REVERSE":
functionCode = 0x01;
refAddr  = 265;
break;
case "SET_MF_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "READ_MF_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
}
break;
#endregion XV307
#region XV308
case "XV308":
hasCmd = false;
switch (cmdTitle)
{
// module function
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x05;
refAddr  = 270;
break;
case "GET_MODBUS_MISC":
functionCode = 0x01;
refAddr  = 270;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// AI function
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
// DO function
case "READ_SIMPLE_AI_DO_STATUS":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_MFDO":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_MFDO":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_MFDO":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_MFDO":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_MFDO":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH5_MFDO":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_MFDO":
functionCode = 0x05;
refAddr  = 6;
break;
case "OUTPUT_CH7_MFDO":
functionCode = 0x05;
refAddr  = 7;
break;
case "SET_MF_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_MF_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
case "SET_DO_REVERSE":
functionCode = 0x05;
refAddr  = 265;
break;
case "GET_DO_REVERSE":
functionCode = 0x01;
refAddr  = 265;
break;
case "SET_MF_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "READ_MF_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
case "READ_SIMPLE_AI_ALARM_STATUS":
functionCode = 0x01;
refAddr  = 261;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x05;
refAddr  = 264;
break;
case "GET_DI_REVERSE":
functionCode = 0x01;
refAddr  = 264;
break;
case "READ_SIMPLE_AI_DI_STATUS":
functionCode = 0x02;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x03;
refAddr  = 128;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x03;
refAddr  = 130;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x03;
refAddr  = 132;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x03;
refAddr  = 134;
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x03;
refAddr  = 136;
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x03;
refAddr  = 138;
break;
case "READ_CH6_DI_COUNTER":
functionCode = 0x03;
refAddr  = 140;
break;
case "READ_CH7_DI_COUNTER":
functionCode = 0x03;
refAddr  = 142;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "CLEAR_CH6_DI_COUNTER":
functionCode = 0x05;
refAddr  = 518;
break;
case "CLEAR_CH7_DI_COUNTER":
functionCode = 0x05;
refAddr  = 519;
break;
}
break;
#endregion XV308
#region XV310
case "XV310":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x05;
refAddr  = 270;
break;
case "GET_MODBUS_MISC":
functionCode = 0x01;
refAddr  = 270;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// AI function
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_SIMPLE_AI_ALARM_STATUS":
functionCode = 0x01;
refAddr  = 261;
break;
// DI function
case "READ_SIMPLE_AI_DI_STATUS":
functionCode = 0x02;
refAddr  = 32;
break;
case "SET_DI_REVERSE":
functionCode = 0x05;
refAddr  = 264;
break;
case "GET_DI_REVERSE":
functionCode = 0x01;
refAddr  = 264;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x03;
refAddr  = 128;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x03;
refAddr  = 130;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x03;
refAddr  = 132;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x03;
refAddr  = 134;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
// AO function
case "OUTPUT_CH0_AO":
functionCode = 0x06;
refAddr  = 32;
break;
case "OUTPUT_CH1_AO":
functionCode = 0x06;
refAddr  = 33;
break;
case "READ_CH0_AO":
functionCode = 0x03;
refAddr  = 64;
break;
case "READ_CH1_AO":
functionCode = 0x03;
refAddr  = 65;
break;
case "READ_CH0_LAST_AO":
functionCode = 0x03;
refAddr  = 32;
break;
case "READ_CH1_LAST_AO":
functionCode = 0x03;
refAddr  = 33;
break;
case "SET_CH0_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 416;
break;
case "SET_CH0_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 288;
break;
case "SET_CH1_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 417;
break;
case "SET_CH1_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 289;
break;
case "GET_CH0_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 416;
break;
case "GET_CH0_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 288;
break;
case "GET_CH1_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 417;
break;
case "GET_CH1_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 289;
break;
case "SET_CH0_AO_POWERON":
functionCode = 0x06;
refAddr  = 192;
break;
case "SET_CH1_AO_POWERON":
functionCode = 0x06;
refAddr  = 193;
break;
case "READ_CH0_AO_POWERON":
functionCode = 0x03;
refAddr  = 192;
break;
case "READ_CH1_AO_POWERON":
functionCode = 0x03;
refAddr  = 193;
break;
// DO function
case "READ_SIMPLE_AI_DO_STATUS":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_MFDO":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_MFDO":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_MFDO":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_MFDO":
functionCode = 0x05;
refAddr  = 3;
break;
case "SET_MF_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "SET_DO_REVERSE":
functionCode = 0x05;
refAddr  = 265;
break;
case "GET_DO_REVERSE":
functionCode = 0x01;
refAddr  = 265;
break;
case "SET_MF_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "READ_MF_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
}
break;
#endregion XV310
#region tDA1P1R1
case "tDA1P1R1":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x05;
refAddr  = 264;
break;
case "GET_DI_REVERSE":
functionCode = 0x01;
refAddr  = 264;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 128;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
// AO function
case "SET_CH0_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 416;
break;
case "GET_CH0_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 416;
break;
case "SET_CH0_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 288;
break;
case "GET_CH0_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 288;
break;
case "READ_CH0_LAST_AO":
functionCode = 0x03;
refAddr  = 0;
break;
case "OUTPUT_CH0_AO":
functionCode = 0x06;
refAddr  = 0;
break;
case "READ_CH0_AO":
functionCode = 0x03;
refAddr  = 64;
break;
case "SET_CH0_AO_POWERON":
functionCode = 0x06;
refAddr  = 192;
break;
case "SET_CH0_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 96;
break;
case "READ_CH0_AO_POWERON":
functionCode = 0x03;
refAddr  = 192;
break;
case "READ_CH0_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 96;
break;
// DO function
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion tDA1P1R1
#region tAD4P2C2
case "tAD4P2C2":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x05;
refAddr  = 270;
break;
case "GET_MODBUS_MISC":
functionCode = 0x01;
refAddr  = 270;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
// AI function
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "SET_CH0_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 320;
break;
case "SET_CH1_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 321;
break;
case "SET_CH0_ENABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 320;
break;
case "SET_CH1_ENABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 321;
break;
case "GET_CH0_AI_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 320;
break;
case "GET_CH1_AI_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 321;
break;
case "SET_CH0_AI_ALARM_MODE":
functionCode = 0x05;
refAddr  = 336;
break;
case "SET_CH1_AI_ALARM_MODE":
functionCode = 0x05;
refAddr  = 337;
break;
case "READ_CH0_AI_ALARM_MODE":
functionCode = 0x01;
refAddr  = 336;
break;
case "READ_CH1_AI_ALARM_MODE":
functionCode = 0x01;
refAddr  = 337;
break;
case "SET_CH0_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 224;
break;
case "SET_CH1_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 225;
break;
case "SET_CH0_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 232;
break;
case "SET_CH1_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 233;
break;
case "GET_CH0_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 224;
break;
case "GET_CH1_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 225;
break;
case "GET_CH0_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 232;
break;
case "GET_CH1_SIMPLE_AI_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 233;
break;
case "CLEAR_CH0_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 304;
break;
case "CLEAR_CH1_HIGH_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 305;
break;
case "CLEAR_CH0_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 288;
break;
case "CLEAR_CH1_LOW_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 289;
break;
case "READ_SIMPLE_AI_ALARM_STATUS":
functionCode = 0x01;
refAddr  = 261;
break;
case "READ_LOW_ALARM_DO_ON":
functionCode = 0x01;
refAddr  = 288;
break;
case "READ_HIGH_ALARM_DO_ON":
functionCode = 0x01;
refAddr  = 304;
break;
// DI function
case "READ_SIMPLE_AI_DI_STATUS":
functionCode = 0x02;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x03;
refAddr  = 128;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x03;
refAddr  = 129;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
//DO Output function
case "READ_SIMPLE_AI_DO_STATUS":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_MFDO":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_MFDO":
functionCode = 0x05;
refAddr  = 1;
break;
case "SET_MF_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "READ_MF_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
case "SET_MF_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_MF_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion tAD4P2C2
#region tP4A4/tP4C4 4DI/4DO
case "tP4A4":
case "tP4C4":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_32Bit_DICnt":
functionCode = 0x05; // DO
refAddr  = 266;
break;
case "GET_32Bit_DICnt":
functionCode = 0x01; // DO
refAddr  = 266;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 256;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 0;
}
else
{
refAddr  = 0;
}
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 1;
}
else
{
refAddr  = 2;
}
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 2;
}
else
{
refAddr  = 4;
}
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 3;
}
else
{
refAddr  = 6;
}
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
// DO function
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion tP4A4/tP4C4
#region tP3R3/tP3POR3 3DI/3DO
case "tP3R3":
case "tP3POR3":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_32Bit_DICnt":
functionCode = 0x05; // DO
refAddr  = 266;
break;
case "GET_32Bit_DICnt":
functionCode = 0x01; // DO
refAddr  = 266;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 256;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 0;
}
else
{
refAddr  = 0;
}
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 1;
}
else
{
refAddr  = 2;
}
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 2;
}
else
{
refAddr  = 4;
}
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
// DO function
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion tP3R3/tP3POR3
#region tP8 8DI
case "tP8":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_32Bit_DICnt":
functionCode = 0x05; // DO
refAddr  = 266;
break;
case "GET_32Bit_DICnt":
functionCode = 0x01; // DO
refAddr  = 266;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 256;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 0;
}
else
{
refAddr  = 0;
}
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 1;
}
else
{
refAddr  = 2;
}
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 2;
}
else
{
refAddr  = 4;
}
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 3;
}
else
{
refAddr  = 6;
}
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 4;
}
else
{
refAddr  = 8;
}
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 5;
}
else
{
refAddr  = 10;
}
break;
case "READ_CH6_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 6;
}
else
{
refAddr  = 12;
}
break;
case "READ_CH7_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 7;
}
else
{
refAddr  = 14;
}
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "CLEAR_CH6_DI_COUNTER":
functionCode = 0x05;
refAddr  = 518;
break;
case "CLEAR_CH7_DI_COUNTER":
functionCode = 0x05;
refAddr  = 519;
break;
}
break;
#endregion tP8
#region tR5 5DO
case "tR5":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "SET_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "READ_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion tR5
#region tC8 8DO
case "tC8":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH5_DO_BIT":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_DO_BIT":
functionCode = 0x05;
refAddr  = 6;
break;
case "OUTPUT_CH7_DO_BIT":
functionCode = 0x05;
refAddr  = 7;
break;
case "SET_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "READ_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion tC8
#region tTH8
case "tTH8":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_TEMPERATURE_DISPLAY_FORMAT":
functionCode = 0x05;
refAddr  = 266;
break;
case "GET_TEMPERATURE_DISPLAY_FORMAT":
functionCode = 0x01;
refAddr  = 266;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
case "SET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 448;
break;
case "SET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 449;
break;
case "SET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 450;
break;
case "SET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 451;
break;
case "SET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 452;
break;
case "SET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 453;
break;
case "SET_CH6_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 454;
break;
case "SET_CH7_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 455;
break;
case "GET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 448;
break;
case "GET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 449;
break;
case "GET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 450;
break;
case "GET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 451;
break;
case "GET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 452;
break;
case "GET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 453;
break;
case "GET_CH6_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 454;
break;
case "GET_CH7_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 455;
break;
case "SET_USER_DEFINED_TYPE_A":
functionCode = 0x10;
refAddr  = 768;
break;
case "SET_USER_DEFINED_TYPE_B":
functionCode = 0x10;
refAddr  = 800;
break;
case "SET_USER_DEFINED_TYPE_C":
functionCode = 0x10;
refAddr  = 832;
break;
case "GET_USER_DEFINED_TYPE_A":
functionCode = 0x03;
refAddr  = 768;
break;
case "GET_USER_DEFINED_TYPE_B":
functionCode = 0x03;
refAddr  = 800;
break;
case "GET_USER_DEFINED_TYPE_C":
functionCode = 0x03;
refAddr  = 832;
break;
}
break;
#endregion tTH8
#region LC101
case "LC101":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_ON_DELAY_TIME":
functionCode = 0x06;
refAddr  = 497;
break;
case "GET_ON_DELAY_TIME":
functionCode = 0x03;
refAddr  = 497;
break;
case "SET_DI_CONNECTION":
functionCode = 0x05;
refAddr  = 273;
break;
case "GET_DI_CONNECTION":
functionCode = 0x01;
refAddr  = 273;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "SET_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "READ_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
}
break;
#endregion LC101
#region LC131
case "LC131":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DI_ALARM_ENABLE_STATUS":
functionCode = 0x05;
refAddr  = 261;
break;
case "SET_DI_ALARM_MODE":
functionCode = 0x05;
refAddr  = 262;
break;
case "READ_DI_ALARM_MODE":
functionCode = 0x01;
refAddr  = 262;
break;
case "SET_DI_CHANNELS_ALARM_CONFIG":
functionCode = 0x0F;
refAddr  = 544;
break;
case "GET_DI_CHANNELS_ALARM_CONFIG":
functionCode = 0x02;
refAddr  = 544;
break;
case "SET_DI_SHORT_CIRCUIT_ALARM_CONFIG":
functionCode = 0x0F;
refAddr  = 552;
break;
case "GET_DI_SHORT_CIRCUIT_ALARM_CONFIG":
functionCode = 0x02;
refAddr  = 552;
break;
case "READ_DI_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 261;
break;
case "READ_DI_SHORT_CIRCUIT_ALARM":
functionCode = 0x01;
refAddr  = 584;
break;
case "READ_DI_ALARM":
functionCode = 0x01;
refAddr  = 576;
break;
case "CLEAR_DI_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 281;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
refAddr  = 2;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "SET_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x05;
refAddr  = 259;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion LC131
#region PIR130, PIR230
case "PIR130":
case "PIR230":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_ON_DELAY_TIME":
functionCode = 0x06;
refAddr  = 515;
break;
case "GET_ON_DELAY_TIME":
functionCode = 0x03;
refAddr  = 515;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
// alarm function
case "SET_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 261;
break;
case "SET_ENABLE_AI_ALARM_MODE":
functionCode = 0x0F;
refAddr  = 261;
break;
case "SET_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 225;
break;
case "READ_SIMPLE_AI_ALARM_STATUS":
functionCode = 0x01;
refAddr  = 261;
break;
case "GET_SIMPLE_AI_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 225;
break;
case "CLEAR_ALARM_LATCH":
functionCode = 0x05;
refAddr  = 304;
break;
case "READ_PIR_ALARM_ON":
functionCode = 0x01;
refAddr  = 304;
break;
// di function
case "READ_SIMPLE_AI_DI_STATUS":
functionCode = 0x02;
refAddr  = 32;
break;
// do function
case "READ_SIMPLE_AI_DO_STATUS":
functionCode = 0x01;
refAddr  = 0;
break;
case "SET_PIR_SENSITIVITY":
functionCode = 0x06;
refAddr  = 516;
break;
case "GET_PIR_SENSITIVITY":
functionCode = 0x03;
refAddr  = 516;
break;
case "SET_PIR_LUMINANCE":
functionCode = 0x06;
refAddr  = 513;
break;
case "GET_PIR_LUMINANCE":
functionCode = 0x03;
refAddr  = 513;
break;
case "SET_PIR_ON_DELAY_TIME":
functionCode = 0x06;
refAddr  = 512;
break;
case "GET_PIR_ON_DELAY_TIME":
functionCode = 0x03;
refAddr  = 512;
break;
case "SET_PIR_BUZZER_MODE":
functionCode = 0x05;
refAddr  = 273;
break;
case "GET_PIR_BUZZER_MODE":
functionCode = 0x01;
refAddr  = 273;
break;
case "READ_ROTARY_SWITCH":
functionCode = 0x46;
refAddr  = 65535;
break;
}
break;
#endregion PIR130
#region LC221
case "LC221":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DI_REVERSE":
functionCode = 0x05;
refAddr  = 264;
break;
case "GET_DI_REVERSE":
functionCode = 0x01;
refAddr  = 264;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 0;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "SET_CH0_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 416;
break;
case "GET_CH0_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 416;
break;
case "SET_CH0_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 288;
break;
case "GET_CH0_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 288;
break;
case "OUTPUT_CH0_AO":
functionCode = 0x06;
refAddr  = 0;
break;
case "READ_CH0_AO":
functionCode = 0x03;
refAddr  = 64;
break;
case "READ_CH0_LAST_AO":
functionCode = 0x03;
refAddr  = 0;
break;
case "SET_CH0_AO_POWERON":
functionCode = 0x06;
refAddr  = 192;
break;
case "SET_CH0_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 96;
break;
case "READ_CH0_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 96;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "SET_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x05;
refAddr  = 259;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
// Log function
case "READ_LOG_STATUS":
functionCode = 0x04;
refAddr  = 874;
break;
case "READ_LOG_RTC":
functionCode = 0x04;
refAddr  = 864;
break;
case "WRITE_LOG_RTC":
functionCode = 0x10; // write registers
refAddr  = 864;
break;
case "SET_LOG_SAMPLE_MODE":
functionCode = 0x10; // write registers
refAddr  = 875;
break;
case "GET_LOG_SAMPLE_MODE":
functionCode = 0x04;
refAddr  = 875;
break;
case "SET_LOG_OVERWRITE": // ok
functionCode = 0x10; // write registers
refAddr  = 876;
break;
case "GET_LOG_OVERWRITE": //ok
functionCode = 0x04;
refAddr  = 876;
break;
case "SET_LOG_SAMPLE_PERIOD": //OK
functionCode = 0x10; // write registers
refAddr  = 877;
break;
case "GET_LOG_SAMPLE_PERIOD": //OK
functionCode = 0x04;
refAddr  = 877;
break;
case "SET_LOG_START_TIME": //OK
functionCode = 0x10; // write registers
refAddr  = 880;
break;
case "GET_LOG_START_TIME": //OK
functionCode = 0x04;
refAddr  = 880;
break;
case "SET_LOG_END_TIME": //OK
functionCode = 0x10; // write registers
refAddr  = 886;
break;
case "GET_LOG_END_TIME": //OK
functionCode = 0x04;
refAddr  = 886;
break;
}
break;
#endregion LC221
#region SC4104
case "SC4104":
case "SC6104":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x06;
refAddr  = 487;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x03;
refAddr  = 487;
break;
case "SET_TEMPERATURE_DISPLAY_FORMAT":
functionCode = 0x05;
refAddr  = 266;
break;
case "GET_TEMPERATURE_DISPLAY_FORMAT":
functionCode = 0x01;
refAddr  = 266;
break;
case "SET_DI_CONNECTION":
functionCode = 0x06;
refAddr  = 273;
break;
case "GET_DI_CONNECTION":
functionCode = 0x03;
refAddr  = 273;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "SET_ON_DELAY_TIME":
functionCode = 0x06;
refAddr  = 497;
break;
case "GET_ON_DELAY_TIME":
functionCode = 0x03;
refAddr  = 497;
break;
case "SET_SC_TEMPERATURE_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 274;
break;
case "GET_SC_TEMPERATURE_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 274;
break;
case "SET_SC_TEMPERATURE_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 274;
break;
case "GET_SC_TEMPERATURE_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 274;
break;
case "SET_SC_TEMPERATURE_ALARM_RANGE":
functionCode = 0x06;
refAddr  = 276;
break;
case "GET_SC_TEMPERATURE_ALARM_RANGE":
functionCode = 0x03;
refAddr  = 276;
break;
case "SET_SC_RELAY_PROTECT_TIME":
functionCode = 0x06;
refAddr  = 275;
break;
case "GET_SC_RELAY_PROTECT_TIME":
functionCode = 0x03;
refAddr  = 275;
break;
case "READ_ROTARY_SWITCH":
functionCode = 0x02;
refAddr  = 320;
break;
case "SET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 288;
break;
case "GET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 288;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "SET_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion SC4104 /SC6104
#region ZT-2005
case "ZT-2005-C8":
case "ZT-2005-C1":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_TEMPERATURE_DISPLAY_FORMAT":
functionCode = 0x05;
refAddr  = 266;
break;
case "GET_TEMPERATURE_DISPLAY_FORMAT":
functionCode = 0x01;
refAddr  = 266;
break;
// Temperature Offset
case "SET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 288;
break;
case "SET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 289;
break;
case "SET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 290;
break;
case "SET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 291;
break;
case "SET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 292;
break;
case "SET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 293;
break;
case "SET_CH6_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 294;
break;
case "SET_CH7_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 295;
break;
case "GET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 288;
break;
case "GET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 289;
break;
case "GET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 290;
break;
case "GET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 291;
break;
case "GET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 292;
break;
case "GET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 293;
break;
case "GET_CH6_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 294;
break;
case "GET_CH7_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 295;
break;
// Read AI function
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
}
break;
#endregion ZT-2005
#region ZT-2015/ Z2015
case "ZT-2015":
case "Z2015":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_ZB_RF_POWER":
functionCode = 0x06;
refAddr  = 502;
break;
case "GET_ZB_RF_POWER":
functionCode = 0x03;
refAddr  = 502;
break;
case "SET_RF_ENABLE_ENCRYPTION":
functionCode = 0x05;
refAddr  = 287;
break;
case "GET_RF_ENCRYPTION":
functionCode = 0x02;
refAddr  = 286;
break;
case "GET_RF_ENABLE_ENCRYPTION":
functionCode = 0x02;
refAddr  = 287;
break;
case "READ_DIP_SWITCH":
functionCode = 0x01;
refAddr  = 320;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 288;
break;
case "SET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 289;
break;
case "SET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 290;
break;
case "SET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 291;
break;
case "SET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 292;
break;
case "SET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 293;
break;
case "GET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 288;
break;
case "GET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 289;
break;
case "GET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 290;
break;
case "GET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 291;
break;
case "GET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 292;
break;
case "GET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 293;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
}
break;
#endregion Z2015
#region ZT-2017 / M2017
case "ZT-2017":
case "Z2017":
case "ZT-2017C":
case "Z2017C":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "READ_DIP_SWITCH":
functionCode = 0x01;
refAddr  = 320;
break;
case "SET_ZB_RF_POWER":
functionCode = 0x06;
refAddr  = 502;
break;
case "GET_ZB_RF_POWER":
functionCode = 0x03;
refAddr  = 502;
break;
case "SET_RF_ENABLE_ENCRYPTION":
functionCode = 0x05;
refAddr  = 287;
break;
case "GET_RF_ENCRYPTION":
functionCode = 0x02;
refAddr  = 286;
break;
case "GET_RF_ENABLE_ENCRYPTION":
functionCode = 0x02;
refAddr  = 287;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
}
break;
#endregion ZT-2017
#region ZT-2018/Z2018
case "ZT-2018":
case "Z2018":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "READ_DIP_SWITCH":
functionCode = 0x01;
refAddr  = 320;
break;
case "SET_ZB_RF_POWER":
functionCode = 0x06;
refAddr  = 502;
break;
case "GET_ZB_RF_POWER":
functionCode = 0x03;
refAddr  = 502;
break;
case "SET_RF_ENABLE_ENCRYPTION":
functionCode = 0x05;
refAddr  = 287;
break;
case "GET_RF_ENCRYPTION":
functionCode = 0x02;
refAddr  = 286;
break;
case "GET_RF_ENABLE_ENCRYPTION":
functionCode = 0x02;
refAddr  = 287;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
// CJC function
case "SET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CJC_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CJC_TEMPERATURE":
functionCode = 0x04;
refAddr  = 128;
break;
case "SET_CH0_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_CJC_OFFSET":
functionCode = 0x46;
refAddr  = 65535;
break;
// AI function
case "SET_CH0_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 256;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 256;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 257;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 257;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 258;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 258;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 259;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 259;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 260;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 260;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 261;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 261;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 262;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 262;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 263;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 263;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
}
break;
#endregion ZT-2018
#region ZT-2024/Z2024
case "ZT-2024":
case "Z2024":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_ZB_RF_POWER":
functionCode = 0x06;
refAddr  = 502;
break;
case "GET_ZB_RF_POWER":
functionCode = 0x03;
refAddr  = 502;
break;
case "SET_RF_ENABLE_ENCRYPTION":
functionCode = 0x05;
refAddr  = 287;
break;
case "GET_RF_ENCRYPTION":
functionCode = 0x02;
refAddr  = 286;
break;
case "GET_RF_ENABLE_ENCRYPTION":
functionCode = 0x02;
refAddr  = 287;
break;
case "READ_DIP_SWITCH":
functionCode = 0x01;
refAddr  = 320;
break;
case "GET_CH0_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 416;
break;
case "GET_CH0_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 288;
break;
case "GET_CH1_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 417;
break;
case "GET_CH1_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 289;
break;
case "GET_CH2_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 418;
break;
case "GET_CH2_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 290;
break;
case "GET_CH3_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 419;
break;
case "GET_CH3_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 291;
break;
case "READ_CH0_LAST_AO":
functionCode = 0x03;
refAddr  = 0;
break;
case "READ_CH1_LAST_AO":
functionCode = 0x03;
refAddr  = 1;
break;
case "READ_CH2_LAST_AO":
functionCode = 0x03;
refAddr  = 2;
break;
case "READ_CH3_LAST_AO":
functionCode = 0x03;
refAddr  = 3;
break;
case "READ_CH0_AO":
functionCode = 0x03;
refAddr  = 64;
break;
case "READ_CH1_AO":
functionCode = 0x03;
refAddr  = 65;
break;
case "READ_CH2_AO":
functionCode = 0x03;
refAddr  = 66;
break;
case "READ_CH3_AO":
functionCode = 0x03;
refAddr  = 67;
break;
case "SET_CH0_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 416;
break;
case "SET_CH0_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 288;
break;
case "SET_CH1_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 417;
break;
case "SET_CH1_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 289;
break;
case "SET_CH2_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 418;
break;
case "SET_CH2_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 290;
break;
case "SET_CH3_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 419;
break;
case "SET_CH3_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 291;
break;
case "OUTPUT_CH0_AO":
functionCode = 0x06;
refAddr  = 0;
break;
case "OUTPUT_CH1_AO":
functionCode = 0x06;
refAddr  = 1;
break;
case "OUTPUT_CH2_AO":
functionCode = 0x06;
refAddr  = 2;
break;
case "OUTPUT_CH3_AO":
functionCode = 0x06;
refAddr  = 3;
break;
case "SET_CH0_AO_POWERON":
functionCode = 0x06;
refAddr  = 192;
break;
case "SET_CH1_AO_POWERON":
functionCode = 0x06;
refAddr  = 193;
break;
case "SET_CH2_AO_POWERON":
functionCode = 0x06;
refAddr  = 194;
break;
case "SET_CH3_AO_POWERON":
functionCode = 0x06;
refAddr  = 195;
break;
case "READ_CH0_AO_POWERON":
functionCode = 0x03;
refAddr  = 192;
break;
case "READ_CH1_AO_POWERON":
functionCode = 0x03;
refAddr  = 193;
break;
case "READ_CH2_AO_POWERON":
functionCode = 0x03;
refAddr  = 194;
break;
case "READ_CH3_AO_POWERON":
functionCode = 0x03;
refAddr  = 195;
break;
case "SET_CH0_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 96;
break;
case "SET_CH1_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 97;
break;
case "SET_CH2_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 98;
break;
case "SET_CH3_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 99;
break;
case "READ_CH0_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 96;
break;
case "READ_CH1_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 97;
break;
case "READ_CH2_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 98;
break;
case "READ_CH3_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 99;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion ZT-2024
#region ZT-2026/Z2026
case "ZT-2026":
case "Z2026":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_ZB_RF_POWER":
functionCode = 0x06;
refAddr  = 502;
break;
case "GET_ZB_RF_POWER":
functionCode = 0x03;
refAddr  = 502;
break;
case "SET_RF_ENABLE_ENCRYPTION":
functionCode = 0x05;
refAddr  = 287;
break;
case "GET_RF_ENCRYPTION":
functionCode = 0x02;
refAddr  = 286;
break;
case "GET_RF_ENABLE_ENCRYPTION":
functionCode = 0x02;
refAddr  = 287;
break;
case "READ_DIP_SWITCH":
functionCode = 0x01;
refAddr  = 320;
break;
// DI function
case "SET_DI_REVERSE":
functionCode = 0x05;
refAddr  = 277;
break;
case "GET_DI_REVERSE":
functionCode = 0x01;
refAddr  = 277;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x03;
refAddr  = 128;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x03;
refAddr  = 130;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 768;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 769;
break;
// AI function
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 256;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 256;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 257;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 257;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 258;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 258;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 259;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 259;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
// Set/Get Low alarm disable/enable status
case "SET_CH0_LOW_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 608;
break;
case "SET_CH1_LOW_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 609;
break;
case "SET_CH2_LOW_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 610;
break;
case "SET_CH3_LOW_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 611;
break;
case "SET_CH0_ENABLE_MULTI_AI_LOW_ALARM":
functionCode = 0x05;
refAddr  = 608;
break;
case "SET_CH1_ENABLE_MULTI_AI_LOW_ALARM":
functionCode = 0x05;
refAddr  = 609;
break;
case "SET_CH2_ENABLE_MULTI_AI_LOW_ALARM":
functionCode = 0x05;
refAddr  = 610;
break;
case "SET_CH3_ENABLE_MULTI_AI_LOW_ALARM":
functionCode = 0x05;
refAddr  = 611;
break;
case "GET_CH0_LOW_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 608;
break;
case "GET_CH1_LOW_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 609;
break;
case "GET_CH2_LOW_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 610;
break;
case "GET_CH3_LOW_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 611;
break;
// Set/Get High alarm disable/enable status
case "SET_CH0_HIGH_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 576;
break;
case "SET_CH1_HIGH_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 577;
break;
case "SET_CH2_HIGH_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 578;
break;
case "SET_CH3_HIGH_DISABLE_AI_ALARM":
functionCode = 0x05;
refAddr  = 579;
break;
case "SET_CH0_ENABLE_MULTI_AI_HIGH_ALARM":
functionCode = 0x05;
refAddr  = 576;
break;
case "SET_CH1_ENABLE_MULTI_AI_HIGH_ALARM":
functionCode = 0x05;
refAddr  = 577;
break;
case "SET_CH2_ENABLE_MULTI_AI_HIGH_ALARM":
functionCode = 0x05;
refAddr  = 578;
break;
case "SET_CH3_ENABLE_MULTI_AI_HIGH_ALARM":
functionCode = 0x05;
refAddr  = 579;
break;
case "GET_CH0_HIGH_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 576;
break;
case "GET_CH1_HIGH_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 577;
break;
case "GET_CH2_HIGH_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 578;
break;
case "GET_CH3_HIGH_ALARM_ENABLE_STATUS":
functionCode = 0x01;
refAddr  = 579;
break;
// Set/Get Low alarm mode
case "SET_CH0_MULTI_AI_LOW_ALARM_MODE":
functionCode = 0x05;
refAddr  = 672;
break;
case "SET_CH1_MULTI_AI_LOW_ALARM_MODE":
functionCode = 0x05;
refAddr  = 673;
break;
case "SET_CH2_MULTI_AI_LOW_ALARM_MODE":
functionCode = 0x05;
refAddr  = 674;
break;
case "SET_CH3_MULTI_AI_LOW_ALARM_MODE":
functionCode = 0x05;
refAddr  = 675;
break;
case "GET_CH0_LOW_ALARM_MODE":
functionCode = 0x01;
refAddr  = 672;
break;
case "GET_CH1_LOW_ALARM_MODE":
functionCode = 0x01;
refAddr  = 673;
break;
case "GET_CH2_LOW_ALARM_MODE":
functionCode = 0x01;
refAddr  = 674;
break;
case "GET_CH3_LOW_ALARM_MODE":
functionCode = 0x01;
refAddr  = 675;
break;
// Set/Get High alarm mode
case "SET_CH0_MULTI_AI_HIGH_ALARM_MODE":
functionCode = 0x05;
refAddr  = 640;
break;
case "SET_CH1_MULTI_AI_HIGH_ALARM_MODE":
functionCode = 0x05;
refAddr  = 641;
break;
case "SET_CH2_MULTI_AI_HIGH_ALARM_MODE":
functionCode = 0x05;
refAddr  = 642;
break;
case "SET_CH3_MULTI_AI_HIGH_ALARM_MODE":
functionCode = 0x05;
refAddr  = 643;
break;
case "GET_CH0_HIGH_ALARM_MODE":
functionCode = 0x01;
refAddr  = 640;
break;
case "GET_CH1_HIGH_ALARM_MODE":
functionCode = 0x01;
refAddr  = 641;
break;
case "GET_CH2_HIGH_ALARM_MODE":
functionCode = 0x01;
refAddr  = 642;
break;
case "GET_CH3_HIGH_ALARM_MODE":
functionCode = 0x01;
refAddr  = 643;
break;
// Set/Get Low alarm Limit
case "SET_CH0_MULTI_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 608;
break;
case "SET_CH1_MULTI_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 609;
break;
case "SET_CH2_MULTI_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 610;
break;
case "SET_CH3_MULTI_AI_LOW_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 611;
break;
case "GET_CH0_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 608;
break;
case "GET_CH1_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 609;
break;
case "GET_CH2_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 610;
break;
case "GET_CH3_LOW_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 611;
break;
// Set/Get High alarm limit
case "SET_CH0_MULTI_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 576;
break;
case "SET_CH1_MULTI_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 577;
break;
case "SET_CH2_MULTI_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 578;
break;
case "SET_CH3_MULTI_AI_HIGH_ALARM_LIMIT":
functionCode = 0x06;
refAddr  = 579;
break;
case "GET_CH0_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 576;
break;
case "GET_CH1_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 577;
break;
case "GET_CH2_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 578;
break;
case "GET_CH3_HIGH_ALARM_LIMIT":
functionCode = 0x03;
refAddr  = 579;
break;
// Set/Get Low Alarm DO Ch
case "SET_CH0_MULTI_AI_LOW_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 328;
break;
case "SET_CH1_MULTI_AI_LOW_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 329;
break;
case "SET_CH2_MULTI_AI_LOW_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 330;
break;
case "SET_CH3_MULTI_AI_LOW_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 331;
break;
case "GET_CH0_LOW_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 328;
break;
case "GET_CH1_LOW_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 329;
break;
case "GET_CH2_LOW_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 330;
break;
case "GET_CH3_LOW_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 331;
break;
// Set/Get High Alarm DO Ch
case "SET_CH0_MULTI_AI_HIGH_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 320;
break;
case "SET_CH1_MULTI_AI_HIGH_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 321;
break;
case "SET_CH2_MULTI_AI_HIGH_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 322;
break;
case "SET_CH3_MULTI_AI_HIGH_ALARM_DOCH":
functionCode = 0x06;
refAddr  = 323;
break;
case "GET_CH0_HIGH_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 320;
break;
case "GET_CH1_HIGH_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 321;
break;
case "GET_CH2_HIGH_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 322;
break;
case "GET_CH3_HIGH_ALARM_DOCH":
functionCode = 0x03;
refAddr  = 323;
break;
// read high low alarm do on
case "READ_CH0_LOW_ALARM_DO_ON":
functionCode = 0x01;
refAddr  = 736;
break;
case "READ_CH1_LOW_ALARM_DO_ON":
functionCode = 0x01;
refAddr  = 736;
break;
case "READ_CH0_HIGH_ALARM_DO_ON":
functionCode = 0x01;
refAddr  = 704;
break;
case "READ_CH1_HIGH_ALARM_DO_ON":
functionCode = 0x01;
refAddr  = 704;
break;
case "READ_CH0_ALARM_DO_CHANNELS":
functionCode = 0x03;
refAddr  = 736;
break;
case "READ_CH1_ALARM_DO_CHANNELS":
functionCode = 0x03;
refAddr  = 737;
break;
case "READ_MULTI_AI_DIO_STATUS":
functionCode = 0x01;
refAddr  = 0;
break;
// DO output
case "OUTPUT_CH0_MFDO":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_MFDO":
functionCode = 0x05;
refAddr  = 1;
break;
case "SET_MF_DO_POWER_ON":
functionCode = 0x0F;
refAddr  = 160;
break;
case "READ_MF_DO_POWER_ON":
functionCode = 0x01;
refAddr  = 160;
break;
case "SET_MF_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_MF_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// AO output
case "SET_CH0_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 416;
break;
case "GET_CH0_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 416;
break;
case "SET_CH1_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 417;
break;
case "GET_CH1_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 417;
break;
case "SET_CH0_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 288;
break;
case "GET_CH0_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 288;
break;
case "SET_CH1_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 289;
break;
case "GET_CH1_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 289;
break;
case "OUTPUT_CH0_AO":
functionCode = 0x06;
refAddr  = 32;
break;
case "OUTPUT_CH1_AO":
functionCode = 0x06;
refAddr  = 33;
break;
case "READ_CH0_AO":
functionCode = 0x03;
refAddr  = 64;
break;
case "READ_CH1_AO":
functionCode = 0x03;
refAddr  = 65;
break;
case "READ_CH0_LAST_AO":
functionCode = 0x03;
refAddr  = 32;
break;
case "READ_CH1_LAST_AO":
functionCode = 0x03;
refAddr  = 33;
break;
case "READ_CH0_AO_POWERON":
functionCode = 0x03;
refAddr  = 192;
break;
case "READ_CH1_AO_POWERON":
functionCode = 0x03;
refAddr  = 193;
break;
case "READ_CH0_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 96;
break;
case "READ_CH1_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 97;
break;
case "SET_CH0_AO_POWERON":
functionCode = 0x06;
refAddr  = 192;
break;
case "SET_CH1_AO_POWERON":
functionCode = 0x06;
refAddr  = 193;
break;
case "SET_CH0_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 96;
break;
case "SET_CH1_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 97;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion ZT-2026
#region ZT-2042/Z2042 8 DO
case "ZT-2042":
case "Z2042":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_ZB_RF_POWER":
functionCode = 0x06;
refAddr  = 502;
break;
case "GET_ZB_RF_POWER":
functionCode = 0x03;
refAddr  = 502;
break;
case "SET_RF_ENABLE_ENCRYPTION":
functionCode = 0x05;
refAddr  = 287;
break;
case "GET_RF_ENCRYPTION":
functionCode = 0x02;
refAddr  = 286;
break;
case "GET_RF_ENABLE_ENCRYPTION":
functionCode = 0x02;
refAddr  = 287;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH5_DO_BIT":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_DO_BIT":
functionCode = 0x05;
refAddr  = 6;
break;
case "OUTPUT_CH7_DO_BIT":
functionCode = 0x05;
refAddr  = 7;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion ZT-2042
#region ZT-2043/Z2043 14 DO
case "ZT-2043":
case "Z2043":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_ZB_RF_POWER":
functionCode = 0x06;
refAddr  = 502;
break;
case "GET_ZB_RF_POWER":
functionCode = 0x03;
refAddr  = 502;
break;
case "SET_RF_ENABLE_ENCRYPTION":
functionCode = 0x05;
refAddr  = 287;
break;
case "GET_RF_ENCRYPTION":
functionCode = 0x02;
refAddr  = 286;
break;
case "GET_RF_ENABLE_ENCRYPTION":
functionCode = 0x02;
refAddr  = 287;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH5_DO_BIT":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_DO_BIT":
functionCode = 0x05;
refAddr  = 6;
break;
case "OUTPUT_CH7_DO_BIT":
functionCode = 0x05;
refAddr  = 7;
break;
case "OUTPUT_CH8_DO_BIT":
functionCode = 0x05;
refAddr  = 8;
break;
case "OUTPUT_CH9_DO_BIT":
functionCode = 0x05;
refAddr  = 9;
break;
case "OUTPUT_CH10_DO_BIT":
functionCode = 0x05;
refAddr  = 10;
break;
case "OUTPUT_CH11_DO_BIT":
functionCode = 0x05;
refAddr  = 11;
break;
case "OUTPUT_CH12_DO_BIT":
functionCode = 0x05;
refAddr  = 12;
break;
case "OUTPUT_CH13_DO_BIT":
functionCode = 0x05;
refAddr  = 13;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion ZT-2043
#region ZT-2052
case "ZT-2052":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_32Bit_DICnt":
functionCode = 0x05; // DO
refAddr  = 266;
break;
case "GET_32Bit_DICnt":
functionCode = 0x01; // DO
refAddr  = 266;
break;
case "SET_ZB_RF_POWER":
functionCode = 0x06;
refAddr  = 502;
break;
case "GET_ZB_RF_POWER":
functionCode = 0x03;
refAddr  = 502;
break;
case "GET_RF_ENCRYPTION":
functionCode = 0x02;
refAddr  = 286;
break;
case "SET_RF_ENABLE_ENCRYPTION":
functionCode = 0x05;
refAddr  = 287;
break;
case "GET_RF_ENABLE_ENCRYPTION":
functionCode = 0X01;
refAddr  = 287;
break;
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DIP_SWITCH":
functionCode = 0x01;
refAddr  = 320;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH": // 0x107
functionCode = 0x05;
refAddr  = 263;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 0;
}
else
{
refAddr  = 0;
}
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 1;
}
else
{
refAddr  = 2;
}
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 2;
}
else
{
refAddr  = 4;
}
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 3;
}
else
{
refAddr  = 6;
}
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 4;
}
else
{
refAddr  = 8;
}
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 5;
}
else
{
refAddr  = 10;
}
break;
case "READ_CH6_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 6;
}
else
{
refAddr  = 12;
}
break;
case "READ_CH7_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 7;
}
else
{
refAddr  = 14;
}
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "CLEAR_CH6_DI_COUNTER":
functionCode = 0x05;
refAddr  = 518;
break;
case "CLEAR_CH7_DI_COUNTER":
functionCode = 0x05;
refAddr  = 519;
break;
}
break;
#endregion ZT-2052
#region ZT-2053/Z2053
case "ZT-2053":
case "Z2053":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_32Bit_DICnt":
functionCode = 0x05; // DO
refAddr  = 266;
break;
case "GET_32Bit_DICnt":
functionCode = 0x01; // DO
refAddr  = 266;
break;
case "SET_ZB_RF_POWER":
functionCode = 0x06;
refAddr  = 502;
break;
case "GET_ZB_RF_POWER":
functionCode = 0x03;
refAddr  = 502;
break;
case "GET_RF_ENCRYPTION":
functionCode = 0x02;
refAddr  = 286;
break;
case "SET_RF_ENABLE_ENCRYPTION":
functionCode = 0x05;
refAddr  = 287;
break;
case "GET_RF_ENABLE_ENCRYPTION":
functionCode = 0X01;
refAddr  = 287;
break;
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DIP_SWITCH":
functionCode = 0x01;
refAddr  = 320;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 0;
}
else
{
refAddr  = 0;
}
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 1;
}
else
{
refAddr  = 2;
}
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 2;
}
else
{
refAddr  = 4;
}
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 3;
}
else
{
refAddr  = 6;
}
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 4;
}
else
{
refAddr  = 8;
}
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 5;
}
else
{
refAddr  = 10;
}
break;
case "READ_CH6_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 6;
}
else
{
refAddr  = 12;
}
break;
case "READ_CH7_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 7;
}
else
{
refAddr  = 14;
}
break;
case "READ_CH8_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 8;
}
else
{
refAddr  = 16;
}
break;
case "READ_CH9_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 9;
}
else
{
refAddr  = 18;
}
break;
case "READ_CH10_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 10;
}
else
{
refAddr  = 20;
}
break;
case "READ_CH11_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 11;
}
else
{
refAddr  = 22;
}
break;
case "READ_CH12_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 12;
}
else
{
refAddr  = 24;
}
break;
case "READ_CH13_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 13;
}
else
{
refAddr  = 26;
}
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "CLEAR_CH6_DI_COUNTER":
functionCode = 0x05;
refAddr  = 518;
break;
case "CLEAR_CH7_DI_COUNTER":
functionCode = 0x05;
refAddr  = 519;
break;
case "CLEAR_CH8_DI_COUNTER":
functionCode = 0x05;
refAddr  = 520;
break;
case "CLEAR_CH9_DI_COUNTER":
functionCode = 0x05;
refAddr  = 521;
break;
case "CLEAR_CH10_DI_COUNTER":
functionCode = 0x05;
refAddr  = 522;
break;
case "CLEAR_CH11_DI_COUNTER":
functionCode = 0x05;
refAddr  = 523;
break;
case "CLEAR_CH12_DI_COUNTER":
functionCode = 0x05;
refAddr  = 524;
break;
case "CLEAR_CH13_DI_COUNTER":
functionCode = 0x05;
refAddr  = 525;
break;
}
break;
#endregion ZT-2052
#region ZT-2055/Z2055 8DI/8DO
case "ZT-2055":
case "Z2055":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_32Bit_DICnt":
functionCode = 0x05; // DO
refAddr  = 266;
break;
case "GET_32Bit_DICnt":
functionCode = 0x01; // DO
refAddr  = 266;
break;
case "SET_ZB_RF_POWER":
functionCode = 0x06;
refAddr  = 502;
break;
case "GET_ZB_RF_POWER":
functionCode = 0x03;
refAddr  = 502;
break;
case "SET_RF_ENABLE_ENCRYPTION":
functionCode = 0x05;
refAddr  = 287;
break;
case "GET_RF_ENCRYPTION":
functionCode = 0x02;
refAddr  = 286;
break;
case "GET_RF_ENABLE_ENCRYPTION":
functionCode = 0x02;
refAddr  = 287;
break;
case "READ_DIP_SWITCH":
functionCode = 0x01;
refAddr  = 320;
break;
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 0;
}
else
{
refAddr  = 0;
}
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 1;
}
else
{
refAddr  = 2;
}
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 2;
}
else
{
refAddr  = 4;
}
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 3;
}
else
{
refAddr  = 6;
}
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 4;
}
else
{
refAddr  = 8;
}
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 5;
}
else
{
refAddr  = 10;
}
break;
case "READ_CH6_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 6;
}
else
{
refAddr  = 12;
}
break;
case "READ_CH7_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 7;
}
else
{
refAddr  = 14;
}
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "CLEAR_CH6_DI_COUNTER":
functionCode = 0x05;
refAddr  = 518;
break;
case "CLEAR_CH7_DI_COUNTER":
functionCode = 0x05;
refAddr  = 519;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "OUTPUT_CH4_DO_BIT":
functionCode = 0x05;
refAddr  = 4;
break;
case "OUTPUT_CH5_DO_BIT":
functionCode = 0x05;
refAddr  = 5;
break;
case "OUTPUT_CH6_DO_BIT":
functionCode = 0x05;
refAddr  = 6;
break;
case "OUTPUT_CH7_DO_BIT":
functionCode = 0x05;
refAddr  = 7;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion ZT-2055
#region ZT-2060/2060 4DI/4DO
case "ZT-2060":
case "2060":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_32Bit_DICnt":
functionCode = 0x05; // DO
refAddr  = 266;
break;
case "GET_32Bit_DICnt":
functionCode = 0x01; // DO
refAddr  = 266;
break;
case "SET_ZB_RF_POWER":
functionCode = 0x06;
refAddr  = 502;
break;
case "GET_ZB_RF_POWER":
functionCode = 0x03;
refAddr  = 502;
break;
case "SET_RF_ENABLE_ENCRYPTION":
functionCode = 0x05;
refAddr  = 287;
break;
case "GET_RF_ENCRYPTION":
functionCode = 0x02;
refAddr  = 286;
break;
case "GET_RF_ENABLE_ENCRYPTION":
functionCode = 0x02;
refAddr  = 287;
break;
case "READ_DIP_SWITCH":
functionCode = 0x01;
refAddr  = 320;
break;
case "SET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_DI_REVERSE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 0;
}
else
{
refAddr  = 0;
}
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 1;
}
else
{
refAddr  = 2;
}
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 2;
}
else
{
refAddr  = 4;
}
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 3;
}
else
{
refAddr  = 6;
}
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 4;
}
else
{
refAddr  = 8;
}
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x04;
if (io.module.dioConfig[2] == 0)
{
refAddr  = 5;
}
else
{
refAddr  = 10;
}
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
case "OUTPUT_CH1_DO_BIT":
functionCode = 0x05;
refAddr  = 1;
break;
case "OUTPUT_CH2_DO_BIT":
functionCode = 0x05;
refAddr  = 2;
break;
case "OUTPUT_CH3_DO_BIT":
functionCode = 0x05;
refAddr  = 3;
break;
case "SET_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_DO_POWER_ON":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_DO_SAFE_VALUE":
functionCode = 0x0F;
refAddr  = 128;
break;
case "READ_DO_SAFE_VALUE":
functionCode = 0x01;
refAddr  = 128;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion ZT-2060
#region M2017
case "M2017":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODBUS_MISC":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
}
break;
#endregion M2017
#region DDC6026
case "DDC6026":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODBUS_DATAFORMAT":
functionCode = 0x05;
refAddr  = 268;
break;
case "GET_MODBUS_DATAFORMAT":
functionCode = 0x01;
refAddr  = 268;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_CHANNEL_ENABLE_STATUS":
functionCode = 0x6;
refAddr  = 489;
break;
case "GET_CHANNEL_ENABLE_STATUS":
functionCode = 0x3;
refAddr  = 489;
break;
case "GET_CH0_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 256;
break;
case "SET_CH0_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 256;
break;
case "GET_CH1_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 257;
break;
case "SET_CH1_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 257;
break;
case "GET_CH2_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 258;
break;
case "SET_CH2_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 258;
break;
case "GET_CH3_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 259;
break;
case "SET_CH3_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 259;
break;
case "GET_CH4_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 260;
break;
case "SET_CH4_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 260;
break;
case "GET_CH5_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 261;
break;
case "SET_CH5_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 261;
break;
case "GET_CH6_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 262;
break;
case "SET_CH6_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 262;
break;
case "GET_CH7_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 263;
break;
case "SET_CH7_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 263;
break;
case "GET_CH8_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 264;
break;
case "SET_CH8_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 264;
break;
case "GET_CH9_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 265;
break;
case "SET_CH9_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 265;
break;
case "GET_CH10_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 266;
break;
case "SET_CH10_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 266;
break;
case "GET_CH11_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 267;
break;
case "SET_CH11_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 267;
break;
case "GET_CH12_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 268;
break;
case "SET_CH12_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 268;
break;
case "GET_CH13_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 269;
break;
case "SET_CH13_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 269;
break;
case "GET_CH14_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 270;
break;
case "SET_CH14_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 270;
break;
case "GET_CH15_INPUT_RANGE":
functionCode = 0x03;
refAddr  = 271;
break;
case "SET_CH15_INPUT_RANGE":
functionCode = 0x06;
refAddr  = 271;
break;
case "READ_CH0_AI":
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI":
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI":
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI":
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI":
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI":
functionCode = 0x04;
refAddr  = 5;
break;
case "READ_CH6_AI":
functionCode = 0x04;
refAddr  = 6;
break;
case "READ_CH7_AI":
functionCode = 0x04;
refAddr  = 7;
break;
case "READ_CH8_AI":
functionCode = 0x04;
refAddr  = 8;
break;
case "READ_CH9_AI":
functionCode = 0x04;
refAddr  = 9;
break;
case "READ_CH10_AI":
functionCode = 0x04;
refAddr  = 10;
break;
case "READ_CH11_AI":
functionCode = 0x04;
refAddr  = 11;
break;
case "READ_CH12_AI":
functionCode = 0x04;
refAddr  = 12;
break;
case "READ_CH13_AI":
functionCode = 0x04;
refAddr  = 13;
break;
case "READ_CH14_AI":
functionCode = 0x04;
refAddr  = 14;
break;
case "READ_CH15_AI":
functionCode = 0x04;
refAddr  = 15;
break;
case "SET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 448;
break;
case "SET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 449;
break;
case "SET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 450;
break;
case "SET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 451;
break;
case "SET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 452;
break;
case "SET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 453;
break;
case "SET_CH6_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 454;
break;
case "SET_CH7_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 455;
break;
case "SET_CH8_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 456;
break;
case "SET_CH9_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 457;
break;
case "SET_CH10_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 458;
break;
case "SET_CH11_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 459;
break;
case "SET_CH12_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 460;
break;
case "SET_CH13_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 461;
break;
case "SET_CH14_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 462;
break;
case "SET_CH15_TEMPERATURE_OFFSET":
functionCode = 0x06;
refAddr  = 463;
break;
case "GET_CH0_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 448;
break;
case "GET_CH1_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 449;
break;
case "GET_CH2_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 450;
break;
case "GET_CH3_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 451;
break;
case "GET_CH4_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 452;
break;
case "GET_CH5_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 453;
break;
case "GET_CH6_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 454;
break;
case "GET_CH7_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 455;
break;
case "GET_CH8_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 456;
break;
case "GET_CH9_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 457;
break;
case "GET_CH10_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 458;
break;
case "GET_CH11_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 459;
break;
case "GET_CH12_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 460;
break;
case "GET_CH13_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 461;
break;
case "GET_CH14_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 462;
break;
case "GET_CH15_TEMPERATURE_OFFSET":
functionCode = 0x03;
refAddr  = 463;
break;
case "READ_DI":
functionCode = 0x01;
refAddr  = 32;
break;
case "READ_DI_HIGH_LATCH":
functionCode = 0x01;
refAddr  = 64;
break;
case "READ_DI_LOW_LATCH":
functionCode = 0x01;
refAddr  = 96;
break;
case "CLEAR_DI_LATCH":
functionCode = 0x05;
refAddr  = 263;
break;
case "READ_CH0_DI_COUNTER":
functionCode = 0x04;
refAddr  = 128;
break;
case "READ_CH1_DI_COUNTER":
functionCode = 0x04;
refAddr  = 130;
break;
case "READ_CH2_DI_COUNTER":
functionCode = 0x04;
refAddr  = 132;
break;
case "READ_CH3_DI_COUNTER":
functionCode = 0x04;
refAddr  = 134;
break;
case "READ_CH4_DI_COUNTER":
functionCode = 0x04;
refAddr  = 136;
break;
case "READ_CH5_DI_COUNTER":
functionCode = 0x04;
refAddr  = 138;
break;
case "READ_CH6_DI_COUNTER":
functionCode = 0x04;
refAddr  = 140;
break;
case "READ_CH7_DI_COUNTER":
functionCode = 0x04;
refAddr  = 142;
break;
case "READ_CH8_DI_COUNTER":
functionCode = 0x04;
refAddr  = 144;
break;
case "READ_CH9_DI_COUNTER":
functionCode = 0x04;
refAddr  = 146;
break;
case "READ_CH10_DI_COUNTER":
functionCode = 0x04;
refAddr  = 148;
break;
case "READ_CH11_DI_COUNTER":
functionCode = 0x04;
refAddr  = 150;
break;
case "READ_CH12_DI_COUNTER":
functionCode = 0x04;
refAddr  = 152;
break;
case "READ_CH13_DI_COUNTER":
functionCode = 0x04;
refAddr  = 154;
break;
case "READ_CH14_DI_COUNTER":
functionCode = 0x04;
refAddr  = 156;
break;
case "READ_CH15_DI_COUNTER":
functionCode = 0x04;
refAddr  = 158;
break;
case "CLEAR_CH0_DI_COUNTER":
functionCode = 0x05;
refAddr  = 512;
break;
case "CLEAR_CH1_DI_COUNTER":
functionCode = 0x05;
refAddr  = 513;
break;
case "CLEAR_CH2_DI_COUNTER":
functionCode = 0x05;
refAddr  = 514;
break;
case "CLEAR_CH3_DI_COUNTER":
functionCode = 0x05;
refAddr  = 515;
break;
case "CLEAR_CH4_DI_COUNTER":
functionCode = 0x05;
refAddr  = 516;
break;
case "CLEAR_CH5_DI_COUNTER":
functionCode = 0x05;
refAddr  = 517;
break;
case "CLEAR_CH6_DI_COUNTER":
functionCode = 0x05;
refAddr  = 518;
break;
case "CLEAR_CH7_DI_COUNTER":
functionCode = 0x05;
refAddr  = 519;
break;
case "CLEAR_CH8_DI_COUNTER":
functionCode = 0x05;
refAddr  = 520;
break;
case "CLEAR_CH9_DI_COUNTER":
functionCode = 0x05;
refAddr  = 521;
break;
case "CLEAR_CH10_DI_COUNTER":
functionCode = 0x05;
refAddr  = 522;
break;
case "CLEAR_CH11_DI_COUNTER":
functionCode = 0x05;
refAddr  = 523;
break;
case "CLEAR_CH12_DI_COUNTER":
functionCode = 0x05;
refAddr  = 524;
break;
case "CLEAR_CH13_DI_COUNTER":
functionCode = 0x05;
refAddr  = 525;
break;
case "CLEAR_CH14_DI_COUNTER":
functionCode = 0x05;
refAddr  = 526;
break;
case "CLEAR_CH15_DI_COUNTER":
functionCode = 0x05;
refAddr  = 527;
break;
case "OUTPUT_CH0_AO":
functionCode = 0x06;
refAddr  = 32;
break;
case "OUTPUT_CH1_AO":
functionCode = 0x06;
refAddr  = 33;
break;
case "OUTPUT_CH2_AO":
functionCode = 0x06;
refAddr  = 34;
break;
case "OUTPUT_CH3_AO":
functionCode = 0x06;
refAddr  = 35;
break;
case "OUTPUT_CH4_AO":
functionCode = 0x06;
refAddr  = 36;
break;
case "OUTPUT_CH5_AO":
functionCode = 0x06;
refAddr  = 37;
break;
case "OUTPUT_CH6_AO":
functionCode = 0x06;
refAddr  = 38;
break;
case "OUTPUT_CH7_AO":
functionCode = 0x06;
refAddr  = 39;
break;
case "OUTPUT_CH8_AO":
functionCode = 0x06;
refAddr  = 40;
break;
case "OUTPUT_CH9_AO":
functionCode = 0x06;
refAddr  = 41;
break;
case "OUTPUT_CH10_AO":
functionCode = 0x06;
refAddr  = 42;
break;
case "OUTPUT_CH11_AO":
functionCode = 0x06;
refAddr  = 43;
break;
case "OUTPUT_CH12_AO":
functionCode = 0x06;
refAddr  = 44;
break;
case "OUTPUT_CH13_AO":
functionCode = 0x06;
refAddr  = 45;
break;
case "OUTPUT_CH14_AO":
functionCode = 0x06;
refAddr  = 46;
break;
case "OUTPUT_CH15_AO":
functionCode = 0x06;
refAddr  = 47;
break;
case "READ_CH0_LAST_AO":
functionCode = 0x03;
refAddr  = 32;
break;
case "READ_CH1_LAST_AO":
functionCode = 0x03;
refAddr  = 33;
break;
case "READ_CH2_LAST_AO":
functionCode = 0x03;
refAddr  = 34;
break;
case "READ_CH3_LAST_AO":
functionCode = 0x03;
refAddr  = 35;
break;
case "READ_CH4_LAST_AO":
functionCode = 0x03;
refAddr  = 36;
break;
case "READ_CH5_LAST_AO":
functionCode = 0x03;
refAddr  = 37;
break;
case "READ_CH6_LAST_AO":
functionCode = 0x03;
refAddr  = 38;
break;
case "READ_CH7_LAST_AO":
functionCode = 0x03;
refAddr  = 39;
break;
case "READ_CH8_LAST_AO":
functionCode = 0x03;
refAddr  = 40;
break;
case "READ_CH9_LAST_AO":
functionCode = 0x03;
refAddr  = 41;
break;
case "READ_CH10_LAST_AO":
functionCode = 0x03;
refAddr  = 42;
break;
case "READ_CH11_LAST_AO":
functionCode = 0x03;
refAddr  = 43;
break;
case "READ_CH12_LAST_AO":
functionCode = 0x03;
refAddr  = 44;
break;
case "READ_CH13_LAST_AO":
functionCode = 0x03;
refAddr  = 45;
break;
case "READ_CH14_LAST_AO":
functionCode = 0x03;
refAddr  = 46;
break;
case "READ_CH15_LAST_AO":
functionCode = 0x03;
refAddr  = 47;
break;
case "READ_CH0_AO":
functionCode = 0x03;
refAddr  = 64;
break;
case "READ_CH1_AO":
functionCode = 0x03;
refAddr  = 65;
break;
case "READ_CH2_AO":
functionCode = 0x03;
refAddr  = 66;
break;
case "READ_CH3_AO":
functionCode = 0x03;
refAddr  = 67;
break;
case "READ_CH4_AO":
functionCode = 0x03;
refAddr  = 68;
break;
case "READ_CH5_AO":
functionCode = 0x03;
refAddr  = 69;
break;
case "READ_CH6_AO":
functionCode = 0x03;
refAddr  = 70;
break;
case "READ_CH7_AO":
functionCode = 0x03;
refAddr  = 71;
break;
case "READ_CH8_AO":
functionCode = 0x03;
refAddr  = 72;
break;
case "READ_CH9_AO":
functionCode = 0x03;
refAddr  = 73;
break;
case "READ_CH10_AO":
functionCode = 0x03;
refAddr  = 74;
break;
case "READ_CH11_AO":
functionCode = 0x03;
refAddr  = 75;
break;
case "READ_CH12_AO":
functionCode = 0x03;
refAddr  = 76;
break;
case "READ_CH13_AO":
functionCode = 0x03;
refAddr  = 77;
break;
case "READ_CH14_AO":
functionCode = 0x03;
refAddr  = 78;
break;
case "READ_CH15_AO":
functionCode = 0x03;
refAddr  = 79;
break;
case "SET_CH0_AO_POWERON":
functionCode = 0x06;
refAddr  = 192;
break;
case "SET_CH1_AO_POWERON":
functionCode = 0x06;
refAddr  = 193;
break;
case "SET_CH2_AO_POWERON":
functionCode = 0x06;
refAddr  = 194;
break;
case "SET_CH3_AO_POWERON":
functionCode = 0x06;
refAddr  = 195;
break;
case "SET_CH4_AO_POWERON":
functionCode = 0x06;
refAddr  = 196;
break;
case "SET_CH5_AO_POWERON":
functionCode = 0x06;
refAddr  = 197;
break;
case "SET_CH6_AO_POWERON":
functionCode = 0x06;
refAddr  = 198;
break;
case "SET_CH7_AO_POWERON":
functionCode = 0x06;
refAddr  = 199;
break;
case "SET_CH8_AO_POWERON":
functionCode = 0x06;
refAddr  = 200;
break;
case "SET_CH9_AO_POWERON":
functionCode = 0x06;
refAddr  = 201;
break;
case "SET_CH10_AO_POWERON":
functionCode = 0x06;
refAddr  = 202;
break;
case "SET_CH11_AO_POWERON":
functionCode = 0x06;
refAddr  = 203;
break;
case "SET_CH12_AO_POWERON":
functionCode = 0x06;
refAddr  = 204;
break;
case "SET_CH13_AO_POWERON":
functionCode = 0x06;
refAddr  = 205;
break;
case "SET_CH14_AO_POWERON":
functionCode = 0x06;
refAddr  = 206;
break;
case "SET_CH15_AO_POWERON":
functionCode = 0x06;
refAddr  = 207;
break;
case "READ_CH0_AO_POWERON":
functionCode = 0x03;
refAddr  = 192;
break;
case "READ_CH1_AO_POWERON":
functionCode = 0x03;
refAddr  = 193;
break;
case "READ_CH2_AO_POWERON":
functionCode = 0x03;
refAddr  = 194;
break;
case "READ_CH3_AO_POWERON":
functionCode = 0x03;
refAddr  = 195;
break;
case "READ_CH4_AO_POWERON":
functionCode = 0x03;
refAddr  = 196;
break;
case "READ_CH5_AO_POWERON":
functionCode = 0x03;
refAddr  = 197;
break;
case "READ_CH6_AO_POWERON":
functionCode = 0x03;
refAddr  = 198;
break;
case "READ_CH7_AO_POWERON":
functionCode = 0x03;
refAddr  = 199;
break;
case "READ_CH8_AO_POWERON":
functionCode = 0x03;
refAddr  = 200;
break;
case "READ_CH9_AO_POWERON":
functionCode = 0x03;
refAddr  = 201;
break;
case "READ_CH10_AO_POWERON":
functionCode = 0x03;
refAddr  = 202;
break;
case "READ_CH11_AO_POWERON":
functionCode = 0x03;
refAddr  = 203;
break;
case "READ_CH12_AO_POWERON":
functionCode = 0x03;
refAddr  = 204;
break;
case "READ_CH13_AO_POWERON":
functionCode = 0x03;
refAddr  = 205;
break;
case "READ_CH14_AO_POWERON":
functionCode = 0x03;
refAddr  = 206;
break;
case "READ_CH15_AO_POWERON":
functionCode = 0x03;
refAddr  = 207;
break;
case "SET_CH0_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 96;
break;
case "SET_CH1_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 97;
break;
case "SET_CH2_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 98;
break;
case "SET_CH3_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 99;
break;
case "SET_CH4_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 100;
break;
case "SET_CH5_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 101;
break;
case "SET_CH6_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 102;
break;
case "SET_CH7_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 103;
break;
case "SET_CH8_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 104;
break;
case "SET_CH9_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 105;
break;
case "SET_CH10_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 106;
break;
case "SET_CH11_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 107;
break;
case "SET_CH12_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 108;
break;
case "SET_CH13_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 109;
break;
case "SET_CH14_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 110;
break;
case "SET_CH15_AO_SAFE_VALUE":
functionCode = 0x06;
refAddr  = 111;
break;
case "READ_CH0_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 96;
break;
case "READ_CH1_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 97;
break;
case "READ_CH2_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 98;
break;
case "READ_CH3_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 99;
break;
case "READ_CH4_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 100;
break;
case "READ_CH5_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 101;
break;
case "READ_CH6_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 102;
break;
case "READ_CH7_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 103;
break;
case "READ_CH8_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 104;
break;
case "READ_CH9_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 105;
break;
case "READ_CH10_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 106;
break;
case "READ_CH11_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 107;
break;
case "READ_CH12_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 108;
break;
case "READ_CH13_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 109;
break;
case "READ_CH14_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 110;
break;
case "READ_CH15_AO_SAFE_VALUE":
functionCode = 0x03;
refAddr  = 111;
break;
case "SET_CH0_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 416;
break;
case "SET_CH1_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 417;
break;
case "SET_CH2_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 418;
break;
case "SET_CH3_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 419;
break;
case "SET_CH4_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 420;
break;
case "SET_CH5_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 421;
break;
case "SET_CH6_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 422;
break;
case "SET_CH7_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 423;
break;
case "SET_CH8_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 424;
break;
case "SET_CH9_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 425;
break;
case "SET_CH10_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 426;
break;
case "SET_CH11_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 427;
break;
case "SET_CH12_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 428;
break;
case "SET_CH13_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 429;
break;
case "SET_CH14_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 430;
break;
case "SET_CH15_AO_TYPE_CODE":
functionCode = 0x06;
refAddr  = 431;
break;
case "GET_CH0_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 416;
break;
case "GET_CH1_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 417;
break;
case "GET_CH2_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 418;
break;
case "GET_CH3_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 419;
break;
case "GET_CH4_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 420;
break;
case "GET_CH5_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 421;
break;
case "GET_CH6_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 422;
break;
case "GET_CH7_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 423;
break;
case "GET_CH8_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 424;
break;
case "GET_CH9_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 425;
break;
case "GET_CH10_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 426;
break;
case "GET_CH11_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 427;
break;
case "GET_CH12_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 428;
break;
case "GET_CH13_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 429;
break;
case "GET_CH14_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 430;
break;
case "GET_CH15_AO_TYPE_CODE":
functionCode = 0x03;
refAddr  = 431;
break;
case "SET_CH0_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 288;
break;
case "SET_CH1_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 289;
break;
case "SET_CH2_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 290;
break;
case "SET_CH3_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 291;
break;
case "SET_CH4_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 292;
break;
case "SET_CH5_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 293;
break;
case "SET_CH6_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 294;
break;
case "SET_CH7_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 295;
break;
case "SET_CH8_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 296;
break;
case "SET_CH9_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 297;
break;
case "SET_CH10_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 298;
break;
case "SET_CH11_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 299;
break;
case "SET_CH12_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 300;
break;
case "SET_CH13_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 301;
break;
case "SET_CH14_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 302;
break;
case "SET_CH15_AO_SLEW_RATE":
functionCode = 0x06;
refAddr  = 303;
break;
case "GET_CH0_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 288;
break;
case "GET_CH1_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 289;
break;
case "GET_CH2_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 290;
break;
case "GET_CH3_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 291;
break;
case "GET_CH4_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 292;
break;
case "GET_CH5_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 293;
break;
case "GET_CH6_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 294;
break;
case "GET_CH7_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 295;
break;
case "GET_CH8_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 296;
break;
case "GET_CH9_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 297;
break;
case "GET_CH10_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 298;
break;
case "GET_CH11_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 299;
break;
case "GET_CH12_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 300;
break;
case "GET_CH13_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 301;
break;
case "GET_CH14_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 302;
break;
case "GET_CH15_AO_SLEW_RATE":
functionCode = 0x03;
refAddr  = 303;
break;
// WDT function
case "SET_WDT_TIMER":
functionCode = 0x06;
refAddr  = 488;
break;
case "GET_WDT_TIMER":
functionCode = 0x03;
refAddr  = 488;
break;
case "SET_WDT_ENABLE":
functionCode = 0x0F;
refAddr  = 259;
break;
case "GET_WDT_ENABLE":
functionCode = 0x01;
refAddr  = 260;
break;
case "READ_WDT_STATUS":
functionCode = 0x01;
refAddr  = 269;
break;
case "CLEAR_WDT_ALARM":
functionCode = 0x05;
refAddr  = 269;
break;
}
break;
#endregion DDC6026
#region 7004PTO
case "7004PTO":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_TYPECODE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
// AO funciton
case "SET_CH0_PTO":
functionCode = 0x06;
refAddr  = 32;
break;
case "SET_CH1_PTO":
functionCode = 0x06;
refAddr  = 33;
break;
case "SET_CH2_PTO":
functionCode = 0x06;
refAddr  = 34;
break;
case "SET_CH3_PTO":
functionCode = 0x06;
refAddr  = 35;
break;
case "GET_CH0_PTO":
functionCode = 0x03;
refAddr  = 32;
break;
case "GET_CH1_PTO":
functionCode = 0x03;
refAddr  = 33;
break;
case "GET_CH2_PTO":
functionCode = 0x03;
refAddr  = 34;
break;
case "GET_CH3_PTO":
functionCode = 0x03;
refAddr  = 35;
break;
}
break;
#endregion 7004PTO
#region tSL-P4R1
case "tSL-P4R1":
case "tSL-PA4R1":
hasCmd = false;
switch (cmdTitle)
{
case "GET_MODULE_NAME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_MODULE_FIRMWARE":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_COMMUNICATE_PARAMETER":
functionCode = 0x46;
refAddr  = 65535;
break;
case "SET_MODULE_ADDRESS":
functionCode = 0x06;
refAddr  = 484;
break;
case "GET_MODULE_ADDRESS":
functionCode = 0x03;
refAddr  = 484;
break;
case "SET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "GET_RESPONSE_DELAY_TIME":
functionCode = 0x46;
refAddr  = 65535;
break;
case "READ_CH0_AI":  //Status of the monitored stack light of channel 0 to 3, 0=off, 1=on,2=blinking
functionCode = 0x04;
refAddr  = 0;
break;
case "READ_CH1_AI"://Status of the monitored stack light of channel 0 to 3, 0=off, 1=on,2=blinking
functionCode = 0x04;
refAddr  = 1;
break;
case "READ_CH2_AI"://Status of the monitored stack light of channel 0 to 3, 0=off, 1=on,2=blinking
functionCode = 0x04;
refAddr  = 2;
break;
case "READ_CH3_AI"://Status of the monitored stack light of channel 0 to 3, 0=off, 1=on,2=blinking
functionCode = 0x04;
refAddr  = 3;
break;
case "READ_CH4_AI": //Combinatorial status of the monitored stack lights
functionCode = 0x04;
refAddr  = 4;
break;
case "READ_CH5_AI": //Time of the previous combinatorial status of the monitored stack lights in seconds
functionCode = 0x04;
refAddr  = 5;
break;
case "GET_DI_FILTER": //Digital filter time in ms, 0 to 6500 Only for tSL-P4R1
functionCode = 0x04;
refAddr  = 201;
break;
case "SET_DI_FILTER":
functionCode = 0x06;
refAddr  = 201;
break;
case "GET_CHANGE_INTERVAL": //Time interval to check is there any on/off change in millisecond.  It should be multiple of 10ms
functionCode = 0x04;
refAddr  = 288;
break;
case "SET_CHANGE_INTERVAL":
functionCode = 0x06;
refAddr  = 288;
break;
case "GET_CHANGE_COUNT": //Number of the above time interval to check the number of on/off change
functionCode = 0x04;
refAddr  = 288;
break;
case "SET_CHANGE_COUNT": //Number of the above time interval to check the number of on/off change
functionCode = 0x06;
refAddr  = 288;
break;
// DO function
case "READ_DO":
functionCode = 0x01;
refAddr  = 0;
break;
case "OUTPUT_CH0_DO_BIT":
functionCode = 0x05;
refAddr  = 0;
break;
}
break;
#endregion tSL-P4R1,
}
if (functionCode != 0xff)
{
if (functionCode != 0x46)
refAddr = (UInt16)(refAddr - 1);
hasCmd = true;
}
else
{
Document.TLog(io.module.ID, p.cmdName + " " + "not in support list");
}
return hasCmd;
}
