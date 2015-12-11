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
    public int troop_num;
	private Transform attacker;
	public int blue_gold=100, red_gold=110, gold;
	public BuildingScript upgrade_base_red, upgrade_base_blue, base_red, base_blue;
	private GameObject placeBuilding;
	public GameObject place_base_red, place_base_blue;
	public bool isLegalPosition;
	private bool drawOrder=false;
	private float x,y;
	
	// Use this for initialization
	void Start () {
	  
	}

	void Update() {
		if (attack) {
            Attack();
		}
		if (deploy) {
            Deploy();
		}
		if (mine) {
            Mine();
		}
		if (red_build || blue_build) {
			Vector3 a = Input.mousePosition;
			Vector3 b = Camera.main.ScreenToWorldPoint(a);
			placeBuilding.transform.position = new Vector3(b.x,b.y,0);
		}
		if (red_build && Input.GetMouseButtonDown (0) && isLegalPosition) {
			
			red_build = false;
			BuildingScript base_red_clone = (BuildingScript) Instantiate (base_red, new_building_pos, Quaternion.identity);
			base_red_clone.transform.name = "base_red";
			Destroy (placeBuilding);
		}
		if (blue_build && Input.GetMouseButtonDown (0)) {
			
			blue_build = false;
			BuildingScript base_blue_clone = (BuildingScript) Instantiate (base_blue, new_building_pos, Quaternion.identity);
			base_blue_clone.transform.name = "base_blue";
			Destroy (placeBuilding);
		}
	}
	// Update is called once per frame
	void OnGUI () {
		new_building_pos = Input.mousePosition;
		new_building_pos.z = 10;
		new_building_pos = Camera.main.ScreenToWorldPoint (new_building_pos);
		DrawOrderBox ();
		DrawResourceBox ();
	}
	

	private void DrawOrderBox(){

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	    RaycastHit hit;
		if (Physics.Raycast (ray, out hit) && Input.GetMouseButtonDown (0) && attack == false && deploy == false && mine == false && red_build == false && blue_build == false && drawOrder==false) {
			attacker = hit.transform.parent;
			buildingName = hit.transform.parent.name;
			//buildingGroup = hit.transform.parent.parent.name;
			building_origin_pos = hit.transform.position;
			drawOrder = true;
			x = Input.mousePosition.x; 
			y = Screen.height-Input.mousePosition.y;
		}
		if (drawOrder) {
			GUI.skin = ordersSkin;
			GUI.BeginGroup (new Rect (x, y, 100, 160));
			GUI.Box (new Rect (0, 0, 100, 160), "");
			GUI.Label (new Rect (20, 10, 100, 20), buildingName);
			if (buildingName == "base_red") {
				if (GUI.Button (new Rect (0, 30, 100, 20), "Attack")) {
					drawOrder = false;
					attack = true;
				}
				if (GUI.Button (new Rect (0, 50, 100, 20), "Deploy")) {
					drawOrder = false;
					deploy = true;
				}
				if (GUI.Button (new Rect (0, 70, 100, 20), "Mine")) {
					drawOrder = false;
					mine = true;
				}
				if (GUI.Button (new Rect (0, 90, 100, 20), "Upgrade $100")) {
					drawOrder = false;
					if (buildingName == "base_red" && red_gold >= 100) {
						red_gold = red_gold - 100;
						BuildingScript buildingScript = attacker.GetComponent < BuildingScript > ();
						troop_num = buildingScript.TroopNum;
						buildingScript.destroy_self = true;

						BuildingScript base_red_clone = (BuildingScript)Instantiate (upgrade_base_red, building_origin_pos, Quaternion.identity);
						base_red_clone.transform.name = "base_red";
						BuildingScript buildingscript = base_red_clone.GetComponent< BuildingScript > ();
						buildingscript.TroopNum = troop_num;
						buildingscript.rate = buildingscript.rate * 0.5f;
					}
					if (buildingName == "base_blue" && blue_gold >= 100) {
						blue_gold = blue_gold - 100;
						BuildingScript buildingScript = attacker.GetComponent < BuildingScript > ();
						troop_num = buildingScript.TroopNum;
						buildingScript.destroy_self = true;

						BuildingScript base_blue_clone = (BuildingScript)Instantiate (upgrade_base_blue, building_origin_pos, Quaternion.identity);
						base_blue_clone.transform.name = "base_blue";
						BuildingScript buildingscript = base_blue_clone.GetComponent< BuildingScript > ();
						buildingscript.TroopNum = troop_num;
						buildingscript.rate = buildingscript.rate * 0.5f;
					}
				}
				if (GUI.Button (new Rect (0, 110, 100, 20), "Build $100")) {
					drawOrder = false;
					if (buildingName == "base_red" && red_gold >= 100) {
						red_build = true;
						red_gold = red_gold - 100;
						placeBuilding = Instantiate (place_base_red);
					}
					if (buildingName == "base_blue" && blue_gold >= 100) {
						blue_build = true;
						blue_gold = blue_gold - 100;
						placeBuilding = Instantiate (place_base_blue);
					}
				
				}
				if (GUI.Button (new Rect (0, 130, 100, 20), "Cancel")) {
					drawOrder = false;
				}
			} 
			else {
				if (GUI.Button (new Rect (0, 40, 100, 20), "Cancel")) {
					drawOrder = false;
				}
			}
			GUI.EndGroup ();
		}
	}
    private void Attack()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            if (hit.transform.parent.name != buildingName)
            {
                attack = false;
                instantiate_unit = true;
                building_destination_pos = hit.transform.position;
                buildingName_destination = hit.transform.parent.name;
                BuildingScript buildingScript = attacker.GetComponent<BuildingScript>();
                troop_num = buildingScript.TroopNum;
                if (troop_num > 1)
                {
                    buildingScript.TroopNum = troop_num - troop_num / 2;
                }
                else
                {
                    instantiate_unit = false;
                }
            }
        }

    }
    private void Deploy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            if (hit.transform.parent.name == buildingName && hit.transform.position != building_origin_pos)
            {
                deploy = false;
                instantiate_unit = true;
                building_destination_pos = hit.transform.position;
                buildingName_destination = hit.transform.parent.name;
                BuildingScript buildingScript = attacker.GetComponent<BuildingScript>();
                troop_num = buildingScript.TroopNum;
                if (troop_num > 1)
                {
                    buildingScript.TroopNum = troop_num - troop_num / 2;
                }
                else
                {
                    instantiate_unit = false;
                }
            }
        }
    }
    private void Mine()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            if (hit.transform.parent.name == "gold_mine")
            {
                mine = false;
                instantiate_unit = true;
                building_destination_pos = hit.transform.position;
                BuildingScript buildingScript = attacker.GetComponent<BuildingScript>();
                troop_num = buildingScript.TroopNum;
                if (troop_num > 1)
                {
                    buildingScript.TroopNum = troop_num - troop_num / 2;
                    
                }
                else
                {
                    instantiate_unit = false;
                }
            }
        }
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
