#-*- coding:utf8 -*-

import os,sys
import sqlite3
import datetime

def notnull(v):
	if v is None:
		return ''
	return str(v)

try:
	command=sys.argv[1]
except:
	command=None
if command not in ('set','list'):
	print("Usage: python %s [set|list]"%sys.argv[0])
	print(" - set = reset all leaf and open an interative console to set each leaf from 0")
	print(" - list = list all data")
	sys.exit(-1)

dbconn = sqlite3.connect('database.sqlite3', timeout=5)
dbcur = dbconn.cursor()

if command=='set':
	print("Please entry IP and hostname (separated with a space) for each leaf. Leave empty to quit")
	print("ctrl-c to cancel")
	print("")
	alldata=[]
	leaf_id=1
	while True:
		newdata=input(' - Leaf %d (ip hostname): '%leaf_id)
		newdata=newdata.strip()
		if newdata=='':
			break
		newdata=list(filter(None,newdata.split(' '))) #remove bad space
		if len(newdata)!=2:
			print("bad format. Please entry IP and hostname (separated with a space)")
			continue
		alldata.append([leaf_id, newdata[0], newdata[1]])
		leaf_id+=1
	print("writing...")
	dbcur.execute('DELETE FROM leaf')
	
	for id,ip,hostname in alldata:
		dbcur.execute('INSERT INTO leaf (leaf_id, ip, hostname) VALUES (?, ?, ?)',[id,ip,hostname])
	dbconn.commit()
	print('done')
		
	sys.exit(0)


if command=='list':
	nbLeaf=0
	for row in dbcur.execute('SELECT leaf_id,ip,hostname,controller_id,switch_id,switch_port,lastset FROM leaf ORDER BY leaf_id'):
		nbLeaf+=1
		print("Leaf %s"%notnull(row[0]))
		print(" - IP = %s"%notnull(row[1]))
		print(" - Hostname = %s"%notnull(row[2]))
		print(" - controller_id = %s"%notnull(row[3]))
		print(" - switch_id = %s"%notnull(row[4]))
		print(" - switch_port = %s"%notnull(row[5]))
		dt=''
		if row[6]:
			dt = datetime.datetime.fromtimestamp(row[6]).isoformat()
		print(" - last request = %s"%dt)
		print()
	
	

