using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour {

    
    public NavMeshAgent agent;
    public LayerMask tarMask;
    public LayerMask obs;
    FieldOfView fov;
    Vector3 destination;

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

        if (fov.ScanView(this.transform.position, this.transform.forward, tarMask, obs))
            agent.SetDestination(fov.FindDestination(this.transform.position, tarMask));
        else
            agent.SetDestination(this.transform.position);

        this.transform.Rotate(0.0f, this.transform.forward.y, 0.0f);
    }
}
