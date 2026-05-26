using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Inventory/CraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField]private ItemData[] recipe = new ItemData[9];

    [SerializeField]private ItemData resultItemData;
    [SerializeField]private int resultItemCount;

    public ItemData[] GetRecipe => recipe;
    public ItemData GetResultItem => resultItemData;

    public int ResultItemCount => resultItemCount;

    
}
