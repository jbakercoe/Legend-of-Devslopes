using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField] private float range = 3f;
    [SerializeField] private float timeBetweenAttacks = 1f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private BoxCollider[] weaponColliders;
    private EnemyHealth enemyHealth;

	// Use this for initialization
	void Start () {
        weaponColliders = GetComponentsInChildren<BoxCollider>();
        player = GameManager.Instance.Player;
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        StartCoroutine(attack());
	}
	
	// Update is called once per frame
	void Update () {
        if (enemyHealth.IsAlive)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < range)
            {
                playerInRange = true;
                anim.SetBool("HeroInRange", true);
                turnTowards(player.transform);
            }
            else
            {
                playerInRange = false;
                anim.SetBool("HeroInRange", false);
            }
        }
	}

    IEnumerator attack()
    {
        if(playerInRange && !GameManager.Instance.GameOver)
        {
            anim.Play("Attack");
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        yield return null;
        if (enemyHealth.IsAlive)
        { StartCoroutine(attack()); }
    }

    public void BeginAttack()
    {
        foreach (var weapon in weaponColliders)
        {
            weapon.enabled = true;
        }
    }

    public void EndAttack()
    {
        foreach (var weapon in weaponColliders)
        {
            weapon.enabled = false;
        }
    }

    private void turnTowards(Transform player)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookDirection = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * 10f);
    }

}
