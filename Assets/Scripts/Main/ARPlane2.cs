using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// An extension of ARPlane.
/// </summary>
[RequireComponent(typeof(ARPlane))]
public class ARPlane2 : MonoBehaviour {
    public ARPlaneMeshVisualizer meshVisualizer;
    public LineRenderer lineRenderer;
    public MeshRenderer meshRenderer;

    void Awake() {
        if (meshVisualizer == null) meshVisualizer = GetComponent<ARPlaneMeshVisualizer>();
        if (lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();
        if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start() {
        CheckVisualizationSetting();

        if (meshRenderer == null)
            Debug.LogWarning("ARPlane is missing a XR Managed Mesh Visualizer...");
        if (lineRenderer == null)
            Debug.LogWarning("ARPlane is missing a line renderer...");
        if (meshRenderer == null)
            Debug.LogWarning("ARPlane is missing a mesh renderer...");
    }

    public void CheckVisualizationSetting() {
        if (PlayerPrefs.GetInt(SettingsScreen.showVisibleFloorTogglePrefName, SettingsScreen.showVisibleFloorTogglePrefDefaultValue) == 1) {
            ShowVisualization();
            return;
        }
        HideVisualization();
    }

    public void ShowVisualization() {
        meshVisualizer.enabled = true;
        meshRenderer.enabled = true;
        lineRenderer.enabled = true;
    }

    public void HideVisualization() {
        meshVisualizer.enabled = false;
        meshRenderer.enabled = false;
        lineRenderer.enabled = false;
    }
}
