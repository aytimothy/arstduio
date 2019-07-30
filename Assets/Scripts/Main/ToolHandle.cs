using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ToolHandle : MonoBehaviour {
    public Tool controller;
    public Camera camera => controller.camera;
    [Tooltip("Higher numbers take precedence over those with lower values.")]
    public int priority;            // priority over other handles (for overlaps)
    public bool isSelected;         // is it currently being dragged around? Set by ToolInputManager.
    protected bool _isSelected;     // hidden state of isSelected

    /// <summary>
    /// Update() call, but only called when the handle is currently dragged.
    /// </summary>
    public abstract void SelectedUpdate();
    /// <summary>
    /// Called on the first frame the handle has started being dragged around.
    /// </summary>
    public abstract void OnSelectOn();
    /// <summary>
    /// Called on the first frame the handle has stopped being dragged around; has been released.
    /// </summary>
    public abstract void OnSelectOff();

    protected virtual void Update() {
        ProcessToolUpdate();
    }

    void ProcessToolUpdate() {
        if (_isSelected != isSelected) {
            _isSelected = isSelected;
            if (isSelected) OnSelectOn();
            if (!isSelected) OnSelectOff();
        }
        if (_isSelected) SelectedUpdate();
    }
}
