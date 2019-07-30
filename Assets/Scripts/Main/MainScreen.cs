using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class MainScreen : MonoBehaviour {
    public MenuScreen menuScreen;
    public SettingsScreen settingsScreen;
    public CreateProjectScreen createProjectScreen;
    public OpenProjectScreen openProjectScreen;
    public SaveProjectScreen saveProjectScreen;
    public CreditsScreen creditsScreen;
    public CreateObjectScreen createObjectScreen;
    public ManageObjectsScreen manageObjectsScreen;
    public ObjectPropertiesScreen objectPropertiesScreen;
    public ObjectAnimationScreen objectAnimationScreen;
    public ARSessionManager arManager;
    public LoggerScreen logScreen;
    public AnimatorScreen animatorScreen;

    public PictureRenderer pictureRenderer;
    void Start() {
        menuScreen.gameObject.SetActive(true);
        settingsScreen.gameObject.SetActive(true);
        createProjectScreen.gameObject.SetActive(true);
        openProjectScreen.gameObject.SetActive(true);
        saveProjectScreen.gameObject.SetActive(true);
        creditsScreen.gameObject.SetActive(true);
        createObjectScreen.gameObject.SetActive(true);
        manageObjectsScreen.gameObject.SetActive(true);
        objectPropertiesScreen.gameObject.SetActive(true);
        objectAnimationScreen.gameObject.SetActive(true);
        logScreen.gameObject.SetActive(true);
        animatorScreen.gameObject.SetActive(true);
    }

    public void CaptureButton_OnClick() {
        string fileName = DateTime.Now.ToString("ddMMyyyyHHmmssSSSS") + ".png";
        string filePath = Application.persistentDataPath + "/" + fileName;
        Debug.Log("Saved screenshot to: " + filePath);
        pictureRenderer.SaveToFile(filePath);
    }

    public void MenuButton_OnClick() {
        menuScreen.Show(true);
    }

    public void SettingsButton_OnClick() {
        settingsScreen.Show(true);
    }

    public void SwitchCameraButton_OnClick() {
        arManager.ToggleSessionType();
    }
}
