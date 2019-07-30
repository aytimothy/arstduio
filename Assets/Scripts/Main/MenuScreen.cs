using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour {
    public SideMenuAnimator sideMenuAnimator;
    public CreateObjectScreen createObjectScreen;
    public ManageObjectsScreen ManageObjectsScreen;
    public ToolInputManager toolInputManager;
    public ToolStateController toolStateController;

    [Header("Tool Buttons")]
    public Button moveToolButton;
    public Button rotateToolButton;
    public Button scaleToolButton;
    public Button poseToolButton;
    public Button gimbalToolButton;
    public Button propertiesToolButton;
    public Button animationToolButton;
    public Button createToolButton;
    public Button duplicateToolButton;
    public TMP_Text globalityText;
    public TMP_Text pivotText;

    void Start() {
        globalityText.text = GetGlobalityStateText(toolStateController.globalityType);
        pivotText.text = GetPivotStateText(toolStateController.pivotType);
        SetupToolButton();
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

    public void CreateNewObjectButton_OnClick() {
        createObjectScreen.Show();
    }

    public void ManageObjectsButton_OnClick() {
        ManageObjectsScreen.Show();
    }

    public void SetupToolButton() {
        ToolState currentToolState = toolStateController.currentToolType;
        moveToolButton.interactable = currentToolState != ToolState.Move;
        rotateToolButton.interactable = currentToolState != ToolState.Rotate;
        scaleToolButton.interactable = currentToolState != ToolState.Scale;
        poseToolButton.interactable = currentToolState != ToolState.Pose;
        gimbalToolButton.interactable = currentToolState != ToolState.Gimbal;
        propertiesToolButton.interactable = currentToolState != ToolState.Properties;
        animationToolButton.interactable = currentToolState != ToolState.Animation;
        createToolButton.interactable = currentToolState != ToolState.Create;
        duplicateToolButton.interactable = currentToolState != ToolState.Duplicate;
        globalityText.text = GetGlobalityStateText(toolStateController.globalityType);
        pivotText.text = GetPivotStateText(toolStateController.pivotType);
    }

    public void MoveToolButton_OnClick() {
        toolStateController.SwitchTools(ToolState.Move);
        moveToolButton.interactable = false;
        rotateToolButton.interactable = true;
        scaleToolButton.interactable = true;
        poseToolButton.interactable = true;
        gimbalToolButton.interactable = true;
        propertiesToolButton.interactable = true;
        animationToolButton.interactable = true;
        createToolButton.interactable = true;
        duplicateToolButton.interactable = true;
    }

    public void RotateToolButton_OnClick() {
        toolStateController.SwitchTools(ToolState.Rotate);
        moveToolButton.interactable = true;
        rotateToolButton.interactable = false;
        scaleToolButton.interactable = true;
        poseToolButton.interactable = true;
        gimbalToolButton.interactable = true;
        propertiesToolButton.interactable = true;
        animationToolButton.interactable = true;
        createToolButton.interactable = true;
        duplicateToolButton.interactable = true;
    }

    public void ScaleToolButton_OnClick() {
        toolStateController.SwitchTools(ToolState.Scale);
        moveToolButton.interactable = true;
        rotateToolButton.interactable = true;
        scaleToolButton.interactable = false;
        poseToolButton.interactable = true;
        gimbalToolButton.interactable = true;
        propertiesToolButton.interactable = true;
        animationToolButton.interactable = true;
        createToolButton.interactable = true;
        duplicateToolButton.interactable = true;
    }

    public void PoseToolButton_OnClick() {
        toolStateController.SwitchTools(ToolState.Pose);
        moveToolButton.interactable = true;
        rotateToolButton.interactable = true;
        scaleToolButton.interactable = true;
        poseToolButton.interactable = false;
        gimbalToolButton.interactable = true;
        propertiesToolButton.interactable = true;
        animationToolButton.interactable = true;
        createToolButton.interactable = true;
        duplicateToolButton.interactable = true;
    }

    public void GimbalToolButton_OnClick() {
        toolStateController.SwitchTools(ToolState.Gimbal);
        moveToolButton.interactable = true;
        rotateToolButton.interactable = true;
        scaleToolButton.interactable = true;
        poseToolButton.interactable = true;
        gimbalToolButton.interactable = false;
        propertiesToolButton.interactable = true;
        animationToolButton.interactable = true;
        createToolButton.interactable = true;
        duplicateToolButton.interactable = true;
    }

    public void PropertiesToolButton_OnClick() {
        toolStateController.SwitchTools(ToolState.Properties);
        moveToolButton.interactable = true;
        rotateToolButton.interactable = true;
        scaleToolButton.interactable = true;
        poseToolButton.interactable = true;
        gimbalToolButton.interactable = true;
        propertiesToolButton.interactable = false;
        animationToolButton.interactable = true;
        createToolButton.interactable = true;
        duplicateToolButton.interactable = true;
    }

    public void AnimateToolButton_OnClick() {
        toolStateController.SwitchTools(ToolState.Animation);
        moveToolButton.interactable = true;
        rotateToolButton.interactable = true;
        scaleToolButton.interactable = true;
        poseToolButton.interactable = true;
        gimbalToolButton.interactable = true;
        propertiesToolButton.interactable = true;
        animationToolButton.interactable = false;
        createToolButton.interactable = true;
        duplicateToolButton.interactable = true;
    }

    public void AddToolButton_OnClick() {
        toolStateController.SwitchTools(ToolState.Create);
        moveToolButton.interactable = true;
        rotateToolButton.interactable = true;
        scaleToolButton.interactable = true;
        poseToolButton.interactable = true;
        gimbalToolButton.interactable = true;
        propertiesToolButton.interactable = true;
        animationToolButton.interactable = true;
        createToolButton.interactable = false;
        duplicateToolButton.interactable = true;
    }

    public void DuplicateToolButton_OnClick() {
        toolStateController.SwitchTools(ToolState.Move);
        moveToolButton.interactable = true;
        rotateToolButton.interactable = true;
        scaleToolButton.interactable = true;
        poseToolButton.interactable = true;
        gimbalToolButton.interactable = true;
        propertiesToolButton.interactable = true;
        animationToolButton.interactable = true;
        createToolButton.interactable = true;
        duplicateToolButton.interactable = false;
    }

    public void PivotToggleButton_OnClick() {
        PivotState newState = toolStateController.TogglePivotState();
        pivotText.text = GetPivotStateText(newState);
    }

    public string GetPivotStateText(PivotState state) {
        switch (state) {
            case PivotState.Pivot:
                return "Origin";
            case PivotState.Center:
                return "Center";
        }

        return "Unknown";
    }

    public void GlobalityToggleButton_OnClick() {
        GlobalityState newState = toolStateController.ToggleGlobalityState();
        globalityText.text = GetGlobalityStateText(newState);
    }

    public string GetGlobalityStateText(GlobalityState state) {
        switch (state) {
            case GlobalityState.Global:
                return "Global";
            case GlobalityState.Local:
                return "Local";
        }

        return "Unknown";
    }
}
