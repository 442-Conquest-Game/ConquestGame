using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour {

	private Vector3 Destination_pos;
	private Vector3 Origin_pos;
	public float UnitCount, EnemyCount, troopNum;
	public BuildingScript base_red, base_blue;
	public GameObject base_red_parent, base_blue_parent;
    private GameManager gameManager;
    
   
	// Use this for initialization
	void Start () {
		GameObject display = GameObject.Find ("Display");
		DisplayScript displayScript = display.GetComponent < DisplayScript > ();
		Destination_pos = displayScript.building_destination_pos;
		Destination_pos.z = 0;
		Origin_pos = displayScript.building_origin_pos;
		UnitCount = displayScript.unit_count;
		GetComponentInChildren < TextMesh > ().text = Mathf.Round (UnitCount).ToString ();
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameController.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, Destination_pos, Time.deltaTime * 2);
		GetComponentInChildren < TextMesh > ().text = Mathf.Round (UnitCount).ToString ();
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("TRIGGER");

		if (transform.name == "unit_red") {
			if (other.gameObject.transform.name == "base_blue") {
				BuildingScript buildingScript = other.gameObject.GetComponent< BuildingScript >();
				troopNum = buildingScript.TroopNum;
				buildingScript.TroopNum = troopNum - UnitCount;
				if(troopNum-UnitCount < 0){
                    GameObject display = GameObject.Find ("Display");
					DisplayScript displayScript = display.GetComponent < DisplayScript > ();
                    Vector3 building_pos = other.gameObject.transform.position;
                    BuildingScript base_red_clone = (BuildingScript)Instantiate(base_red, building_pos, Quaternion.identity);
					base_red_clone.transform.name = "base_red";
					BuildingScript NewBuildingScript = base_red_clone.GetComponent < BuildingScript >();
                    NewBuildingScript.TroopNum = -(troopNum-UnitCount);
                    gameManager.redCount++;
                    gameManager.blueCount--;
                    if(gameManager.blueCount == 0)
                    {
                        gameManager.endGameText.text = "Red Wins!";
                        gameManager.gameOver = true;
                    }
                }
				Destroy (gameObject);
                
			}
			if(other.gameObject.transform.name == "base_red" && other.gameObject.transform.position != Origin_pos){
				BuildingScript buildingScript = other.gameObject.GetComponent< BuildingScript >();
				troopNum = buildingScript.TroopNum;
				buildingScript.TroopNum = troopNum + UnitCount;
				Destroy (gameObject);
			}
			if(other.gameObject.transform.name == "unit_blue"){
				UnitScript unit_script = other.gameObject.GetComponent < UnitScript >();
				EnemyCount = unit_script.UnitCount;
				unit_script.UnitCount = EnemyCount-UnitCount;
				UnitCount = UnitCount - EnemyCount;
			}
		}
		if (transform.name == "unit_blue") {
			Debug.Log ("test1");
			if(other.gameObject.transform.name == "base_red"){
				Debug.Log ("test2");
				BuildingScript buildingScript = other.gameObject.GetComponent< BuildingScript >();
				troopNum = buildingScript.TroopNum;
				buildingScript.TroopNum = troopNum - UnitCount;
				if(troopNum-UnitCount < 0){
					GameObject display = GameObject.Find ("Display");
					DisplayScript displayScript = display.GetComponent < DisplayScript > ();
                    Vector3 building_pos = other.gameObject.transform.position;
                   	BuildingScript base_blue_clone = (BuildingScript) Instantiate (base_blue, building_pos, Quaternion.identity);
					base_blue_clone.transform.name = "base_blue";
//					base_blue_clone.transform.parent = base_blue_parent.transform;
//					base_blue_clone.transform.parent.name = "base_blue";
					BuildingScript NewBuildingScript = base_blue_clone.GetComponent < BuildingScript >();
					NewBuildingScript.TroopNum = -(troopNum-UnitCount);
                    gameManager.redCount--;
                    gameManager.blueCount++;
                    if(gameManager.redCount == 0)
                    {
                        gameManager.endGameText.text = "Blue Wins!";
                        gameManager.gameOver = true;
                    }
                }
				Destroy (gameObject);
                
			}
			if(other.gameObject.transform.name == "base_blue" && other.gameObject.transform.position != Origin_pos){
				BuildingScript buildingScript = other.gameObject.GetComponent< BuildingScript >();
				troopNum = buildingScript.TroopNum;
				buildingScript.TroopNum = troopNum + UnitCount;
				Destroy (gameObject);
			}
			if(other.gameObject.transform.name == "unit_red"){
				UnitScript unit_script = other.gameObject.GetComponent < UnitScript >();
				EnemyCount = unit_script.UnitCount;
				unit_script.UnitCount = EnemyCount-UnitCount;
				UnitCount = UnitCount - EnemyCount;
			}
		}
		if (UnitCount < 0) {
			Destroy (gameObject);
		}
		GetComponentInChildren < TextMesh > ().text = Mathf.Round (UnitCount).ToString ();
	}
}
