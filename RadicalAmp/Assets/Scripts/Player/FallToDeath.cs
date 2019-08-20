using UnityEngine;

public class FallToDeath : MonoBehaviour
{
    PlayerController player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.afterdeath();
        }
    }
}
