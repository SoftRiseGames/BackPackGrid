using UnityEngine;

public class Bandit : IEnemy
{
    public void AttackOnTour(PlayerHandler player,Enemy enemy)
    {
        player.TakeDamageWithShield(enemy.EnemySettings.TakenDamage);
    }

    public void DefenceOnTour(PlayerHandler player, Enemy enemy)
    {
        enemy.ShieldParticle.Play();
        enemy.EarnShield(enemy.EnemySettings.EarnShield);
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
        Player.TakeDamageWithoutShield(enemy.EnemySettings.TakenDamage);
    }

    public void DefenceOnTour(PlayerHandler Player, Enemy enemy)
    {
        enemy.ShieldParticle.Play();
        enemy.EarnShield(enemy.EnemySettings.EarnShield);
        Debug.Log("Defence");
    }

    public void PassiveAttackOnTour(PlayerHandler Player, Enemy enemy)
    {
        Debug.Log("Passive");
    }
}
