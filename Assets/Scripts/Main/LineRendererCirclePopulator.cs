using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using JetBrains.Annotations;
using UnityEditor;
#endif

[RequireComponent(typeof(LineRenderer))]
public class LineRendererCirclePopulator : MonoBehaviour {
    public LineRenderer lineRenderer;
    public Axis axis;
    public float step = 5f;
    public float distance = 1.5f;

    List<Vector3> positions;

    void Awake() {
        if (lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();
    }

    public void PopulateRenderer() {
        PopulateRenderer(axis);
    }

    public void PopulateRenderer(Axis axis) {
        Vector3[] results = PrepareOffsets(axis);
        lineRenderer.positionCount = results.Length;
        lineRenderer.SetPositions(results);
    }

    public Vector3[] PrepareOffsets(Axis axis) {
        List<Vector3> results = new List<Vector3>();
        float angle = 0f;
        switch (axis) {
            case Axis.X:
                while (angle < 360f) {
                    results.Add(new Vector3(0, Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * distance);
                    angle += step;
                }
                results.Add(new Vector3(0, Mathf.Cos(0), Mathf.Sin(0)) * distance);
                break;
            case Axis.Y:
                while (angle < 360f) {
                    results.Add(new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * distance);
                    angle += step;
                }
                results.Add(new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0)) * distance);
                break;
            case Axis.Z:
                while (angle < 360f) {
                    results.Add(new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * distance);
                    angle += step;
                }
                results.Add(new Vector3(Mathf.Cos(0), Mathf.Sin(0), 0) * distance);
                break;
            case Axis.W:
                throw new NotSupportedException();
        }
        return results.ToArray();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(LineRendererCirclePopulator))]
public class LineRendererCirclePopulatorEditor : Editor {
    public LineRendererCirclePopulator component => (LineRendererCirclePopulator) target;

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if (GUILayout.Button("Populate"))
            component.PopulateRenderer();
    }
}
#endif