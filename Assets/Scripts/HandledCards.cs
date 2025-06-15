using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class HandledCards : MonoBehaviour
{
    public List<GameObject> HandledObjects;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            SaveData();
    }

    void SaveData()
    {
        ObjectListClass SaverList = new ObjectListClass();

        for(int i = 0; i<HandledObjects.Count; i++)
        {
            SaverList.GameobjectCountLister.Add(HandledObjects[i].GetComponent<IInventoryObject>().BaseItemObj.ItemName);
        }

        string json = JsonUtility.ToJson(SaverList);
        File.WriteAllText(Application.dataPath + "/SaveData.json", json);
    }
}

public class ObjectListClass
{
    public List<string> GameobjectCountLister = new List<string>();
}
