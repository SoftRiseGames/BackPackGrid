using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  🃏 Kartlarda bu classlar olacak (MeleeWeopen, RangedWeopen, Spells etc)
/// </summary>
public class MeleeWeopen : MonoBehaviour, IItem
{
    public BaseItem BaseItem { get; set; }
    private List<IItemEffect> ItemEffects_OnEveryTour = new();
    private List<IItemEffect> ItemEffects_OnPlaced = new();

    public void OnAttack()
    {
        ItemEffects_OnPlaced?.ForEach((item) =>
        {
            item.ExecuteEffect();
        });
    }

    public void OnTour()
    {
        ItemEffects_OnEveryTour?.ForEach((item) =>
        {
            item.ExecuteEffect();
        });
    }

    public void Init()
    {
        ItemEffects_OnEveryTour = BaseItem.ItemEffects_OnEveryTour;
        ItemEffects_OnPlaced = BaseItem.ItemEffects_OnPlaced;
    }
}