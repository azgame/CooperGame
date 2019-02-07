using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FOVEditor : Editor {

    void OnSceneGUI() {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);

        Vector3 viewAngleA = fov.DirFromAngleX(-fov.fovx / 2.0f, fov.fovy);
        Vector3 viewAngleB = fov.DirFromAngleX(fov.fovx / 2.0f, fov.fovy);
        Vector3 viewAngleC = fov.DirFromAngleY(-fov.fovy / 2.0f);
        Vector3 viewAngleD = fov.DirFromAngleY(fov.fovy / 2.0f);

        Handles.DrawLine(fov.transform.position, fov.transform.position + (viewAngleA * fov.viewRadius));
        Handles.DrawLine(fov.transform.position, fov.transform.position + (viewAngleB * fov.viewRadius));
        Handles.DrawLine(fov.transform.position, fov.transform.position + (viewAngleC * fov.viewRadius));
        Handles.DrawLine(fov.transform.position, fov.transform.position + (viewAngleD * fov.viewRadius));
    }
}
