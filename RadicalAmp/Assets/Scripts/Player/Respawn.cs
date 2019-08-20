using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    
    [SerializeField] GameObject respawner;

    public void RespawnPlayer()
    {    
        player.transform.position = respawner.transform.position;        
    }
}