using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for any tool.
/// </summary>
// If you want to change the position of this thing, you have to affect the ARObject "slave".
public abstract class Tool : MonoBehaviour {
    public ARObject slave;
    public ToolController controller;
    public ToolStateController stateManager;
    public ToolInputManager globalInputManager;
    public Camera camera;

    protected virtual void Start() {
        OnGlobalityChange(stateManager.globalityType, stateManager.globalityType);
        OnPivotChange(stateManager.pivotType, stateManager.pivotType);
        bool visible = PlayerPrefs.GetInt(SettingsScreen.showEditorElementsInRecordingPrefName) == 1;
        if (visible) ShowVisuals();
        if (!visible) HideVisuals();
    }

    protected GlobalityState _globality;
    protected PivotState _pivotState;
    protected Renderer objectRenderer;
    protected virtual void Update() {
        switch (_globality) {
            case GlobalityState.Global:
                transform.rotation = Quaternion.identity;
                break;
            case GlobalityState.Local:
                transform.rotation = slave.transform.rotation;
                break;
        }

        switch (_pivotState) {
            case PivotState.Pivot:
                transform.position = slave.transform.position;
                break;
            case PivotState.Center:
                if (objectRenderer == null) goto case PivotState.Pivot;
                transform.position = objectRenderer.bounds.center;
                break;
        }
    }

    public virtual void OnGlobalityChange(GlobalityState oldState, GlobalityState newState) {
        _globality = newState;
    }

    public virtual void OnPivotChange(PivotState oldState, PivotState newState) {
        objectRenderer = slave.GetComponent<Renderer>();
        _pivotState = newState;
    }


    public abstract void HideVisuals();
    public abstract void ShowVisuals();
}
