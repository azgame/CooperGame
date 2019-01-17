using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    private Vector3 offset;

    public float rotateSpeed = 5;

    void Start() {

        Cursor.visible = false;
        offset = target.position - transform.position;
    }

    void Update() {

        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        Debug.Log("Angle: " + desiredAngle.ToString());
        Debug.Log("Horizontal: " + horizontal.ToString());
        transform.position = target.transform.position - (rotation * offset);

        transform.LookAt(target.position);

        // try the unused code for this to get camera movement down
    }
}
