using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace COOPER
{
    public class CameraManager : MonoBehaviour
    {
        public float followSpeed = 8;
        public float mouseSpeed = 2;
        public float controllerSpeed = 7;

        public Transform target;
        public Transform pivot;
        public Transform camTrans;

        float turnSmoothing = .1f;
        public float minAngle = -35;
        public float maxAngle = 35;

        float smoothX;
        float smoothY;
        float smoothXvel;
        float smoothYvel;

        public float lookAngle;
        public float tiltAngle;

        public void Init(Transform t)
        {
            target = t;

            camTrans = Camera.main.transform;
            pivot = camTrans.parent;
        }

        void FollowTarget(float d)
        {
            float speed = d * followSpeed;
            Vector3 targetPos = Vector3.Lerp(transform.position, target.position, speed);
            transform.position = targetPos;
        }

        void HandleRot(float d, float v, float h, float targetSpeed)
        {
            if (turnSmoothing > 0)
            {
                smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXvel, turnSmoothing);
                smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYvel, turnSmoothing);
            }
            else
            {
                smoothX = h;
                smoothY = v;
            }

            lookAngle += smoothX * targetSpeed;
            transform.rotation = Quaternion.Euler(0, lookAngle, 0);

            tiltAngle -= smoothY * targetSpeed;
            tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
            pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
        }

        public void Tick(float d)
        {
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");

            float targetSpeed = mouseSpeed;

            float c_h = Input.GetAxis("Right Joystick X");
            float c_v = Input.GetAxis("Right Joystick Y");

            if (c_h != 0 || c_v != 0)
            {
                h = c_h;
                v = c_v;

                targetSpeed = controllerSpeed;
            }

            FollowTarget(d);
            HandleRot(d, v, h, targetSpeed);
        }

        public static CameraManager singleton;
        void Awake()
        {
            singleton = this;
        }

    }
}