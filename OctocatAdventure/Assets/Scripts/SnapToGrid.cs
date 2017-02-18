using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour {

	// Use this for initialization
	void Start () {
        snap(transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    static void snap(Transform t) {
        float x = (Mathf.Floor(t.position.x) * 2f + 1) * 0.5f;
        float y = (Mathf.Floor(t.position.y) * 2f + 1) * 0.5f;

        t.position = new Vector3(x, y);
    }
}
