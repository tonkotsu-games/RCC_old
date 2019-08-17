using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public static bool movementLocked = false;
    private NavMeshAgent agent;
    private TestFight testFight;
    private EnemyAttack enemyAttack;
    private BeatStrike beatStrike;

    private bool startAttackbreak = true;

    [Range(.5f, 20f)]

	public float lr = 3f;

	private Transform target;
    private Animator enemyAnim;


    public float distance;
    public float AttackBreak;

    public bool canAttack = false;
    public bool hit = false;

    public bool attack = false;

    private void Start()
	{
        if(testFight == null)
        {
            testFight = GameObject.FindWithTag("TestFight").GetComponent<TestFight>();
        }
        beatStrike = GameObject.FindWithTag("Player").GetComponent<BeatStrike>();
        enemyAttack = transform.GetComponentInChildren<EnemyAttack>();
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyAnim = gameObject.GetComponent<Animator>();
        enemyAnim.SetBool("attack", false);
        enemyAnim.SetBool("disturbed", false);
        enemyAnim.SetBool("cruising", false);
    }

    private void Update()
    {
        if (!movementLocked)
        {
            distance = Vector3.Distance(target.position, transform.position);
            if (distance >= lr)
            {
                enemyAnim.SetBool("cruising", false);
                return;
            }

            if (distance <= lr && !enemyAnim.GetBool("dead"))
            {

                agent.SetDestination(target.position);
                enemyAnim.SetBool("cruising", true);
                if (distance <= agent.stoppingDistance)
                {
                    FaceTarget();
                }
            }



            if (distance < agent.stoppingDistance)
            {
                enemyAnim.SetBool("cruising", false);
                if (testFight.windUp)
                {
                    if (canAttack && beatStrike.IsOnBeat())
                    {
                        WindUP();
                    }
                }
                else
                {
                    if (canAttack && beatStrike.IsOnBeat())
                    {
                        Attack();
                    }
                }

                if (startAttackbreak && !canAttack)
                {
                    StartCoroutine("ChangeAttackBool");
                }
            }
            if (enemyAnim.GetBool("windUpAttack"))
            {
                WindUpAttack();
            }
            if (hit)
            {
                //Debug.Log("HIT");
                hit = false;
                GetHit();
            }
        }

    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void GetHit()
    {
        if (testFight.windUp)
        {
            enemyAnim.SetBool("disturbed", true);
            enemyAnim.SetBool("windUp", false);
            enemyAnim.SetBool("windUpAttack", false);
            canAttack = false;
            StopCoroutine("ChangeAttackBool");
            StartCoroutine("ChangeAttackBool");
            enemyAttack.EndAttackAnimation();
        }
        else
        {
            attack = false;
            enemyAnim.SetBool("disturbed", true);
            enemyAnim.SetBool("attack", false);
            canAttack = false;
            StopCoroutine("ChangeAttackBool");
            StartCoroutine("ChangeAttackBool");
            enemyAttack.EndAttackAnimation();
        }

    }

    private void Attack()
    {
        attack = true;
        enemyAnim.SetBool("attack", true);
        //Debug.Log("HAS ATTACKED");
        canAttack = false;
    }

    private void WindUP()
    {
       
        enemyAnim.SetBool("windUp", true);
        //Debug.Log("Did Windup");
        canAttack = false;
    }

    private void WindUpAttack()
    {
        attack = true;
        enemyAnim.SetBool("windUp", false);
        //Debug.Log("easded");
        enemyAnim.SetBool("windUpAttack", true);
    }

    IEnumerator ChangeAttackBool()
    {
        //Debug.Log("Start");
        startAttackbreak = false;
        yield return new WaitForSeconds(AttackBreak);
        enemyAnim.SetBool("disturbed", false);
        canAttack = true;
    }
    public void EndAttack()
    {
        attack = false;
        enemyAnim.SetBool("attack", false);
        enemyAnim.SetBool("windUpAttack", false);
        enemyAttack.EndAttackAnimation();
        startAttackbreak = true;

    }
    public void EndWindUp()
    {
        enemyAnim.SetBool("windUp", false);
        enemyAnim.SetBool("windUpAttack", true);
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lr);
    }
}
        