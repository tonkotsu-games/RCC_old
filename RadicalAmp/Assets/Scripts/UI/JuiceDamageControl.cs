using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuiceDamageControl : MonoBehaviour
{
    private Slider Juicemeter;
    private AttackingCheck damage;

    // Start is called before the first frame update
    void Start()
    {
        Juicemeter = gameObject.GetComponent<Slider>();
        damage = GameObject.FindWithTag("PlayerWeapon").GetComponent<AttackingCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Juicemeter.value >= 0 && Juicemeter.value <= 25 )
        {
            damage.damage = 1;
        }
        else if(Juicemeter.value >= 26 && Juicemeter.value <= 50)
        {
            damage.damage = 2;
        }
        else if (Juicemeter.value >= 51 && Juicemeter.value <= 100)
        {
            damage.damage = 3;
        }
    }
}