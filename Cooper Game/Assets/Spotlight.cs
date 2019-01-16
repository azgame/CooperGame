using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight : MonoBehaviour {

    public float spotspeed;

    private bool goingUp;

    // Start is called before the first frame update
    void Start() {

        goingUp = true;
    }

    // Update is called once per frame
    void Update() {

        if (goingUp) {
            transform.position += new Vector3(0.0f, spotspeed, 0.0f);
            if (transform.position.y >= 2.8f) goingUp = false;
        }
        else {
            transform.position -= new Vector3(0.0f, spotspeed, 0.0f);
            if (transform.position.y <= -1.0f) goingUp = true;
        }
        
    }
}
