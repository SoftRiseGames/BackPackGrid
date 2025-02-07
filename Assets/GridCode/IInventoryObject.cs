public interface IInventoryObject
{
    void GridIntegration();
    void RegisterYourself();
    void Consume();

   public bool OnUp { get; }
   public bool OnDown { get; }
   public bool onRight { get; }
   public bool onLeft { get; }

    public bool OnDownNext { get;}
    public bool OnUpNext { get;}
    public bool onLeftNext { get;}
    public bool onRightNext { get;}
}
