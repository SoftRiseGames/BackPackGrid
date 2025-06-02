using UnityEngine;

public interface IItemEffect
{
    void ExecuteEffect(Enemy enemy,Cart card);
    void TourEffect(Enemy enemy,Cart card);
}