using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class FieldOfView : MonoBehaviour {


    [Range(0, 360)]
    public float fovx;
    [Range(0, 360)]
    public float fovy;
    [Range(0, 50)]
    public float viewRadius;

    Vector3 pos;
    Vector3 forward;
    Vector3[] angles;

    public Vector3[] Angles {
        get { return angles; }
    }

    public Vector3 DirFromAngleX(float ax_, float ay_) {
        ax_ += this.transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(ax_ * Mathf.Deg2Rad), 0.0f, Mathf.Cos(ax_ * Mathf.Deg2Rad));
    }

    public Vector3 DirFromAngleY(float ay_) {
        float ax_ = this.transform.eulerAngles.y;
        ay_ += this.transform.eulerAngles.x;
        return new Vector3(Mathf.Sin(ax_ * Mathf.Deg2Rad), Mathf.Sin(ay_ * Mathf.Deg2Rad), Mathf.Cos(ax_ * Mathf.Deg2Rad));
    }

    void Update() {
        pos = this.transform.position;
        forward = this.transform.forward;
        angles = CreateFrustum(this.fovx, this.fovy);
    }

    public Vector3[] CreateFrustum(float fovx_, float fovy_) {
        angles = new Vector3[] { DirFromAngleX(-fovx_ / 2.0f, fovy_),
                                 DirFromAngleX(fovx_ / 2.0f, fovy_),
                                 DirFromAngleY(-fovy_ / 2.0f),
                                 DirFromAngleY(fovy_ / 2.0f) };
        return angles;
    }

    public bool ScanView(Vector3 pos_, Vector3 forward_, LayerMask tarMask_, LayerMask obs_) {

        Collider[] tarInView = Physics.OverlapSphere(pos_, viewRadius, tarMask_);

        foreach(Collider c in tarInView)
        {
            if (InView(pos_, c.transform.position, forward_, fovx, angles))
            {
                float tarDist = Vector3.Distance(pos_, c.transform.position);
                if (!Physics.Raycast(pos_, c.transform.position, tarDist, obs_))
                {
                    Debug.DrawLine(pos_, c.transform.position, Color.yellow);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        return false;
    }

    bool InView(Vector3 pos_, Vector3 tar_, Vector3 forward_, float ax_, Vector3[] angles_) {
        Vector3 tarDir = (tar_ - pos_).normalized;
        return (Vector3.Angle(forward_, tarDir) < ax_ &&
            tarDir.y > angles_[2].y &&
            tarDir.y < angles_[3].y);
    }

    public Vector3 FindDestination(Vector3 pos_, LayerMask tarMask_) {

        Collider[] c = Physics.OverlapSphere(pos_, viewRadius, tarMask_);

        foreach(Collider t in c) 
        {
            RaycastHit r;
            if (Physics.Raycast(pos_, (new Vector3(t.transform.position.x, pos_.y, t.transform.position.z) - pos_).normalized, out r, viewRadius))
                return new Vector3(r.point.x, pos_.y, r.point.z);
            else
                return pos_;
        }

        return pos_;
    }
}
