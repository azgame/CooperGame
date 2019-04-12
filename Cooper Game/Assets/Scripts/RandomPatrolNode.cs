using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomPatrolNode : Node {

    public delegate NodeStates RandomPatrolNodeDelegate(GameObject ai, ref NavMeshAgent agent);

    protected RandomPatrolNodeDelegate m_action;
    protected GameObject ai;
    private NavMeshAgent agent;

    public RandomPatrolNode(RandomPatrolNodeDelegate action, GameObject ai_, ref NavMeshAgent agent_) {
        m_action = action;
        ai = ai_;
        agent = agent_;
    }

    public override NodeStates Evaluate() {
        switch (m_action(ai, ref agent)) {
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
