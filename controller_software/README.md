# __NEVER use a master branch in production !!!!!!__
## Project with LD Cubes -> Gen6 branch
## Project with SD Cubes -> RaP branch
## Testing in DEPCTR     -> DEPCTR branch
## Testing in ERLANGEN   -> Gen6 branch

# Start
 
Welcome to Fluence of Future.

## Git

### Install Git
- Ubuntu: `sudo apt-get install git`
- Windows: [Download](https://git-scm.com/download/win)
- Mac: [Download](https://git-scm.com/download/mac)

### diff tool

[Beyond Compare](https://www.scootersoftware.com/download.php) as diff/merge tool
_(Note:  Use bc3 on the command line for BC version 4. Caused by git legacy support from Linux.)_
```
sudo git config --global diff.tool bc3
sudo git config --global merge.tool bc3
sudo git config --global mergetool.bc3.trustExitCode true
sudo git config --global core.editor "nano"
```

### Clone repository

Clone the Existing Repository _controller_software_ from GitHub

```
cd ~
mkdir bpi-r2
cd bpi-r2
git clone https://github.com/FluenceEnergy/controller_software.git
```

alternative
```
sudo mkdir /lproj
sudo chmod a+rw /lproj
cd /lproj
git clone https://github.com/FluenceEnergy/controller_software.git
```
or if you would like use ssh without be asked for user/password 
```
cd ~
mkdir bpi-r2
cd bpi-r2
git clone git@github.com:FluenceEnergy/controller_software.git
```

__HINT__: Add public key (~/.ssh/id_ed25519.pub), created with comand  ```ssh-keygen -t ed25519``` into your GitHub account (User -> Settings -> SSH and GPG keys) and test connection with ```ssh -vT git@github.com```


Add user and email address
```
cd controller_software
git config --local user.email "name.familyname@flueneceenergy.com"
git config --local user.name "GitUserName"
```
Test
```
boris@ubuntu:/lproj/controller_software$ git config --local --list
```
Response
```
core.repositoryformatversion=0
core.filemode=true
core.bare=false
core.logallrefupdates=true
remote.origin.url=https://github.com/FluenceEnergy/controller_software.git
remote.origin.fetch=+refs/heads/*:refs/remotes/origin/*
branch.master.remote=origin
branch.master.merge=refs/heads/master
user.email=boris.kajganic@flueneceenergy.com
user.name=Uskok
```

### Get update from repository

Git command is used to update the local version of a repository from a remote.
```
git pull origin
```

### Useful Git Commands

**git init  [project-name]** - Create an empty local Git repository

**git clone [url]** - Clone a repository whit entire version history into a new directory

**git checkout master** - Switch back to master

**git checkout -b DCS-4299** - Create a new branch named "DCS-4299" and switch to it

**git branch** - List all local branches in your repository

**git commit -a** - Commit all locaql changes in tracked files

**git pull** - Update your local repository and workspace to the newest commit

**git fetch** - Update your local repository to the newest commit

**git push origin &lt;branch&gt;** - Push the branch to remote repository

**git fetch origin Gen6:Gen6** Update your local repository to the newest commit from branch


For a comprehensive guide see [Git Documentation](https://git-scm.com/docs).

## GitEye
User frendly client for git

- Linux: [Download](https://www.collab.net/downloads/giteye#show-Linux)
- Windows: [Download](https://www.collab.net/downloads/giteye#show-Windows)
- Mac: [Download](https://www.collab.net/downloads/giteye#show-Mac_OSX)

If possible Choose to ‘Open with Archive Manager’.

```
cd /opt
sudo mv ~/Downloads/GitEye-2.2.0-linux.x86_64.zip .
sudo unzip GitEye-2.2.0-linux.x86_64.zip -d GitEye
sudo ln -sf /opt/GitEye/GitEye /usr/local/bin/GitEye
```

### Usage
```
GitEye
```

## User settings

Add to the end of */.profile

```
#Added tools for Leaf Controller development
if [ -d "$HOME/bpi-r2/controller_software/tools/bin" ] ; then
   export PATH=$HOME/bpi-r2/controller_software/tools/bin:$PATH
fi
```

Add current user to _dialout_ group. Otherwise USB port is not visible for the user and upload of Arduino programs is not possible.
```
sudo usermod -a -G dialout username
```

```
lsusb
Bus 004 Device 001: ID 1d6b:0003 Linux Foundation 3.0 root hub
Bus 003 Device 005: ID 0e0f:0002 VMware, Inc. Virtual USB Hub
Bus 003 Device 004: ID 0e0f:0002 VMware, Inc. Virtual USB Hub
```
**`Bus 003 Device 003: ID 2341:0042 Arduino SA Mega 2560 R3 (CDC ACM)`**`<- Connected Controllino`
```
Bus 003 Device 002: ID 0e0f:0003 VMware, Inc. Virtual Mouse
Bus 003 Device 001: ID 1d6b:0002 Linux Foundation 2.0 root hub
Bus 001 Device 001: ID 1d6b:0002 Linux Foundation 2.0 root hub
Bus 002 Device 002: ID 0e0f:0002 VMware, Inc. Virtual USB Hub
Bus 002 Device 001: ID 1d6b:0001 Linux Foundation 1.1 root hub
```

Device is **ttyACM0**

```
ls -la /dev/ttyACM0
crw-rw----   1 root  dialout 166,   0 Nov  7 11:26 ttyACM0
```



# Controller Software

- [Core Controller](CoreController/)
- [Node Controller](NodeController/)
- [Leaf Controller](LeafController/)
- [Developer Tools](tools/)
