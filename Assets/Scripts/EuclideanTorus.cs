using UnityEngine;
using System.Collections;

public class EuclideanTorus : MonoBehaviour
{
    //Borders of game area
    [SerializeField]
    private float right = 9.0f;
    private float left = -9.0f;
    private float top = 6.0f;
    private float bottom = -6.0f;


    // Update is called once per frame
    void Update()
    {

        // Teleport the game object
        if (transform.position.x > right)
        {

            transform.position = new Vector3(left, transform.position.y, 0);

        }
        else if (transform.position.x < left)
        {
            transform.position = new Vector3(right, transform.position.y, 0);
        }

        else if (transform.position.y > top)
        {
            transform.position = new Vector3(transform.position.x, bottom, 0);
        }

        else if (transform.position.y < bottom)
        {
            transform.position = new Vector3(transform.position.x, top, 0);
        }
    }
}