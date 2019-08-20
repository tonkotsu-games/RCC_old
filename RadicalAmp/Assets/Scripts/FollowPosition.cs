using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    public Transform followTarget;
    [SerializeField] Vector3 offset;

    void Update()
    {   
        if(followTarget != null)
        {
            gameObject.transform.position = followTarget.position + offset;
        }
    } 
}
