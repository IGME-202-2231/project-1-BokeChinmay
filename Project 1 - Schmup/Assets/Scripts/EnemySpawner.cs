using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    SpriteInfo asteroidPrefab;

    [SerializeField]
    List<Sprite> asteroidImages = new List<Sprite>();
    List<SpriteInfo> spawnedAsteroids = new List<SpriteInfo>();

    //Position Vector
    Vector3 position;

    //How fast the vehicle will move in units per second
    float speed = 4.0f;

    //Direction Vector
    Vector3 direction = Vector3.down;

    //Velocity Vector
    Vector3 velocity = Vector3.zero;

    //Height and Width of Scene
    Camera cam;
    float height;
    float width;


    //Property
    public List<SpriteInfo> SpawnedAsteroids
    {
        get { return spawnedAsteroids; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Height and Width of Scene
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;

        //Taking the object's original position
        foreach (var asteroid in spawnedAsteroids)
        {
            position = transform.position;
        }

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnedAsteroids.Count == 0) 
        {
            Spawn();
        }

        foreach (SpriteInfo sprite in spawnedAsteroids)
        {
            //Velocity is the direction vector times the speed and elapsed time
            velocity = direction * speed * Time.deltaTime;

            //Adding velocity to the position to actually move the object
            sprite.transform.position += velocity;
        }
    }

    public SpriteInfo SpawnAsteroid()
    {
        return Instantiate(asteroidPrefab);
    }

    public void Spawn()
    {
        //DestroyAsteroids();

        for (int i = 0; i < Random.Range(1, 20); i++)
        {
            spawnedAsteroids.Add(SpawnAsteroid());

            //Set Position
            spawnedAsteroids[i].transform.position = new Vector2(Random.Range(0 - width / 2, width), height);

            //Picking one of the two asteroids
            float randValue = Random.Range(1, 101);

            if (randValue <= 20)
            {
                spawnedAsteroids[i].Renderer.sprite = asteroidImages[0];
            }
            else
            {
                spawnedAsteroids[i].Renderer.sprite = asteroidImages[1];
            }
        }
    }

    public void DestroyAsteroids()
    {
        foreach (SpriteInfo asteroid in spawnedAsteroids)
        {
            Destroy(asteroid.gameObject);
        }

        spawnedAsteroids.Clear();
    }
}
