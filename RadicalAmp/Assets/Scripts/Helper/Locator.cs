using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Locator : MonoBehaviour
{
    private BeatAnalyse musicBox;
    private Slider juiceMeter;
    public static Locator instance;

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

        //#region CollectingReturnables
        musicBox = GameObject.FindWithTag("MusicBox").GetComponent<BeatAnalyse>();
        juiceMeter = GameObject.FindWithTag("JuiceMeter").GetComponent<Slider>();
        Debug.Log("This " + instance.gameObject.name + " musicbox" + instance.musicBox);
        //#endregion
    }

    public BeatAnalyse GetBeat()
    {
        /* if(musicBox == null)
        {
            musicBox = GameObject.FindWithTag("MusicBox").GetComponent<BeatAnalyse>();
        }*/

        return musicBox;
    }
    public Slider GetJuiceMeter()
    {
        return juiceMeter;
    }
}
