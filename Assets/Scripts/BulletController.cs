using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    public bool isPlayersBullet;

    public GameObject explosion;
    public GameObject bulletTrail;
    public Transform bulletTrailTransform;

    // Use this for initialization
    void Start()
    {
        // Set the bullet to destroy itself after 1 seconds
        Destroy(gameObject, 1.0f);

        // Push the bullet in the direction it is facing
        GetComponent<Rigidbody2D>()
            .AddForce(transform.up * 40);

        CreateNewTrail();
    }


    void OnTriggerEnter2D(Collider2D c)
    {
        if(isPlayersBullet)
        {
            AsteroidController ac = c.GetComponent<AsteroidController>();
            if (ac != null)
            {
                // Destroy the asteroid
                ac.DestoryAsteroid();

                // Destroy the bullet
                Destroy(gameObject);
            }        
        }
    }

    //Create new bullet trail
    public void CreateNewTrail()
    {
        //Insantiate bullet trail at bullet
        GameObject currentBulletTrail = Instantiate(bulletTrail, transform);
        currentBulletTrail.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        //keep track of the trail's transform
        bulletTrailTransform = currentBulletTrail.transform;
    }

    void OnDestroy()
    {
        Debug.Assert(bulletTrailTransform);

        //Create explosion
        Instantiate(explosion,
            new Vector3(transform.position.x,
                transform.position.y, 0),
                Quaternion.Euler(0, 0, 0));

        bulletTrailTransform.parent = null;
    }
}