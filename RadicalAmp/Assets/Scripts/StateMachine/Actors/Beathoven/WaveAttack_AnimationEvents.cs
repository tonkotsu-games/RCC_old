using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttack_AnimationEvents : MonoBehaviour
{
    private Transform playerTransform;
    private bool checking = false;
    [SerializeField] private float innerRangeOne = 0f;
    [SerializeField] private float outerRangeOne = 5f;
    [SerializeField] private float innerRangeTwo = 5f;
    [SerializeField] private float outerRangeTwo = 10f;
    [SerializeField] private float innerRangeThree = 10f;
    [SerializeField] private float outerRangeThree = 15f;
    [SerializeField] private int damage = 3;
    private float rangeInner;
    private float rangeOuter;
    private int explosionNumber = 0;

    private void Awake()
    {
        playerTransform = Locator.instance.GetPlayerPosition();
    }

    public void Explode()
    {  
        if(explosionNumber == 0)
        {
            EnableCheckWith(innerRangeOne, outerRangeOne);
        }
        else if(explosionNumber == 1)
        {
            EnableCheckWith(innerRangeTwo, outerRangeTwo);
        }
        else if(explosionNumber == 2)
        {
            EnableCheckWith(innerRangeThree, outerRangeThree);
            explosionNumber = 0;
        }
        else
        {
            Debug.LogError("WaveAttack Went Wrong: Amount of Explosions currently not supported!");
        }
        explosionNumber ++;
    }

    void Update()
    {
        if(!checking)
        {
            return;
        }

        CheckPlayer();
    }

    private void EnableCheckWith(float innerRange, float outerRange)
    {
        rangeInner = innerRange;
        rangeOuter = outerRange;
        checking = true;
    }

    public void DisableChecking()
    {
        checking = false;
    }

    private void CheckPlayer()
    {
        float distanceToPlayer = Vector3.Distance(playerTransform.position, this.gameObject.transform.position);
        if(distanceToPlayer >= rangeInner && distanceToPlayer <= rangeOuter)
        {
            playerTransform.gameObject.GetComponent<PlayerController>().life -= damage;

            checking = false;
        }
    }
}
