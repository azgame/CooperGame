using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generic leaf node
public class ActionNode : Node {

    public delegate NodeStates ActionNodeDelegate();

    protected ActionNodeDelegate m_action;
    protected GameObject ai;

    public ActionNode(ActionNodeDelegate action, GameObject ai_) 
    {
        m_action = action;
        ai = ai_;
    }

    public override NodeStates Evaluate() 
    {
        switch (m_action()) 
        {
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
