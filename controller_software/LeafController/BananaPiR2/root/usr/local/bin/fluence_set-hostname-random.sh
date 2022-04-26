#!/bin/bash

HOSTNAME="leaf-$(cat /etc/machine-id | head -c 10)"
hostnamectl set-hostname $HOSTNAME
cat > /etc/hosts <<EOF
127.0.0.1    localhost localhost.localdomain
127.0.1.1    $HOSTNAME
EOF

