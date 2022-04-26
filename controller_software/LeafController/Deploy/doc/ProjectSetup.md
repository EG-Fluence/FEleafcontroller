- Ask Shakti to create the account on array controller
- create on local computer the script ```~/bin/Go2project.sh``` 
    ```bash
    ssh bkajganic.admin@10.194.141.3
    ```
- Logon on array controller and change password (command passwd)
- Generate public key on __local__ computer __only__ if is not exist
    ```
    ssh-keygen -t ed25519
    ```
- Copy public key from __local__ computer to __array__ server
    ```
    ssh-copy-id -i ~/.ssh/id_ed25519.pub user@host
    ```
- Test ssh without passwd.
- Generate public key on __array__ controller __only__ if is not exist
    ```
    ssh-keygen -t ed25519
    ```
- Goto the your GitHub User Settings (https://github.com/settings/keys or GitHub account -> User -> Settings -> SSH and GPG keys ), create new ssh key and upload ed25519.pub from array controller
- Test ssh from aray controller to GitHub
    ```
    ssh -vT git@github.com
    ```
 - If is not working you will ned to setup ssh over https as is described [here](https://docs.github.com/en/github/authenticating-to-github/using-ssh-over-the-https-port)
 - Create local copy of the project
      ```
      cd ~
      mkdir bpi-r2
      cd bpi-r2
      git clone git@github.com:FluenceEnergy/controller_software.git
      ```
 - Add user and email address
      ```
      cd controller_software
      git config --local user.email "name.familyname@flueneceenergy.com"
      git config --local user.name "GitUserName"
      ```
- Get update from repository
      ```
      git pull origin
      ```
- Fetch remote branch (first argument) ```Gen6``` as local branch ```Gen6``` (second argument) and set tracking information for this branch

    ```
    git fetch origin Gen6:Gen6 && git checkout Gen6
    git branch --set-upstream-to=origin/Gen6 Gen6
    ```
