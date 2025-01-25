using UnityEngine;
using UnityEngine.Events;
using System;


public class TwobyOne : MonoBehaviour,IInventoryObject, IRotatable,IHelper
{
    public int SidePosXValue;
    public int SidePosYValue;
    public  void Consume()
    {
        throw new System.NotImplementedException();
    }

    public void RegisterYourself()
    {
        throw new System.NotImplementedException();
    }

    public void RotateLeft(Action<int> callback)
    {
        //Rotate Object
        //Call Callback
        Action<int> callback1 = callback;
        callback1(3);
    }

    public void RotateRight()
    {
        throw new System.NotImplementedException();
    }

    public int SidePosX()
    {
        return this.SidePosXValue;
    }

    public int SidePosY()
    {
        return SidePosYValue;
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