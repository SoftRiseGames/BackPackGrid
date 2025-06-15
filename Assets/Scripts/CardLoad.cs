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
        string json = File.ReadAllText(Application.dataPath + "/SaveData.json");
        ObjectListClass objectListClass = JsonUtility.FromJson<ObjectListClass>(json);

        foreach(string i in objectListClass.GameobjectCountLister)
        {
            LoadedObjectsList.Add(i);
        }
    }
}
