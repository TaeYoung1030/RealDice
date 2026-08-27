using System.Collections;
using UnityEngine;

public class RollDice : MonoBehaviour,ActivityInterface
{
    [Header("설정 값")]
    [SerializeField] float throwPower = 3.5f;
    [SerializeField] float spinPower = 10f;

    [SerializeField] AvatarMovement am;

    [SerializeField] Transform[] diceSurface;

    Rigidbody rb;
    Vector3 currentPosition;
    bool isRolling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentPosition = transform.position;

        rb.isKinematic = true;

    }
    public KeyCode key => KeyCode.Mouse0;
    public string actionText => "주사위 굴리기";
    public void OnActivity()
    {

        if (!GameManager.instance.CanActivity()) return;
        GameManager.instance.SetState(GameState.Rolling);
        Roll();
    }

    public void Roll()
    {
        //주사위가 돌아가는 동작 구현
        
        rb.isKinematic = false;

        transform.position = currentPosition;

        Vector3 randomDir = Vector3.up + Random.insideUnitSphere * 0.5f;

        rb.AddForce(randomDir*throwPower, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * spinPower, ForceMode.Impulse);

        StartCoroutine(CheckResultRoutine());
    }

    IEnumerator CheckResultRoutine()
    {
        yield return new WaitForSeconds(1.0f);

        while (rb.linearVelocity.sqrMagnitude > 0.05f || rb.angularVelocity.sqrMagnitude > 0.05f)
        {
            yield return null; // 다음 프레임까지 대기
        }
        //종료되고 주사위 숫자 표현하기 
        Debug.Log("나온 숫자는 : " + GetResultNumber());

        GameManager.instance.SetState(GameState.Moving);

        //몇 초후에 주사위가 롤백되는지
        Invoke("ResetPosition", 3f);

        //말 이동하는거 구현
        am.MoveAvatar(GetResultNumber());
        

    }

    void SimulateArrive()
    {
        //State를 mission으로 바꿈
        GameManager.instance.OnArriveTile();
    }

    private void ResetPosition()
    {
        rb.isKinematic = true;
        transform.position = currentPosition;
        transform.rotation = Quaternion.identity;
    }

    //높이를 측정해서 현재 주사위의 숫자를 측정하기
    public int GetResultNumber()
    {
        int result = 0;
        float maxY = -999f;

        foreach(Transform t in diceSurface)
        {
            if(t.position.y > maxY)
            {
                maxY = t.position.y;
                result = int.Parse(t.name.Replace("Position", ""));
            }
        }

        return result;
    }
}
