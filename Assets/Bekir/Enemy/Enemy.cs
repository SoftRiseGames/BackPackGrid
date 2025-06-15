using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour, IDamage
{
    public float _health;
    public float _shield;
    public SOEnemy EnemySettings;
    [SerializeField] private PlayerHandler PlayerObject;
    [HideInInspector]public int EnemyPatternCounter;
    [SerializeField] Slider HealthSlider;
    
    public int Order;

    public bool isBurning;
    public bool isBleeding;


    [SerializeField] private List<Sprite> TurnIconImageList;
    [SerializeField] private Image TurnIconImageSet;
    [SerializeField] TextMeshProUGUI DMGText;
    public TextMeshProUGUI BleedingTourText;
    public TextMeshProUGUI BurningTourText;


    public Image BleedingEffect;
    public Image BurnEffect;

    public Image OrderImage;

    public ParticleSystem ShieldParticle;

    private void Start()
    {
        if (EnemySettings.EnemyPattern[EnemyPatternCounter] == "Attack")
            TurnIconImageSet.sprite = TurnIconImageList[0];
        else if (EnemySettings.EnemyPattern[EnemyPatternCounter] == "Defence")
            TurnIconImageSet.sprite = TurnIconImageList[1];
        
        
        _health = EnemySettings.HealthMaxValue;
        HealthSlider.maxValue = EnemySettings.HealthMaxValue;
        _shield = EnemySettings.Shield;
    }

    private void Update()
    {
        if (isBleeding)
            BleedingEffect.gameObject.SetActive(true);
        else
            BleedingEffect.gameObject.SetActive(false);

        if (isBurning)
            BurnEffect.gameObject.SetActive(true);
        else
            BurnEffect.gameObject.SetActive(false);

       
    }
    public void Die()
    {
        EnemyManager.instance.enemies.Remove(gameObject.GetComponent<Enemy>());
        Debug.Log("I am Dead");
        gameObject.SetActive(false);
    }

    public void AttackOnTour()
    {
        _shield = 0;
        EnemySettings.EnemyEffects?.ForEach(effect => effect?.AttackOnTour(PlayerObject,gameObject.GetComponent<Enemy>()));
        EnemyPatternCounterManager();
       
    }

    public void DefenceOnTour()
    {
        _shield = 0;
        EnemySettings.EnemyEffects?.ForEach(effect => effect?.DefenceOnTour(PlayerObject,gameObject.GetComponent<Enemy>()));
        EnemyPatternCounterManager();
       
    }

   
    void EnemyPatternCounterManager()
    {
        EnemyPatternCounter = EnemyPatternCounter + 1;

        if (EnemyPatternCounter >= EnemySettings.EnemyPattern.Count)
        {
            EnemyPatternCounter = 0;
        }

        if (EnemySettings.EnemyPattern[EnemyPatternCounter] == "Attack")
        {
            TurnIconImageSet.sprite = TurnIconImageList[0];
            DMGText.text = EnemySettings.TakenDamage.ToString();
        }
            
        else if (EnemySettings.EnemyPattern[EnemyPatternCounter] == "Defence")
        {
           
            TurnIconImageSet.sprite = TurnIconImageList[1];
            DMGText.text = EnemySettings.EarnShield.ToString();
        }
          
    }

    public void PassiveAttackOnTour()
    {
        return;
    }

    public void TakeDamageWithoutShield(float value)
    {
        _health = _health - value;
        if (_health <= 0)
            Die();
    }

    public void TakeDamageWithShield(float value)
    {
        float shieldScaler = _shield;
        shieldScaler = shieldScaler - value;
        _shield = shieldScaler;

        if (_shield < 0)
            _shield = 0;

        
        if (shieldScaler < 0)
            _health = _health - Mathf.Abs(shieldScaler);

        if (_health <= 0)
            Die();
    }

    public void EarnShield(float value)
    {
        if (_shield < EnemySettings.shieldMaxValue)
            _shield = _shield + value;
        else if (_shield > EnemySettings.shieldMaxValue)
            _shield = EnemySettings.shieldMaxValue;

    }
}
