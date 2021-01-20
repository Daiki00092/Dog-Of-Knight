using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    float x;
    float z;
    public float moveSpeed = 3;
    public Collider weaponCollider;
    public PlayerUIManager playerUIManager;

    public GameObject gameOverText;
    public GameObject enemy;
    public AudioClip gameOverSE;
    public Transform target;
    public int maxHp = 100;
    int hp;
    public int maxSp = 100;
    int sp;

    public AudioClip AttackSE;
    public AudioClip DecreaseHP;
    AudioSource audioSource;
    Rigidbody rb;
    Animator anim;

    void Start()
    {
        hp = maxHp;
        sp = maxSp;
        playerUIManager.Init(this);
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        HideColliderWeapon();
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
        void RestartScene()
        {
            Scene thisScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(thisScene.name);
        }
        RecoverStamina();
    }

    void RecoverStamina()
    {
        sp++;
        if(sp >= maxSp)
        {
            sp = maxSp;
        }
        playerUIManager.Updatestamina(sp);
    }

    void Attack()
    {
        if(sp >= 49)
        {
            sp -= 49;
            anim.SetTrigger("Attack");
            if(target != null)
            {
                LookAtTarget();
                audioSource.PlayOneShot(AttackSE);
            }
        }
    }

    void LookAtTarget()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= 2f)
        {
        transform.LookAt(target);
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.position + rb.velocity;
        transform.LookAt(direction);
        rb.velocity = new Vector3(x, 0, z) * moveSpeed;
        anim.SetFloat("Speed", rb.velocity.magnitude);
    }

    public void HideColliderWeapon()
    {
        weaponCollider.enabled = false;
    }
    public void ShowColliderWeapon()
    {
        weaponCollider.enabled = true;
    }

    void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            anim.SetTrigger("Die");
            hp = 0;
            audioSource.Stop();
            gameOverText.SetActive(true);
            enemy.GetComponent<AudioSource>().Stop();
            audioSource.PlayOneShot(gameOverSE);
        }
        playerUIManager.UpdateHP(hp);
        Debug.Log("残りHP:" + hp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(hp <= 0)
        {
            return;
        }

        Damager damager = other.GetComponent<Damager>();
        if (damager != null)
        {
            anim.SetTrigger("Hurt");
            Damage(damager.damage);
            audioSource.PlayOneShot(DecreaseHP);
        }
    }
}
