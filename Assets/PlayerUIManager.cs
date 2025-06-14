using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUIManager : MonoBehaviour
{
   
    public Slider PlayerHealthSlider;
    public PlayerHandler playerData;
    public TextMeshProUGUI ShieldText;
    public PlayerHandler player;
   
    
    void Update()
    {
        PlayerHealthSlider.value = playerData._health;
        ShieldText.text = player._shield.ToString();

    }
}
