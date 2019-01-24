using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Private
    Vector3 offset;
    float yaw;
    float pitch;
    bool isControllerEnabled;

    // Public
    public GameObject target;
    public float pitchMinClamp;
    public float pitchMaxClamp;
    public float dampen;
    public float rotateSpeed;

    void Start() {

        if (pitchMinClamp <= -20 || pitchMinClamp >= 0) {
            pitchMinClamp = -10;
            Debug.LogWarning("Pitch Minimum not set properly. Changed to: " + pitchMinClamp.ToString());
        }

        if (pitchMaxClamp <= -10 || pitchMaxClamp >= 10) {
            pitchMaxClamp = 0;
            Debug.LogWarning("Pitch Maximum not set properly. Changed to: " + pitchMaxClamp.ToString());
        }

        if (dampen <= 0) {
            dampen = 8;
            Debug.LogWarning("Dampen not set properly. Changed to: " + dampen.ToString());
        }

        if (rotateSpeed <= 0) {
            rotateSpeed = 5;
            Debug.LogWarning("RotateSpeed not set properly. Changed to: " + rotateSpeed.ToString());
        }

        isControllerEnabled = target.GetComponent<Character>().isControllerEnabled;

        yaw = 0;
        pitch = -5;
        offset = target.transform.position - transform.position;
        Cursor.visible = false;
    }

    void FixedUpdate() {

        if (isControllerEnabled) {
            if (Input.GetAxis("Right Joystick X") != 0 || Input.GetAxis("Right Joystick Y") != 0) {
                yaw += Input.GetAxis("Right Joystick X") * rotateSpeed;
                pitch -= Input.GetAxis("Right Joystick Y") * rotateSpeed / dampen;
            }
        }
        else {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
                yaw += Input.GetAxis("Mouse X") * rotateSpeed;
                pitch -= Input.GetAxis("Mouse Y") * rotateSpeed / dampen;
            }
        }

        pitch = Mathf.Clamp(pitch, pitchMinClamp, pitchMaxClamp);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.transform.position);
    }
}
