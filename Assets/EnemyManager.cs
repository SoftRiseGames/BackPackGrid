using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemies;
    private bool isEventRunning = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isEventRunning)
        {
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        isEventRunning = true;

        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy enemy = enemies[i];

            string currentAction = enemy.EnemySettings.EnemyPattern[enemy.EnemyPatternCounter];

            if (currentAction == "Attack")
                enemy.AttackOnTour();
            else if (currentAction == "Defence")
                enemy.DefenceOnTour();

            yield return new WaitForSeconds(0.5f); // Her düþman arasýnda 0.5 saniye bekle
        }
        

        isEventRunning = false;
    }
}
