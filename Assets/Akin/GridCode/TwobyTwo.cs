using System.Collections.Generic;
using UnityEngine;

public class TwobyTwo : IInventoryObject
{
    public bool OnUpMiddle => throw new System.NotImplementedException();

    public bool OnDownMiddle => throw new System.NotImplementedException();

    public bool onRightMiddle => throw new System.NotImplementedException();

    public bool onLeftMiddle => throw new System.NotImplementedException();

    public bool OnDownNext => throw new System.NotImplementedException();

    public bool OnUpNext => throw new System.NotImplementedException();

    public bool onLeftNext => throw new System.NotImplementedException();

    public bool onRightNext => throw new System.NotImplementedException();

    public bool OnDownObjectDedect => throw new System.NotImplementedException();

    public bool OnUpObjectDedect => throw new System.NotImplementedException();

    public bool onLeftObjectDedect => throw new System.NotImplementedException();

    public bool onRightObjectDedect => throw new System.NotImplementedException();

    public bool gridEnter { get; set; }
    public bool OffTheGrid { get; set; }

    public bool OnUpRight => throw new System.NotImplementedException();

    public bool OnDownRight => throw new System.NotImplementedException();

    public bool OnUpLeft => throw new System.NotImplementedException();

    public bool OnDownLeft => throw new System.NotImplementedException();

    public bool OnMiddle => throw new System.NotImplementedException();

    public List<GameObject> AddedMaterialsChecker { get; set; }

    public BaseItem BaseItemObj { get; set; }
    public bool isAdded { get; set; }
    public bool CanEnterPosition { get; set; }

    public void Consume()
    {
        throw new System.NotImplementedException();
    }

    public void GridIntegration()
    {
        throw new System.NotImplementedException();
    }

    public void MoveObjectStarting()
    {
        throw new System.NotImplementedException();
    }

    public void RegisterYourself()
    {
        throw new System.NotImplementedException();
    }
}
