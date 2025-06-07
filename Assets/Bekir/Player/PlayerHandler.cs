using UnityEngine;

public class PlayerHandler : MonoBehaviour, IDamage
{
    public float _health;
    private float _healtCounter;
    public float _shield;

    [HideInInspector] public bool isAttackBuffing;
    [HideInInspector] public bool isLifeStealing;
    private void OnEnable()
    {
        EnemyManager.onPlayerTurn += ShieldRenew;
    }
    private void OnDisable()
    {
        EnemyManager.onPlayerTurn -= ShieldRenew;
    }
    void Start()
    {
        _healtCounter = _health;
    }


    public void TakeDamageWithoutShield(float value)
    {
        _health = _health - value;
        if (_health <= 0)
            Die();
    }

    public void Die()
    {
        Debug.Log("I am Dead");
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
    void ShieldRenew()
    {
        _shield = 0;
    }
}