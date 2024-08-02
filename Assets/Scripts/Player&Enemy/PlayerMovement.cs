using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private int _damage = 5;
    [SerializeField] private int _health = 100;
    [SerializeField] private float _searchRadius = 3f;

    [SerializeField] private Image _healthUI;
    [SerializeField] private Enemy enemy;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private Vector2 direction;
    private bool isAttak = false;
    
    public bool canCutTree = false;
    public bool canCutMeat = false;
    public bool canCutStone = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        FindNearestEnemy();

        if (Input.GetMouseButtonDown(0)) 
        {
            GiveDamage();
        }
        move();
    }
    private void move()
    {
        _animator.SetFloat("Horizontal", direction.x);
        _animator.SetFloat("Vertical", direction.y);
        if (direction.x < 0 && direction.y == 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if(direction.x < 0 && direction.y > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (direction.x < 0 && direction.y < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else 
        {
            _spriteRenderer.flipX = false;
        }
        _rb.MovePosition(_rb.position + direction * _speed * Time.deltaTime);
    }
    public void Heal(int amountheal)
    {
        _health += amountheal;
        healthUI();
    }
    public void GetDamage(int damage)
    {
        _health -= damage;
        healthUI();
    }
    public void healthUI()
    {
        _healthUI.fillAmount = (float)_health / 100;
    }
    public void KatanaUsed(int katanaDmg)
    {
        _damage += katanaDmg;
    }
    public void KatanaUnUsed(int katanaDmg)
    {
        _damage -= katanaDmg;
    }
    public void GiveDamage()
    {
        if (!isAttak)
        {
            _animator.SetBool("Attak", true);
            isAttak = true;
            if (enemy != null)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance <= _searchRadius)
                    enemy.TakeDamage(_damage);
            }
            StartCoroutine(AttakStop());
            
        }
    }

    public void Freze()
    {
        _speed = 0;
    }

    public void Move()
    {
        _speed = 20;
    }

    private IEnumerator AttakStop() 
    {
        yield return new WaitForSeconds(0.8f);
        _animator.SetBool("Attak", false);
        isAttak = false;
    }

    private void FindNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float minDistance = float.MaxValue;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemyObject in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemyObject.transform.position);
            if (distance < minDistance)
            {
                nearestEnemy = enemyObject.GetComponent<Enemy>();
                minDistance = distance;
            }
        }

        enemy = nearestEnemy;
    }

}
