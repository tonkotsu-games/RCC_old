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
    TextMeshProUGUI[] bonusScoresTexts;
    [SerializeField]
    GameObject[] splashImagesBonus;

    [SerializeField]
    float[] bonusScores;


    [SerializeField]
    TextMeshProUGUI inBetweenScoreText;

    float inBetweenScore;
    [SerializeField]
    float modifierMultiplicator;
    [SerializeField]
    TextMeshProUGUI modifierMultiplicatorText;

    [SerializeField]
    TextMeshProUGUI finalScoreDisplay;

    float finalScore;
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


    [SerializeField]
    Slider juiceSlider;
    [SerializeField]
    GameObject finalRankSplash;

    bool normalStatsDone = false;
    public bool scoreDone = false;

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

    /// <summary>
    /// The Enumerator for setting all the normal base stats through scrolling Text
    /// </summary>
    /// <param name="value"></param>
    /// <param name="score"></param>
    /// <param name="valueText"></param>
    /// <param name="scoreText"></param>
    /// <param name="splashImage"></param>
    /// <returns></returns>
    IEnumerator scrollText(float value, float score, TextMeshProUGUI valueText, TextMeshProUGUI scoreText,GameObject splashImage)
    {
        scoreText.gameObject.SetActive(true);
        valueText.gameObject.SetActive(true);
        float tempScore = 0;
        float tempValue = 0;
        while (tempScore != score && tempValue != value)
        {
            yield return new WaitForSeconds(scrollSpeed);
            tempScore += score * 0.05f;
            tempValue += value * 0.05f;

            tempScore = Mathf.RoundToInt(tempScore);
            // DISPLAY SCORE WHEN ITS AN INT
            if (Mathf.Approximately(tempScore, Mathf.RoundToInt(tempScore)))
            {
                scoreText.text = tempScore.ToString();
            }

            // DISPLAY VALUE WHEN ITS AN INT
            if (Mathf.Approximately(tempValue, Mathf.RoundToInt(tempValue)))
            {
                valueText.text = tempValue.ToString();
            }         
        }

        scoreText.gameObject.GetComponent<Animator>().SetTrigger("scoreSet");
        valueText.gameObject.GetComponent<Animator>().SetTrigger("scoreSet");
        splashImage.SetActive(true);

        scoreTracker++;

        if (scoreTracker != scoreTexts.Length && scoreTracker <= valueTexts.Length)
        {
            StartCoroutine(scrollText(valueCollector[scoreTracker], scoreCollector[scoreTracker], valueTexts[scoreTracker], scoreTexts[scoreTracker], splashImages[scoreTracker]));
        }
        else
        {
            NormalStatsDone = true;
        }
    }

    public IEnumerator bonusMultiplierScoreSetter()
    {
        for(int i = 0; i < bonusScores.Length; i++)
        {
            yield return new WaitForSeconds(5 * scrollSpeed);
            bonusScoresTexts[i].gameObject.SetActive(true);
            bonusScoresTexts[i].text = bonusScores[i].ToString();
            splashImagesBonus[i].SetActive(true);
        }

        for (int i = 0; i < scoreCollector.Length; i++)
        {
            inBetweenScore += scoreCollector[i];
        }

        for(int i = 0; i < bonusScores.Length; i++)
        {
            inBetweenScore += bonusScores[i];
        }

        inBetweenScore = Mathf.RoundToInt(inBetweenScore);
        float tempBetweenScore = 0;

        inBetweenScoreText.gameObject.SetActive(true);
        while(tempBetweenScore != inBetweenScore)
        {
            yield return new WaitForSeconds(scrollSpeed);
            tempBetweenScore += inBetweenScore * 0.05f;
            if (Mathf.Approximately(tempBetweenScore, Mathf.RoundToInt(tempBetweenScore))){
                inBetweenScoreText.text = tempBetweenScore.ToString();
            }

        }

        yield return new WaitForSeconds(5 * scrollSpeed);

        modifierMultiplicatorText.gameObject.SetActive(true);
        modifierMultiplicatorText.text ="x " +  modifierMultiplicator.ToString();

        finalScore = tempBetweenScore * modifierMultiplicator;
        Debug.Log("Final Score: " + finalScore);
        finalScore = Mathf.RoundToInt(finalScore);
        
        finalScoreDisplay.gameObject.SetActive(true);
        finalScoreDisplay.text = finalScore.ToString();

        yield return new WaitForSeconds(5 * scrollSpeed);

        juiceSlider.gameObject.SetActive(true);
        while(juiceSlider.value < 1)
        {
            yield return new WaitForSeconds(0.1f);
            juiceSlider.value += 0.1f;
        }

        yield return new WaitForSeconds(10 * scrollSpeed);
        finalRankSplash.SetActive(true);
        scoreDone = true;
    }


    public bool NormalStatsDone
    {
        get { return normalStatsDone; }
        set
        {
            if (normalStatsDone == false)
            {
                Debug.Log("Normal Stats are done");
                StartCoroutine(bonusMultiplierScoreSetter());
                normalStatsDone = value;
            }
            else
            {
                normalStatsDone = value;
            }
        }
    }
}
