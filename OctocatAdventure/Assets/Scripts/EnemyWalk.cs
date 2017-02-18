using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour {
    Rigidbody2D rb;
    gmScript gm;
    int dir = 1;

    RaycastHit2D[] _maybeRaycast;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameMaster").GetComponent<gmScript>();
        _maybeRaycast = new RaycastHit2D[10];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate() {
        rb.velocity = new Vector2(2 * dir, rb.velocity.y);

        if (atEdge()) {
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
}
