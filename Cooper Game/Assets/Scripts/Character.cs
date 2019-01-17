using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public float hspeed;
    public float zspeed;
    public new Camera camera;
    public float turnSmoothing = 15.0f;
    Rigidbody rb;
    Vector3 forward;

    // Start is called before the first frame update
    void Start() {

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

        // TODO: Move this logic into a charMove/charController script
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");

        forward = camera.transform.forward;

        MovementManagement(dx, dz);
    }

    void OnCollisionEnter(Collision collision) {

        rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void OnCollisionStay(Collision collision) {

        rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void MovementManagement(float dx, float dz) {

        rb.velocity = new Vector3((forward.x * dz * hspeed) + (forward.z * dx * hspeed), 0.0f, (forward.z * dz * zspeed) - (forward.x * dx * hspeed));
        Rotating();
    }

    void Rotating() {

        if (Input.GetKey(KeyCode.Q)) {
            transform.Rotate(Vector3.down);
        }
        else if (Input.GetKey(KeyCode.E)) {
            transform.Rotate(Vector3.up);
        }
    }
}

/*
    Move the player relative to the camera's forward vector, rotate the player according to direction traveling, rotate camera with mouse
     */
