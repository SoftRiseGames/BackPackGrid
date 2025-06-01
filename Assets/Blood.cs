using UnityEngine;

public class Blood : IItemEffect
{
    public float TotalDamage = 1;
    public void ExecuteEffect(Enemy enemy)
    {
        float startingBaseDMG = 2;
        PlayerPrefs.SetFloat("FirstEnemyHealth", enemy._health);
        if (PlayerPrefs.HasKey("LastDMG"))
            TotalDamage = PlayerPrefs.GetInt("LastDMG");
        else
            TotalDamage = startingBaseDMG;

        if (enemy._health > 0)
        {
            enemy.TakeDamage(2);
        }
        

        PlayerPrefs.SetFloat("LastEnemyHealth", enemy._health);
        GameObject.Find("SelectedEnemy").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
        PlayerPrefs.DeleteKey("LastDMG");
        PlayerPrefs.SetFloat("BasedDMG", startingBaseDMG);
    }

    public void TourEffect(Enemy enemy)
    {
        Debug.Log("bbbbb");
    }


}


public class Sword : IItemEffect
{


    public float TotalDamage = 2;
    public void ExecuteEffect(Enemy enemy)
    {
        PlayerPrefs.SetFloat("FirstEnemyHealth", enemy._health);
        float startingBaseDMG = 2;
        if (PlayerPrefs.HasKey("BasedDMG"))
            TotalDamage = PlayerPrefs.GetInt("BasedDMG");
        else
            TotalDamage = startingBaseDMG;

        PlayerPrefs.SetFloat("FirstEnemyHealth", enemy._health);
        if (enemy._health > 0)
            enemy.TakeDamage(2);

        GameObject.Find("SelectedEnemy").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
        PlayerPrefs.SetFloat("LastEnemyHealth", enemy._health);
        PlayerPrefs.DeleteKey("BasedDMG");
        PlayerPrefs.SetFloat("LastDMG", TotalDamage);
    }

    public void TourEffect(Enemy enemy)
    {
        // throw new System.NotImplementedException();
    }
}


public class Bleeding : IPassive
{

    public void PassiveEffect(PlayerHandler player, Enemy enemy)
    {
       enemy.TakeDamage(1);
    }
}

public class AttackBuff : IPassive
{

    public void PassiveEffect(PlayerHandler player, Enemy enemy)
    {
        float DMGSteal = PlayerPrefs.GetFloat("LastDMG") + ((PlayerPrefs.GetFloat("LastDMG") / 100) * 10);
        PlayerPrefs.SetFloat("BasedDMG", DMGSteal);
    }
}

public class Burn : IPassive
{
    public void PassiveEffect(PlayerHandler player, Enemy enemy)
    {
        Debug.Log("Passive Damage Taken");
        enemy.TakeDamage(10);
        GameObject.Find("SelectedEnemy").GetComponent<SelectedEnemy>().selectedEnemy = null;

    }
}

public class LifeSteal : IPassive
{
    public void PassiveEffect(PlayerHandler player, Enemy enemy ) 
    {
        float DMGSteal = PlayerPrefs.GetFloat("LastDMG") + (((PlayerPrefs.GetFloat("FirstEnemyHealth")-PlayerPrefs.GetFloat("LastEnemyHealth")/100)*10));
        PlayerPrefs.SetFloat("BasedDMG", DMGSteal);
    }
}



