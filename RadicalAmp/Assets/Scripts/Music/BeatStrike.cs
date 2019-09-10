using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.VFX;

public class BeatStrike : MonoBehaviour
{
    public static BeatStrike instance;
    private Slider juiceMeter;
    private AudioSource wave;
    private PlayerController player;

    [Header("Reaction time for Beat hitting")]
    [SerializeField] int reactionTime;
    [Header("Time in Samples (10.000 = 0,25sec)")]
    [SerializeField] int windowTrigger;
    float timeSample;

    bool action = false;
    bool punish = false;
    public static bool pulseBeat = false;
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

    [Header("On Beat Vertex Displacemet")]
    [SerializeField] Material bodyMat;
    [SerializeField] Material capeMat;
    [SerializeField] float lerpSpeed = 1f;
    private float displFloat = 0f;

    Animator anim;


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
        anim = gameObject.GetComponentInChildren<Animator>();
        wave = GameObject.FindWithTag("Beat").GetComponent<AudioSource>();
        juiceMeter = Locator.instance.GetJuiceMeter();
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        //lerp the Displacement switch back to 0
        if(bodyMat.GetFloat("_displSwitch") > 0f)
        {
            bodyMat.SetFloat("_displSwitch", Mathf.Lerp(bodyMat.GetFloat("_displSwitch"), 0f, lerpSpeed));
            capeMat.SetFloat("_displSwitch", Mathf.Lerp(capeMat.GetFloat("_displSwitch"), 0f, lerpSpeed));

        }


        pulseBeat = false;

        if (Input.GetButtonDown("Dash") && !player.triggerLeft)
        {
            //Reenable collision through Animation Event after Dash
            if (IsOnBeat())
            {
                if(ScoreTracker.instance != null)
                {
                    ScoreTracker.instance.beatsHitTotal++;
                }
                bodyMat.SetFloat("_displSwitch", 1f);
                capeMat.SetFloat("_displSwitch", 1f);

                pulseBeat = true;
                if (EnhancedSkills.instance.currentEnhancedState == EnhancedSkills.EnhancedState.Active)
                {
                    EnhancedSkills.instance.UseEnhancedSkill(EnhancedSkills.ActionsToEnhance.Dash);
                }

                else
                {
                    juiceMeter.value += dashReward;
                    action = true;
                }
            }

            else
            {
                juiceMeter.value -= dashPunish;
                if (EnhancedSkills.instance.currentEnhancedState != EnhancedSkills.EnhancedState.Inactive)
                {
                    EnhancedSkills.instance.ChangeEnhancedState(EnhancedSkills.EnhancedState.Inactive);
                }
            }
        }
        else if (Input.GetButtonDown("Attack"))
        {
            if(IsOnBeat())
            {
                if (ScoreTracker.instance != null)
                {
                    ScoreTracker.instance.beatsHitTotal++;
                }

                bodyMat.SetFloat("_displSwitch", 1f);
                capeMat.SetFloat("_displSwitch", 1f);

                pulseBeat = true;
                if (EnhancedSkills.instance.currentEnhancedState == EnhancedSkills.EnhancedState.Active)
                {
                    EnhancedSkills.instance.UseEnhancedSkill(EnhancedSkills.ActionsToEnhance.Attack);
                }

                else
                {
                    juiceMeter.value += attackReward;
                    beatAttack = true;
                    action = true;
                }
            }
            else
            {
                juiceMeter.value -= attackPunish;
                if (EnhancedSkills.instance.currentEnhancedState != EnhancedSkills.EnhancedState.Inactive)
                {
                    EnhancedSkills.instance.ChangeEnhancedState(EnhancedSkills.EnhancedState.Inactive);
                }
            }
        }
        else if(Input.GetButtonDown("Dance"))
        {
            if (IsOnBeat())
            {
                if (ScoreTracker.instance != null)
                {
                    ScoreTracker.instance.beatsHitTotal++;
                }

                bodyMat.SetFloat("_displSwitch", 1f);
                capeMat.SetFloat("_displSwitch", 1f);

                pulseBeat = true;
                juiceMeter.value += danceReward;
                action = true;

                switch (EnhancedSkills.instance.currentEnhancedState) {
                    case EnhancedSkills.EnhancedState.Inactive:
                            EnhancedSkills.instance.ChangeEnhancedState(EnhancedSkills.EnhancedState.First);
                        break;
                    case EnhancedSkills.EnhancedState.First:
                        EnhancedSkills.instance.ChangeEnhancedState(EnhancedSkills.EnhancedState.Second);
                        break;
                    case EnhancedSkills.EnhancedState.Second:
                        EnhancedSkills.instance.ChangeEnhancedState(EnhancedSkills.EnhancedState.Active);
                        break;


                }
            }
            else
            {
                juiceMeter.value -= dancePunish;
                if (EnhancedSkills.instance.currentEnhancedState != EnhancedSkills.EnhancedState.Inactive)
                {
                    EnhancedSkills.instance.ChangeEnhancedState(EnhancedSkills.EnhancedState.Inactive);
                }
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
    }

    public bool IsOnBeat()
    {
        timeSample = wave.timeSamples - reactionTime;
        for (int i = 0; i < BeatAnalyse.beatStarts.Count; i++)
        {
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