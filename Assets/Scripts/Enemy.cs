using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    private Transform player;
    [SerializeField] private float speed;
    [SerializeField] int damage;

    private void Start()
    {
        
        if(GameObject.FindGameObjectWithTag("Player"))
        {
            player = FindObjectOfType<Player>().transform;
        }
    }


    private void Update()
    {
        ChasePlayer();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }

    private void ChasePlayer()
    {
        if(player != null)
        {
            Vector2 direction = player.position - transform.position;
            transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;
        }

    }

}
