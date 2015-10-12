using UnityEngine;
using System.Collections;

public class DisplayScript : MonoBehaviour {

	public GUISkin resourceSkin, ordersSkin;
	private const int RESOURCE_BOX_HEIGHT = 30, ORDERS_BOX_WIDTH = 100;
	public string objectName, objectName_destination, objectGroup;
	public UnitScript unit_red, unit_blue;
	private Vector3 origin;
	public Vector3 destination;
	private bool attack = false;

	
	// Use this for initialization
	void Start () {
	  
	}

	void Update() {
		if (attack) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit) && Input.GetMouseButtonDown (0)){
				if(hit.transform.parent.parent.name != objectGroup){
					attack = false;
					destination = hit.transform.position;
					objectName_destination = hit.transform.parent.name;
					if(objectGroup == "base_red"){
						Instantiate (unit_red, origin, Quaternion.identity);
					}
					if(objectGroup == "base_blue"){
						Instantiate (unit_blue, origin, Quaternion.identity);
					}
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
	    if (Physics.Raycast (ray, out hit) && Input.GetMouseButtonDown (0) && attack == false) {
			objectName = hit.transform.parent.name;
			objectGroup = hit.transform.parent.parent.name;
			origin = hit.transform.position;
		}
		GUI.Label (new Rect (20, 10, 100, 20), objectName);
		if (objectGroup == "base_red" || objectGroup == "base_blue") {
			if (GUI.Button (new Rect (20, 100, 60, 20), "Attack")){

				attack = true;
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
