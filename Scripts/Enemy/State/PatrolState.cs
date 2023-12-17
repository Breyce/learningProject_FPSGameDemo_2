using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    // track waypoint we are currently tracking
    public int waypointIndex;
    public float waitTimer;
    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        PatrolCycle();
        if (enemy.CanSeePlayer())
        {
            stateMechine.ChangeState(new AttackState());
        }
    }

    public void PatrolCycle()
    {
        //implement patrol system
        if(enemy.Agent.remainingDistance < .2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 2) 
            {             
                if(waypointIndex < enemy.path.waypoints.Count - 1)
                {
                    waypointIndex++;
                }
                else
                {
                    waypointIndex = 0;
                }
                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                waitTimer = 0;
            }
        }
    }
}
