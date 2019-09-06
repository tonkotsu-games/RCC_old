using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceHitProjectile : MonoBehaviour
{
    Vector3 spawnPoint;
    [SerializeField] float projectileSpeed;
    [Range(1,20)]
    [SerializeField] float maxRange;
    [SerializeField] int projectileDamage;
    Slider juiceMeter;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = gameObject.transform.position;
        juiceMeter = Locator.instance.GetJuiceMeter();
    }

    // Update is called once per frame
    void Update()
    {
        projectileDamage = Random.Range(300, 500) + Mathf.RoundToInt(juiceMeter.value*10);
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
        if (other.tag == "Enemy")
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
