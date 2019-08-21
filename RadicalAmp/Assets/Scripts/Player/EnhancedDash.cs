using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedDash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy in dash");
        }
    }
}
