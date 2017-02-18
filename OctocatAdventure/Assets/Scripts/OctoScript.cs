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

    public float delayTime = 0.1f;

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
		float delayTime = gm.inputDelay;

        velX = Mathf.Lerp(prevX, Input.GetAxisRaw("Horizontal") * maxSpeed, harshness);
        velY = Mathf.Lerp(prevY, Input.GetAxisRaw("Vertical") * maxSpeed, harshness);
        
        if (velX != 0 || velY != 0) {
            StartCoroutine("InDelay", new float[] {prevX, prevY, delayTime});
        }

        // Assign new velocity to previous in preparation for next frame
        prevX = velX;
        prevY = velY;
    }

    IEnumerator InDelay(float[] xyt) {
        yield return new WaitForSeconds(xyt[2]);

        rb.velocity = new Vector2(xyt[0], xyt[1]);
    }
}
