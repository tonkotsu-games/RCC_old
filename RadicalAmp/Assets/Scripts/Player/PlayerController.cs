using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    List<Transform> enemiesInScene = new List<Transform>();
    List<Transform> enemiesInRange = new List<Transform>();
    public List<Transform> enemiesInCameraRange = new List<Transform>();

    private float moveHorizontal;
    private float moveVertical;

    [Header("Speed for the Movement")]
    [SerializeField] float movementSpeed;
    [SerializeField] float acceleration;
    [Header("Snapping setting")]
    [Range(0f, 10f)]
    [SerializeField] float snappRange;
    [Range(0f, 90f)]
    [SerializeField] float snappAngle;

    [Header("Enemy detection range")]
    [Range(0f, 15f)]
    [SerializeField] float enemyDetectionRange;

    [Header("Dash settings")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] GameObject dashParticlesPrefab;

    private Vector3 heading;
    private Vector3 dashdirection;
    private Vector3 moveVector;

    private Transform closest = null;
    
    private bool dash = false;
    private bool dashing = false;

    public static bool show = false;
    public static bool attack = false;
    public static bool dancing = false;
    public static bool attack1DONE;

    private PlayerController DeadDisable;
    private Respawn respawn;
    
    public Timer dashTimer = new Timer();

    [Header("Life setting")]
    public int life = 3;

    private AudioSource my_audioSource;
    private Animator anim;
    private Rigidbody rigi;
    private BoxCollider boxCol;

    [Header("Several Audiofiles")]
    public AudioClip dashClip;
    public AudioClip slashClip;

    //gibt an, welcher Dancemove abgespielt werden soll
    private int dancemove;
    
    void Start()
    {
        foreach(GameObject trans in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemiesInScene.Add(trans.transform);
        }

        respawn = GameObject.FindWithTag("Respawn").GetComponent<Respawn>();

        dashing = false;
        dash = false;
        show = false;
        attack = false;
        dancing = false;
        attack1DONE = false;

        dashTimer.Start(dashTime);
        boxCol = GetComponent<BoxCollider>();
        boxCol.enabled = false;

        dancemove = 0;

        rigi = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        DeadDisable = gameObject.GetComponent<PlayerController>();
        my_audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        anim.SetFloat("PosX", moveHorizontal);
        anim.SetFloat("PosY", moveVertical);

        if(Input.GetButtonDown("Attack") && !attack && !dancing && life > 0)
        {
            boxCol.enabled = true;
            Snapping();

            if (attack1DONE == false)
            {
                anim.Play("Attack", 0, 0);
                attack = true;
                my_audioSource.clip = slashClip;
                my_audioSource.Play();
                attack1DONE = true;
            }
            else 
            {
                anim.Play("Attack2", 0, 0);
                attack = true;
                my_audioSource.clip = slashClip;
                my_audioSource.Play();
                attack1DONE = false;
            }
        }
        if(!dash && Input.GetButtonDown("Dash") && !dancing)
        {
            anim.Play("Dashing");
            my_audioSource.clip = dashClip; 
            my_audioSource.Play(); 
            dash = true;
        }
        if(Input.GetButtonDown("Dance") && !dancing && !attack && !dash)
        {

            if (dancemove == 0)
            {
                anim.SetBool("dance", true);
                dancing = true;
                dancemove = 1;
            }
            else if (dancemove == 1)
            {
                anim.SetBool("dance2", true);
                dancing = true;
                dancemove = 2;
            }
            else
            {
                anim.SetBool("dance3", true);
                dancing = true;
                dancemove = 0;
            }

        }
        if(life <= 0)
        {
            //Adding to the death counter for the scoreboard
            ScoreTracker.instance.statContainer[5]++;
                anim.Play("Death");
                DeadDisable.enabled = false;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(!show)
            {
                show = true;
            }
            else if(show)
            {
                show = false;
            }
        }
        EnemyCount();
    }

    private void FixedUpdate()
    {
        MovementCalculation();

        if (!IsGrounded.isGrounded)
        {
            Gravity();
        }
        if (!dashing && !dancing && IsGrounded.isGrounded)
        {
            Move();
        }
        if ((Input.GetAxisRaw("Horizontal") >= 0.1 ||
             Input.GetAxisRaw("Horizontal") <= -0.1 ||
             Input.GetAxisRaw("Vertical") >= 0.1 ||
             Input.GetAxisRaw("Vertical") <= -0.1) &&
             !dashing &&
             !dancing)
        {
            heading = new Vector3(Input.GetAxisRaw("Horizontal"),
                                  0,
                                  Input.GetAxisRaw("Vertical"));
            heading = heading.normalized;

            anim.SetBool("running", true);
            Turn();
        }
        else
        {
            anim.SetBool("running", false);
        }        
        Dashing();
    }

    void MovementCalculation()
    {
        moveVector = new Vector3(moveHorizontal, 0f, moveVertical);

        moveVector = moveVector.normalized * movementSpeed;
    }

    /// <summary>
    /// Function for the Movement
    /// </summary>
    void Move()
    {
        rigi.velocity = new Vector3(moveVector.x,
                                    0,
                                    moveVector.z);        
    }

    void Gravity()
    {
        rigi.velocity = new Vector3(moveVector.x,
                                    -10f,
                                    moveVector.z);
    }
    /// <summary>
    /// Function for dashing in the direction you are facing
    /// </summary>
    void Dashing()
    {
        if (dashing == false)
        {
            if (dash)
            {
                dashdirection = heading;
                dashing = true;
                attack = false;
                SpawnDashParticles();
            }
        }
        else
        {
            if(dashTimer.timeCurrent <= 0)
            {
                dashing = false;
                rigi.velocity = Vector3.zero;
                dashTimer.ResetTimer();
                dash = false;
            }
            else
            {
                dashTimer.Tick();
                rigi.velocity += dashdirection * dashSpeed;
            }
        }
    }

    private void Snapping()
    {
        var closestAngle = snappAngle;

        for (int i = 0; i < enemiesInScene.Count; i++)
        {
            if(enemiesInScene[i] == null)
            {
                enemiesInScene.Remove(enemiesInScene[i]);
                i--;
            }
            else if(Vector3.Distance(enemiesInScene[i].position, gameObject.transform.position) <= snappRange)
            {
                enemiesInRange.Add(enemiesInScene[i]);
            }
        }
        for(int i = 0;i < enemiesInRange.Count; i++)
        {
            if (Vector3.Angle((enemiesInRange[i].position - transform.position), transform.forward) <= closestAngle)
            {
                closest = enemiesInRange[i];
                closestAngle = Vector3.Angle((enemiesInRange[i].position - transform.position), transform.forward);
            }
        }
        if (closest != null)
        {
            transform.rotation = Quaternion.LookRotation(closest.position - transform.position);
            closest = null;
        }
        enemiesInRange.Clear();
    }

    /// <summary>
    /// Function to set you face direction
    /// </summary>
    void Turn()
    {
        if (!attack)
        {
            transform.rotation = Quaternion.LookRotation(heading);
        }
    }
    public void AfterAttack()
    {
        attack = false;
        boxCol.enabled = false;
        BeatStrike.beatAttack = false;
    }
    public void AfterDancing()
    {        
        dancing = false;
        anim.SetBool("dance", false);
        anim.SetBool("dance2", false);
        anim.SetBool("dance3", false);
    }
    public void AfterDash()
    {
        dash = false;
    }
    public void afterdeath()
    {
        respawn.RespawnPlayer();
        life = 3;
        anim.Play("respawn");        
    }
    public void afterrespawn()
    {
        anim.SetBool("backtoidle", true);
        DeadDisable.enabled = true;
    }

    private void SpawnDashParticles()
    {
        GameObject particlesInstance = Instantiate(dashParticlesPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        particlesInstance.GetComponent<FollowPosition>().followTarget = gameObject.transform;
        particlesInstance.gameObject.transform.Rotate(-90,0,0);
        ParticleSystem parts = particlesInstance.GetComponentInChildren<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetime.constant + parts.main.startDelay.constant;
        Destroy(particlesInstance, totalDuration);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, snappRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRange);
    }
    private void EnemyCount()
    {
        for (int i = 0; i < enemiesInScene.Count ;i++)
        {
            if(Vector3.Distance(enemiesInScene[i].position, transform.position) <= enemyDetectionRange)
            {
                if(enemiesInCameraRange.Contains(enemiesInScene[i]))
                {

                }
                else
                {
                    enemiesInCameraRange.Add(enemiesInScene[i]);
                }
            }
            if(Vector3.Distance(enemiesInScene[i].position, transform.position) >= enemyDetectionRange)
            {
                if (enemiesInCameraRange.Contains(enemiesInScene[i]))
                {
                    enemiesInCameraRange.Remove(enemiesInScene[i]);
                }
            }
        }
        CameraFollow.EnemyCheck(enemiesInCameraRange.Count);
    }
}