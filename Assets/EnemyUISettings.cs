using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyUISettings : MonoBehaviour
{
    [SerializeField] Enemy ThisEnemy;
    [SerializeField] bool isShield;
    [SerializeField] bool isHealth;

    [SerializeField] TextMeshProUGUI ShieldText;
    void Start()
    {
        if (isHealth)
            gameObject.GetComponent<Slider>().value = ThisEnemy._health;
        else
            ShieldText.text = ThisEnemy._shield.ToString();
    }

    
    void Update()
    {
        if(isHealth)
            gameObject.GetComponent<Slider>().value = ThisEnemy._health;
        else
            ShieldText.text = ThisEnemy._shield.ToString();
    }
}
