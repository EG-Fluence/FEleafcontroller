## Self Configure Concept

1. Device turns on.

2. If ```/etc/self.conf``` does not exist:
    - Assign machine UUID.
    - Assign random hostname. Save to ```self.conf```
    - Assign random (legal) mac address. Save to ```self.conf```
    - Update .dtb MAC address in kernel (optional)
    - ```/etc/netplan/network.yaml``` is default:
        * no ip address
        * no bridge, or bridge down

3. Discover wan, lan3
    - If Cisco switch is connected
        * Get configuration temporary DHCP address.
        * Retrieve num_leaves
        * Treat Cisco switch as leaf=num_leaves+1 if on lan3 port. If only the Cisco switch is seen on lan3 then current leaf is highest leaf.
    - wan:
        * Treat Cisco switch as leaf 0.
        * If BananaPi or Cisco exists with valid hostname, MAX_LEAF=max(leaf_numbers) and leaf_num=MAX_LEAF+1. Set tentative_hostname based on Cisco hostname.
        * Save Cisco Port ID. (cisco_port_id)
    - lan3:
        * If num_leaves is unknown, exit.
        * If BananaPi or Cisco exists with valid hostname, MIN_LEAF=min(leaf_numbers) and leaf_num=MIN_LEAF-1. Set tentative_hostname based on Cisco hostname.
        * Save Cisco Port ID. (cisco_port_id)
    - else:
        * wait 2 seconds, check again.

4. Partial configure
    - Summary: values saved are leaf_num, cisco_port_id, and tentative_hostname
    - Restart lldpd.
    - Attach wan and lan3 to bridge.
    - Get temporary DHCP address. (br0)

5. Full configure
    - The leaf_num, cisco_port_id, and tentative_hostname uniquely identify the device.
        * There may be cases where multiple leaves are connected directly to the same Cisco switch so they will all be leaf_num=1.
    - Fetch config from configuration server, providing to config server:
        * leaf_num
        * cisco_port_id
    - Complete configuration
        * Confirm hostname from server.
        * Perform any needed system updates.
        * Load image for Controllino if needed.
        * Load modbus maps for Controllino.
        * Start modbus mirror.
        * Download DCS. Configure DCS. Start DCS.
        * Configure and load AWS Greengrass (?)
        * Start/configure DAS if needed.
