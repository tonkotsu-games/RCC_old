using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeathovenAnimationEvents : MonoBehaviour
{
    Actor actor;

    void Start()
    {
        actor = GetComponentInParent<Actor>();
    }

    public void EndWindUp()
    {
        actor.GetComponent<Feedback>().PlayAnimationForState("Attack");
        actor.windupFinished = true;
        //Debug.Log("EndWindup");
    }
}
