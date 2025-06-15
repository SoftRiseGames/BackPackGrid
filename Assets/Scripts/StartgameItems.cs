using UnityEngine;
using System.IO;
public class StartgameItems : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_EDITOR
        if(!File.Exists(Application.dataPath + "/" + "BaseItemData.json"))
        {
            JsonAppendSystem.AddStringItem("Shield");
            JsonAppendSystem.AddStringItem("Sword");
            JsonAppendSystem.AddStringItem("Knife");

        }
#else 
if(!File.Exists(Application.persistentDataPath + "/" + "BaseItemData.json"))
        {
            JsonAppendSystem.AddStringItem("Shield");
            JsonAppendSystem.AddStringItem("Sword");
            JsonAppendSystem.AddStringItem("Knife");

        }
#endif

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
