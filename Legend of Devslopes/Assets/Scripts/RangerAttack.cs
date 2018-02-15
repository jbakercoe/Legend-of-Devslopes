using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : MonoBehaviour
{

    [SerializeField] private float range = 3f;
    [SerializeField] private float timeBetweenAttacks = 1f;
    [SerializeField] Transform fireLocation;

    private Animator anim;
    private GameObject player;
    private GameObject arrow;
    private bool playerInRange;
    private EnemyHealth enemyHealth;

    // Use this for initialization
    void Start()
    {
        arrow = GameManager.Instance.Arrow;
        player = GameManager.Instance.Player;
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        StartCoroutine(attack());
    }

    // Update is called once per frame
    void Update()
    {
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
        if (playerInRange && !GameManager.Instance.GameOver)
        {
            anim.Play("Attack");
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        yield return null;
        if (enemyHealth.IsAlive)
        { StartCoroutine(attack()); }
    }    

    private void turnTowards(Transform player)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookDirection = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * 10f);
    }

    public void FireArrow()
    {
        GameObject newArrow = Instantiate(arrow) as GameObject;
        newArrow.transform.position = fireLocation.position;
        newArrow.transform.rotation = fireLocation.rotation;
        newArrow.GetComponent<Rigidbody>().velocity = transform.forward * 25f;
    }

}
