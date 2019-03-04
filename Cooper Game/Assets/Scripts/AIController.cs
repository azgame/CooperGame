using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour {


    public NavMeshAgent agent;
    public Vector3 destination;
    public Transform target;
    FieldOfView fov;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
    }
	
	
    void Update() {

        destination.x = target.position.x;
        destination.y = this.transform.position.y;
        destination.z = target.position.z;

        if (fov.tarSeen) {
            agent.SetDestination(destination);
        }
        else {
            agent.SetDestination(this.transform.position);
        }

        this.transform.Rotate(0.0f, this.transform.forward.y, 0.0f);
    }
}
