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
    public GameObject stars;
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

<<<<<<< HEAD
		if (Input.GetKeyDown(KeyCode.J)) {
			shoot();
		}
=======
        if (Input.GetKeyDown(KeyCode.Return)) {
            shoot();
        }
>>>>>>> bddc6b31f508a7450deb1c2571ef77ed9dc99cba
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
        float speed = gm.bubSpeed;
        bool gravity = gm.bubGravity;
        Debug.Log(gravity);

        GameObject bubble = (GameObject)Instantiate(bub, transform.position + Vector3.right * 0.5f * dir, Quaternion.identity);
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();

        if (gravity) {
            rb.gravityScale = 1f;
        }
        else {
            rb.gravityScale = 0f;
        }

        rb.velocity = new Vector2(speed * dir, 0);

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject other = collision.gameObject;
        Vector2 whereOther = (Vector2)(other.transform.position - transform.position);

        if (other.CompareTag("Wall") && whereOther.y < 0) {

            if (numJumps > 0) {
                Instantiate(stars, collision.contacts[0].point + Vector2.up * 0.1f, Quaternion.identity);
            }

            numJumps = 0;
            anim.SetBool("grounded", true);
        }
    }

    IEnumerator InDelay(float[] xyt) {
        yield return new WaitForSeconds(xyt[2]);

        rb.velocity = new Vector2(xyt[0], xyt[1]);
    }
}
