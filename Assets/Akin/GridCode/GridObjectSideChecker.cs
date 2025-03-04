using UnityEngine;
using System.Collections.Generic;
public class GridObjectSideChecker : MonoBehaviour
{
    public List<GameObject> UpgradeObjects;
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
            foreach (GameObject i in UpgradeObjects)
            {
                if (collision.gameObject == i)
                {
                    if(i.GetComponent<IInventoryObject>() is IPowerItem)
                    {
                        ((IPowerItem)i.GetComponent<IInventoryObject>()).PowerUpBuffs();
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
