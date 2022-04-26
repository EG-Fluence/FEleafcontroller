## Kernel Build

#### Build Kernel

- Begin with a functional Ubuntu 18.04 environment, such as within a virtual machine. It is not recommended to attempt building the kernel on a Banana Pi itself.

- Install Cross-compile dependencies.

```
sudo apt install \ 
	gcc-arm-linux-gnueabihf libc6-armhf-cross u-boot-tools \
	bc make gcc libc6-dev libncurses5-dev libssl-dev \
	bison flex autoconf ccache build-essential
```

- Use _[frank-w](https://github.com/frank-w/BPI-R2-4.14)_ repository for 4.14. Checkout the same version of code. (8c566f8e0780)
	
```
cd ~
mkdir bpi-r2
cd bpi-r2
mkdir -pv {backup,SD,mount_points/{BPI-ROOT,BPI-BOOT}}
git clone --single-branch --branch master https://github.com/FluenceEnergy/controller_software
git clone --single-branch --branch 4.14-main https://github.com/frank-w/BPI-R2-4.14
export PATH=~/bpi-r2/controller_software/tools/bin:$PATH
cd BPI-R2-4.14
git checkout 8c566f8e0780
```

- Use the adapted kernel configuration file provide [fluence_config](kernel/fluence_config), new [mt7623n-bananapi-bpi-r2.dts](kernel/mt7623n-bananapi-bpi-r2.dts) and corrected Ethernet driver [mtk_eth_soc.c](kernel/mtk_eth_soc.c).

```
cp ~/bpi-r2/controller_software/LeafController/BananaPiR2/doc/kernel/fluence_config .config
cp ~/bpi-r2/controller_software/LeafController/BananaPiR2/doc/kernel/mt7623n-bananapi-bpi-r2.dts ./arch/arm/boot/dts/mt7623n-bananapi-bpi-r2.dts
cp ~/bpi-r2/controller_software/LeafController/BananaPiR2/doc/kernel/mtk_eth_soc.c ./drivers/net/ethernet/mediatek/mtk_eth_soc.c
./build.sh config
```
	
- Exit and save the configuration from the menuing system.
	
- Run ```./build.sh``` script. This will probably take a few hours (only on fancy computer e.g. Mac).
- The build script will ask what package to create. Select option 1 **pack**, which will create a **tar.gz** image in the ```../SD/``` folder.
	
	```
	build successful
	===========================================
	1) pack
	2) install to SD-Card
	3) deb-package
	4) upload
	```

- With the USB inserted into the VM, mount the disks. Backup the existing kernel.

```
cd ../mount_points
sudo mount /dev/sdb1 BPI-BOOT
sudo mount /dev/sdb2 BPI-ROOT
sudo mv BPI-BOOT/bananapi/bpi-r2/linux/uImage{,.orig}
sudo tar xvfzp ../SD/bpi-r2*.tar.gz
```

- If not already done, upload  [kernel/uEnv.txt](kernel/uEnv.txt) file to ```BPI-BOOT/bananapi/bpi-r2/linux/```
```
cp ~/bpi-r2/controller_software/LeafController/BananaPiR2/doc/kernel/uEnv.txt BPI-BOOT/bananapi/bpi-r2/linux/.
```

- Unmount the volumes.

```
sudo umount BPI-BOOT
sudo umount BPI-ROOT
```

How to update both u-boot and Linux kernel
--------------------------------------------
If you don't have tools for banana-pi products, please run below commands to install them:
```
	sudo apt-get install pv
	curl -sL https://github.com/BPI-SINOVOIP/bpi-tools/raw/master/bpi-tools | sudo -E bash
```
1. Install SD card to this host PC, please ensure Ubuntu 18.04 R2 image is installed on this SD card
2. Enter folder SD which is generated after building
3. Run below command to update u-boot and Linux kernel
```
	bpi-update -c bpi-r2.conf
```
4. After it completes, move SD to R2 board
5. Press power button to activate this board
