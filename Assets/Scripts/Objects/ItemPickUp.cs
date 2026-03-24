using UnityEngine;

public class ItemPickUp : MonoBehaviour, ActivityInterface
{
    [Header("¾ÆÀÌÅÛ Á¤º¸(scriptObejct")]
    [SerializeField] ItemData myItem;
    public KeyCode key => KeyCode.G;

    public void OnActivity()
    {
        bool isPickedUp = PlayerInventory.Instance.PickUpItem(myItem);

        if(isPickedUp)
        {
            Debug.Log($"{myItem.itemName} È¹µæ ¿Ï·á!");
            Destroy(gameObject);
        }
    }

   
}
