using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    SpriteInfo playerCollidable = new SpriteInfo();

    List<SpriteInfo> enemyCollidables = new List<SpriteInfo>();

    [SerializeField]
    EnemySpawner spawner = new EnemySpawner();

    // Start is called before the first frame update
    void Start()
    {
        enemyCollidables = spawner.SpawnedAsteroids;
        AABB();
    }

    // Update is called once per frame
    void Update()
    {
        AABB();
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
    void AABB()
    {
        for (int i = 0; i < enemyCollidables.Count; i++)
        {
            if ((enemyCollidables[i].IsColliding = AABBCheck(playerCollidable, enemyCollidables[i])))
            {
                playerCollidable.IsColliding = true;
                break;
            }
            else
            {
                playerCollidable.IsColliding = false;
            }
        }
    }
}
