#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ItemCreatorWindow sinifi, ScriptableObject (BaseItem) yaratan ve birlestiren editor penceresini olusturur.
/// </summary>
public class ItemCreatorWindow : EditorWindow
{
    private BaseItem rootMergeItem; // Tekli item (en ustteki)
    private List<BaseItem> mergeItemList = new List<BaseItem>(); // Liste icerisindeki itemlar
    private string newItemName = "New Item"; // Yeni item ismi
    private string newItemDescription = ""; // Yeni item aciklamasi
    private Sprite newIcon; // Yeni item ikonu
    private string savePath = "Assets/"; // Kayit yolu

    private int selectedTab = 0; // Sekme icin secili indeks

    /// <summary>
    /// Advanced Item Creator penceresini acar.
    /// </summary>
    [MenuItem("Item Creator/Advanced Item Creator")]
    public static void ShowWindow()
    {
        GetWindow<ItemCreatorWindow>("Advanced Item Creator");
    }

    /// <summary>
    /// Pencerenin ana cizim fonksiyonudur. Ustte toolbar ve secili sekmeye gore icerik cizer.
    /// </summary>
    void OnGUI()
    {
        // Ust Panel icin cubuk
        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

        if (GUILayout.Toggle(selectedTab == 0, "Main Page", EditorStyles.toolbarButton))
        {
            selectedTab = 0;
        }

        if (GUILayout.Toggle(selectedTab == 1, "Create Item Page", EditorStyles.toolbarButton))
        {
            selectedTab = 1;
        }

        EditorGUILayout.EndHorizontal();

        // Secili sekmeye gore ilgili UI'i cizer
        EditorGUILayout.Space(10);

        if (selectedTab == 0)
        {
            DrawMainPage();
        }
        else if (selectedTab == 1)
        {
            DrawCreateItemPage();
        }

        // Her sayfanin en altinda olusturulan ScriptableObject'leri listeler
        EditorGUILayout.Space(20);
        DrawCreatedItemsList();
    }

    /// <summary>
    /// Main Page icerisindeki icerikleri cizer.
    /// </summary>
    void DrawMainPage()
    {
        // Ana Sayfa
        EditorGUILayout.LabelField("Item Management", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        // Tekli Item alani (ustteki)
        EditorGUILayout.LabelField("Root Merge Item", EditorStyles.boldLabel);
        rootMergeItem = (BaseItem)EditorGUILayout.ObjectField("Root Item", rootMergeItem, typeof(BaseItem), false);

        EditorGUILayout.Space(10);

        // Liste alani (birden fazla item)
        EditorGUILayout.LabelField("Merge Item List", EditorStyles.boldLabel);
        for (int i = 0; i < mergeItemList.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            mergeItemList[i] = (BaseItem)EditorGUILayout.ObjectField($"Item {i + 1}", mergeItemList[i], typeof(BaseItem), false);

            // Liste elemanini silmek icin buton
            if (GUILayout.Button("Remove", GUILayout.Width(70)))
            {
                mergeItemList.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }

        // Listeye yeni item ekle butonu
        if (GUILayout.Button("Add New Item to List", GUILayout.Height(30)))
        {
            mergeItemList.Add(null);
        }

        EditorGUILayout.Space(20);

        // Yeni Item olusturma alani
        EditorGUILayout.LabelField("Create New Item", EditorStyles.boldLabel);
        newItemName = EditorGUILayout.TextField("Name", newItemName);
        newItemDescription = EditorGUILayout.TextArea(newItemDescription, GUILayout.Height(60));
        newIcon = (Sprite)EditorGUILayout.ObjectField("Icon", newIcon, typeof(Sprite), false);

        EditorGUILayout.Space(10);

        // Merge butonu
        if (GUILayout.Button("Merge Items", GUILayout.Height(40)))
        {
            MergeItems();
        }
    }

    /// <summary>
    /// Create Item Page icerisindeki icerikleri cizer.
    /// </summary>
    void DrawCreateItemPage()
    {
        // Yeni Item Olusturma
        EditorGUILayout.LabelField("New Item Properties", EditorStyles.boldLabel);
        newItemName = EditorGUILayout.TextField("Name", newItemName);
        newItemDescription = EditorGUILayout.TextArea(newItemDescription, GUILayout.Height(60));
        newIcon = (Sprite)EditorGUILayout.ObjectField("Icon", newIcon, typeof(Sprite), false);

        EditorGUILayout.Space(10);

        // Kayit yolu secimi
        EditorGUILayout.LabelField("Save Path for New Item", EditorStyles.boldLabel);
        savePath = EditorGUILayout.TextField("Path", savePath);

        EditorGUILayout.Space(20);

        if (GUILayout.Button("Create Item", GUILayout.Height(40)))
        {
            CreateNewItem();
        }
    }

    /// <summary>
    /// Yeni bir BaseItem olusturur ve diske kaydeder.
    /// </summary>
    void CreateNewItem()
    {
        // Yeni item olusturma islemi
        BaseItem newItem = CreateInstance<BaseItem>();
        newItem.itemName = newItemName;
        newItem.description = newItemDescription;
        newItem.icon = newIcon;

        string path = EditorUtility.SaveFilePanelInProject(
            "Save Item",
            newItemName + ".asset",
            "asset",
            "Select Save Location",
            savePath);

        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(newItem, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorGUIUtility.PingObject(newItem);
        }
    }

    /// <summary>
    /// Belirtilen itemleri birlestirir ve yeni bir BaseItem olarak kaydeder.
    /// </summary>
    void MergeItems()
    {
        // Merge islemi
        if (rootMergeItem == null)
        {
            Debug.LogWarning("Root Merge Item is not set.");
            return;
        }

        // Yeni olusturulan item'a ekleme
        BaseItem newItem = CreateInstance<BaseItem>();
        newItem.itemName = newItemName;
        newItem.description = newItemDescription;
        newItem.icon = newIcon;

        newItem.RootMergeItem = rootMergeItem; // Root item ekleniyor
        newItem.MergedItems.AddRange(mergeItemList); // Liste ekleniyor

        string path = EditorUtility.SaveFilePanelInProject(
            "Save Merged Item",
            newItemName + "_Merged.asset",
            "asset",
            "Select Save Location for Merged Item",
            savePath);

        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(newItem, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorGUIUtility.PingObject(newItem);
        }

        Debug.Log("Merge operation completed!");
    }

    /// <summary>
    /// Projede bulunan tum BaseItem turundeki ScriptableObject'leri listeler.
    /// </summary>
    void DrawCreatedItemsList()
    {
        EditorGUILayout.LabelField("Created Scriptable Objects", EditorStyles.boldLabel);

        // Proje genelinde Assets klasorunde BaseItem tipinde assetleri bulur.
        string[] guids = AssetDatabase.FindAssets("t:BaseItem", new string[] { "Assets" });
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            BaseItem item = AssetDatabase.LoadAssetAtPath<BaseItem>(assetPath);
            if (item != null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(item, typeof(BaseItem), false);
                if (GUILayout.Button("Select", GUILayout.Width(60)))
                {
                    EditorGUIUtility.PingObject(item);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
#endif
