using UnityEngine;

public class ItemPickUp : MonoBehaviour, ActivityInterface
{
    [Header("itemPickUp")]
    [SerializeField] ItemData myItem;
    public KeyCode key => KeyCode.G;
    public string actionText => "카메라 줍기";

    public void OnActivity()
    {
        bool isPickedUp = PlayerInventory.Instance.PickUpItem(myItem);

        if(isPickedUp)
        {
            //?? ????? ??? ?????? ??? ?? ??? ??
            if(myItem.itemType == ItemType.Camera)
                PlayerInventory.Instance.AcquireCamera();
            Destroy(gameObject);
        }
    }

   
}
