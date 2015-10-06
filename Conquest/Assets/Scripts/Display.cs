using UnityEngine;
using System.Collections;

public class Display : MonoBehaviour {

	public GUISkin resourceSkin, ordersSkin;
	private const int RESOURCE_BOX_HEIGHT = 30, ORDERS_BOX_WIDTH = 100;
	private string objectName;


	// Use this for initialization
	void Start () {
	
	}

	void Update() {

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
	    if (Physics.Raycast (ray, out hit) && Input.GetMouseButtonDown (0))
			objectName = hit.transform.parent.name;
		GUI.Label (new Rect (20, 10, 100, 20), objectName);
		GUI.EndGroup ();

	}



	private void DrawResourceBox(){
		GUI.skin = resourceSkin;
		GUI.BeginGroup (new Rect (0, 0, Screen.width, RESOURCE_BOX_HEIGHT));
		GUI.Box (new Rect (0, 0, Screen.width, RESOURCE_BOX_HEIGHT), "");
		GUI.EndGroup ();
	}
}
