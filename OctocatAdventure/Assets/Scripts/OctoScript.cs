using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OctoScript : MonoBehaviour {
    Rigidbody2D rb;

    public float harshness = 0.1f;
    float prevX = 0f, prevY = 0f;
    float velX = 0, velY = 0;
    float maxSpeed = 4f;

    gmScript gm;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameMaster").GetComponent<gmScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate() {
        harshness = gm.movHarshness;

        velX = Mathf.Lerp(prevX, Input.GetAxisRaw("Horizontal") * maxSpeed, harshness);
        velY = Mathf.Lerp(prevY, Input.GetAxisRaw("Vertical") * maxSpeed, harshness);
        rb.velocity = new Vector2(velX, velY);

        // Assign new velocity to previous in preparation for next frame
        prevX = velX;
        prevY = velY;
    }
}
