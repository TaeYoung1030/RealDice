using UnityEngine;
using UnityEngine.AI;

public class AttackState : IEnemyState
{
    private NavMeshAgent agent;
    private float resetTimer = 0f;

    private bool hasCaughtPlayer;
    [SerializeField] float deathSceneTime = 7f;

    public void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("current state : getup");
        agent = enemy.GetComponent<NavMeshAgent>();

        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        enemy.GetComponent<Animator>().SetFloat("Walk", 0f);

        enemy.GetComponent<Animator>().SetTrigger("Attack");

        if(enemy.deathCamera != null)
        {
            enemy.deathCamera.SetActive(true);
        }

        hasCaughtPlayer = false;

    }

    public void ExitState(EnemyStateManager enemy)
    {
        if(agent != null)
        {
            agent.isStopped = false;
        }
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        resetTimer += Time.deltaTime;

        if(resetTimer >= deathSceneTime && !hasCaughtPlayer)
        {
            //enemy.ResetGame();
            //enemy.PlayerFail?.Invoke();
            hasCaughtPlayer=true;
            enemy.FailM();
        }
    }

   
}
