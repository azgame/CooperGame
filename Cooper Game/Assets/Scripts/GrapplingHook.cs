using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace COOPER
{
    public class GrapplingHook : MonoBehaviour
    {
        [Header("Hooks")]
        public GameObject hook;
        public GameObject hookHolder;
        public CameraManager cam;
        public StateManager stateM;
        public GameObject hookedObj;
        [Header("Stats")]
        public float hookTravelSpeed;
        public float playerTravelSpeed;
        public float maxDistance;
        float currentDistance;
        [Header("Bools")]
        public bool hooked;
        public bool fired;
        Vector3 forward;
        Vector3 lScale;
        LineRenderer rope;

        void Start()
        {
            hooked = false;
            fired = false;


            forward.x = 0;
            forward.y = 0;
            forward.z = 0;
            lScale = hook.transform.localScale;
            rope = GetComponent<LineRenderer>();
        }


        void Update()
        {
            if (Input.GetButton("Fire1") && fired == false)
            {
                fired = true;
                forward = cam.forward;
            }

            if (fired)
            {
                rope.SetPosition(0, hookHolder.transform.position);
                rope.SetPosition(1, hook.transform.position);
                rope.enabled = true;
            } 

            if (fired && !hooked)
            {
                hook.transform.Translate(forward * Time.deltaTime * hookTravelSpeed);
                currentDistance = Vector3.Distance(transform.position, hook.transform.position);

                if (currentDistance >= maxDistance && !hooked)
                    ReturnHook();
            }

            if (hooked && fired)
            {
                hook.transform.parent = hookedObj.transform;
                transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, playerTravelSpeed * Time.deltaTime);
                float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

                this.GetComponent<Rigidbody>().useGravity = false;

                if (distanceToHook < 1.1)
                {
                    if (!stateM.onGround)
                    {
                        this.transform.Translate(Vector3.forward * Time.deltaTime * 7f);
                        this.transform.Translate(Vector3.up * Time.deltaTime * 10f);
                    }

                    StartCoroutine("Climb");
                }
            } else
            {
                hook.transform.parent = hookHolder.transform;
                this.GetComponent<Rigidbody>().useGravity = true;
            }
            hook.transform.localScale = lScale;
        }

        IEnumerator Climb()
        {
            yield return new WaitForSeconds(0.1f);
            ReturnHook();
        }

        void ReturnHook()
        {
            hook.transform.rotation = hookHolder.transform.rotation;
            hook.transform.position = hookHolder.transform.position;
            fired = false;
            hooked = false;

            rope.enabled = false;
        }
    }
}
