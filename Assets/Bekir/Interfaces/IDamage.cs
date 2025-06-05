using UnityEngine;

public interface IDamage
{
    void TakeDamageWithoutShield(float value);

    void TakeDamageWithShield(float value);
    void Die();

}