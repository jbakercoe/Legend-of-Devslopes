using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour {
    
    [SerializeField] private Image instructionMenu;
    [SerializeField] private Text buttonText;
    
    private Object[] menuHeadings;

    private Animator anim;
    private Animator textAnim;
    private int instructionIndex = 0;

	// Use this for initialization
	void Start () {
        anim = instructionMenu.GetComponent<Animator>();
	}

    public void OpenInstructions()
    {
        instructionIndex = 0;
        instructionMenu.transform.GetChild(1).gameObject.SetActive(false);
        instructionMenu.transform.GetChild(0).gameObject.SetActive(true);
        buttonText.text = "Next";
        anim.SetTrigger("Open");
    }

    public void CloseInstructions()
    {
        anim.SetTrigger("Close");
    }

    public void SwitchInstructions()
    {
        if(instructionIndex == 0)
        {
            instructionMenu.transform.GetChild(instructionIndex).gameObject.SetActive(false);
            instructionIndex++;
            instructionMenu.transform.GetChild(instructionIndex).gameObject.SetActive(true);
            buttonText.text = "Done";
            
        } else
        {
            CloseInstructions();
        }
    }

}
