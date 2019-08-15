using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenTrigger : MonoBehaviour
{

    [SerializeField] private Animator myAnimator;
    [SerializeField] GameObject splashScreen;
    public AnimationClip splashClip; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player entered");

            GameObject.Find("Protagonist_Final").GetComponent<PlayerController>().enabled = false;
            //Debug.Log("Player locked"); 

            splashScreen.SetActive(true);
            //Debug.Log("Splash screen is active");

            myAnimator.Play("SplashScreenIntroBoss");
            //Debug.Log("Play Splash Animation");
            
            StartCoroutine(SplashScreen()); 


           
        }
    }

    IEnumerator SplashScreen()
    {
        EnemyController.movementLocked = true;

        yield return new WaitForSeconds(splashClip.length);

        this.gameObject.SetActive(false);
        //Debug.Log("I am done now");

        splashScreen.SetActive(false);
        //Debug.Log("Splash Screen Object Deactivated"); 
        
        GameObject.Find("Protagonist_Final").GetComponent<PlayerController>().enabled = true;
        //Debug.Log("Player unlocked"); 
        EnemyController.movementLocked = false;
    }

}
