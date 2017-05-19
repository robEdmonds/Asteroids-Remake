using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour {

    public float speed;
    public float lifeSpan;

    public AudioClip crash;
    public AudioClip shoot;

    public GameObject bullet;
    public float bulletCooldown;
    private float timeToShoot = 0.0f;
    public float maxRangeToAttack;

    private GameController gameController;

    // Use this for initialization
    void Start () {
        // Get a reference to the game controller object and the script
       // GameObject gameControllerObject =
        //    GameObject.FindWithTag("GameController");

       // gameController =
       //     gameControllerObject.GetComponent<GameController>();

        // Destory enemy after the end of its lifespan
        Destroy(gameObject, lifeSpan);
    }
	
	// Update is called once per frame
	void Update () {
        if (timeToShoot <= 0)
        {
            //Debug.Log("can shoot");
            if (WithinRangeToAttack())
            {
                ShootBullet();
                // Reset bullet cooldown
                timeToShoot += bulletCooldown;
                timeToShoot -= Time.deltaTime;
            }
            return;        
        }
            timeToShoot -= Time.deltaTime;    
    }


    void OnTriggerEnter2D(Collider2D c)
    {
        // if the player's bullet hits the enemy
        if (c.gameObject.tag == "Bullet")
        {
            Debug.Assert(c.gameObject.GetComponent<BulletController>());
            if (c.gameObject.GetComponent<BulletController>().isPlayersBullet)
            {
                // Destroy the bullet
                Destroy(c.gameObject);

                EnemyDeath();
            }
        }
    }

    bool WithinRangeToAttack()
    {
        GameObject player = GameObject.FindWithTag("Ship");
        Vector3 offset = player.transform.position - transform.position;
        float sqrLen = offset.sqrMagnitude;
        if (sqrLen < maxRangeToAttack * maxRangeToAttack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ShootBullet()
    {
        // Find bullet target and its direction
        GameObject player = GameObject.FindWithTag("Ship");
        Vector3 targetDirection = (player.transform.position - transform.position).normalized;

        // Determine the direction the bullet will fire
        Vector2 RandomVector = Random.insideUnitCircle * 0.2f;
        Quaternion bulletRotation = new Quaternion();
        bulletRotation.SetFromToRotation(new Vector3(0.0f, 1.0f, 0.0f) , 
                (targetDirection + new Vector3(RandomVector.x, RandomVector.y)).normalized);

        // Spawn a bullet
        Instantiate(bullet,
            new Vector3(transform.position.x, transform.position.y, 0),
             bulletRotation);

        // Play a shoot sound
        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
    }

    void EnemyDeath()
    {
        AudioSource.PlayClipAtPoint
                (crash, Camera.main.transform.position);



        Destroy(gameObject);
    }
}