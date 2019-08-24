using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTesting : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 offsetCurrent;
    [SerializeField] Vector3 offsetZero;
    [SerializeField] Vector3 offsetOne;
    [SerializeField] Vector3 offsetTwo;
    Vector3 newPosition = new Vector3(0,0,0);
    [SerializeField] private int cameraState = 0;
    
    void Start()
    {
        this.gameObject.transform.position = target.position + offsetZero;
        offsetCurrent = offsetZero;
        transform.LookAt(target);
    }


    void Update()
    {
        CalculateOffset();
        CalculateSmoothZFollow();
        CalculateSmoothXFollow();
        ChangePosition();
    }

    private void CalculateSmoothZFollow()
    {
        newPosition.z = ((target.transform.position.z + offsetCurrent.z) - this.gameObject.transform.position.z) * 0.1f* Time.deltaTime * 25f;
    }

    private void CalculateSmoothXFollow()
    {
        newPosition.x = ((target.transform.position.x + offsetCurrent.x) - this.gameObject.transform.position.x) * 0.1f* Time.deltaTime * 25f;
    }

    private void ChangePosition()
    {
        this.gameObject.transform.position += newPosition;
    }

    private void CalculateOffset()
    {
        if(cameraState == 0)
        {
            offsetCurrent.z += (offsetZero.z - offsetCurrent.z) * 0.1f* Time.deltaTime * 25f;
        }
        else if(cameraState == 1)
        {
            offsetCurrent.z += (offsetOne.z - offsetCurrent.z) * 0.1f* Time.deltaTime * 25f;
        }
    }
}
