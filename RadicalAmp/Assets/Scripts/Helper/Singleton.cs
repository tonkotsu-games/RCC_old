using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static GameObject instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this.gameObject;
        }

        else
        {
            Destroy(gameObject);
        }
    }

}
