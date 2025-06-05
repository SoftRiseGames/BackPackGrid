using UnityEngine;

public class GameManagerBekir : MonoBehaviour{
    public CartHandler C_CartHandler;
    public PlayerHandler C_PlayerHandler;
    public int ManaCount;
    public static GameManagerBekir instance;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }
    private void OnEnable()
    {
        EventManagerCode.OnEnemyTurn += ManaRenew;
    }
    private void OnDisable()
    {
        EventManagerCode.OnEnemyTurn -= ManaRenew;
    }
    void ManaRenew()
    {
        ManaCount = 3;
    }
}