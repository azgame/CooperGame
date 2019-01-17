/*
 * 
 * void RotateCamera() {

    mouseX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;

    if (mouseX >= 0.65f && mouseX <= 0.75f && mouseX > prevMouseX) {
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


*/