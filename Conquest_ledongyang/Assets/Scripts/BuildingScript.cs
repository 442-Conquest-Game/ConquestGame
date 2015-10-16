using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingScript : MonoBehaviour {

	private float troopSpawnRate = 1f;
	public float TroopNum = 0f;
	public UnitScript unit_red, unit_blue;
	private Vector3 instantiate_pos;


	// Use this for initialization
	void Start () {
		GetComponentInChildren< TextMesh > ().text = TroopNum.ToString ();

	}
	
	// Update is called once per frame
	void Update () {
		TroopNum = TroopNum + (Time.deltaTime * troopSpawnRate);
		GameObject display = GameObject.Find ("Display");
		DisplayScript displayScript = display.GetComponent< DisplayScript > ();
		instantiate_pos = displayScript.building_origin_pos;


		//check if Instantiate unit from building
		if (displayScript.instantiate_unit) {

			if(displayScript.buildingName == "base_red"){
				Debug.Log ("red");
				UnitScript unit_red_clone = (UnitScript)Instantiate (unit_red, instantiate_pos, Quaternion.identity);
				unit_red_clone.transform.name = "unit_red";
				displayScript.instantiate_unit = false;

			}
			else if(displayScript.buildingName == "base_blue"){
				Debug.Log ("blue");
				UnitScript unit_blue_clone = (UnitScript)Instantiate (unit_blue, instantiate_pos, Quaternion.identity);
				unit_blue_clone.transform.name = "unit_blue";
				displayScript.instantiate_unit = false;

			}

		}

		GetComponentInChildren< TextMesh > ().text = Mathf.Round (TroopNum).ToString ();
	    
		if (TroopNum < 0) {
			Destroy (gameObject);
		}
	}


}
