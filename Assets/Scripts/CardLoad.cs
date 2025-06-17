using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class CardLoad : MonoBehaviour
{
    public List<string> LoadedObjectsList;
    void Start()
    {
        LoadGuns();
    }
    void LoadGuns()
    {
        Debug.Log("LoadGuns");
#if UNITY_EDITOR
        string json = File.ReadAllText(Application.dataPath + "/SaveData.json");
#else
        string json = File.ReadAllText(Application.persistentDataPath + "/SaveData.json");
#endif
        ObjectListClass objectListClass = JsonUtility.FromJson<ObjectListClass>(json);

        foreach(string i in objectListClass.GameobjectCountLister)
        {
            LoadedObjectsList.Add(i);
        }
    }
}
