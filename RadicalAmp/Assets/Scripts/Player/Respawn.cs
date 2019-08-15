using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    
    //public static int enemyDead = 0;

    //int toDieLength;

    [SerializeField] GameObject respawner;
    //[SerializeField] int [] enemyHaveToDie;

    private void Start()
    {
        //player = GameObject.FindWithTag("Player");
        //toDieLength = enemyHaveToDie.Length;
    }

    public void RespawnPlayer()
    {    
        player.transform.position = respawner.transform.position;        
    }
}
