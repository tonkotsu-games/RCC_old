using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControll : MonoBehaviour
{
    [SerializeField] AudioSource[] audioSource;
    Slider juiceMeter;
    GameObject musicBox;

    int child;

    // Start is called before the first frame update
    void Start()
    {
        juiceMeter = GameObject.FindWithTag("JuiceMeter").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(juiceMeter.value >= 9)
        {
            audioSource[1].volume = 1;
        }
        if (juiceMeter.value <= 8)
        {
            audioSource[1].volume = 0;
        }
        if(juiceMeter.value >= 18)
        {
            audioSource[2].volume = 1;
        }
        if(juiceMeter.value <= 17)
        {
            audioSource[2].volume = 0;
        }
        if(juiceMeter.value >= 27)
        {
            audioSource[3].volume = 1;
        }
        if(juiceMeter.value <= 26)
        {
            audioSource[3].volume = 0;
        }
        if(juiceMeter.value >= 36)
        {
            audioSource[4].volume = 1;
        }
        if(juiceMeter.value <= 35)
        {
            audioSource[4].volume = 0;
        }
        if (juiceMeter.value >= 45)
        {
            audioSource[5].volume = 1;
        }
        if (juiceMeter.value <= 44)
        {
            audioSource[5].volume = 0;
        }
        if (juiceMeter.value >= 54)
        {
            audioSource[6].volume = 1;
        }
        if (juiceMeter.value <= 53)
        {
            audioSource[6].volume = 0;
        }
        if (juiceMeter.value >= 63)
        {
            audioSource[7].volume = 1;
        }
        if (juiceMeter.value <= 62)
        {
            audioSource[7].volume = 0;
        }
        if (juiceMeter.value >= 72)
        {
            audioSource[8].volume = 1;
        }
        if (juiceMeter.value <= 71)
        {
            audioSource[8].volume = 0;
        }
        if (juiceMeter.value >= 81)
        {
            audioSource[9].volume = 1;
        }
        if (juiceMeter.value <= 80)
        {
            audioSource[9].volume = 0;
        }
        if (juiceMeter.value >= 90)
        {
            audioSource[10].volume = 1;
        }
        if (juiceMeter.value <= 89)
        {
            audioSource[10].volume = 0;
        }
    }
}
