using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea] public string description;
    public int attackPower;
    public int defensePower;
    public Sprite icon;
}
