using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    [SerializeField] private EnemyType _enemyType;
    private float _healtCounter;
    private float _defaultHealth { get => _enemyType.Health; set => value = default; }
    void Start()
    {
        Init();
    }
    public void Init()
    {
        _healtCounter = _defaultHealth;
    }
    public void TakeDamage(float value)
    {
        if (_healtCounter < value)
        {
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
