using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotateAxisHandle : ToolHandle {
    public RotateTool controller => (RotateTool) base.controller;
    public Color originalColor;
    public Color highlightColor => controller.highlightColor;
    public LineRenderer lineRenderer;
    public Axis axis;
    private List<Vector3> offsets;

    public Vector3 originalScale;

    void Start() {
        originalColor = lineRenderer.materials[0].color;
        offsets = new List<Vector3>(PrepareOffsets(axis));
    }

    public Vector3[] PrepareOffsets(Axis axis) {
        List<Vector3> results = new List<Vector3>();
        float angle = 0f;
        float step = 5f;
        float radius = controller.radius;
        switch (axis) {
            case Axis.X:
                while (angle < 360f) {
                    results.Append(new Vector3(0, Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius);
                    angle += step;
                }
                results.Append(new Vector3(0, Mathf.Cos(0), Mathf.Sin(0)) * radius);
                break;
            case Axis.Y:
                while (angle < 360f) {
                    results.Append(new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * radius);
                    angle += step;
                }
                results.Append(new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0)) * radius);
                break;
            case Axis.Z:
                while (angle < 360f) {
                    results.Append(new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius);
                    angle += step;
                }
                results.Append(new Vector3(Mathf.Cos(0), Mathf.Sin(0), 0) * radius);
                break;
            case Axis.W:
                throw new NotSupportedException();
        }
        return results.ToArray();
    }

    protected override void Update() {
        base.Update();
        if (axis != Axis.W && axis != Axis.None) {
            Vector3[] points = offsets.ToArray();
            for (int i = 0; i < points.Length; i++) points[i] += transform.position;
            lineRenderer.positionCount = points.Length;
            lineRenderer.SetPositions(points);
        }
        if (axis == Axis.W) {
            // todo.
            // throw new NotImplementedException();
        }
    }

    public override void SelectedUpdate() {
        Debug.LogWarning("// todo: Actually detect turning.");
    }

    public override void OnSelectOn() {
        lineRenderer.materials[0].color = highlightColor;
        originalScale = controller.slave.transform.localScale;
    }

    public override void OnSelectOff() {
        lineRenderer.materials[0].color = originalColor;
    }
}
