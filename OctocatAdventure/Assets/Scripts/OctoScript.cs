using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OctoScript : MonoBehaviour {
    Rigidbody2D rb;
    Animator anim;

    public float harshness = 0.1f;
    float prevX = 0f, prevY = 0f;
    float velX = 0, velY = 0;
    float maxSpeed = 4f;

    public float delayTime = 0.1f;
    int numJumps = 0;
	int dir = 1;

	public GameObject bub;
    gmScript gm;

    // Use this for initializationu
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameMaster").GetComponent<gmScript>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate() {
        if (gm.bSwim) {
            anim.SetBool("swimming", true);
            swim();
        }
        else {
            anim.SetBool("swimming", false);
            walk();
        }

		if (Input.GetKeyDown(KeyCode.Return)) {
			shoot();
		}
    }

    void walk() {
        // Turn on gravity
        rb.gravityScale = gm.gravity;

        // Get X velocity for horizontal moving
        velX = Mathf.Lerp(prevX, Input.GetAxisRaw("Horizontal") * maxSpeed, harshness);

        if (velX < -0.2) {
            anim.SetInteger("dir", -1);
			dir = -1;
        }
        else if (velX > 0.2) {
            anim.SetInteger("dir", 1);
			dir = 1;
        }
        else {
            anim.SetInteger("dir", 0);
        }

        if (numJumps > 0) {
            anim.SetInteger("dir", 0);
        }

        // Check for jump key press
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && numJumps < gm.maxJumps) {
            // Add vertical force as impulse
            rb.AddForce(Vector2.up * gm.jumpImpulse, ForceMode2D.Impulse);
            ++numJumps;

            anim.SetBool("grounded", false);
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

	void shoot() {
		GameObject bubble = (GameObject) Instantiate(bub, transform.position + Vector3.right*0.5f*dir, Quaternion.identity);
		bubble.GetComponent<Rigidbody2D>().velocity = new Vector2(2f*dir, 0);
	}	

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject other = collision.gameObject;
        Vector2 whereOther = (Vector2)(other.transform.position - transform.position);

        if (other.CompareTag("Wall") && whereOther.y < 0) {
            numJumps = 0;
            anim.SetBool("grounded", true);
        }
    }

    IEnumerator InDelay(float[] xyt) {
        yield return new WaitForSeconds(xyt[2]);

        rb.velocity = new Vector2(xyt[0], xyt[1]);
    }
}
