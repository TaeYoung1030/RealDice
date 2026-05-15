using System;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [Header("장착 설정")]
    [SerializeField] Transform handPosition;

    [Header("인벤토리 데이터")]
    [SerializeField] ItemData[] slots = new ItemData[2];

    private GameObject[] instItem = new GameObject[2];
    private int currentIndex = -1;

    public event Action<int, ItemData> SlotUpdate;

    private void Awake()
    {
        Instance = this;
    }
   
    public bool PickUpItem(ItemData item)
    {
        for(int i=0; i<slots.Length; i++)
        {
            if(slots[i] == null)
            {
                slots[i] = item;
                //업데이트 됐으니 방송 송출
                SlotUpdate?.Invoke(i, item);
                return true;
            }

        }

        Debug.Log("인벤토리가 꽉 찼습니다");
        return false;
    }

    //키보드 키 몇 번에 저장하는지 설정
    public void equipItem(int slotIndex)
    {
        ItemData itemEquip = slots[slotIndex];
        if(itemEquip == null)
        {
            Debug.Log($"{slotIndex + 1}번 칸이 비어있습니다");
            return;
        }
        //다시 누르면 사라지게(해당 오브젝트가)
        if(currentIndex == slotIndex)
        {
            instItem[slotIndex].SetActive(false);
            currentIndex = -1;
            return;
        }
        if(currentIndex != -1 && instItem[currentIndex] != null)
        {
            instItem[currentIndex].SetActive(false);
        }
        //처음 해당 칸이 비워져있을때
        if (instItem[slotIndex] == null)
        {
            instItem[slotIndex] = Instantiate(itemEquip.item, handPosition.position, handPosition.rotation);

            instItem[slotIndex].transform.SetParent(handPosition);

        }
        else
        {
            instItem[slotIndex].SetActive(true);
        }
        currentIndex = slotIndex;
        Debug.Log($"{itemEquip.itemName} 장착완료");
    }
}
