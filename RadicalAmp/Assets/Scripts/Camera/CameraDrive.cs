using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class CameraDrive : MonoBehaviour
{
    public static CameraDrive instance;

    public float heightSmooth = 2f;
    public float minDistanceSmooth = 1f;
    public float maxDistanceSmooth = 5f;
    private float distanceSmooth;
    public float rotationSmooth = 2f;
    public float distanceToSpeedUp = 5;
    [Range(2, 15)]
    public float height = 6, posZminus = 8;
    [Range(0, 60)]
    public float camRotation = 32;

    [Tooltip("Camera Offset: x = height/y = distance/z = angle")]
    [SerializeField] Vector3 Zoom1 = new Vector3(5f, 6f, 29.5f);
    [Tooltip("Camera Offset: x = height/y = distance/z = angle")]
    [SerializeField] Vector3 Zoom2 = new Vector3(6.5f, 7.5f, 31.5f);
    [Tooltip("Camera Offset: x = height/y = distance/z = angle")]
    [SerializeField] Vector3 Zoom3 = new Vector3(8f, 8.5f, 33f);
    [Tooltip("Camera Offset: x = height/y = distance/z = angle")]
    [SerializeField] Vector3 Zoom4 = new Vector3(10f, 10.5f, 35.5f);
    [Tooltip("Camera Offset: x = height/y = distance/z = angle")]
    [SerializeField] Vector3 Zoom5 = new Vector3(12f, 12f, 37.8f);



    private Transform player;

    private Vector3 velocity = Vector3.zero;

    [SerializeField] float zoomInDelay = 5;

    public bool readyToZoomIn = false;

    public enum ZoomMode { None, In, Out }

    public ZoomMode ZoomState = ZoomMode.None;

    public enum CameraStates { ZoomMin,ZoomTwo,ZoomThree,ZoomFour,ZoomMax}
    public CameraStates currentState = CameraStates.ZoomMax;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        ChangeCameraState(CameraStates.ZoomTwo);
    }

    void Update()
    {
         UpdateCameraPos();

        if (ZoomState == ZoomMode.In)
        {
            ChangeCameraState(CameraStates.ZoomMin);
            return;
        }

        else if (ZoomState == ZoomMode.Out)
        {
            ChangeCameraState(CameraStates.ZoomMax);
            return;
        }
    }

    private void UpdateCameraPos()
    {
       // if(player.transform.position.z - transform.position.z <= distanceToSpeedUp)
       // {
       //     distanceSmooth = maxDistanceSmooth;
       // }
       // else
       // {
       //     distanceSmooth = minDistanceSmooth;
       // }
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.z = player.position.z - posZminus;
        pos.y = player.position.y + height;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, distanceSmooth);
       // Quaternion targetRotation = Quaternion.Euler(camRotation, 0, 0);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSmooth * Time.deltaTime);
       // Vector3 smoothedPos = new Vector3();
       // smoothedPos.x = pos.x;
       // smoothedPos.y = Mathf.Lerp(transform.position.y, pos.y, heightSmooth * Time.deltaTime);
       // smoothedPos.z = Mathf.Lerp(transform.position.z, pos.z, minDistanceSmooth * Time.deltaTime);
       // transform.position = smoothedPos;
    }

    IEnumerator ZoomInDelay()
    {
        yield return new WaitForSeconds(zoomInDelay);
        readyToZoomIn = true;
    }

public void ChangeCameraState(CameraStates requestedState)
    {
        if (currentState == requestedState)
        {
            return;
        }

        else
        {
            switch (requestedState)
            {
                case CameraStates.ZoomMin:
                    currentState = requestedState;
                    height = Zoom1.x;
                    posZminus = Zoom1.y;
                    camRotation = Zoom1.z;
                    break;

                case CameraStates.ZoomTwo:
                    currentState = requestedState;
                    height = Zoom2.x;
                    posZminus = Zoom2.y;
                    camRotation = Zoom2.z;
                    break;

                case CameraStates.ZoomThree:
                    currentState = requestedState;
                    height = Zoom3.x;
                    posZminus = Zoom3.x;
                    camRotation = Zoom3.z;
                    break;

                case CameraStates.ZoomFour:
                    currentState = requestedState;
                    height = Zoom4.x;
                    posZminus = Zoom4.y;
                    camRotation = Zoom4.z;
                    break;

                case CameraStates.ZoomMax:
                    currentState = requestedState;
                    height = Zoom5.x;
                    posZminus = Zoom5.y;
                    camRotation = Zoom5.z;
                    break;
            }
            Debug.Log("Switched Camera to: " + currentState);
        }
    }
}
