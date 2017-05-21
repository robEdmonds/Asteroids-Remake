using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour
{
    bool isInvincible = false;

    public float invinciblityTimer = 2.0f;
    public float respawnTimer = 2.0f;

    public float rotationSpeed = 100.0f;
    public float thrustForce = 3.0f;

    public AudioClip crash;
    public AudioClip shoot;

    public GameObject bullet;
    public GameObject explosion;

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
        if(!isInvincible)
        {
            // If the object is an enemy bullet
            if (c.gameObject.tag == "Bullet" && !(c.gameObject.GetComponent<BulletController>().isPlayersBullet))
            {
                PlayerDeath();
                Destroy(c.gameObject);
            }
            // If the object is an asteroid
            else if ((c.gameObject.tag == "Large Asteroid") || (c.gameObject.tag == "Small Asteroid"))
            {
                PlayerDeath();
            }
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


    void PlayerDeath()
    {
        AudioSource.PlayClipAtPoint
                (crash, Camera.main.transform.position);

        //Create explosion
        Instantiate(explosion,
            new Vector3(transform.position.x,
                transform.position.y, 0),
                Quaternion.Euler(0, 0, 0))
                .GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;

        gameController.PlayerDeath();
    }

    public void RespawnPlayer()
    {
        // Move the ship to the centre of the screen
        transform.position = new Vector3(0, 0, 0);

        // Remove all velocity from the ship
        GetComponent<Rigidbody2D>().
            velocity = new Vector3(0, 0, 0);
        GetComponent<Rigidbody2D>().
            rotation = 0;

        //Make ship temporarily invincible
        StartCoroutine(Invincibility());

        gameController.DecrementLives();
    }

    // COROUTINES //

    IEnumerator Invincibility()
    {
        Animator animator;
        animator = GetComponent<Animator>();

        // Make ship temporarily invincible
        isInvincible = true;
        animator.SetBool("isInvincible", true);

        // Wait for inviciblity to end
        yield return new WaitForSeconds(invinciblityTimer);

        isInvincible = false;
        animator.SetBool("isInvincible", false);
    }   
}