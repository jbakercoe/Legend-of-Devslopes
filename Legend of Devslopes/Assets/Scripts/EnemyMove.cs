using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class EnemyMove : MonoBehaviour {

    private Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    private EnemyHealth enemyHealth;
    

    // Use this for initialization
    void Start () {

        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        player = GameManager.Instance.Player.transform;

	}
	
	// Update is called once per frame
	void Update () {
        if(!GameManager.Instance.GameOver && enemyHealth.IsAlive)
        {
            nav.enabled = true;
            nav.SetDestination(player.position);
        } else
        {
            nav.enabled = false;
        }
        if(GameManager.Instance.GameOver && enemyHealth.IsAlive)
        {
            anim.Play("Idle");
        }

	}
}
