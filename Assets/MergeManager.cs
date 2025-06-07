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

            // Bu objeyle çarpışan objeleri ve BaseItem'larını al
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
                    // Aynı item'ı birden çok kez saymayı önlemek için geçici liste
                    var availablePairs = new List<(GameObject obj, BaseItem item)>(collidePairs);
                    bool hasAll = true;

                    foreach (var requiredItem in savedMaterial.MergedItems)
                    {
                        var match = availablePairs.FirstOrDefault(p => p.item == requiredItem);
                        if (match == default)
                        {
                            hasAll = false;
                            break;
                        }
                        availablePairs.Remove(match);
                    }

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

            // Eğer upgrade gerçekleşmediyse, normal item'ları ekle
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