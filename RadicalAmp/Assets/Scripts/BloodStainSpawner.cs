using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodStainSpawner : MonoBehaviour
{

    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public GameObject bloodStainPrefab;
    Vector3 pos;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("COllision!");
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        
        int i = 0;

        while (i < numCollisionEvents)
        {
         
            pos = collisionEvents[i].intersection;

            SpawnBloodStain();
               
            
            i++;
        }
    }

    private void SpawnBloodStain()
    {
        GameObject particlesInstance = Instantiate(bloodStainPrefab, pos, Quaternion.identity) as GameObject;
        
        ParticleSystem parts = particlesInstance.GetComponentInChildren<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetime.constant + parts.main.startDelay.constant;
        Destroy(particlesInstance, totalDuration);
    }
}
