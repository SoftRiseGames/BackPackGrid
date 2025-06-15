using UnityEngine;

public class StartgameItems : MonoBehaviour
{
    private void Awake()
    {
        JsonAppendSystem.AddStringItem("Shield");
        JsonAppendSystem.AddStringItem("Sword");
        JsonAppendSystem.AddStringItem("Knife");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
