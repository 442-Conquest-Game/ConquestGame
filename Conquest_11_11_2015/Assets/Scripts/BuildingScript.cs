﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingScript : MonoBehaviour {


	public int TroopNum = 0;
	public float rate = 2;
	private float time;
    public UnitScript unit_red;
    public int Unit_count;
	private Vector3 instantiate_pos;
	public bool destroy_self = false;
    public GameManager game;


	// Use this for initialization
	void Start () {
		GetComponentInChildren< TextMesh > ().text = TroopNum.ToString ();
		time = Time.time;
        
	}
	
	// Update is called once per frame
	void Update () {
		if (destroy_self) {
			Destroy (gameObject);
		}
		if (Time.time - time >= rate) {
			TroopNum++;
			time = Time.time;
		}
		
		instantiate_pos.z = 0;
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
<<<<<<< HEAD


		     
=======
		//check if Instantiate unit from building
		if (DisplayScript.instantiate_unit) {

			if(DisplayScript.buildingName == "base_red"){
				UnitScript unit_red_clone = (UnitScript)Instantiate (unit_red, instantiate_pos, Quaternion.identity);
                unit_red_clone.UnitCount = TroopNum/2;
				unit_red_clone.transform.name = "unit_red";
				DisplayScript.instantiate_unit = false;

			}

		}

>>>>>>> b865957327431b6e2bce3e79f91fd1a4f40ed344
		GetComponentInChildren< TextMesh > ().text = TroopNum.ToString ();
	    
        }
	}
   


