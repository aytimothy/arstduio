using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGimbalHandle : ToolHandle {
    public LineRenderer lineRenderer;
    public RotateTool controller => (RotateTool) base.controller;
    public Color highlightColor => controller.highlightColor;
    public Color originalColor;

    protected void Start() {
        originalColor = lineRenderer.materials[0].color;
    }

    public override void SelectedUpdate() {
        Debug.LogWarning("// todo: Actually detect turning.");
    }

    public override void OnSelectOn() {
        lineRenderer.materials[0].color = highlightColor;
    }

    public override void OnSelectOff() {
        lineRenderer.materials[0].color = originalColor;
    }
}
