using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
public class Buttonloader : MonoBehaviour
{
    [SerializeField] List<string> EarnedItems;

    void Start()
    {
        
    }
    public async void AddedItem()
    {
        AudioManager.instance.SoundSfx(AudioManager.instance.audioClips[1]);
        Debug.Log("girdi");
        if(EarnedItems.Count > 0)
        {
            foreach (string i in EarnedItems)
            {
                JsonAppendSystem.AddStringItem(i);
            }
        }
        await Task.Delay(100);
        gameObject.SetActive(false);
    }

   
}
