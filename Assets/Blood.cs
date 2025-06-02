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

        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
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
        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
      
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
        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;

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
        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;

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
        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;

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
        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;

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
        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;

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
        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;

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
        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;

    }

    public void TourEffect(Enemy enemy,Cart card)
    {

    }
}


public class Bleeding : IPassive
{   
    public void PassiveEffect(PlayerHandler player, Enemy enemy, Cart card) 
    {
        if (!enemy.isBleeding)
        {
            card.isCheckedPassiveSituation = true;
            enemy.isBleeding = true;
        }
            

        if (card.isCheckedPassiveSituation)
        {
            enemy.TakeDamage(1);
            GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().MustBeSavedCards = card;
        }
        else
        {
            GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().MustBeSavedCards.TourCount = GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().MustBeSavedCards.TourCount + 1;
            card.TourCount = 0;
        }
    }

    public void PassiveReset(PlayerHandler player, Enemy enemy, Cart card)
    {
        enemy.isBleeding = false;
    }
}

public class AttackBuff : IPassive
{
    public void PassiveEffect(PlayerHandler player, Enemy enemy,Cart card)
    {
        if (!player.isAttackBuffing)
        {
            card.isCheckedPassiveSituation = true;
            player.isAttackBuffing = true;
        }

        if (card.isCheckedPassiveSituation)
        {
            int i = 1;
            PlayerPrefs.SetInt("isHasAttackBuff", i);
        }
        else
        {
            card.TourCount = 0;
        }
            
    }

    public void PassiveReset(PlayerHandler player, Enemy enemy, Cart card)
    {
        card.isCheckedPassiveSituation = false;
        player.isAttackBuffing = false;
        PlayerPrefs.DeleteKey("isHasAttackBuff");
    }
}

public class Burn : IPassive
{
    public void PassiveEffect(PlayerHandler player, Enemy enemy, Cart card)
    {
        if ((!enemy.isBurning))
        {
            card.isCheckedPassiveSituation = true;
            enemy.isBurning = true;
        }


        if (card.isCheckedPassiveSituation)
            enemy.TakeDamage(10);
        else
        {
            card.TourCount = 0;
        }
            
    }

    public void PassiveReset(PlayerHandler player, Enemy enemy, Cart card)
    {
        enemy.isBurning = false;
    }
}

public class LifeSteal : IPassive
{
    public void PassiveEffect(PlayerHandler player, Enemy enemy, Cart card) 
    {
        if (!player.isLifeStealing)
            card.isCheckedPassiveSituation = true;

        if (card.isCheckedPassiveSituation == true)
            player._health = player._health + ((card._baseItem.TotalDamage / 100) * 10);
        else
            card.TourCount = 0;
    }

    public void PassiveReset(PlayerHandler player, Enemy enemy, Cart card)
    {
        player.isLifeStealing = false;
    }
}





