using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer asteroidPrefab;

    [SerializeField]
    List<Sprite> asteroidImages = new List<Sprite>();

    List<SpriteInfo> spawnedAsteroids = new List<SpriteInfo>();

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
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnedAsteroids.Count == 0) 
        {
            Spawn();
        }
    }

    public SpriteRenderer SpawnAsteroid()
    {
        return Instantiate(asteroidPrefab);

    }

    public void Spawn()
    {
        DestroyAsteroids();

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
