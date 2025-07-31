using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

public class IInventoryObjectController : OdinMenuEditorWindow
{
    [MenuItem("Inventory Object Item Controller/Inventory Object Item Controller")]
    private static void OpenWindow()
    {
        GetWindow<IInventoryObjectController>("Inventory Object Controller").Show();
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        tree.Add("Inventory Scanner", new InventoryScanner());
        return tree;
    }

    public class InventoryScanner
    {
        [Title("Manual Template Reference")]
        [ShowInInspector,SerializeReference]
        private IInventoryObject inventoryTemplate;

        [Title("Discovered Inventory Objects")]
        [ReadOnly, ShowInInspector]
        private List<IInventoryObject> foundInventoryObjects = new();

        [Button("Find Inventory Objects")]
        private void FindAllInventoryObjects()
        {
            foundInventoryObjects.Clear();

            // Sahnedeki tüm MonoBehaviour bileþenlerini bul
            string[] guids = AssetDatabase.FindAssets("t:IInventoryObject", new string[] { "Assets" });
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);

                if (obj is IInventoryObject item)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.ObjectField(obj, typeof(UnityEngine.Object), false);
                    if (GUILayout.Button("Select", GUILayout.Width(60)))
                    {
                        EditorGUIUtility.PingObject(obj);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }


            Debug.Log($"Found {foundInventoryObjects.Count} valid IInventoryObject components.");
        }
    }
}
