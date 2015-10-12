using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour {

	private Vector3 Destination;
	private float troopNum, troopNum_des;
	private string ObjectName, ObjectName_Destination;
	// Use this for initialization
	void Start () {
		GameObject display = GameObject.Find ("Display");
		DisplayScript displayScript = display.GetComponent < DisplayScript > ();
		Destination = displayScript.destination;
		ObjectName = displayScript.objectName;
		ObjectName_Destination = displayScript.objectName_destination;
		GameObject building = GameObject.Find (ObjectName);
		BuildingScript buildingScript = building.GetComponent < BuildingScript > ();
		troopNum = buildingScript.TroopNum;
		buildingScript.TroopNum = troopNum / 2;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponentInChildren < TextMesh > ().text = Mathf.Round (troopNum / 2).ToString ();
		transform.position = Vector3.MoveTowards (transform.position, Destination, Time.deltaTime * 5);
		if (transform.position == Destination) {
			Destroy (transform.gameObject);
			GameObject building_des = GameObject.Find (ObjectName_Destination);
			BuildingScript buildingScript_des = building_des.GetComponent < BuildingScript >();
			troopNum_des = buildingScript_des.TroopNum;
			buildingScript_des.TroopNum = troopNum_des - Mathf.Round (troopNum/2);

		}


	}
}
