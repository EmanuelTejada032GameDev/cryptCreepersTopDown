using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeExtenderChekPoint : MonoBehaviour
{
    [SerializeField] int timeAmountToAdd = 8;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.time += timeAmountToAdd;
            Destroy(gameObject, 0.1f);
        }
    }
}
