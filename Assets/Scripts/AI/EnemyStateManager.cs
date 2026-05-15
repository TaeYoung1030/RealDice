using System;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public event Action MonsterDie;
    [SerializeField] GameObject monster;
    [Header("시야 설정")]
    [SerializeField] public float chaseRange = 10f;
    [Range(0, 360)]
    [SerializeField] public float viewAngle = 120f;

    [Header("이동 스탯")]
    [SerializeField] public float walkSpeed = 3f;
    [SerializeField] public float runSpeed = 6f;

    [HideInInspector]
    public Transform player;

    public IEnemyState currentState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        TransitionToState(new GetUpState());
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.UpdateState(this);
    }

    public void TransitionToState(IEnemyState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);

    }

    //die메소드 추후 사용하는 메소드 제작

    private void Die()
    {
        //몬스터가 죽었을때 해당 이벤트를 구독한 모든 곳에 전달
        MonsterDie?.Invoke();
        Destroy(gameObject, 2f);
        
    }

}
