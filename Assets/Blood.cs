using UnityEngine;

public class Blood : IItemEffect
{
    public void ExecuteEffect(Enemy enemy, Cart card)
    {
        float TotalDamage = card._baseItem.TotalDamage;

        if (enemy._health > 0)
        {
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
                enemy.TakeDamage((TotalDamage) + ((TotalDamage / 100) * 10));
            else
                enemy.TakeDamage((TotalDamage));
        }
        GameObject.Find("SelectedEnemy").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
    }

    public void TourEffect(Enemy enemy)
    {
      
    }


}


public class Sword : IItemEffect
{
    public void ExecuteEffect(Enemy enemy, Cart Card)
    {

        float TotalDamage = Card._baseItem.TotalDamage;

        if (enemy._health > 0)
        {
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
                enemy.TakeDamage((TotalDamage) + ((TotalDamage / 100) * 10));
            else
                enemy.TakeDamage((TotalDamage));
        }
        GameObject.Find("SelectedEnemy").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
      
    }

    public void TourEffect(Enemy enemy)
    {
       
    }
}


public class Bleeding : IPassive
{

    public void PassiveEffect(PlayerHandler player, Enemy enemy, Cart card)
    {
       enemy.TakeDamage(1);
    }
}

public class AttackBuff : IPassive
{
    public void PassiveEffect(PlayerHandler player, Enemy enemy,Cart card)
    {
        int i = 1;
        PlayerPrefs.SetInt("isHasAttackBuff", i);
    }
}

public class Burn : IPassive
{
    public void PassiveEffect(PlayerHandler player, Enemy enemy, Cart card)
    {
        enemy.TakeDamage(10);
    }
}

public class LifeSteal : IPassive
{
    public void PassiveEffect(PlayerHandler player, Enemy enemy, Cart card) 
    {
        float DMGSteal = (((PlayerPrefs.GetFloat("FirstEnemyHealth")-PlayerPrefs.GetFloat("LastEnemyHealth")/100)*10));
        player._health = player._health + DMGSteal;
    }
}



