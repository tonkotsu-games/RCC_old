using System.Collections;
using UnityEngine;

public class SplashScreenTrigger : MonoBehaviour
{

    [SerializeField] private Animator myAnimator;
    [SerializeField] GameObject splashScreen;
    public AnimationClip splashClip; 
    public bool movementLock = false;
    [SerializeField] BeatStrike playerBeatStrike;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("Protagonist_SM").GetComponent<PlayerController>().enabled = false;

            splashScreen.SetActive(true);

            myAnimator.Play("SplashScreenIntroBoss");
            
            StartCoroutine(SplashScreen()); 
        }
    }

    IEnumerator SplashScreen()
    {
        movementLock = true;
        playerBeatStrike.enabled = false;

        yield return new WaitForSeconds(splashClip.length);

        this.gameObject.SetActive(false);

        splashScreen.SetActive(false);
        
        GameObject.Find("Protagonist_SM").GetComponent<PlayerController>().enabled = true;

        playerBeatStrike.enabled = true;

        movementLock = false;
    }
}