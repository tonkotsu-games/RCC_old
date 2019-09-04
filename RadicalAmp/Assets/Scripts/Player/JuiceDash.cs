using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class JuiceDash : MonoBehaviour
{

    Slider juiceMeter;
    BeatAnalyse beatAnalyse;
    public float juiceConsumedPerCharge = 30;
    public float abilityRange = 20;
    public float markerOffset = 5;

    public GameObject playerClone;
    //public int enemiesTargetedPerCharge = 3;
    bool beat = false;
    bool abilityReady = false;

    Collider[] enemiesInRange;
    LayerMask layerMask = 1 << 13;
    public List<GameObject> markedTargets;
    List<GameObject> activeMarkers;

    //State Maschine
    public enum ChargeStates { none, first, second, final, success, punish }
    ChargeStates currentState = ChargeStates.none;
    ChargeStates nextState = ChargeStates.first;

    AudioSource source;
    public AudioClip[] chargeClips;


    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        markedTargets = new List<GameObject>();
        activeMarkers = new List<GameObject>();
        juiceMeter = Locator.instance.GetJuiceMeter();
        beatAnalyse = Locator.instance.GetBeat();
    }

    // Update is called once per frame
    void Update()
    {


        //  if (currentState != ChargeStates.final)
        //  {
        if (abilityReady)
        {
            if (Input.GetButtonUp("Attack"))
            {
                ChangeChargeState(ChargeStates.success);
            }
        }
        if (Input.GetButton("Attack"))
        {
            if (beatAnalyse.IsOnBeat(1000) && beat == false)
            {
                
                beat = true;
                Debug.Log("Changing ChargeState");
                ChangeChargeState(nextState);

            }

            else if (!beatAnalyse.IsOnBeat(1000))
            {
                beat = false;
            }
        }
        // }
        //else
        //{
        //   // if(beatAnalyse.IsOnBeat(1000)&&beat == false)
        //   // {
        //   //     ChangeChargeState(ChargeStates.success);
        //   // }
        //   // if (Input.GetButtonUp("Attack") && beatAnalyse.IsOnBeat(1000) && beat == false)
        //   // {
        //   //     Debug.Log("Using Juice Dash");
        //   //     beat = true;
        //   //     ChangeChargeState(nextState);
        //   // }
        //   // else if (Input.GetButtonUp("Attack"))
        //   // {
        //   //     Debug.Log("Punish because released button too early");
        //   //     ChangeChargeState(ChargeStates.punish);
        //   // }
        //   // else if (Input.GetButton("Attack") && beatAnalyse.IsOnBeat(1000) && beat == false)
        //   // {
        //   //     Debug.Log("Punish because held too long");
        //   //     beat = true;
        //   //     ChangeChargeState(ChargeStates.punish);
        //   // }
        //   //
        //   else if (!beatAnalyse.IsOnBeat(1000))
        //   {
        //       beat = false;
        //   }
        //}

    }

    void ChangeChargeState(ChargeStates requestedState)
    {
        if (requestedState == currentState)
        {
            Debug.Log("already in state " + requestedState);
        }
        else
        {

            currentState = requestedState;
            Debug.Log("Current State: " + currentState);

            if (requestedState == ChargeStates.none)
            {
                markedTargets.Clear();
                abilityReady = false;
                DisableAllMarkers();
                Debug.Log("Charge failed");
                nextState = ChargeStates.first;
            }

            else if (requestedState == ChargeStates.first)
            {
                if (juiceMeter.value >= 90)
                {
                    //AudioSource source1 = gameObject.AddComponent<AudioSource>();
                    //source1.clip = chargeClips[0];
                    //source1.Play();
                    StartCoroutine(ReadyUpDelay());
                    CollectEnemies();
                    if (enemiesInRange.Length != 0)
                    {
                        source.clip = chargeClips[0];
                        source.Play();
                        markedTargets.Add(enemiesInRange[0].gameObject);
                        DisplayMarkerOnTarget(markedTargets[0]);
                        foreach (GameObject t in markedTargets)
                        {
                            t.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                        }
                    }
                    nextState = ChargeStates.second;
                    juiceMeter.value -= juiceConsumedPerCharge;
                }
                else
                {
                    ChangeChargeState(ChargeStates.none);
                }

            }
            else if (requestedState == ChargeStates.second)
            {
               //AudioSource source2 = gameObject.AddComponent<AudioSource>();
               //source2.clip = chargeClips[1];
               //source2.Play();
                for (int i = 1; i <= 2; i++)
                {
                    // i == 2 i want to add the 3rd enemy in range  
                    // 2 >= 2 enemies in range
                    if (i <= enemiesInRange.Length - 1)
                    {
                        markedTargets.Add(enemiesInRange[i].gameObject);

                    }
                }

                Debug.Log("Updating Markers");
                foreach (GameObject t in markedTargets)
                {
                    DisplayMarkerOnTarget(t);
                }
                foreach (GameObject t in markedTargets)
                {
                    t.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                }
                nextState = ChargeStates.final;
                juiceMeter.value -= juiceConsumedPerCharge;
            }


            else if (requestedState == ChargeStates.final)
            {
               //AudioSource source3 = gameObject.AddComponent<AudioSource>();
               //source3.clip = chargeClips[3];
               //source3.Play();
                for (int i = 3; i <= 5; i++)
                {
                    if (i <= enemiesInRange.Length - 1)
                    {
                        markedTargets.Add(enemiesInRange[i].gameObject);
                    }
                }


                Debug.Log("Updating Markers");
                foreach (GameObject t in markedTargets)
                {
                    DisplayMarkerOnTarget(t);
                }
                foreach (GameObject t in markedTargets)
                {
                    t.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                }


                nextState = ChargeStates.success;
                juiceMeter.value -= juiceConsumedPerCharge;
            }
            else if (requestedState == ChargeStates.success)
            {
                nextState = ChargeStates.none;
                SpawnClone(markedTargets[0]);
                ChangeChargeState(nextState);

            }
            else if (requestedState == ChargeStates.punish)
            {
                ChangeChargeState(ChargeStates.none);
            }

        }
    }

    IEnumerator ReadyUpDelay()
    {
        yield return new WaitForSeconds(0.2f);
        abilityReady = true;
    }

    void CollectEnemies()
    {
        enemiesInRange = Physics.OverlapSphere(transform.position, abilityRange, layerMask);

    }

    void DisplayMarkerOnTarget(GameObject target)
    {
        if (markedTargets.Count != 0)
        {
            var marker = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), target.transform.position + new Vector3(0, markerOffset, 0), Quaternion.identity);
            marker.transform.parent = target.transform;
            marker.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            activeMarkers.Add(marker);
        }
    }
    void DisableAllMarkers()
    {
        if (activeMarkers.Count != 0)
        {
            foreach (GameObject m in activeMarkers)
            {
                Destroy(m);
            }
        }
    }


    void SpawnClone(GameObject target)
    {
        GameObject clone = Instantiate(playerClone, transform.position, Quaternion.identity);
        clone.GetComponent<JuiceDashClone>().juiceDashScript = this;
        clone.GetComponent<JuiceDashClone>().targets = new List<GameObject>(markedTargets);
        clone.GetComponent<JuiceDashClone>().startAction = true;
        TurnPlayerONandOff(false);


    }

    void TurnPlayerONandOff(bool value)
    {
        SkinnedMeshRenderer[] renderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer rend in renderers)
        {
            rend.enabled = value;
        }
        gameObject.GetComponentInChildren<PlayerAttackCheck>().gameObject.SetActive(value);
        gameObject.GetComponent<CapsuleCollider>().enabled = value;
        gameObject.GetComponent<VFXIntensity>().enabled = value;
        gameObject.GetComponent<PlayerController>().enabled = value;
    }

    public void BackToNormal()
    {
        TurnPlayerONandOff(true);
        markedTargets.Clear();
    }
    private void OnGUI()
    {
        GUILayout.Label(currentState.ToString());
        GUILayout.Label("Ability Ready: " + abilityReady);
    }
}
    


