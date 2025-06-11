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
        //AddList();

        AddList2();
        SaveData();
    }


    void AddList2()
    {
        UpgradedMaterialAdd.Clear();
        HashSet<GameObject> alreadyProcessedObjects = new HashSet<GameObject>();

        // 1. aşama: Önce merge'e uygun objeler
        foreach (GameObject handleCard in handledCardManager.HandledObjects)
        {
            if (alreadyProcessedObjects.Contains(handleCard))
                continue;

            var inventoryObj = handleCard.GetComponent<IInventoryObject>();

            foreach (var kvp in _items)
            {
                if (inventoryObj.BaseItemObj == kvp.Value.RootMergeItem)
                {
                    List<GameObject> collideObjects = inventoryObj.CollideList;
                    List<BaseItem> mergedItems = kvp.Value.MergedItems;
                    List<BaseItem> collideBaseItems = new List<BaseItem>();
                    Dictionary<BaseItem, GameObject> baseItemToObject = new Dictionary<BaseItem, GameObject>();

                    foreach (GameObject obj in collideObjects)
                    {
                        var baseItem = obj.GetComponent<IInventoryObject>().BaseItemObj;
                        collideBaseItems.Add(baseItem);
                        if (!baseItemToObject.ContainsKey(baseItem))
                            baseItemToObject[baseItem] = obj;
                    }

                    bool allRequiredItemsFound = false;
                    List<BaseItem> tempMerged = new List<BaseItem>(mergedItems);
                    List<GameObject> matchedCollideObjects = new List<GameObject>();

                    foreach (BaseItem item in tempMerged.ToList())
                    {
                        bool found = false;
                        for (int i = 0; i < collideBaseItems.Count; i++)
                        {
                            if (collideBaseItems[i] == item)
                            {
                                GameObject matchedObj = baseItemToObject[item];
                                matchedCollideObjects.Add(matchedObj);
                                collideBaseItems.RemoveAt(i);
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            allRequiredItemsFound = false;
                            break;
                        }
                        else
                        {
                            allRequiredItemsFound = true;
                        }
                    }

                    if (allRequiredItemsFound && matchedCollideObjects.Count == mergedItems.Count)
                    {
                        UpgradedMaterialAdd.Add(kvp.Value);
                        alreadyProcessedObjects.Add(handleCard);

                        foreach (var matchedObj in matchedCollideObjects)
                        {
                            alreadyProcessedObjects.Add(matchedObj);
                        }

                        foreach (GameObject obj in collideObjects)
                        {
                            if (!matchedCollideObjects.Contains(obj) && !alreadyProcessedObjects.Contains(obj))
                            {
                                UpgradedMaterialAdd.Add(obj.GetComponent<IInventoryObject>().BaseItemObj);
                                alreadyProcessedObjects.Add(obj);
                            }
                        }

                        break; // Başka kombinasyona bakmaya gerek yok
                    }
                }
            }
        }

        // 2. aşama: Upgrade'e katılamayanları sıradan şekilde işle
        foreach (GameObject handleCard in handledCardManager.HandledObjects)
        {
            if (alreadyProcessedObjects.Contains(handleCard))
                continue;

            var inventoryObj = handleCard.GetComponent<IInventoryObject>();
            UpgradedMaterialAdd.Add(inventoryObj.BaseItemObj);
            alreadyProcessedObjects.Add(handleCard);
        }
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

            // Bu objeyle çarpışan objelerin BaseItem'larını sırayla al
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
                    var requiredItems = savedMaterial.MergedItems;
                    var collideItems = collidePairs.Select(p => p.item).ToList();

                    // Sayı kontrolü: yeterli item yoksa devam et
                    if (collideItems.Count < requiredItems.Count)
                        continue;

                    // Sıralı karşılaştırma
                    bool hasAllInOrder = true;
                    for (int i = 0; i < requiredItems.Count; i++)
                    {
                        if (collideItems[i] != requiredItems[i])
                        {
                            hasAllInOrder = false;
                            break;
                        }
                    }

                    if (hasAllInOrder)
                    {
                        UpgradedMaterialAdd.Add(kvp.Value);
                        upgradeContributorObjects.Add(thisObject);

                        // Contributer objeleri topla
                        for (int i = 0; i < requiredItems.Count; i++)
                        {
                            var contributorObject = collidePairs[i].obj;
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

            // Eğer upgrade yoksa, normal BaseItem'ları ekle
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