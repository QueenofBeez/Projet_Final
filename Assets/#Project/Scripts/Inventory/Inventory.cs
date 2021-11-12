using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Inventory ", menuName = "Items/Inventory")]

public class Inventory : ScriptableObject
{
    [SerializeField] private VoidEvent onInventoryItemsUpdated = null;
    [SerializeField] private ItemSlot testItemSlot = new ItemSlot();

    public ItemContainer itemContainer { get; } = new ItemContainer(9);

    public void OnEnable() => itemContainer.OnItemsUpdated += onInventoryItemsUpdated.Raise;

    public void OnDisable() => itemContainer.OnItemsUpdated -= onInventoryItemsUpdated.Raise;

    [ContextMenu("Test Add")]
    public void TestAdd()
    {
        itemContainer.AddItem(testItemSlot);
    }
}

