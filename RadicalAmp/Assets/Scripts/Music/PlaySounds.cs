using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    [SerializeField] AudioClip splashSounds;
    public AudioSource splashAudioSource; 

    // Start is called before the first frame update
    public void PlayS()
    {
        splashAudioSource.clip = splashSounds; 
        splashAudioSource.Play(); 
    }
      
}
