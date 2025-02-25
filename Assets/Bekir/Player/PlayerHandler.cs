using UnityEngine;

public class PlayerHandler : MonoBehaviour, IDamage
{
    [SerializeField] private float _health;
    private float _healtCounter;


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