using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonAppendSystem
{
#if UNITY_EDITOR
    private static readonly string basePath = Application.dataPath;
#else
    private static readonly string basePath = Application.persistentDataPath;
#endif

    private static readonly string filePath = Path.Combine(basePath, "BaseItemData.json");

    [System.Serializable]
    private class StringListWrapper
    {
        public List<string> list = new();
    }

    public static void AddStringItem(string item)
    {
        if (string.IsNullOrWhiteSpace(item))
        {
            Debug.LogWarning("Boş ya da geçersiz öğe eklendi.");
            return;
        }

        StringListWrapper wrapper = LoadWrapper();
        wrapper.list.Add(item);

        string newJson = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(filePath, newJson);

        Debug.Log($"Yeni öğe eklendi: \"{item}\" → {filePath}");
    }

    private static StringListWrapper LoadWrapper()
    {
        if (!File.Exists(filePath))
            return new StringListWrapper();

        string json = File.ReadAllText(filePath);
        StringListWrapper wrapper = JsonUtility.FromJson<StringListWrapper>(json);
        return wrapper ?? new StringListWrapper();
    }

    public static List<string> GetAllItems()
    {
        return LoadWrapper().list;
    }
}