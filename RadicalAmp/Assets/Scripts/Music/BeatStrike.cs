using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.VFX;


public class BeatStrike : MonoBehaviour
{
    public static BeatStrike instance;
    private Slider juiceMeter;

    public AudioSource wave;

    [SerializeField] VisualEffect particleLeft;
    [SerializeField] VisualEffect particleRight;

    [SerializeField] int reactionTime;
    [Header("Time in Samples (10.000 = 0,25sec)")]
    [SerializeField] int windowTrigger;
    float timeSample;

    bool action = false;
    bool punish = false;
    public static bool beatAttack = false;

    [Header("Juice Reward in int")]
    [SerializeField] int dashReward;
    [SerializeField] int attackReward;
    [SerializeField] int danceReward;

    [Header("Juice Punish in int")]
    [SerializeField] int dashPunish;
    [SerializeField] int attackPunish;
    [SerializeField] int dancePunish;
    [SerializeField] int idlePunish;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        wave = GameObject.FindWithTag("Beat").GetComponent<AudioSource>();
        juiceMeter = GameObject.FindWithTag("JuiceMeter").GetComponent<Slider>();
        particleLeft.Stop();
        particleRight.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Dash"))
        {
            if(IsOnBeat())
            {
                juiceMeter.value += dashReward;
                particleLeft.Play();
                particleRight.Play();
                action = true;
            }else
            {
                juiceMeter.value -= dashPunish;
            }
        }
        else if (Input.GetButtonDown("Attack"))
        {
            if(IsOnBeat())
            {
                juiceMeter.value += attackReward;
                beatAttack = true;
                action = true;
            }
            else
            {
                juiceMeter.value -= attackPunish;
            }
        }
        else if(Input.GetButtonDown("Dance"))
        {
            if(IsOnBeat())
            {
                juiceMeter.value += danceReward;
                action = true;
            }
            else
            {
                juiceMeter.value -= dancePunish;
            }
        }
        Punishing();
    }

    private void Punishing()
    {
        if(!IsOnBeat() && !action && !punish)
        {
            juiceMeter.value -= idlePunish;
            punish = true;
        }else if(!IsOnBeat() && action && !punish)
        {
            punish = true;
            action = false;
        }else if(IsOnBeat())
        {
            punish = false;
        }

        if (PlayerController.dashTime <= 0)
        {
            particleLeft.Stop();
            particleRight.Stop();
        }

    }

    public bool IsOnBeat()
    {
        timeSample = wave.timeSamples - reactionTime;
        for (int i = 0; i < BeatAnalyse.beatStarts.Count; i++)
        {
            //Debug.Log(beat.beatStarts[i]);
            if (timeSample >= (BeatAnalyse.beatStarts[i] - windowTrigger) &&
                timeSample <= (BeatAnalyse.beatStarts[i] + windowTrigger))
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {

        if(BeatAnalyse.beatStarts == null)
        {
            return;
        }
        if (PlayerController.show)
        {
            Vector3 displacement = Camera.main.ScreenToWorldPoint(new Vector3(100, 100, 5));
            float heightMulti = 1;
            float widthMulti = 0.000005f;
            Gizmos.color = new Color(0.5f, 0, 0.5f, 1);

            Gizmos.color = Color.green;
            for (int i = 0; i < BeatAnalyse.beatStarts.Count; i++)
            {
                Gizmos.DrawLine(displacement + new Vector3((BeatAnalyse.beatStarts[i] - windowTrigger + reactionTime) * widthMulti, 0, 0),
                                displacement + new Vector3((BeatAnalyse.beatStarts[i] + windowTrigger + reactionTime) * widthMulti, 0, 0));
            }
            Gizmos.color = Color.red;
            Gizmos.DrawLine(displacement + new Vector3(wave.timeSamples * widthMulti, 0, 0), displacement + new Vector3(wave.timeSamples * widthMulti, heightMulti, 0));
        }
    }
    private void OnGUI()
    {
        if (PlayerController.show)
        {
            GUI.color = Color.magenta;
            GUI.Label(new Rect(10, 10, 200, 30), "Beat: " + IsOnBeat());
        }
        if (BeatAnalyse.beatStarts == null)
        {
            return;
        }
        if (PlayerController.show)
        {
            Vector3 displacement = Camera.main.ScreenToWorldPoint(new Vector3(100, 100, 5));
            float heightMulti = 1;
            float widthMulti = 0.000005f;
            Gizmos.color = new Color(0.5f, 0, 0.5f, 1);

            Gizmos.color = Color.green;
            for (int i = 0; i < BeatAnalyse.beatStarts.Count; i++)
            {
                Gizmos.DrawLine(displacement + new Vector3((BeatAnalyse.beatStarts[i] - windowTrigger + reactionTime) * widthMulti, 0, 0),
                                displacement + new Vector3((BeatAnalyse.beatStarts[i] + windowTrigger + reactionTime) * widthMulti, 0, 0));
            }
            Gizmos.color = Color.red;
            Gizmos.DrawLine(displacement + new Vector3(wave.timeSamples * widthMulti, 0, 0), displacement + new Vector3(wave.timeSamples * widthMulti, heightMulti, 0));
        }

    }
}
