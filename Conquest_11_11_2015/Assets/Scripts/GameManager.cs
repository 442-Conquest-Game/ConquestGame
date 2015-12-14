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
    private GameObject weakest;
    public float spawnWait;
    public GameObject[] player;
    private GameObject target;
    private DisplayScript display;
    
    // Use this for initialization
    void Start () {
        endGameText.text = "";
        restartText.text = "";
        gameOver = false;
        restart = false;
        spawnWait = 2;
        target = GameObject.Find("base_red");
        display = GameObject.Find("Display").GetComponent<DisplayScript>();
        StartCoroutine(Spawn());
	
	}
	
	// Update is called once per frame
	void Update () {
               

        if (gameOver)
        {
            Time.timeScale = 0;
            StopCoroutine(Spawn());
            restartText.text = "Press 'R' to restart the game";
            restart = true;
        }
	    if(restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1;
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
            int min = 0;
            int blueMin = 0;
            if (redCount != 0)
            {
                min = GameObject.Find("base_red").GetComponent<BuildingScript>().TroopNum;
            }
            if (blueCount != 0)
            {
                blueMin = GameObject.Find("base_blue").GetComponent<BuildingScript>().TroopNum;
            }
            BuildingScript script;
            BuildingScript weakScript;
            for (int i = 0; i < blueCount; i++)
            {
                script = AI[i].GetComponent<BuildingScript>();
                if (script.TroopNum > max)
                {
                    max = script.TroopNum;
                    attacker = AI[i];
                }
                if(script.TroopNum <= blueMin)
                {
                    blueMin = script.TroopNum;
                    weakest = AI[i];
                }
            }
            script = attacker.GetComponent<BuildingScript>();
            weakScript = weakest.GetComponent<BuildingScript>();
            script.TroopNum = script.TroopNum / 2;
            if (display.blue_gold >= 50)
            {
                display.blue_gold = display.blue_gold - 50;
                GameObject upgrade = (GameObject)Instantiate(display.upgrade_base_blue, weakest.transform.position, Quaternion.identity);
                upgrade.transform.name = "base_blue";
                BuildingScript upgradeScript = upgrade.GetComponent<BuildingScript>();
                upgradeScript.TroopNum = script.TroopNum;
                upgradeScript.rate = upgradeScript.rate * 0.5f;
                weakScript.destroy_self = true;

                for (int i = 0; i < blueCount; i++)
                {
                    if (upgrade.transform.position == AI[i].transform.position)
                    {
                        AI[i] = upgrade;
                    }
                }
                CastleDatabase data = GameObject.Find("Building Database").GetComponent<CastleDatabase>();
                for (int i = 0; i < 6; i++)
                {
                    if (upgrade.transform.position == data.castles[i].transform.position)
                    {
                        data.castles[i] = upgrade;
                    }
                }
            }
            BuildingScript playerScript;
            for(int i = 0; i < redCount; i++)
            {
                playerScript = player[i].GetComponent<BuildingScript>();
                if(playerScript.TroopNum <= min)
                {
                    min = playerScript.TroopNum;
                    target = player[i];
                }
                if(player[i] == GameObject.FindGameObjectWithTag("Upgrade") && spawnWait % 3 == 0)
                {
                    target = player[i];
                    break;
                }
            }
            UnitScript unit_blue_clone = (UnitScript)Instantiate(unit_blue, attacker.transform.position, Quaternion.identity);
            unit_blue_clone.GetComponentInChildren<TextMesh>().text = unit_blue_clone.UnitCount.ToString();
            unit_blue_clone.UnitCount = script.TroopNum;
            unit_blue_clone.Origin_pos = attacker.transform.position;
            unit_blue_clone.target_pos = target.transform.position;
            unit_blue_clone.transform.name = "unit_blue";
            spawnWait ++;
            if(spawnWait % 5 == 0)
            {
                GameObject redMine = GameObject.FindGameObjectWithTag("Red Gold Mine");
                GameObject blueMine = GameObject.FindGameObjectWithTag("Blue Gold Mine");
                if (attacker.transform.position.x < 0)
                {
                    unit_blue_clone.target_pos = redMine.transform.position;
                }
                else
                {
                    unit_blue_clone.target_pos = blueMine.transform.position;
                }
            }
            if(spawnWait == 15)
            {
                spawnWait = 1;
            }
            yield return new WaitForSeconds(spawnWait);
        }
            
    }
}
