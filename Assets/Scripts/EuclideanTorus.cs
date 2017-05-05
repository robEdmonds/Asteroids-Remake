using UnityEngine;
using System.Collections;

public class EuclideanTorus : MonoBehaviour
{
    //Borders of game area
    [SerializeField]
    private float right = 9.0f;
    [SerializeField]
    private float left = -9.0f;
    [SerializeField]
    private float top = 6.0f;
    [SerializeField]
    private float bottom = -6.0f;


    // Update is called once per frame
    void Update()
    {

        // Teleport the game object
        if (transform.position.x > right)
        {
            if (tag == "Bullet")
            {
                //Detach Bullet Trail 
                Transform currentBulletTrailTransform = GetComponent<BulletController>().bulletTrailTransform;
                Debug.Assert(currentBulletTrailTransform.IsChildOf(transform));
                currentBulletTrailTransform.parent = null;

                TeleportToLeft();

                //Add new bullet trail in new location
                GetComponent<BulletController>().CreateNewTrail();
            }
            else
            {
                TeleportToLeft();
            }

        }
        else if (transform.position.x < left)
        {
            if (tag == "Bullet")
            {
                //Detach Bullet Trail 
                Transform currentBulletTrailTransform = GetComponent<BulletController>().bulletTrailTransform;
                Debug.Assert(currentBulletTrailTransform.IsChildOf(transform));
                currentBulletTrailTransform.parent = null;

                TeleportToRight();

                //Add new bullet trail in new location
                GetComponent<BulletController>().CreateNewTrail();
            }
            else
            {
                TeleportToRight();
            }
        }

        else if (transform.position.y > top)
        {
            if (tag == "Bullet")
            {
                //Detach Bullet Trail 
                Transform currentBulletTrailTransform = GetComponent<BulletController>().bulletTrailTransform;
                Debug.Assert(currentBulletTrailTransform.IsChildOf(transform));
                currentBulletTrailTransform.parent = null;

                TeleportToBottom();

                //Add new bullet trail in new location
                GetComponent<BulletController>().CreateNewTrail();
            }
            else
            {
                TeleportToBottom();
            }
        }

        else if (transform.position.y < bottom)
        {
            if (tag == "Bullet")
            {
                //Detach Bullet Trail 
                Transform currentBulletTrailTransform = GetComponent<BulletController>().bulletTrailTransform;
                Debug.Assert(currentBulletTrailTransform.IsChildOf(transform));
                currentBulletTrailTransform.parent = null;
                
                TeleportToTop();

                //Add new bullet trail in new location
                GetComponent<BulletController>().CreateNewTrail();
            }
            else
            {
                TeleportToTop();
            }
        }
    }

    //Teleport to left side of game area
    void TeleportToLeft()
    {
        transform.position = new Vector3(left, transform.position.y, 0);
    }

    //Teleport to right side of game area
    void TeleportToRight()
    {
        transform.position = new Vector3(right, transform.position.y, 0);
    }

    //Teleport to bottom of game area
    void TeleportToBottom()
    {
        transform.position = new Vector3(transform.position.x, bottom, 0);
    }

    //Teleport to top of game area
    void TeleportToTop()
    {
        transform.position = new Vector3(transform.position.x, top, 0);
    }

}