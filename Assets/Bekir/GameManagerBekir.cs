using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
public class GameManagerBekir : MonoBehaviour{
    public CartHandler C_CartHandler;
    public PlayerHandler C_PlayerHandler;
    public int ManaCount;
    public static GameManagerBekir instance;
    [SerializeField] private Button TourButton;
    [SerializeField] private GameObject DMGEffect;
    [SerializeField] private CinemachineImpulseSource impulse;
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
        EnemyManager.DMGEffectAction += DMGEffectVoid;
        EnemyManager.DMGEffectStopAction += DMGStopActionVoid;
    }
    private void OnDisable()
    {
        EventManagerCode.OnEnemyTurn -= ManaRenew;
        EventManagerCode.OnEnemyTurn -= TourButtonClose;
        EnemyManager.onPlayerTurn -= TourButtonOpen;
        EnemyManager.DMGEffectAction -= DMGEffectVoid;
        EnemyManager.DMGEffectStopAction -= DMGStopActionVoid;
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
    void DMGEffectVoid()
    {
        impulse.DefaultVelocity = new Vector2(Random.Range(-.2f,.2f),Random.Range(-.1f,.1f));
        impulse.GenerateImpulse();
        DMGEffect.SetActive(true);
    }

    void DMGStopActionVoid()
    {
        DMGEffect.SetActive(false);
    }
}