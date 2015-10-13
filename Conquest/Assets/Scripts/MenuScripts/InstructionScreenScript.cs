using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class InstructionScreenScript : MonoBehaviour {

    public Canvas instructionMenu;
    public Button menuButton;


	// Use this for initialization
	void Start ()
    {
        instructionMenu = instructionMenu.GetComponent<Canvas>();
        menuButton = menuButton.GetComponent<Button>();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MenuButtonPress()
    {
        Application.LoadLevel(0);
    }
}
