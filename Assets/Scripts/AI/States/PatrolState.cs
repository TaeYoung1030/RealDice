using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IEnemyState
{
    private NavMeshAgent agent;
    private Animator animator;

    private float waitTimer;
    
    [SerializeField] float patrolWaitTime = 2f;
    public void EnterState(EnemyStateManager enemy)
    {
        animator = enemy.GetComponent<Animator>();
        agent = enemy.GetComponentInParent<NavMeshAgent>();

        animator.SetFloat("Walk" , 3f);
        MoveToRandomPos(enemy);
    }


    public void ExitState(EnemyStateManager enemy)
    {
        
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        if(CanSeePlayer(enemy))
        {
            enemy.TransitionToState(new ChaseState());
            return;
        }

        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetFloat("Walk", 0f);
            waitTimer += Time.deltaTime;

            //탐색 시간이 지나면 다시 그 쪽으로 움직이기 
            if(waitTimer >=  patrolWaitTime)
            {
                //멈출때 애니메이션도 같이 멈출 수 있는 기능 추가
                MoveToRandomPos(enemy);
                waitTimer = 0;

                animator.SetFloat("Walk", 3f);
            }
        }
    }

    private void MoveToRandomPos(EnemyStateManager enemy)
    {
        Vector2 randomCircle = Random.insideUnitCircle * 25f;
        Vector3 randomDirection = new Vector3(randomCircle.x, 0f, randomCircle.y);

        randomDirection += enemy.transform.position;

        NavMeshHit hit;

        if(NavMesh.SamplePosition(randomDirection, out hit , 10f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private bool CanSeePlayer(EnemyStateManager enemy)
    {
        //사이 거리 체크
        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if(distance > enemy.chaseRange)
        {
            return false;
        }

        //몬스터에서 플레이어를 향하는 방향 벡터
        Vector3 dirToPlayer = (enemy.player.position - enemy.transform.position).normalized;

        //시야각
        float angle = Vector3.Angle(enemy.transform.forward, dirToPlayer);

        if(angle > enemy.viewAngle * 0.5f)
        {
            return false;
        }

        //장애물 체크 -> 눈 높이에서 레이저를 쏘도록 높이를 더해줌
        Vector3 eyePosition = enemy.transform.position + Vector3.up * 1.5f;
        Vector3 playerCenter = enemy.player.position + Vector3.up * 1.5f;
        Vector3 rayDir = (playerCenter - eyePosition).normalized;

        RaycastHit hit;

        if(Physics.Raycast(eyePosition, rayDir, out hit, enemy.chaseRange))
        {
            if(hit.collider.CompareTag("Player"))
            {
                Debug.Log("플레이어 발견");
                return true;
            }
        }

        return false;
    }
}
