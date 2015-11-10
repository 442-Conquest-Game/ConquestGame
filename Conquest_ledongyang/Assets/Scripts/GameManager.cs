using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int redCount = 4;
    public int blueCount = 4;
    public GUIText endGameText;
    public GUIText restartText;
    public bool gameOver;
    private bool restart;
    // Use this for initialization
    void Start () {
        endGameText.text = "";
        restartText.text = "";
        gameOver = false;
        restart = false;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (gameOver)
        {
            restartText.text = "Press 'R' to restart the game";
            restart = true;
        }
	    if(restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
	}
}
