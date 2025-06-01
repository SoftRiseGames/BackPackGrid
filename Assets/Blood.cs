using UnityEngine;

public class Blood : IItemEffect
{
    public float TotalDamage = 1;
    public void ExecuteEffect(Enemy enemy)
    {
        float startingBaseDMG = 2;
        if (PlayerPrefs.HasKey("LastDMG"))
            TotalDamage = PlayerPrefs.GetInt("LastDMG");
        else
            TotalDamage = startingBaseDMG;

        if (enemy._health > 0)
            enemy._health = enemy._health - TotalDamage;

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
        float startingBaseDMG = 2;
        if (PlayerPrefs.HasKey("BasedDMG"))
            TotalDamage = PlayerPrefs.GetInt("BasedDMG");
        else
            TotalDamage = startingBaseDMG;


        if (enemy._health > 0)
            enemy._health = enemy._health - TotalDamage;
        GameObject.Find("SelectedEnemy").GetComponent<SelectedEnemy>().selectedEnemy = enemy;
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

    public void PassiveEffect(PlayerHandler player, Enemy enemy, Collider2D collider)
    {
      
        collider.GetComponent<Enemy>()._health = collider.GetComponent<Enemy>()._health - 1;
    }
}

public class AttackBuff : IPassive
{

    public void PassiveEffect(PlayerHandler player, Enemy enemy,Collider2D collider)
    {

    }
}

public class Burn : IPassive
{

    public void PassiveEffect(PlayerHandler player, Enemy enemy, Collider2D collider)
    {
       
        Debug.Log("Passive Damage Taken");
        collider.GetComponent<Enemy>()._health = collider.GetComponent<Enemy>()._health - 10;

    }
}

public class LifeSteal : IPassive
{
    public void PassiveEffect(PlayerHandler player, Enemy enemy, Collider2D collider ) 
    {
        float DMGSteal = PlayerPrefs.GetFloat("LastDMG") + ((PlayerPrefs.GetFloat("LastDMG") / 100) * 10);
        PlayerPrefs.SetFloat("BasedDMG", DMGSteal);
    }
}



