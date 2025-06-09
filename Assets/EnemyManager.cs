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
        if (enemies != null)
        {
            for (int i = 0; i < enemies.Count; i++)
            {

                yield return new WaitForSeconds(0.5f);

                Enemy enemy = enemies[i];

                string currentAction = enemy.EnemySettings.EnemyPattern[enemy.EnemyPatternCounter];

                if (currentAction == "Attack")
                {
                    EventManagerCode.DMGEffectAction.Invoke();
                    enemy.AttackOnTour();
                }

                else if (currentAction == "Defence")
                    enemy.DefenceOnTour();
                yield return new WaitForSeconds(0.5f);
                EventManagerCode.DMGEffectStopAction.Invoke();
            }
            yield return new WaitForSeconds(.5f);
            isEventRunning = false;
            onPlayerTurn?.Invoke();
        }
       
    }
        
       
       
}
