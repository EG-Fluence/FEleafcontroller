#linux: /usr/bin/python3 /logs/reset_datetime.py
#
#crontab: @reboot (sh /usr/bin/python3 /logs/reset_datetime.py)


from datetime import datetime

with open('reboot_datetime.log', 'a') as f:

	# datetime object containing current date and time
	now = datetime.now()
	 
	s =  ""
	s += "+++++++++++++++++++++++++++++++++++++++++++\n"
	s += "Reset DateTime = " + str(now) + "\n"
	s += "-------------------------------------------\n\n"

	f.write(s)
