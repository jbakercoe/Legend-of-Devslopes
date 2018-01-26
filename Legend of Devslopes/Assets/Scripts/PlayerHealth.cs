using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] int maxHealth = 100;
    [SerializeField] float timeSinceLastHit = 2f;
    [SerializeField] Slider healthSlider;

    private float timer = 0f;
    private CharacterController characterController;
    private AudioSource audioSource;
    private Animator anim;
    private new ParticleSystem particleSystem;
    private int currentHealth;

    private void Awake()
    {
        Assert.IsNotNull(healthSlider);
    }

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        particleSystem = GetComponentInChildren<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
	}

    void OnTriggerEnter(Collider other)
    {
        // If collider is a weapon, if enough time has elapsed since last hit, and if !dead
        if(other.tag == "Weapon" && timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            takeHit();
            timer = 0;
        }
    }

    void takeHit()
    {
        particleSystem.Play();
        // could make it so it always does the hurt thing, then checks if dead for the dead thing
        currentHealth -= 10;
        healthSlider.value = currentHealth;
        audioSource.PlayOneShot(audioSource.clip);
        if (currentHealth > 0)
        {
            anim.Play("Hurt");
        } else // Hero is dead, game over
        {
            anim.SetTrigger("HeroDie");
            characterController.enabled = false;
        }
        GameManager.instance.playerHit(currentHealth);
    }

    public void giveHealth(int health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }

}
