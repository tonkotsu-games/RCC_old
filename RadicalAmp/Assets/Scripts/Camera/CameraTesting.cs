using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTesting : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    Vector3 newPosition = new Vector3(0,0,0);
    
    void Start()
    {
        if(offset == new Vector3(0,0,0))
        {
            Debug.LogError("offset not setup on Camera!");
        }

        this.gameObject.transform.position = target.position + offset;
        transform.LookAt(target);
    }


    void Update()
    {
        CalculateSmoothZFollow();
        ChangePosition();
    }

    private void CalculateSmoothZFollow()
    {
        newPosition.z = ((target.transform.position.z + offset.z) - this.gameObject.transform.position.z) * 0.1f* Time.deltaTime;
    }

    private void ChangePosition()
    {
        this.gameObject.transform.position += newPosition;
    }
}
