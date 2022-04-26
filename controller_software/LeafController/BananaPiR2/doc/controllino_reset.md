The fastest way to reset Controllino from any Linux computer.
Just touch serial port with Arduino:
 
 stty -F /dev/ttyACM0
 
Nice trick, this happens exactly in upload of new sketch. Anything sent to port will reset Controllino. Nice to recovery Controllino or add to cron every hour.
