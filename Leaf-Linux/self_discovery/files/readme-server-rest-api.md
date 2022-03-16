# REST API of Configuration Server

## Network configuration

### POST /leafcount
Ask for the number of cubes expected in a loop. The request parameters are sent in a JSON object:
- **switch_name**: *(str)* name of switch where the loop is plugged
- **switch_interface_west**: *(str)* name of switch's port where the loop is plugged, west side.
- **switch_interface_east**: *(str)* name of switch's port where the loop is plugged, east side.

"switch_name" is required. At least one port interface is required *(west or/and east)*.

response 204 if there is not loop with this parameters.  
response 400 if some parameters are wrong.  

response 200 if success. Return a JSON object:  
 - **leaf_count** *deprecated*
 - **number_leaves**: *(int)* number of cubes

### GET /leafcount
identical to **POST /leadcount** except parameters are sent in URL instead in a JSON object.

### POST /config
Request a configuration according the position in network. Parameters are sent in a JSON object:
- **switch_name**: *(str)* name of switch where the loop is plugged.
- **switch_interface_west**: *(str)* name of switch's port where the loop is plugged, west side.
- **switch_interface_east**: *(str)* name of switch's port where the loop is plugged, east side.
- **leaf_uuid**: *(str)* unique id of the device.
- **leaf_position**: *(int, only for cube)* position in loop starting from west interface.
- **macaddress**: *(str)* mac address of the device.

"switch_name" and "leaf_uuid" are required. At least one port interface is required *(west or/and east)*. Position is required only for cubes *(not for controllers)*.

response 204 if there is not configuration

response 400 if some parameters are wrong

response 200 if success. Return a JSON with an object with keys:  
- **id**: *(int)* id of device in database.  
- **loop_id**: *(int)* id of loop in database.  
- **switch_name**: *(str)*  
- **switch_interface_west**: *(str)*  
- **switch_interface_east**: *(str)*  
- **breaker**: *(int)*  
- **feeder**: *(int)*  
- **core**: *(int)*  
- **node**: *(int)*  
- **row**: *(int)*  
- **position**: *(int)*  
- **ip**: *(str)*  
- **gateway**: *(str)*  
- **hostname**: *(str)*  
- **uuid**: *(str)*  
- **macaddress**: *(str)*  
- **devtype**: *(str)* type of device *(see GET /loop/<loopid\> for detail)*.  
- **force-reconfigure**: *(bool)*  
- **lastset**: *(int)* timestamp of the last configuration requests  

Example:
```json
{
	"id" : 24,
	"loop_id" : 5,
	"switch_name": "enst00pr00"
	"switch_interface_west": "p1", 
	"switch_interface_east" : "p2",
	"breaker" : 1,
	"feeder" : 2,
	"core" : 2,
	"node" : 1,
	"row" : 1,
	"position" : 5,
	"ip" : "10.12.17.103/21",
	"gateway" : "10.12.16.1",
	"hostname" : "enst00f012c02n01r01l03.fluenceenergy.com",
	"uuid" : "f55ae4cf787",
	"macaddress" : "01:02:03:04:05:06",
	"devtype" : "CuMC",
	"force-reconfigure" : false,
	"lastset" : 1625409320,
}
```

### GET /config
identical to **POST /config** except parameters are sent in URL instead in a JSON object.

### GET /loop/*<loop_id\>*
Return all information about a loop in JSON format:
- **id**: *(int)*
- **switch_name**: *(str)*
- **switch_interface_west**: *(str)*
- **switch_interface_east**: *(str)*
- **breaker**: *(int)*
- **feeder**: *(int)*
- **core**: *(int)*
- **node**: *(int)*
- **row**: *(int)*
- **leaf_count**: *(int)* *deprecated*
- **number_leaves**: *(int)* define number of cube in this loop, leave zero in another type of device
- **devtype**: *(str)* values are 
	- CMC = *Core Controller*
	- NMC = *Node Controller*
	- CuMC = *Cube Controller*
	- CTRMC = *Core Telco Rack Controller*


### POST /inventory
Return a list of all devices with a filter given in parameter. Parameters are in a JSON object:
- **breaker**: *(int)* optional
- **feeder**: *(int)* optional
- **core**: *(int)* optional
- **node**: *(int)* optional
- **devtype**: *(str)* optional

response 200 and return a JSON object with only the key **devices**, containing a list of declared devices. Each device is represented by a JSON object, as below:
- **id**: *(int)* 
- **breaker**: *(int)* 
- **feeder**: *(int)* 
- **core**: *(int)* 
- **node**: *(int)* 
- **row**: *(int)* 
- **position**: *(int)* 
- **uuid**: *(str)* 
- **macaddress**: *(str)* 
- **lastset**: *(int)*  timestamp of the last configuration requests  
- **ip**: *(str)* 
- **hostname**: *(str)* 
- **gateway**: *(str)* 
- **devtype**: *(str)* type of device in the loop *(see GET /loop/<loopid\> for detail)*.

### POST /inventory/cumc

lists all cubes of the same branch as the source. Parameters are in a JSON object:
- **fromip**: *(int)* IP address of the source *(optional)*
- **fromuuid**: *(int)* UUID of the source *(optional)*

Without parameter, the remote IP address *(which does the requests)*  is used as source.

response 204 if IP or UUID matches with no one device

response 400 if IP or UUID matches with several devices

response 200 if success and return the same data than `GET /inventory`

### POST /inventory/ctrmc

lists the CTR responsible for the branch of the source. Parameters are in a JSON object:
- **fromip**: *(int)* IP address of the source *(optional)*
- **fromuuid**: *(int)* UUID of the source *(optional)*

Without parameter, the remote IP address *(which does the requests)*  is used as source.

response 204 if IP or UUID matches with no one device

response 400 if IP or UUID matches with several devices

response 200 if success and return the same data than `GET /inventory`

### POST /inventory/nmc

lists all NMC below or all the NMC responsible for the branch of the source. Parameters are in a JSON object:
- **fromip**: *(int)* IP address of the source *(optional)*
- **fromuuid**: *(int)* UUID of the source *(optional)*

Without parameter, the remote IP address *(which does the requests)*  is used as source.

response 204 if IP or UUID matches with no one device

response 400 if IP or UUID matches with several devices

response 200 if success and return the same data than `GET /inventory`


## Shared files

### GET /file/*<path\>*
Return the content of a file from "file" folder".  

### GET /file-list
Return the list of downloadable files *(from "file" folder)*, in an JSON object.  
The key **file-list** containing a list of files.  
Each file is represented by a object contening:
- **name**: *(str)* filename with relative path
- **executable**: *(bool)* executable permission of the file

Example:
```javascript
{
	"file-list":[
		{
			"name" : "run.sh",
			"executable" : true,
		},
		{
			"name" : "param.cfg",
			"executable" : false,
		},
		{
			"name" : "documentation.html",
			"executable" : false,
		}
	]
}
```

## Web Interface for managing

### GET /admin
Display web page for manager. Use a web browser to read.  
Return a HTML page.

### GET /admin/*<path\>*
Get extra files for web page


## Sub-routine for web interface 

### GET /adm-info
Return the global information of the server, in a JSON object:  
- **site_code**: *(str)* code name of the server
- **config_version**: *(int)* version of the server

### GET /adm-log
Return list on log message. Parameter sent in a JSON object:  
- **after**: *(timestamp in float)*. Minimum date of logs to be returned

The response is a JSON containing a list of object :
- **date**: *(timestamp in int)*
- **name**: *(str)* 
- **message**: *(str)*

### DELETE /adm-log
Request to delete all logs.

### POST /adm-loop-create
Create a loop for device configuration. Parameters are in a JSON object:
- **switch_name**: *(str)* name of switch where the loop is plugged
- **switch_interface_west**: *(str)* name of switch's port where the loop is plugged, west side.
- **switch_interface_east**: *(str)* name of switch's port where the loop is plugged, east side.
- **loop_breaker**: *(int)* loop property
- **loop_feeder**: *(int)* loop property
- **loop_core**: *(int)* loop property
- **loop_node**: *(int)* loop property
- **loop_row**: *(int)* loop property
- **number_leaves**: *(int)* number of cube in this loop or zero for another type of device
- **loop_devtype**: *(str)* type of device in the loop *(see GET /loop/<loopid\> for detail)*.


response 400 if some parameters are wrong  
response 500 if loop already exists or if error  

response 200 if success. Return the same data than *GET /loop/<loop_id\>* with the new loop.

### GET /adm-loop/*<loop_id\>*
alias of *GET /loop/<loop_id\>*

### DELETE /adm-loop/*<loop_id\>*
Request to delete a loop.

### PUT /adm-loop/*<loop_id\>*
Update a loop. The parameters are the same as for the creation *POST /adm-loop-create*.

response 400 if some parameters are wrong  
response 500 if a loop with same properties already exists or if error  

response 200 if success. Return the same data than *GET /loop/<loop_id\>* with the new loop.

### GET /adm-loop-list
Return the list of loop in JSON format. The have the same structure as *GET /loop/<loop_id\>*.

### POST /adm-loop-forcereconfigure
Force the "reconfiguration" flag for each device in the loop. Parameters are in a JSON object:
- **id**: *(int)* id of the loop
- **value**: *(bool)* value to force in flag

response 200 with a JSON object with only key:
- **newvalue**: *(bool)* with the stored value 

### GET /adm-leaf/*<leaf_id\>*
Return properties of a device.

response 204 if there is no device with this id   
response 400 if some parameters are wrong  

response 200 and return information about a device a JSON object:
- **id**: *(int)* 
- **loop_id**: *(int)* 
- **switch_name**: *(str)* name of switch where the loop is plugged
- **switch_interface_west**: *(str)* name of switch's port where the loop is plugged, west side.
- **switch_interface_east**: *(str)* name of switch's port where the loop is plugged, east side.
- **breaker**: *(int)* 
- **feeder**: *(int)* 
- **core**: *(int)* 
- **node**: *(int)* 
- **row**: *(int)* 
- **position**: *(int)* 
- **ip**: *(str)* 
- **gateway**: *(str)* 
- **hostname**: *(str)* 
- **uuid**: *(str)* 
- **macaddress**: *(str)* 
- **devtype**: *(str)* type of device in the loop *(see GET /loop/<loopid\> for detail)*.
- **lastset**: *(int)*  timestamp of the last configuration requests  
- **force-reconfigure**: *(bool)* force reconfiguration flag

### GET /adm-leaf-list
Return the list of known device in JSON format. The have the same structure as *GET /leaf/<leaf_id>*.  
A device must have requested a configuration *(and given its UUID)* to be known.

### POST /adm-leaf-forcereconfigure
Force the "reconfiguration" flag for a device. Parameters are in a JSON object:
- **id**: *(int)* id of the loop
- **value**: *(bool)* value to force in flag

response 200 with a JSON object with only key:
- **newvalue**: *(bool)* with the stored value 

