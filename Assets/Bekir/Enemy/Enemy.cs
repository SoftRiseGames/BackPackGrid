using UnityEngine;

public class Enemy : MonoBehaviour, IDamage,IEnemy
{
    public float _health;
    private float _healtCounter;
    public PlayerHandler player;
    public float EnemyDamageTaken;
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
        player._health = player._health - 1;
    }

    public void DefenceOnTour()
    {
        return;
    }

    public void PassiveAttackOnTour()
    {
        return;
    }
}
