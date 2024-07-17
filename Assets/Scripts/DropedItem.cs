using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedItem : MonoBehaviour
{
    public Item item;
    private GameObject ItemObj;
    [SerializeField] private Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;
        ItemObj = gameObject;
        StartCoroutine(WaitAndAddComponents());
    }

    private IEnumerator WaitAndAddComponents()
    {
        yield return new WaitForSeconds(1);
        gameObject.AddComponent<CircleCollider2D>();
        var circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = 0.5f;
        circleCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (inventory.freeSpace != 0 && inventory.freeSpace <= 20) 
            {
                inventory.Add(item, ItemObj);
                gameObject.SetActive(false); 
            }
        }
    }
}
