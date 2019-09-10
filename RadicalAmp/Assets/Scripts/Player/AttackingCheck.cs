using UnityEngine;

public class AttackingCheck : MonoBehaviour
{
    private TestFight testFight;
    private PopupDamageController popupController;

    [SerializeField] private GameObject particle;
    [Header("Knockback Strenght float")]
    [SerializeField] private float knockBackStrenght;

    AudioSource my_audioSource;
    public AudioClip[] enemyHitSound;

    EnemyLife live;

    public int damage = 2;

    private void Start()
    {
        my_audioSource = GetComponent<AudioSource>();

        popupController = GameObject.FindWithTag("Player").GetComponent<PopupDamageController>();
        if(testFight == null)
        {
            testFight = GameObject.FindWithTag("TestFight").GetComponent<TestFight>();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController.attack == true && other.gameObject.tag == "Enemy" && !other.gameObject.GetComponent<EnemyLife>().death)
        {
            live = other.gameObject.GetComponent<EnemyLife>();
            live.life -= damage;
            popupController.CreatePopupText(damage, other.gameObject.GetComponent<Transform>().transform);

            my_audioSource.clip = enemyHitSound[Random.Range(0, enemyHitSound.Length)];
            my_audioSource.Play();

            live.BloodSplat();

            if (BeatStrike.beatAttack && !other.gameObject.GetComponent<EnemyLife>().death)
            {
                GameObject particalEffect = Instantiate(particle, this.transform.position, Quaternion.identity);
            }
            if (testFight.knockBack)
            {
                Rigidbody rb = other.GetComponent<Rigidbody>();

                Vector3 direction = other.transform.position - transform.position;
                direction.y = 0;

                rb.transform.position += direction.normalized * knockBackStrenght;
            }
            if (testFight.disturbAttack)
            {
                EnemyController enemy = other.GetComponent<EnemyController>();
                enemy.hit = true;
            }
        }
        else
        {
            return;
        }
    }    
}