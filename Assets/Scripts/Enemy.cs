using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    private Transform player;
    [SerializeField] public float speed;
    [SerializeField] int damage;
    private int scorePoints = 50;


    [SerializeField] private Animator _animator;
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
            GameManager.Instance.Score+=scorePoints;
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
            _animator.SetFloat("Speed", direction.sqrMagnitude);
            _animator.SetFloat("Horizontal", direction.x);
            _animator.SetFloat("Vertical", direction.y);
        }

    }

}
