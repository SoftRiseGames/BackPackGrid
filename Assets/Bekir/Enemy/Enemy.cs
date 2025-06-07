using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour, IDamage
{
    public float _health;
    public float _shield;
    private float _healtCounter;
    public PlayerHandler player;
    public SOEnemy EnemySettings;
    [SerializeField] private PlayerHandler PlayerObject;
    [HideInInspector]public int EnemyPatternCounter;
    public int Order;

    [HideInInspector] public bool isBurning;
    [HideInInspector] public bool isBleeding;
    


    public void Die()
    {
        EnemyManager.instance.enemies.Remove(gameObject.GetComponent<Enemy>());
        Debug.Log("I am Dead");
        gameObject.SetActive(false);
    }

    public void AttackOnTour()
    {
        _shield = 0;
        EnemySettings.EnemyEffects?.ForEach(effect => effect?.AttackOnTour(PlayerObject));
        EnemyPatternCounterManager();
    }

    public void DefenceOnTour()
    {
        _shield = 0;
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

    public void TakeDamageWithoutShield(float value)
    {
        _health = _health - value;
        if (_health <= 0)
            Die();
    }

    public void TakeDamageWithShield(float value)
    {
        float shieldScaler = _shield;
        shieldScaler = shieldScaler - value;
        _shield = shieldScaler;

        if (_shield < 0)
            _shield = 0;

        
        if (shieldScaler < 0)
            _health = _health - Mathf.Abs(shieldScaler);

        if (_health <= 0)
            Die();
    }
}
