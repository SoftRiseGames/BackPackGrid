using UnityEngine;

public interface IPassive 
{
    public void PassiveEffect(PlayerHandler player,Enemy enemy,Cart card);
    public void PassiveStartSetting(PlayerHandler player, Enemy enemy, Cart card);
    public void PassiveReset(PlayerHandler player, Enemy enemy, Cart card);
}

