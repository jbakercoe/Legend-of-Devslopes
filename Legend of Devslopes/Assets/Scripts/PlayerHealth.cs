using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] int maxHealth = 100;
    [SerializeField] float timeSinceLastHit = 2f;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject red;

    private float timer = 0f;
    private CharacterController characterController;
    private Animator anim;
    private Animator redAnim;
    private new ParticleSystem particleSystem;
    private int currentHealth;

    private void Awake()
    {
        Assert.IsNotNull(healthSlider);
        Assert.IsNotNull(red);
    }

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        redAnim = red.GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        particleSystem = GetComponentInChildren<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
	}

    void OnTriggerEnter(Collider other)
    {
        //print("PlayerTriggerEnter: " + other.tag);
        // If collider is a weapon, if enough time has elapsed since last hit, and if !dead
        if(other.tag == "Weapon" && timer >= timeSinceLastHit && !GameManager.Instance.GameOver)
        {
            //print("Taking hit");
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
        AudioManager.Instance.Play("Hero Hurt");
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            if (currentHealth <= 30)
            {
                redAnim.SetBool("hasLowHealth", true);
            }
        } else // Hero is dead, game over
        {
            redAnim.enabled = false;
            anim.SetTrigger("IsDead");
            characterController.enabled = false;
        }
        GameManager.Instance.playerHit(currentHealth);
    }

    public void giveHealth(int health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        healthSlider.value = currentHealth;
        if (currentHealth > 30)
            redAnim.SetBool("hasLowHealth", false);
    }

}
