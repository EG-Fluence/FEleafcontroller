
1. In the ArrayController create the directory ~/controllino_update


2. Using WinSCP copy the following files to the directory ~/controllino_update
	Controllino.hex
	controllino_update-remote.sh
	controllino_update.sh


3. On the ArrayController terminal execute: "cd ~/controllino_update"


4. On the ArrayController terminal execute: "chmod 755 *"


5. On the ArrayController terminal run the script controllino_update.sh <IP address> <root password>

	example: ./controllino_update.sh 10.11.9.101 root


6. Inspect the output for errors


