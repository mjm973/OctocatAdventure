using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour {
    GameObject octo; // OCTOCAT!!!
    gmScript gm;

    float newX = 0, newY = 0;
    float prevX = 0, prevY = 0; // Camera's previous position
    float harshness = 0.2f;

	// Use this for initialization
	void Start () {
        octo = GameObject.Find("Octocat");
        gm = GameObject.Find("GameMaster").GetComponent<gmScript>();
	}
	
	// Update is called once per frame
	void Update () {
        harshness = gm.camHarshness;

        newX = Mathf.Lerp(prevX, octo.transform.position.x, harshness);
        newY = Mathf.Lerp(prevY, octo.transform.position.y, harshness);

        transform.position = new Vector3(newX, newY, transform.position.z);

        prevX = newX;
        prevY = newY;
	}
}
