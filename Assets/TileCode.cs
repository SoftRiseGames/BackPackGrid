using UnityEngine;

public class TileCode : MonoBehaviour
{
    public bool isRaycast;
    public LayerMask layer;


    public bool OnUp;
    public bool OnDown;
    public bool onLeft;
    public bool onRight;

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset, UpOffset;
    public Vector2 bottomOffsetNext, rightOffsetNext, leftOffsetNext, UpOffsetNext;
    private Color debugCollisionColor = Color.red;

    void Start()
    {

      
    }

    void RaycastSystem()
    {
        OnUp = Physics2D.OverlapCircle((Vector2)transform.position + UpOffset, collisionRadius, layer);
        OnDown = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, layer);
        onRight = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, layer);
        onLeft = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, layer);


        if(OnUp == false || OnDown == false || onRight == false || onLeft == false)
        {
            gameObject.layer = LayerMask.NameToLayer("GridBasementEnded");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("gridBasement");
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastSystem(); 
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + UpOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
}
