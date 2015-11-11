using UnityEngine;
using System.Collections;

public class DisplayScript : MonoBehaviour {

	public GUISkin resourceSkin, ordersSkin;
	private const int RESOURCE_BOX_HEIGHT = 30, ORDERS_BOX_WIDTH = 100;
	public string buildingName, buildingName_destination, buildingGroup;
	public Vector3 building_origin_pos;
	public Vector3 building_destination_pos;
	public Vector3 destination_pos;
	private Vector3 new_building_pos;
	public bool attack = false, deploy = false, mine = false, red_build= false, blue_build=false;
	public bool instantiate_unit = false;
	public int troop_num, unit_count;
	private Transform attacker;
	public int blue_gold=100, red_gold=110, gold;
	public BuildingScript upgrade_base_red, upgrade_base_blue, base_red, base_blue;

	
	// Use this for initialization
	void Start () {
	  
	}

	void Update() {
		if (attack) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit) && Input.GetMouseButtonDown (0)){
				if(hit.transform.parent.name != buildingName){
					attack = false;
					instantiate_unit = true;
					building_destination_pos = hit.transform.position;
					buildingName_destination = hit.transform.parent.name;
					BuildingScript buildingScript = attacker.GetComponent < BuildingScript >();
					troop_num = buildingScript.TroopNum;
					if(troop_num > 1){
					    buildingScript.TroopNum = troop_num - troop_num/2;
						unit_count = troop_num/2;
					}
					else{
						instantiate_unit = false;
					}
				}
			}

		}
		if (deploy) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Input.GetMouseButtonDown (0) && Physics.Raycast (ray, out hit)){
				if(hit.transform.parent.name == buildingName && hit.transform.position != building_origin_pos){
					deploy = false;
				    instantiate_unit = true;
				    building_destination_pos = hit.transform.position; 
				    buildingName_destination = hit.transform.parent.name;
				    BuildingScript buildingScript = attacker.GetComponent < BuildingScript >();
				    troop_num = buildingScript.TroopNum;
				    if(troop_num > 1){
					    buildingScript.TroopNum = troop_num - troop_num/2;
					    unit_count = troop_num/2;
				    }
				    else{
					    instantiate_unit = false;
				    }
			   }
			}
		}
		if (mine) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Input.GetMouseButtonDown (0) && Physics.Raycast (ray, out hit)){
				if(hit.transform.parent.name == "gold_mine"){
					mine = false;
					instantiate_unit = true;
					building_destination_pos = hit.transform.position;
					BuildingScript buildingScript = attacker.GetComponent < BuildingScript >();
					troop_num = buildingScript.TroopNum;
					if(troop_num > 1){
						buildingScript.TroopNum = troop_num - troop_num/2;
						unit_count = troop_num/2;
					}
					else{
						instantiate_unit = false;
					}
				}
			}
		}
		if (red_build && Input.GetMouseButtonDown (0)) {
			Debug.Log (Input.mousePosition);
			red_build = false;
			new_building_pos = Input.mousePosition;
			new_building_pos.z = 0;
			BuildingScript base_red_clone = (BuildingScript) Instantiate (base_red, new_building_pos, Quaternion.identity);
			base_red_clone.transform.name = "base_red";
		}
		if (blue_build && Input.GetMouseButtonDown (0)) {
			Debug.Log (Input.mousePosition);
			blue_build = false;
			new_building_pos = Input.mousePosition;
			new_building_pos.z = 0;
			BuildingScript base_blue_clone = (BuildingScript) Instantiate (base_blue, new_building_pos, Quaternion.identity);
			base_blue_clone.transform.name = "base_blue";
		}
	}
	// Update is called once per frame
	void OnGUI () {
		DrawOrderBox ();
		DrawResourceBox ();
	}
	

	private void DrawOrderBox(){
		GUI.skin = ordersSkin;
		GUI.BeginGroup (new Rect (Screen.width - ORDERS_BOX_WIDTH, RESOURCE_BOX_HEIGHT, ORDERS_BOX_WIDTH, Screen.height - RESOURCE_BOX_HEIGHT));
		GUI.Box (new Rect(0,0,ORDERS_BOX_WIDTH, Screen.height - RESOURCE_BOX_HEIGHT),"");
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	    RaycastHit hit;
		if (Physics.Raycast (ray, out hit) && Input.GetMouseButtonDown (0) && attack == false && deploy == false && mine == false) {
			attacker = hit.transform.parent;
			buildingName = hit.transform.parent.name;
			//buildingGroup = hit.transform.parent.parent.name;
			building_origin_pos = hit.transform.position;
		}
		GUI.Label (new Rect (20, 10, 100, 20), buildingName);
		if (buildingName == "base_red" || buildingName == "base_blue") {
			if (GUI.Button (new Rect (20, 40, 60, 20), "Attack")){

				attack = true;
			}
			if (GUI.Button (new Rect (20,80,60,20), "Deploy")){
				deploy = true;
			}
			if (GUI.Button (new Rect (20,120,60,20), "Mine")){
				mine = true;
			}
			if (GUI.Button (new Rect (0, 160, 100, 20), "Upgrade $100")){
				if(buildingName == "base_red" && red_gold >= 100){
					red_gold = red_gold - 100;
					BuildingScript buildingScript = attacker.GetComponent < BuildingScript >();
					troop_num = buildingScript.TroopNum;
					buildingScript.destroy_self = true;

					BuildingScript base_red_clone = (BuildingScript) Instantiate (upgrade_base_red, building_origin_pos, Quaternion.identity);
					base_red_clone.transform.name = "base_red";
					BuildingScript buildingscript = base_red_clone.GetComponent< BuildingScript >();
					buildingscript.TroopNum = troop_num;
					buildingscript.rate = buildingscript.rate*0.5f;
				}
				if(buildingName == "base_blue" && blue_gold >= 100){
					blue_gold = blue_gold - 100;
					BuildingScript buildingScript = attacker.GetComponent < BuildingScript >();
					troop_num = buildingScript.TroopNum;
					buildingScript.destroy_self = true;

					BuildingScript base_blue_clone = (BuildingScript) Instantiate (upgrade_base_blue, building_origin_pos, Quaternion.identity);
					base_blue_clone.transform.name = "base_blue";
					BuildingScript buildingscript = base_blue_clone.GetComponent< BuildingScript >();
					buildingscript.TroopNum = troop_num;
					buildingscript.rate = buildingscript.rate*0.5f;
				}
			}
			if (GUI.Button (new Rect (0, 200, 100, 20), "Build $100")){
				if(buildingName == "base_red" && red_gold >= 100){
					red_build = true;
					red_gold = red_gold - 100;
				}
				if(buildingName == "base_blue" && blue_gold >= 100){
					blue_build = true;
					blue_gold = blue_gold - 100;
				}
				
			}
		}
		GUI.EndGroup ();

	}



	private void DrawResourceBox(){
		GUI.skin = resourceSkin;
		GUI.BeginGroup (new Rect (0, 0, Screen.width, RESOURCE_BOX_HEIGHT));
		GUI.Box (new Rect (0, 0, Screen.width, RESOURCE_BOX_HEIGHT), "");
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit) && Input.GetMouseButtonDown (0) && attack == false) {
			if(hit.transform.parent.name == "base_red"){
				gold = red_gold;
			}
			else if(hit.transform.parent.name == "base_blue"){
				gold = blue_gold;
			}
		}

		GUI.Label (new Rect (10, 0, 40, 100), "Gold:");
		GUI.Label (new Rect (50, 0, 100, 100), gold.ToString());
		GUI.EndGroup ();
	}
}
