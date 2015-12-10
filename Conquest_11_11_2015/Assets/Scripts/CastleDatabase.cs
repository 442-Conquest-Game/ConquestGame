using UnityEngine;
using System.Collections;

public class CastleDatabase : MonoBehaviour {
    public GameObject[] castles;
    public GameManager manager;
	// Use this for initialization
	void Start () {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();  
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
