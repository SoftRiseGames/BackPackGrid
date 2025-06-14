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
    string GetSavePath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/upgrade_log.json";
#else
    return Application.persistentDataPath + "/upgrade_log.json";
#endif
    }

    void SaveUpgradeLog(UpgradeLogData data)
    {
        string path = GetSavePath();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    UpgradeLogData LoadUpgradeLog()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<UpgradeLogData>(json);
        }
        return new UpgradeLogData();
    }

    void AddList2()
    {
        UpgradedMaterialAdd.Clear();
        HashSet<GameObject> alreadyProcessedObjects = new HashSet<GameObject>();

        UpgradeLogData logData = LoadUpgradeLog();

        foreach (GameObject handleCard in handledCardManager.HandledObjects)
        {
            if (alreadyProcessedObjects.Contains(handleCard))
                continue;

            var inventoryObj = handleCard.GetComponent<IInventoryObject>();

            foreach (var kvp in _items)
            {
                if (inventoryObj.BaseItemObj != kvp.Value.RootMergeItem)
                    continue;

                List<GameObject> collideObjects = inventoryObj.CollideList;
                List<BaseItem> mergedItems = kvp.Value.MergedItems;

                List<BaseItem> availableItems = new List<BaseItem>
            {
                inventoryObj.BaseItemObj
            };
                Dictionary<BaseItem, List<GameObject>> itemToGameObjects = new();

                foreach (GameObject obj in collideObjects)
                {
                    var baseItem = obj.GetComponent<IInventoryObject>().BaseItemObj;
                    availableItems.Add(baseItem);

                    if (!itemToGameObjects.ContainsKey(baseItem))
                        itemToGameObjects[baseItem] = new List<GameObject>();

                    itemToGameObjects[baseItem].Add(obj);
                }

                List<BaseItem> requiredItems = new List<BaseItem>(mergedItems);
                List<GameObject> usedColliders = new List<GameObject>();
                bool allMatch = true;

                foreach (BaseItem req in requiredItems.ToList())
                {
                    if (availableItems.Contains(req))
                    {
                        availableItems.Remove(req);
                        requiredItems.Remove(req);

                        if (itemToGameObjects.ContainsKey(req) && itemToGameObjects[req].Count > 0)
                        {
                            GameObject go = itemToGameObjects[req][0];
                            usedColliders.Add(go);
                            itemToGameObjects[req].RemoveAt(0);
                        }
                    }
                    else
                    {
                        allMatch = false;
                        break;
                    }
                }

                if (allMatch && requiredItems.Count == 0)
                {
                    UpgradedMaterialAdd.Add(kvp.Value);
                    alreadyProcessedObjects.Add(handleCard);

                    foreach (var go in usedColliders)
                        alreadyProcessedObjects.Add(go);

                    foreach (GameObject obj in collideObjects)
                    {
                        if (!usedColliders.Contains(obj) && !alreadyProcessedObjects.Contains(obj))
                        {
                            UpgradedMaterialAdd.Add(obj.GetComponent<IInventoryObject>().BaseItemObj);
                            alreadyProcessedObjects.Add(obj);
                        }
                    }

                    // ✅ JSON LOG ENTRY OLUŞTUR
                    UpgradeLogEntry entry = new UpgradeLogEntry();
                    entry.mainObjectName = inventoryObj.BaseItemObj.name;
                    entry.upgradedToName = kvp.Value.name;
                    entry.touchingObjectNames = usedColliders
                        .Select(go => go.GetComponent<IInventoryObject>().BaseItemObj.name)
                        .ToList();

                    logData.upgradeLogs.Add(entry);
                    SaveUpgradeLog(logData);

                    break;
                }
            }
        }

        foreach (GameObject handleCard in handledCardManager.HandledObjects)
        {
            if (alreadyProcessedObjects.Contains(handleCard))
                continue;

            UpgradedMaterialAdd.Add(handleCard.GetComponent<IInventoryObject>().BaseItemObj);
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

[System.Serializable]
public class UpgradeLogEntry
{
    public string mainObjectName;              // Ana obje
    public List<string> touchingObjectNames;   // Temas eden objeler (upgrade için kullanılanlar)
    public string upgradedToName;              // Ortaya çıkan upgrade'li item ismi
}

[System.Serializable]
public class UpgradeLogData
{
    public List<UpgradeLogEntry> upgradeLogs = new();
}