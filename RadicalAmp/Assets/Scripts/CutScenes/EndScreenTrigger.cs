using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EndScreenTrigger : MonoBehaviour
{
    [SerializeField] private Animator myAnimator;
    [SerializeField] BoxCollider EndTriggerZone;
    [SerializeField] AnimationClip EndSceneAnimation;
    [SerializeField] GameObject EndSceneGameObject;

    public void TriggerEndEvent()
    {
        EndSceneGameObject.SetActive(true); 
        StartCoroutine(EndSceneTrigger()); 
    }

   IEnumerator EndSceneTrigger()
    {
        yield return new WaitForSeconds(EndSceneAnimation.length+1);
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
