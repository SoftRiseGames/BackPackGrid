using UnityEngine;

public class TileCode : MonoBehaviour
{
    public bool isRaycast;
    public LayerMask layer;
    public LayerMask HandleCheckLayer;

    public bool OnUp;
    public bool OnDown;
    public bool onLeft;
    public bool onRight;
    public bool OnMiddle;

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset, UpOffset, middleOffset;
    private Color debugCollisionColor = Color.red;

 

    void RaycastSystem()
    {
        OnUp = Physics2D.OverlapCircle((Vector2)transform.position + UpOffset, collisionRadius, layer);
        OnDown = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, layer);
        onRight = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, layer);
        onLeft = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, layer);
        OnMiddle = Physics2D.OverlapCircle((Vector2)transform.position + middleOffset, collisionRadius, HandleCheckLayer);


    }
    
    void TileLock()
    {
        if (OnMiddle)
        {
            gameObject.layer = LayerMask.NameToLayer("GridLocked");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("GridBasement");

        }
        Debug.Log("tile");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastSystem();
        TileLock();

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + UpOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + middleOffset, collisionRadius);
    }

}
