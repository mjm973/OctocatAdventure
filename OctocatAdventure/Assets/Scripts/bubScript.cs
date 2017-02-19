using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubScript : MonoBehaviour {
    Rigidbody2D rb;
	gmScript gm;
    public float lifetime = 5f;
    public GameObject poppin;
	public bool bBounce = true;

    public PhysicsMaterial2D bounce, normal;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
		gm = GameObject.Find("GameMaster").GetComponent<gmScript>();
		bBounce = gm.bubBounce;
    }

    // Update is called once per frame
    void Update() {
        lifetime -= Time.deltaTime;

        if (bBounce) {
            rb.sharedMaterial = bounce;
        } else {
            rb.sharedMaterial = normal;
        }

        if (lifetime <= 0) {

            Instantiate(poppin, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Enemy")) {
            other.SendMessage("damage", 1);

            Instantiate(poppin, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall")) {
            //do nothing 
        }
    }
}

