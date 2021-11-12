using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
{
    [SerializeField] protected Image itemIconImage = null;

    public int SlotIndex { get; private set; }

    public abstract HotbarItem SlotItem { get; set; }

    private void OnEnable() => UpdateSlotUI(); // everytime we open the inventory

    protected virtual void Start() // when we first open the inventory
    {
        SlotIndex = transform.GetSiblingIndex();
        UpdateSlotUI();
    }

    public abstract void OnDrop(PointerEventData eventData);

    public abstract void UpdateSlotUI();

    protected virtual void EnableSlotUI(bool enable) => itemIconImage.enabled = enable; // no matter the slot, the item icon will be either enabled or disabled

}
