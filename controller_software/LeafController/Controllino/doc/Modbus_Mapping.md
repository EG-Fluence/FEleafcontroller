# Modbus

## Modbus object types

| Object type       | Access     | Size    |
| ----------------- | ---------- | ------- |
| Coil              | Read-write | 1 bit   |
| Discrete input    | Read       | 1 bit   |
| Input register    | Read       | 16 bits |
| Holding registers | Read-write | 16 bits |

 Normally coils a are used to write digital values to an outputs.
 Discrete inputs are used to read digital inputs.
 Registers are use to communicate data between the devices and also usually used for analog I/Os.



## Mapping

| Modbus                           | Command  | Controllino function |
| -------------------------------- | -------- | -------------------- |
| Read Coils                       | 0x01     | read Relays          |
| Read Discrete Inputs             | 0x02     | not used             |
| Read Holding Registers           | 0x03     | read Digital Outputs |
| Read Input Registers             | 0x04     |	read Digital Inputs  |
| Write Single Coil	               | 0x05     | write Relay          |
| Write Single Holding Register   	| 0x06     | write DO             |
| Write Multiple Coils             | 0x0f     | write Relays         |
| Write Multiple Holding Resisters | 0x10     | write DOs            |

