using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 movementVector;
    [SerializeField] float speed;

    [SerializeField] Transform aim;
    [SerializeField] Camera camera;


    private Vector2 facingDirection;


    private void Start()
    {
      
    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");
        transform.position += movementVector * speed * Time.deltaTime;

        //Aim movement
        facingDirection = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        // Make sure the distance between player and aim object is the same
        aim.position = transform.position + (Vector3)facingDirection.normalized;


    }
}
