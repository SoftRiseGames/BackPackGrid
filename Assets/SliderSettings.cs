using UnityEngine;
using UnityEngine.UI;
public class SliderSettings : MonoBehaviour
{
    [SerializeField] Enemy ThisEnemy;
    [SerializeField] bool isShield;
    [SerializeField] bool isHealth;
    void Start()
    {

    }

    
    void Update()
    {
        if(isHealth)
            gameObject.GetComponent<Slider>().value = ThisEnemy._health;
        else
            gameObject.GetComponent<Slider>().value = ThisEnemy._shield;
    }
}
