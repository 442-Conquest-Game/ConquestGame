using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectScreenScript : MonoBehaviour {

    public Canvas levelSelectMenu;
    public Button menuButton;

	// Use this for initialization
	void Start ()
    {
        levelSelectMenu = levelSelectMenu.GetComponent<Canvas>();
        menuButton = menuButton.GetComponent<Button>();	
	}

    public void MenuButtonPress()
    {
        Application.LoadLevel(0);
    }
}
