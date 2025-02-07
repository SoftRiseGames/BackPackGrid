#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections.Generic;

public class ItemCreatorWindow : EditorWindow
{
    private List<Item> items = new List<Item>();
    private ReorderableList itemsList;
    private string newItemName = "New Item";
    private string newItemDescription = "";
    private int attackPower;
    private int defensePower;
    private Sprite newIcon;

    [MenuItem("Item Creator/Create Item")]
    public static void ShowWindow()
    {
        GetWindow<ItemCreatorWindow>("Advanced Item Creator");
    }

    void OnEnable()
    {
        // ReorderableList initialization with null support
        itemsList = new ReorderableList(items, typeof(Item), true, true, true, true)
        {
            drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Drag & Drop Items"),
            drawElementCallback = DrawListElement,
            onAddCallback = OnAddListElement,
            elementHeight = EditorGUIUtility.singleLineHeight + 4
        };
    }

    void DrawListElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        // Null-safe element drawing
        items[index] = (Item)EditorGUI.ObjectField(
            new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
            items[index],
            typeof(Item),
            false
        );
    }

    void OnAddListElement(ReorderableList list)
    {
        // Add null instead of new Item
        items.Add(null);
    }
    void OnGUI()
    {
        // Input Items List
        EditorGUILayout.Space(10);
        itemsList.DoLayoutList();
        EditorGUILayout.Space(20);

        // New Item Properties
        EditorGUILayout.LabelField("New Item Properties", EditorStyles.boldLabel);
        newItemName = EditorGUILayout.TextField("Name", newItemName);
        newItemDescription = EditorGUILayout.TextArea(newItemDescription, GUILayout.Height(60));
        attackPower = EditorGUILayout.IntField("Attack Power", attackPower);
        defensePower = EditorGUILayout.IntField("Defense Power", defensePower);
        newIcon = (Sprite)EditorGUILayout.ObjectField("Icon", newIcon, typeof(Sprite), false);
        EditorGUILayout.Space(20);

        // Create Button
        if (GUILayout.Button("Create Combined Item", GUILayout.Height(40)))
        {
            CreateNewItem();
        }
    }

    void CreateNewItem()
    {
        Item newItem = CreateInstance<Item>();
        newItem.itemName = newItemName;
        newItem.description = newItemDescription;
        newItem.attackPower = attackPower;
        newItem.defensePower = defensePower;
        newItem.icon = newIcon;

        // Listede seçili item'ların özelliklerini combine etme örneği
        foreach (var item in items)
        {
            if (item != null)
            {
                newItem.attackPower += item.attackPower;
                newItem.defensePower += item.defensePower;
            }
        }

        string path = EditorUtility.SaveFilePanelInProject(
            "Save Combined Item",
            newItemName + ".asset",
            "asset",
            "Select Save Location");

        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(newItem, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorGUIUtility.PingObject(newItem);
        }
    }
}
#endif