using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color AddColorWithEnter;
    private Color _currentAddedColor;
    private Color _clearColor = new Color(1, 1, 1, 1);
    private bool isClear;
    public float Health = 100;
    private PlayerMovement player;
    public GameObject treeLoot;
    private GameObject container;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _currentAddedColor = _clearColor;
        player = FindObjectOfType<PlayerMovement>();
        container = GameObject.Find("NotStaticObj");
    }

    private void FixedUpdate()
    {
        if (isClear) 
        { 
            _currentAddedColor = Color.Lerp(_currentAddedColor, _clearColor, Time.deltaTime);
        }
        spriteRenderer.color = _currentAddedColor;
    }

    public void OnClick()
    {
        if (player.canCutTree == true)
        {
            Health -= 25;
            if (Health <= 0)
            {
                int count = Random.Range(1, 4);
                var ps = transform.position;
                for (int i = 0; i < count; i++)
                {
                    GameObject treeLootInstance = Instantiate(treeLoot, new Vector3(ps.x, ps.y - i, ps.z), Quaternion.identity);
                    treeLootInstance.transform.parent = container.transform; // «м≥на батьк≥вського об'Їкту на контейнер
                }
                Destroy(gameObject);
            }
        }
    }

    public void OnEnter()
    {
        _currentAddedColor = AddColorWithEnter;
        isClear = false;
    }

    public void OnExit()
    {
        isClear = true;
    }
}
