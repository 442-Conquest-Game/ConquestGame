using UnityEngine;
using System.Collections;

public class DisplayScript : MonoBehaviour {

	public GUISkin resourceSkin, ordersSkin;
	private const int RESOURCE_BOX_HEIGHT = 30, ORDERS_BOX_WIDTH = 100;
	public string buildingName, buildingName_destination, buildingGroup;
	public Vector3 building_origin_pos;
	public Vector3 building_destination_pos;
	private bool attack = false;
    public bool reinforce = false;
	public bool instantiate_unit = false;
	public float troop_num, unit_count;
    private Transform attacker;
   
   

	
	// Use this for initialization
	void Start () {
	  
	}

	void Update() {
		if (attack) {
            //If we're attacking, make a ray where the mouse is pointing.
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
					buildingScript.TroopNum = troop_num/2;
					unit_count = troop_num/2;
				}
			}

		}
        if (reinforce)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast (ray, out hit) && Input.GetMouseButtonDown(0))
            {
                if(hit.transform.parent.name == buildingName)
                {
                    reinforce = false;
                    instantiate_unit = true;
                    building_destination_pos = hit.transform.position;
                    buildingName_destination = hit.transform.parent.name;
                    BuildingScript buildingScript = attacker.GetComponent<BuildingScript>();
                    troop_num = buildingScript.TroopNum;
                    buildingScript.TroopNum = troop_num / 2;
                    unit_count = troop_num / 2;
                }
            }
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
	    if (Physics.Raycast (ray, out hit) && Input.GetMouseButtonDown (0) && attack == false && reinforce == false) {
			attacker = hit.transform.parent;
			buildingName = hit.transform.parent.name;
			building_origin_pos = hit.transform.position;
		}
		GUI.Label (new Rect (20, 10, 100, 20), buildingName);
       	if (buildingName == "base_red" || buildingName == "base_blue") {
			if (GUI.Button (new Rect (20, 60, 60, 20), "Attack")){

				attack = true;
			}
            if(GUI.Button (new Rect(20, 100, 60, 20), "Reinforce"))
            {
                reinforce = true;
            }
		}
		GUI.EndGroup ();

	}



	private void DrawResourceBox(){
		GUI.skin = resourceSkin;
		GUI.BeginGroup (new Rect (0, 0, Screen.width, RESOURCE_BOX_HEIGHT));
		GUI.Box (new Rect (0, 0, Screen.width, RESOURCE_BOX_HEIGHT), "");
		GUI.EndGroup ();
	}
}
