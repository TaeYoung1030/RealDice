using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    public event Action MonsterDie;
    public event Action PlayerFail;

    [SerializeField] GameObject monster;
    [Range(0, 360)]
    [SerializeField] public float viewAngle = 120f;

    [Header("Range")]
    [SerializeField] public float attackRange = 2f;
    [SerializeField] public float chaseRange = 10f;

    [Header("speed")]
    [SerializeField] public float walkSpeed = 3f;
    [SerializeField] public float runSpeed = 6f;
    [Header("Deathcamera")]
    [SerializeField] public GameObject deathCamera;

    [Header("Position")]
    [HideInInspector] Transform monsterStartPos;
    [HideInInspector] Transform playerStartPos;

    [Header("FlashHP")]
    [SerializeField] int maxFlashHit = 3;
    private int currentFlashHit;

    [HideInInspector]
    public Transform player;

    public IEnemyState currentState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //추후 삭제될 부분1
        monsterStartPos = GameObject.Find("MonsterSpawnPosition").transform;
        playerStartPos = GameObject.Find("PlayerSpawnPosition").transform;

        currentFlashHit = maxFlashHit;

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


    public void Die()
    {
        MonsterDie?.Invoke();
        Destroy(gameObject, 3.9f);
        
    }

    public void FailM()
    {
        PlayerFail?.Invoke();
        //ResetGame();
    }

    public void TakeFlash(int damage = 1)
    {
        // if(currentState is DeathState || currentState is ShockState) return;
        //추후 매개변수로 몇 번째 필름을 사용한 촬영인지 연결 -> 일정 횟수 촬영성공시 몬스터 죽음

        currentFlashHit -= damage;
        Debug.Log($"찰칵! 몬스터 체력: {currentFlashHit} / {maxFlashHit}");
        if(currentFlashHit <= 0)
        {
            TransitionToState(new DeathState());
        }
        else
        {
            TransitionToState(new ShockState()); 
        }
    }

    //추후 삭제될 부분2
    public void ResetGame()
    {
        deathCamera.SetActive(false);

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if(agent != null)
        {
            agent.Warp(monsterStartPos.position);
        }
        else
        {
            transform.position = monsterStartPos.position;
        }
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false; 
            player.position = playerStartPos.position; 
            cc.enabled = true;  
        }
        else
        {
            player.position = playerStartPos.position;
        }

        TransitionToState(new GetUpState());
    }

}
