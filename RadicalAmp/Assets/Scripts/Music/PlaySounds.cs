using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    [SerializeField] AudioClip splashSounds;
    public AudioSource splashAudioSource; 

    public void PlayS()
    {
        splashAudioSource.clip = splashSounds; 
        splashAudioSource.Play(); 
    }
}
