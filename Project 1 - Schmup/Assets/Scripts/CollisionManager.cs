using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    Camera cam;
    float height;
    float width;

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
        bool newIteration;
        do
        {
            newIteration = false;
            if(enemyCollidables.Count > 0)
            {
                //newIteration = false;
                for (int i = 0; i < enemyCollidables.Count; i++)
                {
                    if ((enemyCollidables[i].IsColliding = AABBCheck(playerCollidable, enemyCollidables[i])))
                    {
                        playerCollidable.IsColliding = true;
                        Destroy(enemyCollidables[i].gameObject);
                        Destroy(spawner.SpawnedAsteroids[i].gameObject);
                        enemyCollidables.Remove(enemyCollidables[i]);
                        spawner.SpawnedAsteroids.Remove(spawner.SpawnedAsteroids[i]);
                        newIteration = true;
                        break;
                    }
                    else
                    {
                        playerCollidable.IsColliding = false;
                    }
                }
            }
            
        }
        while (newIteration != true);
    }

    void projectileEnemy()
    {
        bool newIteration = true;
        do
        {
            if (enemyCollidables.Count > 0)
            {
                //newIteration = false;
                for (int i = 0; i < enemyCollidables.Count; i++)
                {
                    for (int j = 0; j < projectileCollidables.Count; j++)
                    {
                        if ((enemyCollidables[i].IsColliding = AABBCheck(projectileCollidables[j], enemyCollidables[i])))
                        {
                            projectileCollidables[j].IsColliding = true;
                            Destroy(projectileCollidables[j].gameObject);
                            Destroy(projectiles.SpawnedProjectiles[j].gameObject);
                            Destroy(enemyCollidables[i].gameObject);
                            Destroy(spawner.SpawnedAsteroids[i].gameObject);
                            projectileCollidables.Remove(projectileCollidables[j]);
                            projectiles.SpawnedProjectiles.Remove(projectiles.SpawnedProjectiles[j]);
                            enemyCollidables.Remove(enemyCollidables[i]);
                            spawner.SpawnedAsteroids.Remove(spawner.SpawnedAsteroids[i]);
                            newIteration = true;
                            break;
                        }
                        else if (projectileCollidables[j].transform.position.y > height)
                        {
                            projectileCollidables[j].IsColliding = true;
                            Destroy(projectileCollidables[j].gameObject);
                            Destroy(projectiles.SpawnedProjectiles[j].gameObject);
                            projectileCollidables.Remove(projectileCollidables[j]);
                            projectiles.SpawnedProjectiles.Remove(projectiles.SpawnedProjectiles[j]);
                            newIteration = true;
                            break;
                        }
                        else
                        {
                            enemyCollidables[i].IsColliding = false;
                            projectileCollidables[j].IsColliding = false;
                        }
                    }

                    if (enemyCollidables[i].transform.position.y < (0 - height / 2))
                    {
                        enemyCollidables[i].IsColliding = true;
                        Destroy(enemyCollidables[i].gameObject);
                        enemyCollidables.Remove(enemyCollidables[i]);
                        newIteration = true;
                        break;
                    }
                }
            }    
        }
        while (newIteration != true);
    }
}
