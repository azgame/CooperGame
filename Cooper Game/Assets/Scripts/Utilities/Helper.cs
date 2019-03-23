﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    [Range(0, 1)]
    public float vertical;

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        anim.SetFloat("vertical", vertical);
    }
}
