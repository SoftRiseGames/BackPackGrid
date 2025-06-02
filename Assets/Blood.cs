using UnityEngine;

public class Blood : IItemEffect
{
    public void ExecuteEffect(Enemy enemy, Cart card)
    {
        float TotalDamage = card.CardDamage;

        if (enemy._health > 0)
        {
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                 enemy.TakeDamage((TotalDamage) + ((TotalDamage / 100) * 10));
            }
               
            else
                enemy.TakeDamage((TotalDamage));
        }

        GameObject.Find("SelectedEnemy").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
    }

    public void TourEffect(Enemy enemy,Cart Card)
    {
      
    }


}


public class Sword : IItemEffect
{
    public void ExecuteEffect(Enemy enemy, Cart Card)
    {

        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamage((TotalDamage) + ((TotalDamage / 100) * 10));
            }
                
            else
                enemy.TakeDamage((TotalDamage));
        }
        GameObject.Find("SelectedEnemy").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
      
    }

    public void TourEffect(Enemy enemy,Cart Card)
    {
       
    }
}


public class Knife : IItemEffect
{
    public void ExecuteEffect(Enemy enemy, Cart Card)
    {

        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamage((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamage((TotalDamage));
        }
        GameObject.Find("SelectedEnemy").GetComponent<SelectedEnemy>().selectedEnemy = enemy;

    }

    public void TourEffect(Enemy enemy,Cart card)
    {

    }
}


public class Rifle : IItemEffect
{
    public void ExecuteEffect(Enemy enemy, Cart Card)
    {

        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamage((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamage((TotalDamage));
        }
        GameObject.Find("SelectedEnemy").GetComponent<SelectedEnemy>().selectedEnemy = enemy;

    }

    public void TourEffect(Enemy enemy,Cart Card)
    {

    }
}


public class FireSword : IItemEffect
{
    public void ExecuteEffect(Enemy enemy, Cart Card)
    {

        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamage((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamage((TotalDamage));
        }
        GameObject.Find("SelectedEnemy").GetComponent<SelectedEnemy>().selectedEnemy = enemy;

    }

    public void TourEffect(Enemy enemy,Cart card)
    {

    }
}

public class BloodSword : IItemEffect
{
    public void ExecuteEffect(Enemy enemy, Cart Card)
    {

        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamage((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamage((TotalDamage));
        }
        GameObject.Find("SelectedEnemy").GetComponent<SelectedEnemy>().selectedEnemy = enemy;

    }

    public void TourEffect(Enemy enemy,Cart card)
    {

    }
}
public class GreatSword : IItemEffect
{
    public void ExecuteEffect(Enemy enemy, Cart Card)
    {

        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamage((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamage((TotalDamage));
        }
        GameObject.Find("SelectedEnemy").GetComponent<SelectedEnemy>().selectedEnemy = enemy;

    }

    public void TourEffect(Enemy enemy,Cart card)
    {
        if (!card.isPlayed)
            card.CardDamage = card.CardDamage + 1;
    }
}

public class CursedBloodSword : IItemEffect
{
    public void ExecuteEffect(Enemy enemy, Cart Card)
    {

        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamage((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamage((TotalDamage));
        }
        GameObject.Find("SelectedEnemy").GetComponent<SelectedEnemy>().selectedEnemy = enemy;

    }

    public void TourEffect(Enemy enemy,Cart card)
    {

    }
}

public class ThrowingKnifes : IItemEffect
{
    public void ExecuteEffect(Enemy enemy, Cart Card)
    {

        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamage((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamage((TotalDamage));
        }
        GameObject.Find("SelectedEnemy").GetComponent<SelectedEnemy>().selectedEnemy = enemy;

    }

    public void TourEffect(Enemy enemy,Cart card)
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
        player._health = player._health + ((card._baseItem.TotalDamage/100)*10);
    }
}



