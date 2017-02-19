using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour {
    GameObject octo; // OCTOCAT!!!
    gmScript gm;

    float newX = 0, newY = 0;
    float prevX = 0, prevY = 0; // Camera's previous position
    float harshness = 0.2f;

    // SCREEN SHAKE
    float time = 10;
    float shakeAmount = 5;
    float shakePeriod = 0, amplitude = 0;
    float randomSeed = 0;
    float camZ;

    // Use this for initialization
    void Start() {
        octo = GameObject.Find("Octocat");
        gm = GameObject.Find("GameMaster").GetComponent<gmScript>();
        camZ = transform.position.z; // Save camera depth
    }

    // Update is called once per frame
    void Update() {
        harshness = gm.camHarshness;

        newX = Mathf.Lerp(prevX, octo.transform.position.x, harshness);
        newY = Mathf.Lerp(prevY, octo.transform.position.y, harshness);

        if (time < shakePeriod) {
            float angle = time * Mathf.PI / (shakePeriod); // Angle goes from 0 to Pi over the period of shaking
            amplitude = shakeAmount * Mathf.Sin(angle); // Angle drives a sine function to determine base amount of shaking

            // Using a random seed, obtain values from Perlin Noise for a smoother, more unpredictable shake
            float perlinX = (Mathf.PerlinNoise(randomSeed + time * 10, randomSeed) - 0.5f) * 2;
            float perlinY = (Mathf.PerlinNoise(randomSeed, randomSeed + time * 10) - 0.5f) * 2;

            // Set new position (AKA shake)
            Vector3 newPos = new Vector3(newX + perlinX * amplitude, newY + perlinY * amplitude, camZ);
            transform.position = newPos;

            time += Time.deltaTime;
        }
        else {
            transform.position = new Vector3(newX, newY, camZ); // Define origin as default camera position
        }
        prevX = newX;
        prevY = newY;
    }

    // When called, sets shaking period, base shake amount, chooses a random seed, and resets the timer
    void Shake(Vector2 periodAmp) {
        shakePeriod = periodAmp.x;
        shakeAmount = periodAmp.y;
        randomSeed = Random.Range(0f, 100f);
        time = 0;
    }
}
