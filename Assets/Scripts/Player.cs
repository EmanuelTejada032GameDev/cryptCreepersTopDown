using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 movementVector;
    [SerializeField] float speed;
    [SerializeField] int health;

    [SerializeField] Transform aim;
    [SerializeField] Camera camera;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firepointAim;

    bool availableToShoot = true;
    [SerializeField] float fireRate = 1f;

    private Vector2 facingDirection;
    

    private void Update()
    {
        PlayerMovement();
        shoot();
    }

    private void PlayerMovement()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");
        transform.position += movementVector.normalized * speed * Time.deltaTime;

        //Aim movement
        facingDirection = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        // Make sure the distance between player and aim object is the same
        aim.position = transform.position + (Vector3)facingDirection.normalized;
    }

    private void shoot()
    {
        if (Input.GetMouseButton(0) && availableToShoot)
        {
            availableToShoot = false;
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(bulletPrefab, transform.position, targetRotation);
            StartCoroutine(TimeToShoot());
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            FindObjectOfType<EnemySpawner>().gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    IEnumerator TimeToShoot()
    {
        yield return new WaitForSeconds(1/fireRate);
        availableToShoot = true;
    }
}
