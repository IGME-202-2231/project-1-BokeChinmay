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

    [SerializeField]
    CollisionManager collisionManager;

    //Position Vector
    Vector3 position;

    //How fast the vehicle will move in units per second
    float speed = 0.7f;

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
        set { spawnedAsteroids = value; }
    }

    public List<Sprite> AsteroidImages
    {
        get { return asteroidImages; }
    }


    // Start is called before the first frame update
    void Start()
    {
        //Height and Width of Scene
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;

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

        for (int i = 0; i < spawnedAsteroids.Count; i++)
        {
            if (spawnedAsteroids[i].transform.position.y < (0 - height / 2))
            {
                Destroy(spawnedAsteroids[i].gameObject);
                spawnedAsteroids.RemoveAt(i);
                i++;
                collisionManager.Multiplier = 1;
            }
        }
    }

    public SpriteInfo SpawnAsteroid()
    {
        return Instantiate(asteroidPrefab);
    }

    public void Spawn()
    {
            float range = Random.Range(1, 11);
            for (int i = 0; i < range; i++)
            {
                spawnedAsteroids.Add(SpawnAsteroid());

                //Set Position

                spawnedAsteroids[i].transform.position = new Vector2(Random.Range(0 - width / 2, 0 + width / 2), 0 + height / 2);
                SpriteRenderer spriteR = spawnedAsteroids[i].gameObject.GetComponent<SpriteRenderer>();

                //Picking one of the two asteroids
                float randValue = Random.Range(1, 101);

                if (randValue <= 20)
                {
                    spriteR.sprite = asteroidImages[0];
                }
                else
                {
                    spriteR.sprite = asteroidImages[1];
                }
            }
    }
}
