using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BeathovenFeedback))]
public class Beathoven : Actor
{
    public NavMeshAgent navMeshAgent = null;

    public BossScriptableObject bossData = null;

    private Transform player;

    [SerializeField] BeatAnalyse beatanalyse;
    [SerializeField] int preStartAttack = 1000;

    public string specialAttackState;
    [SerializeField] int preStartSpecialAttack = 1000;

    

    private void Awake()
    {
        if(bossData == null)
        {
            Debug.LogError("bossData not set up!");
            return;
        }

        bossData.healthCurrent = bossData.healthMax;

        if(beatanalyse == null)
        {
            Debug.LogError("MusicBox in Beathoven not set up!");
        }

        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        this.StateMachine.ChangeState(new Idle(this));
    }

    protected override void Update()
    {
        base.Update();

        if(StateMachine.StateCurrent is Idle && player != null)
        {
            FaceTarget();
        }
    }

    public void OnDrawGizmosSelected()
    {
        if(bossData!=null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, bossData.aggroRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, bossData.meleeAttackRange);
        }
    }

    public void ChooseBehaviourAfterIdle(SearchResult searchResult)
    {
        var foundPlayers = searchResult.allHitObjectsWithRequiredTag;

        //Choose what to do by using info provided by the Search
        if(foundPlayers.Count > 0)
        {
            player = foundPlayers[0].gameObject.transform;

            //Attack Player in Melee AttackRange
            if (Vector3.Distance(player.position, this.gameObject.transform.position) < bossData.meleeAttackRange)
            {
              ChooseAttack();
            }
            //Walk to Player in AggroRange
            else if(Vector3.Distance(player.position, this.gameObject.transform.position) < (bossData.aggroRange + 0.5f))
            {
                StateMachine.ChangeState(new WalkTo(this, player, navMeshAgent, bossData.aggroRange, ActorData.meleeAttackRange));
            }
            else
            {
                StateMachine.ReturnToPreviousState();
            }
        }
        else
        {
            StateMachine.ReturnToPreviousState();
        }
    }

    public override void ChooseBehaviour()
    {
        if(StateMachine.StateCurrent is Idle)
        {
            StateMachine.ChangeState(new SearchFor(gameObject, bossData.aggroRange, "Player", ChooseBehaviourAfterIdle));
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

            if (specialAttackState == "waveAttack")
            {
                Debug.LogError("Now in WaveAttack");
                StateMachine.ChangeState(new WaveAttack(this, player));
            }
            else if(specialAttackState == "")
            {
                StateMachine.ChangeState(new Attack(this, player));
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
        if(state is Attack)
        {
            if(beatanalyse.IsOnBeat(preStartAttack))
            {
                return true;
            }
        }
        else if(state is WaveAttack)
        {
            if(beatanalyse.IsOnBeat(preStartSpecialAttack))
            {
                return true;
            }
        }

        return false;
    }
}
