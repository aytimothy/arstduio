using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This is the state machine that waits for all other FlowEventNode(s) to fire before it itself calls its delegated events.
/// </summary>
public class FlowEvent : MonoBehaviour {
    public List<FlowEventNode> nodes;
    public UnityEvent onActivate;
    public bool invokeOnlyOnce;
    public bool activated {
        get {
            foreach (FlowEventNode node in nodes)
                if (!node.activated)
                    return false;
            return true;
        }
        set {
            if (value)
                foreach (FlowEventNode node in nodes)
                    node.Activate();
            if (!value)
                foreach (FlowEventNode node in nodes)
                    node.Deactivate();
        }
    }

    private bool eventInvoked;

	protected virtual void Update() {
        if ((!eventInvoked || !invokeOnlyOnce) && activated) {
            onActivate.Invoke();
            eventInvoked = true;
        }
    }
}
