using UnityEngine;

public enum ItemType
{
    Camera,
    Other
}


[CreateAssetMenu(fileName = "NewItem" , menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public GameObject item;
    public Sprite icon;
}
