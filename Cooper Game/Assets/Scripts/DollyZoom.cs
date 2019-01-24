using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyZoom : MonoBehaviour {

    public Transform target;
    public new Camera camera;

    private float initHeightAtDist;
    private bool dzEnabled;


    float FrustumHeightAtDistance(float distance) {
        return 2.0f * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    float FOVForHeightAndDistance(float height, float distance) {
        return 2.0f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
    }

    // Start the dolly zoom effect.
    void StartDZ() {
        float distance = Vector3.Distance(transform.position, target.position);
        initHeightAtDist = FrustumHeightAtDistance(distance);
        dzEnabled = true;
    }

    // Turn dolly zoom off.
    void StopDZ() {
        dzEnabled = false;
    }

    void Start() {
        StartDZ();
        // SetObliqueness(OblH, OblV);
    }

    void Update() {
        if (dzEnabled) {
            // Measure the new distance and readjust the FOV accordingly.
            float currDistance = Vector3.Distance(transform.position, target.position);
            camera.fieldOfView = FOVForHeightAndDistance(initHeightAtDist, currDistance);
        }

        // Simple control to allow the camera to be moved in and out using the up/down arrows.
        transform.Translate(Input.GetAxis("Mouse ScrollWheel") * Vector3.forward * Time.deltaTime * 5f);

    }
}
