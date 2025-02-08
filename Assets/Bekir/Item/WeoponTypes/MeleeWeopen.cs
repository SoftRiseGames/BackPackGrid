using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  🃏 Kartlarda bu classlar olacak (MeleeWeopen, RangedWeopen, Spells etc)
/// </summary>
public class MeleeWeopen : MonoBehaviour, IItem
{
    public BaseItem BaseItem { get; set; }
    public List<ItemEffect> ItemEffects_OnEveryTour = new();
    public List<ItemEffect> ItemEffects_OnPlaced = new();

    public void OnAttack()
    {
    }

    public void OnTour()
    {
    }

    public void Init()
    {
        ItemEffects_OnEveryTour = BaseItem.ItemEffects_OnEveryTour;
        ItemEffects_OnPlaced = BaseItem.ItemEffects_OnPlaced;
    }
}