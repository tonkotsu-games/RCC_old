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
    [SerializeField] GameObject followCamera;
    [SerializeField] Transform player;
    private bool reachedDesiredPosition = false;
    private Timer holdTimer = new Timer();
    private Vector3 currentPosition;
    private bool returning = false;

    private void OnEnable()
    {
        gameObject.transform.position = playerCamera.position;
        gameObject.transform.rotation = playerCamera.rotation;

        currentPosition = gameObject.transform.position;
    }

    private void Update()
    {
        if(returning)
        {
            desiredPosition.position = player.transform.position + new Vector3(0, 5, -6);
        }

        currentPosition.x += (desiredPosition.position.x - currentPosition.x) * Time.deltaTime * speed * 0.1f;
        currentPosition.y += (desiredPosition.position.y - currentPosition.y) * Time.deltaTime * speed * 0.1f;
        currentPosition.z += (desiredPosition.position.z - currentPosition.z) * Time.deltaTime * speed * 0.1f;

        gameObject.transform.position = currentPosition;
        Debug.Log("desired rotation" + desiredPosition.rotation);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredPosition.rotation, turningRate * Time.deltaTime * 0.1f);

        if (!reachedDesiredPosition)
        {
            if (Vector3.Distance(desiredPosition.position, currentPosition) < 1.5f && !returning)
            {
                reachedDesiredPosition = true;
                holdTimer.Start(holdTimeInSeconds);
                Debug.LogError("Reached");
            }
            else if (Vector3.Distance(desiredPosition.position, currentPosition) < 1.5f && returning)
            {
                followCamera.SetActive(true);
                followCamera.transform.position = transform.position;
                Destroy(this.gameObject);
            }
        }
        else
        {
            holdTimer.Tick();

            if(holdTimer.timeCurrent <= 0)
            {
                holdTimer.TogglePause();
                Debug.LogError("Timer Done");
                desiredPosition.position = player.transform.position + new Vector3(0,5,-6);
                desiredPosition.rotation = followCamera.transform.rotation;
                reachedDesiredPosition = false;
                speed = 25f;
                returning = true;
            }
        }
    }
}
