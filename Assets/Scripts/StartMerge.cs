using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
public class StartMerge : MonoBehaviour
{
    [SerializeField] List<GameObject> PoolObjects;


    public List<string> upgraded;
    public List<string> deleted;

    private void Awake()
    {
       
    }
    void Start()
    {
        ApplySavedItemsToPool();
        LoadUpgradeLists(out upgraded, out deleted);
        ListChecker();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ApplySavedItemsToPool()
    {
        // Json dosyasından kayıtlı string item listesi alınır
        List<string> savedItems = JsonAppendSystem.GetAllItems();

        // 1. Her item'in kaç adet gerektiğini say
        Dictionary<string, int> neededCounts = new();
        foreach (string item in savedItems)
        {
            if (!neededCounts.ContainsKey(item))
                neededCounts[item] = 0;
            neededCounts[item]++;
        }

        // 2. Sahnedeki objeleri tek tek kontrol et
        Dictionary<string, int> activatedSoFar = new();

        foreach (GameObject obj in PoolObjects)
        {
            string pureName = obj.name.Replace("(Clone)", "").Trim();

            if (!neededCounts.ContainsKey(pureName))
            {
                obj.SetActive(false); // İhtiyaç yoksa kapat
                continue;
            }

            if (!activatedSoFar.ContainsKey(pureName))
                activatedSoFar[pureName] = 0;

            if (activatedSoFar[pureName] < neededCounts[pureName])
            {
                obj.SetActive(true);  // Gereken kadarını aç
                activatedSoFar[pureName]++;
            }
            else
            {
                obj.SetActive(false); // Fazlaysa kapat
            }
        }
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
    public void LoadUpgradeLists(out List<string> upgradedList, out List<string> toBeDeletedList)
    {
        upgradedList = new List<string>();
        toBeDeletedList = new List<string>();

        UpgradeLogData logData = LoadUpgradeLog();

        if (logData == null || logData.upgradeLogs == null || logData.upgradeLogs.Count == 0)
        {
            Debug.LogWarning("Upgrade log boş, listeler boş dönülüyor.");
            return;
        }

        foreach (var entry in logData.upgradeLogs)
        {
            if (entry == null) continue;

            // ✅ Upgrade edilen item
            if (!string.IsNullOrEmpty(entry.upgradedToName))
                upgradedList.Add(entry.upgradedToName);

            // ❌ Temas eden objeler (collider objeler)
            if (entry.touchingObjectNames != null && entry.touchingObjectNames.Count > 0)
            {
                foreach (var name in entry.touchingObjectNames)
                {
                    if (!string.IsNullOrEmpty(name))
                        toBeDeletedList.Add(name);
                }
            }

            // ❌ Ana obje de silinecekler listesine ekleniyor
            if (!string.IsNullOrEmpty(entry.mainObjectName))
                toBeDeletedList.Add(entry.mainObjectName);
        }
    }

    void ListChecker()
    {
        // 1. deleted listesindeki her ismin kaç defa silineceğini say
        Dictionary<string, int> deleteCounts = new();
        foreach (string name in deleted)
        {
            if (!deleteCounts.ContainsKey(name))
                deleteCounts[name] = 0;
            deleteCounts[name]++;
        }

        // 2. Sahnedeki silinen obje sayısını takip et
        Dictionary<string, int> deletedSoFar = new();

        foreach (GameObject b in PoolObjects)
        {
            string pureName = b.name.Replace("(Clone)", "").Trim();

            // Silme işlemi
            if (deleteCounts.ContainsKey(pureName))
            {
                if (!deletedSoFar.ContainsKey(pureName))
                    deletedSoFar[pureName] = 0;

                if (deletedSoFar[pureName] < deleteCounts[pureName])
                {
                    b.SetActive(false);
                    deletedSoFar[pureName]++;
                    continue; // Silindiyse artık upgrade edilmesine gerek yok
                }
            }

            // Aktif etme işlemi
            if (upgraded.Contains(pureName))
            {
                b.SetActive(true);
            }
        }

    }


}