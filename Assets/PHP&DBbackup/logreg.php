<?php
include "db.php";

$dt = $_POST;


$UserData = array(
    "id" => 0,
    "login"=> "dismi",
    "password" => "123456",
    "password2" => "123456"
);

$error = array(
    "errorText"=>"empty",
    "isError" => false
);

$ProgressData = array(
    "id" => 0,
    "userID"=> 0,
    "sceneID"=> 0,
    "inventoryID"=> 0
);

$InventoryData = array(
    "id" => 0,
    "userID"=> 0,
    "Slot1"=> "empty",
    "Slot2"=> "empty",
    "Slot3"=> "empty",
    "Slot4"=> "empty",
    "Slot5"=> "empty",
    "Slot6"=> "empty",
    "Slot7"=> "empty",
    "Slot8"=> "empty",
    "Slot9"=> "empty",
    "Slot10"=> "empty",
    "Slot11"=> "empty",
    "Slot12"=> "empty",
    "Slot13"=> "empty",
    "Slot14"=> "empty",
    "Slot15"=> "empty",
    "Slot16"=> "empty",
    "Slot17"=> "empty",
    "Slot18"=> "empty",
    "Slot19"=> "empty",
    "Slot20"=> "empty",
    "BeltSlot1"=> "empty",
    "BeltSlot2"=> "empty",
    "BeltSlot3"=> "empty",
    "BeltSlot4"=> "empty"
);

$usersData = array(
    "UserData" => $UserData,
    "error" => $error,
    "ProgressData" => $ProgressData,
    "InventoryData" => $InventoryData
);



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
else if ($dt['type'] == "saveProgress")
{
    $users = $db->query("SELECT * FROM `Users` WHERE `login` = '{$dt['login']}'");
    if ($users->rowCount() == 1)
    {
        $user = $users->fetch(PDO::FETCH_ASSOC);

        $data = $db->query("SELECT * FROM `Users` WHERE `id` = {$user['id']}")->fetch(PDO::FETCH_ASSOC);
        $progress = $db->query("SELECT * FROM `Progress` WHERE `userID` = {$user['id']}");
        if($progress->rowCount() == 0)
        {
            $Inventory = $db->query("SELECT * FROM `Inventory` WHERE `userID` = '{$user['id']}'")->fetch(PDO::FETCH_ASSOC);

            $db->query("INSERT INTO `Progress`(`userID`,`sceneID`,`inventoryID`)VALUES('{$user['id']}','{$dt['sceneID']}','{$Inventory['id']}')");

            $Progress = $db->query("SELECT * FROM `Progress` WHERE `userID` = '{$user['id']}'")->fetch(PDO::FETCH_ASSOC);

            // Дані про юзера які повернуться в юніті
            $usersData["UserData"]["id"]           = $user['id'];
            $usersData["UserData"]["login"]        = $data['login'];
            $usersData["UserData"]["password"]     = $data['password'];
            $usersData["UserData"]["password2"]    = $data['password'];
            // Інвентарь дані які повернуться в юніті
            $usersData["InventoryData"]["id"]    = $Inventory['id'];
            $usersData["InventoryData"]["userID"]= $Inventory['userID'];
            $usersData["InventoryData"]["Slot1"] = $Inventory['Slot1'];
            $usersData["InventoryData"]["Slot2"] = $Inventory['Slot2'];
            $usersData["InventoryData"]["Slot3"] = $Inventory['Slot3'];
            $usersData["InventoryData"]["Slot4"] = $Inventory['Slot4'];
            $usersData["InventoryData"]["Slot5"] = $Inventory['Slot5'];
            $usersData["InventoryData"]["Slot6"] = $Inventory['Slot6'];
            $usersData["InventoryData"]["Slot7"] = $Inventory['Slot7'];
            $usersData["InventoryData"]["Slot8"] = $Inventory['Slot8'];
            $usersData["InventoryData"]["Slot9"] = $Inventory['Slot9'];
            $usersData["InventoryData"]["Slot10"] = $Inventory['Slot10'];
            $usersData["InventoryData"]["Slot11"] = $Inventory['Slot11'];
            $usersData["InventoryData"]["Slot12"] = $Inventory['Slot12'];
            $usersData["InventoryData"]["Slot13"] = $Inventory['Slot13'];
            $usersData["InventoryData"]["Slot14"] = $Inventory['Slot14'];
            $usersData["InventoryData"]["Slot15"] = $Inventory['Slot15'];
            $usersData["InventoryData"]["Slot16"] = $Inventory['Slot16'];
            $usersData["InventoryData"]["Slot17"] = $Inventory['Slot17'];
            $usersData["InventoryData"]["Slot18"] = $Inventory['Slot18'];
            $usersData["InventoryData"]["Slot19"] = $Inventory['Slot19'];
            $usersData["InventoryData"]["Slot20"] = $Inventory['Slot20'];
            $usersData["InventoryData"]["BeltSlot1"] = $Inventory['BeltSlot1'];
            $usersData["InventoryData"]["BeltSlot2"] = $Inventory['BeltSlot2'];
            $usersData["InventoryData"]["BeltSlot3"] = $Inventory['BeltSlot3'];
            $usersData["InventoryData"]["BeltSlot4"] = $Inventory['BeltSlot4'];
            // Прогрес дані які повернуться в юніті
            $usersData["ProgressData"]["id"] = $Progress['id'];
            $usersData["ProgressData"]["userID"] = $Progress['userID'];
            $usersData["ProgressData"]["sceneID"] = $Progress['sceneID'];
            $usersData["ProgressData"]["inventoryID"] = $Progress['inventoryID'];
        }
        else if ($progress->rowCount() == 1){

            $Inventory = $db->query("SELECT * FROM `Inventory` WHERE `userID` = '{$user['id']}'")->fetch(PDO::FETCH_ASSOC);

            // Прогрес, оновлення в БД
            $db->query("UPDATE `Progress` SET `sceneID` = '{$dt['sceneID']}' WHERE `userID` = '{$user['id']}'");

            // Отримання оновлених даних про інвентар та прогрес
            $Progress = $db->query("SELECT * FROM `Progress` WHERE `userID` = '{$user['id']}'")->fetch(PDO::FETCH_ASSOC);

            // Дані про юзера які повернуться в юніті
            $usersData["UserData"]["id"]           = $user['id'];
            $usersData["UserData"]["login"]        = $data['login'];
            $usersData["UserData"]["password"]     = $data['password'];
            $usersData["UserData"]["password2"]    = $data['password'];
            // Інвентарь дані які повернуться в юніті
            $usersData["InventoryData"]["id"]    = $Inventory['id'];
            $usersData["InventoryData"]["userID"]= $Inventory['userID'];
            $usersData["InventoryData"]["Slot1"] = $Inventory['Slot1'];
            $usersData["InventoryData"]["Slot2"] = $Inventory['Slot2'];
            $usersData["InventoryData"]["Slot3"] = $Inventory['Slot3'];
            $usersData["InventoryData"]["Slot4"] = $Inventory['Slot4'];
            $usersData["InventoryData"]["Slot5"] = $Inventory['Slot5'];
            $usersData["InventoryData"]["Slot6"] = $Inventory['Slot6'];
            $usersData["InventoryData"]["Slot7"] = $Inventory['Slot7'];
            $usersData["InventoryData"]["Slot8"] = $Inventory['Slot8'];
            $usersData["InventoryData"]["Slot9"] = $Inventory['Slot9'];
            $usersData["InventoryData"]["Slot10"] = $Inventory['Slot10'];
            $usersData["InventoryData"]["Slot11"] = $Inventory['Slot11'];
            $usersData["InventoryData"]["Slot12"] = $Inventory['Slot12'];
            $usersData["InventoryData"]["Slot13"] = $Inventory['Slot13'];
            $usersData["InventoryData"]["Slot14"] = $Inventory['Slot14'];
            $usersData["InventoryData"]["Slot15"] = $Inventory['Slot15'];
            $usersData["InventoryData"]["Slot16"] = $Inventory['Slot16'];
            $usersData["InventoryData"]["Slot17"] = $Inventory['Slot17'];
            $usersData["InventoryData"]["Slot18"] = $Inventory['Slot18'];
            $usersData["InventoryData"]["Slot19"] = $Inventory['Slot19'];
            $usersData["InventoryData"]["Slot20"] = $Inventory['Slot20'];
            $usersData["InventoryData"]["BeltSlot1"] = $Inventory['BeltSlot1'];
            $usersData["InventoryData"]["BeltSlot2"] = $Inventory['BeltSlot2'];
            $usersData["InventoryData"]["BeltSlot3"] = $Inventory['BeltSlot3'];
            $usersData["InventoryData"]["BeltSlot4"] = $Inventory['BeltSlot4'];
            // Прогрес дані які повернуться в юніті
            $usersData["ProgressData"]["id"] = $Progress['id'];
            $usersData["ProgressData"]["userID"] = $Progress['userID'];
            $usersData["ProgressData"]["sceneID"] = $Progress['sceneID'];
            $usersData["ProgressData"]["inventoryID"] = $Progress['inventoryID'];
        }
        else
        {
            SetError("Save Data");
        }
    }
    else
    {
        SetError("Save Data");
    }
}
else if ($dt['type'] == "saveInventory")
{
    $users = $db->query("SELECT * FROM `Users` WHERE `login` = '{$dt['login']}'");
    if ($users->rowCount() == 1)
    {
        $user = $users->fetch(PDO::FETCH_ASSOC);

        $data = $db->query("SELECT * FROM `Users` WHERE `id` = {$user['id']}")->fetch(PDO::FETCH_ASSOC);
        $Inventorys = $db->query("SELECT * FROM `Inventory` WHERE `userID` = {$user['id']}");
        if($Inventorys->rowCount() == 0)
        {
            // Інвентарь, додавання в бд
            $db->query("INSERT INTO `Inventory`(`userID`,`Slot1`, `Slot2`, `Slot3`, `Slot4`, `Slot5`, `Slot6`, `Slot7`, `Slot8`, `Slot9`, `Slot10`, `Slot11`, `Slot12`, `Slot13`, `Slot14`, `Slot15`, `Slot16`, `Slot17`, `Slot18`, `Slot19`, `Slot20`, `BeltSlot1`, `BeltSlot2`, `BeltSlot3`, `BeltSlot4`)
                    VALUES ('{$user['id']}','{$dt['Slot1']}','{$dt['Slot2']}','{$dt['Slot3']}','{$dt['Slot4']}','{$dt['Slot5']}','{$dt['Slot6']}','{$dt['Slot7']}','{$dt['Slot8']}','{$dt['Slot9']}','{$dt['Slot10']}','{$dt['Slot11']}','{$dt['Slot12']}','{$dt['Slot13']}','{$dt['Slot14']}','{$dt['Slot15']}',
                            '{$dt['Slot16']}','{$dt['Slot17']}','{$dt['Slot18']}','{$dt['Slot19']}','{$dt['Slot20']}','{$dt['BeltSlot1']}','{$dt['BeltSlot2']}','{$dt['BeltSlot3']}','{$dt['BeltSlot4']}')");

            $Inventory = $db->query("SELECT * FROM `Inventory` WHERE `userID` = '{$user['id']}'")->fetch(PDO::FETCH_ASSOC);

            // Дані про юзера які повернуться в юніті
            $usersData["UserData"]["id"]           = $user['id'];
            $usersData["UserData"]["login"]        = $data['login'];
            $usersData["UserData"]["password"]     = $data['password'];
            $usersData["UserData"]["password2"]    = $data['password'];
            // Інвентарь дані які повернуться в юніті
            $usersData["InventoryData"]["id"]    = $Inventory['id'];
            $usersData["InventoryData"]["userID"]= $Inventory['userID'];
            $usersData["InventoryData"]["Slot1"] = $Inventory['Slot1'];
            $usersData["InventoryData"]["Slot2"] = $Inventory['Slot2'];
            $usersData["InventoryData"]["Slot3"] = $Inventory['Slot3'];
            $usersData["InventoryData"]["Slot4"] = $Inventory['Slot4'];
            $usersData["InventoryData"]["Slot5"] = $Inventory['Slot5'];
            $usersData["InventoryData"]["Slot6"] = $Inventory['Slot6'];
            $usersData["InventoryData"]["Slot7"] = $Inventory['Slot7'];
            $usersData["InventoryData"]["Slot8"] = $Inventory['Slot8'];
            $usersData["InventoryData"]["Slot9"] = $Inventory['Slot9'];
            $usersData["InventoryData"]["Slot10"] = $Inventory['Slot10'];
            $usersData["InventoryData"]["Slot11"] = $Inventory['Slot11'];
            $usersData["InventoryData"]["Slot12"] = $Inventory['Slot12'];
            $usersData["InventoryData"]["Slot13"] = $Inventory['Slot13'];
            $usersData["InventoryData"]["Slot14"] = $Inventory['Slot14'];
            $usersData["InventoryData"]["Slot15"] = $Inventory['Slot15'];
            $usersData["InventoryData"]["Slot16"] = $Inventory['Slot16'];
            $usersData["InventoryData"]["Slot17"] = $Inventory['Slot17'];
            $usersData["InventoryData"]["Slot18"] = $Inventory['Slot18'];
            $usersData["InventoryData"]["Slot19"] = $Inventory['Slot19'];
            $usersData["InventoryData"]["Slot20"] = $Inventory['Slot20'];
            $usersData["InventoryData"]["BeltSlot1"] = $Inventory['BeltSlot1'];
            $usersData["InventoryData"]["BeltSlot2"] = $Inventory['BeltSlot2'];
            $usersData["InventoryData"]["BeltSlot3"] = $Inventory['BeltSlot3'];
            $usersData["InventoryData"]["BeltSlot4"] = $Inventory['BeltSlot4'];
        }
        else if ($Inventorys->rowCount() == 1)
        {
            // Інвентарь, оновлення в БД
            $db->query("UPDATE `Inventory` SET
                `Slot1` = '{$dt['Slot1']}',
                `Slot2` = '{$dt['Slot2']}',
                `Slot3` = '{$dt['Slot3']}',
                `Slot4` = '{$dt['Slot4']}',
                `Slot5` = '{$dt['Slot5']}',
                `Slot6` = '{$dt['Slot6']}',
                `Slot7` = '{$dt['Slot7']}',
                `Slot8` = '{$dt['Slot8']}',
                `Slot9` = '{$dt['Slot9']}',
                `Slot10` = '{$dt['Slot10']}',
                `Slot11` = '{$dt['Slot11']}',
                `Slot12` = '{$dt['Slot12']}',
                `Slot13` = '{$dt['Slot13']}',
                `Slot14` = '{$dt['Slot14']}',
                `Slot15` = '{$dt['Slot15']}',
                `Slot16` = '{$dt['Slot16']}',
                `Slot17` = '{$dt['Slot17']}',
                `Slot18` = '{$dt['Slot18']}',
                `Slot19` = '{$dt['Slot19']}',
                `Slot20` = '{$dt['Slot20']}',
                `BeltSlot1` = '{$dt['BeltSlot1']}',
                `BeltSlot2` = '{$dt['BeltSlot2']}',
                `BeltSlot3` = '{$dt['BeltSlot3']}',
                `BeltSlot4` = '{$dt['BeltSlot4']}'
                WHERE `userID` = '{$user['id']}'");

            // Отримання оновлених даних про інвентар
            $Inventory = $db->query("SELECT * FROM `Inventory` WHERE `userID` = '{$user['id']}'")->fetch(PDO::FETCH_ASSOC);

            // Дані про юзера які повернуться в юніті
            $usersData["UserData"]["id"]           = $user['id'];
            $usersData["UserData"]["login"]        = $data['login'];
            $usersData["UserData"]["password"]     = $data['password'];
            $usersData["UserData"]["password2"]    = $data['password'];
            // Інвентарь дані які повернуться в юніті
            $usersData["InventoryData"]["id"]    = $Inventory['id'];
            $usersData["InventoryData"]["userID"]= $Inventory['userID'];
            $usersData["InventoryData"]["Slot1"] = $Inventory['Slot1'];
            $usersData["InventoryData"]["Slot2"] = $Inventory['Slot2'];
            $usersData["InventoryData"]["Slot3"] = $Inventory['Slot3'];
            $usersData["InventoryData"]["Slot4"] = $Inventory['Slot4'];
            $usersData["InventoryData"]["Slot5"] = $Inventory['Slot5'];
            $usersData["InventoryData"]["Slot6"] = $Inventory['Slot6'];
            $usersData["InventoryData"]["Slot7"] = $Inventory['Slot7'];
            $usersData["InventoryData"]["Slot8"] = $Inventory['Slot8'];
            $usersData["InventoryData"]["Slot9"] = $Inventory['Slot9'];
            $usersData["InventoryData"]["Slot10"] = $Inventory['Slot10'];
            $usersData["InventoryData"]["Slot11"] = $Inventory['Slot11'];
            $usersData["InventoryData"]["Slot12"] = $Inventory['Slot12'];
            $usersData["InventoryData"]["Slot13"] = $Inventory['Slot13'];
            $usersData["InventoryData"]["Slot14"] = $Inventory['Slot14'];
            $usersData["InventoryData"]["Slot15"] = $Inventory['Slot15'];
            $usersData["InventoryData"]["Slot16"] = $Inventory['Slot16'];
            $usersData["InventoryData"]["Slot17"] = $Inventory['Slot17'];
            $usersData["InventoryData"]["Slot18"] = $Inventory['Slot18'];
            $usersData["InventoryData"]["Slot19"] = $Inventory['Slot19'];
            $usersData["InventoryData"]["Slot20"] = $Inventory['Slot20'];
            $usersData["InventoryData"]["BeltSlot1"] = $Inventory['BeltSlot1'];
            $usersData["InventoryData"]["BeltSlot2"] = $Inventory['BeltSlot2'];
            $usersData["InventoryData"]["BeltSlot3"] = $Inventory['BeltSlot3'];
            $usersData["InventoryData"]["BeltSlot4"] = $Inventory['BeltSlot4'];
        }
        else
        {
            SetError("Save Data");
        }
    }
    else
    {
        SetError("Save Data");
    }
}
else if($dt['type'] == "LoadProgress")
{
    $users = $db->query("SELECT * FROM `Users` WHERE `login` = '{$dt['login']}'");
    if ($users->rowCount() == 1)
    {
        $user = $users->fetch(PDO::FETCH_ASSOC);

        $data = $db->query("SELECT * FROM `Users` WHERE `id` = {$user['id']}")->fetch(PDO::FETCH_ASSOC);
        $progress = $db->query("SELECT * FROM `Progress` WHERE `userID` = {$user['id']}");
        if ($progress->rowCount() == 1)
        {
            $GetInventory = $db->query("SELECT * FROM `Inventory` WHERE `userID` = '{$user['id']}'")->fetch(PDO::FETCH_ASSOC);
            $Progress = $db->query("SELECT * FROM `Progress` WHERE `userID` = '{$user['id']}'")->fetch(PDO::FETCH_ASSOC);

            // Дані про юзера які повернуться в юніті
            $usersData["UserData"]["id"]           = $user['id'];
            $usersData["UserData"]["login"]        = $data['login'];
            $usersData["UserData"]["password"]     = $data['password'];
            $usersData["UserData"]["password2"]    = $data['password'];
            // Інвентарь дані які повернуться в юніті
            $usersData["InventoryData"]["id"]    = $GetInventory['id'];
            $usersData["InventoryData"]["userID"]= $GetInventory['userID'];
            $usersData["InventoryData"]["Slot1"] = $GetInventory['Slot1'];
            $usersData["InventoryData"]["Slot2"] = $GetInventory['Slot2'];
            $usersData["InventoryData"]["Slot3"] = $GetInventory['Slot3'];
            $usersData["InventoryData"]["Slot4"] = $GetInventory['Slot4'];
            $usersData["InventoryData"]["Slot5"] = $GetInventory['Slot5'];
            $usersData["InventoryData"]["Slot6"] = $GetInventory['Slot6'];
            $usersData["InventoryData"]["Slot7"] = $GetInventory['Slot7'];
            $usersData["InventoryData"]["Slot8"] = $GetInventory['Slot8'];
            $usersData["InventoryData"]["Slot9"] = $GetInventory['Slot9'];
            $usersData["InventoryData"]["Slot10"] = $GetInventory['Slot10'];
            $usersData["InventoryData"]["Slot11"] = $GetInventory['Slot11'];
            $usersData["InventoryData"]["Slot12"] = $GetInventory['Slot12'];
            $usersData["InventoryData"]["Slot13"] = $GetInventory['Slot13'];
            $usersData["InventoryData"]["Slot14"] = $GetInventory['Slot14'];
            $usersData["InventoryData"]["Slot15"] = $GetInventory['Slot15'];
            $usersData["InventoryData"]["Slot16"] = $GetInventory['Slot16'];
            $usersData["InventoryData"]["Slot17"] = $GetInventory['Slot17'];
            $usersData["InventoryData"]["Slot18"] = $GetInventory['Slot18'];
            $usersData["InventoryData"]["Slot19"] = $GetInventory['Slot19'];
            $usersData["InventoryData"]["Slot20"] = $GetInventory['Slot20'];
            $usersData["InventoryData"]["BeltSlot1"] = $GetInventory['BeltSlot1'];
            $usersData["InventoryData"]["BeltSlot2"] = $GetInventory['BeltSlot2'];
            $usersData["InventoryData"]["BeltSlot3"] = $GetInventory['BeltSlot3'];
            $usersData["InventoryData"]["BeltSlot4"] = $GetInventory['BeltSlot4'];
            // Прогрес дані які повернуться в юніті
            $usersData["ProgressData"]["id"] = $Progress['id'];
            $usersData["ProgressData"]["userID"] = $Progress['userID'];
            $usersData["ProgressData"]["sceneID"] = $Progress['sceneID'];
            $usersData["ProgressData"]["inventoryID"] = $Progress['inventoryID'];
        }
        else if($progress->rowCount() == 0)
        {
            $usersData["UserData"]["id"]           = $user['id'];
            $usersData["UserData"]["login"]        = $data['login'];
            $usersData["UserData"]["password"]     = $data['password'];
            $usersData["UserData"]["password2"]    = $data['password'];
        }
        else
        {
            SetError("load Data");
        }
    }
}
else
{
    SetError("Unknown data");
}

function SetError($text)
{
    global $usersData;
    $usersData["UserData"] = null;
    $usersData["ProgressData"] = null;
    $usersData["error"]["isError"] = true;
    $usersData["error"]["errorText"] = "Error: ".$text;
}

echo json_encode($usersData, JSON_UNESCAPED_UNICODE);
?>