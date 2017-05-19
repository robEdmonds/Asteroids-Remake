using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BulletController))]
public class BulletEuclideanTorus : EuclideanTorus
{
    // Update is called once per frame
    void Update()
    {
        // Teleport the game object
        if (transform.position.x > right)
        {
           //Detach Bullet Trail 
           BulletController bc = GetComponent<BulletController>();
           Debug.Assert(bc != null);
           DetachBulletTrail(bc);

           TeleportToLeft();

           //Add new bullet trail in new location
           bc.CreateNewTrail();

        }
        else if (transform.position.x < left)
        {
            //Detach Bullet Trail 
            BulletController bc = GetComponent<BulletController>();
            Debug.Assert(bc != null);
            DetachBulletTrail(bc);

            TeleportToRight();

            //Add new bullet trail in new location
            bc.CreateNewTrail();
        }

        else if (transform.position.y > top)
        {       
            //Detach Bullet Trail 
            BulletController bc = GetComponent<BulletController>();
            Debug.Assert(bc != null);
            DetachBulletTrail(bc);

            TeleportToBottom();

            //Add new bullet trail in new location
            bc.CreateNewTrail();
        }

        else if (transform.position.y < bottom)
        {
            //Detach Bullet Trail 
            BulletController bc = GetComponent<BulletController>();
            Debug.Assert(bc != null);
            DetachBulletTrail(bc);

            TeleportToTop();

            //Add new bullet trail in new location
            bc.CreateNewTrail();
        }
    }

    void DetachBulletTrail(BulletController _bc)
    {
        Transform currentBulletTrailTransform = _bc.bulletTrailTransform;
        Debug.Assert(currentBulletTrailTransform != null);
        Debug.Assert(currentBulletTrailTransform.IsChildOf(transform));
        currentBulletTrailTransform.parent = null;
    }

}