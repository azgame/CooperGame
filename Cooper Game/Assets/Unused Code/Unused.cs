/*
 * 
 * void RotateCamera() {

    mouseX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;

        if (mouseX >= 0.55f && mouseX <= 0.75f && mouseX > prevMouseX) {
            yaw += rotateSpeed * Input.GetAxis("Mouse X");
            rate = 0.0f;
        }
        else if (mouseX <= 0.45f && mouseX >= 0.25f && mouseX < prevMouseX) {
            yaw += rotateSpeed * Input.GetAxis("Mouse X");
            rate = 0.0f;
        }
        else if (mouseX > 0.75f) {
            yaw += rotateSpeed * Input.GetAxis("Mouse X");
            rate = 0.0f;
        }
        else if (mouseX < 0.25f) {
            yaw += rotateSpeed * Input.GetAxis("Mouse X");
            rate = 0.0f;
        }
        else {
            if (rate < 1) {
                rate += Time.deltaTime / time;
                rate = Mathf.Clamp(rate, 0, time);
                yaw = Mathf.Lerp(yaw, 0, rate);
            }
        }

        mouseY = Camera.main.ScreenToViewportPoint(Input.mousePosition).y;

        if (mouseY >= 0.65f && mouseY <= 0.85f && mouseY > prevMouseY) {
            pitch -= rotateSpeed * Input.GetAxis("Mouse Y");
            rate = 0.0f;
        }
        else if (mouseY <= 0.35f && mouseY >= 0.15f && mouseY < prevMouseY) {
            pitch -= rotateSpeed * Input.GetAxis("Mouse Y");
            rate = 0.0f;
        }
        else if (mouseY > 0.85f) {
            pitch -= rotateSpeed * Input.GetAxis("Mouse Y");
            rate = 0.0f;
        }
        else if (mouseY < 0.15f) {
            pitch -= rotateSpeed * Input.GetAxis("Mouse Y");
            rate = 0.0f;
        }
        else {
            if (rate < 1) {
                rate += Time.deltaTime / time;
                rate = Mathf.Clamp(rate, 0, time);
                pitch = Mathf.Lerp(pitch, 0, rate);
            }
        }

        Quaternion rotation = Quaternion.Euler(Mathf.Clamp(pitch, pitchMinClamp, pitchMaxClamp), yaw, 0);
        
        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.position);

        prevMouseX = mouseX;
        prevMouseY = mouseY;
}


*/