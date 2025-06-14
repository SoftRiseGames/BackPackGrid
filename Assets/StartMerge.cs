using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
public class StartMerge : MonoBehaviour
{
    [SerializeField] List<GameObject> PoolObjects;


    public List<string> upgraded;
    public List<string> deleted;

    
    void Start()
    {
        LoadUpgradeLists(out upgraded, out deleted);
        ListChecker();
    }

    // Update is called once per frame
    void Update()
    {

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

            if (!string.IsNullOrEmpty(entry.upgradedToName))
                upgradedList.Add(entry.upgradedToName);

            if (entry.touchingObjectNames != null && entry.touchingObjectNames.Count > 0)
            {
                foreach (var name in entry.touchingObjectNames)
                {
                    if (!string.IsNullOrEmpty(name))
                        toBeDeletedList.Add(name); 
                }
            }
        }
    }

    void ListChecker()
    {
        foreach (GameObject b in PoolObjects)
        {
            //Debug.Log(b.name);
            foreach (string i in upgraded)
            {
                if (i == b.name)
                    b.SetActive(true);
            }

            foreach(string a in deleted)
            {
                Debug.Log(a);
                if(a == b.name)
                {
                    b.SetActive(false);
                }
                   
            }
            
        }
       

    }


}