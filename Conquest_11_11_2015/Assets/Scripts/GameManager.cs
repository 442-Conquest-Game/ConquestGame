using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int redCount = 3;
    public int blueCount = 3;
    public GUIText endGameText;
    public GUIText restartText;
    public bool gameOver;
    private bool restart;
    public GameObject[] AI;
    public UnitScript unit_blue;
    private GameObject attacker;
    public float spawnWait;
    public GameObject[] player;
    private GameObject target;
    
    // Use this for initialization
    void Start () {
        endGameText.text = "";
        restartText.text = "";
        gameOver = false;
        restart = false;
        spawnWait = 2;
        target = GameObject.Find("base_red");
        StartCoroutine(Spawn());
	
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
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(10);
        while (true)
        {
            int max = 0;
            int min = GameObject.Find("base_red").GetComponent<BuildingScript>().TroopNum;
            BuildingScript script;
            for (int i = 0; i < blueCount; i++)
            {
                script = AI[i].GetComponent<BuildingScript>();
                if (script.TroopNum > max)
                {
                    max = script.TroopNum;
                    attacker = AI[i];
                }
            }
            script = attacker.GetComponent<BuildingScript>();
            script.TroopNum = script.TroopNum / 2;
            BuildingScript playerScript;
            for(int i = 0; i < redCount; i++)
            {
                playerScript = player[i].GetComponent<BuildingScript>();
                if(playerScript.TroopNum <= min)
                {
                    min = playerScript.TroopNum;
                    target = player[i];
                }
            }
            UnitScript unit_blue_clone = (UnitScript)Instantiate(unit_blue, attacker.transform.position, Quaternion.identity);
            unit_blue_clone.GetComponentInChildren<TextMesh>().text = unit_blue_clone.UnitCount.ToString();
            unit_blue_clone.UnitCount = script.TroopNum;
            unit_blue_clone.Origin_pos = attacker.transform.position;
            unit_blue_clone.target_pos = target.transform.position;
            unit_blue_clone.transform.name = "unit_blue";
            spawnWait ++;
            if(spawnWait == 10)
            {
                spawnWait = 1;
            }
            yield return new WaitForSeconds(spawnWait);
        }
            
    }
}
