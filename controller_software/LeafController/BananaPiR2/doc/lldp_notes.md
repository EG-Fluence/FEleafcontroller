## INSTALLATION

```
sudo apt-get install lldpd snmp
sudo service lldpd restart
sudo service snmpd restart
```

## CONFIGURATION

```
cat /etc/default/lldpd 
DAEMON_ARGS="-I wan,lan3 -x -c"
sudo service lldpd restart
```

## USE

```
root@bpi-r2:~# lldpcli
[lldpcli] # show neighbors 
-------------------------------------------------------------------------------
LLDP neighbors:
-------------------------------------------------------------------------------
Interface:    wan, via: LLDP, RID: 1, Time: 0 day, 00:01:56
  Chassis:     
    ChassisID:    mac 78:4f:43:5d:ad:45
    SysName:      germans-macbook-pro.local
    SysDescr:      Darwin 18.7.0 Darwin Kernel Version 18.7.0: Tue Aug 20 16:57:14 PDT 2019; root:xnu-4903.271.2~2/RELEASE_X86_64 x86_64
    TTL:          120
    MgmtIP:       192.168.10.210
    MgmtIP:       2601:805:8300:6c70:10de:b95b:7b6c:d6f
    Capability:   Bridge, on
    Capability:   Router, on
    Capability:   Wlan, on
    Capability:   Station, off
  Port:        
    PortID:       mac a0:ce:c8:05:d4:89
    PortDescr:    en7
-------------------------------------------------------------------------------
```

or

```
root@bpi-r2:~# lldpcli sho nei
-------------------------------------------------------------------------------
LLDP neighbors:
-------------------------------------------------------------------------------
Interface:    wan, via: LLDP, RID: 1, Time: 0 day, 00:02:50
  Chassis:     
    ChassisID:    mac 78:4f:43:5d:ad:45
    SysName:      germans-macbook-pro.local
    SysDescr:      Darwin 18.7.0 Darwin Kernel Version 18.7.0: Tue Aug 20 16:57:14 PDT 2019; root:xnu-4903.271.2~2/RELEASE_X86_64 x86_64
    TTL:          120
    MgmtIP:       192.168.10.210
    MgmtIP:       2601:805:8300:6c70:10de:b95b:7b6c:d6f
    Capability:   Bridge, on
    Capability:   Router, on
    Capability:   Wlan, on
    Capability:   Station, off
  Port:        
    PortID:       mac a0:ce:c8:05:d4:89
    PortDescr:    en7
-------------------------------------------------------------------------------
```

