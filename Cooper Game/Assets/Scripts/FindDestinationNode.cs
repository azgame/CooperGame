using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindDestinationNode : Node {

    public delegate NodeStates FindDestinationNodeDelegate(GameObject ai, ref NavMeshAgent agent, LayerMask target);

    protected FindDestinationNodeDelegate m_action;
    protected GameObject ai;
    private LayerMask target;
    private NavMeshAgent agent;

    public FindDestinationNode(FindDestinationNodeDelegate action, GameObject ai_, ref NavMeshAgent agent_, LayerMask target_) {
        m_action = action;
        ai = ai_;
        target = target_;
        agent = agent_;
    }

    public override NodeStates Evaluate() {
        switch (m_action(ai, ref agent, target)) {
            case NodeStates.SUCCESS:
                m_nodeState = NodeStates.SUCCESS;
                return m_nodeState;
            case NodeStates.FAILURE:
                m_nodeState = NodeStates.FAILURE;
                return m_nodeState;
            case NodeStates.RUNNING:
                m_nodeState = NodeStates.RUNNING;
                return m_nodeState;
            default:
                m_nodeState = NodeStates.FAILURE;
                return m_nodeState;
        }
    }
}
