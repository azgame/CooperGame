using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour {

    // Private
    Rigidbody rb;
    Vector3 forward;
    Vector3 movement;
    float dx;
    float dz;

    // Public
    public float hspeed;
    public float zspeed;
    public float turnSmoothing;
    public bool isControllerEnabled;

    // Start is called before the first frame update
    void Start() {

        rb = GetComponent<Rigidbody>();
        
        if (hspeed <= 0) {
            hspeed = 10.0f;
            Debug.LogWarning("H-Speed not set properly. Defaulting to: " + hspeed.ToString());
        }

        if (zspeed <= 0) {
            zspeed = 10.0f;
            Debug.LogWarning("Z-Speed not set properly. Defaulting to: " + zspeed.ToString());
        }

        if (turnSmoothing <= 0) {
            turnSmoothing = 15.0f;
            Debug.LogWarning("Turn Smoothing not set properly. Defaulting to: " + turnSmoothing.ToString());
        }

    }

    // Update is called once per frame
    void Update() {

        InputManager();
        forward = GetForward();
        MovementManagement();
    }

    void MovementManagement() {

        movement = new Vector3((forward.x * dz) + (forward.z * dx), 0.0f, (forward.z * dz) - (forward.x * dx));
        rb.velocity = movement * hspeed;
        Rotating();
    }

    void Rotating() {

        if (movement != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);
        }
    }

    void InputManager() {

        if (isControllerEnabled) {
            if (Input.GetAxis("Left Joystick X") != 0) { dx = Input.GetAxis("Left Joystick X"); }
            else { dx = 0; }
            if (Input.GetAxis("Left Joystick Y") != 0) { dz = -Input.GetAxis("Left Joystick Y"); }
            else { dz = 0; }
        }
        else {
            if (Input.GetAxis("Horizontal") != 0) { dx = Input.GetAxis("Horizontal"); }
            else { dx = 0; }
            if (Input.GetAxis("Vertical") != 0) { dz = Input.GetAxis("Vertical"); }
            else { dz = 0; }
        }
    }

    Vector3 GetForward() { return Camera.main.transform.forward; }

    void OnCollisionEnter(Collision collision) { rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f); }
    void OnCollisionStay(Collision collision) { rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f); }
}
