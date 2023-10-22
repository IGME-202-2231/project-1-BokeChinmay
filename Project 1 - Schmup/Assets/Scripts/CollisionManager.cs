using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    SpriteInfo playerCollidable = new SpriteInfo();

    List<SpriteInfo> enemyCollidables = new List<SpriteInfo>();

    List<SpriteInfo> projectileCollidables = new List<SpriteInfo>();

    [SerializeField]
    EnemySpawner spawner;

    [SerializeField]
    PlayerFire projectiles;

    [SerializeField]
    SceneSwitcher sceneSwitcher;

    [SerializeField]
    Text healthBar;

    [SerializeField]
    Text playerScore;

    Camera cam;
    float height;
    float width;

    //Score
    int score = 0;
    int multiplier = 1;
    int health = 3;

    //End 
    bool end = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyCollidables = spawner.SpawnedAsteroids;
        projectileCollidables = projectiles.SpawnedProjectiles;

        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;

        if(enemyCollidables.Count > 0 )
        {
            playerEnemy();
            projectileEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerEnemy();
        projectileEnemy();

        if(health <= 0)
        {
            sceneSwitcher.LoadScene();
        }
    }

    bool AABBCheck(SpriteInfo spriteA, SpriteInfo spriteB)
    {
        //Check for collision
        if (
            (spriteB.RectMin.x < spriteA.RectMax.x) &&
            (spriteB.RectMax.x > spriteA.RectMin.x) &&
            (spriteB.RectMin.y < spriteA.RectMax.y) &&
            (spriteB.RectMax.y > spriteA.RectMin.y)
           )
        {
            return true;
        }

        return false;
    }
    void playerEnemy()
    {
        for (int i = enemyCollidables.Count - 1; i >= 0; i--)
        {
            if(spawner.SpawnedAsteroids.Count != 0)
            {
                SpriteInfo asteroid = spawner.SpawnedAsteroids[i].GetComponent<SpriteInfo>();

                if (AABBCheck(playerCollidable, asteroid))
                {
                    GameObject enemy = spawner.SpawnedAsteroids[i].gameObject;

                    spawner.SpawnedAsteroids.RemoveAt(i);
                    Destroy(enemy);

                    health--;
                    healthBar.text = "Health: " + health;
                }
            }
        }
    }

    void projectileEnemy()
    {
        for (int i = spawner.SpawnedAsteroids.Count - 1; i >= 0; i--)
        {
            for (int j = projectiles.SpawnedProjectiles.Count - 1; j >= 0; j--)
            {
                if(spawner.SpawnedAsteroids.Count != 0 && projectiles.SpawnedProjectiles.Count != 0)
                {
                    SpriteInfo asteroid = spawner.SpawnedAsteroids[i].GetComponent<SpriteInfo>(); 
                    SpriteInfo projectile = projectiles.SpawnedProjectiles[j].GetComponent<SpriteInfo>();

                    if (AABBCheck(projectile, asteroid))
                    {
                        GameObject shot = projectiles.SpawnedProjectiles[j].gameObject;
                        GameObject enemy = spawner.SpawnedAsteroids[i].gameObject;


                        //projectiles.SpawnedProjectiles.RemoveAt(j);

                        //spawner.SpawnedAsteroids.RemoveAt(i);

                        //Destroy(projectileCollidables[j].gameObject);
                        //Destroy(projectiles.SpawnedProjectiles[j].gameObject);
                        //Destroy(enemyCollidables[i].gameObject);
                        //Destroy(spawner.SpawnedAsteroids[i].gameObject);

                        projectiles.SpawnedProjectiles.RemoveAt(j);
                        spawner.SpawnedAsteroids.RemoveAt(i);
                        Destroy(shot);
                        Destroy(enemy);

                        multiplier++;
                        score = score * multiplier;
                        playerScore.text = "Score: " + score;
                    }
                    /*
                    else
                    {
                        enemyCollidables[i].IsColliding = false;
                        projectileCollidables[j].IsColliding = false;
                    }
                    */
                }

                if (spawner.SpawnedAsteroids[i].transform.position.y < (0 - height / 2))
                {
                    Destroy(spawner.SpawnedAsteroids[i].gameObject);
                    spawner.SpawnedAsteroids.RemoveAt(i);

                    multiplier = 1;
                }
            }
                
        }
    }
}
