using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Private
    private Vector3 offset;
    private float rate;
    private float yaw;
    private float pitch;
    private float camCount;

    // Public
    public Transform target;
    public float pitchMinClamp;
    public float pitchMaxClamp;
    public float time;

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

        if (time <= 0) {
            time = 20;
        }

        yaw = 0;
        pitch = -5;
        camCount = 0;
        rotateSpeed = 5;
        offset = target.position - transform.position;
        Cursor.visible = false;
    }

    void Update() {

        if (Input.GetAxis("Mouse X") != 0) {
            yaw += Input.GetAxis("Mouse X") * rotateSpeed;
            rate = 0.0f;
            camCount = 0.0f;
        }
        else if (Input.GetAxis("Mouse Y") != 0) {
            pitch -= Input.GetAxis("Mouse Y") * rotateSpeed;
            rate = 0.0f;
            camCount = 0.0f;
        }
        else if (Input.GetAxis("Right Joystick X") != 0) {
            yaw += Input.GetAxis("Right Joystick X") * rotateSpeed;
            rate = 0.0f;
            camCount = 0.0f;
        }
        else if (Input.GetAxis("Right Joystick Y") != 0) {
            pitch -= Input.GetAxis("Mouse Y") * rotateSpeed;
            rate = 0.0f;
            camCount = 0.0f;
        }
        else {
            camCount += Time.deltaTime;
            if (rate < 1 && camCount > 3) {
                rate += Time.deltaTime / time;
                rate = Mathf.Clamp(rate, 0, time);
                yaw = Mathf.Lerp(yaw, target.eulerAngles.y, rate);
                pitch = Mathf.Lerp(pitch, -5, rate);
            }
        }

        Quaternion rotation = Quaternion.Euler(Mathf.Clamp(pitch, pitchMinClamp, pitchMaxClamp), yaw, 0);
        
        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.position);
    }
}

/*
  + Input.GetAxis("Right Joystick X") * rotateSpeed
   + Input.GetAxis("Right Joystick Y") * rotateSpeed
     */
