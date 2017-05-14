using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidController : MonoBehaviour
{

    public AudioClip destroy;
    public GameObject smallAsteroid;
    public GameObject DustEmitter;

    private GameController gameController;

    // Use this for initialization
    void Start()
    {

        // Get a reference to the game controller object and the script
        GameObject gameControllerObject =
            GameObject.FindWithTag("GameController");

        gameController =
            gameControllerObject.GetComponent<GameController>();

        // Push the asteroid in the direction it is facing
        GetComponent<Rigidbody2D>()
            .AddForce(transform.up * Random.Range(-50.0f, 150.0f) * GetComponent<Rigidbody2D>().mass);

        // Give a random angular velocity/rotation
        GetComponent<Rigidbody2D>()
            .angularVelocity = Random.Range(-0.0f, 90.0f);

    }

    void OnCollisionEnter2D(Collision2D c)
    {

        if (c.gameObject.tag.Equals("Bullet") && c.gameObject.GetComponent<BulletController>().isPlayersBullet)
        {
            // Destroy the bullet
            Destroy(c.gameObject);

            // If large asteroid spawn new ones
            if (tag.Equals("Large Asteroid"))
            {
                // Spawn small asteroids
                Instantiate(smallAsteroid,
                    new Vector3(transform.position.x - .55f,
                        transform.position.y - .31f, 0),
                        Quaternion.Euler(0, 0, 161.2f))
                        .GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;

                // Spawn small asteroids
                Instantiate(smallAsteroid,
                    new Vector3(transform.position.x + .57f,
                        transform.position.y + .37f, 0),
                        Quaternion.Euler(0, 0, 0))
                        .GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;

                // Spawn small asteroids
                Instantiate(smallAsteroid,
                    new Vector3(transform.position.x + .23f,
                        transform.position.y - .51f, 0),
                        Quaternion.Euler(0, 0, 307))
                        .GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;

                gameController.SplitAsteroid(); // +2

            }
            else
            {
                // Just a small asteroid destroyed
                gameController.DecrementAsteroids();
            }

            // Play a sound
            AudioSource.PlayClipAtPoint(
                destroy, Camera.main.transform.position);

            //Create dust emitter
            Instantiate(DustEmitter,
                new Vector3(transform.position.x,
                    transform.position.y, 0),
                    Quaternion.Euler(0, 0, 0))
                    .GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;


            // Add to the score
            gameController.IncrementScore();

            // Destroy the current asteroid
            Destroy(gameObject);
        }
    }
}