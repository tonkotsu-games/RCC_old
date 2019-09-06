using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    
    [SerializeField] GameObject respawner;

    [SerializeField] EnemyHP levelBossHP;

    public void RespawnPlayer()
    {    
        player.transform.position = respawner.transform.position;

        if(levelBossHP!=null)
        {
            levelBossHP.life = 20;
        }
        else
        {
            Debug.Log("Level Boss Hp in Respawn not set up");
        }
    }
}