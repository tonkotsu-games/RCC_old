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
            EnemyHP enemyHP = other.GetComponent<EnemyHP>();
            Debug.Log("EnemyLifeAtStart: " + enemyHP);
            enemyHP.life -= dashDamage;
            Debug.Log("EnemyLifeAtEnd: " + enemyHP);
            PopupDamageController.instance.CreatePopupText(dashDamage, other.gameObject.GetComponent<Transform>().transform);
            other.GetComponent<EnemyHP>().BloodSplat();
        }
    }
}
