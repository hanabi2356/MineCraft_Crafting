using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum SlotType
{
    Inventory,
    CraftingInput,
    CraftingOutput
}
public class InventorySlot : MonoBehaviour
{
    public SlotType slotType;
    public Image iconImage;

    private ItemData currentItem;

    public ItemData GetItem => currentItem;

    public void SetItem(ItemData newItem)
    {
        currentItem = newItem;
        if(currentItem != null)
        {
            iconImage.sprite = currentItem.GetIcon;
            iconImage.enabled = true;
        }
        else
        {
            ClearSlot();
        }
    }
    public void ClearSlot()
    {
        currentItem = null;
        if (iconImage != null)
        {
            iconImage.sprite = null;
            iconImage.enabled = false;
        }
    }
}
