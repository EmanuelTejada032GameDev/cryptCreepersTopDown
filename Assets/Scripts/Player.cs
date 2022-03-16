using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PowerUp;

public class Player : MonoBehaviour
{
    private Vector3 movementVector;
    public float speed;
    [SerializeField] int health;

    [SerializeField] Transform aim;
    [SerializeField] Camera mainCamera;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firepointAim;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    bool availableToShoot = true;
    [SerializeField] float fireRate = 1f;

    [SerializeField] AudioClip _powerUpClip;

    bool powerShotEnabled;
    bool Invulnerable;

    [SerializeField] public float blinkRate = 1f;
     private CameraController _cameraController;

    private Vector2 facingDirection;

    public int Health
    {
        get => health;
        set
        {
            health = value;
            UIManager.Instance.UpdateUIhealth(health);
        }
    }

    private void Start()
    {
        UIManager.Instance.UpdateUIhealth(health);
        _cameraController = FindObjectOfType<CameraController>();
    }

    private void Update()
    {
        PlayerMovement();
        shoot();
        ListenForPauseButton();
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
        animator.SetFloat("Speed", movementVector.sqrMagnitude);

        if (aim.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
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


        Health -= damageAmount;
        _cameraController.ShakeCamera();
        Debug.Log("Invulnerable for 3 seconds");
        Invulnerable = true;
        StartCoroutine("DeactivateInvulneravility");

        if (Health <= 0)
        {
            if(FindObjectOfType<EnemySpawner>() != null) FindObjectOfType<EnemySpawner>().gameObject.SetActive(false);
            Destroy(gameObject);
            GameManager.Instance.gameOver = true;
            UIManager.Instance.ShowGameOverScreen();
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
            AudioSource.PlayClipAtPoint(_powerUpClip, transform.position);
            PowerUpType powerUpName = collision.GetComponent<PowerUp>().powerUpType;
            switch (powerUpName)
            {
                case PowerUpType.FireRateIncrease:
                    Debug.Log("Increasing fire rate");
                    fireRate++;
                    break;
                case PowerUpType.ShotPowerIncrease:
                    Debug.Log("Pierce enabled");
                    powerShotEnabled = true;
                    break;
                case PowerUpType.Health:
                    Debug.Log("Health increase +2");
                    Health += 2;
                    break;
                case PowerUpType.Invulnerable:
                    Debug.Log("Invulnerable powerup for 3 seconds");
                    Invulnerable = true;
                    StartCoroutine("DeactivateInvulneravility");
                    break;
            }
            Destroy(collision.gameObject, 0.1f);
        }
    }

    IEnumerator DeactivateInvulneravility()
    {
        StartCoroutine("DamageBlinking");
        yield return new WaitForSeconds(3);
        Debug.Log("Again vulnerable");
        Invulnerable = false;
    }

    IEnumerator DamageBlinking()
    {
        int times = 10;
        while (times > 0)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(times * blinkRate);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(times * blinkRate);
            times--;
        }
    }


    public void ListenForPauseButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           
            UIManager.Instance.ShowPauseMenuScreen();
        }
    }

    
}
