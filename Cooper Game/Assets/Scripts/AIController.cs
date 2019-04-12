using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour {

    List<Node> targetSequenceNodes;
    List<Node> idlePatrolSequenceNodes;
    Selector root;
    Sequence findTargetSequence;
    Sequence randomDestinationSequence;
    public NavMeshAgent agent;
    public LayerMask tarMask;
    public LayerMask obs;
    public float walkRadius;
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
        targetSequenceNodes = new List<Node>();
        idlePatrolSequenceNodes = new List<Node>();
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();

        // Chasing player sequence
        targetSequenceNodes.Add(new FindTargetNode(FindTarget, this.gameObject, tarMask, obs));
        targetSequenceNodes.Add(new FindDestinationNode(FindDestination, this.gameObject, ref agent, tarMask));
        targetSequenceNodes.Add(new WalkNode(MoveTowardsTarget, this.gameObject, ref agent));
        findTargetSequence = new Sequence(targetSequenceNodes);

        // Idle random patrolling sequence
        idlePatrolSequenceNodes.Add(new RandomPatrolNode(RandomPatrol, this.gameObject, ref agent));
        idlePatrolSequenceNodes.Add(new WalkNode(MoveTowardsTarget, this.gameObject, ref agent));
        randomDestinationSequence = new Sequence(idlePatrolSequenceNodes);

        // Add sequences to list for root selector
        List<Node> nodes = new List<Node>();
        nodes.Add(findTargetSequence);
        nodes.Add(randomDestinationSequence);

        // Root
        root = new Selector(nodes);
    }

    void Update() {

        root.Evaluate();

        this.transform.Rotate(0.0f, this.transform.forward.y, 0.0f);
    }


    // Functions to pass to nodes, can be anywhere
    Node.NodeStates FindTarget(GameObject ai, LayerMask tarMask, LayerMask obs) 
    {
        Vector3 pos = ai.transform.position;
        Vector3 forward = ai.transform.forward;

        if (fov.ScanView(pos, forward, tarMask, obs))
            return Node.NodeStates.SUCCESS;
        else
            return Node.NodeStates.FAILURE;
    }

    Node.NodeStates FindDestination(GameObject ai, ref NavMeshAgent agent, LayerMask tarMask) 
    {
        Vector3 pos = ai.transform.position;
        Vector3 destination = fov.FindDestination(pos, tarMask);

        if (destination == pos)
            return Node.NodeStates.FAILURE;
        else {
            agent.SetDestination(destination);
            return Node.NodeStates.SUCCESS;
        }
    }

    Node.NodeStates RandomPatrol(GameObject ai, ref NavMeshAgent agent) 
    {
        Vector3 pos = ai.transform.position;
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;

        randomDirection += pos;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, walkRadius, NavMesh.AllAreas)) {
            agent.SetDestination(hit.position);
            return Node.NodeStates.SUCCESS;
        }
        else {
            return Node.NodeStates.FAILURE;
        }
        
    }

    Node.NodeStates MoveTowardsTarget(GameObject ai, ref NavMeshAgent agent) 
    {
        Vector3 pos = ai.transform.position;

        if (pos == agent.destination)
            return Node.NodeStates.SUCCESS;
        else if (agent.destination == null)
            return Node.NodeStates.FAILURE;
        else
            return Node.NodeStates.RUNNING;
    }
}
