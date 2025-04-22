using UnityEngine;

public class IItemExample :IItemEffect
{
    public void ExecuteEffect(Enemy enemy)
    {
        enemy._health = enemy._health - 1;
        Debug.Log(enemy._health);
        Debug.Log("aaaa");
    }

    public void TourEffect(Enemy enemy)
    {
        Debug.Log("bbbbb");
    }

  
}
