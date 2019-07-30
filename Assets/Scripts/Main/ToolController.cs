using System;
using System.Collections.Generic;
#if UNITY_ANDROID || UNITY_IOS
using System.Management.Instrumentation;
#endif
using UnityEngine;
using UnityEngine.Events;

public class ToolController : MonoBehaviour {
    [Header("Prefab References")]
    public GameObject transformToolPrefab;
    public GameObject rotateToolPrefab;
    public GameObject scaleToolPrefab;
    public GameObject gimbalToolPrefab;
    public GameObject poseToolPrefab;
    public GameObject inspectToolPrefab;
    public GameObject createToolPrefab;
    public GameObject duplicateToolPrefab;
    public GameObject animateToolPrefab;

    public Tool currentToolObject;

    // References can't be set into the instances of tools because reasons.
    [Header("Tool References")]
    public AnimatorScreen animatorScreen;
    public ObjectPropertiesScreen propertiesScreen;
    public Camera activeCamera => ARSessionManager.instance.cam.camera;

    public ARObject currentSelectedObject {
        get { return _currentSelectedObject; }
        set {
            if (value == _currentSelectedObject) return;
            ARObject previousObject = _currentSelectedObject;
            _currentSelectedObject = value;
            toolInputManager.onActiveObjectChanged.Invoke(previousObject, value);
        }
    }
    [SerializeField]
    ARObject _currentSelectedObject;

    public ToolState currentTool {
        get { return toolStateController.currentToolType; }
        set { toolStateController.SwitchTools(value); }
    }
    public GlobalityState currentGlobalit {
        get { return toolStateController.globalityType; }
        set { if (toolStateController.globalityType != value) toolStateController.ToggleGlobalityState(); }
    }
    public PivotState currentPivotState {
        get { return toolStateController.pivotType; }
        set { if (toolStateController.pivotType != value) toolStateController.TogglePivotState(); }
    }
    public ToolStateController toolStateController;
    public ToolInputManager toolInputManager;

    [Header("Debug Settings")]
    public bool printOnObjectChanged = false;

    void Start() {
        SetupEventListeners();
    }

    void SetupEventListeners() {
        toolStateController.OnToolChange.AddListener(OnToolChange);
        toolStateController.OnGlobalityChange.AddListener(OnGlobalityChange);
        toolStateController.OnPivotChange.AddListener(OnPivotChange);
        toolInputManager.onActiveObjectChanged.AddListener(OnTargetChanged);
    }

    public void CreateTool(ARObject slave, ToolState tool) {
        CreateTool(tool, slave);
    }

    public void CreateTool(ToolState tool, ARObject slave) {
        if (slave == null) return;

        GameObject newToolGameObject = null;
        switch (tool) {
            case ToolState.None:
                return;
            case ToolState.Move:
                newToolGameObject = Instantiate(transformToolPrefab);
                break;
            case ToolState.Rotate:
                newToolGameObject = Instantiate(rotateToolPrefab);
                break;
            case ToolState.Scale:
                newToolGameObject = Instantiate(scaleToolPrefab);
                break;
            case ToolState.Pose:
                newToolGameObject = Instantiate(poseToolPrefab);
                break;
            case ToolState.Gimbal:
                newToolGameObject = Instantiate(gimbalToolPrefab);
                break;
            case ToolState.Properties:
                newToolGameObject = Instantiate(inspectToolPrefab);
                break;
            case ToolState.Animation:
                newToolGameObject = Instantiate(animateToolPrefab);
                break;
            case ToolState.Create:
                newToolGameObject = Instantiate(createToolPrefab);
                break;
            case ToolState.Duplicate:
                newToolGameObject = Instantiate(duplicateToolPrefab);
                break;
        }

        if (newToolGameObject == null) {
            Debug.LogError("No Prefab detected!");
            currentTool = ToolState.None;
            return;
        }

        Tool toolComponent = newToolGameObject.GetComponent<Tool>();

        if (toolComponent == null) {
            Debug.LogError("Incompatible prefab detected!");
            currentTool = ToolState.None;
            return;
        }

        toolComponent.controller = this;
        toolComponent.globalInputManager = toolInputManager;
        toolComponent.slave = slave;
        toolComponent.stateManager = toolStateController;
        toolComponent.camera = activeCamera;
        currentToolObject = toolComponent;
    }

    public void ClearTool() {
        if (currentToolObject != null)
            Destroy(currentToolObject.gameObject);
    }

    public void OnTargetChanged(ARObject oldTarget, ARObject newTarget) {
        if (printOnObjectChanged)
            PrintTargetChanged(oldTarget, newTarget);
        ClearTool();
        CreateTool(newTarget, currentTool);
    }

    public void PrintTargetChanged(ARObject oldTarget, ARObject newTarget) {
        if (oldTarget == null && newTarget == null)
            Debug.Log("Nothing -> Nothing");
        if (oldTarget == null && newTarget != null)
            Debug.Log("Nothing -> " + newTarget.name);
        if (oldTarget != null && newTarget == null)
            Debug.Log(oldTarget.name + " -> Nothing");
        if (oldTarget != null && newTarget != null)
            Debug.Log(oldTarget.name + " -> " + newTarget.name);
    }

    public void OnToolChange(ToolState oldState, ToolState newState) {
        ClearTool();
        CreateTool(currentSelectedObject, newState);
    }

    public void OnPivotChange(PivotState oldState, PivotState newState) {
        if (currentToolObject != null)
            currentToolObject.OnPivotChange(oldState, newState);
    }

    public void OnGlobalityChange(GlobalityState oldState, GlobalityState newState) {
        if (currentToolObject != null)
            currentToolObject.OnGlobalityChange(oldState, newState);
    }
}
