<?php
include "db.php";

$dt = $_POST;

if($dt['type'] == "saveScene")
{
    $users = $db->query("SELECT * FROM `Users` WHERE `login` = '{$dt['login']}'");

    if ($users->rowCount() == 1)
    {
        $user = $users->fetch(PDO::FETCH_ASSOC);

        // Сцена, додавання в бд
        $db->query("INSERT INTO `Scene`(`UserID`, `SceneID`, `objName`, `posX`, `posY`)
                    VALUES ('{$user['id']}', '{$dt['SceneID']}', '{$dt['objName']}', '{$dt['posX']}', '{$dt['posY']}')");
    }
}
elseif ($dt['type'] == "DelOldScene")
{
    $users = $db->query("SELECT * FROM `Users` WHERE `login` = '{$dt['login']}'");

    if ($users->rowCount() == 1)
    {
        $user = $users->fetch(PDO::FETCH_ASSOC);

        // Видалення рядків з бази даних, які відповідають користувачеві та сцені
        $db->query("DELETE FROM `Scene` WHERE `UserID` = '{$user['id']}' AND `SceneID` = '{$dt['SceneID']}'");
    }
}
elseif ($dt['type'] == "LoadScene")
{
    $users = $db->query("SELECT * FROM `Users` WHERE `login` = '{$dt['login']}'");

    if ($users->rowCount() == 1)
    {
        $user = $users->fetch(PDO::FETCH_ASSOC);
        $sceneID = $dt['SceneID'];

        $sceneObjects = $db->query("SELECT * FROM `Scene` WHERE `UserID` = {$user['id']} AND `SceneID` = {$sceneID}")->fetchAll(PDO::FETCH_ASSOC);

        $sceneDataArray = array(); // Створюємо порожній масив

        foreach ($sceneObjects as $sceneObject)
        {
            $sceneData = array(
                "id" => $sceneObject['id'],
                "UserID" => $sceneObject['UserID'],
                "SceneID" => $sceneObject['SceneID'],
                "objName" => $sceneObject['objName'],
                "posX" => $sceneObject['posX'],
                "posY" => $sceneObject['posY']
            );

            array_push($sceneDataArray, $sceneData); // Додавання даних об'єкта в масив
        }

        echo json_encode($sceneDataArray, JSON_UNESCAPED_UNICODE);
    }
}
elseif ($dt['type'] == "newGame")
{
    $sceneID = $dt['SceneID'];

    $sceneObjects = $db->query("SELECT * FROM `MainScene` WHERE `SceneID` = {$sceneID}")->fetchAll(PDO::FETCH_ASSOC);

    $mainSceneDataArray = array();

    foreach ($sceneObjects as $sceneObject)
    {
        $MainSceneData = array(
            "id" => $sceneObject['id'],
            "SceneID" => $sceneObject['SceneID'],
            "objName" => $sceneObject['objName'],
            "posX" => $sceneObject['posX'],
            "posY" => $sceneObject['posY']
        );

        array_push($mainSceneDataArray, $MainSceneData ); // Додавання даних об'єкта в масив
    }

    echo json_encode($mainSceneDataArray, JSON_UNESCAPED_UNICODE);
}


?>