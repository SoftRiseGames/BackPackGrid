using UnityEngine;
using UnityEngine.Events;
using System;

public class TwobyOne : MonoBehaviour, IInventoryObject, IRotatable, IHelper
{
    public int SidePosXValue;
    public int SidePosYValue;

   
    public bool OnDown { get; private set; }
    public bool OnUp { get; private set; }
    public bool onLeft { get; private set; }
    public bool onRight { get; private set; }

    public bool OnDownNext { get; private set; }
    public bool OnUpNext { get; private set; }
    public bool onLeftNext { get; private set; }
    public bool onRightNext { get; private set; }

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset, UpOffset;
    public Vector2 bottomNextOffset, rightNextOffset, leftNextOffset, UpNextOffset;
    private Color debugCollisionColor = Color.red;
    public LayerMask LayerSide;
    public LayerMask LayerNextCheck;

    private void Update()
    {
        Ray();
    }

    public void Consume()
    {
        throw new System.NotImplementedException();
    }

    public void RegisterYourself()
    {
        throw new System.NotImplementedException();
    }

    public void RotateLeft(Action<int> callback)
    {
        // Rotate Object
        // Call Callback
        callback?.Invoke(3);
    }

    public void RotateRight()
    {
        throw new System.NotImplementedException();
    }

    public int SidePosX()
    {
        return SidePosXValue;
    }

    public int SidePosY()
    {
        return SidePosYValue;
    }

    void Ray()
    {
        OnUp = Physics2D.OverlapCircle((Vector2)transform.position + UpOffset, collisionRadius, LayerSide);
        OnDown = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, LayerSide);
        onRight = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, LayerSide);
        onLeft = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, LayerSide);


        OnUpNext = Physics2D.OverlapCircle((Vector2)transform.position + UpNextOffset, collisionRadius, LayerNextCheck);
        OnDownNext = Physics2D.OverlapCircle((Vector2)transform.position + bottomNextOffset, collisionRadius, LayerNextCheck);
        onRightNext = Physics2D.OverlapCircle((Vector2)transform.position + rightNextOffset, collisionRadius, LayerNextCheck);
        onLeftNext = Physics2D.OverlapCircle((Vector2)transform.position + leftNextOffset, collisionRadius, LayerNextCheck);


    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + UpOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomNextOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + UpNextOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightNextOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftNextOffset, collisionRadius);
    }

}

public interface IRotatable
{
    void RotateLeft(Action<int> callback);
    void RotateRight();
}

public interface IInventoryObject
{
    void RegisterYourself();
    void Consume();

    // Yeni Eklenen Properties
    
   public bool OnUp { get; }
   public bool OnDown { get; }
   public bool onRight { get; }
   public bool onLeft { get; }

    public bool OnDownNext { get;}
    public bool OnUpNext { get;}
    public bool onLeftNext { get;}
    public bool onRightNext { get;}
}
