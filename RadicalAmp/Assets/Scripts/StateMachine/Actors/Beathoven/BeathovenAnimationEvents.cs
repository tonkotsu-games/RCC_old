using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeathovenAnimationEvents : MonoBehaviour
{
    Actor actor;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject shieldParent;
    bool parented = true;
    Vector3 startRotation;
    Vector3 startPosition;

    void Start()
    {
        actor = GetComponentInParent<Actor>();
        if(shield != null)
        {
            startRotation = shield.transform.localEulerAngles;
            startPosition = shield.transform.localPosition;
            Debug.Log("Start rotation " + startRotation);
        }
    }

    public void EndWindUp()
    {
        actor.GetComponent<Feedback>().PlayAnimationForState("Attack");
        actor.windupFinished = true;
        //Debug.Log("EndWindup");
    }

    public void EndSpecialWindUp()
    {
        actor.GetComponent<Feedback>().PlayAnimationForState("specialAttack");
        actor.windupFinished = true;
        //Debug.Log("EndWindup");
    }

    public void WaveWindUp()
    {
        actor.GetComponent<Feedback>().PlayAnimationForState("waveAttack");
        actor.windupFinished = true;
        Debug.Log("WaveWindup Event");
    }

    public void AfterDeath()
    {
        Destroy(this.gameObject.transform.parent.gameObject);      
    }

    public void ToggleShieldParent()
    {
        if(parented)
        {
            shield.transform.parent = null;
        }
        else
        {
            shield.transform.parent = shieldParent.transform;
            shield.transform.localRotation = Quaternion.identity;
            shield.transform.localRotation = Quaternion.Euler(startRotation);
            shield.transform.localPosition = new Vector3(0f,0f,0f);
            shield.transform.localPosition = startPosition;
        }

        parented = !parented;
    }
}
