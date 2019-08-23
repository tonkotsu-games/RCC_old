using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    private CharacterScriptableObject actorData;
    protected Animator animator;

    [SerializeField] 
    private AnimationClip awakeAnimation;
    
    public AnimationClip attackAnimation;


    private void Awake()
    {
        actorData = this.gameObject.GetComponent<Actor>().ActorData;

        if(actorData != null)
        {
            //Debug.Log("Data on Feedback found on " + this.gameObject.name); 
        }
        else
        {
            Debug.LogError("Data on Feedback NOT found on " + this.gameObject.name);
        }

        animator = this.gameObject.GetComponentInChildren<Animator>();

        if(attackAnimation == null)
        {
            Debug.LogError("Attack Animation not setup in " + this.gameObject.name);
        }
    }

    public void PlayAwake()
    {

    }

    public virtual void PlayAnimationForState(string state)
    {
        
    }

}