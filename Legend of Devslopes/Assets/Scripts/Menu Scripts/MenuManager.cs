using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

public class MenuManager : MonoBehaviour {

    [SerializeField] GameObject hero;
    [SerializeField] GameObject tanker;
    [SerializeField] GameObject ranger;
    [SerializeField] GameObject soldier;
    [SerializeField] GameObject black;

    private Animator heroAnim;
    private Animator tankerAnim;
    private Animator rangerAnim;
    private Animator soldierAnim;
    private Animator blackAnim;

    private void Awake()
    {
        Assert.IsNotNull(hero);
        Assert.IsNotNull(tanker);
        Assert.IsNotNull(ranger);
        Assert.IsNotNull(soldier);
        Assert.IsNotNull(black);
    }

    // Use this for initialization
    void Start ()
    {
        heroAnim = hero.GetComponent<Animator>();
        tankerAnim = tanker.GetComponent<Animator>();
        rangerAnim = ranger.GetComponent<Animator>();
        soldierAnim = soldier.GetComponent<Animator>();
        blackAnim = black.GetComponent<Animator>();
        StartCoroutine(showcase());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Battle()
    {
        //blackAnim.Play("sceneOut");
        Debug.Log("Faded");
        SceneManager.LoadScene("Gameplay");
    }

    public void Quit()
    {
        Debug.Log("quit click");
        Application.Quit();
    }

    IEnumerator showcase()
    {
        yield return new WaitForSeconds(1f);
        heroAnim.Play("SpinAttack");
        float pause = Random.Range(1.5f, 2.8f);
        yield return new WaitForSeconds(pause);
        tankerAnim.Play("Attack");
        pause = Random.Range(1.5f, 2.3f);
        yield return new WaitForSeconds(pause);
        soldierAnim.Play("Attack");
        pause = Random.Range(1.8f, 2.8f);
        yield return new WaitForSeconds(pause);
        rangerAnim.Play("Attack");
        pause = Random.Range(1.8f, 2.5f);
        yield return new WaitForSeconds(pause);
        StartCoroutine(showcase());
    }
    
}
