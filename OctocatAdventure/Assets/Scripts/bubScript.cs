using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubScript : MonoBehaviour {
	Rigidbody2D rb;
	gmScript gm;

	// Use this for initialization
	void Start () {
//		rb = GetComponent<Rigidbody2D>();
//		gm = GameObject.Find("GameMaster").GetComponent<gmScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D collision) {
		GameObject other = collision.gameObject;
		if (other.CompareTag ("Enemy")) {
			Destroy (gameObject);
			Destroy (other);
		} else if (other.CompareTag ("Wall")) {
			//do nothing 
		}			
	}
}
