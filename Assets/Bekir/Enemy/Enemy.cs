using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    public float _health;
    private float _healtCounter;
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
}
