using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataLoader : MonoBehaviour
{
    private WebManager manager;
    public GameObject[] itemObjects;
    public Item[] ObjectsInfo;
    private GameObject container;

    public void Start()
    {
        manager = GetComponent<WebManager>();
    }
    public void NewGameData()
    {
        manager.GetNewSceneObjects(WebManager.usersData.UserData.login, 1);
        StartCoroutine(NewGame());
    }
    public void LoadData()
    {
        manager.LoadProgress(WebManager.usersData.UserData.login);
        StartCoroutine(LoadDatas());
    }
    private IEnumerator NewGame()
    {
        yield return new WaitForSeconds(1f);
        DontDestroyOnLoad(manager);
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(1f);
        container = GameObject.Find("NotStaticObj");
        CreateNewSceneObjects(WebManager.newGameData);
    }
    private IEnumerator LoadDatas()
    {
        yield return new WaitForSeconds(1f); // Затримка на 1 секундy для отримання даних з сервера
        manager.GetSceneObjects(WebManager.usersData.UserData.login, WebManager.usersData.ProgressData.sceneID);
        yield return new WaitForSeconds(1f); 
        if (WebManager.usersData.ProgressData.id != 0)
        {
            DontDestroyOnLoad(manager);
            SceneManager.LoadScene(WebManager.usersData.ProgressData.sceneID);
            yield return new WaitForSeconds(1f);
            container = GameObject.Find("NotStaticObj");
            CreateSceneObjects(WebManager.sceneObjData);
        }
    }
    private void CreateSceneObjects(SceneObjectData[] sceneObjects)
    {
        foreach (var sceneObject in sceneObjects)
        {
            GameObject newObject = InstantiateObject(sceneObject.objName);
            float posX;
            float posY;
            float.TryParse(sceneObject.posX, out posX);
            float.TryParse(sceneObject.posY, out posY);
            SetObjectPosition(newObject, posX, posY);
        }
    }
    private void CreateNewSceneObjects(NewGameData[] sceneObjects)
    {
        foreach (var sceneObject in sceneObjects)
        {
            GameObject newObject = InstantiateObject(sceneObject.objName);
            float posX;
            float posY;
            float.TryParse(sceneObject.posX, out posX);
            float.TryParse(sceneObject.posY, out posY);
            SetObjectPosition(newObject, posX, posY);
        }
    }

    private GameObject InstantiateObject(string objType)
    {
        GameObject prefab = GetPrefabByObjectType(objType);
        GameObject newObject = Instantiate(prefab);
        newObject.transform.parent = container.transform;
        return newObject;
    }

    private void SetObjectPosition(GameObject obj, float posX, float posY)
    {
        obj.transform.position = new Vector3(posX, posY, 0f);
    }

    private GameObject GetPrefabByObjectType(string objType)
    {
        switch (objType)
        {
            case "Axe(Clone)":
                return itemObjects[0];
            case "Pickaxe(Clone)":
                return itemObjects[1];
            case "Knife(Clone)":
                return itemObjects[2];
            case "Stone(Clone)":
                return itemObjects[3];
            case "Wood(Clone)":
                return itemObjects[4];
            case "Meat(Clone)":
                return itemObjects[5];
            case "tree(Clone)":
                return itemObjects[6];
            case "fox(Clone)":
                return itemObjects[7];
            default:
                return null;
        }
    }
}
