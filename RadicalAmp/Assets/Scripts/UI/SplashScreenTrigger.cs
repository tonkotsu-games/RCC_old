using System.Collections;
using UnityEngine;

public class SplashScreenTrigger : MonoBehaviour
{

    [SerializeField] private Animator myAnimator;
    [SerializeField] GameObject splashScreen;
    public AnimationClip splashClip; 

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
        EnemyController.movementLocked = true;

        yield return new WaitForSeconds(splashClip.length);

        this.gameObject.SetActive(false);

        splashScreen.SetActive(false);
        
        GameObject.Find("Protagonist_SM").GetComponent<PlayerController>().enabled = true;
        EnemyController.movementLocked = false;
    }
}