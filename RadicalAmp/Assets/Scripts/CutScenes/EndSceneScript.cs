using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EndSceneScript : MonoBehaviour
{

    [SerializeField] private Animator myAnimator;
    [SerializeField] AnimationClip EndSceneAnimation;

    // Start is called before the first frame update
    void Start ()
    {
        StartCoroutine(EndScene()); 
    }

   IEnumerator EndScene()
    {
        yield return new WaitForSeconds(EndSceneAnimation.length);
        SceneManager.LoadScene("Start_Menu", LoadSceneMode.Single);
    }  
}
