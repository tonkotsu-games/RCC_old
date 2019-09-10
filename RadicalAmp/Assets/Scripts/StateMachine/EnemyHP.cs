using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.VFX;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] ParticleSystem[] bloodSplatter;

    [Header("Enemy Health")]
    public int life;

    private Animator EnemyAnim;
    private Renderer enemyDissolve;

    private float NavmeshSpeed;

    private NavMeshAgent EnemyNav;

    public bool death = false;

    [SerializeField] private EndScreenTrigger endScreen;



    // Start is called before the first frame update
    void Start()
    {
        EnemyAnim = gameObject.GetComponentInChildren<Animator>();
        EnemyAnim.SetBool("dead", false);
        EnemyNav = gameObject.GetComponent<NavMeshAgent>();
        enemyDissolve = gameObject.transform.GetChild(0).GetChild(1).GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {


        if (life <= 0 && !death)
        {
            // Adding to the score for enemies killed
            // ScoreTracker.instance.statContainer[0] += 1;

            if(LevelManager.instance != null)
            {
                Debug.Log("Removing " + gameObject.name + " from list");
                LevelManager.instance.DeleteFromEnemyCount(this.gameObject);
            }
            EnemyDeath();
            death = true;
        }
    }

    public void BloodSplat()
    {
        //Debug.Log("BLOOD!");
        ParticleSystem particles = bloodSplatter[Random.Range(0, bloodSplatter.Length)];
        particles.Play();
    }

    private void EnemyDeath()
    {
        if (ScoreTracker.instance != null)
        {
            ScoreTracker.instance.enemiesKilled++;
        }
       if(gameObject.GetComponent<Beathoven>() != null)
       {
           ScoreTracker.instance.timePassedInSeconds = Mathf.RoundToInt(Time.timeSinceLevelLoad);
           ScoreTracker.instance.CalclulateStats();
           endScreen.TriggerEndEvent();
       }
        EnemyAnim.SetBool("dead", true);
        Debug.Log(EnemyNav.gameObject.name + " died!");
        EnemyNav.gameObject.GetComponent<Actor>().Death();


        EnemyNav.gameObject.transform.LookAt(Locator.instance.GetPlayerPosition());
        //Debug.Log("Deregister Boss from list");


        //Respawn.enemyDead++;
    }
}
