
# UPS

## Installation
```
sudo apt-get install nut
```
```
sudo nano /etc/nut/ups.conf
  [eaton5p]
    driver=usbhid-ups
    port=auto
```
```
sudo nano /etc/nut/upsd.users
   [upsuser]
     password = ups
     actions = SET
     instcmds = ALL
     upsmon master
```
```
sudo nano /etc/nut/upsmon.conf
  MONITOR eaton5p@localhost 1 upsuser ups master

  NOTIFYCMD /usr/local/bin/fluence_nut-notify.sh NOTIFYFLAG ONLINE SYSLOG+WALL+EXEC
    
  NOTIFYMSG ONLINE        "UPS %s on line power"
  NOTIFYMSG ONBATT        "UPS %s on battery"
  NOTIFYMSG LOWBATT       "UPS %s battery is low"
  NOTIFYMSG FSD           "UPS %s: forced shutdown in progress"
  NOTIFYMSG COMMOK        "Communications with UPS %s established"
  NOTIFYMSG COMMBAD       "Communications with UPS %s lost"
  NOTIFYMSG SHUTDOWN      "Auto logout and shutdown proceeding"
  NOTIFYMSG REPLBATT      "UPS %s battery needs to be replaced"
  NOTIFYMSG NOCOMM        "UPS %s is unavailable"
  NOTIFYMSG NOPARENT      "upsmon parent process died - shutdown impossible"
  
  NOTIFYFLAG ONLINE       SYSLOG+WALL
  NOTIFYFLAG ONBATT       SYSLOG+WALL
  NOTIFYFLAG LOWBATT      SYSLOG+WALL
  NOTIFYFLAG FSD          SYSLOG+WALL
  NOTIFYFLAG COMMOK       SYSLOG+WALL
  NOTIFYFLAG COMMBAD      SYSLOG+WALL
  NOTIFYFLAG SHUTDOWN     SYSLOG+WALL
  NOTIFYFLAG REPLBATT     SYSLOG+WALL
  NOTIFYFLAG NOCOMM       SYSLOG+WALL
  NOTIFYFLAG NOPARENT     SYSLOG+WALL

```
```
sudo nano /etc/nut/nut.conf
  MODE=standalone
```

add link for USB devices
```
 cd /etc/udev/rules.d/
 ls -la
 ln -sf /lib/udev/rules.d/62-nut-usbups.rules .
 ls -la
 reboot
```

## Start
```
upsdrvctl start
```

## Stop
```
upsdrvctl stop
```
## Supported command for EATON UPS
```
upscmd -l eaton5p
```
or
```
upsc eaton5p
```

### EATON UPS all loads off
```
upscmd -u upsuser -p ups eaton5p load.off
```

### EATON UPS all loads on
```
upscmd -u upsuser -p ups eaton5p load.on
```

### EATON UPS Outlet 1 load off
```
upscmd -u upsuser -p ups eaton5p outlet.1.load.off
```

### EATON UPS Outlet 1 load on
```
upscmd -u upsuser -p ups eaton5p outlet.1.load.on
```

### EATON UPS Outlet 2 load off
```
upscmd -u upsuser -p ups eaton5p outlet.2.load.off
```

### EATON UPS Outlet 2 load on
```
upscmd -u upsuser -p ups eaton5p outlet.2.load.on
```

Additional read:
- [nut on ubuntu](https://kinavu.org/install-and-configure-nut-on-ubuntu/)
