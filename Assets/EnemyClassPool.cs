using UnityEngine;

public class Bandit : IEnemy
{
    public void AttackOnTour(PlayerHandler player,Enemy enemy)
    {
        player.TakeDamageWithShield(1);
    }

    public void DefenceOnTour(PlayerHandler player, Enemy enemy)
    {
        enemy.ShieldParticle.Play();
        enemy.EarnShield(1);
        Debug.Log("Defence");
    }

    public void PassiveAttackOnTour(PlayerHandler player, Enemy enemy)
    {
        Debug.Log("Passive");
    }
}

public class Wolf : IEnemy
{
    public void AttackOnTour(PlayerHandler Player, Enemy enemy)
    {
        Player.TakeDamageWithoutShield(10);
    }

    public void DefenceOnTour(PlayerHandler Player, Enemy enemy)
    {
        enemy.ShieldParticle.Play();
        enemy.EarnShield(1);
        Debug.Log("Defence");
    }

    public void PassiveAttackOnTour(PlayerHandler Player, Enemy enemy)
    {
        Debug.Log("Passive");
    }
}
