using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceDashClone : MonoBehaviour
{

    public List<GameObject> targets;
    public JuiceDash juiceDashScript;
    Vector3 startPos;
    List<GameObject> holograms;
    public GameObject hologram;
    [SerializeField] AnimationClip attackClip;
    Animator anim;
    bool dashing = false;
    public bool startAction = false;
    bool dashEnabled = false;
    bool delayActive = false;
    bool returned = false;
    public float dashSpeed = 0.1f;
    public float delay = 0.5f;
    Vector3 dashDirection;
    Vector3 nextPos;
    int targetCounter = 0;
    public GameObject dashParticlesPrefab;

    private Tutorial tutotial;

    // Start is called before the first frame update
    void Start()
    {
        tutotial = GameObject.FindWithTag("Canvas").GetComponent<Tutorial>();
        startPos = transform.position;
        anim = gameObject.GetComponent<Animator>();
        holograms = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dashEnabled && startAction)
        {
            Debug.Log("Clone Calculating Position");
            CalculateNextPos(targetCounter);
            targetCounter++;
        }
        else if(dashEnabled)
        {
            if (!dashing)
            {
                dashing = true;
                Debug.Log("enablingDashing");
                SpawnDashParticles();
                Physics.IgnoreLayerCollision(9, 13, true);
            }
            else
            {
                if (Vector3.Distance(transform.position, nextPos) >= 1)
                {
                    transform.LookAt(nextPos);
                    transform.position = Vector3.Lerp(transform.position, nextPos, dashSpeed*Time.deltaTime);
                }
                else
                {                  
                    if (!delayActive)
                    {
                        if (!returned)
                        {
                            GameObject holo = Instantiate(hologram, transform.position, Quaternion.identity);
                            holograms.Add(holo);
                        }
                        anim.Play("Dashing");
                        delayActive = true;
                        StartCoroutine(DelayBetweenDashes());
                    }
                }
            }
        }
    }

    void CalculateNextPos(int counter)
    {
        if (counter < targets.Count)
        {
            nextPos = targets[counter].transform.position;
            dashEnabled = true;
        }
        else if (counter == targets.Count)
        {
            returned = true;
            nextPos = startPos;
            dashEnabled = true;
        }
        else
        {
            startAction = false;
            targetCounter = 0;
            StartCoroutine(DelayBetweenKill());
        }
    }

    private void SpawnDashParticles()
    {
        GameObject particlesInstance = Instantiate(dashParticlesPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        particlesInstance.GetComponent<FollowPosition>().followTarget = gameObject.transform;
        particlesInstance.gameObject.transform.Rotate(-90, 0, 0);
        ParticleSystem parts = particlesInstance.GetComponentInChildren<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetime.constant + parts.main.startDelay.constant;
        Destroy(particlesInstance, totalDuration);
    }

    IEnumerator DelayBetweenDashes()
    {
        Debug.Log("starting waittime");
        yield return new WaitForSeconds(delay);
        delayActive = false;
        dashEnabled = false;
        dashing = false;
        Physics.IgnoreLayerCollision(9, 13, false);
    }

    IEnumerator DelayBetweenKill()
    {
        juiceDashScript.BackToNormal();
        SkinnedMeshRenderer[] renderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer rend in renderers)
        {
            rend.enabled = false;
        }

        int counter = 0;
        if(tutotial.currentStep == Tutorial.TutorialSteps.JuiceDashTest)
        {
            while (counter < targets.Count)
            {
                yield return new WaitForSeconds(0.1f);
                holograms[counter].GetComponent<Animator>().speed = 5;
                holograms[counter].GetComponent<TutorialClone>().PlayAttack(true);
                targets[counter].GetComponent<TutorialAttacks>().Death();
                targets[counter].GetComponentInChildren<Animator>().speed = 1;
                yield return new WaitForSeconds(attackClip.length * 0.2f);
                Destroy(holograms[counter]);
                counter++;
            }
        }
        else
        {
            while (counter < targets.Count)
            {
                yield return new WaitForSeconds(0.1f);
                holograms[counter].GetComponent<Animator>().speed = 5;
                holograms[counter].GetComponent<TutorialClone>().PlayAttack(true);
                targets[counter].GetComponent<EnemyHP>().BloodSplat();
                targets[counter].GetComponent<EnemyHP>().BloodSplat();
                targets[counter].GetComponent<EnemyHP>().BloodSplat();
                targets[counter].GetComponent<EnemyHP>().life = 0;
                targets[counter].GetComponentInChildren<Animator>().speed = 1;
                yield return new WaitForSeconds(attackClip.length * 0.2f);
                Destroy(holograms[counter]);
                counter++;
            }
        }
        Destroy(gameObject);
    }
}