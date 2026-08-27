using UnityEngine;

public class EquipCamera : MonoBehaviour
{
    [SerializeField] Transform camDirection;
    [SerializeField] GameObject messagePanel;
    [SerializeField] float display_Time = 1f;

    private bool isEquipped = false;

    private void Start()
    {
        if(messagePanel != null)
        {
            messagePanel.SetActive(false);
        }

        if(camDirection == null)
        {
            camDirection = Camera.main.transform;
        }
      
    }
    // Update is called once per frame
    void Update()
    {
        if(isEquipped && Input.GetMouseButtonDown(0))
        {
            //카메라를 들고 있을때만 가능하게끔 
            TakePhoto();
        }
    }

    void TakePhoto()
    {
        if(!PlayerInventory.Instance.TryUseFilm())
        {
            messagePanel.SetActive(true);

            CancelInvoke("HideMessage");
            Invoke("HideMessage",display_Time);
            return;
        }
        Debug.Log("카메라 클릭");
        //카메라 클릭했을 시 찰칵 소리 + 라이트 + 괴물한테 충격 가하기 
        //필름이 없을때 찍으려고 하면 몇 초간 알림 메시지(필름이 필요합니다!) 출력하기
        //카메라 먹었을때 필름 ui 출력하기 
        RaycastHit hit;
        if(Physics.Raycast(camDirection.position, camDirection.forward, out hit))
        {
            Debug.Log(hit.collider.gameObject.name + "촬영!");
            if(hit.collider.CompareTag("Monster"))
            {
                EnemyStateManager enemy = hit.collider.GetComponent<EnemyStateManager>();

                if(enemy != null)
                {
                    enemy.TakeFlash();
                }
            }
        }
    }

    public void SetEquipped(bool equipped)
    {
        isEquipped = equipped;
    }

    private void HideMessage()
    {
        messagePanel.SetActive(false);
    }
}
