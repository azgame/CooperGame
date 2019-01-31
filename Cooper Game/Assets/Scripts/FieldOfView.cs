using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldOfView : MonoBehaviour {

    [Range(0, 360)]
    public float fovx;
    [Range(0, 360)]
    public float fovy;
    public float viewRadius;
    Vector3[] angles;
    List<Vector3> vertices;
    public LayerMask walls;
    Vector3 pos;
    public Material vismat;

    public Vector3 DirFromAngleX(float angle_) {
        angle_ += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle_ * Mathf.Deg2Rad), 0.0f, Mathf.Cos(angle_ * Mathf.Deg2Rad));
    }

    public Vector3 DirFromAngleY(float angle_) {
        angle_ += transform.eulerAngles.y;
        return new Vector3(0.0f, Mathf.Sin(angle_ * Mathf.Deg2Rad), Mathf.Cos(angle_ * Mathf.Deg2Rad));
    }

    void Start() {
        angles = new Vector3[] { DirFromAngleX(-fovx / 2.0f), DirFromAngleX(fovx / 2.0f), DirFromAngleY(-fovy / 2.0f), DirFromAngleY(fovy / 2.0f) };
        vertices = new List<Vector3>();
    }

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
}
