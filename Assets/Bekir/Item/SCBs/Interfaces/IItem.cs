using System.Collections.Generic;
using UnityEngine;

public interface IItem
{

    /// <summary>
    /// function called once when cart spawned
    /// </summary>
    void Init(BaseItem baseItem);
    /// <summary>
    /// function of every tour. maybe some passive things might be happend.
    /// </summary>
    void OnTour();
    void OnAttack();

}