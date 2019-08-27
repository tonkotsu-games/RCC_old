using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locator : MonoBehaviour
{
    public static Locator instance;


    private BeatAnalyse musicBox;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        #region CollectingReturnables
        musicBox = GameObject.FindWithTag("MusicBox").GetComponent<BeatAnalyse>();
        #endregion
    }

}
