
using UnityEngine;

public class InventoryUI : MonoBehaviour {
	public Transform inventorySlots;
	Inventory inventory;
	public InventorySlot[] slots;
	public Transform beltSlotsTrans;
	public InventorySlot[] beltSlots;


	void Start () {
		inventory = Inventory.instance;
		inventory.onItemChangedCallBack += UpdateUI;
		inventory.onBeltItemChangedCallBack += UpdateBeltUI;
		slots = inventorySlots.GetComponentsInChildren<InventorySlot> ();
		beltSlots = beltSlotsTrans.GetComponentsInChildren<InventorySlot> ();
	}

	void UpdateBeltUI (Item item, GameObject itemObject, int j, string instruction) {
		if (instruction == "add") {
			beltSlots [j].isWeared = true;
			beltSlots [j].AddItem (item);
			beltSlots [j].AddItemObject (itemObject);
	}
		if (instruction == "remove") {
			beltSlots [j].ClearSlot ();
			beltSlots [j].isWeared = false;
		}
		
	}	

	void UpdateUI (Item item, GameObject itemObject, int i, string instruction) {

		if (instruction == "add") {
			slots [i].AddItem (item);
			slots [i].AddItemObject (itemObject);
	}
		if (instruction == "remove") {
			slots [i].ClearSlot ();
	}
}
}
