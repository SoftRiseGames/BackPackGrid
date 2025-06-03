using System;
using System.Collections.Generic;
using UnityEngine;
public interface IInventoryObject
{
    void GridIntegration();
    void RegisterYourself();
    void Consume();
    void MoveObjectStarting();
  
    public BaseItem BaseItemObj { get; }
    public bool OnUpMiddle { get; }
    public bool OnDownMiddle { get; }
    public bool onRightMiddle { get; }
    public bool onLeftMiddle { get; }

    public bool OnUpRight { get; }
    public bool OnDownRight { get; }
    
    public bool OnUpLeft { get; }
    public bool OnDownLeft { get; }

    public bool OnDownNext { get; }
    public bool OnUpNext { get; }
    public bool onLeftNext { get; }
    public bool onRightNext { get; }

    public bool OnDownObjectDedect { get; }
    public bool OnUpObjectDedect { get; }
    public bool onLeftObjectDedect { get; }
    public bool onRightObjectDedect { get; }

    public List<GameObject> AddedMaterialsChecker { get; }
    public bool gridEnter { get; set; }
    public bool isAdded { get; set; }
    public bool CanEnterPosition { get; set; }

    public List<GameObject> CollideList { get; set; }

}
