using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupSpawner : MonoBehaviour
{

    [SerializeField] GameObject ScoreTracker;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ScoreTracker);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
