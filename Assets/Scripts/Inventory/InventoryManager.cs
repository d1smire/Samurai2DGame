using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private GameObject Inventory;
    private Inventory Inventorys;
    private InventoryData inventoryData;
    [SerializeField] private DataLoader dataLoader;
    private bool IsOpen = false;
    private GameObject container;

    private void Start()
    {
        if (WebManager.usersData.ProgressData.id == 0)
        {
            Inventory = GameObject.Find("Inventory");
            Inventorys = GameObject.Find("Player").GetComponent<Inventory>();
            Inventory.SetActive(false);
        }
        else
        {
            dataLoader = GameObject.Find("WebManager").GetComponent<DataLoader>();
            Inventory = GameObject.Find("Inventory");
            container = GameObject.Find("NotStaticObj");
            Inventorys = GameObject.Find("Player").GetComponent<Inventory>();
            Inventory.SetActive(false);
            UpdateInventory();
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            if (!IsOpen)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }
    public void UpdateInventory()
    {
        // Отримати дані інвентаря з WebManager.usersData
        inventoryData = WebManager.usersData.InventoryData;

        ItemByTypeInv(inventoryData.Slot1); ItemByTypeInv(inventoryData.Slot2); ItemByTypeInv(inventoryData.Slot3);
        ItemByTypeInv(inventoryData.Slot4); ItemByTypeInv(inventoryData.Slot5); ItemByTypeInv(inventoryData.Slot6);
        ItemByTypeInv(inventoryData.Slot7); ItemByTypeInv(inventoryData.Slot8); ItemByTypeInv(inventoryData.Slot9);
        ItemByTypeInv(inventoryData.Slot10); ItemByTypeInv(inventoryData.Slot11); ItemByTypeInv(inventoryData.Slot12);
        ItemByTypeInv(inventoryData.Slot13); ItemByTypeInv(inventoryData.Slot14); ItemByTypeInv(inventoryData.Slot15);
        ItemByTypeInv(inventoryData.Slot16); ItemByTypeInv(inventoryData.Slot17); ItemByTypeInv(inventoryData.Slot18);
        ItemByTypeInv(inventoryData.Slot19); ItemByTypeInv(inventoryData.Slot20);
        ItemByTypeOnBelt(inventoryData.BeltSlot1); ItemByTypeOnBelt(inventoryData.BeltSlot2);
        ItemByTypeOnBelt(inventoryData.BeltSlot3); ItemByTypeOnBelt(inventoryData.BeltSlot4);
    }
    private void ItemByTypeInv(string itemType)
    {
        switch (itemType)
        {
            case "Axe":
                GameObject axeCopy = Instantiate(dataLoader.itemObjects[0], container.transform);
                axeCopy.transform.position = Vector3.zero;
                axeCopy.SetActive(false);
                Inventorys.Add(dataLoader.ObjectsInfo[0], axeCopy);
                break;
            case "Pickaxe":
                GameObject pickaxeCopy = Instantiate(dataLoader.itemObjects[1], container.transform);
                pickaxeCopy.transform.position = Vector3.zero;
                pickaxeCopy.SetActive(false);
                Inventorys.Add(dataLoader.ObjectsInfo[1], pickaxeCopy);
                break;
            case "Knife":
                GameObject knifeCopy = Instantiate(dataLoader.itemObjects[2], container.transform);
                knifeCopy.transform.position = Vector3.zero;
                knifeCopy.SetActive(false);
                Inventorys.Add(dataLoader.ObjectsInfo[2], knifeCopy);
                break;
            case "Stone":
                GameObject stoneCopy = Instantiate(dataLoader.itemObjects[3], container.transform);
                stoneCopy.transform.position = Vector3.zero;
                stoneCopy.SetActive(false);
                Inventorys.Add(dataLoader.ObjectsInfo[3], stoneCopy);
                break;
            case "Wood":
                GameObject woodCopy = Instantiate(dataLoader.itemObjects[4], container.transform);
                woodCopy.transform.position = Vector3.zero;
                woodCopy.SetActive(false);
                Inventorys.Add(dataLoader.ObjectsInfo[4], woodCopy);
                break;
            case "Meat":
                GameObject meatCopy = Instantiate(dataLoader.itemObjects[5], container.transform);
                meatCopy.transform.position = Vector3.zero;
                meatCopy.SetActive(false);
                Inventorys.Add(dataLoader.ObjectsInfo[5], meatCopy);
                break;
            case "empty":
                break;
        }
    }

    private void ItemByTypeOnBelt(string itemType)
    {
        switch (itemType)
        {
            case "Axe":
                GameObject axeCopy = Instantiate(dataLoader.itemObjects[0], container.transform);
                axeCopy.transform.position = Vector3.zero;
                axeCopy.SetActive(false);
                Inventorys.AddOnBelt(dataLoader.ObjectsInfo[0], axeCopy);
                break;
            case "Pickaxe":
                GameObject pickaxeCopy = Instantiate(dataLoader.itemObjects[1], container.transform);
                pickaxeCopy.transform.position = Vector3.zero;
                pickaxeCopy.SetActive(false);
                Inventorys.AddOnBelt(dataLoader.ObjectsInfo[1], pickaxeCopy);
                break;
            case "Knife":
                GameObject knifeCopy = Instantiate(dataLoader.itemObjects[2], container.transform);
                knifeCopy.transform.position = Vector3.zero;
                knifeCopy.SetActive(false);
                Inventorys.AddOnBelt(dataLoader.ObjectsInfo[2], knifeCopy);
                break;
            case "empty":
                break;
        }
    }
    public void CloseInventory() 
    {
        Inventory.SetActive(false);
        IsOpen = false;
    }
    public void OpenInventory() 
    {
        Inventory.SetActive(true);
        IsOpen = true;
    }
}
