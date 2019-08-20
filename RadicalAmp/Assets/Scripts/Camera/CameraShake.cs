using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    
    Animator cameraAnim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }

 //  public void StartShake()
 //  {
 //      cameraAnim = gameObject.GetComponent<Animator>();
 //      StartCoroutine(ShakeIt());
 //  }
 //  
 //
 //  public IEnumerator ShakeIt()
 //  {
 //      while (true)
 //      {
 //          cameraAnim.SetTrigger("shake");
 //          Debug.Log("SHAKE");
 //          yield return new WaitForSeconds(0.5f);
 //
 //      }
 //  }


}
