using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour, IDamage
{
    public float _health;
    private float _healtCounter;
    public PlayerHandler player;
    public SOEnemy EnemySettings;
    [SerializeField] private PlayerHandler PlayerObject;
    [HideInInspector]public int EnemyPatternCounter;
    public int Order;


    public void Die()
    {
        EnemyManager.instance.enemies.Remove(gameObject.GetComponent<Enemy>());
        Debug.Log("I am Dead");
        gameObject.SetActive(false);
    }

    public void AttackOnTour()
    {
        EnemySettings.EnemyEffects?.ForEach(effect => effect?.AttackOnTour(PlayerObject));
        EnemyPatternCounterManager();
    }

    public void DefenceOnTour()
    {
        EnemySettings.EnemyEffects?.ForEach(effect => effect?.DefenceOnTour(PlayerObject));
        EnemyPatternCounterManager();
    }

    void EnemyPatternCounterManager()
    {
        EnemyPatternCounter = EnemyPatternCounter + 1;

        if (EnemyPatternCounter >= EnemySettings.EnemyPattern.Count)
            EnemyPatternCounter = 0;
    }

    public void PassiveAttackOnTour()
    {
        return;
    }

    public void TakeDamage(float value)
    {
        _health = _health - value;
        if (_health <= 0)
            Die();
    }
}
