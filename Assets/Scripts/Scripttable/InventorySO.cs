using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Objects(InventoryItem")]

public class InventoryItemSO : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public float damage;
    public float attackSpeed;
    public float extraHealth;

}

public enum ItemType
{
    Weapon,
    Equipment,

}