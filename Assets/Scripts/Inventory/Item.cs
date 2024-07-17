using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    public string name = "New item";
    public Sprite icon = null;
    public GameObject prefab;
    public string description = "New item";
    public int healingPower = 0;
    public int damage = 0;
    public bool canUse = false;
    public bool canWear = false;
    public bool canDrop = false;
    public bool isWood = false;
    public bool isStone = false;
    public bool isFood = false;
    public bool isKnife = false;
    public bool isAxe = false;
    public bool isPickaxe = false;
    public bool isKatana = false;
}
