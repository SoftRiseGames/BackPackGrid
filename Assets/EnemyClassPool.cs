using UnityEngine;

public class Bandit : IEnemy
{
    public void AttackOnTour(PlayerHandler player)
    {
        player.TakeDamageWithShield(1);
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

public class Wolf : IEnemy
{
    public void AttackOnTour(PlayerHandler Player)
    {
        Player.TakeDamageWithoutShield(10);
    }

    public void DefenceOnTour(PlayerHandler Player)
    {
        Debug.Log("Defence");
    }

    public void PassiveAttackOnTour(PlayerHandler Player)
    {
        Debug.Log("Passive");
    }
}
