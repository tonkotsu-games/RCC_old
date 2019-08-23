using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : IState
{
    Actor actor;
    bool animationFinished;

    public Death(Actor actor)
    {
        this.actor = actor;
    }

    public void Enter()
    {
        //Debug.Log(actor.gameObject.name + " Died");
        actor.StateMachine.TogglePause();
    }

    public void Execute()
    {

    }
    public void Exit()
    {

    }
}
