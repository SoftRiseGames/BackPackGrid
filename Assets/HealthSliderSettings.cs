using UnityEngine;
using UnityEngine.UI;
public class HealthSliderSettings : MonoBehaviour
{
    [SerializeField] Enemy ThisEnemy;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Slider>().value = ThisEnemy._health;
    }
}
