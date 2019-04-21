using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace COOPER
{
    public class StateManager : MonoBehaviour
    {
        [Header("Init")]
        public GameObject activeMdl;
        public GrapplingHook gh;
        [HideInInspector]
        public Animator anim;

        [Header("Inputs")]
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public Vector3 moveDir;

        [Header("Stats")]
        public float jumpSpeed = 4.5f;
        public float moveSpeed = 2;
        public float runSpeed = 3.5f;
        public float rotateSpeed = 5;
        public float toGround = 0.5f;
        Vector3 jumpVec;

        [Header("States")]
        public bool run;
        public bool onGround;
        public bool jump;
        public bool wallTouch;

        [HideInInspector]
        public Rigidbody rb;

        [HideInInspector]
        public float delta;
        [HideInInspector]
        public LayerMask ignoreLayers;

        public void Init()
        {
            SetupAnimator();
            rb = GetComponent<Rigidbody>();
            rb.angularDrag = 999;
            rb.drag = 4;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            gameObject.layer = 8;
            ignoreLayers = ~(1 << 9);
            jumpVec.x = 0; jumpVec.y = jumpSpeed; jumpVec.z = 0;

            anim.SetBool("onGround", true);

            wallTouch = false;
            prevGround = true;
        }

        void SetupAnimator()
        {
            if (activeMdl == null)
            {
                anim = GetComponentInChildren<Animator>();
                if (anim == null)
                {
                    Debug.Log("No model");
                }
                else
                {
                    activeMdl = anim.gameObject;
                }
            }

            if (anim == null)
            {
                anim = activeMdl.GetComponent<Animator>();
                if (anim == null)
                    Debug.Log("No Animator Found");
            }


        }

        public void FixedTick(float d)
        {
            delta = d;

            rb.drag = (moveAmount > 0 || onGround == false) ? 0 : 4;

            float targetSpeed = moveSpeed;
            if (run)
                targetSpeed = runSpeed;

            
            // if using a controller than you'll see when you slightly tilt the joystick you'll walk slowly
            if(onGround)
                rb.velocity = moveDir * (targetSpeed * moveAmount);

            Vector3 targetDir = moveDir;
            targetDir.y = 0;
            if (targetDir == Vector3.zero)
                targetDir = transform.forward;
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, delta * moveAmount * rotateSpeed);
            transform.rotation = targetRotation;

            HandleMovementAnimations();
        }

        public void Tick(float d)
        {
            delta = d;
            onGround = OnGround();
            anim.SetBool("onGround", onGround);
        }

        public bool OnGround()
        {
            if (skipGroundCheck)
            {
                skipTimer += delta;
                if (skipTimer > 0.2f)
                    skipGroundCheck = false;
                prevGround = false;
                return false;
            }
            bool r = false;
            skipTimer = 0;
            Vector3 origin = transform.position + (Vector3.up * toGround);
            Vector3 dir = -Vector3.up;
            float dis = toGround + 0.1f;
            RaycastHit hit;
            if (Physics.Raycast(origin, dir, out hit, dis))
            {
                Vector3 targetPosition = hit.point;
                transform.position = targetPosition;
                r = true;
            }
            if(r && !prevGround)
            {
                anim.Play("jump_land");
            }

            prevGround = r;
            return r;
        }

        public 

        bool skipGroundCheck;
        float skipTimer;
        bool prevGround;

        public void Jump()
        {

            anim.Play("jump_launch");
            Vector3 targetVel = transform.forward * 5;
            targetVel.y = 6;
            rb.velocity = targetVel;
            skipGroundCheck = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Wall")
            {
                wallTouch = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Wall")
            {
                wallTouch = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Wall")
            {
                wallTouch = false;
            }
        }


        void HandleMovementAnimations()
        {
            anim.SetFloat("vertical", moveAmount, 0.4f, delta);
            anim.SetBool("run", run);
        }
    }
}