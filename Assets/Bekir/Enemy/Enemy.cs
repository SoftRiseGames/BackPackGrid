using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour, IDamage
{
    public float _health;
    private float _healtCounter;
    public PlayerHandler player;
    public SOEnemy EnemySettings;
    [SerializeField] private PlayerHandler PlayerObject;
    public int EnemyPatternCounter;
    [HideInInspector] public int Order;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            _health = _health - 1;
    }
    public void TakeDamage(float value)
    {
        
            if (_health < value){
            Die();
            return;
        }
        _healtCounter -= value;
    }

    public void Die()
    {
        Debug.Log("I am Dead");
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
}
