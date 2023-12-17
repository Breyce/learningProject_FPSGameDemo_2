using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMechine : MonoBehaviour
{
    public BaseState activeState;

    public void Initialise()
    {
        //setup default state
        ChangeState(new PatrolState());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        //check activeState
        if(activeState != null) 
        {
            //cleanup activeState
            activeState.Exit();
        }
        //change to newState
        activeState = newState;

        if(activeState != null)
        {
            //setup newState
            activeState.stateMechine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
