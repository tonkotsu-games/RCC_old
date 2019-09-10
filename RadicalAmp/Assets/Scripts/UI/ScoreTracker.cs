using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{

    public static ScoreTracker instance;

    public List<int> statContainer;
    public List<int> bonusScores;
    public int deaths = 0;
    public int hitsTaken = 0;
    public int enemiesKilled = 0;
    public int beatsHitTotal = 0;
    public int totalBeats = 0;
    public int beatsHitPercent = 0;
    public int specialsUsed = 0;
    public int timePassedInSeconds = 0;
    int estimatedLevelCompletionTime = 1800;
    public int secondsDifference = 0;
    bool calculatingDone = false;
    public bool doCalculations = false;


    int completionBonus = 0;
    int noHitsTakenBonus = 0;
    int everyBeatHitBonus = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (doCalculations)
        {
            CalclulateStats();
            doCalculations = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        statContainer = new List<int>(6);
    }

    public void ExportStats()
    {
        statContainer.Add(enemiesKilled);
        statContainer.Add(beatsHitPercent);
        statContainer.Add(specialsUsed);
        statContainer.Add(secondsDifference);
        statContainer.Add(hitsTaken);
        statContainer.Add(deaths);

        bonusScores.Add(completionBonus);
        bonusScores.Add(noHitsTakenBonus);
        bonusScores.Add(everyBeatHitBonus);
    }
    

    public void CalclulateStats()
    {
        secondsDifference = estimatedLevelCompletionTime - timePassedInSeconds;
        beatsHitPercent = Mathf.RoundToInt((beatsHitTotal*100)/totalBeats);
        if(hitsTaken == 0)
        {
            noHitsTakenBonus = 8000;
        }
        if(GameObject.FindWithTag("Enemy") == null)
        {
            completionBonus = 5000;
        }
        if(beatsHitPercent == 100)
        {
            everyBeatHitBonus = 15000;
        }
        ExportStats();
    }
}
