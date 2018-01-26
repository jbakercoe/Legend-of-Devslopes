using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    [SerializeField] int hp;
    private GameObject player;
    private PlayerHealth playerHealth;
    private AudioSource audioSrc;

	// Use this for initialization
	void Start () {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player.GetComponent<CharacterController>())
        {
            playerHealth.giveHealth(hp);
            audioSrc.PlayOneShot(audioSrc.clip);
            Destroy(gameObject);
            //GameManager.instance.spawnPowerUp();
        }
    }
}
