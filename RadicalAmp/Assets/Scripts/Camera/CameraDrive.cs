using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraDrive : MonoBehaviour
{

    public float smooth = 0.3f, height, posZminus;

    private Transform player;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.z = player.position.z - posZminus;
        pos.y = player.position.y + height;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
    }
}
