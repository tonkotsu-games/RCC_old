using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeAttack : MonoBehaviour
{
	[Range(.5f, 5f)]
	public float ar = 3.2f;

    //public Animator enemyAnim;

	private Transform target;
	public NavMeshAgent agent;

	public void Start()
	{
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
	}

    public void AttackTarget()
    {
        //keine Ahnung, es zieht ja kein Leben ab, sondern Juice, deswegen weiß ich nicht, wie viel Juice abgezogen wird, usw.
        //also hier nur ne Idee, wie ich es machen würde, dies das Ananas <3

        //enemyAnim.Play("EnemyAttack", 0, 0);
        //target.juice -= 1;
        //if (target.juice <= 0)
        //{
        //  target.respawn(); (?) wahrscheinlich eher einfach Juice Meter auf 0 haben anstelle von Respawn aber naja
        //}
        //Debug.Log("BAM");
    }


    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ar);
    }
}
