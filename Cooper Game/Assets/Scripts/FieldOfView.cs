using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class FieldOfView : MonoBehaviour {

    [Range(0, 360)]
    public float fovx;
    [Range(0, 360)]
    public float fovy;
    public float viewRadius;
    Vector3 viewDir;
    Vector2 forward;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public MeshFilter meshFilter;
    public bool tarSeen;
    Mesh viewMesh;
    Vector3 pos;
    Vector3 feet;
    Vector3[] angles;
    List<Vector3> vertices;


    public Vector3 DirFromAngleX(float ax_, float ay_) {
        ax_ += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(ax_ * Mathf.Deg2Rad), 0.0f, Mathf.Cos(ax_ * Mathf.Deg2Rad));
    }

    public Vector3 DirFromAngleY(float ay_) {
        float ax_ = transform.eulerAngles.y;
        ay_ += transform.eulerAngles.x;
        return new Vector3(Mathf.Sin(ax_ * Mathf.Deg2Rad), Mathf.Sin(ay_ * Mathf.Deg2Rad), Mathf.Cos(ax_ * Mathf.Deg2Rad));
    }

    void Start() {

        ///angles = new Vector3[] { DirFromAngleX(-fovx / 2.0f, fovy), DirFromAngleX(fovx / 2.0f, fovy), DirFromAngleY(-fovy / 2.0f), DirFromAngleY(fovy / 2.0f) };
        vertices = new List<Vector3>();
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        meshFilter.mesh = viewMesh;
        tarSeen = false;
    }

    void Update() {

        angles = new Vector3[] { DirFromAngleX(-fovx / 2.0f, fovy), DirFromAngleX(fovx / 2.0f, fovy), DirFromAngleY(-fovy / 2.0f), DirFromAngleY(fovy / 2.0f) };
        vertices.Clear();

        forward = new Vector2(this.transform.forward.x, this.transform.forward.z);
        pos = this.transform.position;
        feet = pos - new Vector3(0.0f, this.transform.localScale.y / 2.5f, 0.0f);

        ScanView();
        AddVertices();
        SortVertices();

        List<Vector3> v = new List<Vector3>();

        int count = 0;
        if (this.transform.eulerAngles.y < 320.0f && this.transform.eulerAngles.y > 220.0f) {
            for (int i = 0; i < vertices.Count; i++) {
                if (FindDegree(feet, vertices[i]) < fovx) {
                    count++;
                }
            }
            
            v = vertices.Skip(vertices.Count - count).Concat(vertices.Take(vertices.Count - count)).ToList();
        }
        else {
            v = vertices;
        }
    }

    void ScanView() {

        Collider[] tarInView = Physics.OverlapSphere(feet, viewRadius, targetMask);

        for (int i = 0; i < tarInView.Length; i++) {
            Transform target = tarInView[i].transform;
            if (InView(target.position, this.transform.forward, fovx, angles)) {
                float tarDist = Vector3.Distance(feet, target.position);
                if (!Physics.Raycast(feet, target.transform.position, tarDist, obstacleMask)) {
                    tarSeen = true;
                    Debug.DrawLine(this.transform.position, target.transform.position, Color.yellow);
                }
                else if (Physics.Raycast(feet, target.transform.position, tarDist)) {

                }
            }
            else {
                tarSeen = false;
            }
        }
    }

    void AddVertices() {

        vertices.Add(CheckRayCast(feet, feet + (new Vector3(angles[0].x, 0.0f, angles[0].z) * viewRadius)));
        vertices.Add(CheckRayCast(feet, feet + (new Vector3(angles[1].x, 0.0f, angles[1].z) * viewRadius)));
        vertices.Add(CheckRayCast(feet, feet + (this.transform.forward * viewRadius)));
        
        Collider[] obs = Physics.OverlapSphere(feet, viewRadius);
        Corners(obs);
    }


    void Corners(Collider[] obs) {

        foreach (Collider col in obs) {
            if (col.gameObject.tag == "Wall") {

                Vector3 pos_ = col.gameObject.transform.position;

                if (InView(pos_, this.transform.forward, fovx / 2.0f, angles)) {
                    Transform t = col.GetComponent<Transform>();
                    Vector3 s = t.localScale / 2.0f;

                    vertices.Add(CheckRayCast( feet, new Vector3(pos_.x - s.x, pos_.y - s.y + 0.2f, pos_.z - s.z)));
                    vertices.Add(CheckRayCast( feet, new Vector3(pos_.x - s.x + 0.1f, pos_.y - s.y + 0.2f, pos_.z - s.z - 0.1f)));
                    vertices.Add(CheckRayCast( feet, new Vector3(pos_.x - s.x - 0.1f, pos_.y - s.y + 0.2f, pos_.z - s.z + 0.1f)));
                    vertices.Add(CheckRayCast( feet, new Vector3(pos_.x - s.x, pos_.y - s.y + 0.2f, pos_.z + s.z)));
                    vertices.Add(CheckRayCast( feet, new Vector3(pos_.x - s.x + 0.1f, pos_.y - s.y + 0.2f, pos_.z + s.z - 0.1f)));
                    vertices.Add(CheckRayCast( feet, new Vector3(pos_.x - s.x - 0.1f, pos_.y - s.y + 0.2f, pos_.z + s.z + 0.1f)));
                    vertices.Add(CheckRayCast( feet, new Vector3(pos_.x + s.x, pos_.y - s.y + 0.2f, pos_.z - s.z)));
                    vertices.Add(CheckRayCast( feet, new Vector3(pos_.x + s.x + 0.1f, pos_.y - s.y + 0.2f, pos_.z - s.z - 0.1f)));
                    vertices.Add(CheckRayCast( feet, new Vector3(pos_.x + s.x - 0.1f, pos_.y - s.y + 0.2f, pos_.z - s.z + 0.1f)));
                    vertices.Add(CheckRayCast( feet, new Vector3(pos_.x + s.x, pos_.y - s.y + 0.2f, pos_.z + s.z)));
                    vertices.Add(CheckRayCast( feet, new Vector3(pos_.x + s.x + 0.1f, pos_.y - s.y + 0.2f, pos_.z + s.z - 0.1f)));
                    vertices.Add(CheckRayCast( feet, new Vector3(pos_.x + s.x - 0.1f, pos_.y - s.y + 0.2f, pos_.z + s.z + 0.1f)));
                }
            }
        }
    }

    void SortVertices() {
        vertices.Sort((a, b) => FindDegree(feet, b).CompareTo(FindDegree(feet, a)));
    }

    float FindDegree(Vector3 origin, Vector3 v) {
        float value = (Mathf.Atan2(origin.z - v.z, origin.x - v.x) * Mathf.Rad2Deg);
        if (value < 0) value += 360f;
        return value;
    }
    
    Vector2 HVec2(Vector3 v) { return new Vector2(v.x, v.y); }

    bool InView(Vector3 pos_, Vector3 fow, float ax_, Vector3[] angles_) {
        Vector3 tarDir = (pos_ - feet).normalized;
        return (Vector3.Angle(fow, tarDir) < ax_ &&
            tarDir.y > angles_[2].y &&
            tarDir.y < angles_[3].y);
    }

    Vector3 CheckRayCast(Vector3 origin, Vector3 tar) {

        RaycastHit r;
        if (Physics.Raycast(origin, (new Vector3(tar.x, origin.y, tar.z) - origin).normalized, out r, viewRadius, obstacleMask)) {
            return r.point;
        }
        else {
            return new Vector3((origin + ((tar - origin).normalized * viewRadius)).x, origin.y, (origin + ((tar - origin).normalized * viewRadius)).z);
        }
    }

    void DrawFieldOfView(List<Vector3> v) {

        viewMesh.Clear();

        int vertCount = v.Count + 1;
        Vector3[] verts = new Vector3[vertCount];
        int[] tris = new int[(vertCount - 1) * 3];

        verts[0] = new Vector3(0.0f, -this.transform.localScale.y / 2.5f, 0.0f);
        for (int i = 0; i < vertCount - 1; i++) {
            verts[i + 1] = transform.InverseTransformPoint(v[i]);

            if (i < vertCount - 2) {
                tris[i * 3] = 0;
                tris[i * 3 + 1] = i + 1;
                tris[i * 3 + 2] = i + 2;
            }
        }
        
        viewMesh.vertices = verts;
        viewMesh.triangles = tris;
        viewMesh.RecalculateNormals();
    }
}
