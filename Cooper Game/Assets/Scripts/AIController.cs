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


    // AI Pipeline
    /// <summary>
    /// AI input - check view for inputs
    /// AI input - check sound map for inputs
    /// Organize input, state, and location
    /// Given state, what decision path should we take?
    /// Decision function
    /// Ai output - given decision, decide state, location to move to, action to take
    /// </summary>

    
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
