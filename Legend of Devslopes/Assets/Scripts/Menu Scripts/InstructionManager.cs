using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour {
    
    [SerializeField] private Image instructionMenu;
    [SerializeField] private GameObject walker;

    private class menuSection
    {



        public menuSection()
        {

        }
    }

    private Object[] menuHeadings;

    private Animator anim;
    private Animator walkerAnim;

	// Use this for initialization
	void Start () {
        anim = instructionMenu.GetComponent<Animator>();
        walkerAnim = walker.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenInstructions()
    {
        //instructionMenu.gameObject.SetActive(true);
        anim.SetBool("isOpen", true);
        walkerAnim.Play("Walk");
        StartCoroutine(demoTurn());
    }

    private void initializeMenuHeadings()
    {

    }

    IEnumerator demoTurn()
    {
        yield return new WaitForSeconds(1f);
        walker.transform.Rotate(new Vector3(0f, 90f, 0f));
        StartCoroutine(demoTurn());
    }

}
