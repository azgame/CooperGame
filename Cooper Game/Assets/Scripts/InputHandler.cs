using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    float vert;
    float hori;

    StateManager state;
    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<StateManager>();
        state.Init();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
    }

    void UpdateState()
    {
        state.horizontal = hori;
        state.vertical = vert;
        state.Tick(Time.deltaTime);
    }
}
