using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        if(enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            if(shotTimer > enemy.fireRate)
            {
                Shoot();
            }

            if(moveTimer > Random.Range(3, 7))
            {
                //Random.insideUnitSphere: 
                enemy.Agent.SetDestination(enemy.transform.position+(Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        }
        else // lost sight of player
        {
            losePlayerTimer += Time.deltaTime;
            if(losePlayerTimer > Random.Range(3, 7))
            {
                //change to the search state
                stateMechine.ChangeState(new SearchState());
            }
        }

        enemy.LastKnowPos = enemy.Player.transform.position;
    }

    public void Shoot()
    {
        //store a reference to the gun barrel
        Transform gunBarrel = enemy.gunBarrel;

        //Instantiate a new bullet
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunBarrel.position, enemy.transform.rotation);

        //calculate the direction to the player
        Vector3 shootDirection = (enemy.Player.transform.position - gunBarrel.transform.position).normalized;

        //add force to the RB
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3f,3f),Vector3.up) * shootDirection * 40;
        Debug.Log("Shoot...");
        shotTimer = 0;
    }
}
