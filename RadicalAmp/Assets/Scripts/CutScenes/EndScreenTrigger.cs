using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class EndScreenTrigger : MonoBehaviour
{

    [SerializeField] private Animator myAnimator;
    [SerializeField] BoxCollider EndTriggerZone;
    [SerializeField] AnimationClip EndSceneAnimation;
    [SerializeField] GameObject EndSceneGameObject; 

    // Start is called before the first frame update
    public void TriggerEndEvent()
    {
        EndSceneGameObject.SetActive(true); 
        StartCoroutine(EndSceneTrigger()); 
    }

   IEnumerator EndSceneTrigger()
    {
        yield return new WaitForSeconds(EndSceneAnimation.length);
        EndTriggerZone.enabled = true; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("EndScreen", LoadSceneMode.Single); 
        }
    }
}
