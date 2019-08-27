using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRingController : MonoBehaviour
{
    public int ringNumber;

    public static float stepSize;
    public static float moveSpeed;
    public bool isMoving;

    private Vector3 basePosition;

    private void Start()
    {
        SetBasePos();
    }

    public void Update()
    {
        if (isMoving)
        {
            MoveUp();
        }
        
    }
    public void MoveUp()
    {
        if (transform.position.y <= basePosition.y + stepSize)
        {
            transform.Translate(0,moveSpeed * Time.deltaTime , 0);
        }
        else
        {
            isMoving = false;
        }
    }

    public void SetBasePos()
    {
        basePosition = transform.position;
    }
}
