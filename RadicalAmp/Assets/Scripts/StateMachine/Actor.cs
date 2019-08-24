using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private StateMachine stateMachine = new StateMachine();

    public StateMachine StateMachine { get => stateMachine; private set => stateMachine = value; }

    [SerializeField]
    private CharacterScriptableObject actorData;

    public virtual CharacterScriptableObject ActorData { get => actorData; private set => actorData = value; }

    public bool windupFinished;
    public bool attacking = false;

    private void Update()
    {
        StateMachine.StateExecuteTick();
    }

    public virtual void ChooseBehaviour()
    {

    }

    public virtual void FaceTarget()
    {

    }

    public virtual bool CheckBeat(IState state)
    {
        return false;
    }

    public virtual void Death()
    {
        StateMachine.ChangeState(new Death(this));
    }
}