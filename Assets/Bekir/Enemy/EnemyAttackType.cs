using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Create Enemy Attack Type", menuName = "Enemy Section/Enemy Attack Type", order = 2)]
public class EnemyAttackType : ScriptableObject
{
    public float Damage;
    [ListDrawerSettings]
    [SerializeReference, PolymorphicDrawerSettings]
    public List<IEnemyAttack> ItemEffects_OnEveryTour = new();
}