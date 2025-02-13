using System;

public interface IInventoryObject
{
    void GridIntegration();
    void RegisterYourself();
    void Consume();

    void MoveObjectStarting();
    public bool OnUp { get; }
    public bool OnDown { get; }
    public bool onRight { get; }
    public bool onLeft { get; }

    public bool OnDownNext { get; }
    public bool OnUpNext { get; }
    public bool onLeftNext { get; }
    public bool onRightNext { get; }

    public bool OnDownObjectDedect { get; }
    public bool OnUpObjectDedect { get; }
    public bool onLeftObjectDedect { get; }
    public bool onRightObjectDedect { get; }



}
