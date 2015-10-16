using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private int ScrollWidth = 15; 
	public int ScrollSpeed = 25;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		MoveCamera ();
	
	}

	private void MoveCamera(){
		float xpos = Input.mousePosition.x;
		float ypos = Input.mousePosition.y;
		Vector3 movement = new Vector3 (0, 0, 0);

		//horizontal camera movement
		if (xpos >= 0 && xpos < ScrollWidth) {
			movement.x = movement.x - ScrollSpeed;
		} else if (xpos <= Screen.width && xpos > Screen.width - ScrollWidth) {
			movement.x = movement.x + ScrollSpeed;
		}

		//vertical camera movement
		if (ypos >= 0 && ypos < ScrollWidth) {
			movement.y = movement.y - ScrollSpeed;
		} else if (ypos <= Screen.height && ypos > Screen.height - ScrollWidth) {
			movement.y = movement.y + ScrollSpeed;
		}

		//away from ground movement
		movement.z = movement.z + ScrollSpeed * Input.GetAxis ("Mouse ScrollWheel");

		//calcualte camera position
		Vector3 origin = Camera.main.transform.position;
		Vector3 destination = origin;
		destination.x += movement.x;
		destination.y += movement.y;
		destination.z += movement.z;

		if (origin != destination) {
			Camera.main.transform.position = Vector3.MoveTowards (origin, destination, Time.deltaTime * ScrollSpeed);
		}
	}
}
