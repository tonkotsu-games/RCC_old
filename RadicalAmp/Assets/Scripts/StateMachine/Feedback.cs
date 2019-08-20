using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    private CharacterScriptableObject actorData;

    private void Start()
    {
        actorData = this.gameObject.GetComponent<Actor>().ActorData;

        if(actorData != null)
        {
            Debug.Log("Data on Feedback found on " + this.gameObject.name); 
        }
        else
        {
            Debug.LogError("Data on Feedback NOT found on " + this.gameObject.name);
        }
    }
}