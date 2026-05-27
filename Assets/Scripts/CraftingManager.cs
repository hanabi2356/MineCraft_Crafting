using UnityEngine;
using System.Collections.Generic;

public class CraftingManager : MonoBehaviour
{
    private static CraftingManager _instance;
    public static CraftingManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    [field: SerializeField] public List<CraftingRecipe> recipes { get; private set; }

    [SerializeField] private Transform craftingGridTransform;
    [SerializeField] private InventorySlot outputSlot;

    private InventorySlot[] craftingInputSlot = new InventorySlot[9];


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        InventorySlot[] slots = craftingGridTransform.GetComponentsInChildren<InventorySlot>();
        for(int i=0; i<9; i++)
        {
            craftingInputSlot[i]=slots[i];
            craftingInputSlot[i].slotType = SlotType.CraftingInput;
        }
        outputSlot.slotType = SlotType.CraftingOutput;
        UpdateCraftingOutput();
    }
    private void UpdateCraftingOutput()
    {
        CraftingRecipe matchedRecipe = FindMatchingRecipe();

        if (matchedRecipe != null)
        {
            outputSlot.SetItem(matchedRecipe.GetResultItem);
        }
        else
        {
            outputSlot.ClearSlot();
        }
    }
    private CraftingRecipe FindMatchingRecipe()
    {
        // 1. 현재 조합대의 알맹이 행렬 추출
        ItemData[,] inputMatrix = Get2DMatrix(craftingInputSlot);
        ItemData[,] trimmedInput = TrimMatrix(inputMatrix);

        if (trimmedInput == null) return null; // 조합대가 비어있으면 패스

        // 2. 레시피 데이터베이스를 순회하며 알맹이 비교
        foreach (var recipe in recipes)
        {
            ItemData[,] recipeMatrix = Get2DMatrixFromRecipe(recipe.GetRecipe);
            ItemData[,] trimmedRecipe = TrimMatrix(recipeMatrix);

            if (CompareMatrices(trimmedInput, trimmedRecipe))
            {
                return recipe; // 모양이 일치하면 해당 조합법 반환
            }
        }
        return null;
    }
    private ItemData[,] Get2DMatrix(InventorySlot[] slots)
    {
        ItemData[,] matrix = new ItemData[3, 3];
        for (int i = 0; i < 9; i++)
        {
            matrix[i / 3, i % 3] = slots[i].GetItem;
        }
        return matrix;
    }

    private ItemData[,] Get2DMatrixFromRecipe(ItemData[] ingredients)
    {
        ItemData[,] matrix = new ItemData[3, 3];
        for (int i = 0; i < 9; i++)
        {
            matrix[i / 3, i % 3] = ingredients[i];
        }
        return matrix;
    }

    private ItemData[,] TrimMatrix(ItemData[,] matrix)
    {
        int minX = 3, maxX = -1, minY = 3, maxY = -1;
        bool hasItem = false;

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (matrix[y, x] != null)
                {
                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                    hasItem = true;
                }
            }
        }

        if (!hasItem) return null;

        int width = maxX - minX + 1;
        int height = maxY - minY + 1;
        ItemData[,] trimmed = new ItemData[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                trimmed[y, x] = matrix[minY + y, minX + x];
            }
        }

        return trimmed;
    }

    private bool CompareMatrices(ItemData[,] a, ItemData[,] b)
    {
        if (a == null || b == null) return false;
        if (a.GetLength(0) != b.GetLength(0) || a.GetLength(1) != b.GetLength(1)) return false;

        for (int y = 0; y < a.GetLength(0); y++)
        {
            for (int x = 0; x < a.GetLength(1); x++)
            {
                if (a[y, x] != b[y, x]) return false;
            }
        }
        return true;
    }

    
    public void OnTakeResultItem()
    {
        for (int i = 0; i < 9; i++)
        {
            craftingInputSlot[i].ClearSlot();
        }
        UpdateCraftingOutput();
    }
    void Update()
    {
        
    }
}
