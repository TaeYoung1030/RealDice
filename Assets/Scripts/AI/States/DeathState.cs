using UnityEngine;
using UnityEngine.AI;

public class DeathState : IEnemyState
{
    public void EnterState(EnemyStateManager enemy)
    {
        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

        if(agent != null )
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        Collider coll = enemy.GetComponent<Collider>();
        if( coll != null )  coll.enabled = false;

        enemy.GetComponent<Animator>().SetTrigger("Death");
        enemy.Die();

    }

    public void ExitState(EnemyStateManager enemy)
    {
        
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        
    }
}
