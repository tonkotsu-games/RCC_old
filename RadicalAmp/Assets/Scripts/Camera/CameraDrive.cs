using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraDrive : MonoBehaviour
{
    public static CameraDrive instance;

    public float smooth = 0.3f;
    [Range(2, 10)]
    public float height, posZminus;

    private Transform player;

    private Vector3 velocity = Vector3.zero;

    [SerializeField] float zoomInDelay = 5;

    public bool readyToZoomIn = false;

    public enum ZoomMode { None,In,Out}

    public ZoomMode ZoomState = ZoomMode.None;

    public enum CameraStates { ZoomedIn,ZoomedOut}
    public CameraStates currentState = CameraStates.ZoomedIn;


    private void Awake()
    {
        if(instance == null)
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
        ChangeCameraState(CameraStates.ZoomedOut);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraPos();

        if (ZoomState == ZoomMode.In)
        {
            ChangeCameraState(CameraStates.ZoomedIn);
            return;
        }

        else if (ZoomState == ZoomMode.Out)
        {
            ChangeCameraState(CameraStates.ZoomedOut);
            return;
        }
    }

    private void UpdateCameraPos()
    {
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.z = player.position.z - posZminus;
        pos.y = player.position.y + height;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
    }
    public void ChangeCameraState(CameraStates requestedState)
    {
        if (currentState == requestedState)
        {
            Debug.Log("Already in Camerastate: " + requestedState);
            return;
        }

        else
        {
            switch (requestedState)
            {
                case CameraStates.ZoomedIn:
                    Debug.Log("Zooming In");

                    currentState = CameraStates.ZoomedIn;
                    height = 7;
                    posZminus = 6;
                    
                    break;

                case CameraStates.ZoomedOut:
                    Debug.Log("Zooming Out");

                    currentState = CameraStates.ZoomedOut;
                    height = 12;
                    posZminus = 11;
                    readyToZoomIn = false;
                    StartCoroutine(ZoomInDelay());
                    break;
            }
        }
    }

    IEnumerator ZoomInDelay()
    {
        yield return new WaitForSeconds(zoomInDelay);
        readyToZoomIn = true;
    }
}
