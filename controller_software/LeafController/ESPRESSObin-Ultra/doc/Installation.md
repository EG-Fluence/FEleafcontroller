# Manual installation

```bash
root@ccpe10c18a:/etc# ls -la /etc/resolv.conf
lrwxrwxrwx 1 root root 39 Aug 14 00:54 /etc/resolv.conf -> ../run/systemd/resolve/stub-resolv.conf

mv /etc/resolve.conf /etc/resolve.conf_org

cp /etc/resolve.conf_org /etc/resolve.conf
```

Nano editor is not installed, feel pain and use __vi__
```bash
vi /etc/resolve.conf
```

change nameserver 192.168.178.1 or any other local server

```bash
apt update
apt upgrade
apt install nano
```
