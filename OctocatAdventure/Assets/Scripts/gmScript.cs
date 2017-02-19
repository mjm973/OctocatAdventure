using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gmScript : MonoBehaviour {
    public float camHarshness = 0.05f;
    public float movHarshness = 0.1f;
	public float inputDelay = 0f;
    public int maxJumps = 1;
    public float gravity = 1f;
    public float jumpImpulse = 5f;

	public float bubSpeed = 2f;
	public bool bubGravity = false;
       
    public bool bSwim = false;

    GameObject pPanel;
	Button shButton; 
    bool pauseActive = true;

    // Use this for initialization
    void Start () {
        Time.timeScale = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (pPanel == null) {
            pPanel = GameObject.Find("PausePanel");
        }

        if (pPanel.activeInHierarchy) {
            movHarshness = GameObject.Find("HarshSlider").GetComponent<Slider>().value;
            camHarshness = GameObject.Find("CamSlider").GetComponent<Slider>().value;
            inputDelay = GameObject.Find("DelaySlider").GetComponent<Slider>().value;
			gravity = GameObject.Find("GravitySlider").GetComponent<Slider>().value;
			jumpImpulse = GameObject.Find("ImpulseSlider").GetComponent<Slider>().value;
			//dropdown value plus 1 to match the options 
			maxJumps = GameObject.Find("JumpsNum").GetComponent<Dropdown>().value + 1;

			bubSpeed = GameObject.Find("BubSpeedSlider").GetComponent<Slider>().value;
			bubGravity = GameObject.Find("BubGravityToggle").GetComponent<Toggle>().isOn;
        }


		shButton = GameObject.Find("ShowHideBtn").GetComponent<Button>();
		shButton.onClick.AddListener(showHidePanel);
			

//        if (Input.GetKeyDown(KeyCode.Space) && pPanel != null) {
//            pauseActive = !pauseActive;
//
//            if (pauseActive) {
//                Time.timeScale = 0f;
//            } else {
//                Time.timeScale = 1f;
//            }
//
//            pPanel.SetActive(pauseActive);
//        }

    }

	void showHidePanel() {
		if (pPanel != null) {
			pauseActive = !pauseActive;

			if (pauseActive) {
				Time.timeScale = 0f;
			} else {
				Time.timeScale = 1f;
			}

			pPanel.SetActive(pauseActive);
		}
	}
		
}


