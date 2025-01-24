using UnityEngine;
using UnityEngine.Events;
using System;

public class TwobyOne : InventoryObject, IRotatable
{
    public override void Consume()
    {
        throw new System.NotImplementedException();
    }

    public override void RegisterYourself()
    {
        throw new System.NotImplementedException();
    }

    public void RotateLeft(Action<int> callback)
    {
        //Rotate Object
        throw new System.NotImplementedException();

        //Call Callback
        Action<int> callback1 = callback;
        callback1(3);
    }

    public void RotateRight()
    {
        throw new System.NotImplementedException();
    }
}

internal interface IRotatable
{
    public void RotateLeft(Action<int> callback);
    public void RotateRight();
}

public interface IInventoryObject
{
    public void RegisterYourself();
    public void Consume();

}