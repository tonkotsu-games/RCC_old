using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuiceDash : MonoBehaviour
{

    Slider juiceMeter;
    BeatAnalyse beatAnalyse;
    public float juiceConsumedPerCharge = 30;
    float range;
    bool beat = false;

    public enum ChargeStates { none, first, second,final,success,punish}
    ChargeStates currentState = ChargeStates.none;
    ChargeStates nextState = ChargeStates.first;


    // Start is called before the first frame update
    void Start()
    {
        juiceMeter = Locator.instance.GetJuiceMeter();
        beatAnalyse = Locator.instance.GetBeat();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != ChargeStates.final)
        {
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
            else if (Input.GetButtonUp("Attack"))
            {
                ChangeChargeState(ChargeStates.none);
            }
        }
        else
        {
            if (Input.GetButtonUp("Attack") && beatAnalyse.IsOnBeat(1000) && beat == false)
            {
                ChangeChargeState(nextState);
            }
            else if (Input.GetButtonUp("Attack"))
            {
                ChangeChargeState(ChargeStates.punish);
            }
            else if (Input.GetButton("Attack") && beatAnalyse.IsOnBeat(1000) && beat == false)
            {
                ChangeChargeState(ChargeStates.punish);
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
        else {
            currentState = requestedState;

            if (requestedState == ChargeStates.none)
            {
                Debug.Log("Charge failed");
                nextState = ChargeStates.first;
            }
            else if (requestedState == ChargeStates.first)
            {
                if (juiceMeter.value == 100)
                {
                    nextState = ChargeStates.second;
                    juiceMeter.value -= juiceConsumedPerCharge;
                }
                else
                {
                    ChangeChargeState(ChargeStates.none);
                }
                
            }
            else if(requestedState == ChargeStates.second)
            {
                nextState = ChargeStates.final;
                juiceMeter.value -= juiceConsumedPerCharge;
            }
            else if(requestedState == ChargeStates.final)
            {
                nextState = ChargeStates.success;
                juiceMeter.value -= juiceConsumedPerCharge;
            }
            else if(requestedState == ChargeStates.success)
            {

            }
        }
    }

    void UseJuiceDash()
    {

    }
   
}
