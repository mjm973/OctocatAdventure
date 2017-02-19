using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OctoScript : MonoBehaviour {
    Rigidbody2D rb;
    Animator anim;
    AudioSource aud;
    SpriteRenderer sr;

    public float harshness = 0.1f;
    float prevX = 0f, prevY = 0f;
    float velX = 0, velY = 0;
    float maxSpeed = 4f;

    public float delayTime = 0.1f;
    int numJumps = 0;
    int dir = 1;

    float _mOuchTime = 0.2f;
    float ouchTime = 0;

    [Header("PARTICLEZ!!!")]
    public GameObject bub;
    public GameObject stars;

    [Header("Audio")]
    public AudioClip jumpy;
    public AudioClip shooty;
    public AudioClip hurt;

    gmScript gm;
    GameObject cam;

    // Use this for initializationu
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameMaster").GetComponent<gmScript>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.J)) {
            shoot();
        }
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

        ouchTime -= Time.fixedDeltaTime;
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

            aud.PlayOneShot(jumpy);

            rb.AddForce(Vector2.up * gm.jumpImpulse, ForceMode2D.Impulse);
            ++numJumps;

            anim.SetBool("grounded", false);
        }
        // Update velocity. We keep y vel the same because PHYSICZ
        if (ouchTime <= 0) {
            rb.velocity = new Vector2(velX, rb.velocity.y);
        }
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

        GameObject bubble = (GameObject)Instantiate(bub, transform.position + Vector3.right * 0.5f * dir, Quaternion.identity);
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();

        aud.PlayOneShot(shooty);

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
                Instantiate(stars, collision.contacts[0].point + Vector2.up * 0.25f, Quaternion.identity);
            }

            numJumps = 0;
            anim.SetBool("grounded", true);
        } else if (other.CompareTag("Enemy") && ouchTime <= 0) {
            ouchTime = _mOuchTime;

            aud.PlayOneShot(hurt);

            rb.AddForce(-whereOther * 10f, ForceMode2D.Impulse);

            anim.SetTrigger("hurt");

            StartCoroutine("blink", sr);

            StartCoroutine("colors", sr);

            cam.SendMessage("Shake", new Vector2(0.3f, 1.5f));
        }
    }

    IEnumerator InDelay(float[] xyt) {
        yield return new WaitForSeconds(xyt[2]);

        rb.velocity = new Vector2(xyt[0], xyt[1]);
    }

    IEnumerator blink(SpriteRenderer s) {
        Color col = s.color;
        for (int q = 0; q < 3; ++q) {
            col.a = 0.2f;
            s.color = col;

            yield return new WaitForSeconds(0.1f);

            col.a = 1f;
            s.color = col;

            yield return new WaitForSeconds(0.1f);
        }
        col.a = 1f;
        s.color = col;
    }

    IEnumerator colors(SpriteRenderer s) {
        Color col = s.color;
        Color temp = Color.yellow;
        for (int q = 0; q < 3; ++q) {
            s.color = temp;

            yield return new WaitForSeconds(0.1f);

            s.color = col;

            yield return new WaitForSeconds(0.1f);
        }
        col.a = 1f;
        s.color = Color.white;
    }
}
