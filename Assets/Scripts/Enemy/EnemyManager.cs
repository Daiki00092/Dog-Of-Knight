using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{

    public Transform target;
    public Collider weaponCollider;
    Animator anim;
    NavMeshAgent agent;
    public EnemyUIManager enemyUIManager;
    public GameObject gameClearText;

    public AudioClip gameClearSE;
    AudioSource audioSource;

    public int maxHp = 100;
    public int hp = 100;

    void Start()
    {
        hp = maxHp;
        enemyUIManager.Init(this);

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        HideColliderWeapon();
    }

    void Update()
    {
        agent.destination = target.position;
        anim.SetFloat("Distance",agent.remainingDistance);
    }

    public void LookAtTarget()
    {
        transform   .LookAt(target);
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
            audioSource.PlayOneShot(gameClearSE);
            gameClearText.SetActive(true);
        }
        enemyUIManager.UpdateHP(hp);
        Debug.Log("残りHP:" + hp);
    }

    private void OnTriggerEnter(Collider other)
    {
        Damager damager = other.GetComponent<Damager>();
        if(damager != null)
        {
            anim.SetTrigger("Hurt");
            Damage(damager.damage);
        }
    }
}
