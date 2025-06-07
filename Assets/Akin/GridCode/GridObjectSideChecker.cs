using UnityEngine;
using System.Collections.Generic;
public class GridObjectSideChecker : MonoBehaviour
{
    [SerializeField] GameObject ParentObj;
    public DataControllerHolder dataHolder;
    bool CanObjectAddable;
    public bool isObjectTrueDedect;
    public GameObject colliderNew;
    void Start()
    {
        CanObjectAddable = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InvObject")
        {
            isObjectTrueDedect = true;
           
        }
        else
            isObjectTrueDedect = false;


        if (isObjectTrueDedect)
            colliderNew = collision.gameObject;

      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InvObject")
        {
            isObjectTrueDedect = false;
        }
    }
    private void Update()
    {
        if (isObjectTrueDedect == true)
        {
            Debug.Log("Girdi");
            Debug.Log(colliderNew.GetComponent<IInventoryObject>().gridEnter);
            if (colliderNew.GetComponent<IInventoryObject>().gridEnter)
            {
                if (!ParentObj.GetComponent<IInventoryObject>().CollideList.Contains(colliderNew.gameObject))
                {
                    ParentObj.GetComponent<IInventoryObject>().CollideList.Add(colliderNew.gameObject);
                }
            }
           
        }
       

        else if (isObjectTrueDedect == false && colliderNew != null)
        {
            if (ParentObj.GetComponent<IInventoryObject>().CollideList.Contains(colliderNew.gameObject))
            {
                ParentObj.GetComponent<IInventoryObject>().CollideList.Remove(colliderNew.gameObject);
            }
            colliderNew = null;
        }
        else
            return;
    }
    void ObjectCheck()
    {

    }
}
