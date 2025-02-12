using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    [SerializeField] private float _health;
    private float _healtCounter;
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
}
