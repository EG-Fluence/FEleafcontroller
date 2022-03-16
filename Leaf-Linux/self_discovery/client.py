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
	def __init__(self,serverURL, secure=False):
		self.session=requests.Session()
		self.serverURL=serverURL
	
	def GET(self, page,params=None,tag=''):
		printColor(10,'GET',self.serverURL+page,tag)
		
		r = self.session.get(self.serverURL+page ,params=params, verify=False)
		
		if r.status_code!=200: #bad request
			printColor(12,'!!! status=%s'%repr(r.status_code))
			printColor(12,r.text)
			#raise Exception('status = %s'%repr(r.status_code))
			return {}
		ret=r.json()
		printColor(9,ret)
		return ret
	
	def POST(self, page, params=None, tag=''):
		printColor(10,'POST',self.serverURL+page,tag)
		r = self.session.post(self.serverURL+page ,data=params, verify=False)
		
		if r.status_code!=200: #bad request
			printColor(12,'!!! status=%s'%repr(r.status_code))
			printColor(12,r.text)
			#raise Exception('status = %s'%repr(r.status_code))
			return {}
		ret=r.json()
		printColor(9,ret)
		return ret
	def POSTjson(self, page, params=None, tag=''):
		printColor(10,'POST',self.serverURL+page,tag)
		r = self.session.post(self.serverURL+page ,json=params, verify=False)
		
		if r.status_code!=200: #bad request
			printColor(12,'!!! status=%s'%repr(r.status_code))
			printColor(12,r.text)
			#raise Exception('status = %s'%repr(r.status_code))
			return {}
		ret=r.json()
		printColor(9,ret)
		return ret


argc=len(sys.argv)

data={
	'leaf_id':int(arrayGet(sys.argv,1,1)),
	'controller_id':arrayGet(sys.argv,2,'<uuid>'),
	'switch_id':arrayGet(sys.argv,3,'enstXXcYY…'),
	'switch_port':arrayGet(sys.argv,4,'Gi6'),
}

rc=RestClient('http://127.0.0.1:5000')
rc.POSTjson('/config',data)
