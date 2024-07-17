using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	public static Inventory instance;
	public int space = 20;
	public int freeSpace = 20;

	public Item[] items;
	public GameObject[] itemObjects;
	public Item[] beltItems;
	public GameObject[] beltItemObjects;
	public delegate void OnItemChanged(Item item, GameObject itemObject, int i, string d);
	public OnItemChanged onItemChangedCallBack;
	public delegate void OnBeltItemChanged();
	public OnItemChanged onBeltItemChangedCallBack;

	   #region Singleton
	void Awake() {
		if (instance != null) {

			Debug.LogWarning ("There is more than one instance of an inventory");
			return;
		}
		instance = this;

	}
	#endregion

	public bool Add(Item item, GameObject itemObject) {

		for (int j = 0; j < space; j++) {
			if (items[j] == null && itemObjects[j] == null) {
				items [j] = item;
				itemObjects [j] = itemObject;
				freeSpace--;
				if (onItemChangedCallBack != null) {
					onItemChangedCallBack.Invoke (item, itemObject, j, "add");
				}
				return true;

			} else	

				continue;
		}
		return false;	
	}

	public void AddOnBelt(Item beltItem, GameObject beltItemObject) {
		//if (beltItems.Length > 4)
		//{

		//}
		for (int i = 0; i < 4; i++) {
			if (beltItems [i] == null && beltItemObjects[i] == null) {
				beltItems [i] = beltItem;
				beltItemObjects [i] = beltItemObject;
				Remove (beltItem, beltItemObject);
				if (onBeltItemChangedCallBack != null) {
					onBeltItemChangedCallBack.Invoke (beltItem, beltItemObject, i, "add");
				}
				return;
			}
		}
	}

	public void Remove(Item item, GameObject obj) {
		for (int i = 0; i < space; i++) {
			if (item == items [i] && obj == itemObjects [i]) {
				items [i] = null;
				itemObjects [i] = null;
				freeSpace++;
				if (onItemChangedCallBack != null) {
					onItemChangedCallBack.Invoke (item, obj, i, "remove");
				}
				return;
			}

		}
	}

	public void removeFromBelt (Item item, GameObject itemObj) {
		for (int j = 0; j < beltItemObjects.Length; j++)
		{
			if (itemObj == beltItemObjects[j])
			{
				beltItems[j] = null;
				beltItemObjects[j] = null;
				if (onBeltItemChangedCallBack != null)
					onBeltItemChangedCallBack.Invoke(item, itemObj, j, "remove");
			}
		}
		return;
	}
}