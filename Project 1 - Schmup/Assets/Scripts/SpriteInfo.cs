using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
    Vector3 position;

    [SerializeField]
    Vector2 rectSize = Vector2.one;

    public Vector3 Position
    {
        get { return position; }
    }
    public Vector2 RectSize
    {
        get { return rectSize; }
    }

    SpriteRenderer spriteRenderer = new SpriteRenderer();

    //Properties for Min and Max
    public Vector2 RectMin
    {
        get
        {
            return new Vector2(transform.position.x - rectSize.x / 2,
                                 transform.position.y - rectSize.y / 2);
        }
    }
    public Vector2 RectMax
    {
        get
        {
            return new Vector2(transform.position.x + rectSize.x / 2,
                               transform.position.y + rectSize.y / 2);
        }
    }

    //Bools for Collision
    bool isColliding = false;

    //Sprite Renderer
    SpriteRenderer renderer;

    public bool IsColliding
    {
        get { return isColliding; }
        set { isColliding = value; }
    }

    
    public SpriteRenderer Renderer
    {
        get { return renderer; } 
    }
    
    public Sprite Sprite
    {
        set { spriteRenderer.sprite = value; }

    }

    private void Start()
    {
        position = transform.position;
    }
}
