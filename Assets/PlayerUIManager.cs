using UnityEngine;
using UnityEngine.UI;
public class PlayerUIManager : MonoBehaviour
{
   
    public Slider PlayerHealthSlider;
    public PlayerHandler playerData;
    public PlayerHandler playerShield;
    public Slider PlayerShieldSlider;
   
    
    void Update()
    {
        PlayerHealthSlider.value = playerData._health;
        PlayerShieldSlider.value = playerData.shield;
    }
}
