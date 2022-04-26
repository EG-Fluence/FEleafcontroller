## Image Creation Notes

### Descriptive Notes

- System is **Ubuntu 18.04 LTS** with **main** and **universe** repositories enabled.

- System is configured to fully utilize **systemd** without compatibility layer.

- Network is configued with **netplan**.

- Configuration scripts are in ```/usr/local/bin/fluence_*```. 

- IPV6 is disabled.

- The tool **[etckeeper](https://www.vultr.com/docs/using-etckeeper-for-version-control-of-etc)** is installed to track changes in ```/etc```. By default, changes are committed whenever **apt install** is run, but it can also be run manually when making configuration changes.

- Log rotation has not been configured.

- Advancion software is not installed.

- The image will be kept to 15G to support storing two full system images on a 32G SD card.

- The **config** kernel module is loaded by default.

- Kernel is built to fully support kernel containerization features needed by AWS Greengrass; VLAN support; and cgroups (bpf), iptables, and ebtables firewalling support.  Details in [kernel_build.md](kernel_build.md).

- SSH is configured to permit root login. A unique host key is created on first boot.

- The default username and password is **root/root**.

- The presence of the file ```/etc/FLUENCE_INITIALIZED``` indicates that the device has been booted once and initial configuration scripts have been run.

- Default installed packages are documented in [manual_packages.txt](manual_packages.txt)

- The Mango IAS DAS is installed in ```/srv/das/```. Logs and configuration directories are symlinked into ```/var/log/das``` and ```/etc/das```. Packaged ***openjdk-11-jdk-headless*** is used based on testing of a few different Java versions.

- AWS Greengrass is installed in ```/srv/aws/``` but not configured. Dependencies have been installed to ensure it fully passes the AWS compatability checker, including optional dependencies. This includes the **nodejs** and **openjdk-8-jdk-headless** packages to enable support for these languages. AWS greengrass requires the **python3.7** package.

- Default runlevel is set to **2**.

### AWS Greengrass Notes

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

### DAS Notes

- Install Mango DAS. Use the scripts and replace the /etc/init.d with the new systemd unit file. Also run script to symlink DAS logs to /var/log/das and configs to /etc/das.

	- **Instructions needed for systemd** Benefit of moving to this is security--better containerization for DAS process.

	- **bcrypt fails to build** gem update to fix is not merged yet.

### Flash Image

-  Example using **dd** to flash an image in MacOS.

	```bash
	diskutil list
	diskutil unmountDisk disk_
	sudo dd \
	  if=ubuntu-18.04-bpi-r2-preview.img \
	  of=/dev/rdisk_ \
	  bs=32M

### Save Image

- Example using **dd* to save an image (MacOS). Assuming the partition table is 8G.

	- Note: If a large SD card is used, but the partitions of the file systems are limited to something smaller (e.g. 8G), then an .img file can be truncated with the **truncate** tool, available as a cask in Mac Homebrew. the **dd** utility can also be involved with the **bs** and **count** parameter to stop the read. Example:
	
	```
	# 8G filesystem
	sudo dd \
			if=/dev/rdisk2
			of=outfile.img
			bs=32M
			count=256
	```
