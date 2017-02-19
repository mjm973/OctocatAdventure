using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour {
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    gmScript gm;

    int health = 3;
    int dir = 1;
    bool bRight = false;

    GameObject cam;

    RaycastHit2D[] _maybeRaycast;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        gm = GameObject.Find("GameMaster").GetComponent<gmScript>();
        anim = GetComponent<Animator>();
        cam = GameObject.Find("Main Camera");
        _maybeRaycast = new RaycastHit2D[10];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate() {
        rb.velocity = new Vector2(2 * dir, rb.velocity.y);

        if (atEdge()) {
            bRight = !bRight;
            anim.SetBool("right", bRight);
            dir *= -1;
        }
    }

    bool atEdge() {
        RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)transform.position, new Vector2(dir, -1), 1.4f);
        if (hits.Length > 0) {
            foreach (RaycastHit2D hit in hits) {
                if (hit.collider.gameObject.CompareTag("Wall")) {
                    return false;
                }
            }
        }

        return true;
    }

    void damage(int amount) {
        health -= amount;

        StartCoroutine("blink");


        StartCoroutine("colors");


        cam.SendMessage("Shake", new Vector2(0.3f, 1.5f));

        if (health <= 0) {
            Destroy(gameObject);
        }
    }
    
    IEnumerator blink() {
        Color col = sr.color;
        for (int q = 0; q < 3; ++q) {
            col.a = 0.2f;
            sr.color = col;

            yield return new WaitForSeconds(0.1f);

            col.a = 1f;
            sr.color = col;

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator colors() {
        Color col = sr.color;
        Color temp = Color.yellow;
        for (int q = 0; q < 3; ++q) {
            sr.color = temp;

            yield return new WaitForSeconds(0.1f);

            sr.color = col;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
