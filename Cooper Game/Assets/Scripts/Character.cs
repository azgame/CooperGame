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
    bool jump;
    [Range(1, 5)]
    public float speed;
    public float turnSmoothing;
    bool isGrounded;

    // Air Movement
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpVelocity;
    Vector3 jumpVec;
    public float groundCheckDist;

    // HID
    public bool isControllerEnabled;

    void Start() {

        rb = GetComponent<Rigidbody>();

        if (turnSmoothing <= 0) {
            turnSmoothing = 15.0f;
            Debug.LogWarning("Turn Smoothing not set properly. Defaulting to: " + turnSmoothing.ToString());
        }
        isGrounded = true;
        jumpVec = Vector3.zero;
    }

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

    void InputManager() {

        if (isControllerEnabled) {
            dx = Input.GetAxis("Left Joystick X");
            dz = Input.GetAxis("Left Joystick Y");
        }
        else {
            dx = Input.GetAxis("Horizontal");
            dz = Input.GetAxis("Vertical");
            jump = Input.GetButtonDown("Jump");
        }
    }

    void GroundMovement() {
        if (isGrounded) {
            movement = new Vector3((forward.x * dz) + (forward.z * dx), jumpVec.y, (forward.z * dz) - (forward.x * dx));
            rb.velocity = movement * speed;
        }
        else {
            movement = new Vector3(rb.velocity.x, jumpVec.y * speed, rb.velocity.z);
            rb.velocity = movement;
        }
    }

    void Jump() {

        if (isGrounded) {
            jumpVec = Vector3.zero;
            if (jump) {
                jumpVec = (rb.velocity + Vector3.up) * jumpVelocity;
                isGrounded = false;
            }
        }

        if (jumpVec.y < 0) {
            jumpVec += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (jumpVec.y > 0 && !Input.GetButton("Jump")) {
            jumpVec += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Rotating() {

        if ((movement.x != 0.0f || movement.z != 0.0f) && isGrounded) {
            Vector3 lookAt = new Vector3(movement.x, 0.0f, movement.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt).normalized, 0.2f);
        }
    }


    Vector3 GetForward() { return Camera.main.transform.forward; }

    void OnCollisionEnter(Collision collision) {
        rb.angularVelocity = Vector3.zero;
        if (collision.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }

    void OnCollisionStay(Collision collision) {
        rb.angularVelocity = Vector3.zero;
    }

    void OnCollisionExit(Collision collision) {
        rb.angularVelocity = Vector3.zero;
        if (collision.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }
}
