using UnityEngine;

public interface IPowerItem 
{
    BaseItem BaseItemObject { get; set; }

    void PowerUpBuffs();

}
