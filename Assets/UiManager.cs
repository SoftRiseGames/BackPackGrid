using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
   
    public Slider PlayerHealthSlider;
    public PlayerHandler playerHealth;
   
    
    void Update()
    {
        PlayerHealthSlider.value = playerHealth._health;
    }
}
