using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScreenManager : MonoBehaviour
{

    public float scrollSpeed;

    [SerializeField]
    TextMeshProUGUI[] scoreTexts;

    [SerializeField]
    TextMeshProUGUI[] valueTexts;

    [SerializeField]
    GameObject[] splashImages;

    public float[] valueCollector = new float[6];
    float[] scoreCollector;

    int scoreTracker = 0;




    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(scrollText(ScoreTracker.instance.statContainer[scoreTracker],1000,valueTexts[scoreTracker], scoreTexts[scoreTracker],splashImages[scoreTracker])); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator scrollText(float value, float score, TextMeshProUGUI valueText, TextMeshProUGUI scoreText,GameObject splashImage)
    {
        scoreText.gameObject.SetActive(true);
        if (scoreTracker < valueTexts.Length)
        {
            valueText.gameObject.SetActive(true);
        }
        float tempScore = 0;
        float tempValue = 0;
        while (tempScore != score && tempValue != value)
        {
            yield return new WaitForSeconds(scrollSpeed);
            tempScore += score * 0.05f;
            tempValue += value * 0.05f;
            scoreText.text = tempScore.ToString();
            if (scoreTracker < valueTexts.Length)
            {
                if (Mathf.Approximately(tempValue, Mathf.RoundToInt(tempValue)))
                {
                    valueText.text = tempValue.ToString();
                }
            }
            
        }
        scoreText.gameObject.GetComponent<Animator>().SetTrigger("scoreSet");
        if (scoreTracker < valueTexts.Length)
        {
            valueText.gameObject.GetComponent<Animator>().SetTrigger("scoreSet");
        }
        splashImage.SetActive(true);
        splashImage.GetComponent<Animator>().SetTrigger("splashSet");

        scoreTracker++;
        if (scoreTracker != scoreTexts.Length && scoreTracker < valueTexts.Length)
        {
            StartCoroutine(scrollText(ScoreTracker.instance.statContainer[scoreTracker], 5000,valueTexts[scoreTracker], scoreTexts[scoreTracker], splashImages[scoreTracker]));
        }
        else
        {
            StartCoroutine(scrollText(ScoreTracker.instance.statContainer[scoreTracker], 5000, valueTexts[0], scoreTexts[scoreTracker], splashImages[scoreTracker]));
        }
    }
}
