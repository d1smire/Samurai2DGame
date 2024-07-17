using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private int sortingorder;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        sortingorder = _renderer.sortingOrder;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var obj = collision.gameObject.GetComponent<SpriteRenderer>();
            _renderer.sortingOrder = (int)(sortingorder + obj.sortingOrder);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _renderer.sortingOrder = (int)sortingorder;
    }
}
