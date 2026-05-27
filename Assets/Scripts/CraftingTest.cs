using UnityEngine;

public class CraftingTest : MonoBehaviour
{
    public ItemData woodItem;   // 원목 아이템 데이터 연결
    public ItemData stickItem; // 막대 아이템 데이터 연결
    public ItemData steelItem;  // 철 아이템 데이터 연결

    public Transform craftingGridTransform;
    private InventorySlot[] inputSlots;

    void Start()
    {
        inputSlots = craftingGridTransform.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        // [1번 키] 막대 조합 테스트 (원목 세로 2개 배치 - 일부러 오른쪽 아래 구석 5, 8번에 배치)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (woodItem == null) return;
            ClearAll();

            inputSlots[5].SetItem(woodItem);
            inputSlots[8].SetItem(woodItem);
            Debug.Log("5번, 8번 슬롯에 원목을 일자로 배치했습니다. 결과창을 확인하세요!");
        }

        // [2번 키] 철 곡괭이 조합 테스트 (철 3개 + 막대 2개 T자 배치 - 일부러 왼쪽에 치우치게 배치)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (steelItem == null || stickItem == null) return;
            ClearAll();

            // 왼쪽에 치우친 T자 모양
            inputSlots[0].SetItem(steelItem);
            inputSlots[1].SetItem(steelItem);
            inputSlots[2].SetItem(steelItem);
            inputSlots[4].SetItem(stickItem);
            inputSlots[7].SetItem(stickItem);
            Debug.Log("왼쪽 정렬 T자 모양으로 철 곡괭이 재료를 배치했습니다. 결과창을 확인하세요!");
        }

        // [3번 키] 조합대 전부 비우기
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ClearAll();
            Debug.Log("조합대를 전부 비웠습니다.");
        }
    }

    private void ClearAll()
    {
        for (int i = 0; i < 9; i++)
        {
            inputSlots[i].ClearSlot();
        }
    }
}
