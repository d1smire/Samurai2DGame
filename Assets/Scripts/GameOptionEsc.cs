using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOptionEsc : MonoBehaviour
{
    private GameObject EscMenu;
    [SerializeField] private WebManager webManager;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerMovement _player;
    private bool IsOpen = false;
    private GameObject container;
    private GameObject SavingMenu;

    private void Start()
    {
        container = GameObject.Find("NotStaticObj");
        webManager = GameObject.Find("WebManager").GetComponent<WebManager>();
        _inventory = GameObject.Find("Player").GetComponent<Inventory>();
        _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        SavingMenu = GameObject.Find("Saving");
        EscMenu = GameObject.Find("EscMenu");
        EscMenu.SetActive(false);
        SavingMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!IsOpen)
            {
                EscMenuOpen();
            }
            else
            {
                EscMenuClose();
            }
        }
    }

    public void EscMenuClose()
    {
        EscMenu.SetActive(false);
        IsOpen = false;
    }
    public void EscMenuOpen()
    {
        EscMenu.SetActive(true);
        IsOpen = true;
    }

    public void OptionBtn() 
    {
    
    }

    public void SaveBtn()
    {
        StopAllCoroutines();
        SavingMenu.SetActive(true);
        _player.Freze();
        webManager.SaveInventory(WebManager.usersData.UserData.login, CheackItemName(_inventory.items[0]), CheackItemName(_inventory.items[1]), CheackItemName(_inventory.items[2]), CheackItemName(_inventory.items[3]), CheackItemName(_inventory.items[4])
            , CheackItemName(_inventory.items[5]), CheackItemName(_inventory.items[6]), CheackItemName(_inventory.items[7]), CheackItemName(_inventory.items[8]), CheackItemName(_inventory.items[9]), CheackItemName(_inventory.items[10])
            , CheackItemName(_inventory.items[11]), CheackItemName(_inventory.items[12]), CheackItemName(_inventory.items[13]), CheackItemName(_inventory.items[14]), CheackItemName(_inventory.items[15]), CheackItemName(_inventory.items[16])
            , CheackItemName(_inventory.items[17]), CheackItemName(_inventory.items[18]), CheackItemName(_inventory.items[19]), CheackItemName(_inventory.beltItems[0]), CheackItemName(_inventory.beltItems[1]), CheackItemName(_inventory.beltItems[2]), CheackItemName(_inventory.beltItems[3]));
        int sceneId = SceneManager.GetActiveScene().buildIndex;
        webManager.SaveProgress(WebManager.usersData.UserData.login, sceneId);
        webManager.DelOldScene(WebManager.usersData.UserData.login, sceneId);
        StartCoroutine(SaveSceneData());
    }
    private IEnumerator SaveSceneData()
    {
        yield return new WaitForSeconds(1f); // Очікування 1 секунди перед початком збереження об'єктів на сцені
        int sceneId = SceneManager.GetActiveScene().buildIndex;
        Transform[] childTransforms = container.GetComponentsInChildren<Transform>();
        string[] objectNames = new string[childTransforms.Length];
        string[] objectPositionsX = new string[childTransforms.Length];
        string[] objectPositionsY = new string[childTransforms.Length];

        for (int i = 1; i < childTransforms.Length; i++)
        {
            Transform childTransform = childTransforms[i];
            objectNames[i] = childTransform.name;
            objectPositionsX[i] = childTransform.position.x.ToString();
            objectPositionsY[i] = childTransform.position.y.ToString();
            webManager.SaveScene(WebManager.usersData.UserData.login, sceneId, objectNames[i], objectPositionsX[i], objectPositionsY[i]);
            yield return StartCoroutine(Deley()); // Очікування завершення запиту перед наступною ітерацією
        }
        SavingMenu.SetActive(false);
        _player.Move();
    }
    private IEnumerator Deley()
    {
        yield return new WaitForSeconds(1f); // Затримка на 1 секунду для отримання даних з сервера
    }
    public void SaveQuitBtn()
    {
        Destroy(webManager.gameObject);
        SceneManager.LoadScene(0);
    }
    private string CheackItemName(Item Item) 
    {
        if (Item == null)
        {
            return "empty";
        }
        else
        {
            return Item.name;
        }
    }
}
