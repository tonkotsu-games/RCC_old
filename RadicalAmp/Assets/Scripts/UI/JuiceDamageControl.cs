using UnityEngine;
using UnityEngine.UI;

public class JuiceDamageControl : MonoBehaviour
{
    private Slider Juicemeter;
    private AttackingCheck damage;

    void Start()
    {
        Juicemeter = gameObject.GetComponent<Slider>();
        damage = GameObject.FindWithTag("PlayerWeapon").GetComponent<AttackingCheck>();
    }

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