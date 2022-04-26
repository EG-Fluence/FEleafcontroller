#!/bin/bash
rm -fv /etc/machine-id /var/lib/dbus/machine-id
dbus-uuidgen --ensure
systemd-machine-id-setup

/bin/rm -v /etc/ssh/ssh_host_*
/usr/sbin/dpkg-reconfigure openssh-server
systemctl reload ssh

