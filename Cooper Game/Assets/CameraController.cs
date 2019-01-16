using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public Transform target;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float time = 3.0f;
    private float rate = 0.0f;
    private float mouseX = 0.0f;
    private float prevMouseX = 0.0f;

    void Start() {

        // Cursor.visible = false;
    }

    void Update() {

        mouseX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        

        if (mouseX >= 0.65f && mouseX <=0.75f && mouseX > prevMouseX) {
            yaw -= speedH * Input.GetAxis("Mouse X");
            rate = 0.0f;
        }
        else if (mouseX <= 0.35f && mouseX >= 0.25f && mouseX < prevMouseX) {
            yaw -= speedH * Input.GetAxis("Mouse X");
            rate = 0.0f;
        }
        else if (mouseX > 0.75f) {
            yaw -= speedH * Input.GetAxis("Mouse X");
            rate = 0.0f;
        }
        else if (mouseX < 0.25f) {
            yaw -= speedH * Input.GetAxis("Mouse X");
            rate = 0.0f;
        }
        else {
            if (rate < 1) {
                rate += Time.deltaTime / time;
                rate = Mathf.Clamp(rate, 0, time);
                yaw = Mathf.Lerp(yaw, 0, rate);
            }
        }

        // pitch -= speedV * Input.GetAxis("Mouse Y");
        // transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        transform.LookAt(target);
        transform.Translate(new Vector3(yaw, 0.0f, 0.0f) * speedH);

        prevMouseX = mouseX;
    }
}
