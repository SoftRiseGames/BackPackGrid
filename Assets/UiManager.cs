using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    public Slider EnemyHealthSlider;
    public Slider PlayerHealthSlider;

    public Enemy enemyHealth;
    public PlayerHandler playerHealth;
   
    
    void Update()
    {
        EnemyHealthSlider.value = enemyHealth._health;
        PlayerHealthSlider.value = playerHealth._health;
    }
}
