using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    float vert;
    float hori;
    float delta;

    StateManager state;
    CameraManager camera;
    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<StateManager>();
        state.Init();

        camera = CameraManager.singleton;
        camera.Init(this.transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        delta = Time.fixedDeltaTime;
        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
    }

    void Update()
    {
        delta = Time.deltaTime;
        camera.Tick(delta);
    }

    void UpdateState()
    {
        state.horizontal = hori;
        state.vertical = vert;
        state.Tick(delta);
    }
}
