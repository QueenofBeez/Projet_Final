using UnityEngine;
using System.Text;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable Item")]

public class ConsumableItem : InventoryItem
{
    [Header("Consumable Data")]
    [SerializeField] private string useText = "Does something, maybe?";
    public override string GetInfoDisplayText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(Name).AppendLine();
        builder.Append("<color=green>Use:").Append(useText).Append("</color>").AppendLine();
        builder.Append("Max Stack: ").Append(MaxStack).AppendLine();

        return builder.ToString();
    }

}
