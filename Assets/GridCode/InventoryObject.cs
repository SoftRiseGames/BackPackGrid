using UnityEngine;

public abstract class InventoryObject:IInventoryObject
{
    public bool OnUp => throw new System.NotImplementedException();

    public bool OnDown => throw new System.NotImplementedException();

    public bool onRight => throw new System.NotImplementedException();

    public bool onLeft => throw new System.NotImplementedException();

    public virtual void Consume()
    {
        throw new System.NotImplementedException();
    }

    public virtual void RegisterYourself()
    {
        throw new System.NotImplementedException();
    }

}
