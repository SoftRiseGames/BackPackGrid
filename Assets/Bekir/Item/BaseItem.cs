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
    public BaseItem UpgradedItem;
    public int HandCardCount;
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
    public List<BaseItem> MergedItems;
    [ListDrawerSettings]
    [SerializeReference, PolymorphicDrawerSettings]
    public List<IItemEffect> ItemEffects_OnEveryTour = new();
    [ListDrawerSettings]
    [SerializeReference, PolymorphicDrawerSettings]
    public List<IItemEffect> ItemEffects_OnEnemy = new();
    [SerializeReference, PolymorphicDrawerSettings]
    public List<IItemEffect> ItemEffects_OnPlaced = new();
   
}

