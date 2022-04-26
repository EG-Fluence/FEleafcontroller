#-*- coding:utf8 -*-

import sys
import requests
import json

def printColor(c,*args):
	print(*args)
def arrayGet(array,index,defval=None):
	try:
		return array[index]
	except:
		return defval

class RestClient:
	def __init__(self,serverURL, secure=False, timeout=3):
		self.session=requests.Session()
		self.serverURL=serverURL
		self.timeout=timeout
	
	def GETraw(self, page,params=None,tag=''):
		#printColor(10,'GET',self.serverURL+page,tag)
		
		r = self.session.get(self.serverURL+page ,params=params, verify=False, timeout=self.timeout)
		
		if r.status_code!=200: #bad request
			printColor(12,'!!! status=%s'%repr(r.status_code))
			printColor(12,r.text)
			#raise Exception('status = %s'%repr(r.status_code))
			return None
		return r.content
	
	def GET(self, page,params=None,tag=''):
		#printColor(10,'GET',self.serverURL+page,tag)
		
		r = self.session.get(self.serverURL+page ,params=params, verify=False, timeout=self.timeout)
		
		if r.status_code!=200: #bad request
			printColor(12,'!!! status=%s'%repr(r.status_code))
			printColor(12,r.text)
			#raise Exception('status = %s'%repr(r.status_code))
			return {}
		ret=r.json()
		return ret
	
	def POST(self, page, params=None, tag=''):
		#printColor(10,'POST',self.serverURL+page,tag)
		r = self.session.post(self.serverURL+page ,data=params, verify=False, timeout=self.timeout)
		
		if r.status_code!=200: #bad request
			printColor(12,'!!! status=%s'%repr(r.status_code))
			printColor(12,r.text)
			#raise Exception('status = %s'%repr(r.status_code))
			return {}
		ret=r.json()
		return ret
	def POSTjson(self, page, params=None, tag=''):
		#printColor(10,'POST',self.serverURL+page,tag)
		r = self.session.post(self.serverURL+page ,json=params, verify=False, timeout=self.timeout)
		
		if r.status_code!=200: #bad request
			printColor(12,'!!! status=%s'%repr(r.status_code))
			printColor(12,r.text)
			#raise Exception('status = %s'%repr(r.status_code))
			return {}
		ret=r.json()
		return ret
