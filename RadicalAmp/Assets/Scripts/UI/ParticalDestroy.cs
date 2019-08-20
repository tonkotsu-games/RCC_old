using UnityEngine;

public class ParticalDestroy : MonoBehaviour
{
    ParticleSystem particle;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(particle.isStopped)
        {
            if(gameObject != null)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
