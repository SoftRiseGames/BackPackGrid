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

    private int selectedTab = 0; // Secili sekme indeksi

    // Sidebar icin scroll view pozisyonu
    private Vector2 sidebarScrollPos;

    /// <summary>
    /// Advanced Item Creator penceresini acar.
    /// </summary>
    [MenuItem("Item Creator/Advanced Item Creator")]
    public static void ShowWindow()
    {
        GetWindow<ItemCreatorWindow>("Advanced Item Creator");
    }

    /// <summary>
    /// Pencerenin ana cizim fonksiyonudur. Sol ve sag alanlar cizilir.
    /// </summary>
    void OnGUI()
    {
        // Ana layout: sol kisimda sayfa icerigi, sag kisimda sidebar (olusturulan asset'ler)
        EditorGUILayout.BeginHorizontal();
        {
            // SOL KISIM: Sayfa icerigi
            EditorGUILayout.BeginVertical();
            {
                // Ustte toolbar (sekme secimi)
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

                EditorGUILayout.Space(10);

                // Secili sekmeye gore icerik cizimi
                if (selectedTab == 0)
                {
                    DrawMainPage();
                }
                else if (selectedTab == 1)
                {
                    DrawCreateItemPage();
                }
            }
            EditorGUILayout.EndVertical();

            // SAG KISIM: Sidebar - Olusturulan ScriptableObject'lerin listelendigii alan
            // Sidebar genisligini 300px sabitliyoruz.
            EditorGUILayout.BeginVertical(GUILayout.Width(300));
            {
                EditorGUILayout.LabelField("Created Scriptable Objects", EditorStyles.boldLabel);
                // Pencere yuksekliginden 50 piksel cikarilarak sidebar yuksekligi hesaplanir.
                float sidebarHeight = position.height - 50;
                // ScrollView; yatay scrollbar olmasin diye alwaysShowHorizontal false, dikey her zaman gosterilsin.
                sidebarScrollPos = EditorGUILayout.BeginScrollView(
                    sidebarScrollPos,
                    /*alwaysShowHorizontal:*/ false,
                    /*alwaysShowVertical:*/ true,
                    GUILayout.Width(300),
                    GUILayout.Height(sidebarHeight)
                );
                {
                    EditorGUILayout.BeginVertical();
                    {
                        DrawCreatedItemsListContent();
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// Main Page icerisindeki icerikleri cizer.
    /// </summary>
    void DrawMainPage()
    {
        EditorGUILayout.LabelField("Item Management", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        // Root Merge Item alani
        EditorGUILayout.LabelField("Root Merge Item", EditorStyles.boldLabel);
        rootMergeItem = (BaseItem)EditorGUILayout.ObjectField("Root Item", rootMergeItem, typeof(BaseItem), false);
        EditorGUILayout.Space(10);

        // Merge Item List alani
        EditorGUILayout.LabelField("Merge Item List", EditorStyles.boldLabel);
        for (int i = 0; i < mergeItemList.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            mergeItemList[i] = (BaseItem)EditorGUILayout.ObjectField($"Item {i + 1}", mergeItemList[i], typeof(BaseItem), false);
            if (GUILayout.Button("Remove", GUILayout.Width(70)))
            {
                mergeItemList.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }
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
        EditorGUILayout.LabelField("New Item Properties", EditorStyles.boldLabel);
        newItemName = EditorGUILayout.TextField("Name", newItemName);
        newItemDescription = EditorGUILayout.TextArea(newItemDescription, GUILayout.Height(60));
        newIcon = (Sprite)EditorGUILayout.ObjectField("Icon", newIcon, typeof(Sprite), false);

        EditorGUILayout.Space(10);

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
    /// Belirtilen item'leri birlestirir ve yeni bir BaseItem olarak kaydeder.
    /// </summary>
    void MergeItems()
    {
        if (rootMergeItem == null)
        {
            Debug.LogWarning("Root Merge Item is not set.");
            return;
        }

        BaseItem newItem = CreateInstance<BaseItem>();
        newItem.itemName = newItemName;
        newItem.description = newItemDescription;
        newItem.icon = newIcon;
        newItem.RootMergeItem = rootMergeItem;
        newItem.MergedItems.AddRange(mergeItemList);

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
    /// Sidebar icerisinde goruntulenecek olan, Assets icindeki tum BaseItem assetlerini cizer.
    /// </summary>
    void DrawCreatedItemsListContent()
    {
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
