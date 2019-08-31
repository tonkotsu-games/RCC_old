using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightTargetFollow : MonoBehaviour
{
    [Header("Player as Target")]
    [SerializeField] Transform target;

    void Update()
    {
        transform.position = target.position;
    }
}
