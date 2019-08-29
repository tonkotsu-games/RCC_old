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

    [SerializeField]
    float inBetweenScore;

    public float[] valueCollector; // = new float[6];
    public float[] scoreCollector; //= new float[6];

    int scoreTracker = 0;

    [SerializeField]
    [Tooltip("Time estimated to finish the level in seconds")]
    float timeBase;

    [Header("ValueToScore Converters")]
    [SerializeField]
    float scorePerEnemyKilled;
    [SerializeField]
    float scorePerBeatPercentage;
    [SerializeField]
    float scorePerSpecialUsed;
    [SerializeField]
    float scorePerSecondDifference;
    [SerializeField]
    float scorePerHitTaken;
    [SerializeField]
    float scorePerDeath;



    // Start is called before the first frame update
    void Start()
    {     
       // ScoreTracker.instance.statContainer[3] = Time.timeSinceLevelLoad / 60;
      //  Debug.Log("Time since level load: " + Time.timeSinceLevelLoad);
      //  Debug.Log("Time in tracker: " + ScoreTracker.instance.statContainer[3].ToString());
        #region Calculating Values to score

      // //Enemies killed
      // scoreCollector[0] = ScoreTracker.instance.statContainer[0] * scorePerEnemyKilled;
      // //Beats hit
      // scoreCollector[1] = ScoreTracker.instance.statContainer[1] * scorePerBeatPercentage;
      // //Specials
      // scoreCollector[2] = ScoreTracker.instance.statContainer[2] * scorePerSpecialUsed;
      // //Time
      // scoreCollector[3] = Mathf.RoundToInt(ScoreTracker.instance.statContainer[3]) * scorePerSecondDifference;
      // //Hits taken
      // scoreCollector[4] = ScoreTracker.instance.statContainer[4] * scorePerHitTaken;
      // //Deaths
      // scoreCollector[5] = ScoreTracker.instance.statContainer[5] * scorePerDeath;


        #endregion

        StartCoroutine(scrollText(valueCollector[scoreTracker], scoreCollector[scoreTracker], valueTexts[scoreTracker], scoreTexts[scoreTracker], splashImages[scoreTracker]));

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

        if(scoreTracker == )
        scoreText.gameObject.GetComponent<Animator>().SetTrigger("scoreSet");
        if (scoreTracker < valueTexts.Length)
        {
            valueText.gameObject.GetComponent<Animator>().SetTrigger("scoreSet");
        }
        splashImage.SetActive(true);
        splashImage.GetComponent<Animator>().SetTrigger("splashSet");

        scoreTracker++;
        Debug.Log("ScoreTracker: " + scoreTracker);
        if (scoreTracker != scoreTexts.Length && scoreTracker < valueTexts.Length)
        {
            Debug.Log("Values,Scores : " + ScoreTracker.instance.statContainer[scoreTracker] + scoreCollector[scoreTracker]);
            StartCoroutine(scrollText(valueCollector[scoreTracker],scoreCollector[scoreTracker],valueTexts[scoreTracker], scoreTexts[scoreTracker], splashImages[scoreTracker]));
        }
        else
        {
            Debug.Log("Values,Scores : " + ScoreTracker.instance.statContainer[scoreTracker] + scoreCollector[scoreTracker]);
            StartCoroutine(scrollText(valueCollector[scoreTracker], scoreCollector[scoreTracker], valueTexts[0], scoreTexts[scoreTracker], splashImages[scoreTracker]));
        }
    }
}
