using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemies;
    private bool isEventRunning = false;
    public static Action onPlayerTurn;

   
    private void OnEnable()
    {
        EventManagerCode.OnEnemyTurn += StartCoroutineEvent;
    }
    private void OnDisable()
    {
        EventManagerCode.OnEnemyTurn -= StartCoroutineEvent;
    }
    
    
    void StartCoroutineEvent()
    {
        if (!isEventRunning)
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
        onPlayerTurn?.Invoke();
    }
}
