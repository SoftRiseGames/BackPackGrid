using UnityEngine;
using System.Threading.Tasks;
public class PlayerHandler : MonoBehaviour, IDamage
{
    [HideInInspector]public float _health;
    private float _healtCounter;
    public float _shield;
    public float _ShieldMaxValue;
    [HideInInspector] public bool isAttackBuffing;
    [HideInInspector] public bool isLifeStealing;
    public float HealthMaxValue;
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
        _health = HealthMaxValue;
        _healtCounter = _health;
    }


    public async void TakeDamageWithoutShield(float value)
    {
        GetComponent<Animator>().SetBool("isDamage", true);
        _health = _health - value;
        if (_health <= 0)
            Die();
        else
        {
            await Task.Delay(200);
            GetComponent<Animator>().SetBool("isDamage", false);
        }

    }

    public void Die()
    {
        Debug.Log("I am Dead");
    }

    public async void TakeDamageWithShield(float value)
    {
        GetComponent<Animator>().SetBool("isDamage", true);
        float shieldScaler = _shield;
        shieldScaler = shieldScaler - value;
        _shield = shieldScaler;

        if (_shield < 0)
            _shield = 0;


        if (shieldScaler < 0)
            _health = _health - Mathf.Abs(shieldScaler);

        if (_health <= 0)
            Die();
        else
        {
            await Task.Delay(200);
            GetComponent<Animator>().SetBool("isDamage", false);
        }
    }
    void ShieldRenew()
    {
        _shield = 0;
    }

    public void EarnShield(float value)
    {
        if (_shield < _ShieldMaxValue)
            _shield = _shield + value;
        else
            _shield = _ShieldMaxValue;
    }
}