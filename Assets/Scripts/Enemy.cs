using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    private Transform player;
    [SerializeField] private float speed;

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
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
            Destroy(gameObject, 0.5f);
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = player.position - transform.position;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

}
