using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceHitProjectile : MonoBehaviour
{
    Vector3 spawnPoint;
    [SerializeField] float projectileSpeed;
    [Range(1,20)]
    [SerializeField] float maxRange;
    [SerializeField] int projectileDamage;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(gameObject.transform.position, spawnPoint) < maxRange)
        {
            gameObject.transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
        else
        { 
            Debug.Log("Destroying Projectile");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && other.gameObject.GetComponent<EnemyHP>() != null)
        {
            EnemyHP enemyLife = other.GetComponent<EnemyHP>();
            Debug.Log("EnemyLifeAtStart: " + enemyLife);
            enemyLife.life -= projectileDamage;
            Debug.Log("EnemyLifeAtEnd: " + enemyLife);
            PopupDamageController.instance.CreatePopupText(projectileDamage.ToString(), other.gameObject.GetComponent<Transform>().transform);
            other.GetComponent<EnemyHP>().BloodSplat();
        }
    }
}
