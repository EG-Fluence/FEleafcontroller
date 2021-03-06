switch(cmdTitle)
{
	case "CLEAR_ALARM_LATCH":
		switch(io.module.ID)
		{
				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			//[05_263]
			break;
				
			case "PIR130":
			case "PIR230":
			//[05_304]
			break;
		}
	break;
	case "CLEAR_CH0_COUNTER":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[05_132]
			break;
				
			case "7088":
			//[05_64]
			break;
		}
	break;
	case "CLEAR_CH0_DI_COUNTER":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7002":
			case "7024R":
			case "7024U":
			case "7026":
			case "7041":
			case "7041D":
			case "7044":
			case "7044D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "DDC6026":
			case "LC131":
			case "LC221":
			case "tAD4P2C2":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "XV107":
			case "XV110":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[05_512]
			break;
				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			//[05_265]
			break;
			
				
			case "Z2026":
			case "ZT-2026":
			//[05_768]
			break;
		}
	break;
	case "CLEAR_CH0_HIGH_ALARM_LATCH":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7005":
			case "7026":
			case "tAD4P2C2":
			//[05_304]
			break;
			
			case "7003":			
				//[05_704]
			break;
		}
	break;
	case "CLEAR_CH0_LOW_ALARM_LATCH":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7005":
			case "7026":
			case "tAD4P2C2":
			//[05_288]
			break;
				
			case "7003":
			//[05_736]
			break;
		}
	break;
	case "CLEAR_DI_ALARM_LATCH":
		switch(io.module.ID)
		{				
			case "LC131":
			//[05_281]
			break;
		}
	break;
	case "CLEAR_DI_LATCH":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7002":
			case "7024R":
			case "7024U":
			case "7026":
			case "DDC6026":
			case "LC131":
			case "LC221":
			case "tAD4P2C2":
			case "tDA1P1R1":
			case "XV107":
			case "XV110":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			case "Z2026":
			case "ZT-2026":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[05_263]
			break;
				
			case "7041":
			case "7041D":
			case "7044":
			case "7044D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			//[05_256]
			break;
		}
	break;
	case "CLEAR_WDT_ALARM":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7002":
			case "7003":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7088":
			case "DDC6026":
			case "GPS721":
			case "LC131":
			case "LC221":
			case "SC4104":
			case "SC6104":
			case "tAD4P2C2":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tR5":
			case "Z2024":
			case "Z2026":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2055":
			case "ZT-2060":
			//[05_269]
			break;
		}
	break;
	case "GET_32Bit_DICnt":
		switch(io.module.ID)
		{
				
			case "2060":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[01_266]
			break;
		}
	break;
	case "GET_BATTERY_BACKUP_ENABLE_STATUS":
		switch(io.module.ID)
		{
				
			case "7024U":
			case "7028":
			case "7084":
			//[01_768]
			break;
		}
	break;
	case "GET_CF_ALARM_MODE":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[01_139]
			break;
		}
	break;
	case "GET_CF_XOR":
		switch(io.module.ID)
		{
				
			case "7084":
			//[01_928]
			break;
		}
	break;
	case "GET_CH0_AI_ALARM_ENABLE_STATUS":
		switch(io.module.ID)
		{
				
			case "7026":
			case "tAD4P2C2":
			//[01_320]
			break;
		}
	break;
	case "GET_CH0_AO_SLEW_RATE":
		switch(io.module.ID)
		{
				
			case "7022":
			case "7022A":
			//[46_51]
			break;
				
			case "7024U":
			case "7026":
			case "7028":
			case "DDC6026":
			case "LC221":
			case "tDA1P1R1":
			case "XV307":
			case "XV310":
			case "Z2024":
			case "Z2026":
			case "ZT-2024":
			case "ZT-2026":
			//[03_288]
			break;
		}
	break;
	case "GET_CH0_AO_TYPE_CODE":
		switch(io.module.ID)
		{
				
			case "7022":
			case "7022A":
			//[46_7]
			break;
				
			case "7024U":
			case "7028":
			//[03_256]
			break;
				
			case "7026":
			case "DDC6026":
			case "LC221":
			case "tDA1P1R1":
			case "XV307":
			case "XV310":
			case "Z2024":
			case "Z2026":
			case "ZT-2024":
			case "ZT-2026":
			//[03_416]
			break;
		}
	break;
	case "GET_CH0_CF_ALARM_VALUE":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[03_128]
			break;
		}
	break;
	case "GET_CH0_CJC_OFFSET":
		switch(io.module.ID)
		{				
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "Z2018":
			case "ZT-2018":
			//[46_43]
			break;
		}
	break;
	case "GET_CH0_COUNTER_ENABLE_STATUS":
		switch(io.module.ID)
		{				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[01_134]
			break;
		}
	break;
	case "GET_CH0_DI_TRIGGER_STATE":
		switch(io.module.ID)
		{				
			case "7088":
			//[01_928]
			break;
		}
	break;
	case "GET_CH0_ENABLE_DI_TRIGGER":
		switch(io.module.ID)
		{				
			case "7088":
			//[01_896]
			break;
		}
	break;
	case "GET_CH0_HIGH_ALARM_DOCH":
		switch(io.module.ID)
		{				
			case "7005":
			case "Z2026":
			case "ZT-2026":
			//[03_320]
			break;
		}
	break;
	case "GET_CH0_HIGH_ALARM_ENABLE_STATUS":
		switch(io.module.ID)
		{				
			case "7005":
			//[01_320]
			break;
				
			case "Z2026":
			case "ZT-2026":
			//[01_576]
			break;
		}
	break;
	case "GET_CH0_HIGH_ALARM_LIMIT":
		switch(io.module.ID)
		{
				
			case "7005":
			//[03_224]
			break;
				
			case "Z2026":
			case "ZT-2026":
			//[03_576]
			break;
		}
	break;
	case "GET_CH0_HIGH_ALARM_MODE":
		switch(io.module.ID)
		{
				
			case "7005":
			//[01_336]
			break;
				
			case "Z2026":
			case "ZT-2026":
			//[01_640]
			break;
		}
	break;
	case "GET_CH0_INPUT_RANGE":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7003":
			case "7005":
			case "7015":
			case "7015P":
			case "7017mC16":
			case "7017Z":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7084":
			case "M2017":
			case "tAD4P2C2":
			case "tTH8":
			case "XV306":
			case "XV308":
			case "XV310":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			//[46_7]
			break;
				
			case "7017R":
			case "7017RM":
			case "7018R":
			case "7026":
			case "DDC6026":
			case "Z2018":
			case "Z2026":
			case "ZT-2018":
			case "ZT-2026":
			//[03_256]
			break;
		}
	break;
	case "GET_CH0_INPUT_SIGNAL":
		switch(io.module.ID)
		{				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[02_128]
			break;
		}
	break;
	case "GET_CH0_LINEAR_MAP_CONFIG":
		switch(io.module.ID)
		{				
			case "7016":
			//[03_160]
			break;
		}
	break;
	case "GET_CH0_LOW_ALARM_DOCH":
		switch(io.module.ID)
		{				
			case "7005":
			case "Z2026":
			case "ZT-2026":
			//[03_328]
			break;
		}
	break;
	case "GET_CH0_LOW_ALARM_ENABLE_STATUS":
		switch(io.module.ID)
		{
				
			case "7005":
			//[01_328]
			break;
				
			case "Z2026":
			case "ZT-2026":
			//[01_608]
			break;
		}
	break;
	case "GET_CH0_LOW_ALARM_LIMIT":
		switch(io.module.ID)
		{
				
			case "7005":
			//[03_232]
			break;
				
			case "Z2026":
			case "ZT-2026":
			//[03_608]
			break;
		}
	break;
	case "GET_CH0_LOW_ALARM_MODE":
		switch(io.module.ID)
		{
				
			case "7005":
			//[01_344]
			break;
				
			case "Z2026":
			case "ZT-2026":
			//[01_672]
			break;
		}
	break;
	case "GET_CH0_LOW_PASS_FILTER":
		switch(io.module.ID)
		{				
			case "7084":
			//[03_161]
			break;
		}
	break;
	case "GET_CH0_MAX_COUNTER":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7084":
			case "7088":
			//[03_64]
			break;
		}
	break;
	case "GET_CH0_PRESET_COUNTER":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7084":
			case "7088":
			//[03_96]
			break;
		}
	break;
	case "GET_CH0_PTO":
		switch(io.module.ID)
		{
				
			case "7004PTO":
			//[03_32]
			break;
		}
	break;
	case "GET_CH0_PULSE_COUNT":
		switch(io.module.ID)
		{
				
			case "7088":
			//[03_800]
			break;
		}
	break;
	case "GET_CH0_PWM_DUTY":
		switch(io.module.ID)
		{				
			case "7088":
			//[03_704]
			break;
		}
	break;
	case "GET_CH0_PWM_FREQ":
		switch(io.module.ID)
		{				
			case "7088":
			//[03_736]
			break;
		}
	break;
	case "GET_CH0_PWM_PULSE_MODE":
		switch(io.module.ID)
		{
				
			case "7088":
			//[01_864]
			break;
		}
	break;
	case "GET_CH0_SIMPLE_AI_HIGH_ALARM_LIMIT":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7026":
			case "tAD4P2C2":
			//[03_224]
			break;
				
			case "7003":
			//[03_576]
			break;
		}
	break;
	case "GET_CH0_SIMPLE_AI_LOW_ALARM_LIMIT":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7026":
			case "tAD4P2C2":
			//[03_232]
			break;
				
			case "7003":
			//[03_608]
			break;
		}
	break;
	case "GET_CH0_SYNC_PWM":
		switch(io.module.ID)
		{				
			case "7088":
			//[01_960]
			break;
		}
	break;
	case "GET_CH0_TEMPERATURE_OFFSET":
		switch(io.module.ID)
		{
				
			case "7005":
			case "7015":
			case "7015P":
			case "7019":
			case "7019R":
			case "7019Z":
			case "SC4104":
			case "SC6104":
			case "Z2015":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			case "ZT-2015":
			//[03_288]
			break;
				
			case "DDC6026":
			case "tTH8":
			//[03_448]
			break;
		}
	break;
	case "GET_CHANNEL_ENABLE_STATUS":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7003":
			case "7005":
			case "7015":
			case "7015P":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017mC16":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7017Z":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7026":
			case "7088":
			case "M2017":
			case "tAD2":
			case "tAD4P2C2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			case "tTH8":
			case "XV306":
			case "XV308":
			case "XV310":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2026":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2026":
			//[46_37]
			break;
				
			case "DDC6026":
			//[03_489]
			break;
		}
	break;
	case "GET_CJC_ENABLE_STATUS":
		switch(io.module.ID)
		{
				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "Z2018":
			case "ZT-2018":
			//[46_45]
			break;
		}
	break;
	case "GET_COMMUNICATE_PARAMETER":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7002":
			case "7003":
			case "7004PTO":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7015":
			case "7015P":
			case "7016":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017mC16":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7017Z":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "7033":
			case "7041":
			case "7041D":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7084":
			case "7088":
			case "DDC6026":
			case "GPS721":
			case "LC101":
			case "LC131":
			case "LC221":
			case "M2017":
			case "PIR130":
			case "PIR230":
			case "SC4104":
			case "SC6104":
			case "tAD2":
			case "tAD4P2C2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "tR5":
			case "tSL-P4R1":
			case "tSL-PA4R1":
			case "tTH8":
			case "XV107":
			case "XV110":
			case "XV111":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2024":
			case "Z2026":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[46_5]
			break;
		}
	break;
	case "GET_COUNTER_ENABLE_STATUS":
		switch(io.module.ID)
		{
				
			case "7084":
			//[03_489]
			break;
		}
	break;
	case "GET_DI_CHANNELS_ALARM_CONFIG":
		switch(io.module.ID)
		{
				
			case "LC131":
			//[02_544]
			break;
		}
	break;
	case "GET_DI_CONNECTION":
		switch(io.module.ID)
		{
				
			case "LC101":
			//[01_273]
			break;
				
			case "SC4104":
			case "SC6104":
			//[03_273]
			break;
		}
	break;
	
	//42 (0x2A) Read the DI/O active states 3.7.11 
	case "GET_DI_REVERSE":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7041":
			case "7041D":
			case "7044":
			case "7044D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "LC131":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "XV107":
			case "XV110":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			case "tAD4P2C2":
			//[46_42]
			break;
				
			case "7002":
			case "7024R":
			case "7024U":
			case "7026":
			case "LC221":
			case "tDA1P1R1":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			//[01_264]
			break;
			
				
			case "7088":
			case "Z2026":
			case "ZT-2026":
			//[01_277]
			break;			
		}
	break;
	case "GET_DI_SHORT_CIRCUIT_ALARM_CONFIG":
		switch(io.module.ID)
		{
				
			case "LC131":
			//[02_552]
			break;
		}
	break;
	case "GET_DO_REVERSE":
		switch(io.module.ID)
		{
				
			case "XV107":
			case "XV111":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			//[01_265]
			break;
		}
	break;
	case "GET_ENABLE_THERMOCOUPLE_OPEN_DETECTION":
		switch(io.module.ID)
		{				
			case "7018R":
			//[01_275]
			break;
		}
	break;
	case "GET_FILTER_WIDTH":
		switch(io.module.ID)
		{
				
			case "7046":
			case "7051":
			case "7051D":
			case "7053":
			case "7053D":
			//[03_497]
			break;
		}
	break;
	case "GET_FREQ_MODE":
		switch(io.module.ID)
		{				
			case "7084":
			//[01_832]
			break;
		}
	break;
	case "GET_FREQ_TIMEOUT":
		switch(io.module.ID)
		{
				
			case "7084":
			//[03_160]
			break;
		}
	break;
	case "GET_GATE_MODE":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[01_130]
			break;
		}
	break;
	case "GET_HIGH_SIGNAL_WIDTH":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[03_161]
			break;
		}
	break;
	case "GET_HIGH_TRIGGER_LEVEL_VOLTAGE":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[03_163]
			break;
		}
	break;
	case "GET_LED_MODE":
		switch(io.module.ID)
		{
				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			//[03_494]
			break;
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[01_142]
			break;
		}
	break;
	case "GET_LINEAR_MODE_ENABLE_STATUS":
		switch(io.module.ID)
		{
				
			case "7016":
			//[01_264]
			break;
		}
	break;
	case "GET_LOG_SAMPLE_MODE":
		switch(io.module.ID)
		{
				
			case "7017mC16":
			case "LC221":
			//[04_875]
			break;
		}
	break;
	case "GET_LOW_PASS_FILTER_ENABLE_STATUS":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[01_138]
			break;
				
			case "7084":
			//[01_896]
			break;
		}
	break;
	case "GET_LOW_SIGNAL_WIDTH":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[03_160]
			break;
		}
	break;
	case "GET_LOW_TRIGGER_LEVEL_VOLTAGE":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[03_162]
			break;
		}
	break;
	case "GET_MODBUS_DATAFORMAT":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7003":
			case "7005":
			case "7015":
			case "7015P":
			case "7016":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017mC16":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7017Z":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7022":
			case "7022A":
			case "7024U":
			case "7026":
			case "7028":
			case "7084":
			case "DDC6026":
			case "LC221":
			case "M2017":
			case "tAD2":
			case "tAD4P2C2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			case "tTH8":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2024":
			case "Z2026":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2024":
			case "ZT-2026":
			//[01_268]
			break;
		}
	break;
	case "GET_MODBUS_MISC":
		switch(io.module.ID)
		{				
			case "7002":
			case "7003":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7015":
			case "7015P":
			case "7016":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017mC16":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7017Z":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7026":
			case "7033":
			case "M2017":
			case "SC4104":
			case "SC6104":
			case "tAD2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			case "tTH8":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2026":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2026":
			//[46_41]
			break;
				
			case "tAD4P2C2":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			//[01_270]
			break;
		}
	break;
	case "GET_MODULE_ADDRESS":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7002":
			case "7003":
			case "7004PTO":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7015":
			case "7015P":
			case "7016":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017mC16":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7017Z":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "7033":
			case "7041":
			case "7041D":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7084":
			case "7088":
			case "DDC6026":
			case "GPS721":
			case "LC101":
			case "LC131":
			case "LC221":
			case "M2017":
			case "PIR130":
			case "PIR230":
			case "SC4104":
			case "SC6104":
			case "tAD2":
			case "tAD4P2C2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "tR5":
			case "tSL-P4R1":
			case "tSL-PA4R1":
			case "tTH8":
			case "XV107":
			case "XV110":
			case "XV111":
			case "XV306":
			case "XV307":
			case "XV310":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2024":
			case "Z2026":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[03_484]
			break;
		}
	break;
	case "GET_MODULE_CJC_OFFSET":
		switch(io.module.ID)
		{
				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "Z2018":
			case "ZT-2018":
			//[46_43]
			break;
		}
	break;
	case "GET_MODULE_FIRMWARE":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7002":
			case "7003":
			case "7004PTO":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7015":
			case "7015P":
			case "7016":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017mC16":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7017Z":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "7033":
			case "7041":
			case "7041D":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7084":
			case "7088":
			case "DDC6026":
			case "GPS721":
			case "LC101":
			case "LC131":
			case "LC221":
			case "M2017":
			case "PIR130":
			case "PIR230":
			case "SC4104":
			case "SC6104":
			case "tAD2":
			case "tAD4P2C2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "tR5":
			case "tSL-P4R1":
			case "tSL-PA4R1":
			case "tTH8":
			case "XV107":
			case "XV110":
			case "XV111":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2024":
			case "Z2026":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[46_32]
			break;
		}
	break;
	case "GET_MODULE_NAME":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7002":
			case "7003":
			case "7004PTO":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7015":
			case "7015P":
			case "7016":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017mC16":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7017Z":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "7033":
			case "7041":
			case "7041D":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7084":
			case "7088":
			case "DDC6026":
			case "GPS721":
			case "LC101":
			case "LC131":
			case "LC221":
			case "M2017":
			case "PIR130":
			case "PIR230":
			case "SC4104":
			case "SC6104":
			case "tAD2":
			case "tAD4P2C2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "tR5":
			case "tSL-P4R1":
			case "tSL-PA4R1":
			case "tTH8":
			case "XV107":
			case "XV110":
			case "XV111":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2024":
			case "Z2026":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[46_0]
			break;
		}
	break;
	case "GET_MODULE_SLEWRATE":
		switch(io.module.ID)
		{
				
			case "7024":
			case "7024R":
			//[46_51]
			break;
		}
	break;
	case "GET_MODULE_TYPECODE":
		switch(io.module.ID)
		{
				
			case "7004PTO":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7018":
			case "701816":
			case "7018R":
			case "7024":
			case "7024R":
			case "7033":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7088":
			case "tAD2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			//[46_7]
			break;
		}
	break;
	case "GET_ON_DELAY_TIME":
		switch(io.module.ID)
		{
				
			case "LC101":
			case "SC4104":
			case "SC6104":
			//[03_497]
			break;
			
				
			case "PIR130":
			case "PIR230":
			//[03_515]
			break;
		}
	break;
	case "GET_PIR_BUZZER_MODE":
		switch(io.module.ID)
		{
				
			case "PIR130":
			case "PIR230":
			//[01_273]
			break;
		}
	break;
	case "GET_PIR_LUMINANCE":
		switch(io.module.ID)
		{
				
			case "PIR130":
			case "PIR230":
			//[03_513]
			break;
		}
	break;
	case "GET_PIR_ON_DELAY_TIME":
		switch(io.module.ID)
		{
				
			case "PIR130":
			case "PIR230":
			//[03_512]
			break;
		}
	break;
	case "GET_PIR_SENSITIVITY":
		switch(io.module.ID)
		{
				
			case "PIR130":
			case "PIR230":
			//[03_516]
			break;
		}
	break;
	case "GET_RESPONSE_DELAY_TIME":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7003":
			case "7004PTO":
			case "7005":
			case "7015":
			case "7015P":
			case "7016":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7028":
			case "7033":
			case "7041":
			case "7041D":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7084":
			case "7088":
			case "DDC6026":
			case "GPS721":
			case "LC101":
			case "LC131":
			case "LC221":
			case "M2017":
			case "PIR130":
			case "PIR230":
			case "tAD2":
			case "tAD4P2C2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "tR5":
			case "tSL-P4R1":
			case "tSL-PA4R1":
			case "tTH8":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			//[46_53]
			break;
				
			case "7026":
			case "SC4104":
			case "SC6104":
			//[03_487]
			break;
		}
	break;
	case "GET_RF_ENABLE_ENCRYPTION":
		switch(io.module.ID)
		{
				
			case "2060":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2024":
			case "Z2026":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2055":
			case "ZT-2060":
			//[02_287]
			break;
				
			case "ZT-2052":
			case "ZT-2053":
			//[01_287]
			break;
		}
	break;
	case "GET_RF_ENCRYPTION":
		switch(io.module.ID)
		{
				
			case "2060":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2024":
			case "Z2026":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[02_286]
			break;
		}
	break;
	case "GET_SC_RELAY_PROTECT_TIME":
		switch(io.module.ID)
		{
				
			case "SC4104":
			case "SC6104":
			//[03_275]
			break;
		}
	break;
	case "GET_SC_TEMPERATURE_ALARM_RANGE":
		switch(io.module.ID)
		{
				
			case "SC4104":
			case "SC6104":
			//[03_276]
			break;
		}
	break;
	case "GET_SC_TEMPERATURE_HIGH_ALARM_LIMIT":
		switch(io.module.ID)
		{
				
			case "SC4104":
			case "SC6104":
			//[03_274]
			break;
		}
	break;
	case "GET_SC_TEMPERATURE_LOW_ALARM_LIMIT":
		switch(io.module.ID)
		{				
			case "SC4104":
			case "SC6104":
			//[03_274]
			break;
		}
	break;
	case "GET_SIMPLE_AI_HIGH_ALARM_LIMIT":
		switch(io.module.ID)
		{				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			case "PIR130":
			case "PIR230":
			//[03_225]
			break;
		}
	break;
	case "GET_SIMPLE_AI_LOW_ALARM_LIMIT":
		switch(io.module.ID)
		{				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			//[03_224]
			break;
		}
	break;
	
	case "OUTPUT_CH0_AO":
		switch(io.module.ID)
		{
				
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7028":
			case "LC221":
			case "tDA1P1R1":
			case "Z2024":
			case "ZT-2024":
			//[06_0]
			break;
				
			case "7026":
			case "DDC6026":
			case "XV307":
			case "XV310":
			case "Z2026":
			case "ZT-2026":
			//[06_32]
			break;
		}
	break;
	case "OUTPUT_CH0_DO_BIT":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7024U":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "GPS721":
			case "LC101":
			case "LC131":
			case "LC221":
			case "SC4104":
			case "SC6104":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tR5":
			case "tSL-P4R1":
			case "tSL-PA4R1":
			case "XV107":
			case "XV111":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2055":
			case "ZT-2060":
			//[05_0]
			break;
		}
	break;
	case "OUTPUT_CH0_MFDO":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7003":
			case "7005":
			case "7026":
			case "tAD4P2C2":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			case "Z2026":
			case "ZT-2026":
			//[05_0]
			break;
				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			//[05_32]
			break;
		}
	break;
	case "OUTPUT_EXCITATION_AO":
		switch(io.module.ID)
		{				
			case "7016":
			//[06_32]
			break;
		}
	break;
	case "OUTPUT_PWM":
		switch(io.module.ID)
		{
				
			case "7088":
			//[0F_0]
			break;
		}
	break;
	case "OUTPUT_SYNC_PWM":
		switch(io.module.ID)
		{
				
			case "7088":
			//[05_289]
			break;
		}
	break;
	case "READ_17Z_SINGLE_ENDED":
		switch(io.module.ID)
		{
				
			case "7017mC16":
			case "7017Z":
			//[01_276]
			break;
		}
	break;
	case "READ_CF_ALARM_DO_STATUS":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[01_0]
			break;
		}
	break;
	case "READ_CF_ALARM_MODE_STATUS":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[01_136]
			break;
		}
	break;
	case "READ_CF_DI_LOW_PASS_FILTER":
		switch(io.module.ID)
		{				
			case "7084":
			//[01_40]
			break;
		}
	break;
	case "READ_CF_DI_XOR":
		switch(io.module.ID)
		{
				
			case "7084":
			//[01_32]
			break;
		}
	break;
	case "READ_CH0_AI":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7003":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7015":
			case "7015P":
			case "7016":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017mC16":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7017Z":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7026":
			case "7033":
			case "DDC6026":
			case "M2017":
			case "PIR130":
			case "PIR230":
			case "SC4104":
			case "SC6104":
			case "tAD2":
			case "tAD4P2C2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			case "tSL-P4R1":
			case "tSL-PA4R1":
			case "tTH8":
			case "XV306":
			case "XV308":
			case "XV310":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2026":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2026":
			//[04_0]
			break;
		}
	break;
	case "READ_CH0_AI_ALARM_MODE":
		switch(io.module.ID)
		{
				
			case "7026":
			case "tAD4P2C2":
			//[01_336]
			break;
		}
	break;
	case "READ_CH0_ALARM_DO_CHANNELS":
		switch(io.module.ID)
		{
				
			case "7005":
			case "Z2026":
			case "ZT-2026":
			//[03_736]
			break;
		}
	break;
	case "READ_CH0_ALARM_DO_ON":
		switch(io.module.ID)
		{
				
			case "7005":
			//[03_704]
			break;
		}
	break;
	case "READ_CH0_AO":
		switch(io.module.ID)
		{
				
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "DDC6026":
			case "LC221":
			case "tDA1P1R1":
			case "XV307":
			case "XV310":
			case "Z2024":
			case "Z2026":
			case "ZT-2024":
			case "ZT-2026":
			//[03_64]
			break;
		}
	break;
	case "READ_CH0_AO_POWERON":
		switch(io.module.ID)
		{
				
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "DDC6026":
			case "tDA1P1R1":
			case "XV307":
			case "XV310":
			case "Z2024":
			case "Z2026":
			case "ZT-2024":
			case "ZT-2026":
			//[03_192]
			break;
		}
	break;
	case "READ_CH0_AO_SAFE_VALUE":
		switch(io.module.ID)
		{
				
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "DDC6026":
			case "LC221":
			case "tDA1P1R1":
			case "Z2024":
			case "Z2026":
			case "ZT-2024":
			case "ZT-2026":
			//[03_96]
			break;
		}
	break;
	case "READ_CH0_COUNTER_FREQ":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7084":
			case "7088":
			//[03_0]
			break;
		}
	break;
	case "READ_CH0_DI_COUNTER":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7041":
			case "7041D":
			case "7044":
			case "7044D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "LC131":
			case "LC221":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "XV107":
			case "XV110":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[04_0]
			break;
				
			case "7002":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			//[03_96]
			break;
				
			case "7024R":
			case "7024U":
			case "7026":
			case "tAD4P2C2":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			case "Z2026":
			case "ZT-2026":
			//[03_128]
			break;
				
			case "DDC6026":
			case "tDA1P1R1":
			//[04_128]
			break;
		}
	break;
	case "READ_CH0_HIGH_ALARM_DO_ON":
		switch(io.module.ID)
		{
				
			case "Z2026":
			case "ZT-2026":
			//[01_704]
			break;
		}
	break;
	case "READ_CH0_LAST_AO":
		switch(io.module.ID)
		{
				
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7028":
			case "LC221":
			case "tDA1P1R1":
			case "Z2024":
			case "ZT-2024":
			//[03_0]
			break;
				
			case "7026":
			case "DDC6026":
			case "XV307":
			case "XV310":
			case "Z2026":
			case "ZT-2026":
			//[03_32]
			break;
		}
	break;
	case "READ_CH0_LOW_ALARM_DO_ON":
		switch(io.module.ID)
		{
				
			case "Z2026":
			case "ZT-2026":
			//[01_736]
			break;
		}
	break;
	case "READ_CJC_TEMPERATURE":
		switch(io.module.ID)
		{
				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "Z2018":
			case "ZT-2018":
			//[04_128]
			break;
		}
	break;
	case "READ_COUNTER_OVERFLOW":
		switch(io.module.ID)
		{
				
			case "7084":
			//[01_64]
			break;
		}
	break;
	case "READ_DI":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7024R":
			case "7024U":
			case "7026":
			case "7041":
			case "7041D":
			case "7044":
			case "7044D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "DDC6026":
			case "LC101":
			case "LC131":
			case "LC221":
			case "SC4104":
			case "SC6104":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "XV107":
			case "XV110":
			case "Z2026":
			case "ZT-2026":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[01_32]
			break;
		}
	break;
	case "READ_DI_ALARM":
		switch(io.module.ID)
		{
				
			case "LC131":
			//[01_576]
			break;
		}
	break;
	case "READ_DI_ALARM_ENABLE_STATUS":
		switch(io.module.ID)
		{
				
			case "LC131":
			//[01_261]
			break;
		}
	break;
	case "READ_DI_ALARM_MODE":
		switch(io.module.ID)
		{				
			case "LC131":
			//[01_262]
			break;
		}
	break;
	case "READ_DI_HIGH_LATCH":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7002":
			case "7024R":
			case "7024U":
			case "7026":
			case "7041":
			case "7041D":
			case "7044":
			case "7044D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "DDC6026":
			case "LC131":
			case "LC221":
			case "tAD4P2C2":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "XV107":
			case "XV110":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			case "Z2026":
			case "ZT-2026":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[01_64]
			break;
		}
	break;
	case "READ_DI_LOW_LATCH":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7002":
			case "7024R":
			case "7024U":
			case "7026":
			case "7041":
			case "7041D":
			case "7044":
			case "7044D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "DDC6026":
			case "LC131":
			case "LC221":
			case "tAD4P2C2":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "XV107":
			case "XV110":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			case "Z2026":
			case "ZT-2026":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[01_96]
			break;
		}
	break;
	case "READ_DI_SHORT_CIRCUIT_ALARM":
		switch(io.module.ID)
		{
				
			case "LC131":
			//[01_584]
			break;
		}
	break;
	case "READ_DO":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7024U":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "GPS721":
			case "LC101":
			case "LC131":
			case "LC221":
			case "SC4104":
			case "SC6104":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tR5":
			case "tSL-P4R1":
			case "tSL-PA4R1":
			case "XV107":
			case "XV111":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2055":
			case "ZT-2060":
			//[01_0]
			break;
		}
	break;
	case "READ_DO_POWER_ON":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "GPS721":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2055":
			case "ZT-2060":
			//[46_40]
			break;
				
			case "7024U":
			case "LC101":
			case "LC131":
			case "LC221":
			case "SC4104":
			case "SC6104":
			case "tC8":
			case "tR5":
			case "XV107":
			case "XV111":
			//[01_160]
			break;
		}
	break;
	case "READ_DO_SAFE_VALUE":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7024U":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "GPS721":
			case "LC131":
			case "LC221":
			case "SC4104":
			case "SC6104":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tR5":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2055":
			case "ZT-2060":
			//[01_128]
			break;
		}
	break;
	case "READ_EXCITATION_AO":
		switch(io.module.ID)
		{
				
			case "7016":
			//[03_32]
			break;
		}
	break;
	case "READ_GPS_DATA":
		switch(io.module.ID)
		{
				
			case "GPS721":
			//[04_0]
			break;
		}
	break;
	case "READ_GPS_DATE":
		switch(io.module.ID)
		{
				
			case "GPS721":
			//[04_0]
			break;
		}
	break;
	case "READ_HIGH_ALARM_DO_ON":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7026":
			case "tAD4P2C2":
			//[01_304]
			break;
				
			case "7003":
			//[01_704]
			break;
		}
	break;
	
	case "READ_LOG_STATUS":
		switch(io.module.ID)
		{				
			case "7017mC16":
			case "LC221":
			//[04_874]
			break;
		}
	break;
	case "READ_LOW_ALARM_DO_ON":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7026":
			case "tAD4P2C2":
			//[01_288]
			break;
				
			case "7003":
			//[01_736]
			break;
		}
	break;
	case "READ_MF_DO_POWER_ON":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			//[01_192]
			break;
				
			case "7003":
			case "7026":
			case "tAD4P2C2":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			case "Z2026":
			case "ZT-2026":
			//[01_160]
			break;
		}
	break;
	case "READ_MF_DO_SAFE_VALUE":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7003":
			case "7026":
			case "tAD4P2C2":
			case "XV308":
			case "Z2026":
			case "ZT-2026":
			//[01_128]
			break;
				
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			//[01_96]
			break;
		}
	break;
	case "READ_MULTI_AI_DIO_STATUS":
		switch(io.module.ID)
		{
				
			case "7005":
			case "Z2026":
			case "ZT-2026":
			//[01_0]
			break;
		}
	break;
	case "READ_PIR_ALARM_ON":
		switch(io.module.ID)
		{				
			case "PIR130":
			case "PIR230":
			//[01_304]
			break;
		}
	break;
	case "READ_PWM_DI_STATUS":
		switch(io.module.ID)
		{
				
			case "7088":
			//[01_32]
			break;
		}
	break;
	case "READ_PWM_ON_STATUS":
		switch(io.module.ID)
		{
				
			case "7088":
			//[01_0]
			break;
		}
	break;
	case "READ_ROTARY_SWITCH":
		switch(io.module.ID)
		{
				
			case "PIR130":
			case "PIR230":
			//[46_165]
			break;
				
			case "SC4104":
			case "SC6104":
			//[02_320]
			break;
		}
	break;
	case "READ_SIMPLE_AI_ALARM_STATUS":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7003":
			case "7016":
			case "7026":
			case "PIR130":
			case "PIR230":
			case "tAD4P2C2":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			//[01_261]
			break;
				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			//[01_262]
			break;
		}
	break;
	case "READ_SIMPLE_AI_DI_STATUS":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7003":
			case "7026":
			case "PIR130":
			case "PIR230":
			case "tAD4P2C2":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			//[02_32]
			break;
			
				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			//[02_0]
			break;
		}
	break;
	case "READ_SIMPLE_AI_DO_STATUS":
		switch(io.module.ID)
		{				
			case "7002":
			case "7003":
			case "7026":
			case "PIR130":
			case "PIR230":
			case "tAD4P2C2":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			//[01_0]
			break;
				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			//[01_32]
			break;
		}
	break;
	case "READ_WDT_STATUS":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7002":
			case "7003":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7088":
			case "DDC6026":
			case "GPS721":
			case "LC131":
			case "LC221":
			case "SC4104":
			case "SC6104":
			case "tAD4P2C2":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tR5":
			case "Z2024":
			case "Z2026":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2055":
			case "ZT-2060":
			//[01_269]
			break;
		}
	break;
	case "READ_WIRING_STATUS":
		switch(io.module.ID)
		{				
			case "7028":
			//[01_224]
			break;
		}
	break;
	case "SET_17Z_SINGLE_ENDED":
		switch(io.module.ID)
		{
				
			case "7017mC16":
			case "7017Z":
			//[05_276]
			break;
		}
	break;
	case "SET_32Bit_DICnt":
		switch(io.module.ID)
		{
				
			case "2060":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[05_266]
			break;
		}
	break;
	case "SET_BATTERY_BACKUP_ENABLE_STATUS":
		switch(io.module.ID)
		{
				
			case "7024U":
			case "7028":
			case "7084":
			//[0F_768]
			break;
		}
	break;
	case "SET_CF_ALARM_MODE":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[05_139]
			break;
		}
	break;
	case "SET_CF_ALARM_MODE_STATUS":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[0F_136]
			break;
		}
	break;
	case "SET_CF_DO_OUTPUT":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[0F_0]
			break;
		}
	break;
	case "SET_CF_XOR":
		switch(io.module.ID)
		{
				
			case "7084":
			//[0F_928]
			break;
		}
	break;
	case "SET_CH0_AI_ALARM_MODE":
		switch(io.module.ID)
		{
				
			case "7026":
			case "tAD4P2C2":
			//[05_336]
			break;
		}
	break;
	case "SET_CH0_AO_POWERON":
		switch(io.module.ID)
		{
				
			case "7016":
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "DDC6026":
			case "LC221":
			case "tDA1P1R1":
			case "XV307":
			case "XV310":
			case "Z2024":
			case "Z2026":
			case "ZT-2024":
			case "ZT-2026":
			//[06_192]
			break;
		}
	break;
	case "SET_CH0_AO_SAFE_VALUE":
		switch(io.module.ID)
		{
				
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "DDC6026":
			case "LC221":
			case "tDA1P1R1":
			case "Z2024":
			case "Z2026":
			case "ZT-2024":
			case "ZT-2026":
			//[06_96]
			break;
		}
	break;
	case "SET_CH0_AO_SLEW_RATE":
		switch(io.module.ID)
		{
				
			case "7022":
			case "7022A":
			//[46_52]
			break;
				
			case "7024U":
			case "7026":
			case "7028":
			case "DDC6026":
			case "LC221":
			case "tDA1P1R1":
			case "XV307":
			case "XV310":
			case "Z2024":
			case "Z2026":
			case "ZT-2024":
			case "ZT-2026":
			//[06_288]
			break;
		}
	break;
	case "SET_CH0_AO_TYPE_CODE":
		switch(io.module.ID)
		{
				
			case "7022":
			case "7022A":
			//[46_8]
			break;
				
			case "7024U":
			case "7028":
			//[06_256]
			break;
				
			case "7026":
			case "DDC6026":
			case "LC221":
			case "tDA1P1R1":
			case "XV307":
			case "XV310":
			case "Z2024":
			case "Z2026":
			case "ZT-2024":
			case "ZT-2026":
			//[06_416]
			break;
		}
	break;
	case "SET_CH0_CF_ALARM_VALUE":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[10_128]
			break;
		}
	break;
	case "SET_CH0_CJC_OFFSET":
		switch(io.module.ID)
		{
				
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "Z2018":
			case "ZT-2018":
			//[46_44]
			break;
		}
	break;
	case "SET_CH0_COUNTER_ENABLE_STATUS":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[05_134]
			break;
		}
	break;
	case "SET_CH0_DISABLE_AI_ALARM":
		switch(io.module.ID)
		{
				
			case "7026":
			case "tAD4P2C2":
			//[05_320]
			break;
		}
	break;
	case "SET_CH0_DI_TRIGGER_STATE":
		switch(io.module.ID)
		{				
			case "7088":
			//[05_928]
			break;
		}
	break;
	case "SET_CH0_ENABLE_AI_ALARM":
		switch(io.module.ID)
		{
				
			case "7026":
			case "tAD4P2C2":
			//[05_320]
			break;
		}
	break;
	case "SET_CH0_ENABLE_DI_TRIGGER":
		switch(io.module.ID)
		{				
			case "7088":
			//[05_896]
			break;
		}
	break;
	case "SET_CH0_ENABLE_MULTI_AI_HIGH_ALARM":
		switch(io.module.ID)
		{
				
			case "7005":
			//[05_320]
			break;
				
			case "Z2026":
			case "ZT-2026":
			//[05_576]
			break;
		}
	break;
	case "SET_CH0_ENABLE_MULTI_AI_LOW_ALARM":
		switch(io.module.ID)
		{
				
			case "7005":
			//[05_328]
			break;
				
			case "Z2026":
			case "ZT-2026":
			//[05_608]
			break;
		}
	break;
	case "SET_CH0_HIGH_DISABLE_AI_ALARM":
		switch(io.module.ID)
		{
				
			case "7005":
			//[05_320]
			break;
				
			case "Z2026":
			case "ZT-2026":
			//[05_576]
			break;
		}
	break;
	case "SET_CH0_INPUT_RANGE":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7003":
			case "7005":
			case "7015":
			case "7015P":
			case "7017mC16":
			case "7017Z":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7026":
			case "7084":
			case "M2017":
			case "tAD4P2C2":
			case "tTH8":
			case "XV306":
			case "XV308":
			case "XV310":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			//[46_8]
			break;
				
			case "7017R":
			case "7017RM":
			case "7018R":
			case "DDC6026":
			case "Z2018":
			case "Z2026":
			case "ZT-2018":
			case "ZT-2026":
			//[06_256]
			break;
		}
	break;
	case "SET_CH0_INPUT_SIGNAL":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[0F_128]
			break;
		}
	break;
	case "SET_CH0_LINEAR_MAP_CONFIG":
		switch(io.module.ID)
		{
				
			case "7016":
			//[10_160]
			break;
		}
	break;
	case "SET_CH0_LOW_DISABLE_AI_ALARM":
		switch(io.module.ID)
		{
				
			case "7005":
			//[05_328]
			break;
				
			case "Z2026":
			case "ZT-2026":
			//[05_608]
			break;
		}
	break;
	case "SET_CH0_LOW_PASS_FILTER":
		switch(io.module.ID)
		{				
			case "7084":
			//[06_161]
			break;
		}
	break;
	case "SET_CH0_MAX_COUNTER":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7084":
			case "7088":
			//[10_64]
			break;
		}
	break;
	case "SET_CH0_MULTI_AI_HIGH_ALARM_DOCH":
		switch(io.module.ID)
		{				
			case "7005":
			case "Z2026":
			case "ZT-2026":
			//[06_320]
			break;
		}
	break;
	case "SET_CH0_MULTI_AI_HIGH_ALARM_LIMIT":
		switch(io.module.ID)
		{
				
			case "7005":
			//[06_224]
			break;
				
			case "Z2026":
			case "ZT-2026":
			//[06_576]
			break;
		}
	break;
	case "SET_CH0_MULTI_AI_HIGH_ALARM_MODE":
		switch(io.module.ID)
		{
				
			case "7005":
			//[05_336]
			break;
				
			case "Z2026":
			case "ZT-2026":
			//[05_640]
			break;
		}
	break;
	case "SET_CH0_MULTI_AI_LOW_ALARM_DOCH":
		switch(io.module.ID)
		{				
			case "7005":
			case "Z2026":
			case "ZT-2026":
			//[06_328]
			break;
		}
	break;
	case "SET_CH0_MULTI_AI_LOW_ALARM_LIMIT":
		switch(io.module.ID)
		{
				
			case "7005":
			//[06_232]
			break;
				
			case "Z2026":
			case "ZT-2026":
			//[06_608]
			break;
		}
	break;
	case "SET_CH0_MULTI_AI_LOW_ALARM_MODE":
		switch(io.module.ID)
		{
				
			case "7005":
			//[05_344]
			break;
				
			case "Z2026":
			case "ZT-2026":
			//[05_672]
			break;
		}
	break;
	case "SET_CH0_PRESET_COUNTER":
		switch(io.module.ID)
		{				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7084":
			case "7088":
			//[10_96]
			break;
		}
	break;
	case "SET_CH0_PTO":
		switch(io.module.ID)
		{				
			case "7004PTO":
			//[06_32]
			break;
		}
	break;
	case "SET_CH0_PULSE_COUNT":
		switch(io.module.ID)
		{				
			case "7088":
			//[06_800]
			break;
		}
	break;
	case "SET_CH0_PWM_DUTY":
		switch(io.module.ID)
		{				
			case "7088":
			//[06_704]
			break;
		}
	break;
	case "SET_CH0_PWM_FREQ":
		switch(io.module.ID)
		{				
			case "7088":
			//[10_736]
			break;
		}
	break;
	case "SET_CH0_PWM_PULSE_MODE":
		switch(io.module.ID)
		{				
			case "7088":
			//[05_864]
			break;
		}
	break;
	case "SET_CH0_SIMPLE_AI_HIGH_ALARM_LIMIT":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7026":
			case "tAD4P2C2":
			//[06_224]
			break;
				
			case "7003":
			//[06_576]
			break;
		}
	break;
	case "SET_CH0_SIMPLE_AI_LOW_ALARM_LIMIT":
		switch(io.module.ID)
		{				
			case "7002":
			case "7026":
			case "tAD4P2C2":
			//[06_232]
			break;
				
			case "7003":
			//[06_608]
			break;
		}
	break;
	case "SET_CH0_SYNC_PWM":
		switch(io.module.ID)
		{			
			case "7088":
			//[05_960]
			break;
		}
	break;
	case "SET_CH0_TEMPERATURE_OFFSET":
		switch(io.module.ID)
		{
				
			case "7005":
			case "7015":
			case "7015P":
			case "7019":
			case "7019R":
			case "7019Z":
			case "SC4104":
			case "SC6104":
			case "Z2015":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			case "ZT-2015":
			//[06_288]
			break;
				
			case "DDC6026":
			case "tTH8":
			//[06_448]
			break;
		}
	break;
	case "SET_CHANGE_INTERVAL":
		switch(io.module.ID)
		{				
			case "tSL-P4R1":
			case "tSL-PA4R1":
			//[06_288]
			break;
		}
	break;
	case "SET_CHANNEL_ENABLE_STATUS":
		switch(io.module.ID)
		{				
			case "7002":
			case "7003":
			case "7005":
			case "7015":
			case "7015P":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017mC16":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7017Z":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7026":
			case "7088":
			case "M2017":
			case "tAD2":
			case "tAD4P2C2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			case "tTH8":
			case "XV306":
			case "XV308":
			case "XV310":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2026":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2026":
			//[46_38]
			break;
				
			case "DDC6026":
			//[06_489]
			break;
		}
	break;
	case "SET_CJC_ENABLE_STATUS":
		switch(io.module.ID)
		{
				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "Z2018":
			case "ZT-2018":
			//[46_46]
			break;
		}
	break;
	case "SET_COMMUNICATE_PARAMETER":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7002":
			case "7003":
			case "7004PTO":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7015":
			case "7015P":
			case "7016":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017mC16":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7017Z":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "7033":
			case "7041":
			case "7041D":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7084":
			case "7088":
			case "DDC6026":
			case "GPS721":
			case "LC101":
			case "LC131":
			case "LC221":
			case "M2017":
			case "PIR130":
			case "PIR230":
			case "SC4104":
			case "SC6104":
			case "tAD2":
			case "tAD4P2C2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "tR5":
			case "tSL-P4R1":
			case "tSL-PA4R1":
			case "tTH8":
			case "XV107":
			case "XV110":
			case "XV111":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2024":
			case "Z2026":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[46_6]
			break;
		}
	break;
	case "SET_COUNTER_ENABLE_STATUS":
		switch(io.module.ID)
		{				
			case "7084":
			//[06_489]
			break;
		}
	break;
	case "SET_DISABLE_AI_ALARM":
		switch(io.module.ID)
		{				
			case "7002":
			case "7003":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			case "PIR130":
			case "PIR230":
			//[05_261]
			break;
		}
	break;
	case "SET_DI_ALARM_ENABLE_STATUS":
		switch(io.module.ID)
		{				
			case "LC131":
			//[05_261]
			break;
		}
	break;
	case "SET_DI_ALARM_MODE":
		switch(io.module.ID)
		{				
			case "LC131":
			//[05_262]
			break;
		}
	break;
	case "SET_DI_CHANNELS_ALARM_CONFIG":
		switch(io.module.ID)
		{				
			case "LC131":
			//[0F_544]
			break;
		}
	break;
	case "SET_DI_CONNECTION":
		switch(io.module.ID)
		{
				
			case "LC101":
			//[05_273]
			break;
				
			case "SC4104":
			case "SC6104":
			//[06_273]
			break;
		}
	break;
	case "SET_DI_FILTER":
		switch(io.module.ID)
		{				
			case "tSL-P4R1":
			case "tSL-PA4R1":
			//[06_201]
			break;
		}
	break;
	//41 (0x29) Set the DI/O active states 3.7.10
	case "SET_DI_REVERSE":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7041":
			case "7041D":
			case "7044":
			case "7044D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "LC131":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "XV107":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			case "tAD4P2C2":
			//[46_41]
			break;
				
			case "7002":
			case "7024R":
			case "7024U":
			case "7026":
			case "LC221":
			case "tDA1P1R1":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			//[05_264]
			break;
				
			case "7088":
			case "Z2026":
			case "ZT-2026":
			//[05_277]
			break;
		}
	break;
	case "SET_DI_SHORT_CIRCUIT_ALARM_CONFIG":
		switch(io.module.ID)
		{				
			case "LC131":
			//[0F_552]
			break;
		}
	break;
	case "SET_DO_POWER_ON":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "GPS721":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2055":
			case "ZT-2060":
			//[46_39]
			break;
			
				
			case "7024U":
			case "LC101":
			case "LC131":
			case "LC221":
			case "SC4104":
			case "SC6104":
			case "tC8":
			case "tR5":
			case "XV107":
			case "XV111":
			//[0F_160]
			break;
		}
	break;
	case "SET_DO_REVERSE":
		switch(io.module.ID)
		{
				
			case "XV107":
			case "XV111":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			//[05_265]
			break;
		}
	break;
	case "SET_DO_SAFE_VALUE":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7024U":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "GPS721":
			case "LC131":
			case "LC221":
			case "SC4104":
			case "SC6104":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tR5":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2055":
			case "ZT-2060":
			//[0F_128]
			break;
		}
	break;
	case "SET_ENABLE_AI_ALARM_MODE":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7003":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			case "PIR130":
			case "PIR230":
			//[0F_261]
			break;
		}
	break;
	case "SET_ENABLE_THERMOCOUPLE_OPEN_DETECTION":
		switch(io.module.ID)
		{
				
			case "7018R":
			//[05_275]
			break;
		}
	break;
	case "SET_FILTER_WIDTH":
		switch(io.module.ID)
		{
				
			case "7046":
			case "7051":
			case "7051D":
			case "7053":
			case "7053D":
			//[06_497]
			break;
		}
	break;
	case "SET_FREQ_MODE":
		switch(io.module.ID)
		{				
			case "7084":
			//[0F_832]
			break;
		}
	break;
	case "SET_FREQ_TIMEOUT":
		switch(io.module.ID)
		{				
			case "7084":
			//[06_160]
			break;
		}
	break;
	case "SET_GATE_MODE":
		switch(io.module.ID)
		{				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[0F_130]
			break;
		}
	break;
	case "SET_GPS_DATE":
		switch(io.module.ID)
		{				
			case "GPS721":
			//[05_273]
			break;
		}
	break;
	case "SET_HIGH_SIGNAL_WIDTH":
		switch(io.module.ID)
		{				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[06_161]
			break;
		}
	break;
	case "SET_HIGH_TRIGGER_LEVEL_VOLTAGE":
		switch(io.module.ID)
		{				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[06_163]
			break;
		}
	break;
	case "SET_LED_DATA":
		switch(io.module.ID)
		{				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			//[06_495]
			break;
		}
	break;
	case "SET_LED_MODE":
		switch(io.module.ID)
		{				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			//[06_494]
			break;
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[05_142]
			break;
		}
	break;
	case "SET_LINEAR_MODE_ENABLE_STATUS":
		switch(io.module.ID)
		{				
			case "7016":
			//[05_264]
			break;
		}
	break;
	case "SET_LOG_SAMPLE_MODE":
		switch(io.module.ID)
		{				
			case "7017mC16":
			case "LC221":
			//[10_875]
			break;
		}
	break;
	case "SET_LOW_PASS_FILTER_ENABLE_STATUS":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[05_138]
			break;
				
			case "7084":
			//[0F_896]
			break;
		}
	break;
	case "SET_LOW_SIGNAL_WIDTH":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[06_160]
			break;
		}
	break;
	case "SET_LOW_TRIGGER_LEVEL_VOLTAGE":
		switch(io.module.ID)
		{
				
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			//[06_162]
			break;
		}
	break;
	case "SET_MF_DO_POWER_ON":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			//[0F_192]
			break;
				
			case "7003":
			case "7026":
			case "tAD4P2C2":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			case "Z2026":
			case "ZT-2026":
			//[0F_160]
			break;
		}
	break;
	case "SET_MF_DO_SAFE_VALUE":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7003":
			case "7026":
			case "tAD4P2C2":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			case "Z2026":
			case "ZT-2026":
			//[0F_128]
			break;
				
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			//[0F_96]
			break;
		}
	break;
	case "SET_MODBUS_DATAFORMAT":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7003":
			case "7005":
			case "7015":
			case "7015P":
			case "7016":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017mC16":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7017Z":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7022":
			case "7022A":
			case "7024U":
			case "7026":
			case "7028":
			case "7084":
			case "DDC6026":
			case "LC221":
			case "M2017":
			case "tAD2":
			case "tAD4P2C2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			case "tTH8":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2024":
			case "Z2026":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2024":
			case "ZT-2026":
			//[05_268]
			break;
		}
	break;
	case "SET_MODBUS_MISC":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7003":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7015":
			case "7015P":
			case "7016":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017mC16":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7017Z":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7026":
			case "7033":
			case "M2017":
			case "tAD2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			case "tTH8":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2026":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2026":
			//[46_42]
			break;
				
			case "tAD4P2C2":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			//[05_270]
			break;
		}
	break;
	case "SET_MODULE_ADDRESS":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7002":
			case "7003":
			case "7004PTO":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7015":
			case "7015P":
			case "7016":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017mC16":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7017Z":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "7033":
			case "7041":
			case "7041D":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7084":
			case "DDC6026":
			case "GPS721":
			case "tAD2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2024":
			case "Z2026":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[46_4]
			break;
				
			case "7088":
			case "LC101":
			case "LC131":
			case "LC221":
			case "M2017":
			case "PIR130":
			case "PIR230":
			case "SC4104":
			case "SC6104":
			case "tAD4P2C2":
			case "tC8":
			case "tDA1P1R1":
			case "tR5":
			case "tSL-P4R1":
			case "tSL-PA4R1":
			case "tTH8":
			case "XV107":
			case "XV110":
			case "XV111":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			//[06_484]
			break;
		}
	break;
	case "SET_MODULE_CJC_OFFSET":
		switch(io.module.ID)
		{
				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "Z2018":
			case "ZT-2018":
			//[46_44]
			break;
		}
	break;
	case "SET_MODULE_SLEWRATE":
		switch(io.module.ID)
		{
				
			case "7024":
			case "7024R":
			//[46_52]
			break;
		}
	break;
	case "SET_MODULE_TYPECODE":
		switch(io.module.ID)
		{
				
			case "7004PTO":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7018":
			case "701816":
			case "7018R":
			case "7024":
			case "7024R":
			case "7033":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7088":
			case "tAD2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			//[46_8]
			break;
		}
	break;
	case "SET_ON_DELAY_TIME":
		switch(io.module.ID)
		{
				
			case "LC101":
			case "SC4104":
			case "SC6104":
			//[06_497]
			break;
				
			case "PIR130":
			case "PIR230":
			//[06_515]
			break;
		}
	break;
	case "SET_PIR_BUZZER_MODE":
		switch(io.module.ID)
		{				
			case "PIR130":
			case "PIR230":
			//[05_273]
			break;
		}
	break;
	case "SET_PIR_LUMINANCE":
		switch(io.module.ID)
		{				
			case "PIR130":
			case "PIR230":
			//[06_513]
			break;
		}
	break;
	case "SET_PIR_ON_DELAY_TIME":
		switch(io.module.ID)
		{				
			case "PIR130":
			case "PIR230":
			//[06_512]
			break;
		}
	break;
	case "SET_PIR_SENSITIVITY":
		switch(io.module.ID)
		{				
			case "PIR130":
			case "PIR230":
			//[06_516]
			break;
		}
	break;
	case "SET_RESPONSE_DELAY_TIME":
		switch(io.module.ID)
		{
				
			case "7002":
			case "7003":
			case "7004PTO":
			case "7005":
			case "7015":
			case "7015P":
			case "7016":
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "7018":
			case "701816":
			case "7018R":
			case "7018Z":
			case "7019":
			case "7019R":
			case "7019Z":
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7028":
			case "7033":
			case "7041":
			case "7041D":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7046":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7051":
			case "7051D":
			case "7052":
			case "7052D":
			case "7053":
			case "7053D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7058":
			case "7058D":
			case "7059":
			case "7059D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7084":
			case "7088":
			case "DDC6026":
			case "GPS721":
			case "LC101":
			case "LC131":
			case "LC221":
			case "M2017":
			case "PIR130":
			case "PIR230":
			case "tAD2":
			case "tAD4P2C2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tP8":
			case "tR5":
			case "tSL-P4R1":
			case "tSL-PA4R1":
			case "tTH8":
			case "XV306":
			case "XV307":
			case "XV308":
			case "XV310":
			//[46_54]
			break;
				
			case "7026":
			case "SC4104":
			case "SC6104":
			//[06_487]
			break;
		}
	break;
	case "SET_RF_ENABLE_ENCRYPTION":
		switch(io.module.ID)
		{
				
			case "2060":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2024":
			case "Z2026":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[05_287]
			break;
		}
	break;
	case "SET_SAVE_PWM_CONFIG":
		switch(io.module.ID)
		{				
			case "7088":
			//[05_288]
			break;
		}
	break;
	case "SET_SC_RELAY_PROTECT_TIME":
		switch(io.module.ID)
		{				
			case "SC4104":
			case "SC6104":
			//[06_275]
			break;
		}
	break;
	case "SET_SC_TEMPERATURE_ALARM_RANGE":
		switch(io.module.ID)
		{				
			case "SC4104":
			case "SC6104":
			//[06_276]
			break;
		}
	break;
	case "SET_SC_TEMPERATURE_HIGH_ALARM_LIMIT":
		switch(io.module.ID)
		{				
			case "SC4104":
			case "SC6104":
			//[06_274]
			break;
		}
	break;
	case "SET_SC_TEMPERATURE_LOW_ALARM_LIMIT":
		switch(io.module.ID)
		{				
			case "SC4104":
			case "SC6104":
			//[06_274]
			break;
		}
	break;
	case "SET_SIMPLE_AI_HIGH_ALARM_LIMIT":
		switch(io.module.ID)
		{				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			case "PIR130":
			case "PIR230":
			//[06_225]
			break;
		}
	break;
	case "SET_SIMPLE_AI_LOW_ALARM_LIMIT":
		switch(io.module.ID)
		{				
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			//[06_224]
			break;
		}
	break;
	case "GET_SOFTWARE_WDT_ENABLE_STATUS":
		switch(io.module.ID)
		{				
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "tAD2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			//[01_277]
			break;
		}
	break;
	case "SET_SOFTWARE_WDT_ENABLE_STATUS":
		switch(io.module.ID)
		{				
			case "7017":
			case "7017A5":
			case "7017C":
			case "7017F":
			case "7017R":
			case "7017RC":
			case "7017RM":
			case "tAD2":
			case "tAD5":
			case "tAD5C":
			case "tAD8":
			case "tAD8C":
			//[05_277]
			break;
		}
	break;
	case "GET_STOP_ON_OVERFLOW":
		switch(io.module.ID)
		{				
			case "7084":
			//[01_864]
			break;
		}
	break;
	case "SET_STOP_ON_OVERFLOW":
		switch(io.module.ID)
		{				
			case "7084":
			//[0F_864]
			break;
		}
	break;
	case "GET_TEMPERATURE_DISPLAY_FORMAT":
		switch(io.module.ID)
		{
				
			case "7005":
			case "SC4104":
			case "SC6104":
			case "tTH8":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			//[01_266]
			break;
		}
	break;
	case "SET_TEMPERATURE_DISPLAY_FORMAT":
		switch(io.module.ID)
		{				
			case "7005":
			case "SC4104":
			case "SC6104":
			case "tTH8":
			case "ZT-2005-C1":
			case "ZT-2005-C8":
			//[05_266]
			break;
		}
	break;
	case "GET_USER_DEFINED_TYPE_A":
		switch(io.module.ID)
		{
				
			case "7005":
			case "tTH8":
			//[03_768]
			break;
		}
	break;
	case "GET_USER_DEFINED_TYPE_B":
		switch(io.module.ID)
		{				
			case "7005":
			case "tTH8":
			//[03_800]
			break;
		}
	break;
	case "GET_USER_DEFINED_TYPE_C":
		switch(io.module.ID)
		{
				
			case "7005":
			case "tTH8":
			//[03_832]
			break;
		}
	break;
	case "SET_USER_DEFINED_TYPE_A":
		switch(io.module.ID)
		{				
			case "7005":
			case "tTH8":
			//[10_768]
			break;
		}
	break;
	case "SET_USER_DEFINED_TYPE_B":
		switch(io.module.ID)
		{				
			case "7005":
			case "tTH8":
			//[10_800]
			break;
		}
	break;
	case "SET_USER_DEFINED_TYPE_C":
		switch(io.module.ID)
		{				
			case "7005":
			case "tTH8":
			//[10_832]
			break;
		}
	break;
	case "GET_WDT_ENABLE":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7002":
			case "7003":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7088":
			case "DDC6026":
			case "GPS721":
			case "LC131":
			case "LC221":
			case "SC4104":
			case "SC6104":
			case "tAD4P2C2":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tR5":
			case "Z2024":
			case "Z2026":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2055":
			case "ZT-2060":
			//[01_260]
			break;
		}
	break;
	case "SET_WDT_ENABLE":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7088":
			case "DDC6026":
			case "GPS721":
			case "SC4104":
			case "SC6104":
			case "tAD4P2C2":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tR5":
			case "Z2024":
			case "Z2026":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2055":
			case "ZT-2060":
			//[0F_259]
			break;
				
			case "7002":
			case "7003":
			case "LC131":
			case "LC221":
			//[05_259]
			break;
		}
	break;
	case "GET_WDT_TIMER":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7002":
			case "7003":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7088":
			case "DDC6026":
			case "GPS721":
			case "LC131":
			case "LC221":
			case "SC4104":
			case "SC6104":
			case "tAD4P2C2":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tR5":
			case "Z2024":
			case "Z2026":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2055":
			case "ZT-2060":
			//[03_488]
			break;
		}
	break;
	case "SET_WDT_TIMER":
		switch(io.module.ID)
		{
				
			case "2060":
			case "7002":
			case "7003":
			case "7005":
			case "7011":
			case "7011D":
			case "7011P":
			case "7011PD":
			case "7016":
			case "7022":
			case "7022A":
			case "7024":
			case "7024R":
			case "7024U":
			case "7026":
			case "7028":
			case "7042":
			case "7042D":
			case "7043":
			case "7043D":
			case "7044":
			case "7044D":
			case "7045":
			case "7045D":
			case "7050":
			case "7050A":
			case "7050AD":
			case "7050D":
			case "7054":
			case "7054D":
			case "7055":
			case "7055D":
			case "7060":
			case "7060D":
			case "7061":
			case "7061D":
			case "7063":
			case "7063A":
			case "7063AD":
			case "7063B":
			case "7063BD":
			case "7063D":
			case "7065":
			case "7065A":
			case "7065AD":
			case "7065B":
			case "7065BD":
			case "7065D":
			case "7066":
			case "7066D":
			case "7067":
			case "7067D":
			case "7068":
			case "7068D":
			case "7069":
			case "7069D":
			case "7080":
			case "7080B":
			case "7080BD":
			case "7080D":
			case "7088":
			case "DDC6026":
			case "GPS721":
			case "LC131":
			case "LC221":
			case "SC4104":
			case "SC6104":
			case "tAD4P2C2":
			case "tC8":
			case "tDA1P1R1":
			case "tP3POR3":
			case "tP3R3":
			case "tP4A4":
			case "tP4C4":
			case "tR5":
			case "Z2024":
			case "Z2026":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2055":
			case "ZT-2060":
			//[06_488]
			break;
		}
	break;
	case "GET_ZB_RF_POWER":
		switch(io.module.ID)
		{
				
			case "2060":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2024":
			case "Z2026":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[03_502]
			break;
		}
	break;
	case "SET_ZB_RF_POWER":
		switch(io.module.ID)
		{
				
			case "2060":
			case "Z2015":
			case "Z2017":
			case "Z2017C":
			case "Z2018":
			case "Z2024":
			case "Z2026":
			case "ZT-2015":
			case "ZT-2017":
			case "ZT-2017C":
			case "ZT-2018":
			case "ZT-2024":
			case "ZT-2026":
			case "ZT-2042":
			case "ZT-2043":
			case "ZT-2052":
			case "ZT-2053":
			case "ZT-2055":
			case "ZT-2060":
			//[06_502]
			break;
		}
	break;
	
	case "READ_LOG_RTC":
		switch(io.module.ID)
		{				
			case "7017mC16":
			case "LC221":
			//[04_864]
			break;
		}
	break;
	case "WRITE_LOG_RTC":
		switch(io.module.ID)
		{				
			case "7017mC16":
			case "LC221":
			//[10_864]
			break;
		}
	break;
}
