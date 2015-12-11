using UnityEngine;
using System.Collections;

public class PlaceBuildingScript : MonoBehaviour {

	int count;
	// Use this for initialization
	void Start () {
		count = 0;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other){
		count++;
		GameObject display = GameObject.Find ("Display");
		DisplayScript displayScript = display.GetComponent < DisplayScript > ();
		displayScript.isLegalPosition = false;

		this.GetComponent<SpriteRenderer> ().color = new Color (1f, 0f, 0f, 1f);

	}
	void OnTriggerExit2D(Collider2D other){
		count--;
		if (count == 0) {
			GameObject display = GameObject.Find ("Display");
			DisplayScript displayScript = display.GetComponent < DisplayScript > ();
			displayScript.isLegalPosition = true;

			this.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		}
	}
}
