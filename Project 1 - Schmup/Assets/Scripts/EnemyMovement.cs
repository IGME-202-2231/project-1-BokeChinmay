using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    List<SpriteInfo> asteroids = new List<SpriteInfo>();

    //Position Vector
    Vector3 position;

    //How fast the vehicle will move in units per second
    float speed = 4.0f;

    //Direction Vector
    Vector3 direction = Vector3.down;

    //Velocity Vector
    Vector3 velocity = Vector3.zero;

    [SerializeField]
    EnemySpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        asteroids = spawner.SpawnedAsteroids;

        //Taking the object's original position
        foreach (var asteroid in asteroids)
        {
            position = asteroid.Position;
        }
    }

    // Update is called once per frame
    void Update()
    {

        foreach (SpriteInfo sprite in asteroids) 
        {
            //Velocity is the direction vector times the speed and elapsed time
            velocity = direction * speed * Time.deltaTime;

            //Adding velocity to the position to actually move the object
            position += velocity;
        }
        
    }
}
