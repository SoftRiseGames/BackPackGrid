using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance; 
    public List<Enemy> enemies;
    private bool isEventRunning = false;
    public static Action onPlayerTurn;
    public static Action DMGEffectAction;
    public static Action DMGEffectStopAction;
    Coroutine CoroutineTimerControl;
    private void OnEnable()
    {
        EventManagerCode.OnEnemyTurn += StartCoroutineEvent;
    }
    private void OnDisable()
    {
        EventManagerCode.OnEnemyTurn -= StartCoroutineEvent;
    }

    private void Start()
    {
        if (instance == null)
            instance = this;

       
    }

    private void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Order = i;
        }
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

        // Liste null mu ya da boþ mu kontrolü
        if (enemies != null && enemies.Count > 0)
        {
            // Kopyasýný alýyoruz. Böylece orijinal liste deðiþse bile hata almayýz.
            List<Enemy> enemiesCopy = new List<Enemy>(enemies);

            for (int i = 0; i < enemiesCopy.Count; i++)
            {
                yield return new WaitForSeconds(0.5f);

                Enemy enemy = enemiesCopy[i];
                if (enemy == null) continue;

                if (enemy.EnemySettings.EnemyPattern.Count > enemy.EnemyPatternCounter)
                {
                    string currentAction = enemy.EnemySettings.EnemyPattern[enemy.EnemyPatternCounter];

                    if (currentAction == "Attack")
                    {
                        EventManagerCode.DMGEffectAction?.Invoke();
                        enemy.AttackOnTour();
                    }
                    else if (currentAction == "Defence")
                    {
                        enemy.DefenceOnTour();
                    }
                }

                yield return new WaitForSeconds(0.5f);
                EventManagerCode.DMGEffectStopAction?.Invoke();
            }
        }

        yield return new WaitForSeconds(0.5f);
        isEventRunning = false;
        onPlayerTurn?.Invoke();
    }



}
