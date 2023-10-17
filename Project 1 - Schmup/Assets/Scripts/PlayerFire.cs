using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    SpriteInfo projectilePrefab;

    [SerializeField]
    SpriteInfo playerObject;

    List<SpriteInfo> spawnedProjectiles = new List<SpriteInfo>();

    //Position Vector
    Vector3 position;

    //How fast the vehicle will move in units per second
    float speed = 3.0f;

    //Direction Vector
    Vector3 direction = Vector3.up;

    //Velocity Vector
    Vector3 velocity = Vector3.zero;

    Camera cam;
    float height;
    float width;

    //Property 
    public List<SpriteInfo> SpawnedProjectiles
    {
        get { return spawnedProjectiles; }
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Spawn();
        }

        for (int i = 0; i < spawnedProjectiles.Count; i++)
        {
            velocity = direction * speed * Time.deltaTime;
            spawnedProjectiles[i].transform.position += velocity;

            /*
            if (spawnedProjectiles[i].transform.position.y >= height)
            {
                Destroy(spawnedProjectiles[i].gameObject);
            }
            */
        }

    }

    public SpriteInfo SpawnProjectile()
    {
        return Instantiate(projectilePrefab,
                           new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + 1, -1),
                           Quaternion.identity);
    }

    public void Spawn()
    {
        SpriteInfo newProjectile = SpawnProjectile();
        spawnedProjectiles.Add(newProjectile);

        newProjectile.transform.Rotate(new Vector3(0, 0, 90));

        /*
        for (int i = 0; i < 5; i++)
        {
            velocity = direction * speed * Time.deltaTime;
            newProjectile.transform.position += velocity;
        }
        */
        /*
        while(newProjectile.transform.position.y <= height)
        {
            velocity = direction * speed * Time.deltaTime;
            newProjectile.transform.position += velocity;
        }
        */
       
    }
}
