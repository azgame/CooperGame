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

FIELD OF VIEW

void Update() {

        vertices.Clear();

        pos = this.transform.position;

        vertices.Add(pos);
        vertices.Add(CheckRayCast(pos, (pos + (new Vector3(angles[0].x, angles[2].y, angles[0].z) * viewRadius))));
        vertices.Add(CheckRayCast(pos, (pos + (new Vector3(angles[0].x, angles[3].y, angles[0].z) * viewRadius))));
        vertices.Add(CheckRayCast(pos, (pos + (new Vector3(angles[1].x, angles[2].y, angles[0].z) * viewRadius))));
        vertices.Add(CheckRayCast(pos, (pos + (new Vector3(angles[1].x, angles[3].y, angles[0].z) * viewRadius))));

        AddVertices();

        vertices.Sort((a, b) => Vector2.Angle(Vector2.right, new Vector2(a.x, a.y)).CompareTo(Vector2.Angle(Vector2.right, new Vector2(b.x, b.y))));

        GameObject cone = new GameObject();
        cone.AddComponent<MeshFilter>();
        cone.AddComponent<MeshRenderer>();

        Mesh mesh = cone.GetComponent<MeshFilter>().mesh;
        Vector3[] m = vertices.ToArray();
        mesh.vertices = m;
        mesh.triangles = new int[] {
            0, 1, 2,
            0, 2, 4,
            0, 4, 3,
            0, 3, 1,
            2, 1, 4,
            3, 4, 1
        };
        cone.GetComponent<MeshRenderer>().material = vismat;
        cone.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.05f);
        Destroy(cone, 0.025f);
    }

    bool InView(Vector3 pos_, Vector3[] angles_) {
        return pos_.x > pos.x + (angles_[0].x * viewRadius) &&
                    pos_.x < pos.x + (angles_[1].x * viewRadius) &&
                    pos_.y > pos.y + (angles_[2].y * viewRadius) &&
                    pos_.y < pos.y + (angles_[3].y * viewRadius);
    }

    void AddVertices() {
        Collider[] obs = Physics.OverlapSphere(pos, viewRadius);
        foreach (Collider col in obs) {
            if (col.gameObject.tag == "Wall") {

                Vector3 pos_ = col.gameObject.transform.position;
                bool inView = InView(pos_, angles);

                if (inView) {
                    Transform t = col.GetComponent<Transform>();
                    Vector3 s = t.localScale / 2.0f;
                    vertices.Add(CheckRayCast(pos, new Vector3(pos_.x - s.x, pos_.y - s.y, pos_.z - s.z)));
                    vertices.Add(CheckRayCast(pos, new Vector3(pos_.x - s.x, pos_.y + s.y, pos_.z - s.z)));
                    vertices.Add(CheckRayCast(pos, new Vector3(pos_.x - s.x, pos_.y - s.y, pos_.z + s.z)));
                    vertices.Add(CheckRayCast(pos, new Vector3(pos_.x - s.x, pos_.y + s.y, pos_.z + s.z)));
                    vertices.Add(CheckRayCast(pos, new Vector3(pos_.x + s.x, pos_.y - s.y, pos_.z - s.z)));
                    vertices.Add(CheckRayCast(pos, new Vector3(pos_.x + s.x, pos_.y + s.y, pos_.z - s.z)));
                    vertices.Add(CheckRayCast(pos, new Vector3(pos_.x + s.x, pos_.y - s.y, pos_.z + s.z)));
                    vertices.Add(CheckRayCast(pos, new Vector3(pos_.x + s.x, pos_.y + s.y, pos_.z + s.z)));
                }
            }
        }
    }

    Vector3 CheckRayCast(Vector3 origin, Vector3 tar) {

        RaycastHit r;
        if (Physics.Raycast(origin, (tar - origin).normalized, out r, viewRadius * 2, walls)) {
            return r.point;
        }
        else {
            return tar;
        }
    }

    static int CompareRaycastAngles(Vector3 origin, Vector3 lhs, Vector3 rhs) {

        float a1 = Mathf.Atan2(lhs.y - origin.y, lhs.x - origin.x);
        float a2 = Mathf.Atan2(rhs.y - origin.y, rhs.x - origin.x);
        if (a1 > a2) { return 1; }
        else { return 0; }

    }




*/
