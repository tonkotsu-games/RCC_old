using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCamera : MonoBehaviour
{
    [SerializeField] Transform desiredPosition;
    [SerializeField] float holdTimeInSeconds;
    [SerializeField] Transform playerCamera;
    [SerializeField] float turningRate;
    [SerializeField] float speed;
    private bool reachedDesiredPosition = false;
    private Timer holdTimer = new Timer();
    private Vector3 currentPosition;

    private void OnEnable()
    {
        gameObject.transform.position = playerCamera.position;
        gameObject.transform.rotation = playerCamera.rotation;

        currentPosition = gameObject.transform.position;
    }

    private void Update()
    {
        if(!reachedDesiredPosition)
        {
            currentPosition.x += (desiredPosition.position.x - currentPosition.x) * Time.deltaTime * speed * 0.1f;
            currentPosition.y += (desiredPosition.position.y - currentPosition.y) * Time.deltaTime * speed * 0.1f;
            currentPosition.z += (desiredPosition.position.z - currentPosition.z) * Time.deltaTime * speed * 0.1f;

            gameObject.transform.position = currentPosition;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredPosition.rotation, turningRate * Time.deltaTime * 0.1f);
            Debug.Log("current " + currentPosition);
            Debug.Log(Vector3.Distance(desiredPosition.position, currentPosition));

            if (Vector3.Distance(desiredPosition.position, currentPosition) < 1f)
            {
                gameObject.transform.position = desiredPosition.position;
                reachedDesiredPosition = true;
                holdTimer.Start(holdTimeInSeconds);
                Debug.LogError("Reached");
            }
        }
        else
        {
            holdTimer.Tick();

            if(holdTimer.timeCurrent <= 0)
            {
                holdTimer.TogglePause();
                Debug.LogError("Reached");
            }
        }
    }
}
