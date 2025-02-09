using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class BaseItem : ScriptableObject
{
    /// <summary>
    /// for UI
    /// </summary>
    public string itemName;
    /// <summary>
    /// for giving tip to player and for uI
    /// </summary>
    [TextArea] public string description;
    /// <summary>
    /// Items Sprite
    /// </summary>
    public Sprite icon;
    /// <summary>
    /// how many cell taking
    /// </summary>
    public Vector2Int cellSize;
    public BaseItem RootMergeItem;
    public List<BaseItem> MergedItems;
    public List<ItemEffect> ItemEffects_OnEveryTour = new();
    public List<ItemEffect> ItemEffects_OnPlaced = new();
}

