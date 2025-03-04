using UnityEngine;
using System.Collections.Generic;
public class GridObjectSideChecker : MonoBehaviour
{
    public List<BaseItem> UpgradeObjects;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InvObject")
        {
            foreach (BaseItem i in UpgradeObjects)
            {
                if (collision.GetComponent<IInventoryObject>() is IPowerItem)
                {
                    if (collision.GetComponent<IPowerItem>().BaseItemObject == i)
                    {
                        ((IPowerItem)collision).PowerUpBuffs();
                    }
                    
                }
                
            }
        }
        else
            return;
    }

    void ObjectCheck()
    {

    }
}
