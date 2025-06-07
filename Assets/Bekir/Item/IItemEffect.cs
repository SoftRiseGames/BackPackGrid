using UnityEngine;

public interface IItemEffect
{
    void ExecuteEffect(Enemy enemy,PlayerHandler player,Cart card);
    void TourEffect(Enemy enemy,Cart card);
}