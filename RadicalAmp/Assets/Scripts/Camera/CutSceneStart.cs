using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CutSceneStart : MonoBehaviour
{

    
    [SerializeField] GameObject tutorialText;
    public AnimationClip cameraDrive;
   
    // Start is called before the first frame update
    void Start()
    {
        
        GameObject.Find("Protagonist_Final").GetComponent<PlayerController>().enabled = false;
        GameObject.Find("UI_Manager").GetComponent<Tutorial>().enabled = false;
        tutorialText.SetActive(false); 
        StartCoroutine(StartAnim());
        
       
    }

    IEnumerator StartAnim()
    {
        //print(Time.time);
        yield return new WaitForSeconds(cameraDrive.length);
       
        GameObject.Find("Protagonist_Final").GetComponent<PlayerController>().enabled = true;
        GameObject.Find("UI_Manager").GetComponent<Tutorial>().enabled = true;
        tutorialText.SetActive(true);
        this.gameObject.SetActive(false); 
    }
}
