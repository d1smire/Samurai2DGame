using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseOfItems : MonoBehaviour {

    [SerializeField] private GameObject itemObject;
    [SerializeField] private Inventory inventory;
        
    [SerializeField] private PlayerMovement player;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        player = GetComponent<PlayerMovement>();
    }

    public void ItemFunctions(Item item, GameObject itemObj)
    {
        if (item.isFood)
        {
            player.Heal(item.healingPower);
            Inventory.instance.Remove(item, itemObj);
        }
        else if (item.isKatana)
        {
            player.KatanaUsed(item.damage);
        }
        else if (item.isKnife)
        {
            player.canCutMeat = true;
        }
        else if (item.isAxe)
        {
            player.canCutTree = true;
        }
        else if (item.isPickaxe)
        {
            player.canCutStone = true;
        }
    }
    public void ItemUnEquip(Item item, GameObject itemObj)
    {
        if (item.isKatana)
        {
            player.KatanaUnUsed(item.damage);
        }
        else if (item.isKnife)
        {
            player.canCutMeat = false;
        }
        else if (item.isAxe)
        {
            player.canCutTree = false;
        }
        else if (item.isPickaxe)
        {
            player.canCutStone = false;
        }
    }
}
