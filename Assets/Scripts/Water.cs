using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    private float originalSpeed;
    [SerializeField] private float speedRateReduction = 0.5f;
    private Player player;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
        originalSpeed = player.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.speed *= speedRateReduction; 

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.speed = originalSpeed;
        }
    }
}
