using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedDash : MonoBehaviour
{
    public int dashDamage = 1;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyLife enemyLife = other.GetComponent<EnemyLife>();
            Debug.Log("EnemyLifeAtStart: " + enemyLife);
            enemyLife.life -= dashDamage;
            Debug.Log("EnemyLifeAtEnd: " + enemyLife);
            PopupDamageController.instance.CreatePopupText(dashDamage.ToString(), other.gameObject.GetComponent<Transform>().transform);
            other.GetComponent<EnemyLife>().BloodSplat();
        }
    }
}
