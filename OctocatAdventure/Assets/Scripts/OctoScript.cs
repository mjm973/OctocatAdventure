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
    int numJumps = 0;

    gmScript gm;

	// Use this for initializationu
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameMaster").GetComponent<gmScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate() {
        if (gm.bSwim) {
            swim();
        } else {
            walk();
        }
    }

    void walk() {
        // Turn on gravity
        rb.gravityScale = gm.gravity;

        // Get X velocity for horizontal moving
        velX = Mathf.Lerp(prevX, Input.GetAxisRaw("Horizontal") * maxSpeed, harshness);

        // Check for jump key press
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && numJumps < gm.maxJumps) {
            // Add vertical force as impulse
            rb.AddForce(Vector2.up * gm.jumpImpulse, ForceMode2D.Impulse);
            ++numJumps;
        }
         // Update velocity. We keep y vel the same because PHYSICZ
        rb.velocity = new Vector2(velX, rb.velocity.y);
        prevX = velX; // Update previous X for Lerping
    }

    void swim() {
        rb.gravityScale = 0f;

        harshness = gm.movHarshness;
        float delayTime = gm.inputDelay;

        velX = Mathf.Lerp(prevX, Input.GetAxisRaw("Horizontal") * maxSpeed, harshness);
        velY = Mathf.Lerp(prevY, Input.GetAxisRaw("Vertical") * maxSpeed, harshness);

        if (velX != 0 || velY != 0) {
            StartCoroutine("InDelay", new float[] { prevX, prevY, delayTime });
        }

        // Assign new velocity to previous in preparation for next frame
        prevX = velX;
        prevY = velY;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject other = collision.gameObject;
        Vector2 whereOther = (Vector2)(other.transform.position - transform.position);

        if (other.CompareTag("Wall") && whereOther.y < 0) {
            numJumps = 0;
        }
    }

    IEnumerator InDelay(float[] xyt) {
        yield return new WaitForSeconds(xyt[2]);

        rb.velocity = new Vector2(xyt[0], xyt[1]);
    }
}
