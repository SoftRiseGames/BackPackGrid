using UnityEngine;

public interface IDamage
{
    void TakeDamageWithShield(float value);

    void TakeDamageWithoutShield(float value);
    void Die();

}