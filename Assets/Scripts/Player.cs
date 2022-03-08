using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PowerUp;

public class Player : MonoBehaviour
{
    private Vector3 movementVector;
    [SerializeField] float speed;
    [SerializeField] int health;

    [SerializeField] Transform aim;
    [SerializeField] Camera mainCamera;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firepointAim;

    bool availableToShoot = true;
    [SerializeField] float fireRate = 1f;

    bool powerShotEnabled;
    bool Invulnerable;

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
        facingDirection = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

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
            GameObject bulletClone = Instantiate(bulletPrefab, transform.position, targetRotation);
            if (powerShotEnabled)
            {
                bulletClone.GetComponent<Bullet>().powerShot = true;
            }
            StartCoroutine(TimeToShoot());
        }
    }

    public void TakeDamage(int damageAmount)
    {

        if (Invulnerable)
            return;


        health -= damageAmount;
        Invulnerable = true;
        StartCoroutine("InactivateInvulneravility");

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


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("PowerUp"))
        {
            PowerUpType powerUpName = collision.GetComponent<PowerUp>().powerUpType;
            switch (powerUpName)
            {
                case PowerUpType.FireRateIncrease:
                    fireRate++;
                    break;
                case PowerUpType.ShotPowerIncrease:
                    powerShotEnabled = true;
                    break;
                case PowerUpType.Health:
                    Debug.Log($"Adding two point to health, current : {health}");
                    health += 2;
                    Debug.Log($"Adding two point to healt, now is {health}");

                    break;
                case PowerUpType.Invulnerable:
                    Invulnerable = true;
                    StartCoroutine("InactivateInvulneravility");
                    break;
            }
            Destroy(collision.gameObject, 0.1f);
        }
    }

    IEnumerator InactivateInvulneravility()
    {
        yield return new WaitForSeconds(3);
        Invulnerable = false;
    }
}
