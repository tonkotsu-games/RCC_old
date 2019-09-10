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
    [Range(0f,20f)]
    [SerializeField] float abilityRange = 20;
    public float markerOffset = 5;
    Animator playerAnim;

    public GameObject playerClone;
    //public int enemiesTargetedPerCharge = 3;
    private bool beat = false;
    private bool abilityReady = false;
    private bool triggerLeft = false;

    Collider[] enemiesInRange;
    LayerMask layerMask = 1 << 13;
    public List<GameObject> markedTargets;
    List<GameObject> activeMarkers;

    //State Maschine
    public enum ChargeStates { none, first, second, final, success, punish }
    ChargeStates currentState = ChargeStates.none;
    ChargeStates nextState = ChargeStates.first;

    public Material juiceTargetMat;
    AudioSource source;
    public AudioClip[] chargeClips;

    CameraFollow nikCam;
    Vector3 zoomSpeedZeroBase;
    Vector3 zoomSpeedOneBase;
    Vector3 zoomSpeedTwoBase;
    Vector3 zoomSpeedFourBase;
    public float zoomSpeedYIncrease;

   //bool shakeEnabled = false;
   //float shakeIntensity;
   //public float shakeIntensityFirst;
   //public float shakeIntensitySecond;
   //public float shakeIntensityFinal;
   //
   //public float shakeDuration;
   //public float shakeSpeed;
   //float shakeTimer;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
        //source = gameObject.GetComponent<AudioSource>();
        markedTargets = new List<GameObject>();
        activeMarkers = new List<GameObject>();
        juiceMeter = Locator.instance.GetJuiceMeter();
        beatAnalyse = Locator.instance.GetBeat();
        nikCam = Locator.instance.GetNikCam().GetComponent<CameraFollow>() ;
        zoomSpeedZeroBase = nikCam.zoomSpeedZero;
        zoomSpeedOneBase = nikCam.zoomSpeedOne;
        zoomSpeedTwoBase = nikCam.zoomSpeedTwo;
        zoomSpeedFourBase = nikCam.zoomSpeedFour;
        //shakeTimer = shakeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("LeftTrigger") >= 0.5f)
        {
            triggerLeft = true;
        }
        else
        {
            triggerLeft = false;
        }

        //if (shakeEnabled)
        //{
        //    JuiceDashCamShake();
        //}
        //  if (currentState != ChargeStates.final)
        //  {
        if (abilityReady)
        {
            if (Input.GetButtonUp("Dash"))
            {
                ChangeChargeState(ChargeStates.success);
            }
        }
        if (Input.GetButton("Dash") &&
            triggerLeft)
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
            //Debug.Log("Current State: " + currentState);

            if (requestedState == ChargeStates.none)
            {
                markedTargets.Clear();
                abilityReady = false;
                DisableAllMarkers();
                //Debug.Log("Charge failed");
                nextState = ChargeStates.first;
            }

            else if (requestedState == ChargeStates.first)
            {
                if (juiceMeter.value >= 90)
                {
                    playerAnim.SetTrigger("JuiceDash1");
                    //nikCam.gameObject.GetComponent<CameraShake>().enabled = false ;
                    //shakeIntensity = shakeIntensityFirst;
                    //shakeEnabled = true;
                    CameraFollow.juiceDashActive = true;
                    nikCam.zoomSpeedTwo = new Vector3(zoomSpeedTwoBase.x, zoomSpeedTwoBase.y + zoomSpeedYIncrease, zoomSpeedTwoBase.z);
                    CameraFollow.ChangeCameraState(2);
                    //AudioSource source1 = gameObject.AddComponent<AudioSource>();
                    //source1.clip = chargeClips[0];
                    //source1.Play();
                    CollectEnemies();
                    if (enemiesInRange.Length != 0)
                    {
                        markedTargets.Add(enemiesInRange[0].gameObject);
                        gameObject.transform.LookAt(markedTargets[0].transform);
                        DisplayMarkerOnTarget(markedTargets[0]);
                        if (markedTargets[0].GetComponent<NavMeshAgent>() != null)
                        {
                            foreach (GameObject target in markedTargets)
                            {
                                target.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                            }
                        }
                    }
                    abilityReady = true;
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
                //shakeIntensity = shakeIntensitySecond;
                playerAnim.SetTrigger("JuiceDash2");
                nikCam.zoomSpeedOne = new Vector3(zoomSpeedOneBase.x, zoomSpeedOneBase.y + zoomSpeedYIncrease, zoomSpeedOneBase.z);
                CameraFollow.ChangeCameraState(1);
                for (int i = 1; i <= 2; i++)
                {
                    if (i <= enemiesInRange.Length - 1)
                    {
                        markedTargets.Add(enemiesInRange[i].gameObject);

                    }
                }
                foreach (GameObject target in markedTargets)
                {
                    if (target.GetComponent<NavMeshAgent>() != null)
                    {
                        DisplayMarkerOnTarget(target);
                        target.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                    }
                }
                nextState = ChargeStates.final;
                juiceMeter.value -= juiceConsumedPerCharge;
            }
            else if (requestedState == ChargeStates.final)
            {
                //AudioSource source3 = gameObject.AddComponent<AudioSource>();
                //source3.clip = chargeClips[3];
                //source3.Play();
                //shakeIntensity = shakeIntensityFinal;
                Physics.IgnoreLayerCollision(9, 13, true);
                playerAnim.SetTrigger("JuiceDash3");
                nikCam.zoomSpeedZero = new Vector3(zoomSpeedZeroBase.x, zoomSpeedZeroBase.y + zoomSpeedYIncrease, zoomSpeedZeroBase.z);
                CameraFollow.ChangeCameraState(0);
                for (int i = 3; i <= 5; i++)
                {
                    if (i <= enemiesInRange.Length - 1)
                    {
                        markedTargets.Add(enemiesInRange[i].gameObject);
                    }
                }
                foreach (GameObject target in markedTargets)
                {
                    if (target.GetComponent<NavMeshAgent>() != null)
                    {
                        DisplayMarkerOnTarget(target);
                        target.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                    }
                }
                nextState = ChargeStates.success;
                juiceMeter.value -= juiceConsumedPerCharge;
            }
            else if (requestedState == ChargeStates.success)
            {
                if (markedTargets.Count != 0)
                {
                    transform.LookAt(markedTargets[0].transform);
                }
                nextState = ChargeStates.none;
                SpawnClone();
                ChangeChargeState(nextState);
            }
            else if (requestedState == ChargeStates.punish)
            {
                ChangeChargeState(ChargeStates.none);
            }
        }
    }

    void CollectEnemies()
    {
        enemiesInRange = Physics.OverlapSphere(transform.position, abilityRange, layerMask);
    }

    void DisplayMarkerOnTarget(GameObject target)
    {
        if (markedTargets.Count != 0)
        {
            if (target.GetComponentInChildren<Beathoven>() != null)
            {
                Debug.Log("Its a wild beathoven");
            }
            else
            {
                SkinnedMeshRenderer rend = target.GetComponentInChildren<SkinnedMeshRenderer>();
                rend.material = juiceTargetMat;
                target.GetComponentInChildren<Animator>().speed = 0;
                if (target.GetComponent<NavMeshAgent>() != null)
                {
                    target.GetComponent<NavMeshAgent>().isStopped = true;
                }
            }
        }
    }

    void DisableAllMarkers()
    {
        if (activeMarkers.Count != 0)
        {
            foreach (GameObject marker in activeMarkers)
            {
                Destroy(marker);
            }
        }
    }

    void SpawnClone()
    {
        //nikCam.gameObject.GetComponent<CameraShake>().enabled = true;
        nikCam.zoomSpeedFour = new Vector3(zoomSpeedFourBase.x, zoomSpeedFourBase.y + zoomSpeedYIncrease*2, zoomSpeedFourBase.z);
        CameraFollow.ChangeCameraState(4);
        GameObject clone = Instantiate(playerClone, transform.position, Quaternion.identity);
        clone.GetComponent<JuiceDashClone>().juiceDashScript = this;
        clone.GetComponent<JuiceDashClone>().targets = new List<GameObject>(markedTargets);
        clone.GetComponent<JuiceDashClone>().startAction = true;
        playerAnim.SetTrigger("JuiceDashUSED");
        TurnPlayerONandOff(false);
    }

    void TurnPlayerONandOff(bool value)
    {
        SkinnedMeshRenderer[] renderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            renderer.enabled = value;
        }
        gameObject.GetComponentInChildren<PlayerAttackCheck>().gameObject.SetActive(value);
        gameObject.GetComponent<CapsuleCollider>().enabled = value;
        gameObject.GetComponent<VFXIntensity>().enabled = value;
        gameObject.GetComponent<PlayerController>().enabled = value;
    }

    public void BackToNormal()
    {
        nikCam.zoomSpeedZero = zoomSpeedZeroBase;
        nikCam.zoomSpeedOne = zoomSpeedOneBase;
        nikCam.zoomSpeedTwo = zoomSpeedTwoBase;
        nikCam.zoomSpeedFour = zoomSpeedFourBase;
        CameraFollow.juiceDashActive = false;
        TurnPlayerONandOff(true);
        Physics.IgnoreLayerCollision(9, 13, false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, abilityRange);
    }
    //void JuiceDashCamShake()
    //{
    //    if (shakeTimer > 0)
    //    {
    //        nikCam.gameObject.GetComponent<Camera>().fieldOfView -= shakeIntensity;
    //
    //        shakeDuration -= Time.deltaTime * shakeSpeed;
    //    }
    //    else
    //    {
    //        shakeTimer = shakeDuration;
    //        nikCam.gameObject.GetComponent<Camera>().fieldOfView = 60;
    //    }
    //}
}