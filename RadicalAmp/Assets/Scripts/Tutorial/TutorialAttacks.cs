using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAttacks : MonoBehaviour
{
    [SerializeField] Tutorial tutorial;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Projectile" && tutorial.currentStep == Tutorial.TutorialSteps.EmpowerSlashTest)
        {
            Death();
        }
    }

    public void Death()
    {
        if(tutorial.EmpowerClone.Contains(this.gameObject))
        {
            tutorial.EmpowerClone.Remove(this.gameObject);
        }
        else if(tutorial.JuiceDashClone.Contains(this.gameObject))
        {
            tutorial.JuiceDashClone.Remove(this.gameObject);
        }
        Destroy(this.gameObject);
    }
}