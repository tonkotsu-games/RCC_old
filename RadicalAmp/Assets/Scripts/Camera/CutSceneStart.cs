using System.Collections;
using UnityEngine;

public class CutSceneStart : MonoBehaviour
{
    
    [SerializeField] GameObject tutorialText;
    public AnimationClip cameraDrive;
   
    void Start()
    {        
        GameObject.Find("Protagonist_Final").GetComponent<PlayerController>().enabled = false;
        GameObject.Find("UI_Manager").GetComponent<Tutorial>().enabled = false;
        tutorialText.SetActive(false); 
        StartCoroutine(StartAnim());       
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(cameraDrive.length);
       
        GameObject.Find("Protagonist_Final").GetComponent<PlayerController>().enabled = true;
        GameObject.Find("UI_Manager").GetComponent<Tutorial>().enabled = true;
        tutorialText.SetActive(true);
        this.gameObject.SetActive(false); 
    }
}
