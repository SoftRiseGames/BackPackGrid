using UnityEngine;
using System.Collections.Generic;
public class GridObjectSideChecker : MonoBehaviour
{
    [SerializeField] GameObject ParentObj;
    bool CanObjectAddable;
    void Start()
    {
        CanObjectAddable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InvObject" && CanObjectAddable == true)
        {
            ParentObj.GetComponent<IInventoryObject>().AddedMaterialsChecker.Add(collision.gameObject);
            CanObjectAddable = false;
        }
        else
            return;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InvObject" && CanObjectAddable == false)
        {
            ParentObj.GetComponent<IInventoryObject>().AddedMaterialsChecker.Remove(collision.gameObject);
            CanObjectAddable = true;
        }
        else
            return;
    }

    void ObjectCheck()
    {

    }
}
