using UnityEngine;
using System.Collections.Generic;
public class GridObjectSideChecker : MonoBehaviour
{
    [SerializeField] GameObject ParentObj;
    public List<GameObject> DataController;
    public DataControllerHolder dataHolder;
    bool CanObjectAddable;
    void Start()
    {
        CanObjectAddable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InvObject" && CanObjectAddable == true && collision.GetComponent<IInventoryObject>().CanEnterPosition == false && ParentObj.GetComponent<IInventoryObject>().CanEnterPosition == false)
        {
            Debug.Log("GirdiGirdiGirdi");
            if(collision.gameObject.name == "Sword")
                Debug.Log(collision.GetComponent<IInventoryObject>().CanEnterPosition);

            if (!dataHolder.DataHolder.Contains(collision.gameObject))
            {
                ParentObj.GetComponent<IInventoryObject>().AddedMaterialsChecker.Add(collision.gameObject);
                dataHolder.DataHolder.Add(collision.gameObject);
            }
            CanObjectAddable = false;
        }
        else
            return;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
  
        if (collision.gameObject.tag == "InvObject" && CanObjectAddable == false && collision.GetComponent<IInventoryObject>().CanEnterPosition == false &&ParentObj.GetComponent<IInventoryObject>().CanEnterPosition == false)
        {
            ParentObj.GetComponent<IInventoryObject>().AddedMaterialsChecker.Remove(collision.gameObject);
            CanObjectAddable = true;
        }
        else
            return;
       
    }
    private void Update()
    {
        var addedMaterials = ParentObj.GetComponent<IInventoryObject>().AddedMaterialsChecker;
        if(dataHolder.DataHolder != null)
        {
            foreach (GameObject obj in new List<GameObject>(dataHolder.DataHolder))
            {
                if (!addedMaterials.Contains(obj))
                {
                    GameObject.Find("HandledCardManager").GetComponent<HandledCards>().HandledObjects.Add(obj.GetComponent<IInventoryObject>().BaseItemObj);
                    ParentObj.GetComponent<IInventoryObject>().AddedMaterialsChecker.Remove(obj);
                    dataHolder.DataHolder.Remove(obj);
                }
            }
        }
        
    }
    void ObjectCheck()
    {

    }
}
