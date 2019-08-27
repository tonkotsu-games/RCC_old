﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera State")]
    [Tooltip("Camera state integer, dependent on the number of enemies near the player.")]
    [SerializeField] private int cameraState = 0;

    [Header("Camera Target")]
    [Tooltip("The cameras desired target.")]
    [SerializeField] Transform target;
    
    //[Tooltip("The offsets the camera should have from the target, dependent on the Camera State.")]
    [Header("Offsets")]
    [Tooltip("The offset for CameraState 0.")]
    [SerializeField] Vector3 offsetZero = new Vector3(0f,5f,-6f);
    [Tooltip("The offset for CameraState 1.")]
    [SerializeField] Vector3 offsetOne = new Vector3(0f,7f,-7.5f);
    [Tooltip("The offset for CameraState 2.")]
    [SerializeField] Vector3 offsetTwo = new Vector3(0f,8f,-8.5f);
    [Tooltip("The offset for CameraState 3.")]
    [SerializeField] Vector3 offsetThree = new Vector3(0f,10f,-10.5f);
    [Tooltip("The offset for CameraState 4 (currently the maximum, even if there are more enemies).")]
    [SerializeField] Vector3 offsetFour = new Vector3(0f,12f,-12f);
    //[Tooltip("The angles the camera should adopt, dependent on the CameraState.")]
    [Header("Angles")]
    [Tooltip("The angle for CameraState 0.")]
    [SerializeField] Vector3 angleZero = new Vector3(25.5f,0f,0f);
    [Tooltip("The angle for CameraState 1.")]
    [SerializeField] Vector3 angleOne = new Vector3(31.5f,0f,0f);
    [Tooltip("The angle for CameraState 2.")]
    [SerializeField] Vector3 angleTwo = new Vector3(33f,0f,0f);
    [Tooltip("The angle for CameraState 3.")]
    [SerializeField] Vector3 angleThree = new Vector3(35.5f,0f,0f);
    [Tooltip("The angle for CameraState 4.")]
    [SerializeField] Vector3 angleFour = new Vector3(37.8f,0f,0f);
    //[Tooltip("The speed at which the camera should follow the Player, dependent on the CameraState.")]
    [Header("Follow Speed")]
    [Tooltip("The follow speed for CameraState 0.")]
    [SerializeField] Vector3 followSpeedZero = new Vector3(20f,15f,40f);
    [Tooltip("The follow speed for CameraState 1.")]
    [SerializeField] Vector3 followSpeedOne = new Vector3(20f,15f,40f);
    [Tooltip("The follow speed for CameraState 2.")]
    [SerializeField] Vector3 followSpeedTwo = new Vector3(20f,15f,40f);
    [Tooltip("The follow speed for CameraState 3.")]
    [SerializeField] Vector3 followSpeedThree = new Vector3(20f,15f,40f);
    [Tooltip("The follow speed for CameraState 4.")]
    [SerializeField] Vector3 followSpeedFour = new Vector3(20f,15f,40f);
    //[Tooltip("The speed at which the camera should move to the state dependent offset, also dependent on the CameraState.")]
    [Header("Zoom Speed")]
    [Tooltip("The zoom speed for CameraState 0.")]
    [SerializeField] Vector3 zoomSpeedZero = new Vector3(10f,10f,10f);
    [Tooltip("The zoom speed for CameraState 1.")]
    [SerializeField] Vector3 zoomSpeedOne = new Vector3(10f,10f,10f);
    [Tooltip("The zoom speed for CameraState 2.")]
    [SerializeField] Vector3 zoomSpeedTwo = new Vector3(10f,10f,10f);
    [Tooltip("The zoom speed for CameraState 3.")]
    [SerializeField] Vector3 zoomSpeedThree = new Vector3(10f,10f,10f);
    [Tooltip("The zoom speed for CameraState 4.")]
    [SerializeField] Vector3 zoomSpeedFour = new Vector3(10f,10f,10f);
    //[Tooltip("The speed at which the camera should to the state dependent angle, currently only around X-Axis.")]
    [Header("Camera X Rotation Speed")]
    [Tooltip("Maximum turn rate in degrees per second for State 0.")]
    [SerializeField] float turningRateZero = 30f;
    [Tooltip("Maximum turn rate in degrees per second for State 1.")]
    [SerializeField] float turningRateOne = 30f;
    [Tooltip("Maximum turn rate in degrees per second for State 2.")]
    [SerializeField] float turningRateTwo = 30f;
    [Tooltip("Maximum turn rate in degrees per second for State 3.")]
    [SerializeField] float turningRateThree = 30f;
    [Tooltip("Maximum turn rate in degrees per second for State 4.")]
    [SerializeField] float turningRateFour = 30f;

    private float turningRate = 30f;
    private Vector3 offsetCurrent;
    private Vector3 followSpeed = new Vector3(20f,15f,40f); 
    private Vector3 newPosition = new Vector3(0,0,0);

    // Rotation we should blend towards.
    private Quaternion targetRotation = Quaternion.identity;
     
    void Start()
    {
        this.gameObject.transform.position = target.position + offsetZero;
        offsetCurrent = offsetZero;
        followSpeed = followSpeedZero;
        turningRate = turningRateZero;
    }


    void Update()
    {
        EnemyCheck();
        SetVariables();
        CalculateSmoothFollow();
        ChangePositionAndRotation();
    }

    private void EnemyCheck()
    {
        //Check for amount of Enemies near the player

        //Set cameraState to that amount
        
    }

    //Calculates the new offset to transition into different View/State
    private void SetVariables()
    {
        switch (cameraState)
        {
            case 0:
                offsetCurrent = CalculateSmoothMovementFromTo(offsetCurrent, offsetZero, zoomSpeedZero.x, zoomSpeedZero.y, zoomSpeedZero.z);
                SetBlendedEulerAngles(angleZero);
                followSpeed = followSpeedZero;
                turningRate = turningRateZero;
                break;
            case 1:
                offsetCurrent = CalculateSmoothMovementFromTo(offsetCurrent, offsetOne, zoomSpeedOne.x, zoomSpeedOne.y, zoomSpeedOne.z);
                SetBlendedEulerAngles(angleOne);
                followSpeed = followSpeedOne;
                turningRate = turningRateOne;
                break;
            case 2:
                offsetCurrent = CalculateSmoothMovementFromTo(offsetCurrent, offsetTwo, zoomSpeedTwo.x, zoomSpeedTwo.y, zoomSpeedTwo.z);
                SetBlendedEulerAngles(angleTwo);
                followSpeed = followSpeedTwo;
                turningRate = turningRateTwo;
                break;
            case 3:
                offsetCurrent = CalculateSmoothMovementFromTo(offsetCurrent, offsetThree, zoomSpeedThree.x, zoomSpeedThree.y, zoomSpeedThree.z);
                SetBlendedEulerAngles(angleThree);
                followSpeed = followSpeedThree;
                turningRate = turningRateThree;
                break;
            case 4:
                offsetCurrent = CalculateSmoothMovementFromTo(offsetCurrent, offsetFour,zoomSpeedFour.x, zoomSpeedFour.y, zoomSpeedFour.z);
                SetBlendedEulerAngles(angleFour);
                followSpeed = followSpeedFour;
                turningRate = turningRateFour;
                break;
            default:
                Debug.LogError("This State doesn't exist yet");
                cameraState = 4;
                break;
        }
    }

    //Calculates how to move camera to target
    private void CalculateSmoothFollow()
    {
        newPosition = CalculateSmoothMovementFromTo(this.gameObject.transform.position, (target.transform.position+offsetCurrent), followSpeed.x, followSpeed.y, followSpeed.z);
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
