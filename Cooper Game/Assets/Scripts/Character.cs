using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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

        /*
          + Input.GetAxis("Left Joystick X")
          + Input.GetAxis("Left Joystick Y")
        */
        
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
        Rotating(dx, dz);
    }

    void Rotating(float dx, float dz) {

        Vector3 newDir = new Vector3((forward.x * dz) + (forward.z * dx), 0.0f, (forward.z * dz) - (forward.x * dx));
        if (newDir != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newDir.normalized), 0.2f);
        }
    }
}
