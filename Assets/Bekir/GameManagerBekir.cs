using UnityEngine;
using UnityEngine.UI;

public class GameManagerBekir : MonoBehaviour{
    public CartHandler C_CartHandler;
    public PlayerHandler C_PlayerHandler;
    public int ManaCount;
    public static GameManagerBekir instance;
    [SerializeField] private Button TourButton;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }
    private void OnEnable()
    {
        EventManagerCode.OnEnemyTurn += ManaRenew;
        EventManagerCode.OnEnemyTurn += TourButtonClose;
        EnemyManager.onPlayerTurn += TourButtonOpen;
    }
    private void OnDisable()
    {
        EventManagerCode.OnEnemyTurn -= ManaRenew;
        EventManagerCode.OnEnemyTurn -= TourButtonClose;
        EnemyManager.onPlayerTurn -= TourButtonOpen;
    }
    void ManaRenew()
    {
        ManaCount = 3;
    }
    void TourButtonClose()
    {
        TourButton.interactable = false;
    }
    void TourButtonOpen()
    {
        TourButton.interactable = true;
    }
}