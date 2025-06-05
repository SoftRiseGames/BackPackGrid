using UnityEngine;

public class PlayerHandler : MonoBehaviour, IDamage
{
    public float _health;
    private float _healtCounter;
    public float shield;

    [HideInInspector] public bool isAttackBuffing;
    [HideInInspector] public bool isLifeStealing;
    void Start()
    {
        _healtCounter = _health;
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
}