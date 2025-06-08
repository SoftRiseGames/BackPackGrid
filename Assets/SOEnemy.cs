using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "EnemySO",menuName ="EnemySO")]
public class SOEnemy : ScriptableObject
{
    public float Health;
    public float Shield;
    public float shieldMaxValue;
    public float HealthMaxValue;
    [SerializeReference, PolymorphicDrawerSettings]
    public List<IEnemy> EnemyEffects = new();
    public List<string> EnemyPattern; //String Values Attack And Defence;
    
}
