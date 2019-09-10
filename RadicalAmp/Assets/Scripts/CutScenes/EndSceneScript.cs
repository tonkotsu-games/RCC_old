using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EndSceneScript : MonoBehaviour
{

    [SerializeField] private Animator myAnimator;
    [SerializeField] AnimationClip EndSceneAnimation;
    [SerializeField] ScoreScreenManager scoreScreen;

    // Start is called before the first frame update
    void Start ()
    {
        StartCoroutine(EndScene()); 
    }

   IEnumerator EndScene()
    {
        yield return new WaitForSeconds(EndSceneAnimation.length);
        scoreScreen.gameObject.SetActive(true);
        //SceneManager.LoadScene("Start_Menu", LoadSceneMode.Single);
    }

    private void Update()
    {
        if (Input.GetButton("Start") && scoreScreen.scoreDone)
        {
            SceneManager.LoadScene("Start_Menu", LoadSceneMode.Single);
        }
    }
}
