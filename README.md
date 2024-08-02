## Samurai 2d game

<p align="center">
<img src="https://i.ibb.co/7WM3dSz/Samurai-Icon.png" width="126px">
</p>

<p align="center">
<img alt="Static Badge" src="https://img.shields.io/badge/Engine%20-Unity%202022.3.35f1-purple">
<img alt="Static Badge" src="https://img.shields.io/badge/Version%20-0.0.1%20(Alpha)-blue">
</p>

### About
____
A prototype game about samurai on an island. The game has registration, login, save progress, inventory, enemies to fight, and resources to collect. 

### How it work 
____
It's all based on UnityWebRequest, which is how we exchange data with the server where it is processed and sent back to unity if necessary. 
Here's a quick example of how it works:
#### </br>C# code which send data to server
```
IEnumerator SendData(WWWForm form, RequestType type)
{
    using (UnityWebRequest www = UnityWebRequest.Post(targetURL, form))
    {
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            var data = SetUserData(www.downloadHandler.text);
            if (!data.error.isError)
            {
                if (type != RequestType.saveProgress || type != RequestType.LoadProgress || type != RequestType.saveInventory)
                {
                    usersData = data;
                    if (type == RequestType.logging)
                    {
                        OnLogged.Invoke();
                    }
                    else
                    {
                        OnRegistered.Invoke();
                    }
                }
            }
            else
            {
                usersData = data;
                OnError.Invoke();
            }
        }
    }
}
```
For watching all c# code u can [click here](Assets/Scripts/DataBase/WebManager.cs)
#### </br>Second is the php code for add our data to database and get is back to unity here
```
<?php
include "db.php";

$dt = $_POST;

if ($dt['type'] == "logging"){
    if (isset($dt['login']) && isset($dt['password'])){
        $users = $db->query("SELECT * FROM `Users` WHERE `login` = '{$dt['login']}'");

        if ($users->rowCount() == 1)
        {
            $user = $users->fetch(PDO::FETCH_ASSOC);

            if (password_verify($dt['password'], $user['password'])){

                $data = $db->query("SELECT * FROM `Users` WHERE `id` = {$user['id']}")->fetch(PDO::FETCH_ASSOC);

                $usersData["UserData"]["id"]         = $user['id'];
                $usersData["UserData"]["login"]      = $data['login'];
                $usersData["UserData"]["password"]   = $data['password'];
                $usersData["UserData"]["password2"]  = $data['password'];
            }
        }
        else
        {
            SetError("Password or User uncorrenct");
        }
    }
    else
    {
            SetError("Password or User uncorrenct");
    }
}
else if ($dt['type'] == "register")
{
    if (isset($dt['login']) && isset($dt['password1']) && isset($dt['password2']))
    {
        $users = $db->query("SELECT * FROM `Users` WHERE `login` = '{$dt['login']}'");
        if ($users->rowCount() == 0)
        {
            if ($dt['password1'] == $dt['password2'])
            {
                $hash = password_hash($dt['password1'], PASSWORD_DEFAULT);
                //Створення юзера
                $db->query("INSERT INTO `Users`(`login`, `password`) VALUES ('{$dt['login']}','{$hash}')");
            }
            else
            {
                SetError("Password");
            }
        }
        else
        {
            SetError("User Exists");
        }
    }
}
```
For watching all php code u can [click here](Assets/PHP&Database/logreg.php)

### Developers
____
[<img alt="Static Badge" src="https://img.shields.io/badge/-Bohdan-green">](https://github.com/d1smire)

### Game Screnshots
____
