using UnityEngine;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemies;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            Event();
    }

    void Event()
    {
        
        for(int i = 0; i<enemies.Count; i++)
        {
            if(enemies[i].EnemySettings.EnemyPattern[enemies[i].EnemyPatternCounter] == "Attack")
                enemies[i].GetComponent<Enemy>().AttackOnTour();
            else if (enemies[i].EnemySettings.EnemyPattern[enemies[i].EnemyPatternCounter] == "Defence")
                enemies[i].GetComponent<Enemy>().DefenceOnTour();
        }
        
    }
   
}
