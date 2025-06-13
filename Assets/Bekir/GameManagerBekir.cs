using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using System.Collections.Generic;
public class GameManagerBekir : MonoBehaviour{
    public CartHandler C_CartHandler;
    public PlayerHandler C_PlayerHandler;
    public int ManaCount;
    public static GameManagerBekir instance;
    [SerializeField] private Button TourButton;
    [SerializeField] private GameObject DMGEffect;
    [SerializeField] private CinemachineImpulseSource impulse;

    [SerializeField] List<Image> ManaImages;
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
        EventManagerCode.DMGEffectAction += DMGEffectVoid;
        EventManagerCode.DMGEffectStopAction += DMGStopActionVoid;
    }
    private void OnDisable()
    {
        EventManagerCode.OnEnemyTurn -= ManaRenew;
        EventManagerCode.OnEnemyTurn -= TourButtonClose;
        EnemyManager.onPlayerTurn -= TourButtonOpen;
        EventManagerCode.DMGEffectAction -= DMGEffectVoid;
        EventManagerCode.DMGEffectStopAction -= DMGStopActionVoid;
    }
    private void Update()
    {
        ManaListControl();
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
    void ManaListControl()
    {
        for(int i = 0; i<ManaImages.Count; i++)
        {
            if (i < ManaCount)
                ManaImages[i].gameObject.SetActive(true);
            else
                ManaImages[i].gameObject.SetActive(false);
        }
    }
    void DMGStopActionVoid()
    {
        DMGEffect.SetActive(false);
    }
}