using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class BaseItem : ScriptableObject
{
    
    /// <summary>
    /// for UI
    /// </summary>
    public string ItemName;
    public int HandCardCount;
    public bool isHavePassive;
    public float TotalDamage;
    public int ManaCount;
    [HideInInspector]public bool isAttackDMG;
    public int order;
    /// <summary>
    /// for giving tip to player and for uI
    /// </summary>
    [TextArea] public string Description;
    /// <summary>
    /// Items Sprite
    /// </summary>
    public Sprite ItemSprite;
    /// <summary>
    /// how many cell taking
    /// </summary>
    public Vector2Int cellSize;
    public BaseItem RootMergeItem;
    public int PassiveTourCount;
    public List<BaseItem> MergedItems;
    public bool isEnemyEffect;
    public bool isCharacterEffect;
    [ListDrawerSettings]
   
    [ListDrawerSettings]
    [SerializeReference, PolymorphicDrawerSettings]
    public List<IItemEffect> ItemEffects_OnEffectedObject = new();
    [SerializeReference, PolymorphicDrawerSettings]
    public List<IItemEffect> ItemEffects_OnPlaced = new();

    [SerializeReference, PolymorphicDrawerSettings]
    public List<IPassive> ItemEffects_OnEveryTour = new();


}

