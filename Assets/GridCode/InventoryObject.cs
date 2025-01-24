using UnityEngine;

public abstract class InventoryObject:IInventoryObject
{
    public virtual void Consume()
    {
        throw new System.NotImplementedException();
    }

    public virtual void RegisterYourself()
    {
        throw new System.NotImplementedException();
    }

}
