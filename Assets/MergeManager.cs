using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.IO;
public class MergeManager : MonoBehaviour
{
    public SerializedDictionary<string, BaseItem> _items = new();
    public HandledCards handledCardManager;

    public List<BaseItem> UpgradedMaterialAdd;
    void Start()
    {

    }
    // Update is called once per frame
  
    public void SaveAndSkip()
    {
        AddList();
        SaveData();
    }
    void AddList()
    {
        UpgradedMaterialAdd.Clear();

        HashSet<GameObject> upgradeContributorObjects = new HashSet<GameObject>();
        HashSet<(GameObject, BaseItem)> addedObjects = new HashSet<(GameObject, BaseItem)>();

        foreach (GameObject handleCard in handledCardManager.HandledObjects)
        {
            var handleInventory = handleCard.GetComponent<IInventoryObject>();
            var thisBaseItem = handleInventory.BaseItemObj;
            var thisObject = handleCard;
            var collideList = handleInventory.CollideList;

            List<(GameObject obj, BaseItem item)> collidePairs = collideList
                .Select(obj => (obj, obj.GetComponent<IInventoryObject>().BaseItemObj))
                .ToList();

            bool upgraded = false;

            foreach (var kvp in _items)
            {
                if (thisBaseItem != kvp.Value.RootMergeItem)
                    continue;

                foreach (BaseItem savedMaterial in kvp.Value.MergedItems)
                {
                    bool hasAll = savedMaterial.MergedItems.All(
                        requiredItem =>
                            collidePairs.Any(pair => pair.item == requiredItem)
                    );

                    if (hasAll)
                    {
                        UpgradedMaterialAdd.Add(kvp.Value);

                        upgradeContributorObjects.Add(thisObject);
                        foreach (var requiredItem in savedMaterial.MergedItems)
                        {
                            var contributorObject = collidePairs
                                .FirstOrDefault(pair => pair.item == requiredItem).obj;

                            if (contributorObject != null)
                                upgradeContributorObjects.Add(contributorObject);
                        }

                        upgraded = true;
                        break;
                    }
                }

                if (upgraded)
                    break;
            }

            if (!upgraded)
            {
                foreach (var (obj, item) in collidePairs)
                {
                    var key = (obj, item);
                    if (!upgradeContributorObjects.Contains(obj) && !addedObjects.Contains(key))
                    {
                        UpgradedMaterialAdd.Add(item);
                        addedObjects.Add(key);
                    }
                }

                var selfKey = (thisObject, thisBaseItem);
                if (!upgradeContributorObjects.Contains(thisObject) && !addedObjects.Contains(selfKey))
                {
                    UpgradedMaterialAdd.Add(thisBaseItem);
                    addedObjects.Add(selfKey);
                }
            }
        }

        Debug.Log("Toplam item havuzu:");
        foreach (var item in UpgradedMaterialAdd)
        {
            Debug.Log(item.name);
        }
    }
    void SaveData()
    {
        ObjectListClass SaverList = new ObjectListClass();

        for (int i = 0; i < UpgradedMaterialAdd.Count; i++)
        {
            SaverList.GameobjectCountLister.Add(UpgradedMaterialAdd[i].ItemName);
        }
        string json = JsonUtility.ToJson(SaverList);
        File.WriteAllText(Application.dataPath + "/SaveData.json", json);
    }
}
