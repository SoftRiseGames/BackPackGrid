using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
public class GameManagerBekir : MonoBehaviour
{
    public CartHandler C_CartHandler;
    public PlayerHandler C_PlayerHandler;
    public int ManaCount;
    public int MaxMana;
    public static GameManagerBekir instance;
    [SerializeField] private Button TourButton;
    [SerializeField] private GameObject DMGEffect;
    [SerializeField] private CinemachineImpulseSource impulse;
    [SerializeField] private TextMeshProUGUI OurMana;
    [SerializeField] private TextMeshProUGUI TotalMana;

    //[SerializeField] List<Image> ManaImages;
    public GameObject CollectGamematerial;
    private void Start()
    {
        ManaCount = MaxMana;
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
        EventManagerCode.WarisOver += WarOver;
    }
    private void OnDisable()
    {
        EventManagerCode.OnEnemyTurn -= ManaRenew;
        EventManagerCode.OnEnemyTurn -= TourButtonClose;
        EnemyManager.onPlayerTurn -= TourButtonOpen;
        EventManagerCode.DMGEffectAction -= DMGEffectVoid;
        EventManagerCode.DMGEffectStopAction -= DMGStopActionVoid;
        EventManagerCode.WarisOver -= WarOver;
    }
    private void Update()
    {
        ManaListControl();
    }
    void ManaRenew()
    {
        ManaCount = 3;
    }
    async void WarOver()
    {
        if (TourButton != null)
            TourButton.interactable = false;

        await Task.Delay(400);

        if (CollectGamematerial != null)
            CollectGamematerial.SetActive(true);
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
        //impulse.DefaultVelocity = new Vector2(Random.Range(-.2f,.2f),Random.Range(-.1f,.1f));
        //impulse.GenerateImpulse();
        DMGEffect.SetActive(true);
    }
    void ManaListControl()
    {
        OurMana.text = ManaCount.ToString();
        TotalMana.text = "/" + MaxMana.ToString();
    }
    void DMGStopActionVoid()
    {
        DMGEffect.SetActive(false);
    }
}