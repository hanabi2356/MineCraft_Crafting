using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField]private string itemName;
    [SerializeField]private Sprite icon;

    public string GetItemName => itemName;
    public Sprite GetIcon => icon;
    
}
