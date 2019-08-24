using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BasicEnemyFeedback))]
public class BasicEnemy : Actor
{
    public int healthCurrent;
    
    public NavMeshAgent navMeshAgent = null;

    public BasicEnemyScriptableObject basicEnemyData = null;

    private Transform player;

    [SerializeField] BeatAnalyse beatanalyse;
    [SerializeField] int preStartAttack = 1000;
    [SerializeField] int preStartSpecialAttack = 1000;

    [SerializeField] string specialAttackState;

    

    private void Awake()
    {
        if(basicEnemyData == null)
        {
            Debug.LogError("basicEnemyData not set up!");
            return;
        }

        healthCurrent = basicEnemyData.healthMax;

        if(beatanalyse == null)
        {
            Debug.LogError("MusicBox in " + gameObject.name + " not set up!");
        }

        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        this.StateMachine.ChangeState(new Idle(this));
    }

    private void Update()
    {
        StateMachine.StateExecuteTick();

        if(StateMachine.StateCurrent is Idle && player != null)
        {
            FaceTarget();
        }
    }

    public void OnDrawGizmosSelected()
    {
        if(basicEnemyData!=null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, basicEnemyData.aggroRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, basicEnemyData.meleeAttackRange);
        }
    }

    public void ChooseBehaviourAfterIdle(SearchResult searchResult)
    {
        var foundPlayers = searchResult.allHitObjectsWithRequiredTag;

        //Choose what to do by using info provided by the Search
        if(foundPlayers.Count > 0)
        {
            //Debug.Log("not empty " + foundPlayers.Count);
            player = foundPlayers[0].gameObject.transform;
            //Debug.Log(player.gameObject.name);
            //Debug.Log("player " + player.position + " BasicEnemy " + this.gameObject.transform.position );

            //Attack Player in Melee AttackRange
            if (Vector3.Distance(player.position, this.gameObject.transform.position) < basicEnemyData.meleeAttackRange)
            {
              ChooseAttack();
            }
            //Walk to Player in AggroRange
            else if(Vector3.Distance(player.position, this.gameObject.transform.position) < (basicEnemyData.aggroRange + 0.5f))
            {
                //Debug.Log("went to walkTo");
                StateMachine.ChangeState(new WalkTo(this, player, navMeshAgent, basicEnemyData.aggroRange, ActorData.meleeAttackRange));
            }
            else
            {
                StateMachine.ReturnToPreviousState();
                //Debug.Log("Did nothing");
            }
        }
        else
        {
            //else Debug.Log("No Player found");
            //Debug.Log("returned to previous");
            StateMachine.ReturnToPreviousState();
        }
    }

    public override void ChooseBehaviour()
    {
        if(StateMachine.StateCurrent is Idle)
        {
            //Debug.Log("Choosing to Search");
            StateMachine.ChangeState(new SearchFor(gameObject, basicEnemyData.aggroRange, "Player", ChooseBehaviourAfterIdle));
        }
        else if(StateMachine.StateCurrent is WalkTo)
        {
            ChooseAttack();
        }
    }

    private void ChooseAttack()
    {
        int randomNumber = Random.Range(0, 5);
        Debug.Log("RandomNumber " + randomNumber);

        if (randomNumber < 4)
        {
            StateMachine.ChangeState(new Attack(this, player));
        }
        else
        {
            Debug.Log("SpecialAttack!");

            if (specialAttackState == "specialAttack")
            {
                StateMachine.ChangeState(new SpecialAttack(this, player));
            }
        }
    }

    public override void FaceTarget()
    {
        Vector3 direction = (player.position - this.gameObject.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        this.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2.5f);
    }

    public override bool CheckBeat(IState state)
    {
        //Debug.Log("CheckingBeat");
        if(state is Attack)
        {
            //Debug.Log("IsAttack");
            if(beatanalyse.IsOnBeat(preStartAttack))
            {
                //Debug.Log("HitBeat");
                return true;
            }
        }
        else if(state is SpecialAttack)
        {
            //Debug.Log("IsAttack");
            if(beatanalyse.IsOnBeat(preStartSpecialAttack))
            {
                //Debug.Log("HitBeat");
                return true;
            }
        }

        return false;
    }
}
