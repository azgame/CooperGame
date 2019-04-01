using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace COOPER
{
    public class InputHandler : MonoBehaviour
    {
        float vert;
        float hori;
        float delta;
        bool sprint;

        StateManager state;
        CameraManager camManager;
        // Start is called before the first frame update
        void Start()
        {
            state = GetComponent<StateManager>();
            state.Init();

            camManager = CameraManager.singleton;
            camManager.Init(this.transform);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            GetInput();
            UpdateState();
            state.FixedTick(delta);
            camManager.Tick(delta);
        }

        void GetInput()
        {
            hori = Input.GetAxis("Horizontal");
            vert = Input.GetAxis("Vertical");
            sprint = Input.GetButton("Sprint");
        }

        void Update()
        {
            delta = Time.deltaTime;
            state.Tick(delta);
        }

        void UpdateState()
        {
            state.horizontal = hori;
            state.vertical = vert;

            Move();
            
        }

        void Move()
        {
            Vector3 v = state.vertical * camManager.transform.forward;
            Vector3 h = state.horizontal * camManager.transform.right;
            state.moveDir = (v + h).normalized;

            float m = Mathf.Abs(hori) + Mathf.Abs(vert);
            state.moveAmount = Mathf.Clamp01(m);

            if (sprint)
            {
                state.run = (state.moveAmount > 0);
            } else
            {
                state.run = false;
            }
        }
    }
}