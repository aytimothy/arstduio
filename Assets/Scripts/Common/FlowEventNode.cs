using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This is the event activators that FlowEvent waits for activation.
/// </summary>
public class FlowEventNode : MonoBehaviour {
    public bool activateOnStart;
    public bool activateOnEnable;
    public bool invokeOnceOnly;
    private bool eventInvoked;
    public bool activated { get; private set; }
    public UnityEvent onActivate;

    void Start() {
        if (activateOnStart)
            Activate();
    }

    void OnEnable() {
        if (activateOnEnable)
            Activate();
    }

    public void Activate() {
        if (!activated && (!invokeOnceOnly || !eventInvoked)) {
            eventInvoked = true;
            onActivate.Invoke();
        }
        activated = true;
    }

    public void Deactivate() {
        activated = false;
    }

    public void Dispose(bool destroyGameObject = false) {
        if (destroyGameObject)
            Destroy(gameObject);
        if (!destroyGameObject)
            Destroy(this);
    }
}
