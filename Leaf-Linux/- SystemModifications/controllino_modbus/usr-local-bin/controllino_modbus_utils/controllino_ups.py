# -*- coding: utf-8 -*-
"""
Created on Tue Feb  2 14:05:12 2021

@author: Georg.Kordowich
"""
from controllino_modbus_utils.controllino_generic_modbus_device import ModbusDevice
import controllino_modbus_utils.controllino_modbus_utils as cmu_utils


'''
This dict is used to encode values (like status or serial number) of the UPS.
This is necessary, so the values can be read via modbus.
!!!!!!!!!!!!!!  
'''
#TODO: A complete list could not be found. This dictionary is work in progress!!!!!!!!!!!!
upsDict = {
    'on': 1,
    'off': 0,
    
    'yes': 1,
    'no': 0,
    
    'enabled': 1,
    'disabled': 0,
    
    'normal': 0,
    
    'floating': 0,
    
    'PbAc': 0,
    'EATON': 0,
    '5P 850': 0,
    'G115K44007': 0,
    'ups': 0,
    'usbhid-ups': 0,
    'MGE HID 1.39': 0,
    'auto': 0,
    '2.7.4': 274,

    'PowerShare Outlet 1': 0,
    'PowerShare Outlet 2': 0,
    'Main Outlet': 0,

    '02.14.0026': 0,
    'OL CHRG': 0,
    'Done and passed': 0,
    'offline / line interactive' :0,
    'ffff': 65535,

    }



'''
Function will encode the values that are read via the ups command line driver.
First it tries to convert to int, then to float, then it uses the upsDict for 'Translation' of values.
If all that failes, it will write the value 0xFF. That means an unknown value was read from the UPS.
'''
def encodeUpsValues(value):
    try:
        ret = int(value)
        if ret < 0:
            ret = ret & 0xffff
        elif ret > 65535:
            ret = 65535
    except ValueError:
        try:
            ret = float(value)
            ret = int(ret*10)
            if ret >65535:
                ret = 65535
        except ValueError:
            try:
                ret = upsDict[value.strip()]
            except KeyError:
                ret = 0xFF
    return ret



'''
Function get the values from the UPS and stores them in the according dataframe.
'''
def readUps(deviceSettings, dataFrame):
    if (deviceSettings.use == False):
        return # ignore UPS
    
    # execute the bash command and read the output. 
    op, err = cmu_utils.executeBash('upsc eaton5p')  # Todo: Make Dynamic
    
    dataFrame.loc['heartbeat', 'value'] = (dataFrame.loc['heartbeat', 'value'] + 1)%65536
    
    if op is not None:
        op = op.decode('utf-8')
    
    try:
        #What those lines do: Find the respective status in the returned string, get the value of that status, then encode it with a dedicated function.
        dataFrame.loc['battery_capacity', 'value'] = encodeUpsValues(op.split('battery.capacity')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['battery_charge',  'value'] = encodeUpsValues(op.split('battery.charge')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['battery_charge_low', 'value'] = encodeUpsValues(op.split('battery.charge.low')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['battery_charge_restart', 'value'] = encodeUpsValues(op.split('battery.charge.restart')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['battery_charger_status', 'value'] = encodeUpsValues(op.split('battery.charger.status')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['battery_energysave', 'value'] = encodeUpsValues(op.split('battery.energysave')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['battery_energysave_delay', 'value'] = encodeUpsValues(op.split('battery.energysave.delay')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['battery_energysave_load', 'value'] = encodeUpsValues(op.split('battery.energysave.load')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['battery_protection', 'value'] = encodeUpsValues(op.split('battery.protection')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['battery_runtime', 'value'] = encodeUpsValues(op.split('battery.runtime')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['battery_type', 'value'] = encodeUpsValues(op.split('battery.type')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['battery_voltage', 'value'] = encodeUpsValues(op.split('battery.voltage')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['battery_voltage_nominal', 'value'] = encodeUpsValues(op.split('battery.voltage.nominal')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['device_mfr', 'value'] = encodeUpsValues(op.split('device.mfr')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['device_model', 'value'] = encodeUpsValues(op.split('device.model')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['device_serial', 'value'] = encodeUpsValues(op.split('device.serial')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['device_type', 'value'] = encodeUpsValues(op.split('device.type')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['driver_name', 'value'] = encodeUpsValues(op.split('driver.name')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['driver_parameter_pollfreq', 'value'] = encodeUpsValues(op.split('driver.parameter.pollfreq')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['driver_parameter_pollinterval', 'value'] = encodeUpsValues(op.split('driver.parameter.pollinterval')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['driver_parameter_port', 'value'] = encodeUpsValues(op.split('driver.parameter.port')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['driver_parameter_synchronous', 'value'] = encodeUpsValues(op.split('driver.parameter.synchronous')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['driver_version', 'value'] = encodeUpsValues(op.split('driver.version')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['driver_version_data', 'value'] = encodeUpsValues(op.split('driver.version.data')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['driver_version_internal', 'value'] = encodeUpsValues(op.split('driver.version.internal')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['input_current', 'value'] = encodeUpsValues(op.split('input.current')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['input_frequency', 'value'] = encodeUpsValues(op.split('input.frequency')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['input_frequency_extended', 'value'] = encodeUpsValues(op.split('input.frequency.extended')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['input_frequency_nominal', 'value'] = encodeUpsValues(op.split('input.frequency.nominal')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['input_sensitivity', 'value'] = encodeUpsValues(op.split('input.sensitivity')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['input_transfer_boost_low', 'value'] = encodeUpsValues(op.split('input.transfer.boost.low')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['input_transfer_high', 'value'] = encodeUpsValues(op.split('input.transfer.high')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['input_transfer_low', 'value'] = encodeUpsValues(op.split('input.transfer.low')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['input_transfer_trim_high', 'value'] = encodeUpsValues(op.split('input.transfer.trim.high')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['input_voltage', 'value'] = encodeUpsValues(op.split('input.voltage')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['input_voltage_extended', 'value'] = encodeUpsValues(op.split('input.voltage.extended')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['input_voltage_nominal', 'value'] = encodeUpsValues(op.split('input.voltage.nominal')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_1_autoswitch_charge_low', 'value'] = encodeUpsValues(op.split('outlet.1.autoswitch.charge.low')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_1_delay_shutdown', 'value'] = encodeUpsValues(op.split('outlet.1.delay.shutdown')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_1_delay_start', 'value'] = encodeUpsValues(op.split('outlet.1.delay.start')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_1_desc', 'value'] = encodeUpsValues(op.split('outlet.1.desc')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_1_id', 'value'] = encodeUpsValues(op.split('outlet.1.id')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_1_status', 'value'] = encodeUpsValues(op.split('outlet.1.status')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_1_switchable', 'value'] = encodeUpsValues(op.split('outlet.1.switchable')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_2_autoswitch_charge_low', 'value'] = encodeUpsValues(op.split('outlet.2.autoswitch.charge.low')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_2_delay_shutdown', 'value'] = encodeUpsValues(op.split('outlet.2.delay.shutdown')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_2_delay_start', 'value'] = encodeUpsValues(op.split('outlet.2.delay.start')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_2_desc', 'value'] = encodeUpsValues(op.split('outlet.2.desc')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_2_id', 'value'] = encodeUpsValues(op.split('outlet.2.id')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_2_status', 'value'] = encodeUpsValues(op.split('outlet.2.status')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_2_switchable', 'value'] = encodeUpsValues(op.split('outlet.2.switchable')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_desc', 'value'] = encodeUpsValues(op.split('outlet.desc')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_id', 'value'] = encodeUpsValues(op.split('outlet.id')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['outlet_switchable', 'value'] = encodeUpsValues(op.split('outlet.switchable')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['output_current', 'value'] = encodeUpsValues(op.split('output.current')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['output_frequency', 'value'] = encodeUpsValues(op.split('output.frequency')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['output_frequency_nominal', 'value'] = encodeUpsValues(op.split('output.frequency.nominal')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['output_powerfactor', 'value'] = encodeUpsValues(op.split('output.powerfactor')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['output_voltage', 'value'] = encodeUpsValues(op.split('output.voltage')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['output_voltage_nominal', 'value'] = encodeUpsValues(op.split('output.voltage.nominal')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_beeper_status', 'value'] = encodeUpsValues(op.split('ups.beeper.status')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_delay_shutdown', 'value'] = encodeUpsValues(op.split('ups.delay.shutdown')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_delay_start', 'value'] = encodeUpsValues(op.split('ups.delay.start')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_efficiency', 'value'] = encodeUpsValues(op.split('ups.efficiency')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_firmware', 'value'] = encodeUpsValues(op.split('ups.firmware')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_load', 'value'] = encodeUpsValues(op.split('ups.load')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_load_high', 'value'] = encodeUpsValues(op.split('ups.load.high')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_mfr', 'value'] = encodeUpsValues(op.split('ups.mfr')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_model', 'value'] = encodeUpsValues(op.split('ups.model')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_power', 'value'] = encodeUpsValues(op.split('ups.power')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_power_nominal', 'value'] = encodeUpsValues(op.split('ups.power.nominal')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_productid', 'value'] = encodeUpsValues(op.split('ups.productid')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_realpower', 'value'] = encodeUpsValues(op.split('ups.realpower')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_realpower_nominal', 'value'] = encodeUpsValues(op.split('ups.realpower.nominal')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_serial', 'value'] = encodeUpsValues(op.split('ups.serial')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_shutdown', 'value'] = encodeUpsValues(op.split('ups.shutdown')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_start_auto', 'value'] = encodeUpsValues(op.split('ups.start.auto')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_start_battery', 'value'] = encodeUpsValues(op.split('ups.start.battery')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_start_reboot', 'value'] = encodeUpsValues(op.split('ups.start.reboot')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_status', 'value'] = encodeUpsValues(op.split('ups.status')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_test_interval', 'value'] = encodeUpsValues(op.split('ups.test.interval')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_test_result', 'value'] = encodeUpsValues(op.split('ups.test.result')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_timer_shutdown', 'value'] = encodeUpsValues(op.split('ups.timer.shutdown')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_timer_start', 'value'] = encodeUpsValues(op.split('ups.timer.start')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_type', 'value'] = encodeUpsValues(op.split('ups.type')[1].splitlines()[0].split(':')[1])
        dataFrame.loc['ups_vendorid', 'value'] = encodeUpsValues(op.split('ups.vendorid')[1].splitlines()[0].split(':')[1])
    except IndexError:
        print('#===========!!!IMPORTANT!!!===========#')
        print('Connection to CLient UPS impossible.')
        print('The UPS does not give the expected replies, check UPS')
        dataFrame.loc['readAlarm', 'value'] = 1
        return dataFrame
    except AttributeError:
        print('#===========!!!IMPORTANT!!!===========#')
        print('Connection to CLient UPS impossible.')
        print('The UPS does not give the expected replies, check UPS')
        dataFrame.loc['readAlarm', 'value'] = 1
        return dataFrame
    
    # read successful
    dataFrame.loc['readTimestamp0':'readTimestamp3', 'value'] = cmu_utils.getTimeStampRegs()
    dataFrame.loc['readAlarm', 'value'] = 0
    return dataFrame



'''
The following List contains all signal names, addresses and also contains dummy values for the eatonUPS client.
It is used to initialize the datastore for the values which are read via modbus.
'''
eatonUpsHrRegisterList = [
    ['battery_capacity',                0x0000, 0xFF, 'ro', 'r'],  # fLoat
    ['battery_charge',                  0x0001, 0xFF, 'ro', 'r'],  # int
    ['battery_charge_low',              0x0002, 0xFF, 'ro', 'r'],  # int
    ['battery_charge_restart',          0x0003, 0xFF, 'ro', 'r'],  # int
    ['battery_charger_status',          0x0004, 0xFF, 'ro', 'r'],  #
    ['battery_energysave',              0x0005, 0xFF, 'ro', 'r'],  #
    ['battery_energysave_delay',        0x0006, 0xFF, 'ro', 'r'],  # int
    ['battery_energysave_load',         0x0007, 0xFF, 'ro', 'r'],  # int
    ['battery_protection',              0x0008, 0xFF, 'ro', 'r'],  # yes = 1, no = 0
    ['battery_runtime',                 0x0009, 0xFF, 'ro', 'r'],  # int
    ['battery_type',                    0x000A, 0xFF, 'ro', 'r'],  #
    ['battery_voltage',                 0x000B, 0xFF, 'ro', 'r'],  # float
    ['battery_voltage_nominal',         0x000C, 0xFF, 'ro', 'r'],  # int
    ['device_mfr',                      0x000D, 0xFF, 'ro', 'r'],  # 0 = Eaton
    ['device_model',                    0x000E, 0xFF, 'ro', 'r'],  # 0 = 5P 850
    ['device_serial',                   0x000F, 0xFF, 'ro', 'r'],  # str
    ['device_type',                     0x0010, 0xFF, 'ro', 'r'],  # str
    ['driver_name',                     0x0011, 0xFF, 'ro', 'r'],  # str
    ['driver_parameter_pollfreq',       0x0012, 0xFF, 'ro', 'r'],  # int
    ['driver_parameter_pollinterval',   0x0013, 0xFF, 'ro', 'r'],  # int
    ['driver_parameter_port',           0x0014, 0xFF, 'ro', 'r'],  # str
    ['driver_parameter_synchronous',    0x0015, 0xFF, 'ro', 'r'],  # str
    ['driver_version',                  0x0016, 0xFF, 'ro', 'r'],  # str
    ['driver_version_data',             0x0017, 0xFF, 'ro', 'r'],  # str
    ['driver_version_internal',         0x0018, 0xFF, 'ro', 'r'],  # str
    ['input_current',                   0x0019, 0xFF, 'ro', 'r'],  # float
    ['input_frequency',                 0x001A, 0xFF, 'ro', 'r'],  # float
    ['input_frequency_extended',        0x001B, 0xFF, 'ro', 'r'],  # str
    ['input_frequency_nominal',         0x001C, 0xFF, 'ro', 'r'],  # float
    ['input_sensitivity',               0x001D, 0xFF, 'ro', 'r'],  # str
    ['input_transfer_boost_low',        0x001E, 0xFF, 'ro', 'r'],  # int
    ['input_transfer_high',             0x001F, 0xFF, 'ro', 'r'],  # int
    ['input_transfer_low',              0x0020, 0xFF, 'ro', 'r'],  # int
    ['input_transfer_trim_high',        0x0021, 0xFF, 'ro', 'r'],  # int
    ['input_voltage',                   0x0022, 0xFF, 'ro', 'r'],  # float
    ['input_voltage_extended',          0x0023, 0xFF, 'ro', 'r'],  #
    ['input_voltage_nominal',           0x0024, 0xFF, 'ro', 'r'],  #
    ['outlet_1_autoswitch_charge_low',  0x0025, 0xFF, 'ro', 'r'],  #
    ['outlet_1_delay_shutdown',         0x0026, 0xFF, 'ro', 'r'],  #
    ['outlet_1_delay_start',            0x0027, 0xFF, 'ro', 'r'],  #
    ['outlet_1_desc',                   0x0028, 0xFF, 'ro', 'r'],  #
    ['outlet_1_id',                     0x0029, 0xFF, 'ro', 'r'],  #
    ['outlet_1_status',                 0x002A, 0xFF, 'ro', 'r'],  #
    ['outlet_1_switchable',             0x002B, 0xFF, 'ro', 'r'],  #
    ['outlet_2_autoswitch_charge_low',  0x002C, 0xFF, 'ro', 'r'],  #
    ['outlet_2_delay_shutdown',         0x002D, 0xFF, 'ro', 'r'],  #
    ['outlet_2_delay_start',            0x002E, 0xFF, 'ro', 'r'],  #
    ['outlet_2_desc',                   0x002F, 0xFF, 'ro', 'r'],  #
    ['outlet_2_id',                     0x0030, 0xFF, 'ro', 'r'],  #
    ['outlet_2_status',                 0x0031, 0xFF, 'ro', 'r'],  #
    ['outlet_2_switchable',             0x0032, 0xFF, 'ro', 'r'],  #
    ['outlet_desc',                     0x0033, 0xFF, 'ro', 'r'],  #
    ['outlet_id',                       0x0034, 0xFF, 'ro', 'r'],  #
    ['outlet_switchable',               0x0035, 0xFF, 'ro', 'r'],  #
    ['output_current',                  0x0036, 0xFF, 'ro', 'r'],  #
    ['output_frequency',                0x0037, 0xFF, 'ro', 'r'],  #
    ['output_frequency_nominal',        0x0038, 0xFF, 'ro', 'r'],  #
    ['output_powerfactor',              0x0039, 0xFF, 'ro', 'r'],  #
    ['output_voltage',                  0x003A, 0xFF, 'ro', 'r'],  #
    ['output_voltage_nominal',          0x003B, 0xFF, 'ro', 'r'],  #
    ['ups_beeper_status',               0x003C, 0xFF, 'ro', 'r'],  #
    ['ups_delay_shutdown',              0x003D, 0xFF, 'ro', 'r'],  #
    ['ups_delay_start',                 0x003E, 0xFF, 'ro', 'r'],  #
    ['ups_efficiency',                  0x003F, 0xFF, 'ro', 'r'],  #
    ['ups_firmware',                    0x0040, 0xFF, 'ro', 'r'],  #
    ['ups_load',                        0x0041, 0xFF, 'ro', 'r'],  #
    ['ups_load_high',                   0x0042, 0xFF, 'ro', 'r'],  #
    ['ups_mfr',                         0x0043, 0xFF, 'ro', 'r'],  #
    ['ups_model',                       0x0044, 0xFF, 'ro', 'r'],  #
    ['ups_power',                       0x0045, 0xFF, 'ro', 'r'],  #
    ['ups_power_nominal',               0x0046, 0xFF, 'ro', 'r'],  #
    ['ups_productid',                   0x0047, 0xFF, 'ro', 'r'],  #
    ['ups_realpower',                   0x0048, 0xFF, 'ro', 'r'],  #
    ['ups_realpower_nominal',           0x0049, 0xFF, 'ro', 'r'],  #
    ['ups_serial',                      0x004A, 0xFF, 'ro', 'r'],  #
    ['ups_shutdown',                    0x004B, 0xFF, 'ro', 'r'],  #
    ['ups_start_auto',                  0x004C, 0xFF, 'ro', 'r'],  #
    ['ups_start_battery',               0x004D, 0xFF, 'ro', 'r'],  #
    ['ups_start_reboot',                0x004E, 0xFF, 'ro', 'r'],  #
    ['ups_status',                      0x004F, 0xFF, 'ro', 'r'],  #
    ['ups_test_interval',               0x0050, 0xFF, 'ro', 'r'],  #
    ['ups_test_result',                 0x0051, 0xFF, 'ro', 'r'],  #
    ['ups_timer_shutdown',              0x0052, 0xFF, 'ro', 'r'],  #
    ['ups_timer_start',                 0x0053, 0xFF, 'ro', 'r'],  #
    ['ups_type',                        0x0054, 0xFF, 'ro', 'r'],  #
    ['ups_vendorid',                    0x0055, 0xFF, 'ro', 'r'],  #
    
    ['readAlarm',                       0x0056, 0xFF, 'ro', 'v'], # 
    ['readTimestamp0',                  0x0057, 0xFF, 'ro', 'v'], # 
    ['readTimestamp1',                  0x0058, 0xFF, 'ro', 'v'], # 
    ['readTimestamp2',                  0x0059, 0xFF, 'ro', 'v'], # 
    ['readTimestamp3',                  0x005A, 0xFF, 'ro', 'v'], # 
    ['heartbeat',                       0x005B, 0,    'ro', 'v']  # 
                                                        ]

class UpsDevice(ModbusDevice):
    def __init__(self, deviceSettings):
        super(UpsDevice, self).__init__(deviceSettings, 
               coRegisterList = None, 
               diRegisterList = None, 
               hrRegisterList = eatonUpsHrRegisterList, 
               irRegisterList = None)
    
    '''
    Method overwrites the readAll method from the ModbusDevice parent class
    '''
    def readAll(self):
        self.hrDataFrame = readUps(self.deviceSettings, self.hrDataFrame)





















