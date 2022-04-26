# Network

```
cat /etc/network/interfaces

auto eth0
iface eth0 inet manual
  pre-up ip link set $IFACE up
  post-down ip link set $IFACE down

auto eth1
iface eth1 inet manual
  pre-up ip link set $IFACE up
  post-down ip link set $IFACE down

auto br0
iface br0 inet dhcp
    bridge_ports wan lan3 
    bridge_fd 5
    bridge_stp no
```

## Netplan

Netplan is a utility for easily configuring networking on a linux system. 
You simply create a YAML description of the required network interfaces and what each should be configured to do. From this description Netplan will generate all the necessary configuration for your chosen renderer tool.

```
less /etc/netplan/network.yaml 
```
default output:
```
network:
  version: 2
  renderer: networkd
  bridges:
    br0:
      dhcp4: false
    br1:
      addresses:
      - 192.168.0.109/24
      gateway4: 192.168.0.1
      interfaces:
      - lan0
      - lan1
      - lan2
  ethernets:
    eth0:
      dhcp4: false
    eth1:
      dhcp4: false
    lan0:
      dhcp4: false
    lan1:
      dhcp4: false
    lan2:
      dhcp4: false
```
Change IP address and gateway 
```
nano /etc/netplan/network.yaml 
```

Generate the required configuration for the renderers.
```
sudo netplan generate 
```
Apply all configuration for the renderers, restarting them as necessary.
```
sudo netplan apply
```

## NAT

Enable IP Forwarding in Linux:
__Uncomment the next line to enable packet forwarding for IPv4 in__ `/etc/sysctl.conf`
```
net.ipv4.ip_forward = 1
```
Apply changes
``` 
sysctl -p /etc/sysctl.conf
```
NAT
``` 
iptables -t nat -A PREROUTING -p tcp --dport 80 -j DNAT --to-destination 172.16.255.254:80
iptables -t nat -A POSTROUTING -p tcp -d 172.16.255.254 --dport 80 -j SNAT --to-source 192.168.2.40 
iptables -t nat -A PREROUTING -p tcp --dport 8080 -j DNAT --to-destination 172.16.255.254:8080
iptables -t nat -A POSTROUTING -p tcp -d 172.16.255.254 --dport 8080 -j SNAT --to-source 192.168.2.40 
iptables -t nat -A PREROUTING -p tcp --dport 1502 -j DNAT --to-destination 172.16.255.254:1502
iptables -t nat -A POSTROUTING -p tcp -d 172.16.255.254 --dport 1502 -j SNAT --to-source 192.168.2.40
``` 
To have there rules persist across reboots, install iptables-persistent and netfilter-persistent

``` 
sudo apt install iptables-persistent
systemctl enable netfilter-persistent
systemctl start netfilter-persistent
iptables-save > /etc/iptables/rules.v4
```
two commands for saving and reloading:
```
sudo netfilter-persistent save
sudo netfilter-persistent reload
```

To list the rules type:
``` 
root@leaf1:/usr/local/src# iptables -t nat -L -n -v
Chain PREROUTING (policy ACCEPT 13 packets, 6938 bytes)
 pkts bytes target     prot opt in     out     source               destination         
    0     0 DNAT       tcp  --  *      *       0.0.0.0/0            0.0.0.0/0            tcp dpt:80 to:172.16.255.254:80
    0     0 DNAT       tcp  --  *      *       0.0.0.0/0            0.0.0.0/0            tcp dpt:8080 to:172.16.255.254:8080
    0     0 DNAT       tcp  --  *      *       0.0.0.0/0            0.0.0.0/0            tcp dpt:1502 to:172.16.255.254:1502
Chain INPUT (policy ACCEPT 11 packets, 6336 bytes)
 pkts bytes target     prot opt in     out     source               destination         
Chain OUTPUT (policy ACCEPT 3 packets, 178 bytes)
 pkts bytes target     prot opt in     out     source               destination         
Chain POSTROUTING (policy ACCEPT 5 packets, 780 bytes)
 pkts bytes target     prot opt in     out     source               destination         
    0     0 SNAT       tcp  --  *      *       0.0.0.0/0            172.16.255.254       tcp dpt:80 to:192.168.2.40
    0     0 SNAT       tcp  --  *      *       0.0.0.0/0            172.16.255.254       tcp dpt:8080 to:192.168.2.40
    0     0 SNAT       tcp  --  *      *       0.0.0.0/0            172.16.255.254       tcp dpt:1502 to:192.168.2.40
``` 
Test from the Array Controller if the services on the Controllino are reachable
``` 
ijefferson.admin@array:~$ tcping core01-leaf01  1502
core01-leaf01 port 1502 open.
ijefferson.admin@array:~$ tcping core01-leaf01  80
core01-leaf01 port 80 open.
ijefferson.admin@array:~$ tcping core01-leaf01  8080
core01-leaf01 port 8080 open.
``` 

## Jump servers
Steve Hay:
    I use my ```.ssh/config``` file to help with jump servers 
```  
Host warrior_c03
  User shay.admin
  Hostname 10.194.3.6
 ProxyCommand ssh -q -W %h:%p warrior_array 
```
```
ssh -J bkajganic.admin@10.250.225.7,bkajganic.admin@10.194.5.8 root@172.16.1.19
```

## SSH Port Forwarding
```
ssh -J mt.fmetz@enst01nm02pr.fluenceenergy.com,mt.fmetz@sandbox -f mt.fmetz@array -L 5001:127.0.0.1:5000 -N
ssh -J bkajganic.admin@10.250.225.7,bkajganic.admin@10.194.5.8 -f root@172.16.1.19 -L 8080:127.0.0.1:8080 -N
```
## VNC
```
ssh -J bkajganic.admin@10.250.225.7 -f bkajganic.admin@10.194.5.8 -L 5910:127.0.0.1:5910 -N
```

## Add proxy server

Add the 10.194.5.8 server as proxy: 
edit profile file and add this below line at the bottom

```  
#cat /etc/profile
export http_proxy=http://172.16.1.4:3128
export https_proxy=https://172.16.1.4:3128
```  
source the profile
```  
#. /etc/profile
OR
#source /etc/profile
```  
verify the proxy is set in environmental variable
```  
env |egrep -i "http" 
```  
