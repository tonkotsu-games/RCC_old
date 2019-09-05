using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Locator : MonoBehaviour
{
    public static Locator instance;
    private BeatAnalyse musicBox;
    private Slider juiceMeter;
    private GameObject nikCam;
    private Transform playerPosition;

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
        nikCam = GameObject.FindWithTag("MainCamera");
        //Debug.Log("This " + instance.gameObject.name + " musicbox" + instance.musicBox);
        playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
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

    public Transform GetPlayerPosition()
    {
        return playerPosition;
    }
    public GameObject GetNikCam()
    {
        return nikCam;
    }
}
