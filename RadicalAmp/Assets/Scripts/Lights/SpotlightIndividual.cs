using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightIndividual : MonoBehaviour
{
    [SerializeField] Transform target;
    public bool isActive;

    void Update()
    {
        transform.LookAt(target);
    }

    void TurnMeOff()
    {
        isActive = false;
        gameObject.SetActive(false);
    }
}
