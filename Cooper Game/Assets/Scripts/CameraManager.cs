using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public void Init()
    {

    }

    public void Tick(float d)
    {
        float h = Input.GetAxis("");
    }

    public static CameraManager singleton;
    void Awake()
    {
        singleton = this;
    }

}
