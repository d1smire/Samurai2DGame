using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /*
     1. Зробити рух в зоні 
     4. Зробити 2 види ворогів(Самураї звичайні та з метальною зброєю)/(не об.)
     */
    public Transform target;
    public float Speed = 5f;
    public float chaseSpeedMultiplier = 2f;
    public float detectionRadius = 10f;
    public float exitRadius = 15f;
    public float attackRange = 1f;


    [SerializeField] private int attackDamage = 10;
    [SerializeField] private int health = 100;
    [SerializeField] private float Startspeed;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private GameObject _dropedItem;


    private GameObject container;
    private bool isChasing = false;
    private PlayerMovement player;
    private Vector3 initialPosition;
    private Vector3 currentDestination;

    private void Start()
    {
        container = GameObject.Find("NotStaticObj");
        initialPosition = transform.position;
        currentDestination = initialPosition;
        player = FindObjectOfType<PlayerMovement>();
        Startspeed = Speed;
    }

    private void Update()
    {
        if (IsLive())
        {
            if (isChasing)
            {
                if (target != null)
                {
                    currentDestination = target.position;
                    float distanceToTarget = Vector3.Distance(transform.position, currentDestination);
                    if (distanceToTarget <= attackRange && !isAttacking)
                    {
                        isAttacking = true;
                        Attack();
                    }
                    else if (distanceToTarget > exitRadius)
                    {
                        Speed = Startspeed;
                        isChasing = false;
                    }
                }
                else
                {
                    currentDestination = initialPosition;
                    isChasing = false;
                }
            }
            else
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag("Player"))
                    {
                        target = collider.transform;
                        currentDestination = target.position;
                        isChasing = true;
                        break;
                    }
                }
            }

            Vector3 movementDirection = (currentDestination - transform.position).normalized;
            transform.position += movementDirection * Speed * Time.deltaTime;
        }
        else 
        {
            int count = Random.Range(1, 2);
            var ps = transform.position;
            for (int i = 0; i < count; i++)
            {
                GameObject droppedItemInstance = Instantiate(_dropedItem, new Vector3(ps.x, ps.y - i, ps.z), Quaternion.identity);
                droppedItemInstance.transform.parent = container.transform;
            }
            Destroy(gameObject);
        }
    }

    private void Attack()
    {
        if (isAttacking)
        {
            Speed = 0f;
            if (target != null)
            {
                StartCoroutine(atk());
            }
        }
    }
    private IEnumerator atk()
    {
        yield return new WaitForSeconds(1);
        isAttacking = false;
        player.GetDamage(attackDamage);
        Speed = Startspeed;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private bool IsLive()
    {
        if (health <= 0) 
        {
            return false;
        }
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, exitRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
