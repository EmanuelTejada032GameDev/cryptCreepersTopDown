using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5;
    [SerializeField] int damage;
    void Update()
    {
        transform.position += transform.right  * bulletSpeed * Time.deltaTime;
        Destroy(gameObject, 3f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
        
