using UnityEngine;

public class PlayerActivity : MonoBehaviour
{
    [SerializeField] Transform camTrans;
    [SerializeField] GameObject dice;

    Outline currentOutline;
    void Update()
    {
        Outline();

        Activity();
    }

    void Activity()
    {
        RaycastHit hit;

        if(Physics.Raycast(camTrans.position,camTrans.forward, out hit))
        {
            //raycast를 쏴서 인터페이스 기능을 가진 오브젝트가 맞았는지 확인 후 그 기능을 실행 - >인터페이스라 알아서 나눠서 동작 실행
            ActivityInterface ac = hit.collider.GetComponentInParent<ActivityInterface>();
 
            //인터페이스 해당 기능 사용
            if(ac != null)
            {
                if(Input.GetKeyDown(ac.key))
                {
                    ac.OnActivity();

                }
            }          
            
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlayerInventory.Instance.equipItem(0);
            }

        }
    
    }

    //outline 생기게 하는 메소드
    void Outline()
    {
        RaycastHit hit;

        if(Physics.Raycast(camTrans.position, camTrans.forward, out hit))
        {
            //outline 찾기
            Outline outline = hit.collider.GetComponentInParent<Outline>();
            //새로운 outline을 발견
            if(outline != null)
            {
                //전에 보고있던 물체랑 다른 물체였다면
                if(currentOutline != outline)
                {
                    //그 전에 보고 있던게 있었으면 그거 끄기
                    if(currentOutline != null) currentOutline.enabled = false;

                    currentOutline = outline;
                    currentOutline.enabled = true;
                }
                
            }
            else
            {
                ClearOutline();
            }
        }
        else
        {
            ClearOutline();
        }
    }
    
    //안 볼때 outline 정리하기
    void ClearOutline()
    {
        if(currentOutline != null)
        {
            currentOutline.enabled = false;
            currentOutline = null;
        }
    }
}
