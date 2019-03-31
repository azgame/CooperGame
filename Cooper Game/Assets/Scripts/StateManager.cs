using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public float horizontal;
    public float vertical;

    public GameObject activeMdl;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Rigidbody rb;

    float delta;

    public void Init()
    {
        SetupAnimator();
        rb = GetComponent<Rigidbody>();
    }

    void SetupAnimator()
    {
        if (activeMdl == null)
        {
            anim = GetComponentInChildren<Animator>();
            if (anim = null)
            {
                Debug.Log("No model!");
            }
            else
            {
                activeMdl = anim.gameObject;
            }
        }

        if (anim = null)
            anim = activeMdl.GetComponent<Animator>();
        if(anim)
        anim.applyRootMotion = false;

    }

    public void Tick(float d)
    {
        delta = d;
    }
}
