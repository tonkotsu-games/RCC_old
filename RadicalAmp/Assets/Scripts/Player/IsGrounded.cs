using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    public static bool isGrounded = false;

    private void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag != "Player" && other.gameObject.tag != "Fall")
        {
            isGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}
