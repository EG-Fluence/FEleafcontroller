#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import os
import sys
import flask
import json
import threading
import sqlite3
import time
import traceback
import functools
import configparser
import csv
import itertools
import re
import argparse


#==== CONSTANTS 1/2 ====
SCRIPT_PATH = os.path.dirname( os.path.abspath( sys.argv[0] ))
VERSION=0 #updated after read config file

BREAKER_MIN=1
BREAKER_MAX=24

FEEDER_MIN=1
FEEDER_MAX=24

CORE_MIN=1
CORE_MAX=9

NODE_MIN=1
NODE_MAX=6
NODE_NONE=0

ROW_MIN=1
ROW_MAX=2
ROW_NONE=0

CUBE_MIN=1
CUBE_MAX=16
CUBE_NONE=0

#==== ARGUMENTS ====
parser = argparse.ArgumentParser()
parser.add_argument("-t", "--template", type=str, help="Use a CSV export from Template to initialize database (loop list)")
parser.add_argument("-d", "--database", type=str, help="database file path")
parser.add_argument("-c", "--config", type=str, help="configuration file path")
SCRIPT_ARGS = parser.parse_args()

#==== CONSTANTS 2/2 ====
if SCRIPT_ARGS.database:
	DATABASE_FILENAME = os.path.normpath( os.path.abspath( SCRIPT_ARGS.database ) )
else:
	DATABASE_FILENAME = os.path.join( SCRIPT_PATH, 'files/database.sqlite3' )
if SCRIPT_ARGS.config:
	CONFIG_FILENAME = os.path.normpath( os.path.abspath( SCRIPT_ARGS.config ) )
else:
	CONFIG_FILENAME = os.path.join( SCRIPT_PATH, 'config.ini' )



#==== TOOLS ====
def intval(v):
	"""convert to int, return None if fails"""
	try:
		return int(v)
	except Exception as e:
		return None
def floatval(v):
	"""convert to float, return None if fails"""
	try:
		return float(v)
	except Exception as e:
		print('Except floatval(%s) : %s %s'%(repr(v),str(type(e)),str(e)))
		return None
def trim(v):
	"""remove spaces in begin and end. If it's a 'not' value, return the value as it"""
	if not v:
		return v
	return str(v).strip()

def responseJson(code,message):
	"""create a 'response' object for json communication"""
	resp = flask.jsonify(message)
	resp.status_code = code
	return resp

def autoExcCatch(func):
	"""allows to catch except for functions which hide their exceptions"""
	@functools.wraps(func)
	def wrapper(*args, **kwargs):
		try:
			return func(*args, **kwargs)
		except Exception as e:
			print('-----------')
			try:
				funcName=func.__qualname__+'()'
			except:
				funcName=str(func)
			print(funcName, type(e), e)
			traceback.print_exc() #doesn't work
			print('-----------')
			return responseJson(500,str(e))
	return wrapper

def dbInsert(dbcur,tableName,data):
	"""insert new row in database from a dict"""
	columns=[]
	values=[]
	for k,v in data.items():
		columns.append(k)
		values.append(v)
	dbcur.execute('INSERT INTO '+tableName+' ( ' + (','.join(columns)) +' ) VALUES ( ' + (','.join(['?']*len(values))) +' )', values)
def dbUpdate(dbcur, tableName, data, conditions):
	"""update data in database from a dict"""
	columns=[]
	colConds=[]
	values=[]
	for k,v in data.items():
		columns.append("%s=?"%(k))
		values.append(v)
	for k,v in conditions.items():
		colConds.append("%s=?"%(k))
		values.append(v)
	
	dbcur.execute('UPDATE '+tableName+' SET ' + (", ".join(columns)) +' WHERE ' + (" AND ".join(colConds)), values)


#==== MAIN CLASS =====
class Application:
	def __init__(self,config):
		self.dblock = threading.Lock() #allows a thread-safe access to database
		self._site_code = config.get('DEFAULT','site_code')
		self._feeder_as_letter = config.getboolean('DEFAULT','feeder_as_letter', fallback=False)
		self._domain = config.get('DEFAULT','domain') #since config v3
		self._hostname_format=config.get('DEFAULT','hostname_format', fallback='') #until config v2
		self._ip_format=config.get('DEFAULT','ip_format', fallback='') #only config v1
		self._gateway_format=config.get('DEFAULT','gateway_format', fallback='') #only config v1
		self._position_offset=config.getint('DEFAULT','position_offset',fallback=0) #only config v1
		self._log_max_day=config.getint('DEFAULT','log_max_day')
		self._log_max_size=config.getint('DEFAULT','log_max_size')
		self._files_folder = os.path.join( SCRIPT_PATH, config.get('DEFAULT','files_folder', fallback='files') )
	def getDB(self):
		return sqlite3.connect(DATABASE_FILENAME, timeout=5) #timeout is for manage write/lock time
	def getSiteCode(self):
		return self._site_code
	def isFeederAsLetter(self):
		return self._feeder_as_letter
	def getLogParams(self):
		return int(time.time()-self._log_max_day*86400), self._log_max_size
	def getFilesFolder(self):
		return self._files_folder
	def computeIp(self, core, node, position, feeder, cube, breaker, row, devtype):
		if CONFIG_VERSION>=3:
			cube=position
			if devtype=='CuMC':
				return '%d.%d.%d.%d/21'%(
					10,
					breaker*10+feeder if 0<feeder<=24 else 0,
					core*8+node if 0<core<=9 and 0<node<=6 else 0,
					100*row+cube if 0<cube<=16 and 0<row<=2 else 0
				)
			if devtype=='CMC':
				return '%d.%d.%d.%d/21'%(
					10,
					breaker*10+feeder if 0<feeder<=24 else 0,
					core*8 if 0<core<=9 else 0,
					3
				)
			if devtype=='NMC':
				return '%d.%d.%d.%d/21'%(
					10,
					breaker*10+feeder if 0<feeder<=24 else 0,
					core*8+node if 0<core<=9 and 0<node<=6 else 0,
					3
				)
			if devtype=='CTRMC':
				return '%d.%d.%d.%d/21'%(
					10,
					breaker*10+feeder if 0<feeder<=24 else 0,
					core*8 if 0<core<=9 else 0,
					161
				)
			return '0.0.0.0/32'
		if CONFIG_VERSION>=2:
			return '%d.%d.%d.%d/23'%(
				10,
				feeder if 0<feeder<=41 else 0,
				core*10+node if 0<core<=25 and 0<node<=4 else 0,
				10*cube+position if 0<cube<=24 and 0<position<=9 else 0
			)
		data={'core':core, 'node':node, 'row':position, 'position':position+self._position_offset, 'site_code':self._site_code}
		return self._ip_format.format(**data)
	def computeGateway(self, core, node, position, feeder, cube, breaker, row, devtype):
		if CONFIG_VERSION>=3:
			cube=position
			if devtype=='CuMC':
				return '%d.%d.%d.%d'%(
					10,
					breaker*10+feeder if 0<feeder<=24 else 0,
					core*8 if 0<core<=9 and 0<node<=6 else 0,
					1
				)
			if devtype in ('CMC','NMC','CTRMC'):
				return '%d.%d.%d.%d'%(
					10,
					breaker*10+feeder if 0<feeder<=24 else 0,
					core*8 if 0<core<=9 else 0,
					1
				)
			return '0.0.0.0'
		if CONFIG_VERSION>=2:
			return '%d.%d.%d.%d'%(
				10,
				feeder if 0<feeder<=41 else 0,
				core*10+node if 0<=core<=25 and 0<=node<=4 else 0,
				1
			)
		data={'core':core, 'node':node, 'row':position, 'position':position+self._position_offset, 'site_code':self._site_code}
		return self._gateway_format.format(**data)
	def computeHostname(self, core, node, position, feeder, cube, breaker, row, devtype):
		if CONFIG_VERSION>=3:
			cube=position
		data={
			'core':core,'node':node, 'row':row, 'position':position, 
			'site_code':self._site_code, 'feeder':feeder, 'cube':cube, 'breaker':breaker,
			'domain':self._domain,'domainext':'.'+self._domain if self._domain else self._domain
		}
		computedIp=self.computeIp(core, node, position, feeder, cube, breaker, row, devtype)
		
		ipmask = computedIp.split('/')
		try:
			data['mask'] = int(ipmask[1])
		except Exception as e:
			data['mask'] = 0
		
		ip = ipmask[0].split('.')
		try:
			data['ip1'] = int(ip[0])
		except Exception as e:
			data['ip1'] = 0
		try:
			data['ip2'] = int(ip[1])
		except Exception as e:
			data['ip2'] = 0
		try:
			data['ip3'] = int(ip[2])
		except Exception as e:
			data['ip3'] = 0
		try:
			data['ip4'] = int(ip[3])
		except Exception as e:
			data['ip4'] = 0
		
		
		if CONFIG_VERSION<3:
			return self._hostname_format.format(**data)
		
		hostname = '{site_code}'.format(**data)
		
		if self._feeder_as_letter:
			hostname += "f{:02d}{}".format(data['breaker'], chr(96+data['feeder']) if data['feeder'] else '')
		else:
			hostname += "f{ip2:03d}".format(**data)
		
		
		if devtype=='CuMC':
			return hostname + 'c{core:02d}n{node:02d}r{row:02d}l{cube:02d}{domainext}'.format(**data)
		if devtype=='CMC':
			return hostname + 'c{core:02d}n{node:02d}r00z00{domainext}'.format(**data)
		if devtype=='CTRMC':
			return hostname + 'd{core:02d}n00r00p01{domainext}'.format(**data) #enst10f011d01n00r00p01
		if devtype=='NMC':
			return hostname + 'c{core:02d}n{node:02d}r00z01{domainext}'.format(**data)
		return ''
	
	def resetWithTemplateFile(self, filename):
		"""clear loop and leaf list and apply the template on loop"""
		loopData={}
		nbError=0
		
		with open(filename,'r',encoding='utf8') as fdfile:
			fdcsv=csv.reader(fdfile, delimiter=';', quotechar='"')
			
			#-- load header and rename columns
			header=[]
			for col in next(fdcsv):
				col=col.lower().strip()
				if 'breaker' in col and 'feeder' in col and '#' in col:
					header.append('breaker-feeder')
				elif 'core' in col and '#' in col:
					header.append('core')
				elif 'node' in col and '#' in col:
					header.append('node')
				elif 'row' in col and '#' in col:
					header.append('row')
				elif 'device' in col and '#' in col:
					header.append('device')
				elif col in ('switch','port'):
					header.append(col)
				else:
					header.append('_'+col) #useless columns
			
			#-- check columns
			for essentialColumn in ['breaker-feeder','core','node','row','device','switch','port']:
				if essentialColumn in header:
					continue
				print('ERROR: column "{}" is not found'.format(essentialColumn))
				nbError+=1
			if nbError:
				return False
			
			#-- read data
			for line in fdcsv:
				row = dict(itertools.zip_longest(header,line))
				
				for k in ['breaker-feeder','core','node','row','device']:
					row[k]=row[k].lower()
				
				if not re.match('^[lz][0-9]+$',row['device']): #very light first filter
					continue
				
				valueSwitch = row['switch'].strip()
				valuePorts = list(map( str.strip, (row['port']+'/').replace('&','/').replace('|','/').split('/') ))
				value=int(row['breaker-feeder'][1:])
				valueBreaker,valueFeeder = value//10, value%10
				valueCore = int(row['core'][1:])
				
				if row['device']=='z00': #core controller
					loopKey=valueSwitch+'-'+row['breaker-feeder']+'-'+row['core']+'-'+row['device']
					
					#check values
					if loopKey in loopData:
						print('ERROR: core controller already defined in '+loopKey)
						nbError+=1
					if not valueSwitch:
						print('ERROR: core controller has not switch in '+loopKey)
						nbError+=1
					if not valuePorts[0]:
						print('ERROR: core controller has not west-port in '+loopKey)
						nbError+=1
					
					if not BREAKER_MIN<=valueBreaker<=BREAKER_MAX:
						print('ERROR: core controller has wrong value for Breaker "{}" in {}'.format(valueBreaker,loopKey))
						nbError+=1
					if not FEEDER_MIN<=valueFeeder<=FEEDER_MAX:
						print('ERROR: core controller has wrong value for Feeder "{}" in {}'.format(valueBreaker,loopKey))
						nbError+=1
					if not CORE_MIN<=valueCore<=CORE_MAX:
						print('ERROR: core controller has wrong value for Core "{}" in {}'.format(valueCore,loopKey))
						nbError+=1
					
					#store
					loopData[loopKey]={
						'devtype':'CMC',
						'switch':valueSwitch,
						'wport':valuePorts[0],
						'breaker':valueBreaker,
						'feeder':valueFeeder,
						'core':valueCore,
					}
				elif row['device']=='z01': #node controller
					loopKey=valueSwitch+'-'+row['breaker-feeder']+'-'+row['core']+'-'+row['node']+'-'+row['device']
					valueNode = int(row['node'][1:])
					
					#check values
					if loopKey in loopData:
						print('ERROR: node controller already defined in '+loopKey)
						nbError+=1
					if not valueSwitch:
						print('ERROR: node controller has not switch in '+loopKey)
						nbError+=1
					if not valuePorts[0]:
						print('ERROR: node controller has not west-port in '+loopKey)
						nbError+=1
					
					if not BREAKER_MIN<=valueBreaker<=BREAKER_MAX:
						print('ERROR: node controller has wrong value for Breaker "{}" in {}'.format(valueBreaker,loopKey))
						nbError+=1
					if not FEEDER_MIN<=valueFeeder<=FEEDER_MAX:
						print('ERROR: node controller has wrong value for Feeder "{}" in {}'.format(valueBreaker,loopKey))
						nbError+=1
					if not CORE_MIN<=valueCore<=CORE_MAX:
						print('ERROR: node controller has wrong value for Core "{}" in {}'.format(valueCore,loopKey))
						nbError+=1
					if not NODE_MIN<=valueNode<=NODE_MAX:
						print('ERROR: node controller has wrong value for Node "{}" in {}'.format(valueNode,loopKey))
						nbError+=1
					
					#store
					loopData[loopKey]={
						'devtype':'NMC',
						'switch':valueSwitch,
						'wport':valuePorts[0],
						'breaker':valueBreaker,
						'feeder':valueFeeder,
						'core':valueCore,
						'node':valueNode,
					}
				elif row['device'].startswith('l'): #cube
					loopKey=valueSwitch+'-'+row['breaker-feeder']+'-'+row['core']+'-'+row['node']+'-'+row['row']
					valueNode = int(row['node'][1:])
					valueRow = int(row['row'][1:])
					valueLeaf = int(row['device'][1:])
					
					#prepare more data
					if not valueSwitch:
						print('ERROR: cube has not switch in '+loopKey)
						nbError+=1
					if not valuePorts[0] or not valuePorts[1]:
						print('ERROR: cube has not west-port or east-port in '+loopKey)
						nbError+=1
					
					#check values
					if not BREAKER_MIN<=valueBreaker<=BREAKER_MAX:
						print('ERROR: cube has wrong value for Breaker "{}" in {}'.format(valueBreaker,loopKey))
						nbError+=1
					if not FEEDER_MIN<=valueFeeder<=FEEDER_MAX:
						print('ERROR: cube has wrong value for Feeder "{}" in {}'.format(valueBreaker,loopKey))
						nbError+=1
					if not CORE_MIN<=valueCore<=CORE_MAX:
						print('ERROR: cube has wrong value for Core "{}" in {}'.format(valueCore,loopKey))
						nbError+=1
					if not NODE_MIN<=valueNode<=NODE_MAX:
						print('ERROR: cube has wrong value for Node "{}" in {}'.format(valueNode,loopKey))
						nbError+=1
					if not ROW_MIN<=valueRow<=ROW_MAX:
						print('ERROR: cube has wrong value for Row "{}" in {}'.format(valueRow,loopKey))
						nbError+=1
					if not CUBE_MIN<=valueLeaf<=CUBE_MAX:
						print('ERROR: cube has wrong value for Cube "{}" in {}'.format(valueLeaf,loopKey))
						nbError+=1
					
					#store
					if not loopKey in loopData:
						loopData[loopKey]={
							'devtype':'CuMC',
							'switch':valueSwitch,
							'wport':valuePorts[0],
							'eport':valuePorts[1],
							'breaker':valueBreaker,
							'feeder':valueFeeder,
							'core':valueCore,
							'node':valueNode,
							'row':valueRow,
							'maxLeaf':0
						}
					loopData[loopKey]['maxLeaf'] = max(valueLeaf,loopData[loopKey]['maxLeaf'])
		
		#-- check
		if nbError>0:
			return False
		
		#-- process
		dbconn=application.getDB()
		dbcur=dbconn.cursor()
		with application.dblock:
			dbcur.execute('DELETE FROM leaf')
			dbcur.execute('DELETE FROM loop')
			dbcur.execute("DELETE FROM sqlite_sequence WHERE name='leaf'")
			dbcur.execute("DELETE FROM sqlite_sequence WHERE name='loop'")
			dbconn.commit()
			dbcur.execute('VACUUM')
			
			for _,loopItem in loopData.items():
				data={
					'devtype':loopItem['devtype'],
					'switch_name':loopItem['switch'],
					'switch_interface_west':loopItem['wport'],
					'switch_interface_east':'',
					'breaker':loopItem['breaker'],
					'feeder':loopItem['feeder'],
					'core':loopItem['core'],
					'node':0,
					'row':0,
					'number_leaves':0,
				}
				if loopItem['devtype'] in ('CuMC','NMC'):
					data['node']=loopItem['core']
				if loopItem['devtype'] == 'CuMC':
					data['row']=loopItem['row']
					data['switch_interface_east']=loopItem['eport']
					data['number_leaves']=loopItem['maxLeaf']
				
				dbInsert(dbcur,'loop',data)
			dbconn.commit()
		return True

if not os.path.exists(DATABASE_FILENAME):
	print('Database file not found at "{}"'.format(DATABASE_FILENAME), file=sys.stderr)
	sys.exit(-1)

if not os.path.exists(CONFIG_FILENAME):
	print('Config file not found at "{}"'.format(CONFIG_FILENAME), file=sys.stderr)
	sys.exit(-1)

config = configparser.ConfigParser()
config.read(CONFIG_FILENAME)

try:
	CONFIG_VERSION=config.getint('DEFAULT','version',fallback=1)
except Exception as e:
	CONFIG_VERSION=1

application=Application(config)


#==== FUNCTIONS ====
def _readLoopDataFromRequest(reqdata):
	"""extract data sent by UI and check and fix values"""
	#-- get data from request ----
	data={
		'switch_name':trim(reqdata.get('switch_name')),
		'switch_interface_west':trim(reqdata.get('switch_interface_west')),
		'switch_interface_east':trim(reqdata.get('switch_interface_east')),
		'loop_core':intval(reqdata.get('core')),
		'loop_node':intval(reqdata.get('node')),
		'number_leaves':intval(reqdata.get('number_leaves')),
	}
	
	if CONFIG_VERSION==2:
		data.update({
			'loop_feeder':intval(reqdata.get('feeder')),
			'loop_cube':intval(reqdata.get('cube')),
		})
	if CONFIG_VERSION>=3:
		data.update({
			'loop_devtype':reqdata.get('devtype'),
			'loop_breaker':intval(reqdata.get('breaker')),
			'loop_feeder':intval(reqdata.get('feeder')), #config v2
			'loop_row':intval(reqdata.get('row')),
		})
	
	#-- check data ----
	if not data['switch_name'] or not data['switch_interface_west'] or not data['loop_core']:
		return (False,data)
	
	if CONFIG_VERSION==2:
		if not data['loop_feeder'] or not data['loop_cube'] or not data['loop_node'] or not data['number_leaves']:
			return (False,data)
	if CONFIG_VERSION>=3:
		if not data['loop_devtype'] or not data['loop_breaker'] or not data['loop_feeder']:
			return (False,data)
		if not data['loop_devtype'] in ['CuMC','CMC','NMC','CTRMC']:
			return (False,data)
		if not BREAKER_MIN<=data['loop_breaker']<=BREAKER_MAX:
			return (False,data)
		if not FEEDER_MIN<=data['loop_feeder']<=FEEDER_MAX:
			return (False,data)
		if not CORE_MIN<=data['loop_core']<=CORE_MAX:
			return (False,data)
		
		if data['loop_devtype'] in ('CuMC','NMC'):
			if not NODE_MIN<=data['loop_node']<=NODE_MAX:
				return (False,data)
		else:
			data['loop_node']=NODE_NONE
		
		if data['loop_devtype']=='CuMC':
			if not ROW_MIN<=data['loop_row']<=ROW_MAX:
				return (False,data)
			if not CUBE_MIN<=data['number_leaves']<=CUBE_MAX:
				return (False,data)
			if not data['switch_interface_east']:
				return (False,data)
		else:
			data['loop_row']=ROW_NONE
			data['number_leaves']=CUBE_NONE
			data['switch_interface_east']=''
		
	else:
		if not data['switch_interface_east']:
			return (False,data)
	
	return (True,data)


def _loop_get(dbcur,loop_id):
	"""return data about a loop"""
	with application.dblock:
		dbcur.execute('SELECT id, switch_name, switch_interface_west, switch_interface_east, core, node, number_leaves, feeder, cube, breaker, row, devtype FROM loop WHERE id=?',[loop_id])
		row=dbcur.fetchone()
	if not row:
		return None #no content
	result={
		'id':row[0],
		'switch_name':row[1],
		'switch_interface_west':row[2],
		'switch_interface_east':row[3],
		'core':row[4],
		'node':row[5],
		'leaf_count':row[6], #deprecated
		'number_leaves':row[6],
	}
	if CONFIG_VERSION==2:
		result.update({
			'feeder':row[7],
			'cube':row[8],
		})
	if CONFIG_VERSION>=3:
		result.update({
			'breaker':row[9],
			'feeder':row[7],
			'row':row[10],
			'devtype':row[11],
		})
	return result
def _leaf_get(dbcur,leaf_id):
	"""return data about a leaf"""
	row=None
	
	with application.dblock:
		dbcur.execute("""
			SELECT loop.id, loop.switch_name, loop.switch_interface_west, loop.switch_interface_east, loop.core, loop.node,
				leaf.id, leaf.position, leaf.uuid, leaf.lastset,
				leaf.is_configured,
				loop.feeder, loop.cube,
				loop.breaker, loop.row, loop.devtype, 
				leaf.macaddress
			FROM leaf INNER JOIN loop ON (loop.id=leaf.loop_id)
			WHERE leaf.id=?
		""",[leaf_id])
		row=dbcur.fetchone()
	
	if not row:
		return None #no content
	
	result={
		'loop_id':row[0],
		'switch_name':row[1],
		'switch_interface_west':row[2],
		'switch_interface_east':row[3],
		'core':row[4],
		'node':row[5],
		'id':row[6],
		'position':row[7],
		'uuid':row[8],
		'lastset':row[9],
		'ip':application.computeIp(row[4],row[5],row[7],row[11],row[12],row[13],row[14],row[15]),
		'gateway':application.computeGateway(row[4],row[5],row[7],row[11],row[12],row[13],row[14],row[15]),
		'hostname':application.computeHostname(row[4],row[5],row[7],row[11],row[12],row[13],row[14],row[15]),
		'force-reconfigure':not bool(row[10]),
	}
	if CONFIG_VERSION==2:
		result.update({
			'feeder':row[11],
			'cube':row[12],
		})
	if CONFIG_VERSION>=3:
		result.update({
			'breaker':row[13],
			'feeder':row[11],
			'row':row[14],
			'devtype':row[15],
			'macaddress':row[16],
		})
	return result
def _leaf_getLastConfig(dbcur,leaf_id):
	"""return data about a leaf with last configuration given"""
	row=None
	with application.dblock:
		dbcur.execute("""
			SELECT loop.id, leaf.lastconfig_switch_name, leaf.lastconfig_port_west, leaf.lastconfig_port_east, leaf.lastconfig_core, leaf.lastconfig_node,
				leaf.id, leaf.position, leaf.uuid, leaf.lastset,
				leaf.is_configured,
				leaf.lastconfig_feeder, 0,
				leaf.lastconfig_breaker, leaf.lastconfig_row, leaf.lastconfig_devtype,
				leaf.lastconfig_hostname, leaf.lastconfig_ip, leaf.lastconfig_gateway,
				leaf.macaddress
			FROM leaf INNER JOIN loop ON (loop.id=leaf.loop_id)
			WHERE leaf.id=?
		""",[leaf_id])
		row=dbcur.fetchone()
	if not row:
		return None #no content
	
	result={
		'loop_id':row[0],
		'switch_name':row[1],
		'switch_interface_west':row[2],
		'switch_interface_east':row[3],
		'core':row[4],
		'node':row[5],
		'id':row[6],
		'position':row[7],
		'uuid':row[8],
		'macaddress':row[19],
		'lastset':row[9],
		'ip':row[17],
		'gateway':row[18],
		'hostname':row[16],
		'force-reconfigure':not bool(row[10]),
		'breaker':row[13],
		'feeder':row[11],
		'row':row[14],
		'devtype':row[15],
	}
	return result

def _inventory_findSource(dbcur, fromIP=None, fromUUID=None):
	"""find and list device from IP or UUID information"""
	
	querySearch="""
		SELECT leaf.id, leaf.lastconfig_breaker, leaf.lastconfig_feeder, leaf.lastconfig_core, leaf.lastconfig_node, leaf.lastconfig_row, leaf.lastconfig_devtype
		FROM leaf
	"""
	
	conditionSql=[]
	conditionData=[]
	
	if fromIP!=None:
		if '/' in fromIP:
			conditionSql.append('leaf.lastconfig_ip=?')
			conditionData.append(fromIP)
		else:
			conditionSql.append('leaf.lastconfig_ip LIKE ?')
			conditionData.append(fromIP+"/%")
	
	if fromUUID!=None:
		conditionSql.append('leaf.uuid=?')
		conditionData.append(fromUUID)
	
	if conditionSql:
		querySearch+=" WHERE leaf.uuid IS NOT NULL AND "+ (" AND ".join(conditionSql))+ " "
	
	fromList = []
	with application.dblock:
		for row in dbcur.execute(querySearch, conditionData):
			fromList.append({
				'id':row[0],
				'breaker':row[1],
				'feeder':row[2],
				'core':row[3],
				'node':row[4],
				'row':row[5],
				'devtype':row[6],
			})
	return fromList
def _inventory(dbcur, breaker=None, feeder=None, core=None, node=None, row=None, devtype=None):
	"""List all devices with optional filter (breaker, feeder, core, node, row and devtype)"""
	sqlCondition=[]
	dataCondition=[]
	
	if breaker:
		sqlCondition.append("leaf.lastconfig_breaker=?")
		dataCondition.append(breaker)
	if feeder:
		sqlCondition.append("leaf.lastconfig_feeder=?")
		dataCondition.append(feeder)
	if core:
		sqlCondition.append("leaf.lastconfig_core=?")
		dataCondition.append(core)
	if node:
		sqlCondition.append("leaf.lastconfig_node=?")
		dataCondition.append(node)
	if devtype:
		sqlCondition.append("leaf.lastconfig_devtype=?")
		dataCondition.append(devtype)
	
	query="""
		SELECT loop.id, leaf.lastconfig_switch_name, leaf.lastconfig_port_west, leaf.lastconfig_port_east, leaf.lastconfig_core, leaf.lastconfig_node,
			leaf.id, leaf.position, leaf.uuid, leaf.lastset,
			leaf.is_configured,
			leaf.lastconfig_feeder, 0,
			leaf.lastconfig_breaker, leaf.lastconfig_row, leaf.lastconfig_devtype,
			leaf.lastconfig_hostname, leaf.lastconfig_ip, leaf.lastconfig_gateway,
			leaf.macaddress
		FROM leaf INNER JOIN loop ON (loop.id=leaf.loop_id)
	"""
	if sqlCondition:
		query+=" WHERE "+ (" AND ".join(sqlCondition))+ " "
	query+=" ORDER BY leaf.lastconfig_switch_name, leaf.lastconfig_port_west, leaf.lastconfig_port_east, leaf.position, leaf.uuid "

	ret=[]
	with application.dblock:
		for row in dbcur.execute(query,dataCondition):
			ret.append({
				'id':row[6],
				'breaker':row[13],
				'feeder':row[11],
				'core':row[4],
				'node':row[5],
				'row':row[14],
				'position':row[7],
				'uuid':row[8],
				'macaddress':row[19],
				'lastset':row[9],
				'ip':row[17],
				'gateway':row[18],
				'hostname':row[16],
				'devtype':row[15],
			})
	
	return ret


def log_write_commit(name,message):
	"""store a log in database and commit transition"""
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	with application.dblock:
		log_write(dbcur,name,message)
		dbconn.commit()
def log_write(dbcur,name,message):
	"""store a log in database"""
	#-- format ----
	message = json.dumps(message)
	
	#-- limit manager ----
	maxDate, maxSize = application.getLogParams()
	dbcur.execute('SELECT date FROM log ORDER BY date DESC LIMIT 1 OFFSET ?',[maxSize])
	row=dbcur.fetchone()
	if row:
		maxDate=max(maxDate,row[0])
	dbcur.execute('DELETE FROM log WHERE date<=?',[maxDate])
	
	#-- write ----
	dbcur.execute('INSERT INTO log (date,name,message) VALUES (?,?,?)',[time.time(),name,message])


#==== REST API ====
api = flask.Flask(__name__)

#---- EXTERNAL CALL ----
@api.route('/leafcount',methods=['GET','POST'])
@autoExcCatch
def leafcount():
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	
	if flask.request.method == 'POST':
		reqdata = flask.request.get_json(force=True, silent=False)
	else:
		reqdata = flask.request.args

	switch_name = trim(reqdata.get('switch_name'))
	switch_interface_west = trim(reqdata.get('switch_interface_west'))
	switch_interface_east = trim(reqdata.get('switch_interface_east'))
	if not switch_name or (not switch_interface_west and not switch_interface_east):
		return responseJson(400,'wrong params')
	
	query='SELECT loop.number_leaves FROM loop WHERE loop.switch_name=?'
	queryParams=[switch_name]
	if switch_interface_west:
		query+=' AND loop.switch_interface_west=?'
		queryParams.append(switch_interface_west)
	if switch_interface_east:
		query+=' AND loop.switch_interface_east=?'
		queryParams.append(switch_interface_east)
	
	row=None
	with application.dblock:
		dbcur.execute(query,queryParams)
		row=dbcur.fetchone()
	if not row:
		return responseJson(204,None) #no content
	
	return responseJson(200,{
		'leaf_count': row[0], #deprecated
		'number_leaves': row[0],
	})


@api.route('/config',methods=['GET','POST'])
@autoExcCatch
def configuration():
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	
	if flask.request.method == 'POST':
		reqdata = flask.request.get_json(force=True, silent=False)
	else:
		reqdata = flask.request.args
	
	switch_name=reqdata.get('switch_name')
	switch_interface_west = trim(reqdata.get('switch_interface_west'))
	switch_interface_east = trim(reqdata.get('switch_interface_east'))
	leaf_uuid = trim(reqdata.get('leaf_uuid'))
	leaf_position = intval(reqdata.get('leaf_position',0))
	leaf_macaddress= trim(reqdata.get('macaddress',''))
	
	logData={'switch_name':switch_name,'switch_interface_west':switch_interface_west,'switch_interface_east':switch_interface_east,'leaf_uuid':leaf_uuid,'macaddress':leaf_macaddress,'leaf_position':leaf_position}
	
	if not switch_name or not leaf_uuid or ( not switch_interface_west and not switch_interface_east):
		log_write_commit("config failed - wrong parameters",logData)
		return responseJson(400,'wrong parameters')
	
	
	query="""
		SELECT loop.id, leaf.id, COALESCE(leaf.is_configured,0), loop.devtype, loop.number_leaves
		FROM loop
			LEFT OUTER JOIN leaf ON (loop.id=leaf.loop_id AND leaf.position=?)
		WHERE loop.switch_name=?
	"""
	queryParams=[leaf_position,switch_name]
	if switch_interface_west:
		query+=' AND loop.switch_interface_west=?'
		queryParams.append(switch_interface_west)
	if switch_interface_east:
		query+=' AND loop.switch_interface_east=?'
		queryParams.append(switch_interface_east)
	query+=' LIMIT 1'
	
	row=None
	with application.dblock:
		dbcur.execute(query,queryParams)
		row=dbcur.fetchone()
	
	#-- not found case ----
	if not row:
		log_write_commit("config failed - not found",logData)
		return responseJson(204,None) #no content
	
	#-- found case ----
	loop_id=row[0]
	leaf_id=row[1]
	forceReconfigure=not bool(row[2]) #True if leaf doesn't exists or haven't is_configured
	loop_devtype=row[3]
	loop_numberLeaves=row[4]
	
	if loop_devtype=="CuMC" and not 1<=leaf_position<=loop_numberLeaves:
		log_write_commit("config failed - wrong position",logData)
		return responseJson(400,'wrong position')
	
	
	with application.dblock:
		dbcur.execute("UPDATE leaf SET uuid=NULL, macaddress='' WHERE uuid=?",[leaf_uuid]) #clear old config
		if CONFIG_VERSION>=3:
			if leaf_id:
				dbUpdate(dbcur, "leaf", {
					'position':leaf_position,
					'uuid':leaf_uuid,
					'macaddress':leaf_macaddress,
					'lastset':int(time.time()),
					'is_configured':1
				}, {"id":leaf_id})
			else:
				dbInsert(dbcur, "leaf", {
					'loop_id':loop_id,
					'position':leaf_position,
					'uuid':leaf_uuid,
					'macaddress':leaf_macaddress,
					'lastset':int(time.time()),
					'is_configured':1
				})
				leaf_id=dbcur.lastrowid
		else:
			if leaf_id:
				dbcur.execute('UPDATE leaf SET position=?,uuid=?,lastset=?,is_configured=? WHERE id=?',[leaf_position, leaf_uuid, int(time.time()), 1, leaf_id]) #set
			else:
				dbcur.execute('INSERT INTO leaf (loop_id, position, uuid, lastset, is_configured) VALUES (?,?,?,?,?)',[loop_id, leaf_position, leaf_uuid, int(time.time()), 1]) #set
				leaf_id=dbcur.lastrowid
		dbconn.commit()
		
	#-- refresh data ----
	leafItem=_leaf_get(dbcur,leaf_id)
	if leafItem:
		logData.update({
			'node':leafItem['node'],
			'core':leafItem['core'],
			'ip':leafItem['ip'],
			'hostname':leafItem['hostname'],
			'force-reconfigure':forceReconfigure,
		})
		if CONFIG_VERSION==2:
			logData.update({
				'feeder':leafItem['feeder'],
				'cube':leafItem['cube'],
			})
		if CONFIG_VERSION>=3:
			logData.update({
				'breaker':leafItem['breaker'],
				'feeder':leafItem['feeder'],
				'row':leafItem['row'],
				'devtype':leafItem['devtype'],
			})
			with application.dblock:
				dbcur.execute('UPDATE leaf SET lastconfig_switch_name=?, lastconfig_port_west=?, lastconfig_port_east=?, lastconfig_devtype=?, lastconfig_feeder=?, lastconfig_breaker=?, lastconfig_node=?, lastconfig_core=?, lastconfig_row=?, lastconfig_hostname=?, lastconfig_ip=?, lastconfig_gateway=?   WHERE id=?', [
					leafItem['switch_name'], leafItem['switch_interface_west'], leafItem['switch_interface_east'], leafItem['devtype'], 
					leafItem['feeder'], leafItem['breaker'], leafItem['node'], leafItem['core'], leafItem['row'],
					leafItem['hostname'], leafItem['ip'], leafItem['gateway'], 
					leaf_id
				]) #set
				dbconn.commit()
		
		leafItem.update({
			'force-reconfigure':forceReconfigure,
		})
		log_write_commit("config set",logData)
		return responseJson(200,leafItem) #success
	log_write_commit("config not stored",logData)
	return responseJson(204,None) #no content

@api.route('/inventory',methods=['POST'])
@autoExcCatch
def inventory():
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	
	reqdata = flask.request.get_json(force=True, silent=False)
	breaker = intval(reqdata.get('breaker'))
	feeder = intval(reqdata.get('feeder'))
	core = intval(reqdata.get('core'))
	node = intval(reqdata.get('node'))
	devtype = reqdata.get('devtype')
	
	sqlCondition=[]
	dataCondition=[]
	
	if breaker:
		sqlCondition.append("leaf.lastconfig_breaker=?")
		dataCondition.append(breaker)
	if feeder:
		sqlCondition.append("leaf.lastconfig_feeder=?")
		dataCondition.append(feeder)
	if core:
		sqlCondition.append("leaf.lastconfig_core=?")
		dataCondition.append(core)
	if node:
		sqlCondition.append("leaf.lastconfig_node=?")
		dataCondition.append(node)
	if devtype:
		sqlCondition.append("leaf.lastconfig_devtype=?")
		dataCondition.append(devtype)
	
	query="""
		SELECT loop.id, leaf.lastconfig_switch_name, leaf.lastconfig_port_west, leaf.lastconfig_port_east, leaf.lastconfig_core, leaf.lastconfig_node,
			leaf.id, leaf.position, leaf.uuid, leaf.lastset,
			leaf.is_configured,
			leaf.lastconfig_feeder, 0,
			leaf.lastconfig_breaker, leaf.lastconfig_row, leaf.lastconfig_devtype,
			leaf.lastconfig_hostname, leaf.lastconfig_ip, leaf.lastconfig_gateway,
			leaf.macaddress
		FROM leaf INNER JOIN loop ON (loop.id=leaf.loop_id)
	"""
	if sqlCondition:
		query+=" WHERE "+ (" AND ".join(sqlCondition))+ " "
	query+=" ORDER BY leaf.lastconfig_switch_name, leaf.lastconfig_port_west, leaf.lastconfig_port_east, leaf.position, leaf.uuid "

	ret=[]
	with application.dblock:
		for row in dbcur.execute(query,dataCondition):
			ret.append({
				'id':row[6],
				'breaker':row[13],
				'feeder':row[11],
				'core':row[4],
				'node':row[5],
				'row':row[14],
				'position':row[7],
				'uuid':row[8],
				'macaddress':row[19],
				'lastset':row[9],
				'ip':row[17],
				'gateway':row[18],
				'hostname':row[16],
				'devtype':row[15],
			})
	return responseJson(200,{"devices":ret}) #success
	
@api.route('/inventory/cumc',methods=['POST'])
@autoExcCatch
def inventory_cumc():
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	
	reqdata = flask.request.get_json(force=True, silent=False)
	fromIP = reqdata.get('fromip')
	fromUUID = reqdata.get('fromuuid')
	if not fromIP and not fromUUID:
		fromIP=flask.request.remote_addr
	
	fromList = _inventory_findSource(dbcur, fromIP, fromUUID)
	
	if not fromList:
		return responseJson(204,"") #no content
	
	if len(fromList)>1:
		return responseJson(400,"The parameters match too many devices") #error
	
	fromData=fromList[0]
	result=None
	
	if fromData['devtype']=='CMC' or  fromData['devtype']=='CTRMC':
		result = _inventory(dbcur, fromData['breaker'], fromData['feeder'], fromData['core'], node=None, row=None, devtype='CuMC')
	elif fromData['devtype']=='NMC':
		result = _inventory(dbcur, fromData['breaker'], fromData['feeder'], fromData['core'], fromData['node'], row=None, devtype='CuMC')
	else:
		result = _inventory(dbcur, fromData['breaker'], fromData['feeder'], fromData['core'], fromData['node'], fromData['row'], devtype='CuMC')
	
	return responseJson(200,{"devices":result}) #success

@api.route('/inventory/ctrmc',methods=['POST'])
@autoExcCatch
def inventory_ctrmc():
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	
	reqdata = flask.request.get_json(force=True, silent=False)
	fromIP = reqdata.get('fromip')
	fromUUID = reqdata.get('fromuuid')
	if not fromIP and not fromUUID:
		fromIP=flask.request.remote_addr
	
	fromList = _inventory_findSource(dbcur, fromIP, fromUUID)
	
	if not fromList:
		return responseJson(204,"") #no content
	
	if len(fromList)>1:
		return responseJson(400,"The parameters match too many devices") #error
	
	fromData=fromList[0]
	result=None
	
	result = _inventory(dbcur, fromData['breaker'], fromData['feeder'], fromData['core'], node=None, row=None, devtype='CTRMC')
	return responseJson(200,{"devices":result}) #success

@api.route('/inventory/nmc',methods=['POST'])
@autoExcCatch
def inventory_nmc():
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	
	reqdata = flask.request.get_json(force=True, silent=False)
	fromIP = reqdata.get('fromip')
	fromUUID = reqdata.get('fromuuid')
	if not fromIP and not fromUUID:
		fromIP=flask.request.remote_addr
	
	fromList = _inventory_findSource(dbcur, fromIP, fromUUID)
	
	if not fromList:
		return responseJson(204,"") #no content
	
	if len(fromList)>1:
		return responseJson(400,"The parameters match too many devices") #error
	
	fromData=fromList[0]
	result=None
	
	result = _inventory(dbcur, fromData['breaker'], fromData['feeder'], fromData['core'], node=fromData['node'], row=fromData['row'], devtype='NMC')
	return responseJson(200,{"devices":result}) #success

#---- HTML PAGES -----
@api.route('/admin/')
def static_admin_index():
	return flask.send_from_directory('admin', 'index.html')
@api.route('/admin/<path:path>')
def static_admin(path):
	return flask.send_from_directory('admin', path)
@api.route('/file/<path:path>')
def static_file(path):
	folder=application.getFilesFolder()
	return flask.send_from_directory(folder, path)

@api.route('/file-list',methods=['GET'])
@autoExcCatch
def file_list_get():
	result={"file-list":[]}
	folder=application.getFilesFolder()
	for root,dirs,files in os.walk(folder):
		relroot=os.path.relpath(root,folder) #relative path
		if relroot=='.':
			relroot=''
		
		for filename in files:
			fullfilename = os.path.join(root,filename)
			relfilename = os.path.join(relroot,filename)
			result['file-list'].append({
				'name':relfilename,
				'executable':os.access(fullfilename,os.X_OK),
			})
		
	result['file-list'].sort(key=lambda x:x['name'].lower()) #insensitive alpha sort
	return responseJson(200,result) #success

#---- ADMIN REQUESTS ----
#--info system
@api.route('/adm-info',methods=['GET'])
@autoExcCatch
def adm_get_info():
	return responseJson(200,{
		'site_code':application.getSiteCode(),
		'config_version':CONFIG_VERSION,
		'feeder_as_letter':application.isFeederAsLetter(),
	}) #success

#--log
@api.route('/adm-log',methods=['GET'])
@autoExcCatch
def adm_get_log():
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	
	afterDate = floatval(flask.request.args.get('after'))
	
	ret=[]
	with application.dblock:
		for row in dbcur.execute('SELECT date, name, message FROM log WHERE date>? ORDER BY date',[afterDate]):
			ret.append({
				'date':row[0],
				'name':row[1],
				'message':row[2],
			})
	return responseJson(200,ret) #success

@api.route('/adm-log',methods=['DELETE'])
@autoExcCatch
def adm_del_log():
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	with application.dblock:
		dbcur.execute('DELETE FROM log')
		dbconn.commit()
		dbcur.execute('VACUUM')
	
	return responseJson(200,None) #success

#--loop
@api.route('/adm-loop-create',methods=['POST'])
@autoExcCatch
def adm_loop_create():
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	
	success,reqdata = _readLoopDataFromRequest(flask.request.get_json(force=True, silent=False))
	if not success:
		log_write_commit("create loop failed - wrong parameters",reqdata)
		return responseJson(400,"wrong parameters")
	
	#-- check unique ----
	row=None
	with application.dblock:
		dbcur.execute('SELECT id FROM loop WHERE switch_name=? AND switch_interface_west=? AND switch_interface_east=?',[
			reqdata['switch_name'],
			reqdata['switch_interface_west'],
			reqdata['switch_interface_east'],
		])
		row=dbcur.fetchone()
	if row:
		log_write_commit("create loop failed - loop already exists",reqdata)
		return responseJson(500,"Loop already exists")
	
	#-- apply
	dataForm={
		'switch_name':reqdata['switch_name'], 
		'switch_interface_west':reqdata['switch_interface_west'],
		'switch_interface_east':reqdata['switch_interface_east'],
		'core':reqdata['loop_core'],
		'node':reqdata['loop_node'],
		'number_leaves':reqdata['number_leaves'],
	}
	if CONFIG_VERSION==2:
		dataForm.update({
			'feeder':reqdata['loop_feeder'],
			'cube':reqdata['loop_cube'],
		})
	if CONFIG_VERSION>=3:
		dataForm.update({
			'breaker':reqdata['loop_breaker'],
			'feeder':reqdata['loop_feeder'],
			'row':reqdata['loop_row'],
			'devtype':reqdata['loop_devtype'],
		})
	with application.dblock:
		dbInsert(dbcur,'loop',dataForm)
		dbconn.commit()
		loop_id=dbcur.lastrowid
	if loop_id:
		log_write_commit("create loop",reqdata)
	else:
		log_write_commit("create loop failed - cannot create the loop",reqdata)
		return responseJson(500,"Cannot create the loop")
	
	#-- refresh data ----
	loopItem=_loop_get(dbcur,loop_id)
	if loopItem:
		return responseJson(200,loopItem) #success
	return responseJson(204,None) #no content

@api.route('/loop/<int:loop_id>',methods=['GET'])
@autoExcCatch
def loop_get(loop_id):
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	loopItem=_loop_get(dbcur,loop_id)
	if loopItem:
		return responseJson(200,loopItem) #success
	return responseJson(204,None) #no content

@api.route('/adm-loop/<int:loop_id>',methods=['GET','DELETE','PUT'])
@autoExcCatch
def adm_loop(loop_id):
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	
	#-- read ----
	if flask.request.method == 'GET':
		loopItem=_loop_get(dbcur,loop_id)
		if loopItem:
			return responseJson(200,loopItem) #success
		return responseJson(204,None) #no content
	
	#-- delete ----
	if flask.request.method == 'DELETE':
		loopItem=_loop_get(dbcur,loop_id)
		with application.dblock:
			if not loopItem:
				log_write(dbcur,'delete loop failed - not found',{'loop_id':loop_id})
			else:
				dbcur.execute('DELETE FROM loop WHERE id=?',[loop_id])
				dbcur.execute('DELETE FROM leaf WHERE loop_id=?',[loop_id])
				log_write(dbcur,'delete loop',loopItem)
			dbconn.commit()
		return responseJson(200,None) #success
	
	#-- update ----
	if flask.request.method == 'PUT':
		success,reqdata = _readLoopDataFromRequest(flask.request.get_json(force=True, silent=False))
		if not success:
			log_write_commit("update loop failed - wrong parameters",reqdata)
			return responseJson(400,"wrong parameters")
			
		#-- check unique
		row=None
		with application.dblock:
			dbcur.execute('SELECT id FROM loop WHERE switch_name=? AND switch_interface_west=? AND switch_interface_east=? AND id!=?',[
				reqdata['switch_name'],
				reqdata['switch_interface_west'],
				reqdata['switch_interface_east'],
				loop_id,
			])
			row=dbcur.fetchone()
		if row:
			log_write_commit("update loop failed - loop already exists",reqdata)
			return responseJson(500,"Loop already exists")
		
		#-- get old value for logging
		logData=dict(reqdata)
		loopItem=_loop_get(dbcur,loop_id)
		if not loopItem:
			log_write_commit("update loop failed - loop not found",logData)
			return responseJson(204,None) #no content
		
		logData['old_switch_name']=loopItem['switch_name']
		logData['old_switch_interface_west']=loopItem['switch_interface_west']
		logData['old_switch_interface_east']=loopItem['switch_interface_east']
		logData['old_loop_core']=loopItem['core']
		logData['old_loop_node']=loopItem['node']
		logData['old_number_leaves']=loopItem['number_leaves']
		if CONFIG_VERSION==2:
			logData['old_loop_feeder']=loopItem['feeder']
			logData['old_loop_cube']=loopItem['cube']
		if CONFIG_VERSION>=3:
			logData['old_loop_devtype']=loopItem['devtype']
			logData['old_loop_breaker']=loopItem['breaker']
			logData['old_loop_feeder']=loopItem['feeder']
			logData['old_loop_row']=loopItem['row']
		
		#-- apply
		dataForm={
			'switch_name':reqdata['switch_name'], 
			'switch_interface_west':reqdata['switch_interface_west'],
			'switch_interface_east':reqdata['switch_interface_east'],
			'core':reqdata['loop_core'],
			'node':reqdata['loop_node'],
			'number_leaves':reqdata['number_leaves'],
		}
		if CONFIG_VERSION==2:
			dataForm.update({
				'feeder':reqdata['loop_feeder'],
				'cube':reqdata['loop_cube'],
			})
		if CONFIG_VERSION>=3:
			dataForm.update({
				'breaker':reqdata['loop_breaker'],
				'feeder':reqdata['loop_feeder'],
				'row':reqdata['loop_row'],
				'devtype':reqdata['loop_devtype'],
			})
		with application.dblock:
			dbUpdate(dbcur,'loop',dataForm, {'id':loop_id})
			log_write(dbcur,"update loop",logData)
			dbconn.commit()
		
		#-- refresh data
		loopItem=_loop_get(dbcur,loop_id)
		if loopItem:
			return responseJson(200,loopItem) #success
		return responseJson(204,None) #no content
	
	#-- unknown method ----
	return responseJson(400,"Wrong command")

@api.route('/adm-loop-list',methods=['GET'])
@autoExcCatch
def adm_looplist():
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	
	ret=[]
	with application.dblock:
		for row in dbcur.execute('SELECT id, switch_name, switch_interface_west, switch_interface_east, core, node, number_leaves, feeder, cube, breaker, row, devtype FROM loop ORDER BY switch_name, switch_interface_west, switch_interface_east'):
			ret.append({
				'id':row[0],
				'switch_name':row[1],
				'switch_interface_west':row[2],
				'switch_interface_east':row[3],
				'core':row[4],
				'node':row[5],
				'leaf_count':row[6], #deprecated
				'number_leaves':row[6],
			})
			if CONFIG_VERSION==2:
				ret[-1].update({
					'feeder':row[7],
					'cube':row[8],
				})
			if CONFIG_VERSION>=3:
				ret[-1].update({
					'breaker':row[9],
					'feeder':row[7],
					'row':row[10],
					'devtype':row[11],
				})
	return responseJson(200,ret) #success

@api.route('/adm-loop-forcereconfigure',methods=['POST'])
@autoExcCatch
def adm_loop_forcereconfigure():
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	
	reqdata = flask.request.get_json(force=True, silent=False)
	loopid = intval(reqdata.get('id'))
	value = bool(reqdata.get('value'))
	
	with application.dblock:
		dbcur.execute('UPDATE leaf SET is_configured=? WHERE loop_id=?',[int(not value),loopid])
		dbconn.commit()
	
	return responseJson(200,{'newvalue':value}) #success

#--leaf
@api.route('/adm-leaf/<int:leaf_id>',methods=['GET'])
@autoExcCatch
def adm_leaf(leaf_id):
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	
	#-- read ----
	if CONFIG_VERSION>=3:
		leafItem=_leaf_getLastConfig(dbcur,leaf_id)
	else:
		leafItem=_leaf_get(dbcur,leaf_id)
	if leafItem:
		return responseJson(200,leafItem) #success
	return responseJson(204,None) #no content

@api.route('/adm-leaf-list',methods=['GET'])
@autoExcCatch
def adm_leaflist():
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	
	if CONFIG_VERSION>=3:
		query="""
			SELECT loop.id, leaf.lastconfig_switch_name, leaf.lastconfig_port_west, leaf.lastconfig_port_east, leaf.lastconfig_core, leaf.lastconfig_node,
				leaf.id, leaf.position, leaf.uuid, leaf.lastset,
				leaf.is_configured,
				leaf.lastconfig_feeder, 0,
				leaf.lastconfig_breaker, leaf.lastconfig_row, leaf.lastconfig_devtype,
				leaf.lastconfig_hostname, leaf.lastconfig_ip, leaf.lastconfig_gateway,
				leaf.macaddress
			FROM leaf INNER JOIN loop ON (loop.id=leaf.loop_id)
			ORDER BY leaf.lastconfig_switch_name, leaf.lastconfig_port_west, leaf.lastconfig_port_east, leaf.position, leaf.uuid
		"""
		ret=[]
		with application.dblock:
			for row in dbcur.execute(query):
				ret.append({
					'loop_id':row[0],
					'switch_name':row[1],
					'switch_interface_west':row[2],
					'switch_interface_east':row[3],
					'core':row[4],
					'node':row[5],
					'id':row[6],
					'position':row[7],
					'uuid':row[8],
					'macaddress':row[19],
					'lastset':row[9],
					'ip':row[17],
					'gateway':row[18],
					'hostname':row[16],
					'force-reconfigure':not bool(row[10]),
					'breaker':row[13],
					'feeder':row[11],
					'row':row[14],
					'devtype':row[15],
				})
	else:
		query="""
			SELECT loop.id, loop.switch_name, loop.switch_interface_west, loop.switch_interface_east, loop.core, loop.node,
				leaf.id, leaf.position, leaf.uuid, leaf.lastset,
				leaf.is_configured,
				loop.feeder, loop.cube,
				loop.breaker, loop.row, loop.devtype
			FROM leaf INNER JOIN loop ON (loop.id=leaf.loop_id)
			ORDER BY loop.switch_name, loop.switch_interface_west, loop.switch_interface_east, leaf.position, leaf.uuid
		"""
		ret=[]
		with application.dblock:
			dbcur.execute(query)
			rows=dbcur.fetchall()
		for row in rows:
			ret.append({
				'loop_id':row[0],
				'switch_name':row[1],
				'switch_interface_west':row[2],
				'switch_interface_east':row[3],
				'core':row[4],
				'node':row[5],
				'id':row[6],
				'position':row[7],
				'uuid':row[8],
				'lastset':row[9],
				'ip':application.computeIp(row[4],row[5],row[7],row[11],row[12],row[13],row[14],row[15]),
				'gateway':application.computeGateway(row[4],row[5],row[7],row[11],row[12],row[13],row[14],row[15]),
				'hostname':application.computeHostname(row[4],row[5],row[7],row[11],row[12],row[13],row[14],row[15]),
				'force-reconfigure':not bool(row[10]),
			})
			if CONFIG_VERSION==2:
				ret[-1].update({
					'feeder':row[11],
					'cube':row[12],
				})
			if CONFIG_VERSION>=3:
				ret[-1].update({
					'breaker':row[13],
					'feeder':row[11],
					'row':row[14],
					'devtype':row[15],
				})
	return responseJson(200,ret) #success

@api.route('/adm-leaf-forcereconfigure',methods=['POST'])
@autoExcCatch
def adm_leaf_forcereconfigure():
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	
	reqdata = flask.request.get_json(force=True, silent=False)
	leafid = intval(reqdata.get('id'))
	value = bool(reqdata.get('value'))
	
	with application.dblock:
		dbcur.execute('UPDATE leaf SET is_configured=? WHERE id=?',[int(not value),leafid])
		dbconn.commit()
	
	return responseJson(200,{'newvalue':value}) #success


#==== UPDATE DATABASE STRUCTURE ====
def _listColumns(dbcur,tablename):
	query = 'pragma table_info('+tablename+')'
	return [row[1] for row in dbcur.execute(query)]
def updateDatabaseStructure_v2():
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	columns = _listColumns(dbcur,'loop')
	hasFail=False
	hasChanged=0
	if not 'feeder' in columns:
		try:
			dbcur.execute('ALTER TABLE loop ADD COLUMN feeder INTEGER NOT NULL DEFAULT 0')
			hasChanged+=1
		except Exception as e:
			print("[Update DB v2] loop.feeder: <%s> %s"%(type(e).__name__,str(e)),file=sys.stderr)
			hasFail=True
	if not 'cube' in columns:
		try:
			dbcur.execute('ALTER TABLE loop ADD COLUMN cube INTEGER NOT NULL DEFAULT 0')
			hasChanged+=1
		except Exception as e:
			print("[Update DB v2] loop.cube: <%s> %s"%(type(e).__name__,str(e)),file=sys.stderr)
			hasFail=True
	if hasFail:
		sys.exit(-1)
	return hasChanged
def updateDatabaseStructure_v3():
	dbconn=application.getDB()
	dbcur=dbconn.cursor()
	hasFail=False
	hasChanged=0
	
	columns = _listColumns(dbcur,'loop')
	if not 'breaker' in columns:
		try:
			dbcur.execute('ALTER TABLE loop ADD COLUMN breaker INTEGER NOT NULL DEFAULT 0')
			hasChanged+=1
		except Exception as e:
			print("[Update DB v3] loop.breaker: <%s> %s"%(type(e).__name__,str(e)),file=sys.stderr)
			hasFail=True
	if not 'row' in columns:
		try:
			dbcur.execute('ALTER TABLE loop ADD COLUMN row INTEGER NOT NULL DEFAULT 0')
			hasChanged+=1
		except Exception as e:
			print("[Update DB v3] loop.row: <%s> %s"%(type(e).__name__,str(e)),file=sys.stderr)
			hasFail=True
	if not 'devtype' in columns:
		try:
			dbcur.execute('ALTER TABLE loop ADD COLUMN devtype VARCHAR(4) NOT NULL DEFAULT \'cube\'')
			hasChanged+=1
		except Exception as e:
			print("[Update DB v3] loop.devtype: <%s> %s"%(type(e).__name__,str(e)),file=sys.stderr)
			hasFail=True
	
	columns = _listColumns(dbcur,'leaf')
	for textField in ['lastconfig_switch_name','lastconfig_port_west','lastconfig_port_east','lastconfig_hostname','lastconfig_ip','lastconfig_gateway','macaddress']:
		if textField in columns:
			continue
		try:
			dbcur.execute("ALTER TABLE leaf ADD COLUMN %s TEXT NOT NULL DEFAULT ''"%textField)
			hasChanged+=1
		except Exception as e:
			print("[Update DB v3] leaf.%s: <%s> %s"%(textField, type(e).__name__, str(e)), file=sys.stderr)
			hasFail=True
	for intField in ['lastconfig_feeder','lastconfig_breaker','lastconfig_core','lastconfig_node','lastconfig_row']:
		if intField in columns:
			continue
		try:
			dbcur.execute("ALTER TABLE leaf ADD COLUMN %s INTEGER NOT NULL DEFAULT 0"%intField)
			hasChanged+=1
		except Exception as e:
			print("[Update DB v3] leaf.%s: <%s> %s"%(intField, type(e).__name__, str(e)), file=sys.stderr)
			hasFail=True
	if not 'lastconfig_devtype' in columns:
		try:
			dbcur.execute('ALTER TABLE leaf ADD COLUMN lastconfig_devtype VARCHAR(4) NOT NULL DEFAULT \'\'')
			hasChanged+=1
		except Exception as e:
			print("[Update DB v3] leaf.lastconfig_devtype: <%s> %s"%(type(e).__name__,str(e)),file=sys.stderr)
			hasFail=True
	
	if hasFail:
		sys.exit(-1)
	
	dbcur.execute("UPDATE loop SET devtype='CuMC' WHERE devtype='cube'") #DEBUG just for migration
	dbcur.execute("UPDATE loop SET devtype='CMC' WHERE devtype='cc'") #DEBUG just for migration
	dbcur.execute("UPDATE loop SET devtype='NMC' WHERE devtype='nc'") #DEBUG just for migration
	dbconn.commit()
	
	return hasChanged

updateDatabaseStructure_v2()
updateDatabaseStructure_v3()

#==== Template ====
if SCRIPT_ARGS.template:
	print('applying template : load "'+SCRIPT_ARGS.template+'"')
	if application.resetWithTemplateFile(SCRIPT_ARGS.template):
		log_write_commit('Database cleared, template loaded : '+SCRIPT_ARGS.template,None)
		print('applying template : success')
	else:
		sys.exit(-1)


#==== LAUNCH ====
try:
	port=config.getint('DEFAULT','port',fallback=5000)
except Exception as e:
	port=5000

log_write_commit('server start',None)
api.run(host='0.0.0.0', threaded=True, port=port, debug=False) #WARNING: This is a development server. Do not use it in a production deployment. Use a production WSGI server instead.
log_write_commit('server stop',None)
