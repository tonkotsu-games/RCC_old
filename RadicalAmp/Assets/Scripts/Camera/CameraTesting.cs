using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTesting : MonoBehaviour
{
    [Header("Camera State")]
    [Tooltip("Camera state integer, dependent on the number of enemies near the player.")]
    [SerializeField] private int cameraState = 0;

    [Header("Camera Target")]
    [SerializeField] Transform target;
    Vector3 offsetCurrent;
    [Header("Offsets")]
    [SerializeField] Vector3 offsetZero;
    [SerializeField] Vector3 offsetOne;
    [SerializeField] Vector3 offsetTwo;
    [SerializeField] Vector3 offsetThree;
    [SerializeField] Vector3 offsetFour;
    Vector3 newPosition = new Vector3(0,0,0);
    [Header("Follow Speed")]
    [SerializeField] float followSpeedX = 25f;
    [SerializeField] float followSpeedY = 25f;
    [SerializeField] float followSpeedZ = 2.5f;
    [Header("Zoom Speed")]
    [SerializeField] float zoomSpeedX = 25f;
    [SerializeField] float zoomSpeedY = 25f;
    [SerializeField] float zoomSpeedZ = 25f;
    [Header("Camera X Rotation Speed")]
    [Tooltip("Maximum turn rate in degrees per second..")]
    [SerializeField] float turningRate = 25f;

    // Rotation we should blend towards.
    private Quaternion targetRotation = Quaternion.identity;
     
    void Start()
    {
        this.gameObject.transform.position = target.position + offsetZero;
        offsetCurrent = offsetZero;
    }


    void Update()
    {
        EnemyCheck();
        CalculateOffset();
        CalculateSmoothFollow();
        ChangePositionAndRotation();
    }

    private void EnemyCheck()
    {
        //Check for amount of Enemies near the player

        //Set cameraState to that amount
        
    }

    //Calculates the new offset to transition into different View/State
    private void CalculateOffset()
    {
        switch (cameraState)
        {
            case 0:
                offsetCurrent = CalculateSmoothMovementFromTo(offsetCurrent, offsetZero, zoomSpeedX, zoomSpeedY, zoomSpeedZ);
                SetBlendedEulerAngles(new Vector3(25.5f,0,0));
                break;
            case 1:
                offsetCurrent = CalculateSmoothMovementFromTo(offsetCurrent, offsetOne, zoomSpeedX, zoomSpeedY, zoomSpeedZ);
                SetBlendedEulerAngles(new Vector3(31.5f,0,0));
                break;
            case 2:
                offsetCurrent = CalculateSmoothMovementFromTo(offsetCurrent, offsetTwo, zoomSpeedX, zoomSpeedY, zoomSpeedZ);
                SetBlendedEulerAngles(new Vector3(33f,0,0));
                break;
            case 3:
                offsetCurrent = CalculateSmoothMovementFromTo(offsetCurrent, offsetThree, zoomSpeedX, zoomSpeedY, zoomSpeedZ);
                SetBlendedEulerAngles(new Vector3(35.5f,0,0));
                break;
            case 4:
                offsetCurrent = CalculateSmoothMovementFromTo(offsetCurrent, offsetFour, zoomSpeedX, zoomSpeedY, zoomSpeedZ);
                SetBlendedEulerAngles(new Vector3(37.8f,0,0));
                break;
            default:
                Debug.LogError("This State doesn't exist yet");
                cameraState = 0;
                break;
        }
    }

    //Calculates how to move camera to target
    private void CalculateSmoothFollow()
    {
        newPosition = CalculateSmoothMovementFromTo(this.gameObject.transform.position, (target.transform.position+offsetCurrent), followSpeedX, followSpeedY, followSpeedZ);
    }

    private void ChangePositionAndRotation()
    {
        this.gameObject.transform.position = newPosition;
        // Turn towards our target rotation.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turningRate * Time.deltaTime * 0.1f);
    }

    // Call this when you want to turn the object smoothly.
    public void SetBlendedEulerAngles(Vector3 angles)
    {
       targetRotation = Quaternion.Euler(angles);
    }

    Vector3 CalculateSmoothMovementFromTo(Vector3 positionCurrent, Vector3 positionTarget, float speedX, float speedY, float speedZ)
    {
        positionCurrent.x += (positionTarget.x - positionCurrent.x) * Time.deltaTime * speedX * 0.1f;
        positionCurrent.y += (positionTarget.y - positionCurrent.y) * Time.deltaTime * speedY * 0.1f;
        positionCurrent.z += (positionTarget.z - positionCurrent.z) * Time.deltaTime * speedZ * 0.1f;
        
        return positionCurrent;
    }
}
