using System.Collections;
using UnityEngine;

public class CutSceneStart : MonoBehaviour
{
    [SerializeField] GameState gameState;
    [SerializeField] Tutorial tutorialStep;
    [SerializeField] GameObject tutorialText;
    public AnimationClip cameraDrive;
   
    void Start()
    {
        if (!gameState.testing)
        {
            GameObject.Find("Protagonist_SM").GetComponent<PlayerController>().enabled = false;
            GameObject.Find("UI_Manager").GetComponent<Tutorial>().enabled = false;
            tutorialText.SetActive(false);
            StartCoroutine(StartAnim());
        }
        else
        {
            Camera introCam = GetComponent<Camera>();
            tutorialText.SetActive(false);
            tutorialStep.Testing();
            introCam.enabled = false;
        }
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(cameraDrive.length);
       
        GameObject.Find("Protagonist_SM").GetComponent<PlayerController>().enabled = true;
        GameObject.Find("UI_Manager").GetComponent<Tutorial>().enabled = true;
        tutorialText.SetActive(true);
        this.gameObject.SetActive(false); 
    }
}
