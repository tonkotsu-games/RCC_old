using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraDrive : Singleton
{

    public float smooth = 0.3f;
    [Range(2, 10)]
    public float height, posZminus;

    private Transform player;

    private Vector3 velocity = Vector3.zero;

    [SerializeField] float zoomInDelay = 5;
    public bool zoomInReady = false;

    public bool setZoomedOut;
    public bool setZoomedIn;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        ZoomIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<CameraRangeAdjust>().CheckForEnemiesInRange())
        {
            ZoomOut();
        }
        else if(gameObject.GetComponent<CameraRangeAdjust>().CheckForEnemiesInRange() == false && zoomInReady)
        {
            ZoomIn();
        }
        
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.z = player.position.z - posZminus;
        pos.y = player.position.y + height;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);

        if (setZoomedIn)
        {
            ZoomIn();
            return;
        }

        else if (setZoomedOut)
        {
            ZoomOut();
            return;
        }
    }

    public void ZoomOut()
    {
        Debug.Log("ZoomingOut");

       
        height = 8;
        posZminus = 11;
        zoomInReady = false;

        if (!setZoomedOut)
        {
            StartCoroutine(ZoomInDelay(zoomInDelay));
        }
    }

    public void ZoomIn()
    {
     
        height = 5;
        posZminus = 6;
    }

    IEnumerator ZoomInDelay(float waittime)
    {
        yield return new WaitForSeconds(waittime);


        zoomInReady = true;
    }
}
