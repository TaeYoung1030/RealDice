using UnityEngine;

public class FilmPickUp : MonoBehaviour, ActivityInterface
{
    //필름을 먹을때마다 이 스크립트가 작동하여 amount만큼 필름개수가 추가됨
    [SerializeField] int amount = 1;
    public KeyCode key => KeyCode.G;
    public string actionText => "필름 줍기";

    public void OnActivity()
    {
        PlayerInventory.Instance.AddFilm(amount);
        Destroy(gameObject);
    }

}
