using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
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

    public int FilmCount {get; private set;}
    public bool HasCamera {get; private set;}

    public event Action<int> FilmChanged;
    public event Action<bool> CameraChanged;
    public event Action CameraAcquired;

    public event Action<int, ItemData> SlotUpdate;

    private void Awake()
    {
        Instance = this;
    }

    //필름을 먹을때마다 필름이 추가되는 함수, Invoke로 event 구독 발송
    public void AddFilm(int amount = 1)
    {
        FilmCount += amount;
        FilmChanged?.Invoke(FilmCount);
    }

    //카메라를 클릭시 필름 한 번 소모하는 함수 사용
    public bool TryUseFilm()
    {
        if(FilmCount <= 0) return false;

        FilmCount--;
        FilmChanged?.Invoke(FilmCount);
        return true;
    }
    //이 함수 기능 다시 확인해보기
    public void AcquireCamera()
    {
        if(HasCamera) return;

        HasCamera = true;
        CameraChanged?.Invoke(true);
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
            EquipCamera camera = instItem[slotIndex].GetComponent<EquipCamera>();
            camera.SetEquipped(false);
            currentIndex = -1;
            return;
        }
        if(currentIndex != -1 && instItem[currentIndex] != null)
        {
            instItem[currentIndex].SetActive(false);
            EquipCamera oldCamera = instItem[currentIndex].GetComponent<EquipCamera>();
            if (oldCamera != null) oldCamera.SetEquipped(false);
        }
        //처음 해당 칸이 비워져있을때
        if (instItem[slotIndex] == null)
        {
            instItem[slotIndex] = Instantiate(itemEquip.item, handPosition, false);
            instItem[slotIndex].transform.localPosition = Vector3.zero;
            instItem[slotIndex].transform.localRotation = Quaternion.identity;

            //instItem[slotIndex].transform.SetParent(handPosition);

        }
        else
        {
            instItem[slotIndex].SetActive(true);
        }
        EquipCamera newCamera = instItem[slotIndex].GetComponent<EquipCamera>();
        if (newCamera != null)
        {
            newCamera.SetEquipped(true);
        }
        currentIndex = slotIndex;
        Debug.Log($"{itemEquip.itemName} 장착완료");
    }

    public void ResetMissionInventory()
    {
        FilmCount = 0;
        FilmChanged?.Invoke(FilmCount);

        HasCamera = false;
        CameraChanged?.Invoke(false);

        for(int i=0; i<slots.Length; i++)
        {
            slots[i] = null;

            SlotUpdate?.Invoke(i, null);

            if (instItem[i] != null)
            {
                Destroy(instItem[i]);
            }

            instItem[i] = null;
        }

        currentIndex = -1;
    }
}
