using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private int maxHealth;
    [SerializeField] private float hitBuffer;
    [SerializeField] private float dissapearSpeed = 2f;
    [SerializeField] private int damagePerHit = 10;
    [SerializeField] private GameObject minimapIcon;

    private AudioSource audioSource;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private Animator anim;
    private ParticleSystem particleSystem;
    private float timer;
    private bool isAlive;
    private bool isReadyToDissapear;
    private int currentHealth;
    //private GameObject minimapIcon;

    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
    }


	// Use this for initialization
	void Start () {
        GameManager.instance.RegisterEnemy(this);
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        timer = 0;
        isAlive = true;
        isReadyToDissapear = false;
        currentHealth = maxHealth;
        //minimapIcon = Get
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if (isReadyToDissapear)
        {
            transform.Translate(-Vector3.up * dissapearSpeed * Time.deltaTime);
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if(timer > hitBuffer && other.tag == "PlayerWeapon")
        {
            takeHit();
            timer = 0f;
        }
    }

    private void takeHit()
    {
        particleSystem.Play();
        currentHealth -= damagePerHit;
        audioSource.PlayOneShot(audioSource.clip);
        if(currentHealth <= 0)
        {
            isAlive = false;
            killEnemy();
        } else
        {
            anim.Play("Hurt");
        }
    }

    private void killEnemy()
    {
        GameManager.instance.KillEnemy(this);
        capsuleCollider.enabled = false;
        navMeshAgent.enabled = false;
        anim.SetTrigger("EnemyDie");
        rigidBody.isKinematic = true;
        StartCoroutine(removeEnemy());
        minimapIcon.gameObject.SetActive(false);
    }

    IEnumerator removeEnemy()
    {
        yield return new WaitForSeconds(4f);
        isReadyToDissapear = true;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
