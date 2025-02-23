using UnityEngine;


[CreateAssetMenu(fileName = "Create Enemy", menuName = "Enemy Section/Enemy Type", order = 1)]
public class EnemyType : ScriptableObject
{
    public string EnemyName;
    public float Health;
    public Sprite EnemySprite;
    public EnemyAttackType EnemyAttack;
}