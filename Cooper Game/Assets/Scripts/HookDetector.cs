using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace COOPER
{
    public class HookDetector : MonoBehaviour
    {
        public GrapplingHook gh;
        //void Start()
        //{

        //}

        //void Update()
        //{

        //}

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Ledge")
            {
                gh.hooked = true;
                gh.hookedObj = other.gameObject;
            }
        }

    }
}