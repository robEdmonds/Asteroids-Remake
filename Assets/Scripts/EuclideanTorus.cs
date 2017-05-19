using UnityEngine;
using System.Collections;

public class EuclideanTorus : MonoBehaviour
{
    //Borders of game area
    [SerializeField]
    protected float right = 9.0f;
    [SerializeField]
    protected float left = -9.0f;
    [SerializeField]
    protected float top = 6.0f;
    [SerializeField]
    protected float bottom = -6.0f;


    // Update is called once per frame
    void Update()
    {

        // Teleport the game object
        if (transform.position.x > right)
        {
            if (tag == "Bullet")
            {
                //Detach Bullet Trail 
                BulletController bc = GetComponent<BulletController>();
                Debug.Assert(bc != null);

                Transform currentBulletTrailTransform = bc.bulletTrailTransform;
                Debug.Assert(currentBulletTrailTransform != null);
                Debug.Assert(currentBulletTrailTransform.IsChildOf(transform));

                currentBulletTrailTransform.parent = null;
                
                TeleportToLeft();
                
                //Add new bullet trail in new location
                bc.CreateNewTrail();

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
                BulletController bc = GetComponent<BulletController>();
                Debug.Assert(bc != null);
                Transform currentBulletTrailTransform = bc.bulletTrailTransform;
                Debug.Assert(currentBulletTrailTransform != null);
                Debug.Assert(currentBulletTrailTransform.IsChildOf(transform));
                currentBulletTrailTransform.parent = null;

                TeleportToRight();

                //Add new bullet trail in new location
                bc.CreateNewTrail();
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
                BulletController bc = GetComponent<BulletController>();
                Debug.Assert(bc != null);
                Transform currentBulletTrailTransform = bc.bulletTrailTransform;
                Debug.Assert(currentBulletTrailTransform != null);
                Debug.Assert(currentBulletTrailTransform.IsChildOf(transform));
                currentBulletTrailTransform.parent = null;

                TeleportToBottom();

                //Add new bullet trail in new location
                bc.CreateNewTrail();
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
                BulletController bc = GetComponent<BulletController>();
                Debug.Assert(bc != null);
                Transform currentBulletTrailTransform = bc.bulletTrailTransform;
                Debug.Assert(currentBulletTrailTransform != null);
                Debug.Assert(currentBulletTrailTransform.IsChildOf(transform));
                currentBulletTrailTransform.parent = null;

                TeleportToTop();

                //Add new bullet trail in new location
                bc.CreateNewTrail();
            }
            else
            {
                TeleportToTop();
            }
        }
    }

    //Teleport to left side of game area
    protected void TeleportToLeft()
    {
        transform.position = new Vector3(left, transform.position.y, 0);
    }

    //Teleport to right side of game area
    protected void TeleportToRight()
    {
        transform.position = new Vector3(right, transform.position.y, 0);
    }

    //Teleport to bottom of game area
    protected void TeleportToBottom()
    {
        transform.position = new Vector3(transform.position.x, bottom, 0);
    }

    //Teleport to top of game area
    protected void TeleportToTop()
    {
        transform.position = new Vector3(transform.position.x, top, 0);
    }

}