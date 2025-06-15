using UnityEngine;
using System.Collections.Generic;
public class Buttonloader : MonoBehaviour
{
    [SerializeField] List<string> EarnedItems;

    void Start()
    {
        
    }
    public void AddedItem()
    {
        Debug.Log("girdi");
        if(EarnedItems.Count > 0)
        {
            foreach (string i in EarnedItems)
            {
                JsonAppendSystem.AddStringItem(i);
            }
        }
       
        gameObject.SetActive(false);
    }

   
}
