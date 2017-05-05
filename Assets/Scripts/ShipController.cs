﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour
{
    bool isInvincible = false;

    float rotationSpeed = 100.0f;
    float thrustForce = 3f;


    public AudioClip crash;
    public AudioClip shoot;

    public GameObject bullet;

    private GameController gameController;

    void Start()
    {
        // Get a reference to the game controller object and the script
        GameObject gameControllerObject =
            GameObject.FindWithTag("GameController");

        gameController =
            gameControllerObject.GetComponent<GameController>();
}

    void FixedUpdate()
    {

        // Rotate the ship if necessary
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") *
            rotationSpeed * Time.deltaTime);

        // Thrust the ship if necessary
        GetComponent<Rigidbody2D>().
            AddForce(transform.up * thrustForce *
                Input.GetAxis("Vertical"));

        // Has a bullet been fired
        if (Input.GetMouseButtonDown(0))
            ShootBullet();

    }

    void OnTriggerEnter2D(Collider2D c)
    {

        // Anything except a bullet is an asteroid
        if (c.gameObject.tag != "Bullet" && !isInvincible)
        {

            AudioSource.PlayClipAtPoint
                (crash, Camera.main.transform.position);

            // Move the ship to the centre of the screen
            transform.position = new Vector3(0, 0, 0);

            // Remove all velocity from the ship
            GetComponent<Rigidbody2D>().
                velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody2D>().
                rotation = 0;

            //Make ship temporarily invincible
            StartCoroutine(Invincibility(2.0f));

            gameController.DecrementLives();

           

        }
    }

    void ShootBullet()
    {

        // Spawn a bullet
        Instantiate(bullet,
            new Vector3(transform.position.x, transform.position.y, 0),
            transform.rotation);

        // Play a shoot sound
        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
    }

    IEnumerator Invincibility(float dt)
    {
        Animator animator;
        animator = GetComponent<Animator>();

        // Make ship temporarily invincible
        isInvincible = true;
        animator.SetBool("isInvincible", true);

        // suspend execution for 5 seconds
        yield return new WaitForSeconds(dt);

        isInvincible = false;
        animator.SetBool("isInvincible", false);
    }
}