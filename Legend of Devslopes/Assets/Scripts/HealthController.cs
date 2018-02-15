using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    [SerializeField] int hp;
    private GameObject player;
    private PlayerHealth playerHealth;

	// Use this for initialization
	void Start () {
        player = GameManager.Instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
	}
	
    private void OnTriggerEnter(Collider other)
    {
        if (other == player.GetComponent<CharacterController>())
        {
            playerHealth.giveHealth(hp);            
            Destroy(gameObject);
        }
    }
}
