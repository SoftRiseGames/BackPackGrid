using System;

public interface IInventoryObject
{
    void GridIntegration();
    void RegisterYourself();
    void Consume();

    void MoveObjectStarting();
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

    public bool gridEnter { get; set; }

    

}
