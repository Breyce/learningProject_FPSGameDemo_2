using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.LastKnowPos);
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            stateMechine.ChangeState(new AttackState());
        }

        if (enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance)
        {
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;
            //Debug.Log("here " + searchTimer + "   " + enemy.Agent.stoppingDistance);
            if (searchTimer > 4)
            {
                stateMechine.ChangeState(new PatrolState());
            }

            if (moveTimer > Random.Range(3, 5))
            {
                //Random.insideUnitSphere: 
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 10));
                moveTimer = 0;
            }
        }
    }
}
