using UnityEditor.AdaptivePerformance.Editor;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [Header("장착 설정")]
    [SerializeField] Transform handPosition;
    private GameObject currentEquipObeject;

    [Header("인벤토리 데이터")]
    [SerializeField] ItemData[] slots = new ItemData[2];

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
                return true;
            }

        }

        Debug.Log("인벤토리가 꽉 찼습니다");
        return false;
    }

    public void equipItem(int slotIndex)
    {
        ItemData itemEquip = slots[slotIndex];
        if(itemEquip == null)
        {
            Debug.Log($"{slotIndex + 1}번 칸이 비어있습니다");
            return;
        }

        if(currentEquipObeject != null)
        {
            Destroy(currentEquipObeject);
        }

        currentEquipObeject = Instantiate(itemEquip.item, handPosition.position, handPosition.rotation);

        currentEquipObeject.transform.SetParent(handPosition);

        Debug.Log($"{itemEquip.itemName} 장착완료");
    }
}
