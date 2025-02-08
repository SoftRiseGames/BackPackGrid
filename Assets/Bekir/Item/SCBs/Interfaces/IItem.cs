using UnityEngine;

public interface IItem
{
    BaseItem BaseItem { get; set; }

    /// <summary>
    /// function of every tour. maybe some passive things might be happend.
    /// </summary>
    void OnTour();
    void OnAttack();
}