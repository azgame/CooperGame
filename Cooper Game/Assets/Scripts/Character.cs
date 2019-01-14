using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public float hspeed;
    public float zspeed;
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

        Vector3 movement = new Vector3(dx * hspeed, 0.0f, dz * zspeed);
        rb.velocity = movement;
    }
}
