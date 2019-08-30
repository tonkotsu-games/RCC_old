using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.VFX;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] VisualEffect blood;
    [SerializeField] ParticleSystem[] bloodSplatter;

    [Header("Enemy Health")]
    public int life;

    private Animator EnemyAnim;
    private Renderer enemyDissolve;

    private float NavmeshSpeed;

    private NavMeshAgent EnemyNav;

    public bool death = false;

    [SerializeField] BoxCollider endScreenTrigger;



    // Start is called before the first frame update
    void Start()
    {
        EnemyAnim = gameObject.GetComponentInChildren<Animator>();
        EnemyAnim.SetBool("dead", false);
        EnemyNav = gameObject.GetComponent<NavMeshAgent>();
        blood.SetFloat("Velocity Multiplier", 6f);
        blood.Stop();
        enemyDissolve = gameObject.transform.GetChild(0).GetChild(1).GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {


        if (life <= 0 && !death)
        {
            // Adding to the score for enemies killed
            // ScoreTracker.instance.statContainer[0] += 1;
            EnemyDeath();
            death = true;
        }
    }

    public void BloodSplat()
    {
        blood.Play();
        //Debug.Log("BLOOD!");
        bloodSplatter[Random.Range(0,bloodSplatter.Length)].Play();
    }

    private void EnemyDeath()
    {
       if(gameObject.GetComponent<Beathoven>() != null)
       {
           endScreenTrigger.enabled = true;
       }
        EnemyAnim.SetBool("dead", true);
        Debug.Log(EnemyNav.gameObject.name + " died!");
        EnemyNav.gameObject.GetComponent<Actor>().Death();


        //Debug.Log("Deregister Boss from list");


        //Respawn.enemyDead++;
    }
}
