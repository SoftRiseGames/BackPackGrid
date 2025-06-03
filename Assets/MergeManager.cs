using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using System.Linq;
public class MergeManager : MonoBehaviour
{
    public SerializedDictionary<string, BaseItem> _items = new();
    public HandledCards handledCardManager;

    public List<BaseItem> UpgradedMaterialAdd;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            UpgradedMaterialAdd.Clear();

            HashSet<GameObject> upgradeContributorObjects = new HashSet<GameObject>(); // Artık BaseItem değil, GameObject
            HashSet<GameObject> addedObjects = new HashSet<GameObject>(); // Aynı objeyi 2 kez eklememek için (isteğe bağlı)

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
                            // 1. Upgrade sonucu eklensin
                            UpgradedMaterialAdd.Add(kvp.Value);

                            // 2. Upgrade’e katkı sağlayan GameObject’leri işaretle
                            upgradeContributorObjects.Add(thisObject); // root objeyi işaretle
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
                        if (!upgradeContributorObjects.Contains(obj))
                        {
                            UpgradedMaterialAdd.Add(item);
                        }
                    }

                    if (!upgradeContributorObjects.Contains(thisObject))
                    {
                        UpgradedMaterialAdd.Add(thisBaseItem);
                    }
                }
            }

            Debug.Log("Toplam item havuzu:");
            foreach (var item in UpgradedMaterialAdd)
            {
                Debug.Log(item.name);
            }
        }


    }
}
