using UnityEngine;
using System.Threading.Tasks;
public class Blood : IItemEffect
{
    public async void ExecuteEffect(Enemy enemy,PlayerHandler player, Cart card)
    {
        enemy.GetComponent<Animator>().SetBool("isDamage", true);
        float TotalDamage = card.CardDamage;

        if (enemy._health > 0)
        {
            EventManagerCode.DMGEffectAction.Invoke();
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamageWithShield((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamageWithShield((TotalDamage));
        }

        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
        await Task.Delay(500);
        enemy.GetComponent<Animator>().SetBool("isDamage", false);
        EventManagerCode.DMGEffectStopAction.Invoke();
    }

    public void TourEffect(Enemy enemy,Cart Card)
    {
      
    }


}
/*
void ZýrhFaktoruYok()
{
    public async void ExecuteEffect(Enemy enemy, PlayerHandler player, Cart Card)
    {

        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            EventManagerCode.DMGEffectAction.Invoke();
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamageWithoutShield((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamageWithoutShield((TotalDamage));
        }
        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
        await Task.Delay(300);
        EventManagerCode.DMGEffectStopAction.Invoke();

    }
}
*/
/*
void ZýrhaVurur()
{
    float TotalDamage = Card.CardDamage;

    if (enemy._health > 0)
    {
        EventManagerCode.DMGEffectAction.Invoke();
        if (PlayerPrefs.HasKey("isHasAttackBuff"))
        {
            Debug.Log("AttackBuff");
            enemy.TakeDamageWithShield((TotalDamage) + ((TotalDamage / 100) * 10));
        }

        else
            enemy.TakeDamageWithShield((TotalDamage));
    }
    GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
    await Task.Delay(300);
    EventManagerCode.DMGEffectStopAction.Invoke();

}
*/
public class Sword : IItemEffect
{
    public async void ExecuteEffect(Enemy enemy,PlayerHandler player, Cart Card)
    {

        enemy.GetComponent<Animator>().SetBool("isDamage", true);
        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            EventManagerCode.DMGEffectAction.Invoke();
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamageWithShield((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamageWithShield((TotalDamage));
        }

        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
        await Task.Delay(500);
        enemy.GetComponent<Animator>().SetBool("isDamage", false);
        EventManagerCode.DMGEffectStopAction.Invoke();

    }

    public void TourEffect(Enemy enemy,Cart Card)
    {
       
    }
}


public class Knife : IItemEffect
{
    public async void ExecuteEffect(Enemy enemy, PlayerHandler player, Cart Card)
    {
        enemy.GetComponent<Animator>().SetBool("isDamage", true);
        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            EventManagerCode.DMGEffectAction.Invoke();
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamageWithShield((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamageWithShield((TotalDamage));
        }

        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
        await Task.Delay(500);
        enemy.GetComponent<Animator>().SetBool("isDamage", false);
        EventManagerCode.DMGEffectStopAction.Invoke();

    }

    public void TourEffect(Enemy enemy,Cart card)
    {

    }
}


public class Rifle : IItemEffect
{
    public async void ExecuteEffect(Enemy enemy,PlayerHandler player, Cart Card)
    {
        enemy.GetComponent<Animator>().SetBool("isDamage", true);
        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            EventManagerCode.DMGEffectAction.Invoke();
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamageWithShield((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamageWithShield((TotalDamage));
        }

        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
        await Task.Delay(500);
        enemy.GetComponent<Animator>().SetBool("isDamage", false);
        EventManagerCode.DMGEffectStopAction.Invoke();
    }

    public void TourEffect(Enemy enemy,Cart Card)
    {

    }
}


public class FireSword : IItemEffect
{
    public async void ExecuteEffect(Enemy enemy,PlayerHandler player, Cart Card)
    {

        enemy.GetComponent<Animator>().SetBool("isDamage", true);
        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            EventManagerCode.DMGEffectAction.Invoke();
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamageWithShield((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamageWithShield((TotalDamage));
        }
        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
        await Task.Delay(300);
        EventManagerCode.DMGEffectStopAction.Invoke();
    }

    public void TourEffect(Enemy enemy,Cart card)
    {

    }
}

public class BloodSword : IItemEffect
{
    public async void ExecuteEffect(Enemy enemy,PlayerHandler player, Cart Card)
    {
        enemy.GetComponent<Animator>().SetBool("isDamage", true);
        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            EventManagerCode.DMGEffectAction.Invoke();
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamageWithShield((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamageWithShield((TotalDamage));
        }

        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
        await Task.Delay(500);
        enemy.GetComponent<Animator>().SetBool("isDamage", false);
        EventManagerCode.DMGEffectStopAction.Invoke();
    }

    public void TourEffect(Enemy enemy,Cart card)
    {

    }
}
public class GreatSword : IItemEffect
{
    public async void ExecuteEffect(Enemy enemy,PlayerHandler player, Cart Card)
    {
        Debug.Log(Card.CardDamage);
        enemy.GetComponent<Animator>().SetBool("isDamage", true);
        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            EventManagerCode.DMGEffectAction.Invoke();
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamageWithShield((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamageWithShield((TotalDamage));
        }
        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
        await Task.Delay(300);
        enemy.GetComponent<Animator>().SetBool("isDamage", false);
        EventManagerCode.DMGEffectStopAction.Invoke();
    }

    public void TourEffect(Enemy enemy,Cart card)
    {
        if (!card.isPlayed)
            card.CardDamage = card.CardDamage + 1;
    }
}

public class CursedBloodSword : IItemEffect
{
    public async void ExecuteEffect(Enemy enemy,PlayerHandler player, Cart Card)
    {

        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            EventManagerCode.DMGEffectAction.Invoke();
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamageWithShield((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamageWithShield((TotalDamage));
        }
        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
        await Task.Delay(300);
        EventManagerCode.DMGEffectStopAction.Invoke();
    }

    public void TourEffect(Enemy enemy,Cart card)
    {

    }
}

public class ThrowingKnifes : IItemEffect
{
    public async void ExecuteEffect(Enemy enemy,PlayerHandler player, Cart Card)
    {

        float TotalDamage = Card.CardDamage;

        if (enemy._health > 0)
        {
            EventManagerCode.DMGEffectAction.Invoke();
            if (PlayerPrefs.HasKey("isHasAttackBuff"))
            {
                Debug.Log("AttackBuff");
                enemy.TakeDamageWithShield((TotalDamage) + ((TotalDamage / 100) * 10));
            }

            else
                enemy.TakeDamageWithShield((TotalDamage));
        }
        GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
        await Task.Delay(300);
        EventManagerCode.DMGEffectStopAction.Invoke();
    }

    public void TourEffect(Enemy enemy,Cart card)
    {

    }
}

public class Shield : IItemEffect
{
    public void ExecuteEffect(Enemy enemy, PlayerHandler player, Cart card)
    {
        player.EarnShield(5);
    }

    public void TourEffect(Enemy enemy, Cart card)
    {
        //throw new System.NotImplementedException();
    }
}

public class Bleeding : IPassive
{   
    public async void PassiveEffect(PlayerHandler player, Enemy enemy, Cart card) 
    {
        if (!card.isCheckedPassiveSituation)
        {
            card.isCheckedPassiveSituation = true;
        }
      
        if (card.isCheckedPassiveSituation)
        {
            EventManagerCode.DMGEffectAction.Invoke();
            enemy.TakeDamageWithoutShield(1);
            enemy.BleedingTourText.text = (card.PassiveTourCount - 1).ToString();
            await Task.Delay(300);
            EventManagerCode.DMGEffectStopAction.Invoke();
        }
        else
        {
            GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().MustBeSavedCards.PassiveTourCount = GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().MustBeSavedCards.PassiveTourCount + 1;
            card.PassiveTourCount = 0;
        }
    }

    public void PassiveReset(PlayerHandler player, Enemy enemy, Cart card)
    {
        enemy.isBleeding = false;
    }

    public void PassiveStartSetting(PlayerHandler player, Enemy enemy, Cart card)
    {
        if (!enemy.isBleeding)
        {
            enemy.BleedingTourText.text = (card.PassiveTourCount).ToString();
            GameObject.Find("MustBeSavedObjects").GetComponent<SelectedEnemy>().MustBeSavedCards = card;
            enemy.isBleeding = true;
          
        }
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
            card.PassiveTourCount = 0;
        }
            
    }

    public void PassiveReset(PlayerHandler player, Enemy enemy, Cart card)
    {
        card.isCheckedPassiveSituation = false;
        player.isAttackBuffing = false;
        PlayerPrefs.DeleteKey("isHasAttackBuff");
    }

    public void PassiveStartSetting(PlayerHandler player, Enemy enemy, Cart card)
    {
       
    }
}

public class Burn : IPassive
{
    public async void PassiveEffect(PlayerHandler player, Enemy enemy, Cart card)
    {
        if ((!card.isCheckedPassiveSituation))
        {
            card.isCheckedPassiveSituation = true;
        }


        if (card.isCheckedPassiveSituation)
        {
            EventManagerCode.DMGEffectAction.Invoke();
            enemy.BurningTourText.text = (card.PassiveTourCount-1).ToString();
            enemy.TakeDamageWithoutShield(10);
            await Task.Delay(300);
            EventManagerCode.DMGEffectStopAction.Invoke();
        }
        else
        {
            card.PassiveTourCount = 0;
        }
            
    }

    public void PassiveReset(PlayerHandler player, Enemy enemy, Cart card)
    {
        enemy.isBurning = false;
    }

    public void PassiveStartSetting(PlayerHandler player, Enemy enemy, Cart card)
    {
        if ((!enemy.isBurning))
        {

            enemy.BurningTourText.text = (card.PassiveTourCount).ToString();
            enemy.isBurning = true;
        }

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
            card.PassiveTourCount = 0;
    }

    public void PassiveReset(PlayerHandler player, Enemy enemy, Cart card)
    {
        player.isLifeStealing = false;
    }

    public void PassiveStartSetting(PlayerHandler player, Enemy enemy, Cart card)
    {
        
    }
}







