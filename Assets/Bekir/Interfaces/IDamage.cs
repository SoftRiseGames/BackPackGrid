using UnityEngine;

public interface IDamage
{
    void TakeDamageWithoutShield(float value);

    void TakeDamageWithShield(float value);

    void EarnShield(float value);
    void Die();

}