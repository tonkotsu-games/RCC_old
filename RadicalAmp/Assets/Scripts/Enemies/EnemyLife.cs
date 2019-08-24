using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.VFX;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] VisualEffect blood;

    [Header("Enemy Health")]
    public int life;

    private Animator EnemyAnim;

    private float NavmeshSpeed;

    private NavMeshAgent EnemyNav;

    public bool death = false;

    // Start is called before the first frame update
    void Start()
    {
        EnemyAnim = gameObject.GetComponent<Animator>();
        EnemyAnim.SetBool("dead", false);
        EnemyNav = gameObject.GetComponent<NavMeshAgent>();
        blood.SetFloat("Velocity Multiplier", 6f);
        blood.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0 && !death)
        {
            EnemyDeath();
            death = true;
        }
    }

    public void BloodSplat()
    {
        blood.Play();
        //Debug.Log("BLOOD!");
    }

    public void AfterDeath()
    {
        //StageClearedManager.instance.DeRegisterFromList(transform.parent);
        //Debug.Log("Deregister Bunny from list");
        Destroy(this.gameObject.transform.parent.gameObject);      
    }

    private void EnemyDeath()
    {
        EnemyAnim.SetBool("dead", true);
        StageClearedManager.instance.DeRegisterFromList(transform.parent);
        //Debug.Log("Deregister Boss from list");


        //Respawn.enemyDead++;
    }
    
}
