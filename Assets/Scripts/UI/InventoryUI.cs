using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("UI슬롯 이미지")]
    [SerializeField] Image[] slotIcons = new Image[2];
    [Header("슬롯 배경 이미지")]
    [SerializeField] Image[] slotBgIcons = new Image[2];

    private void Start()
    {
        //구독 신청
        PlayerInventory.Instance.SlotUpdate += UpdateSlotUI;

        for(int i=0; i<slotIcons.Length; i++)
        {
            UpdateSlotUI(i, null);
        }
    }

    //방송 켜지면 자동 실행
    private void UpdateSlotUI(int slotIndex, ItemData newData)
    {
        if(newData != null)
        {
            slotIcons[slotIndex].sprite = newData.icon;
            slotIcons[slotIndex].gameObject.SetActive(true);
            slotBgIcons[slotIndex].gameObject.SetActive(true);
        }
        else
        {
            slotIcons[slotIndex].sprite = null;
            slotIcons[slotIndex].gameObject.SetActive(false);
            slotBgIcons[slotIndex].gameObject.SetActive (false);
        }
    }
}
