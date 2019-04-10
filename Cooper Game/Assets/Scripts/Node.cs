using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generic root node
[System.Serializable]
public abstract class Node {

    public enum NodeStates {
        FAILURE,
        SUCCESS,
        RUNNING
    }
    
    public delegate NodeStates NodeReturn();

    protected NodeStates m_nodeState;

    public NodeStates nodeState {
        get { return m_nodeState; }
    }

    public Node() {}

    public abstract NodeStates Evaluate();
}
