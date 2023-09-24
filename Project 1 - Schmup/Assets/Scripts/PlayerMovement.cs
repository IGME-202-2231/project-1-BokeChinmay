using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Position Vector
    Vector3 position;

    //How fast the vehicle will move in units per second
    [SerializeField]
    float speed = 1.0f;

    //Direction Vector
    Vector3 direction = Vector3.right;

    //Velocity Vector
    Vector3 velocity = Vector3.zero;

    //Height and Width of Scene
    Camera cam;
    float height;
    float width;

    //Property
    public Vector3 Direction
    {
        get { return direction; }
        set
        {
            //Making sure the value is not null
            if (value != null)
            {
                direction = value.normalized;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //Taking the object's original position
        position = transform.position;

        //Height and Width of Scene
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        //Velocity is the direction vector times the speed and elapsed time
        velocity = direction * speed * Time.deltaTime;

        //Adding velocity to the position to actually move the object
        position += velocity;

        //Along X-Axis
        if (position.x >= width / 2)
        {
            position.x = -(width / 2);
        }
        else if (position.x <= -(width / 2))
        {
            position.x = width / 2;
        }

        //Along Y-Axis
        if (position.y >= 0)
        {
            position.y = 0;
        }
        else if (position.y <= -(height / 2))
        {
            position.y = -(height / 2);
        }

    }
}
