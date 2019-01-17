using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public float hspeed;
    public float zspeed;
    public Camera camera;
    public float turnSmoothing = 15.0f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start() {

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

        // TODO: Move this logic into a charMove/charController script
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");

        MovementManagement(dx, dz);
        Debug.Log("Forward x: " + transform.forward.x.ToString() + " Forward z: " + transform.forward.z.ToString());

        //Vector3 forwardmovement = new Vector3(dz * hspeed * transform.forward.x, 0.0f, dz * zspeed * transform.forward.z);
        //Vector3 strafemovement = new Vector3(dx * hspeed * transform.forward.z, 0.0f, dz * zspeed * transform.forward.z);
        //rb.velocity = forwardmovement;
        rb.velocity = new Vector3(transform.forward.x * dx * hspeed, 0.0f, transform.forward.z * dz * zspeed);
    }

    void OnCollisionEnter(Collision collision) {

        rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void OnCollisionStay(Collision collision) {

        rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void MovementManagement(float horizontal, float vertical) {
        // If there is some axis input...
        if (horizontal != 0.0f || vertical != 0.0f) {
            // ... set the players rotation and set the speed parameter to 5.3f.
            Rotating(horizontal, vertical);
        }
    }

    void Rotating(float horizontal, float vertical) {
        // Create a new vector of the horizontal and vertical inputs.
        Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
        targetDirection = Camera.main.transform.TransformDirection(targetDirection);
        targetDirection.y = 0.0f;

        // Create a rotation based on this new vector assuming that up is the global y axis.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        // Create a rotation that is an increment closer to the target rotation from the player's rotation.
        Quaternion newRotation = Quaternion.Lerp(rb.rotation, targetRotation, turnSmoothing * Time.deltaTime);

        // Change the players rotation to this new rotation.
        rb.MoveRotation(newRotation);
    }
}
