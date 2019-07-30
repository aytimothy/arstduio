using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class SettingsScreen : MonoBehaviour  {
    public SideMenuAnimator sideMenuAnimator;

    public CreateProjectScreen createProjectScreen;
    public OpenProjectScreen openProjectScreen;
    public SaveProjectScreen saveProjectScreen;
    public CreditsScreen creditsScreen;
    public ARSessionManager sessionManager;
    public ToolInputManager toolInputManager;
    public ToolController toolController;

    public Toggle showVisibleFloorToggle;
    public Toggle recordEditorToggle;

    public void Start() {
        ReadSettings();
    }

    public void Show(bool isTopMenuCall = false) {
        sideMenuAnimator.Show();
        if (isTopMenuCall) toolInputManager.screenVisible = true;
    }

    public void Hide(bool isTopMenuCall = false) {
        sideMenuAnimator.Hide();
        if (isTopMenuCall) toolInputManager.screenVisible = false;
        PlayerPrefs.Save();
    }

    public void NewProjectButton_OnClick() {
        createProjectScreen.Show();
    }

    public void OpenProjectButton_OnClick() {
        openProjectScreen.Show();
    }

    public void SaveProjectButton_OnClick() {
        saveProjectScreen.Show();
    }

    public void SaveProjectAsButton_OnClick() {
        saveProjectScreen.Show();
    }

    public void ResetTrackingButton_OnClick() {
        sessionManager.Reset();
    }

    public void ShowCreditsButton_OnClick() {
        creditsScreen.Show();
    }

    public const string showVisibleFloorTogglePrefName = "pref_show_visible_floor";
    public const string showEditorElementsInRecordingPrefName = "pref_editor_elements_in_recording";
    public const int showVisibleFloorTogglePrefDefaultValue = 1;
    public const int showEditorElementsTogglePrefDefaultValue = 1;
    public void ReadSettings() {
        showVisibleFloorToggle.isOn = PlayerPrefs.GetInt(showVisibleFloorTogglePrefName, showVisibleFloorTogglePrefDefaultValue) == 1;
        recordEditorToggle.isOn = PlayerPrefs.GetInt(showEditorElementsInRecordingPrefName, showEditorElementsTogglePrefDefaultValue) == 1;
    }

    public void ShowVisibleFloorToggle_OnValueChange(bool newValue) {
        ARPlaneManager planeManager = sessionManager.sessionObject.GetComponent<ARPlaneManager>();
        if (planeManager != null)
            foreach (ARPlane plane in planeManager.trackables) {
                ARPlane2 customComponent = plane.GetComponent<ARPlane2>();
                if (customComponent == null) continue;
                if (newValue) customComponent.ShowVisualization();
                if (!newValue) customComponent.HideVisualization();
            }

        ARPointCloudManager pointCloudManager = sessionManager.sessionObject.GetComponent<ARPointCloudManager>();
        if (pointCloudManager != null)
            foreach (ARPointCloud pointCloud in pointCloudManager.trackables) {
                ARPointCloud2 customComponent = pointCloud.GetComponent<ARPointCloud2>();
                if (customComponent == null) continue;
                if (newValue) customComponent.ShowVisualization();
                if (!newValue) customComponent.HideVisualization();
            }

        if (toolController.currentToolObject != null) {
            if (newValue) toolController.currentToolObject.ShowVisuals();
            if (!newValue) toolController.currentToolObject.HideVisuals();
        }
        PlayerPrefs.SetInt(showVisibleFloorTogglePrefName, (newValue) ? 1 : 0);
    }

    public void ShowEditorElementsInRecordingToggle_OnValueChange(bool newValue) {
        Debug.LogError("// todo: Actually toggle.");
        PlayerPrefs.SetInt(showEditorElementsInRecordingPrefName, (newValue) ? 1 : 0);
    }
}
