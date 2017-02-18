﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gmScript : MonoBehaviour {
    public float movHarshness = 0.1f;
    GameObject pPanel;
    bool pauseActive = true;

	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
        if (pPanel == null) {
            pPanel = GameObject.Find("PausePanel");
        }

        if (pPanel.activeInHierarchy) {
            movHarshness = GameObject.Find("HarshSlider").GetComponent<Slider>().value;
        }

        if (Input.GetKeyDown(KeyCode.Space) && pPanel != null) {
            pauseActive = !pauseActive;
            pPanel.SetActive(pauseActive);
        }
    }
}
