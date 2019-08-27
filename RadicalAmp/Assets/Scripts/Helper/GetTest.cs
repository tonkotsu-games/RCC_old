using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTest : MonoBehaviour
{
    [SerializeField]
    private BeatAnalyse musicBox;
    // Start is called before the first frame update
    void Start()
    {
        musicBox = Locator.instance.GetBeat();
        Debug.Log("musicbox " + musicBox);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
