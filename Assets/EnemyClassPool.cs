using UnityEngine;

public class Bandit : IEnemy
{
    public void AttackOnTour(PlayerHandler player)
    {
        player._health = player._health - 1;
    }

    public void DefenceOnTour(PlayerHandler player)
    {
        Debug.Log("Defence");
    }

    public void PassiveAttackOnTour(PlayerHandler player)
    {
        Debug.Log("Passive");
    }
}
