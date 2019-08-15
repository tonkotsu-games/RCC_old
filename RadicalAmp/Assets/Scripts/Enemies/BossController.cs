using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{

    [SerializeField] States currentState = States.Idle;
    [Range(.5f, 5f)]
    [SerializeField] float detectionRange = 3f;
    [SerializeField] float attackBreak;
    [SerializeField] Text stateIndicator;

    private States lastState;
    public Transform Target;
    private EnemyMeleeAttack meleeAttack;
    private Animator enemyAnim;

    private float distance;
    private bool canAttack = true;

    private NavMeshAgent agent;

    public States CurrentState
    {
        get
        {
            return currentState;
        }

        private set
        {
            if (currentState != value)
            {
                lastState = currentState;
                Debug.Log("State Change: " + value);
                currentState = value;
                UpdateStateUI();
                UpdateNavMeshAgentStats();
            }
        }
    }

    public void Start()
    {
        currentState = States.Idle;

        Target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        meleeAttack = GetComponent<EnemyMeleeAttack>();
        enemyAnim = gameObject.GetComponent<Animator>();


    }

    private void Update()
    {

        distance = Vector3.Distance(Target.position, transform.position);

        if (distance >= detectionRange)
        {
            CurrentState = States.Idle;
            return;
        }

        if (distance < detectionRange)
        {
            if (currentState == States.PikeShieldStab || currentState == States.ShieldPlant || currentState == States.ShieldFortress)
            {
                return;
            }

            else
            {
                CurrentState = States.Chase;

                agent.SetDestination(Target.position); 

                if (distance <= agent.stoppingDistance)
                {
                    RotateTowardsTarget();
                }
            }
        }

        if (distance < agent.stoppingDistance)
        {
            if (canAttack)
            {
                CurrentState = States.ShieldPush;

                meleeAttack.AttackTarget();
                enemyAnim.Play("EnemyAttack", 0, 0);
                canAttack = false;

                StartCoroutine(SetAttackAfterBreak());
            }
        }       
    }

 
    private void UpdateNavMeshAgentStats()
    {
        
        if (currentState == States.ShieldPlant || currentState == States.ShieldFortress || currentState == States.PikeShieldStab)
        {
            agent.speed = 0f;
            agent.angularSpeed = 0f;
            agent.acceleration = 0f;
        }   
        else
        {
            agent.speed = 2f;
            agent.angularSpeed = 120f;
            agent.acceleration = 8f;
        }
    }

    private void UpdateStateUI()
    {
        stateIndicator.text = currentState.ToString();

        if (currentState == States.ShieldPush)
        {
            stateIndicator.color = Color.red;
        }
        else
        { 
            stateIndicator.color = Color.white;
        }
    }

    private void RotateTowardsTarget() 
    {
        Vector3 direction = (Target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private IEnumerator SetAttackAfterBreak()  
    {
        yield return new WaitForSeconds(attackBreak);
        canAttack = true;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }


    //Boss Behavior States
    public enum States
    {
        Idle, //Idle State - just standing around doing nothing
        Chase, //Chase State - when the Boss follows the player
        PikeStab, //PikeStab State - like the state below just without being planted down
        PikeShieldStab, //PikeShieldStab State - the Boss will stab the cube to the front fast
        ShieldPush, //ShieldPush State - which is basically like the other enemies regular attack
        ShieldPlant, //ShieldPlant State - the state in which the Boss plants down the shield/will be followed be the ShieldFortress State
        ShieldFortress, //ShieldFortress State - the state in which the Boss has his shield planted down and the player needs to dash against him
        Sing, //Sing State - he shoots out waves which spread out and damage the player --- maybe use the BunnyGirl Spread Attack for this?
        Death //Death State - when he is dead and before Despawn
    }
}

class BossState
{
    public readonly BossController.States State;

    public readonly TransitionDelegate DoesTransition;
    public readonly System.Action Update;
    public readonly System.Action Start;
    public delegate bool TransitionDelegate(out BossController.States state);

    public BossState(BossController.States myState, TransitionDelegate transitions, System.Action update, System.Action start)
    {
        State = myState;
        DoesTransition = transitions;
        Update = update;
        Start = start;
    }


         
}
