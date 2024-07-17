using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
	UseOfItems itemUse;
	public Item item;
	GameObject ItemInfo;
	public GameObject ObjectInSlot;
	bool infoShown = false;
	public bool SlotClicked = false;
	public Transform InventorySlots;
	public InventorySlot[] slots;
	public Transform BeltSlots;
	public InventorySlot[] beltSlots;
	public Image icon;
	public bool isWeared;
    GameObject infofh;
	Image info;
	Image InfoIcon;
	Text description;
	Image UnEquipButton;
	Image UseButton;
	Image WearButton;
	Image DropButton;
	GameObject Player;

	void Start () {
		InfoIcon = GameObject.Find ("ItemIcon").GetComponent<Image>();
		info = GameObject.Find ("ItemInfo").GetComponent<Image> ();
		infofh = GameObject.Find("ItemInfo");
		Player = GameObject.Find ("Player");
		UseButton = GameObject.Find ("UseButton").GetComponent<Image>();
		WearButton = GameObject.Find ("WearButton").GetComponent<Image>();
		DropButton = GameObject.Find ("DropButton").GetComponent<Image>();
		UnEquipButton = GameObject.Find ("UnEquipButton").GetComponent<Image> ();
		description = GameObject.Find ("Description").GetComponent<Text>();
		itemUse = Player.GetComponent<UseOfItems> ();
		slots = InventorySlots.GetComponentsInChildren<InventorySlot> ();
		beltSlots = BeltSlots.GetComponentsInChildren<InventorySlot> ();
		UnEquipButton.enabled = false;
		hideInfo();
	}
		

	public void showInfo() {			
		
		if (item != null) {
			for (int i = 0; i < slots.Length; i++) 
			{
				if (slots[i].infoShown)
					slots [i].hideInfo();
			}

			for (int i = 0; i < beltSlots.Length; i++) 
			{
				if (beltSlots [i].infoShown) 
				{                  
					beltSlots [i].hideInfo();
				}
			}


			SlotClicked = true;

			if (isWeared)
				unEquipButtonAppear ();
			else
				EquipButtonAppear ();

            if (item.canUse)
            {
                UseButton.enabled = true;
                //UseButton.image.enabled = true;
            }

            if (item.canWear)
                WearButton.enabled = true;
			else
				WearButton.enabled = false;

			if (item.canDrop)
            {
                DropButton.enabled = true;
                //DropButton.image.enabled = true;
            }


            InfoIcon.sprite = item.icon;
			InfoIcon.enabled = true;
            description.text = item.description;
            description.enabled = true;

			info.enabled = true;
			infoShown = true;
		}
	}

	public void hideInfo() 
	{			
		UseButton.enabled = false;
		WearButton.enabled = false;
		DropButton.enabled = false;
		//UseButton.image.enabled = false;
		//WearButton.image.enabled = false;
		//DropButton.image.enabled = false;
		description.enabled = false;
		InfoIcon.enabled = false;
		info.enabled = false;
		infoShown = false;
		SlotClicked = false;
	}

	public void AddItem(Item newItem) 
	{
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
	}

	public void AddItemObject(GameObject itemObj) 
	{
		ObjectInSlot = itemObj;
	}

	public void ClearSlot() 
	{
		item = null;
		icon.sprite = null;
		icon.enabled = false;
	}

	public void UseItem() 
	{
		if (SlotClicked) 
		{
            itemUse.ItemFunctions(item, ObjectInSlot);
            hideInfo ();
		}
	}

	public void addOnBelt() 
	{			
		if (SlotClicked) 
		{
            itemUse.ItemFunctions(item, ObjectInSlot);
            Inventory.instance.AddOnBelt (item, ObjectInSlot);
			Inventory.instance.Remove (item, ObjectInSlot);
			ObjectInSlot = null;
			item = null;
			hideInfo ();
		}
	}

	public void DropItem() 
	{
		if (SlotClicked) 
		{
			ObjectInSlot.transform.position = Player.transform.position + new Vector3(2,0,0);
			ObjectInSlot.SetActive(true);
			Inventory.instance.Remove (item, ObjectInSlot);
            ObjectInSlot = null;
			hideInfo();
		}
    }

	public void unEquipButtonAppear() 
	{
		if (item != null) 
		{
			WearButton.enabled = false;
			UnEquipButton.enabled = true;
		}
	}

	public void EquipButtonAppear() 
	{
		if (item != null) 
		{
			WearButton.enabled = true;
			UnEquipButton.enabled = false;
		}
	}

	public void unEquip () 
	{							
		if (SlotClicked) 
		{
			UnEquipButton.enabled = false;
			itemUse.ItemUnEquip(item,ObjectInSlot);	
			Inventory.instance.Add (item, ObjectInSlot);
			Inventory.instance.removeFromBelt (item, ObjectInSlot);
			ObjectInSlot = null;
			ClearSlot ();
			hideInfo ();
		}
	}

}
