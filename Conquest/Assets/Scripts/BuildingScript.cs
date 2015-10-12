using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingScript : MonoBehaviour {

	private float troopSpawnRate = 1f;
	public float TroopNum = 0f;


	// Use this for initialization
	void Start () {
		GetComponentInChildren< TextMesh > ().text = TroopNum.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		TroopNum = TroopNum + (Time.deltaTime * troopSpawnRate);
		GetComponentInChildren< TextMesh > ().text = Mathf.Round (TroopNum).ToString ();
	    
	}
}
