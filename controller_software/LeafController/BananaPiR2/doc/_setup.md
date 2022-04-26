# Leaf Controller Banana Pi R2 Quick Start

## Debian Stretch (Frank-W) Flash

- Flash image

	```bash
	diskutil list
	diskutil unmountDisk disk_
	sudo dd \
	  if=deb_stretch_4.14.80_SD.img \
	  of=/dev/rdisk_ \
	  bs=32m
	```
	
## Ubuntu 18.04 Preview (Frank-W)

- Flash image

	```bash
	diskutil list
	diskutil unmountDisk disk_
	sudo dd \
	  if=ubuntu-18.04-bpi-r2-preview.img \
	  of=/dev/rdisk_ \
	  bs=32m
	```
- Mount SDcard in Linux VM (Note: Ensure USB3 support is enabled in VM if needed)

	```
	mkdir -p /mnt/usb
	mount /dev/sdb2 /mnt/usb
	```

- Build Kernel

	- Use _frank-w_ repository for 4.14.
	- Extract known good ```.config``` from Ubuntu 1804 image
	- Make build environment
	- Add the following options to the kernel
		- CONFIG\_OVERLAY=y
		- CONFIG\_SECCOMP\_FILTER=y
		- CONFIG\_SECCOMP=y
	- Run ```./build.sh``` script.
	- Create **tar.gz** package
	- Unpack into tree(s)
	- Edit uEnv.txt

- Edit **/mnt/usb/etc/network/interfaces** to configure LANs properly. Used stretch image as example.

- Configure **/mnt/usb/etc/resolv.conf** to point to proper DNS
	```
	nameserver 192.168.2.1
	```

- Edit **/mnt/usb/etc/ssh/sshd_config** To enable SSH login for a root user on Debian Linux system you need to first configure SSH server. 
Open /etc/ssh/sshd_config and change the following line:
	```
    FROM:
    PermitRootLogin without-password
    TO:
    PermitRootLogin yes
	```
  Once you made the above change restart your SSH server:
	```
	#/etc/init.d/ssh restart
	[ ok ] Restarting ssh (via systemctl): ssh.service.
	```

- Ssh into device. Run ```apt update && apt upgrade```.

- Set **root** password to **root**.

- Edit **/etc/hostname** to something unique (example):

	```
	bpi-prototype-nn
	```

- Edit **/etc/hosts** to match:

	```
	127.0.0.1	localhost localhost.localdomain
	127.0.1.1	bpi-prototype-nn
	::1		localhost ip6-localhost ip6-loopback
	ff02::1		ip6-allnodes
	ff02::2		ip6-allrouters
	```

- Add the Greengrass user and group.

	```bash
	sudo adduser --system ggc_user
	sudo addgroup --system ggc_group
	```

- Checked **cgroups=memory** enabled, **fs.protected_hardlinks=1**, and **fs.protected_symlinks=1**.

	```
	sysctl fs.protected_hardlinks
	sysctl fs.protected_symlinks
	cat /proc/cgroups
	```

- Add **universe** to the Ubuntu repositories.

	```
	deb http://ports.ubuntu.com/ubuntu-ports/ bionic main universe
	deb-src http://ports.ubuntu.com/ubuntu-ports/ bionic main universe
	deb http://ports.ubuntu.com/ubuntu-ports/ bionic-updates main universe
	deb-src http://ports.ubuntu.com/ubuntu-ports/ bionic-updates main universe
	deb http://ports.ubuntu.com/ubuntu-ports/ bionic-security main universe
	deb-src http://ports.ubuntu.com/ubuntu-ports/ bionic-security main universe
	```

- Make sure the **configs** module is loaded at boot.

	```
	modprobe configs
	echo configs >> /etc/modules
	```

- Install Mango DAS. Use the scripts and replace the /etc/init.d with the new systemd unit file. Also run script to symlink DAS logs to /var/log/das and configs to /etc/das.

	- **Instructions needed for systemd** Benefit of moving to this is security--better containerization for DAS process.

	- **bcrypt fails to build** gem update to fix is not merged yet.

- Install AWS Greengrass dependencies.

	```
	sudo apt install python2.7 python3.7 nodejs openjdk-8-jdk-headless openjdk-11-jdk-headless zlib1g-dev python-pip python3-pip build-essential unzip bash-completion rsync etckeeper
	sudo python3.7 -m pip install greengrasssdk
	ln -s /usr/lib/jvm/java-8-openjdk-armhf/jre/bin/java /usr/bin/java8
	ln -s /usr/bin/nodejs /usr/bin/nodejs8.10
	```

- Install AWS Greengrass
	
- Check Greengrass dependencies

- Image saved.

	- Note: If a large SD card is used, but the partitions of the file systems are limited to something smaller (e.g. 8G), then an .img file can be truncated with the **truncate** tool, available as a cask in Mac Homebrew. the **dd** utility can also be involved with the **bs** and **count** parameter to stop the read. Example:
	
	```
	# 8G filesystem
	sudo dd \
			if=infile.img
			of=outfile.img
			bs=32m
			count=256
	```

## Debian 10 Stock Image Setup

- Plug Ethernet into LAN0, which is the interface physically next to the WAN port. The WAN port is the port that is upside down.

	This port is configured as:
	- IP: 192.168.0.10
	- Subnet: 255.255.255.0

- Connect vis SSH using username:root / password:bananapi.

	- Using Mac/Linux to upload key and connect:

		- ssh-copy-id root@192.168.0.10
		- ssh root@192.168.0.10

- Configure network to be compatible with environment

	- Example: (Mac Defaults)
 
		- Edit ```/etc/network/interfaces``` to be static IP **192.168.2.6** and default route **192.168.2.1**

		- Edit ```/etc/resolv.conf``` to be **192.168.2.1**

- Reset the interface: ```ifdown lan0 ; ifup lan0```

	__It may be necessary to restart the router.__

- Check interfaces with ```ip addr show```. Check route with ```ip route```.

- Update the system. ```apt update && apt upgrade```



## Additional Information

### Connecting to VMWare Fusion

- The SDCard can be connected to a Linux VM using VMWare Fusion, but USB3.0 mode must be enabled in the advanced USB settings of the VM in order for the volume to be detected. The **/dev/sdb2** volume is an **ext4** volume.

### Intial Ethernet Port Configuration

```console
1: lo: <LOOPBACK,UP,LOWER_UP> mtu 65536 qdisc noqueue state UNKNOWN group default qlen 1000
    link/loopback 00:00:00:00:00:00 brd 00:00:00:00:00:00
    inet 127.0.0.1/8 scope host lo
       valid_lft forever preferred_lft forever
    inet6 ::1/128 scope host 
       valid_lft forever preferred_lft forever
2: eth0: <BROADCAST,MULTICAST,UP,LOWER_UP> mtu 1500 qdisc pfifo_fast state UP group default qlen 1000
    link/ether a6:f0:c6:49:9a:74 brd ff:ff:ff:ff:ff:ff
    inet6 fe80::a4f0:c6ff:fe49:9a74/64 scope link 
       valid_lft forever preferred_lft forever
3: eth1: <BROADCAST,MULTICAST,UP,LOWER_UP> mtu 1500 qdisc pfifo_fast state UP group default qlen 1000
    link/ether 62:3e:a6:41:b5:11 brd ff:ff:ff:ff:ff:ff
    inet6 fe80::603e:a6ff:fe41:b511/64 scope link 
       valid_lft forever preferred_lft forever
4: wan@eth1: <NO-CARRIER,BROADCAST,MULTICAST,UP> mtu 1500 qdisc noqueue state LOWERLAYERDOWN group default qlen 1000
    link/ether 62:3e:a6:41:b5:11 brd ff:ff:ff:ff:ff:ff
5: lan0@eth0: <BROADCAST,MULTICAST,UP,LOWER_UP> mtu 1500 qdisc noqueue state UP group default qlen 1000
    link/ether 02:01:02:03:04:00 brd ff:ff:ff:ff:ff:ff
    inet 192.168.0.10/24 brd 192.168.0.255 scope global lan0
       valid_lft forever preferred_lft forever
    inet6 fe80::1:2ff:fe03:400/64 scope link 
       valid_lft forever preferred_lft forever
6: lan1@eth0: <NO-CARRIER,BROADCAST,MULTICAST,UP> mtu 1500 qdisc noqueue master br0 state LOWERLAYERDOWN group default qlen 1000
    link/ether a6:f0:c6:49:9a:74 brd ff:ff:ff:ff:ff:ff
7: lan2@eth0: <NO-CARRIER,BROADCAST,MULTICAST,UP> mtu 1500 qdisc noqueue master br0 state LOWERLAYERDOWN group default qlen 1000
    link/ether a6:f0:c6:49:9a:74 brd ff:ff:ff:ff:ff:ff
8: lan3@eth0: <NO-CARRIER,BROADCAST,MULTICAST,UP> mtu 1500 qdisc noqueue state LOWERLAYERDOWN group default qlen 1000
    link/ether a6:f0:c6:49:9a:74 brd ff:ff:ff:ff:ff:ff
9: br0: <NO-CARRIER,BROADCAST,MULTICAST,UP> mtu 1500 qdisc noqueue state DOWN group default qlen 1000
    link/ether a6:f0:c6:49:9a:74 brd ff:ff:ff:ff:ff:ff
    inet 192.168.40.1/24 brd 192.168.40.255 scope global br0
       valid_lft forever preferred_lft forever
10: lan3.60@lan3: <NO-CARRIER,BROADCAST,MULTICAST,UP> mtu 1500 qdisc noqueue state LOWERLAYERDOWN group default qlen 1000
    link/ether 02:01:02:03:04:03 brd ff:ff:ff:ff:ff:ff
    inet 192.168.60.10/24 brd 192.168.60.255 scope global lan3.60
       valid_lft forever preferred_lft forever
```

## Working Multiport /etc/network/interfaces

```
#cpu-ports (eth0+1) bring up without any ip-configuration
auto eth0
iface eth0 inet manual
  pre-up ip link set $IFACE up
  post-down ip link set $IFACE down

#2nd gmac (cpu-port) only in 4.14.53+
auto eth1
iface eth1 inet manual
  pre-up ip link set $IFACE up
  post-down ip link set $IFACE down

#wan-port as dhcp-client
auto wan
allow-hotplug wan
iface wan inet dhcp

#lan0 has fixed address
auto lan0
iface lan0 inet static
  #hwaddress ether 08:00:00:00:00:00 # if you want to set MAC manually
  address 192.168.2.6             #Chanaged !!
  netmask 255.255.255.0
  gateway 192.168.2.1             #Changed !!
  pre-up ip link set $IFACE address 02:01:02:03:04:00 up
#  pre-up ip link set $IFACE up
  post-down ip link set $IFACE down

#lan1+lan2 in bridge (same subnet)
auto lan1
iface lan1 inet manual
auto lan2
iface lan2 inet manual

auto br0
iface br0 inet static
    address 192.168.40.1
    netmask 255.255.255.0
    bridge_ports lan1 lan2
    bridge_fd 5
    bridge_stp no

#auto lan3
#iface lan3 inet manual
#lan3-port as dhcp-client
auto lan3
allow-hotplug lan3
iface lan3 inet dhcp


#example of vlan-configuration
auto lan3.60
iface lan3.60 inet static
  address 192.168.60.10
  netmask 255.255.255.0
#  gateway 192.168.0.5
  pre-up ip link set $IFACE address 02:01:02:03:04:03 up


source-directory /etc/network/interfaces.d
```