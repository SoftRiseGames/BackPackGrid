using UnityEngine;
using System.IO;

public class StartGame : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
#if UNITY_EDITOR
        EditorSil("Upgrade_log.json");
        EditorSil("SaveData.json");
#else
        OyunSil("Upgrade_log.json");
        OyunSil("SaveData.json")
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EditorSil(string fileName)
    {
        Debug.Log("girdi");
        string path = Application.dataPath + "/"+fileName;
        if(File.Exists(path))
            File.Delete(path);

    }
    void OyunSil(string fileName)
    {
        string path = Application.persistentDataPath+"/" + fileName;

        if (File.Exists(path))
            File.Delete(path);
    }
}
