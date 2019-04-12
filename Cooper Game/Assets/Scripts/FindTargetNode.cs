using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetNode : Node {

    public delegate NodeStates FindTargetNodeDelegate(GameObject ai, LayerMask target, LayerMask obs);

    protected FindTargetNodeDelegate m_action;
    protected GameObject ai;
    private LayerMask target;
    private LayerMask obs;

    public FindTargetNode(FindTargetNodeDelegate action, GameObject ai_, LayerMask target_, LayerMask obs_) 
    {
        m_action = action;
        ai = ai_;
        target = target_;
        obs = obs_;
    }

    public override NodeStates Evaluate() {
        switch (m_action(ai, target, obs)) {
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
