using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using Newtonsoft.Json;

[System.Serializable]
public class UsersData
{
    public User UserData;
    public Error error;
    public ProgressData ProgressData;
    public InventoryData InventoryData;

    public UsersData()
    {

    }

    public UsersData(User userData, Error error, ProgressData userprogress, InventoryData userinventory)
    {
        UserData = userData;
        this.error = error;
        ProgressData = userprogress;
        InventoryData = userinventory;
    }
}
[System.Serializable]
public class Error
{
    public string errorText;
    public bool isError;
}
[System.Serializable]
public class User
{
    public int id;
    public string login;
    public string password;
    public string password2;

    public User()
    {
        
    }
    
    public User(string logIn,string pass1, string pass2)
    {
        login = logIn;
        password = pass1;
        password2 = pass2;
    }
    public void SetLogin(string Login) => login = Login;
    public void SetPass1(string pass) => password = pass;
    public void SetPass2(string pass) => password2 = pass;
}
[System.Serializable]
public class ProgressData
{
    public int id;
    public int userID;
    public int sceneID;
    public int inventoryID;

    public ProgressData() 
    {
        
    }
    public ProgressData(int UserID, int SceneID, int InventoryID)
    {
        userID = UserID;
        sceneID = SceneID;
        inventoryID = InventoryID;
    }
    public void SetUserID(int UserID) => userID = UserID;
    public void SetSceneID(int SceneID) => sceneID = SceneID;
    public void SetInventoryID(int InventoryID) => inventoryID = InventoryID;
}
[System.Serializable]
public class InventoryData
{
    public int id;
    public int userID;
    public string Slot1;
    public string Slot2;
    public string Slot3;
    public string Slot4;
    public string Slot5;
    public string Slot6;
    public string Slot7;
    public string Slot8;
    public string Slot9;
    public string Slot10;
    public string Slot11;
    public string Slot12;
    public string Slot13;
    public string Slot14;
    public string Slot15;
    public string Slot16;
    public string Slot17;
    public string Slot18;
    public string Slot19;
    public string Slot20;
    public string BeltSlot1;
    public string BeltSlot2;
    public string BeltSlot3;
    public string BeltSlot4;

    public InventoryData()
    {

    }
    public InventoryData(int UserID,string slot1, string slot2, string slot3, string slot4, string slot5, string slot6, string slot7,  
        string slot8, string slot9, string slot10, string slot11, string slot12, string slot13, string slot14, string slot15, 
        string slot16, string slot17, string slot18, string slot19, string slot20, string beltSlot1, string beltSlot2, string beltSlot3, string beltSlot4)
    {
        userID = UserID;
        Slot1 = slot1; Slot2 = slot2; Slot3 = slot3; Slot4 = slot4; Slot5 = slot5; 
        Slot6 = slot6; Slot7 = slot7; Slot8 = slot8; Slot9 = slot9; Slot10 = slot10; 
        Slot11 = slot11; Slot12 = slot12; Slot13 = slot13; Slot14 = slot14; Slot15 = slot15;
        Slot16 = slot16; Slot17 = slot17; Slot18 = slot18; Slot19 = slot19; Slot20 = slot20;
        BeltSlot1 = beltSlot1; BeltSlot2 = beltSlot2; BeltSlot3 = beltSlot3; BeltSlot4 = beltSlot4;
    }
}
[System.Serializable]
public class NewGameData
{
    public int id;
    public int SceneID;
    public string objName;
    public string posX;
    public string posY;

    public NewGameData()
    {

    }

    public NewGameData(int sceneID, string ObjName, string PosX, string PosY)
    {
        SceneID = sceneID;
        objName = ObjName;
        posX = PosX;
        posY = PosY;
    }
}
[System.Serializable]
public class SceneObjectData
{
    public int id;
    public int UserID;
    public int SceneID;
    public string objName;
    public string posX;
    public string posY;

    public SceneObjectData()
    {

    }

    public SceneObjectData(int UserID, int SceneID, string objName, string posX, string posY)
    {
        this.UserID = UserID;
        this.SceneID = SceneID;
        this.objName = objName;
        this.posX = posX;
        this.posY = posY;
    }
}

public class WebManager : MonoBehaviour
{
    public static UsersData usersData = new UsersData();
    public static NewGameData[] newGameData;
    public static SceneObjectData[] sceneObjData;
    [SerializeField] private string targetURL;
    [SerializeField] private string targetSceneURL;

    public UnityEvent OnLogged, OnRegistered, OnError;
    
    public enum RequestType
    {
        logging, register, saveProgress, LoadProgress, saveScene, LoadScene, newGame, DelOldScene, saveInventory
    }
    
    public string GetUserData(UsersData data)
    {
        return JsonUtility.ToJson(data);
    }

    public UsersData SetUserData(string data)
    {
        print(data);
        var usersData = JsonUtility.FromJson<UsersData>(data);
        WebManager.usersData = new UsersData(usersData.UserData, usersData.error, usersData.ProgressData, usersData.InventoryData);
        return WebManager.usersData;
    }
    
    private void Start()
    {
        usersData.error = new Error() { errorText = "text", isError = true };
        usersData.UserData = new User("Dismi", "test1", "test1");
        usersData.ProgressData = new ProgressData(0, 0, 0);
        usersData.InventoryData = new InventoryData(0, "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty", "empty");
        //sceneData = new SceneData(0,"name","0.0","0.0");
    }

    public void Login(string login, string password)
    {
        StopAllCoroutines();
        if (CheckString(login) && CheckString(password))
        {
            Logging(login, password);
        }
        else
        {
            usersData.error.errorText = "Невірний ввід логіну чи паролю";
            usersData.error.isError = true;
            OnError.Invoke();
        }
    }
    public void Registration(string login, string password, string password2)
    {
        StopAllCoroutines();
        if (CheckString(login) && CheckString(password) && CheckString(password2) && password == password2)
        {
            Registering(login, password, password2);
        }
        else
        {
            usersData.error.errorText = "Перевірте довжину логіну або паролю не меньше 4 символів та не більше 16, або ж перевірте чи однакові паролі";
            usersData.error.isError = true;
            OnError.Invoke();
        }
    }

    public bool CheckString(string toCheck)
    {
        toCheck = toCheck.Trim();
        if (toCheck.Length > 4 && toCheck.Length < 16)
        {
            return true;
        }
        return false;
    }
    
    public void Logging(string login, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.logging.ToString());
        form.AddField("login", login);
        form.AddField("password", password);
        StartCoroutine(SendData(form, RequestType.logging));
    }
    
    public void Registering(string login, string password1, string password2)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.register.ToString());
        form.AddField("login", login);
        form.AddField("password1", password1);
        form.AddField("password2", password2);
        StartCoroutine(SendData(form, RequestType.register));
    }

    public void SaveProgress(string login, int sceneID)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.saveProgress.ToString());
        form.AddField("login", login);
        form.AddField("sceneID", sceneID);
        StartCoroutine(SendData(form, RequestType.saveProgress));
    }
    public void LoadProgress(string login)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.LoadProgress.ToString());
        form.AddField("login", login);
        StartCoroutine(SendData(form, RequestType.LoadProgress));
    }
    public void SaveScene(string login, int sceneID, string objName, string posX, string posY)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.saveScene.ToString());
        form.AddField("login", login);
        form.AddField("SceneID", sceneID);
        form.AddField("objName", objName);
        form.AddField("posX", posX);
        form.AddField("posY", posY);
        StartCoroutine(SendSceneData(form, RequestType.saveScene));
    }
    public void SaveInventory(string login,string slot1, string slot2, string slot3, string slot4, string slot5, string slot6, string slot7,
        string slot8, string slot9, string slot10, string slot11, string slot12, string slot13, string slot14, string slot15,
        string slot16, string slot17, string slot18, string slot19, string slot20, string beltSlot1, string beltSlot2, string beltSlot3, string beltSlot4)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.saveInventory.ToString());
        form.AddField("login", login);
        form.AddField("Slot1", slot1);
        form.AddField("Slot2", slot2);
        form.AddField("Slot3", slot3);
        form.AddField("Slot4", slot4);
        form.AddField("Slot5", slot5);
        form.AddField("Slot6", slot6);
        form.AddField("Slot7", slot7);
        form.AddField("Slot8", slot8);
        form.AddField("Slot9", slot9);
        form.AddField("Slot10", slot10);
        form.AddField("Slot11", slot11);
        form.AddField("Slot12", slot12);
        form.AddField("Slot13", slot13);
        form.AddField("Slot14", slot14);
        form.AddField("Slot15", slot15);
        form.AddField("Slot16", slot16);
        form.AddField("Slot17", slot17);
        form.AddField("Slot18", slot18);
        form.AddField("Slot19", slot19);
        form.AddField("Slot20", slot20);
        form.AddField("BeltSlot1", beltSlot1);
        form.AddField("BeltSlot2", beltSlot2);
        form.AddField("BeltSlot3", beltSlot3);
        form.AddField("BeltSlot4", beltSlot4);
        StartCoroutine(SendData(form, RequestType.saveInventory));
    }
    public void DelOldScene(string login,int sceneID)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.DelOldScene.ToString());
        form.AddField("login", login);
        form.AddField("SceneID", sceneID);
        StartCoroutine(SendSceneData(form, RequestType.DelOldScene));
    }
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
    IEnumerator SendSceneData(WWWForm form, RequestType type)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(targetSceneURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
        }
    }
    public SceneObjectData[] SetSceneData(string data)
    {
        print(data);
        SceneObjectData[] sceneObjects = JsonConvert.DeserializeObject<SceneObjectData[]>(data);
        sceneObjData = sceneObjects;
        return sceneObjData;
    }
    public NewGameData[] SetNewSceneData(string data)
    {
        print(data);
        NewGameData[] sceneObjects = JsonConvert.DeserializeObject<NewGameData[]>(data);
        newGameData = sceneObjects;
        return newGameData;
    }
    public void GetSceneObjects(string login, int sceneID)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.LoadScene.ToString());
        form.AddField("login", login);
        form.AddField("SceneID", sceneID.ToString());
        StartCoroutine(GetSceneObjectsRequest(form, RequestType.LoadScene));
    }
    public void GetNewSceneObjects(string login, int sceneID)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.newGame.ToString());
        form.AddField("login", login);
        form.AddField("SceneID", sceneID.ToString());
        StartCoroutine(GetSceneObjectsRequest(form, RequestType.newGame));
    }
    private IEnumerator GetSceneObjectsRequest(WWWForm form, RequestType type)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(targetSceneURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (type == RequestType.newGame)
                {
                    var data = www.downloadHandler.text;
                    newGameData = SetNewSceneData(data);
                }
                else if (type == RequestType.LoadScene)
                {
                    var data = www.downloadHandler.text;
                    sceneObjData = SetSceneData(data);
                }
            }
        }
    }
}
