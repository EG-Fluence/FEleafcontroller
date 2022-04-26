# Tools
Set of __[tools](bin/)__ to make life easier for developer

Set default working directories uses in the project
```
cd ~
mkdir bpi-r2
cd bpi-r2
mkdir -pv {backup,SD,mount_points/{BPI-ROOT,BPI-BOOT}}
export PATH=~/bpi-r2/controller_software/tools/bin:$PATH
```

You can permanetly add tools to the end of your .profile file

```
# ~/.profile: executed by the command interpreter for login shells.
# This file is not read by bash(1), if ~/.bash_profile or ~/.bash_login
# exists.
# see /usr/share/doc/bash/examples/startup-files for examples.
# the files are located in the bash-doc package.

# the default umask is set in /etc/profile; for setting the umask
# for ssh logins, install and configure the libpam-umask package.
#umask 022

# if running bash
if [ -n "$BASH_VERSION" ]; then
    # include .bashrc if it exists
    if [ -f "$HOME/.bashrc" ]; then
        . "$HOME/.bashrc"
    fi
fi

# set PATH so it includes user's private bin if it exists
if [ -d "$HOME/bin" ] ; then
    PATH="$HOME/bin:$PATH"
fi

# set PATH so it includes user's private bin if it exists
if [ -d "$HOME/.local/bin" ] ; then
    PATH="$HOME/.local/bin:$PATH"
fi

#Added tools for Leaf Controller development
if [ -d "$HOME/bpi-r2/controller_software/tools/bin" ] ; then
   export PATH=$HOME/bpi-r2/controller_software/tools/bin:$PATH
fi
```
## Useful commands

### Find all Serial ports
```
ls -la /sys/class/tty/*/device/driver | awk '{print $9}' | awk -F'/' '{print $5}'
```
