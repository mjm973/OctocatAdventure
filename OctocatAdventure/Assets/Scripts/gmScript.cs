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
	public bool bubBounce = true;
       
    public bool bSwim = false;

    GameObject pPanel;
	Button shButton; 
    bool pauseActive = true;

	bool bPrev, pPrev;

	GameObject playerTab, bubbleTab;
	GameObject[] playerControls, bubbleControls; 

	Toggle pToggle, bToggle;

    // Use this for initialization
    void Start () {
        Time.timeScale = 0f;

		playerTab = GameObject.Find ("PlayerTab");
		playerControls = GameObject.FindGameObjectsWithTag ("PlayerControl");
		bubbleTab = GameObject.Find ("BubbleTab");
		bubbleControls = GameObject.FindGameObjectsWithTag ("BubbleControl");
	}
	
	// Update is called once per frame
	void Update () {
        if (pPanel == null) {
            pPanel = GameObject.Find("PausePanel");
        }

		bool ptabIsOn = true;
		bool btabIsOn = false;

        if (pPanel.activeInHierarchy) {

			ptabIsOn = playerTab.GetComponent<Toggle> ().isOn;
			btabIsOn = bubbleTab.GetComponent<Toggle> ().isOn;


			if ((ptabIsOn && pPrev == false) || (!ptabIsOn && pPrev == true)) {
				foreach (GameObject i in playerControls) {
					i.SetActive (true);
				}
				foreach (GameObject i in bubbleControls) {
					i.SetActive (false);
				}

				movHarshness = GameObject.Find ("HarshSlider").GetComponent<Slider> ().value;
				camHarshness = GameObject.Find ("CamSlider").GetComponent<Slider> ().value;
				inputDelay = GameObject.Find ("DelaySlider").GetComponent<Slider> ().value;
				gravity = GameObject.Find ("GravitySlider").GetComponent<Slider> ().value;
				jumpImpulse = GameObject.Find ("ImpulseSlider").GetComponent<Slider> ().value;
				//dropdown value plus 1 to match the options 
				maxJumps = GameObject.Find ("JumpsNum").GetComponent<Dropdown> ().value + 1;

				pPrev = ptabIsOn;

			} else {
//				foreach (GameObject i in playerControls) {
//					i.SetActive (false);
//				}
			}

			if ((btabIsOn && bPrev == false) || (!btabIsOn && bPrev == true)) {
				foreach (GameObject i in bubbleControls) {
					i.SetActive (true);
				}
				foreach (GameObject i in playerControls) {
					i.SetActive (false);
				}

				bubSpeed = GameObject.Find ("BubSpeedSlider").GetComponent<Slider> ().value;
				bubGravity = GameObject.Find ("BubGravityToggle").GetComponent<Toggle> ().isOn;
				bubBounce = GameObject.Find ("BubBounceToggle").GetComponent<Toggle> ().isOn; 

				bPrev = btabIsOn;

			} else {
//				foreach (GameObject i in bubbleControls) {
//					i.SetActive (false);
//				}
			}

        }


		shButton = GameObject.Find("ShowHideBtn").GetComponent<Button>();
		shButton.onClick.AddListener(this.showHidePanel);
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

//	void showHidePlayerTab() {
//		foreach (GameObject i in playerControls) {
//			i.SetActive (true);
//		}
//		foreach (GameObject i in bubbleControls) {
//			i.SetActive (false);
//		}
//
//		movHarshness = GameObject.Find ("HarshSlider").GetComponent<Slider> ().value;
//		camHarshness = GameObject.Find ("CamSlider").GetComponent<Slider> ().value;
//		inputDelay = GameObject.Find ("DelaySlider").GetComponent<Slider> ().value;
//		gravity = GameObject.Find ("GravitySlider").GetComponent<Slider> ().value;
//		jumpImpulse = GameObject.Find ("ImpulseSlider").GetComponent<Slider> ().value;
//		//dropdown value plus 1 to match the options 
//		maxJumps = GameObject.Find ("JumpsNum").GetComponent<Dropdown> ().value + 1;
//	}
//
//	void showHideBubbleTab() {
//		foreach (GameObject i in bubbleControls) {
//			i.SetActive (true);
//		}
//		foreach (GameObject i in playerControls) {
//			i.SetActive (false);
//		}
//
//		bubSpeed = GameObject.Find ("BubSpeedSlider").GetComponent<Slider> ().value;
//		bubGravity = GameObject.Find ("BubGravityToggle").GetComponent<Toggle> ().isOn;
//		bubBounce = GameObject.Find ("BubBounceToggle").GetComponent<Toggle> ().isOn; 
//	}

		
}


