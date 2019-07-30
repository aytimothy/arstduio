using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Extension of ARPointCloud.
/// </summary>
[RequireComponent(typeof(ARPointCloud))]
public class ARPointCloud2 : MonoBehaviour {
    public ARPointCloudParticleVisualizer particleVisualizer;
    public ParticleSystem particles;

    void Awake() {
        if (particleVisualizer == null) particleVisualizer = GetComponent<ARPointCloudParticleVisualizer>();
        if (particles == null) particles = GetComponent<ParticleSystem>();
    }

    void Start() {
        CheckVisualizationSetting();
    }

    public void CheckVisualizationSetting() {
        if (PlayerPrefs.GetInt(SettingsScreen.showVisibleFloorTogglePrefName, SettingsScreen.showVisibleFloorTogglePrefDefaultValue) == 1) {
            ShowVisualization();
            return;
        }
        HideVisualization();
    }

    public void ShowVisualization() {
        particleVisualizer.enabled = true;
        particles.Play();
    }

    public void HideVisualization() {
        particleVisualizer.enabled = false;
        particles.Stop();
        particles.Clear();
    }
}
