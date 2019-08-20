using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private StateMachine stateMachine = new StateMachine();

    public StateMachine StateMachine { get => stateMachine; private set => stateMachine = value; }

    [SerializeField]
    private CharacterScriptableObject actorData;

    public CharacterScriptableObject ActorData { get => actorData; private set => actorData = value; }

    private void Update()
    {
        StateMachine.StateExecuteTick();
    }
}