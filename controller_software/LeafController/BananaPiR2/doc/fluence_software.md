# Services

Since Ubuntu 15.10 (resp. Debian 8 "jessie"), you have to use the following command to configure your service minidlna to run at startup:
```
sudo systemctl enable fluence-init.service
```
And to disable it again from starting at boot time:
```
sudo systemctl disable fluence-init.service
```
This works with all service name references that you can find with 
```
ls /etc/systemd/system/*.service
ls /lib/systemd/system/*.service
```

## Status
```
sudo systemctl status fluence-init.service
```
```
 journalctl -u fluence-init -r
 ```

| Link                                                      | Description           |
|-----------------------------------------------------------|-----------------------|
| [DAS](DAS.md)                                             | DAS                   |
| [UPS](UPS.md)                                             | UPS                   |

