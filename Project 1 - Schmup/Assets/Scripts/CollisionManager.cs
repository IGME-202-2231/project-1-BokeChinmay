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

    public int Multiplier
    {
        get { return multiplier; }
        set { multiplier = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyCollidables = spawner.SpawnedAsteroids;
        projectileCollidables = projectiles.SpawnedProjectiles;

        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;

        if(enemyCollidables.Count > 0)
        {
            playerEnemy();
            projectileEnemy();
        }

        score = 0;
        multiplier = 1;
        health = 3;

        healthBar.text = "Health: " + health;
        playerScore.text = "Score: " + score;
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

    /*
    void playerEnemy()
    {
        if (enemyCollidables.Count != 0)
        {
            for (int i = enemyCollidables.Count - 1; i >= 0; i--)
            {
                SpriteInfo asteroid = enemyCollidables[i].GetComponent<SpriteInfo>();

                if (AABBCheck(playerCollidable, asteroid))
                {
                    GameObject enemy = enemyCollidables[i].gameObject;

                    enemyCollidables.RemoveAt(i);
                    Destroy(enemy);

                    health--;
                    healthBar.text = "Health: " + health;
                    multiplier = 1;
                }

            }
        }
    }
    */

    void playerEnemy()
    {
        if (enemyCollidables.Count != 0)
        {
            for (int i = enemyCollidables.Count - 1; i >= 0; i--)
            {
                SpriteInfo asteroid = enemyCollidables[i].GetComponent<SpriteInfo>();

                if (AABBCheck(playerCollidable, asteroid))
                {
                    GameObject enemy = enemyCollidables[i].gameObject;

                    SpriteRenderer spriteR = enemyCollidables[i].GetComponent<SpriteRenderer>();
                    
                    if(spriteR.sprite == spawner.AsteroidImages[1])
                    {
                        enemyCollidables.RemoveAt(i);
                        Destroy(enemy);

                        health--;
                        healthBar.text = "Health: " + health;
                        multiplier = 1;
                    }
                    else
                    {
                        enemyCollidables.RemoveAt(i);
                        Destroy(enemy);

                        health = health - 2;
                        healthBar.text = "Health: " + health;
                        multiplier = 1;
                    }
                }

            }
        }
    }

    void projectileEnemy()
    {
        if (enemyCollidables.Count != 0 && projectileCollidables.Count != 0)
        {
            for (int i = enemyCollidables.Count - 1; i >= 0; i--)
            {
                for (int j = projectileCollidables.Count - 1; j >= 0; j--)
                {

                    SpriteInfo asteroid = enemyCollidables[i].GetComponent<SpriteInfo>();
                    SpriteInfo projectile = projectileCollidables[j].GetComponent<SpriteInfo>();

                    if (AABBCheck(projectile, asteroid))
                    {
                        GameObject shot = projectileCollidables[j].gameObject;
                        GameObject enemy = enemyCollidables[i].gameObject;

                        SpriteRenderer spriteR = enemyCollidables[i].GetComponent<SpriteRenderer>();


                        if (spriteR.sprite == spawner.AsteroidImages[0])
                        {
                            spriteR.sprite = spawner.AsteroidImages[1];
                            projectileCollidables.RemoveAt(j);
                            Destroy(shot);

                            multiplier++;
                            score = score + (1 * multiplier);
                            playerScore.text = "Score: " + score;
                        }
                        else
                        {
                            projectileCollidables.RemoveAt(j);
                            enemyCollidables.RemoveAt(i);
                            Destroy(shot);
                            Destroy(enemy);

                            multiplier++;
                            score = score + (1 * multiplier);
                            playerScore.text = "Score: " + score;
                        }   
                    }
                }
            }
        }
    }
}
