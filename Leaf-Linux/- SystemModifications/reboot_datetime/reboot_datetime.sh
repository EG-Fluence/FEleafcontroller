# crontab file line:
# @reboot (sh /logs/reboot_datetime.sh)

timedatectl >>/logs/reboot_datetime.log
echo >>/logs/reboot_datetime.log
