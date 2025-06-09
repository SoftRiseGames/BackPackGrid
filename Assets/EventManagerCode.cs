using UnityEngine;
using System.Threading.Tasks;
using System;
public class EventManagerCode : MonoBehaviour
{

    public static Action OnEnemyTurn;
    public static Action DMGEffectAction;
    public static Action DMGEffectStopAction;
    public void OnEnemyTurnVoid() => OnEnemyTurn.Invoke();

    public void PrefDeleter()
    {
        PlayerPrefs.DeleteKey("FirstEnemyHealth");
        PlayerPrefs.DeleteKey("LastEnemyHealth");
    }
}
