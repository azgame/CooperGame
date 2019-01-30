using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour {

    // Components
    Rigidbody rb;

    // Ground Movement
    Vector3 forward;
    Vector3 movement;
    float dx;
    float dz;
    [Range(1, 5)]
    public float speed;
    public float turnSmoothing;
    public float groundRadius;
    bool isGrounded;

    // Air Movement
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpVelocity;
    Vector3 jumpVec;

    // HID
    public bool isControllerEnabled;

    // Start is called before the first frame update
    void Start() {

        rb = GetComponent<Rigidbody>();

        if (turnSmoothing <= 0) {
            turnSmoothing = 15.0f;
            Debug.LogWarning("Turn Smoothing not set properly. Defaulting to: " + turnSmoothing.ToString());
        }
        isGrounded = true;
        jumpVec = Vector3.zero;
    }

    // Update is called once per frame
    void Update() {

        InputManager();
        forward = GetForward();
    }

    void FixedUpdate() {
        MovementManagement();
    }

    void MovementManagement() {

        GroundMovement();
        Jump();
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

    void GroundMovement() {

        if (isGrounded) {
            if (Input.GetButtonDown("Jump")) {
                jumpVec = Vector3.up * jumpVelocity;
            }
        }
        else {
            jumpVec = Vector3.zero;
        }

        movement = new Vector3((forward.x * dz) + (forward.z * dx), jumpVec.y, (forward.z * dz) - (forward.x * dx));
        rb.velocity = movement * speed;
    }

    void Jump() {
        if (rb.velocity.y <= 0) {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    Vector3 GetForward() { return Camera.main.transform.forward; }

    void OnCollisionEnter(Collision collision) {
        rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        if (collision.gameObject.tag == "Ground") {
            Debug.Log("ground collision");
            isGrounded = true;
        }
    }

    void OnCollisionStay(Collision collision) {
        rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }
}
