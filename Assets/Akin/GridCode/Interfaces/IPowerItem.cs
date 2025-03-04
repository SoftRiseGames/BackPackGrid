using UnityEngine;
public interface IPowerItem
{
    public BaseItem BaseItemObject { get; set; }
    void PowerUpBuffs();
}
