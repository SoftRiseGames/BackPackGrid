using UnityEngine;

public class Blood :IItemEffect
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


public class Sword : IItemEffect
{
    public void ExecuteEffect(Enemy enemy)
    {
        Debug.Log("DDDDDD");
    }

    public void TourEffect(Enemy enemy)
    {
       // throw new System.NotImplementedException();
    }
}


