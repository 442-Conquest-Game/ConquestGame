using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour {

	private Vector3 Destination_pos;
	public Vector3 Origin_pos;
	private Vector3 new_building_pos;
    public Vector3 target_pos;
	public int UnitCount, unitCount, EnemyCount, troopNum;
	public BuildingScript base_red, base_blue;
	public bool go_back=false;
	public int gold;
    private int j =0;
    
    public GameManager managerScript;
    private CastleDatabase data;
	//public GameObject base_red_parent, base_blue_parent;
	//private string ObjectName, ObjectName_Destination;
	// Use this for initialization
	void Start () {
		GameObject display = GameObject.Find ("Display");
		DisplayScript displayScript = display.GetComponent < DisplayScript > ();
        GameObject gameManager = GameObject.Find("GameManager");
        managerScript = gameManager.GetComponent<GameManager>();
		Destination_pos = displayScript.building_destination_pos;
		Destination_pos.z = 0;
        data = GameObject.Find("Building Database").GetComponent<CastleDatabase>();
		Origin_pos = displayScript.building_origin_pos;
		Origin_pos.z = 0;
		GetComponentInChildren < TextMesh > ().text = UnitCount.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
        
		if (!go_back) {
            if (transform.name == "unit_red")
            {
                transform.position = Vector3.MoveTowards(transform.position, Destination_pos, Time.deltaTime * 2);
            }
            if(transform.name == "unit_blue")
            {
                transform.position = Vector3.MoveTowards(transform.position, target_pos, Time.deltaTime * 2);
            }
            
                
            GetComponentInChildren < TextMesh > ().text = UnitCount.ToString ();
		}
		if (go_back) {
			transform.position = Vector3.MoveTowards (transform.position, Origin_pos, Time.deltaTime * 2);
			GetComponentInChildren < TextMesh > ().text = UnitCount.ToString () + " I have gold $10!";
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (transform.name == "unit_red") {
			if (other.gameObject.transform.name == "base_blue") {
				BuildingScript buildingScript = other.gameObject.GetComponent< BuildingScript >();
				troopNum = buildingScript.TroopNum;
				buildingScript.TroopNum = troopNum - UnitCount;
				if(troopNum-UnitCount < 0){
					new_building_pos = other.gameObject.transform.position;
					Destroy (other.gameObject);
                    managerScript.redCount++;
                    managerScript.blueCount--;
                    if(managerScript.blueCount == 0)
                    {
                        managerScript.endGameText.text = "Red Wins!";
                        managerScript.gameOver = true;
                    }
					GameObject display = GameObject.Find ("Display");
					DisplayScript displayScript = display.GetComponent < DisplayScript > ();
					Destination_pos = displayScript.building_destination_pos;

					BuildingScript base_red_clone = (BuildingScript) Instantiate (base_red, new_building_pos, Quaternion.identity);
					base_red_clone.transform.name = "base_red";
//					base_red_clone.transform.parent = base_red_parent.transform;
//					base_red_clone.transform.parent.name = "base_red";
					BuildingScript NewBuildingScript = base_red_clone.GetComponent < BuildingScript >();
					NewBuildingScript.TroopNum = -(troopNum-UnitCount);
				}
				Destroy (gameObject);
			}
           
			if(other.gameObject.transform.name == "base_red" && other.transform.position != Origin_pos) {
				BuildingScript buildingScript = other.gameObject.GetComponent< BuildingScript >();
				troopNum = buildingScript.TroopNum;
				buildingScript.TroopNum = troopNum + UnitCount;
                Debug.Log("Entered");
				Destroy (gameObject);
			}
			if(other.gameObject.transform.name == "base_red" && go_back==true){
				go_back = false;
				BuildingScript buildingScript = other.gameObject.GetComponent< BuildingScript >();
				troopNum = buildingScript.TroopNum;
				buildingScript.TroopNum = troopNum + UnitCount;
				if(gold>0){
					GameObject display = GameObject.Find ("Display");
					DisplayScript displayScript = display.GetComponent < DisplayScript > ();
					displayScript.red_gold = displayScript.red_gold+gold;
				}
				Destroy (gameObject);
			}
			if(other.gameObject.transform.name == "unit_blue"){
				UnitScript unit_script = other.gameObject.GetComponent < UnitScript >();
				EnemyCount = unit_script.UnitCount;
                Debug.Log(EnemyCount);
				if(UnitCount > EnemyCount){
					UnitCount = UnitCount - EnemyCount;
				}
				else if(UnitCount <= EnemyCount){
					Destroy (gameObject);
				}
			}
			if(other.gameObject.transform.name == "gold_mine"){

				go_back = true;
				gold = gold+10;
			}
		}
		if (transform.name == "unit_blue") {
			if(other.gameObject.transform.name == "base_red"){
				BuildingScript buildingScript = other.gameObject.GetComponent< BuildingScript >();
				troopNum = buildingScript.TroopNum;
				buildingScript.TroopNum = troopNum - UnitCount;
				if(troopNum-UnitCount < 0){
					new_building_pos = other.gameObject.transform.position;
					Destroy (other.gameObject);
                   
					GameObject display = GameObject.Find ("Display");
					DisplayScript displayScript = display.GetComponent < DisplayScript > ();
					Destination_pos = displayScript.building_destination_pos;

					BuildingScript base_blue_clone = (BuildingScript)Instantiate(base_blue, new_building_pos, Quaternion.identity);
					base_blue_clone.transform.name = "base_blue";
//					base_blue_clone.transform.parent = base_blue_parent.transform;
//					base_blue_clone.transform.parent.name = "base_blue";
					BuildingScript NewBuildingScript = base_blue_clone.GetComponent < BuildingScript >();
					NewBuildingScript.TroopNum = -(troopNum-UnitCount);
                    
                    managerScript.redCount--;
                    managerScript.blueCount++;
                    managerScript.AI = new GameObject[managerScript.blueCount];
                    j = 0;
                    while (j != managerScript.blueCount)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            if (data.castles[i].transform.name == "base_blue")
                            {
                                managerScript.AI[j] = data.castles[i];
                                j++;
                            }
                        }
                    }
                    Debug.Log("exited loop");
                    if (managerScript.redCount == 0)
                    {
                        managerScript.endGameText.text = "Blue Wins!";
                        managerScript.gameOver = true;
                    }
                }
				Destroy (gameObject);
               
			}
		//	if(other.gameObject.transform.name == "base_blue" && other.transform.position.x != Origin_pos.x && other.transform.position.y != Origin_pos.y) {
			//	BuildingScript buildingScript = other.gameObject.GetComponent< BuildingScript >();
				//troopNum = buildingScript.TroopNum;
			//	buildingScript.TroopNum = troopNum + UnitCount;
				
		//		Destroy (gameObject);
	//		}
			if(other.gameObject.transform.name == "base_blue" && go_back==true){
				go_back = false;
				BuildingScript buildingScript = other.gameObject.GetComponent< BuildingScript >();
				troopNum = buildingScript.TroopNum;
				buildingScript.TroopNum = troopNum + UnitCount;
				if(gold>0){
					GameObject display = GameObject.Find ("Display");
					DisplayScript displayScript = display.GetComponent < DisplayScript > ();
					displayScript.blue_gold = displayScript.blue_gold+gold;
				}
				Destroy (gameObject);
			}
			if(other.gameObject.transform.name == "unit_red"){
				UnitScript unit_script = other.gameObject.GetComponent < UnitScript >();
				EnemyCount = unit_script.UnitCount;
				if(UnitCount > EnemyCount){
					UnitCount = UnitCount - EnemyCount;
				}
				else if(UnitCount <= EnemyCount){
					Destroy (gameObject);
				}
			}
			if(other.gameObject.transform.name == "gold_mine"){

				go_back = true;
				gold = gold+10;
			}
		}
		
		GetComponentInChildren < TextMesh > ().text = UnitCount.ToString ();
	}
}
