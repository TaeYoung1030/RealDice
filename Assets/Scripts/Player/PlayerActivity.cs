using UnityEngine;

public class PlayerActivity : MonoBehaviour
{
    [SerializeField] Transform camTrans;
    [SerializeField] GameObject dice;

    // Canvas에 붙인 InteractionPromptUI를 Inspector에서 연결
    [SerializeField] PromptUI interactionPromptUI;

    private Outline currentOutline;

    private void Update()
    {
        CheckInteractionTarget();

        // 인벤토리 단축키는 오브젝트를 바라보지 않아도 동작해야 함
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerInventory.Instance.equipItem(0);
        }
    }

    private void CheckInteractionTarget()
    {
        // 카메라 중앙에서 한 번만 Raycast
        if (Physics.Raycast(camTrans.position, camTrans.forward, out RaycastHit hit))
        {
            // 바라보는 오브젝트 또는 부모에서 Outline 찾기
            Outline outline = hit.collider.GetComponentInParent<Outline>();

            // 바라보는 오브젝트 또는 부모에서 상호작용 스크립트 찾기
            ActivityInterface activity = hit.collider.GetComponentInParent<ActivityInterface>();

            // Outline은 있어도 상호작용은 없을 수 있음
            UpdateOutline(outline);

            // 상호작용 가능한 오브젝트라면
            if (activity != null)
            {
                // [G] 카메라 줍기 같은 UI 표시
                interactionPromptUI.Show(activity.key, activity.actionText);

                // 해당 오브젝트에 설정된 키를 눌렀을 때 실행
                if (Input.GetKeyDown(activity.key))
                {
                    activity.OnActivity();
                }
            }
            else
            {
                // Outline만 있고 상호작용이 없는 오브젝트면 안내 UI는 숨김
                interactionPromptUI.Hide();
            }
        }
        else
        {
            // 아무것도 바라보지 않을 때
            ClearOutline();
            interactionPromptUI.Hide();
        }
    }

    // 기존 Outline() 내부에서 “Outline 교체” 역할만 따로 뺀 함수
    private void UpdateOutline(Outline newOutline)
    {
        // 이미 같은 오브젝트를 보고 있으면 다시 끄고 켤 필요 없음
        if (currentOutline == newOutline)
        {
            return;
        }

        // 이전에 보던 오브젝트 Outline 끄기
        if (currentOutline != null)
        {
            currentOutline.enabled = false;
        }

        // 새로 바라보는 오브젝트 저장
        currentOutline = newOutline;

        // 새 Outline이 있다면 켜기
        if (currentOutline != null)
        {
            currentOutline.enabled = true;
        }
    }

    // 시선을 돌렸을 때 현재 Outline 정리
    private void ClearOutline()
    {
        if (currentOutline != null)
        {
            currentOutline.enabled = false;
            currentOutline = null;
        }
    }
}