using UnityEngine;

public interface IPassive 
{
    public void PassiveEffect(PlayerHandler player, Enemy enemy,Collider2D collider);
}
