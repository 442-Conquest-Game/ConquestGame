using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    public Canvas quitMenu;
    public Button play;
    public Button instructions;
    public Button levelSelect;
    public Button exit;
    public Button yes;
    public Button no;


	// Use this for initialization
	void Start ()
    {
        quitMenu = quitMenu.GetComponent<Canvas>();
        quitMenu.enabled = false;

        play = play.GetComponent<Button>();
        instructions = instructions.GetComponent<Button>();
        levelSelect = levelSelect.GetComponent<Button>();
        exit = exit.GetComponent<Button>();
        
	
	}

    public void ExitPress()
    {
        quitMenu.enabled = true;
        play.enabled = false;
        instructions.enabled = false;
        levelSelect.enabled = false;
        exit.enabled = false;
    }

    public void NoPress()
    {
        quitMenu.enabled = false;
        play.enabled = true;
        instructions.enabled = true;
        levelSelect.enabled = true;
        exit.enabled = true;
    }

    public void StartLevel()
    {
        Application.LoadLevel(3);
    }

    public void InstructionSelect()
    {
        Application.LoadLevel(1);
    }

    public void LevelSelect()
    {
        Application.LoadLevel(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
