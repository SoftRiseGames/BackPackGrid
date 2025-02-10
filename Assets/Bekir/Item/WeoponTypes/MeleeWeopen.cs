using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  🃏 Kartlarda bu classlar olacak (MeleeWeopen, RangedWeopen, Spells etc)
/// </summary>
public class MeleeWeopen : MonoBehaviour, IItem
{
    [Header("----UI")]
    [SerializeField] private Image _itemImageHolder;
    [SerializeField] private TMP_Text _nameTMP;
    [SerializeField] private TMP_Text _descTMP;
    [SerializeField] private TMP_Text _statsTMP;
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
        InitializeVisual();
    }
    private void InitializeVisual()
    {
        _itemImageHolder.sprite = BaseItem.ItemSprite;
        _nameTMP.text = BaseItem.ItemName;
        _descTMP.text = BaseItem.Description;
    }
}