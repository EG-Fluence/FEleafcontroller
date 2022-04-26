#/bin/bash

# prepare environment
python3 -m pip install flask
python3 -m pip install requests

export TRANSVERFOLDER=controller_software
adduser --disabled-password --gecos "" username

# TODO: Copy files in a temporary folder and set a variable with the path
# TODO: check variable/path ${TRANSVERFOLDER}


# copy program
mkdir -pv /usr/local/bin/fluence_selfconfiguration-server
cp -rp ~/${TRANSVERFOLDER}/LeafController/SelfConfiguration/admin /usr/local/bin/fluence_selfconfiguration-server/admin
cp -p ~/${TRANSVERFOLDER}/LeafController/SelfConfiguration/server.py /usr/local/bin/fluence_selfconfiguration-server/
cp -p ~/${TRANSVERFOLDER}/LeafController/SelfConfiguration/*.md /usr/local/bin/fluence_selfconfiguration-server/

# copy working files
mkdir -p /home/fos/SelfConfiguration/server
cp -rp ~/${TRANSVERFOLDER}/LeafController/SelfConfiguration/file /home/fos/SelfConfiguration/server/file
cp -p ~/${TRANSVERFOLDER}/LeafController/SelfConfiguration/config.ini /home/fos/SelfConfiguration/server/
cp -p ~/${TRANSVERFOLDER}/LeafController/SelfConfiguration/database.sqlite3 /home/fos/SelfConfiguration/server/

# copy launcher
cp -pr ~/${TRANSVERFOLDER}/ArrayController/root/usr/local/bin/fluence_selfconfiguration-server.sh /usr/local/bin/fluence_selfconfiguration-server.sh


# activate service
cp -pr ~/${TRANSVERFOLDER}/ArrayController/root/etc/systemd/system/fluence-selfconfiguration.service /etc/systemd/system/fluence-selfconfiguration.service
systemctl enable fluence-selfconfiguration.service
systemctl start fluence-selfconfiguration.service
